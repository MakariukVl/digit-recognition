using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;
using IO = System.IO;
using AForge.Neuro;
using AForge.Neuro.Learning;
using System.Windows.Forms.DataVisualization.Charting;


namespace lab3_v2
{
    public partial class MultilayerForm : Form
    {
        // Constants
        internal readonly PictureSize _pictureSize = new PictureSize { Height = 28, Width = 28 };

        // Mixed Images Order
        private MixOrder _trainingOrder;           // Структура-словник яка визначає порядок зчитування тренувальних зображень

        // Елементи AForge.NET
        ActivationNetwork _network;

        // Елементи для _learning 
        ManualResetEvent _pauseResetEvent;     // .Set() - для продовження, .Reset() - для паузи
        bool _isPause;  //Пауза для кнопки старт. Щоб знати що Старт натиснута другий раз (Пауза)
        // Для асинхронного використання, юзай if(_isPause) _pauseResetEvent.WaitOne(); в дочірніх потоках (в місці для паузи) 
        

        public MultilayerForm()
        {
            InitializeComponent();
            InitializeDefaults();
            // Назначання подій для віртуалізації ListView 1.listTraining 2.listTesting
            listTraining.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listTrainingRecieve);
            listTesting.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listTestingRecieve);
            chartLearning.Series.Clear();
        }

        private void InitializeDefaults()
        {
            // Папка зображень для навчання за-замовченням
            tbTrainingDIR.Text = IO.Directory.GetCurrentDirectory() + @"\MNIST_data\train\600";
            // Перемішування - поки не використовуємо, але оголошуємо
            _trainingOrder = new MixOrder { isMixed = false, ORDER = new Dictionary<int, string>() };
            // Вибір папки зображень для навчання по-замовченню
            folderSelectTraining.SelectedPath = tbTrainingDIR.Text;
            folderSelectTraining.RootFolder = Environment.SpecialFolder.MyComputer;

            // Папка зображень для тестування за-замовченням
            tbTestingDIR.Text = IO.Directory.GetCurrentDirectory() + @"\MNIST_data\test\100";
            // Вибір папки зображень для навчання по-замовченню
            folderSelectTesting.SelectedPath = tbTestingDIR.Text;
            folderSelectTesting.RootFolder = Environment.SpecialFolder.MyComputer;
            // Пауза
            _pauseResetEvent = new ManualResetEvent(false); //на паузі. Виклич .Reset() для того щоб поставити на паузу
            _isPause = true;                             //на паузі
        }

        private void InitListView(ListView lv)
        {
            lv.View = View.LargeIcon;
            lv.VirtualMode = true;
        }

        /// <summary>
        /// Метод обробляє зображення, визначаючи яскравість його пікселів
        /// </summary>
        /// <param name="image">зображення. Можна брати з ImageList</param>
        /// <returns>повертає массив яскравостей, нормалізований в межах (-1.0, 1.0)</returns>
        private double[] ImageToBrightnessArray(Image image)
        {
            List<double> brightnessArray = new List<double>();
            Bitmap bitmap = new Bitmap(image);
            int maxY = _pictureSize.Height;
            int maxX = _pictureSize.Width;

            for (int pixelY = 0; pixelY < maxY; pixelY++)
            {
                for (int pixelX = 0; pixelX < maxX; pixelX++)
                {
                    // зчитуємо яскравість пікселя
                    double pixelBrightness = Convert.ToDouble(
                        bitmap.GetPixel(pixelX, pixelY).GetBrightness()
                        );
                    // Нормалізуємо в (-1.0, 1.0)
                    pixelBrightness = (pixelBrightness - 0.5) * 2;    
                    brightnessArray.Add(pixelBrightness);
                }
            }
            return brightnessArray.ToArray();
        }

        /// <summary>
        /// Завантажує всі зображення формату *png у каталозі у ImageList. 
        /// Безпечно запускати ассинхронно, але нема підтримки функції "відмінити асинхронне виконання".
        /// Зате є підтримка звітів прогресу виконання.
        /// </summary>
        /// <param name="pathDIR">шлях до каталогу. Не перевіряється</param>
        /// <remarks>Перевірте pathDIR на існування папки у файловій системі, перш ніж передавати у параметри</remarks>
        /// <returns>ImageList з усіма зображеннями *png. Пошук можна здійснювати по словнику, ключ-ім'я файлу із зображенням (без .png)</returns>
        private ImageList LoadImagesAsync(string pathDIR, BackgroundWorker worker) //потрібно перевірити валідність pathDIR перед викликом LoadImages
        {
            int highestProgressReached = 0;
            ImageList result = new ImageList();
            result.ColorDepth = ColorDepth.Depth8Bit;
            result.ImageSize = new Size(_pictureSize.Width, _pictureSize.Height);
            result.TransparentColor = Color.Transparent;

            string[] files = IO.Directory.GetFiles(pathDIR, "*.png", IO.SearchOption.TopDirectoryOnly);
            if ((files == null) || (files.Length <= 0))
            {
                return result; //is NULL or Empty
            }

            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                // Парсимо шлях до файла 
                string[] splpath = file.Split('\\');
                string key = splpath[splpath.Length - 1].Split('.')[0];     // Ключ - назва файлу (зображення)
                result.Images.Add(key, Image.FromFile(file));
                // Звіт про прогресс виконання 
                // доступний з події bwTrainingLoaderAsync_ProgressChanged
                int completeProgress =
                    (int)((float)i / (float)(files.Length - 1) * 100);  // Progress: 1-100%
                if (completeProgress > highestProgressReached)
                {
                    highestProgressReached = completeProgress;
                    worker.ReportProgress(completeProgress);
                }
            }
            return result;
        }

        private Dictionary<int, string> MixImagesAsync()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            Random rand = new Random();

            // Заповнюємо порядковий словник НЕЗМІШАНИМИ значеннями
            for (int i = 0; i < imglistTraning.Images.Count; i++)
            {                                                   // ключ (порядок) | назва файлу
                result.Add(i, imglistTraning.Images.Keys[i]);   // ---------------+-------------
            }                                                   //     int        | string
            // Змішуємо їх
            for (int i = result.Count - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);
                //обміняти значення
                string temp = result[j];
                result[j] = result[i];
                result[i] = temp;
            }
            return result;
        }

        /// <summary>
        /// Метод звіряє тестову вибірку з виводом нейромережі. Повертає
        /// список неправильно розпізнаних зображень.
        /// </summary>
        /// <remarks>
        /// Перед викликом слід впевнитись що:
        /// 1. існує нейромережа _network
        /// 2. існує imglistTesting (і в ньому є зображення)
        /// </remarks>
        /// <returns>список неправильно розпізнаних зображень</returns>
        private ImageList CheckUnrecAsync()
        {
            //int unrecCount = 0;
            ImageList unrecImages = new ImageList();
            unrecImages.ColorDepth = ColorDepth.Depth8Bit;
            unrecImages.ImageSize = new Size(_pictureSize.Width, _pictureSize.Height);
            unrecImages.TransparentColor = Color.Transparent;

            for (int indexTest = 0; indexTest < imglistTesting.Images.Count; indexTest++)
            {
                // Перетворюємо зображення у представлення для нейромережі (на вхід Compute)
                Image image = imglistTesting.Images[indexTest];
                string key = imglistTesting.Images.Keys[indexTest];
                double[] brightnessArr = ImageToBrightnessArray(image);
                // Показуємо зображення нейромережі та дивимось як вона його розпізнала
                int neuroGuess;
                double[] neuroGuessArr = _network.Compute(brightnessArr);
                MostProbableNumber(neuroGuessArr, out neuroGuess);  // Представлення нейромережі приводим до ЛЮДСЬКОГО
                // Порівнюємо здогадки нейромережі з правильними відповідями
                int dogma = KeyToNumber(key);
                if (neuroGuess != dogma) unrecImages.Images.Add(key, image);  //unrecCount++
            }
            return unrecImages;     //unrecCount;
        }

        private int KeyToNumber(string key)
        {
            int result;
            if (Int32.TryParse(key[0].ToString(), out result))
                return result;
            else
                return -1;
        }

        /// <summary>
        /// Представляє цифру (0,1,2,..9) як вивід нейромережі, а саме
        /// массив double[], де +1 на відповідному нейроні ({1,-1,..,-1}, {-1,1,..,-1},..{-1,..,-1,1})
        /// визначає цифру (бо +1 - найбільша імовірність (діапазон: -1..1)), а на
        /// всіх інших нейронах -1 (найменша імовірність), 
        /// тобто цифра 3 буде {-1,-1,-1,1,-1,-1,-1,-1,-1,-1}:
        ///     0% - 0
        ///     0% - 1
        ///     0% - 2
        ///   100% - 3
        ///     0% - 4
        ///     0% - 5
        ///     0% - 6
        ///     0% - 7
        ///     0% - 8
        ///     0% - 9
        /// </summary>
        /// <param name="number">Число, представлене по-людьськи</param>
        /// <returns>Число, представлене для нейромережі</returns>
        private double[] RepresentNumberAsNeuroOutput(int number)
        {
            if (number < 0 || number >= 10) 
                throw new ArgumentException("Аргумент має недопустиме значення, допустимi вiд 0 до 9", "number");
            double[] neuroOutput = new double[10];  // 10 - к-ть нейронів вихідного шару нейромережі

            for (int neuron = 0; neuron < 10; neuron++) // перебираєм нейрони як цифри від 0 до 9 (0,1,2,..9)
            {
                if (number == neuron) neuroOutput[neuron] = 1;  // +1 найвища ймовірність, 100%
                else neuroOutput[neuron] = -1;                  // -1 найменша ймовірність, 0%
            }
            return neuroOutput;
        }

        /// <summary>
        /// Цей метод бере вивід нейромережі (не по-людьськи закодований) та визначає
        /// яку цифру розпізнала нейромережа (по максимальній ймовірності). 
        /// Якщо максимальних ймовірностей кілька то пріорітет має більша цифра над меншою.
        /// </summary>
        /// <param name="networkOutput">вивід з нейромережі, у якому знаходяться ймовірності на кожну цифру</param>
        /// <param name="mostProbable">найбільш імовірна цифра</param>
        private void MostProbableNumber(double[] networkOutput, out int mostProbable)
        {
            if (networkOutput == null) throw new ArgumentNullException("networkOutput", "Аргумент має неприпустиме значення NULL.");
            if (networkOutput.Length != 10) throw
                new ArgumentException("Неприпустима довжина аргумента-массива, припустима рiвна 10.", "networkOutput");
            double maxProbability;     // ймовірність, діапазон -1..+1 (min,max)
            // визначаємо максимальну ймовірність, запам'ятовуємо її індекс (це розпізнана цифра)
            maxProbability = networkOutput[0];
            mostProbable = 0;
            for (int neuron = 1; neuron < 10; neuron++)
            {
                if (networkOutput[neuron] >= maxProbability)
                {
                    maxProbability = networkOutput[neuron];
                    mostProbable = neuron;
                }
            }
        }

        /// <summary>
        /// Цей метод бере вивід нейромережі (не по-людьськи закодований) та визначає 
        /// дві найбільш імовірні цифри які розпізнала нейромережа та їх імовірності.
        /// Якщо максимальних ймовірностей кілька то пріорітет має більша цифра над меншою.
        /// </summary>
        /// <param name="networkOutput">вивід з нейромережі, у якому знаходяться ймовірності на кожну цифру</param>
        /// <param name="firstProbable">перша найбільш імовірний вивід</param>
        /// <param name="secondProbable">друга найбільш імовірний вивід</param>
        private void MostProbableNumber(double[] networkOutput, out NeuroOut firstProbable, out NeuroOut secondProbable)
        {
            if (networkOutput == null) throw new ArgumentNullException("networkOutput", "Аргумент має неприпустиме значення NULL.");
            if (networkOutput.Length != 10) throw
                new ArgumentException("Неприпустима довжина аргумента-массива, припустима рiвна 10.", "networkOutput");
            int firstNum, secondNum;                    // перша і друга найбільша по ймовірності цифра
            double firstProbability, secondProbability; // перша і друга ймовірність, діапазон -1..+1 (min,max)
            // визначаємо першу і другу ймовірність, запам'ятовуємо їх індекси (це розпізнані цифри)
            firstProbability = networkOutput[0];
            firstNum = 0;
            // Перший прохід визначає first Most Probable Number
            for (int neuron = 1; neuron < 10; neuron++)
            {
                if (networkOutput[neuron] >= firstProbability)
                {
                    firstProbability = networkOutput[neuron];
                    firstNum = neuron;
                }
            }
            // Другий прохід визначає first Most Probable Number
            if (firstNum != 0) { secondNum = 0; secondProbability = networkOutput[0]; }
            else { secondNum = 1; secondProbability = networkOutput[1]; }
            for (int neuron = 0; neuron < 10; neuron++)
            {
                if ((networkOutput[neuron] >= secondProbability) && (neuron != firstNum))
                {
                    secondProbability = networkOutput[neuron];
                    secondNum = neuron;
                }
            }
            firstProbable = new NeuroOut { Number = firstNum, Probability = firstProbability };
            secondProbable = new NeuroOut { Number = secondNum, Probability = secondProbability };
        }


        #region Асинхронний завантажувач ТРЕНУВАЛЬНОЇ вибірки
        // Ассинхронний завантажувач ImageList вміє
        // виконувати завантаження зображень у іншому потоці, ніж форма,
        // забезпечуючи її більшу відклікуваність, керує доступом
        // до контролів забезпечуючи потокову безпечність. 
        // Оперативно відображає як стан процесу завантаження так і результат,
        // також є підтримка змішування шляхом використання структури
        // _trainingOrder та її словника ORDER, які теж своєчасно
        // створюються та оновлюються.
        private void bwTrainingLoaderAsync_DoWork(object sender, DoWorkEventArgs e)
        {
            // отримуєм BackgroundWorker, викликаний цією подією
            BackgroundWorker worker = sender as BackgroundWorker;
            // встановлюємо результат обчислень який буде
            // доступний з події bwTrainingLoaderAsync_RunWorkerCompleted
            e.Result = LoadImagesAsync((string)e.Argument, worker);
        }

        private void bwTrainingLoaderAsync_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressTraining.Value = e.ProgressPercentage;
        }

        private void bwTrainingLoaderAsync_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Спершу обробляємо помилки
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else
            {
                // Далі обробляємо випадок у якому фоновий процесс виконано успішно
                ImageList result = e.Result as ImageList;
                int pictureCount = result.Images.Count;
                tbTrainingPicCount.Text = pictureCount.ToString();                
                Thread.Sleep(100); 
                // Копіюємо посилання, щоб використати переваги фонового виконання
                listTraining.Enabled = false;   //Блокуємо listTraining перед модифікуванням VirtualListSize та imglistTraning
                listTraining.LargeImageList = result;
                imglistTraning = result;
                listTraining.VirtualListSize = pictureCount;
                InitListView(listTraining);
                listTraining.Enabled = true;    //Розблоковуємо listTraining                
            }
            // ANYWAY, розблокуємо кнопки-завантажувачі listTraining
            btnTrainingMassEnable(true);
        }

        // Обробники натискань кнопок
        private void btnTrainingLoad_Click(object sender, EventArgs e)
        {
            // Цей метод завантажує зображення з тієї папки,
            // яка вказана в TextBox
            if (IO.Directory.Exists(tbTrainingDIR.Text))
            {
                // блокуємо КНОПКИ-завантажувачі listTraining
                btnTrainingMassEnable(false);
                listTraining.Focus();
                // Ставимо флаг що вибірка НЕ перемішана i чистимо массив 'ПОРЯДОК елементів'
                _trainingOrder.isMixed = false;
                _trainingOrder.ORDER.Clear();
                // отримуєм шлях до папки з TextBox
                string pathDIR = tbTrainingDIR.Text;
                // запускаємо операцію у фоновому режимі
                bwTrainingLoaderAsync.RunWorkerAsync(pathDIR);
            }
            else return;
        }

        private void btnTrainingSelect_Click(object sender, EventArgs e)
        {
            // Цей метод завантажує зображення з тієї папки,
            // яку користувач обере у діалоговому вікні
            if (folderSelectTraining.ShowDialog() == DialogResult.OK)
            {
                if (IO.Directory.Exists(folderSelectTraining.SelectedPath))
                {
                    // блокуємо КНОПКИ-завантажувачі listTraining
                    btnTrainingMassEnable(false);
                    listTraining.Focus();
                    // Ставимо флаг що вибірка не змішана i чистимо порядковий словник
                    _trainingOrder.isMixed = false;
                    _trainingOrder.ORDER.Clear();
                    // отримуєм шлях до папки (вибраний у діалоговому вікні)
                    string pathDIR = folderSelectTraining.SelectedPath;
                    // записуємо шлях у TextBox
                    tbTrainingDIR.Text = pathDIR;
                    // запускаємо операцію у фоновому режимі
                    bwTrainingLoaderAsync.RunWorkerAsync(pathDIR);
                }
                else return;
            }
        }

        // Режим віртуалізації ListdView - обробники подій
        private void listTrainingRecieve(object sender, RetrieveVirtualItemEventArgs e)
        {
            // тут ми динамічно повертаємо ListViewItem           
            string itemText;
            if (_trainingOrder.isMixed) // 2 режими роботи
            {
                // Перемішані
                itemText = KeyToNumber(_trainingOrder.ORDER[e.ItemIndex]).ToString();
                int imageindex = imglistTraning.Images.IndexOfKey(_trainingOrder.ORDER[e.ItemIndex]);
                e.Item = new ListViewItem(itemText, imageindex);
            }
            else
            {
                // Неперемішані
                itemText = KeyToNumber(imglistTraning.Images.Keys[e.ItemIndex]).ToString();     //НЕ ПРАЦЮЄ З КНОПКОЮ "MIX"
                e.Item = new ListViewItem(itemText, e.ItemIndex);                               //ЦЕ ТАКОЖ                
            }           
        }

        // Блокувач/Розблоковувач кнопок для уникнення помилок ассинхронного виконання
        private void btnTrainingMassEnable(bool enable)
        {
            btnTrainingLoad.Enabled = enable;
            btnTrainingMix.Enabled = enable;
            btnTrainingSelect.Enabled = enable;
        }
        #endregion


        #region Асинхронний перемішувач
        // Ассинхронний перемішувач
        private void bwMix_DoWork(object sender, DoWorkEventArgs e)
        {
            // отримуєм BackgroundWorker, викликаний цією подією
            BackgroundWorker worker = sender as BackgroundWorker;
            // встановлюємо результат обчислень який буде
            // доступний з події bwMix_RunWorkerCompleted
            e.Result = MixImagesAsync();
        }

        private void bwMix_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Спершу обробляємо помилки
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else
            {
                // Далі обробляємо випадок у якому фоновий процесс виконано успішно
                Dictionary<int, string> result = e.Result as Dictionary<int, string>;
                _trainingOrder.ORDER = result;
                _trainingOrder.isMixed = true;              
                listTraining.EnsureVisible(0);
                listTraining.RedrawItems(0, 17, false);
            }
            // ANYWAY, розблокуємо кнопки-завантажувачі listTraining
            btnTrainingMassEnable(true);
        }

        // Обробник натискань кнопки
        private void btnTrainingMix_Click(object sender, EventArgs e)
        {
            // блокуємо кнопки-завантажувачі listTraining
            btnTrainingMassEnable(false);
            listTraining.Focus();
            // запускаємо операцію у фоновому режимі
            bwMix.RunWorkerAsync();
        }
        #endregion


        #region Асинхронний завантажувач вибірки ТЕСТУВАННЯ
        // Ассинхронний завантажувач ImageList вміє
        // виконувати завантаження зображень у іншому потоці, ніж форма,
        // забезпечуючи її більшу відклікуваність, керує доступом
        // до контролів забезпечуючи потокову безпечність. 
        // Оперативно відображає як стан процесу завантаження так і результат.
        // Вибірка для тестування НЕ ПЕРЕМІШУЄТЬСЯ.
        private void bwTestingLoaderAsync_DoWork(object sender, DoWorkEventArgs e)
        {
            // отримуєм BackgroundWorker, викликаний цією подією
            BackgroundWorker worker = sender as BackgroundWorker;
            // встановлюємо результат обчислень який буде
            // доступний з події bwTestingLoaderAsync_RunWorkerCompleted
            e.Result = LoadImagesAsync((string)e.Argument, worker);
        }

        private void bwTestingLoaderAsync_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressTesting.Value = e.ProgressPercentage;
        }

        private void bwTestingLoaderAsync_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Спершу обробляємо помилки
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else
            {
                // Далі обробляємо випадок у якому фоновий процесс виконано успішно
                ImageList result = e.Result as ImageList;
                int pictureCount = result.Images.Count;
                tbTestingPicCount.Text = pictureCount.ToString();
                tbUnrecOnly.Text = pictureCount.ToString();
                Thread.Sleep(100);
                // Копіюємо посилання, щоб використати переваги фонового виконання
                listTesting.Enabled = false;    //Блокуємо listTesting перед модифікуванням VirtualListSize та imglistTesting
                listTesting.LargeImageList = result;
                imglistTesting = result;
                listTesting.VirtualListSize = pictureCount;
                InitListView(listTesting); 
                listTesting.Enabled = true;     //Розблоковуємо listTesting
                //SetTestImagelist(result);
                // Тестуємо Нейромережу якщо є нейромережа
                if (_network != null) bwUnrec.RunWorkerAsync();
            }
            // ANYWAY, розблокуємо кнопки-завантажувачі listTesting
            btnTestingMassEnable(true);
        }

        // Обробники натискань кнопок
        private void btnTestingLoad_Click(object sender, EventArgs e)
        {
            // Цей метод завантажує зображення з тієї папки,
            // яка вказана в TextBox
            if (IO.Directory.Exists(tbTestingDIR.Text))
            {
                // блокуємо КНОПКИ-завантажувачі listTesting
                btnTestingMassEnable(false);
                listTesting.Focus();
                // отримуєм шлях до папки з TextBox
                string pathDIR = tbTestingDIR.Text;
                // запускаємо операцію у фоновому режимі
                bwTestingLoaderAsync.RunWorkerAsync(pathDIR);
            }
            else return;
        }

        private void btnTestingSelect_Click(object sender, EventArgs e)
        {
            // Цей метод завантажує зображення з тієї папки,
            // яку користувач обере у діалоговому вікні
            if (folderSelectTesting.ShowDialog() == DialogResult.OK)
            {
                if (IO.Directory.Exists(folderSelectTesting.SelectedPath))
                {
                    // блокуємо КНОПКИ-завантажувачі listTesting
                    btnTestingMassEnable(false);
                    listTesting.Focus();
                    // отримуєм шлях до папки (вибраний у діалоговому вікні)
                    string pathDIR = folderSelectTesting.SelectedPath;
                    // записуємо шлях у TextBox
                    tbTestingDIR.Text = pathDIR;
                    // запускаємо операцію у фоновому режимі
                    bwTestingLoaderAsync.RunWorkerAsync(pathDIR);
                }
                else return;
            }
        }

        // Режим віртуалізації GridView - обробники подій
        private void listTestingRecieve(object sender, RetrieveVirtualItemEventArgs e)
        {
            // Тут, у listTesting, вивід трішки відрізняється від того що був у listTraning
            // він буде виглядати 
            //     так: 0 : ?
            // або так: 3 : 6(-0.3), 3(-0.3)

            // тут ми динамічно повертаємо ListViewItem
            // listTestingRecieve працює у 3-х режимах               
            if ((listTesting.LargeImageList != null) && (listTesting.LargeImageList.Images.Count > 0))  // Є тестова вибірка (not null or empty)
            {
                string itemText;
                if (_network != null)   // Є нейромережа
                {
                    // Вивід виглядає так: '3 : 6(-0.3), 3(-0.3)'
                    // Перетворюємо зображення у представлення для нейромережі (на вхід Compute)
                    Image image = listTesting.LargeImageList.Images[e.ItemIndex]; 
                    double[] brightnessArr = ImageToBrightnessArray(image);
                    // Показуємо зображення нейромережі та дивимось як вона його розпізнала
                    NeuroOut first, second;
                    double[] neuroGuessArr = _network.Compute(brightnessArr);
                    MostProbableNumber(neuroGuessArr, out first, out second);   // Представлення нейромережі приводим до ЛЮДСЬКОГО
                    // Повертаємо VirtualItem
                    itemText = KeyToNumber(listTesting.LargeImageList.Images.Keys[e.ItemIndex]).ToString();
                    itemText = String.Format("{0} : {1}({2}), {3}({4})", itemText,
                        first.Number, first.Probability.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture),
                        second.Number, second.Probability.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture));
                    e.Item = new ListViewItem(itemText, e.ItemIndex);
                    return;
                    
                }
                else // НЕМА нейромережі
                {
                    // Вивід виглядає так: '0 : ?'
                    itemText = KeyToNumber(imglistTesting.Images.Keys[e.ItemIndex]).ToString();
                    itemText = String.Format("{0} : ?", itemText);
                    e.Item = new ListViewItem(itemText, e.ItemIndex);
                }
            }
            else
            {
                //EMPTY
                e.Item = new ListViewItem();
            }
        }

        // Блокувач/Розблоковувач кнопок для уникнення помилок ассинхронного виконання
        private void btnTestingMassEnable(bool enable)
        {
            btnTestingLoad.Enabled = enable;
            btnTestingSelect.Enabled = enable;
        }
        #endregion


        #region Однопоточний Створювач/Завантажувач/Зберігач НЕЙРОМЕРЕЖІ з АСИНХРОННИМ тестувальником
        // Однопоточний Створювач/Завантажувач/Зберігач НЕЙРОМЕРЕЖІ виконує
        // операції створення, завантаження та зберігання нейромережі
        // у томуж потоці що й головна форма. Також здійснює ТЕСТУВАННЯ
        // якщо завантажена вибірка ТЕСТУВАННЯ і завантажена(або створена) НЕЙРОМЕРЕЖА.
        // Тоді тестування треба узгодити з listTestingRecieve (якимось флагом isNetwork, isTesting), 
        // дізнатись які елементи видимі та перемалювати їх у самому методі (за допомогою 
        // listTesting.RedrawItems(0, 17, false)) Testing
        // Вивід у listTesting, буде трішки відрізняється від того що був у listTraning
            // він буде виглядати 
            //     так: 0 : ?
            // або так: 3 : 6(-0.3), 3(-0.3)

        // Обробники подій кнопок
        private void btnCreateNeuro_Click(object sender, EventArgs e)
        {
            /*float min = Convert.ToSingle(
                textBox_NeuroRangeMin.Text);
            float max = Convert.ToSingle(
                textBox_NeuroRangeMax.Text);
            double alpha = Convert.ToDouble(
                textBox_NeuroAlpha.Text);
            double lernrate = Convert.ToDouble(
                textBox_LearningRate.Text);         //LEARNING..
            double moment = Convert.ToDouble(
                textBox_Momentum.Text);             //LEARNING..
             * 
            //double learnrate, momentum - відноситься до learning
             * 
            network = new MultilayerClassifier(
                alpha, arch,
                min, max,
                lernrate,
                moment);

            button_NeuroSave.Enabled = true;
            button_Learning.Enabled = true;*/

            // Розпарсимо архітектуру мережі
            string architect = tbArchitect.Text;
            int[] iLayers;      // вихідний массив для TryParseArchitect. 
            float min, max;
            double alpha;       //double learnrate, momentum - відноситься до learning
            // Якщо розпарситься успішно,то iLayers буде містити приховані рівні 
            // нейромережі + останній (вихідний) рівень у вигляді массиву int[]
            if (this.TryParseArchitect(architect, out iLayers)) 
            {
                if (float.TryParse(tbRangeMin.Text, out min))
                {
                    if (float.TryParse(tbRangeMax.Text, out max))
                    {
                        if (double.TryParse(tbAlpha.Text, out alpha))
                        {
                            //Розраховуємо к-ть входів нейромережі, виходячи з розмірів зображення (к-ть пікселів)
                            int inputLayer = 
                                _pictureSize.Height * _pictureSize.Width;  //вхідний шар (28x28)=784
                            // ДІАПАЗОНИ ПОЧАТКОВОЇ РАНДОМНОЇ ІНІЦІАЛІЗАЦІЇ ВАГ
                            Neuron.RandRange = new AForge.Range(min, max);
                            //Створення мережі                            
                            MyActivationNetwork nw = new MyActivationNetwork(
                                alpha,              //активаційна ф-ція - сигмоїдна (біполярна), крутизна-alpha
                                inputLayer,         //784: к-ть зважених входів (без threshold)
                                iLayers             //архітектура мережі : 784-...-10 (останній шар, 10, додається в TryParseArchitect)
                                );                  //припущення: існує хочаб один прихований шар  
                            _network = nw as ActivationNetwork;
                            // Розблоковуємо кнопки та чекбокси
                            LearningAbilitiesMassEnable(true);
                            // Тестимо нейромережу
                            // Чи не тестимо, а просто даєм команду listTesting перемалюватись (listTesting.RedrawItems(0, х, false))
                            // а там вже логіка listTestingRecieve буде працювати.
                            // ВИСНОВОК: потрібно написати таку логіку для listTestingRecieve
                            // НА ДОДАЧУ потрібно передати результати тесту у два текстбокси
                            // tbUnrecOnly-(Count) і tbTestingRate-(%)

                            // Цей шмат коду треба перенести у окремий метод і викликати в різних місцях коду 
                            // (btnCreateNeuro_Click, btnLoadNeuro_Click, bwTestingLoaderAsync_RunWorkerCompleted) 
                            // в залежності від того у якій послідовності ми робим: 
                            // imglistTesting а потім _network чи навпаки
                            // мережу ми щойно створили (є), а imglistTesting перевірим IsNullOrEmpty
                            if ((imglistTesting.Images != null) && (imglistTesting.Images.Count > 0))
                            {
                                /*
                                // якщо є і тестовів зображення і нейромережа
                                int unrecCount = CheckUnrec();
                                // рахуємо % невірно розпізнаних
                                int testCount = imglistTesting.Images.Count;
                                double rate;
                                rate = ((double)unrecCount / (double)testCount) * 100d;
                                // виводимо результати на форму
                                tbTestingRate.Text = String.Format("{0,2:N1}", rate);
                                if (checkUnrecognizeOnly.Checked)
                                {
                                    // тільки нерозпізнанні
                                    tbUnrecOnly.Text = unrecCount.ToString();
                                }
                                else
                                {
                                    // всі, imglistTesting як відомо не NULL or Empty
                                    tbUnrecOnly.Text = testCount.ToString();
                                }*/
                                // запускаємо операцію ТЕСТУВАННЯ у фоновому режимі
                                bwUnrec.RunWorkerAsync();
                            }
                        }
                    }
                }
            }
        }

        private void btnLoadNeuro_Click(object sender, EventArgs e)
        {
            string file;
            string init_dir = IO.Directory.GetCurrentDirectory() + @"\MNIST_data";

            fileSelectNetwork.Filter = "Neuro binary files (*.bin)|*.bin|All files (*.*)|*.*";
            fileSelectNetwork.InitialDirectory = init_dir;
            if (fileSelectNetwork.ShowDialog() == DialogResult.OK)
            {
                // Те що файл існує і шлях корректний перевіряє Dialog автоматично але я всерівно перевірю ще раз
                file = fileSelectNetwork.FileName;
                if (IO.File.Exists(file))
                {
                    // Спроба завантажити у форматі MyActivationNetwork
                    MyActivationNetwork nw = Network.Load(file) as MyActivationNetwork;
                    _network = nw as ActivationNetwork;
                    // Якщо спроба не вдалась завантажуємо у форматі ActivationNetwork, але тоді нам невідома альфа
                    if (nw == null)
                    {
                        _network = Network.Load(file) as ActivationNetwork;
                    }
                    // виводимо архітектуру завантаженої мережі та альфу на форму
                    string arch = "";
                    for (int layer = 0; layer < _network.Layers.Length - 1; layer++)
                    {
                        if (layer == 0) arch = _network.Layers[0].Neurons.Length.ToString();
                        else arch += "-" + _network.Layers[layer].Neurons.Length.ToString();
                    }
                    tbArchitect.Text = arch;
                    try { tbAlpha.Text = nw.Alpha.ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture); }
                    catch { /*Not Valid Format*/ }
                    // Розблоковуємо кнопки та чекбокси
                    LearningAbilitiesMassEnable(true);
                    if ((imglistTesting.Images != null) && (imglistTesting.Images.Count > 0))
                    {
                        // запускаємо операцію ТЕСТУВАННЯ у фоновому режимі
                        bwUnrec.RunWorkerAsync();
                    }
                }
            }
        }

        private void btnSaveNeuro_Click(object sender, EventArgs e)
        {
            fileSaveNetwork.Filter = "Neuro binary files (*.bin)|*.bin|All files (*.*)|*.*";
            fileSaveNetwork.InitialDirectory = IO.Directory.GetCurrentDirectory() + @"\MNIST_data";
            fileSaveNetwork.FileName = IO.Directory.GetCurrentDirectory() + @"\MNIST_data\" + tbArchitect.Text;
            if (fileSaveNetwork.ShowDialog() == DialogResult.OK)
            {
                string file = fileSaveNetwork.FileName;
                MyActivationNetwork nw = _network as MyActivationNetwork;
                nw.Save(file);
            }
        }

        // Парсер Архітектури
        /// <summary>
        /// Парсить архітектуру і повертає массив int[] який містить
        /// окрім розпарсених прихованих шарів ще й останній шар (10 нейронів 
        /// (цифри 0,1,2,3,4,5,6,7,8,9))
        /// </summary>
        /// <param name="architect">Архітектура, у вихляді чисел розділених 
        /// дефісом '-'. Наприклад: 12-24, 1-2-3, чи без дефісу: 60</param>
        /// <param name="iLayers">Повертаємий массив int[], що є розпарсеною 
        /// строкою архітектури + останній шар (у кінці), який містить 10 нейронів</param>
        /// <returns>Успіх чи провал розпарсення, якщо успіх то iLayers має валідні 
        /// значення, якщо провал то iLayers не можна використовувати для
        /// створення мережі</returns>
        private bool TryParseArchitect(string architect, out int[] iLayers)
        {
            bool sucess = true;
            /*// PARSING NETWORK ARCHITECTURE
            string[] arch_parts = architecture.Split('-');
            int[] arch = new int[arch_parts.Length + 1];
            for (int hidden_layer = 0; hidden_layer < arch_parts.Length; hidden_layer++)
            {
                arch[hidden_layer] = Convert.ToInt32(
                    arch_parts[hidden_layer].Trim()
                    );
            }
            arch[arch_parts.Length] = 10;   // останній шар: 10 нейронів (цифри 0,1,2,3,4,5,6,7,8,9)
            // СТВОРЕННЯ МЕРЕЖІ
            inputs_per_neuron = 784;        //вхідний шар (28x28)=784
             */

            if (architect != String.Empty)
            {
                // Розпарсимо архітектуру мережі
                string[] sLayers = architect.Split('-');
                iLayers = new int[sLayers.Length + 1];
                //Проходимось по прихаваним шарам
                for (int layer = 0; layer < sLayers.Length; layer++)
                {
                    //Конвертуємо в int кожен прихований шар
                    if (!int.TryParse(sLayers[layer].Trim(), out iLayers[layer]))
                    {                       
                        return false;
                    }
                    //І не забуваємо про останній шар
                    iLayers[iLayers.Length - 1] = 10;   // останній шар: 10 нейронів (цифри 0,1,2,3,4,5,6,7,8,9)
                }
            }
            else
            {
                iLayers = new int[1] { 10 };
                return false;
            }
            return sucess;
        }
     

        // Асинхронний перевіряч нейромережі
        // Спробуємо зробити те що повністю робить CheckUnrec та частково btnCreateNeuro_Click 
        // АСИНХРОННИМ
        private void bwUnrec_DoWork(object sender, DoWorkEventArgs e)
        {
            // отримуєм BackgroundWorker, викликаний цією подією
            BackgroundWorker worker = sender as BackgroundWorker;
            // встановлюємо результат обчислень який буде
            // доступний з події bwUnrec_RunWorkerCompleted
            e.Result = CheckUnrecAsync();
        }

        private void bwUnrec_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Спершу обробляємо помилки
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else
            {
                // Далі обробляємо випадок у якому фоновий процесс виконано успішно
                // Відповідальнасть за те що:
                // 1. існує нейромережа _network
                // 2. існує imglistTesting (і в ньому є зображення)
                // лежить на программісті який користується bwTest

                // якщо є і тестовів зображення і нейромережа
                        //int unrecCount = (int)e.Result;
                imglistUnrec = e.Result as ImageList;
                int unrecCount = imglistUnrec.Images.Count;
                int testCount = imglistTesting.Images.Count;
                // виводимо результати тесту на форму
                tbSetUnrecText(testCount, unrecCount);
                // встановлюємо список зображень згідно з checkUnrecognizeOnly
                if (checkUnrecognizeOnly.Checked) SetTestImagelist(imglistUnrec);
                else SetTestImagelist(imglistTesting);
                // Redraw and set VirtualListSize              
                //RedrawVirtualItems(testCount, unrecCount);
            }
        }

        // Виводить % та к-ть зображень у listTesting на TextBox форми
        private void tbSetUnrecText(int testCount, int unrecCount)
        {
            double rate;
            // рахуємо % невірно розпізнаних
            rate = ((double)unrecCount / (double)testCount) * 100d;
            // виводимо результати на форму
            tbTestingRate.Text = String.Format("{0:0.0}", rate);
            tbUnrecOnly.Text = unrecCount.ToString();
        }

        // Перемальовувач ListView listTesting на формi AND SET VIRTUAL LIST SIZE
        // Він взагалі ПОТРІБЕН?
        private void RedrawVirtualItems(int testCount, int unrecCount)
        {
            // провсяк випадок блокуємо listTesting
            listTesting.Enabled = false;
            if (checkUnrecognizeOnly.Checked)
            {
                // тільки нерозпізнанні  
                listTesting.VirtualListSize = unrecCount;
                listTesting.EnsureVisible(0);
                if (unrecCount < 17) listTesting.RedrawItems(0, unrecCount - 1, false);
                else listTesting.RedrawItems(0, 17, false);
            }
            else
            {
                // всі 
                listTesting.VirtualListSize = testCount;
                listTesting.EnsureVisible(0);
                if (testCount < 17) listTesting.RedrawItems(0, testCount - 1, false);
                else listTesting.RedrawItems(0, 17, false);
            }
            // У будь-якому разі розблоковуємо listTesting
            listTesting.Enabled = true;
        }

        /// <summary>
        /// Змінює Imagelist та VirtualListSize для listView listTesting. 
        /// Зазвичай вибір йде між imglistTesting та imglistUnrec
        /// </summary>
        /// <param name="imglist">Imagelist для заміни в listTesting</param>
        private void SetTestImagelist(ImageList imglist)
        {
            // Testing or Unrec
            listTesting.Enabled = false;
            listTesting.LargeImageList = imglist;
            listTesting.VirtualListSize = imglist.Images.Count;
            listTesting.Enabled = true;
        }

        private void LearningAbilitiesMassEnable(bool enable)
        {
            // кнопки 'Зберегти нейромережу' та 'Навчання'
            btnSaveNeuro.Enabled = enable;
            btnLearning.Enabled = enable;
            // чекбокси групи "Критерії зупинки"             
            checkEpoch.Enabled = enable;
            checkTime.Enabled = enable;
            checkLearningError.Enabled = enable;
            checkTestError.Enabled = enable;
            // та ті що відносяться до Chart
            checkChart.Enabled = enable;
            checkMixingWhenLearning.Enabled = enable;
        }
        #endregion

        // Обробник галочки UnrecognizeOnly (CheckBox)
        private void checkUnrecognizeOnly_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox unrec = sender as CheckBox;
            // встановлюємо список зображень згідно з checkUnrecognizeOnly
            // Повторного тестування НЕ проводимо
            if (unrec.Checked)
            {
                if (imglistUnrec != null)
                {
                    SetTestImagelist(imglistUnrec);
                }
            }
            else
            {
                SetTestImagelist(imglistTesting);
            }
        }




        private ActivationNetwork LearnAsync(ActivationNetwork network, BackgroundWorker worker) //Як будемо network модифікувати? Invoke()?
        {
            double learnRate, momentum;
            int epoch_max, duration_max;
            TimeSpan tsDurationMax = new TimeSpan();
            double learnErr_max, testRate_max;

            //BackPropagationLearning learning;            
            bool learningReady = double.TryParse(tbLearningRate.Text, out learnRate);
            learningReady &= double.TryParse(tbMomentum.Text, out momentum);

            if (learningReady)
            {
                // Створюєм екземпляр BackPropagationLearning
                BackPropagationLearning _learning = new BackPropagationLearning(_network);
                _learning.LearningRate = learnRate;
                _learning.Momentum = momentum;

                // CURRENT Values
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                int epochCount = 1;
                double learnErr = 0d;
                double testRate = 101d;

                #region Тренувальні Набори Данних
                // СКЛАДАЄМО 1 РАЗ ТРЕНУВАЛЬНІ ПАРИ ВХІД/ВИХІД ДЛЯ МЕТОДУ RunEpoch() щоб одразу вчитися на всій епосі
                double[][] epochIN;     //Jagged Array IN
                double[][] epochOUT;    //Jagged Array OUT
                epochIN = new double[imglistTraning.Images.Count][];
                epochOUT = new double[imglistTraning.Images.Count][];
                for (int learnIndex = 0; learnIndex < imglistTraning.Images.Count; learnIndex++)
                {
                    Image image;
                    string key;
                    if (_trainingOrder.isMixed == false)
                    {
                        image = imglistTraning.Images[learnIndex]; //Неперемішані
                        key = imglistTraning.Images.Keys[learnIndex];
                    }
                    else
                    {
                        int mixIndex = imglistTraning.Images.Keys.IndexOf(_trainingOrder.ORDER[learnIndex]);    //Index Convert
                        image = imglistTraning.Images[mixIndex];    // Перемішані
                        key = imglistTraning.Images.Keys[mixIndex];
                    }
                    epochIN[learnIndex] = ImageToBrightnessArray(image);     //IN (index of image/pixel brightness line)
                    int dogma = KeyToNumber(key);   // Правильно розпізнана цифра
                    epochOUT[learnIndex] = RepresentNumberAsNeuroOutput(dogma);    //OUT (index of image/neuro output format)
                }
                #endregion

                while (true)
                {
                    sw.Start();
                    // ЗАВЖДИ ПЕРЕВІРЯЄМО ЩО В ТЕКСТБОКСАХ
                    bool timeReady = false;
                    try
                    {
                        tsDurationMax = TimeSpan.Parse(tbTime.Text);
                        duration_max = Convert.ToInt32(tsDurationMax.TotalMilliseconds);
                        timeReady = true;
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message);
                    }
                    bool epochReady = int.TryParse(tbEpoch.Text, out epoch_max);
                    bool learnErrReady = double.TryParse(tbLearningError.Text, out learnErr_max);
                    bool testErrReady = double.TryParse(tbTestingError.Text, out testRate_max);

                    // Start Learning
                    double e = _learning.RunEpoch(epochIN, epochOUT);
                    #region Навчання на виборці тренувальних зображень
                    /*int unrecLearnCount = 0;    //USELESS
                    // Прокручуємо 1-у епоху по 1 вхыдному зображенню
                    for (int learnIndex = 0; learnIndex < imglistTraning.Images.Count; learnIndex++)
                    {
                        // СКЛАДАЄМО ТРЕНУВАЛЬНІ ПАРИ ВХІД/ВИХІД ДЛЯ МЕТОДУ Run()
                        Image image;
                        string key;
                        if (_trainingOrder.isMixed == false)
                        {
                            image = imglistTraning.Images[learnIndex]; //Неперемішані
                            key = imglistTraning.Images.Keys[learnIndex];
                        }
                        else
                        {
                            int mixIndex = imglistTraning.Images.Keys.IndexOf(_trainingOrder.ORDER[learnIndex]);    //Index Convert
                            image = imglistTraning.Images[mixIndex];    // Перемішані
                            key = imglistTraning.Images.Keys[mixIndex];
                        }
                        double[] brightnessArr = ImageToBrightnessArray(image);     //IN
                        int dogma = KeyToNumber(key);   // Правильно розпізнана цифра
                        double[] dogmaArr = RepresentNumberAsNeuroOutput(dogma);    //OUT
                        // ПОЧИНАЄМО НАВЧАННЯ
                        _learning.Run(brightnessArr, dogmaArr);     // Навчання
                        // Рахуємо неспівпалі зображення (Після навчання)
                        int neuroGuess;                 // Цифра, розпізнана нейромережею
                        double[] neuroGuessArr = _network.Compute(brightnessArr);//USELESS
                        MostProbableNumber(neuroGuessArr, out neuroGuess);  // Представлення нейромережі приводим до ЛЮДСЬКОГО
                        // Порівнюємо здогадки нейромережі з правильними відповідями
                        if (neuroGuess != dogma) unrecLearnCount++; //USELESS
                    }*/
                    #endregion
                    // Time condition 
                    if (sw.IsRunning) sw.Stop();
                    // Get the elapsed time as a TimeSpan value.
                    TimeSpan tsElapsed = sw.Elapsed;
                    sw.Start();
                    if (checkTime.Checked && timeReady)
                    {
                        if (tsElapsed.TotalMilliseconds >= tsDurationMax.TotalMilliseconds)
                        {
                            // STOP Learning (но с возможностью возврата к обучению с того места в котором закончили)
                            _isPause = true;
                        }
                    }
                    // Epoch condition
                    if (checkEpoch.Checked && epochReady)
                    {
                        if (epochCount >= epoch_max)
                        {
                            // STOP Learning (но с возможностью возврата к обучению с того места в котором закончили)
                            _isPause = true;
                        }
                    }
                    // Learning Err condition 
                    learnErr = Math.Sqrt((2d * e) / (10d * imglistTraning.Images.Count));
                    //learnErr = Math.Sqrt((2d * unrecLearnCount) / (10d * imglistTraning.Images.Count));
                    if (checkLearningError.Checked && learnErrReady)
                    {
                        if (learnErr <= learnErr_max)
                        {
                            // STOP Learning (но с возможностью возврата к обучению с того места в котором закончили)
                            _isPause = true;
                        }
                    }
                    // Testing Err condition
                    if (checkTestError.Checked && testErrReady)
                    {
                        int unrecTestCount = 0;
                        for (int indexTest = 0; indexTest < imglistTesting.Images.Count; indexTest++)
                        {
                            // Перетворюємо зображення у представлення для нейромережі (на вхід Compute)
                            Image image = imglistTesting.Images[indexTest];
                            string key = imglistTesting.Images.Keys[indexTest];
                            double[] brightnessArr = ImageToBrightnessArray(image);
                            // Показуємо зображення нейромережі та дивимось як вона його розпізнала
                            int neuroGuess;
                            double[] neuroGuessArr = _network.Compute(brightnessArr);
                            MostProbableNumber(neuroGuessArr, out neuroGuess);  // Представлення нейромережі приводим до ЛЮДСЬКОГО
                            // Порівнюємо здогадки нейромережі з правильними відповідями
                            int dogma = KeyToNumber(key);
                            if (neuroGuess != dogma) unrecTestCount++;
                        }
                        // рахуємо % невірно розпізнаних
                        testRate = ((double)unrecTestCount / (double)imglistTesting.Images.Count) * 100d;
                        // Check                       
                        if (testRate <= testRate_max)
                        {
                            // STOP Learning (но с возможностью возврата к обучению с того места в котором закончили)
                            _isPause = true;
                        }
                    }
                    if (sw.IsRunning) sw.Stop();   // Треба перевірити чи зупинка потоку зупиняє відлік часу
                    // Формуємо данні для звіту на форму 
                    tsElapsed = sw.Elapsed;
                    CurrentLearningState currentState =
                        new CurrentLearningState() { Epochs = epochCount, Duration = tsElapsed, LearningErr = learnErr, TestingErr = testRate };
                    // Report
                    worker.ReportProgress(0, currentState);
                    // Якщо кнопка `Призупинити` натиснута або виконана якась з умов зупинки
                    if (_isPause)
                    {
                        _pauseResetEvent.Reset();     // На кнопку старт треба поставити .Set() i _isPause=!_isPause; щоб вийти з паузи
                        _pauseResetEvent.WaitOne();   // Ставимо на паузу (Якщо в нас _pauseResetEvent.Reset())
                    }
                    epochCount++;
                    // Перемішуємо вхідну збірку та перезбираємо пари вхід/вихід (Чому б просто не перемішати)                    
                    if (checkMixingWhenLearning.Checked)
                    {
                        sw.Start();
                        _trainingOrder.ORDER = MixImagesAsync();
                        _trainingOrder.isMixed = true;
                        epochIN = new double[imglistTraning.Images.Count][];
                        epochOUT = new double[imglistTraning.Images.Count][];
                        // Wait Mix procedure
                        while (bwMix.IsBusy)
                        { };
                        for (int learnIndex = 0; learnIndex < imglistTraning.Images.Count; learnIndex++)
                        {
                            Image image;
                            string key;
                            if (_trainingOrder.isMixed == false)
                            {
                                image = imglistTraning.Images[learnIndex]; //Неперемішані
                                key = imglistTraning.Images.Keys[learnIndex];
                            }
                            else
                            {
                                int mixIndex = imglistTraning.Images.Keys.IndexOf(_trainingOrder.ORDER[learnIndex]);    //Index Convert
                                image = imglistTraning.Images[mixIndex];    // Перемішані
                                key = imglistTraning.Images.Keys[mixIndex];
                            }
                            epochIN[learnIndex] = ImageToBrightnessArray(image);     //IN
                            int dogma = KeyToNumber(key);   // Правильно розпізнана цифра
                            epochOUT[learnIndex] = RepresentNumberAsNeuroOutput(dogma);    //OUT
                        }                                             
                        sw.Stop();
                    }
                }

            }    
            
            return network;
        }

        private void bwLearning_DoWork(object sender, DoWorkEventArgs e)
        {
            // отримуєм BackgroundWorker, викликаний цією подією
            BackgroundWorker worker = sender as BackgroundWorker;
            // встановлюємо результат обчислень який буде
            // доступний з події bwTestingLoaderAsync_RunWorkerCompleted
            e.Result = LearnAsync(_network, worker);
        }

        private void bwLearning_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Будемо через bwLearning_ProgressChanged передавати всю інфу на форму
            CurrentLearningState currentState =
                (CurrentLearningState)e.UserState;
            // Виводимо Текст
            tbEpochProgress.Text = currentState.Epochs.ToString("0", System.Globalization.CultureInfo.InvariantCulture);
            tbTimeProgress.Text = String.Format("{0:00}:{1:00}:{2:00}",
                currentState.Duration.Hours,
                currentState.Duration.Minutes,
                currentState.Duration.Seconds);
            tbLearningErrorProgress.Text = currentState.LearningErr.ToString("0.000", System.Globalization.CultureInfo.InvariantCulture);
            if (currentState.TestingErr <= 100)
                tbTestingErrorProgress.Text = currentState.TestingErr.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture);
            else
                tbTestingErrorProgress.Text = "";
            // Малюємо Графік
            if (checkChart.Checked == true)
            {
                DrawLearningEpoch(currentState);
            }
            
            if (_isPause == true)
            {
                // запускаємо операцію Фінальне ТЕСТУВАННЯ у фоновому режимі
                if ((imglistTesting.Images != null) && (imglistTesting.Images.Count > 0))
                    bwUnrec.RunWorkerAsync();
                // перемалюємо listTraining якщо було перемішування
                if (checkMixingWhenLearning.Checked)
                {
                    listTraining.EnsureVisible(0);
                    listTraining.RedrawItems(0, 17, false);
                }
                btnLearning.Text = "Навчати";
            }
        }

        private void bwLearning_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Цей код мабуть буде недосяжний
        }

        private void btnLearning_Click(object sender, EventArgs e)
        {
            if (!bwLearning.IsBusy) bwLearning.RunWorkerAsync();
            if (_isPause)   //запускаємо
            {
                btnLearning.Text = "Призупинити";
                _isPause = false;
                _pauseResetEvent.Set();
            }
            else            //зупиняємо
            {
                btnLearning.Text = "Навчати";
                _isPause = true;
                _pauseResetEvent.Reset();
            }
        }

        // Ініціалізує графік (за необхідності) та малює ОДНУ ТОЧКУ
        private void DrawLearningEpoch(CurrentLearningState learnState)
        {
            if ((chartLearning.Series == null) || (chartLearning.Series.Count <= 0))
            {
                //Init NEW
                chartLearning.Series.Clear();
                //chartLearning.ChartAreas.Clear();
                //Define Area
                //chartLearning.ChartAreas.Add("LearningStateArea");
                chartLearning.ChartAreas[0].Area3DStyle.Enable3D = false;
                chartLearning.ChartAreas[0].Area3DStyle.Perspective = 0;
                //New LearnErr Serie
                chartLearning.Series.Add("LearnErr");
                chartLearning.Series["LearnErr"].ChartType = SeriesChartType.FastLine;
                chartLearning.Series["LearnErr"].MarkerStyle = MarkerStyle.Circle;
                chartLearning.Series["LearnErr"].MarkerSize = 3;
                chartLearning.Series["LearnErr"].MarkerColor = System.Drawing.Color.DarkBlue;
                chartLearning.Series["LearnErr"].Color = System.Drawing.Color.DarkBlue;
                chartLearning.Series["LearnErr"].XAxisType = AxisType.Primary;
                chartLearning.Series["LearnErr"].YAxisType = AxisType.Primary;
                //New TestErr Serie
                chartLearning.Series.Add("TestErr");
                chartLearning.Series["TestErr"].ChartType = SeriesChartType.FastLine;
                chartLearning.Series["TestErr"].MarkerStyle = MarkerStyle.Circle;
                chartLearning.Series["TestErr"].MarkerSize = 3;
                chartLearning.Series["TestErr"].MarkerColor = System.Drawing.Color.Brown;
                chartLearning.Series["TestErr"].Color = System.Drawing.Color.Brown;
                chartLearning.Series["TestErr"].XAxisType = AxisType.Primary;
                chartLearning.Series["TestErr"].YAxisType = AxisType.Secondary;
                //Define Axis Title
                chartLearning.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font(FontFamily.GenericSansSerif, 10f, FontStyle.Bold);
                chartLearning.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.Black;
                chartLearning.ChartAreas[0].AxisX.Title = "Епоха";
                chartLearning.ChartAreas[0].AxisX.IsStartedFromZero = false;
                chartLearning.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font(FontFamily.GenericSansSerif, 10f, FontStyle.Bold);
                chartLearning.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.DarkBlue;
                chartLearning.ChartAreas[0].AxisY.Title = "Похибка на 1 вихід";
                chartLearning.ChartAreas[0].AxisY.IsStartedFromZero = false;
                chartLearning.ChartAreas[0].AxisY2.TitleFont = new System.Drawing.Font(FontFamily.GenericSansSerif, 10f, FontStyle.Bold);
                chartLearning.ChartAreas[0].AxisY2.TitleForeColor = System.Drawing.Color.Brown;
                chartLearning.ChartAreas[0].AxisY2.Title = "Частка помилок, %";
                //Striplines
                chartLearning.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chartLearning.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Blue;
                chartLearning.ChartAreas[0].AxisY.MajorGrid.LineWidth = 1;
                chartLearning.ChartAreas[0].AxisY2.MajorGrid.LineColor = System.Drawing.Color.DarkOrange;
                chartLearning.ChartAreas[0].AxisY2.MajorGrid.LineWidth = 1;
            }
            // DRAW ONE POINT
            chartLearning.Series["LearnErr"].Points.AddXY((double)learnState.Epochs, learnState.LearningErr);
            if (learnState.TestingErr <= 100d)
                chartLearning.Series["TestErr"].Points.AddXY((double)learnState.Epochs, learnState.TestingErr);
        }

        /*private bool _IsPause
        {
            get { return _isPause; }
            set
            {
                if (value)
                {
                    btnLearning.Text = "Навчати";    //зупиняємо
                    _pauseResetEvent.Reset();
                }
                else
                {
                    btnLearning.Text = "Призупинити";      //запускаємо
                    _pauseResetEvent.Set();
                }
                _isPause = value;
            }
        }*/

    }
}
