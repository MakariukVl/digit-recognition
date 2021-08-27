namespace lab3_v2
{
    partial class MultilayerForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.imglistTraning = new System.Windows.Forms.ImageList(this.components);
            this.listTraining = new System.Windows.Forms.ListView();
            this.listTesting = new System.Windows.Forms.ListView();
            this.imglistTesting = new System.Windows.Forms.ImageList(this.components);
            this.gbTrainingDIR = new System.Windows.Forms.GroupBox();
            this.progressTraining = new System.Windows.Forms.ProgressBar();
            this.btnTrainingMix = new System.Windows.Forms.Button();
            this.lbTrainingPicCount = new System.Windows.Forms.Label();
            this.tbTrainingPicCount = new System.Windows.Forms.TextBox();
            this.btnTrainingLoad = new System.Windows.Forms.Button();
            this.btnTrainingSelect = new System.Windows.Forms.Button();
            this.tbTrainingDIR = new System.Windows.Forms.TextBox();
            this.lbTrainingDIR = new System.Windows.Forms.Label();
            this.gbTestingDIR = new System.Windows.Forms.GroupBox();
            this.tbTestingRate = new System.Windows.Forms.TextBox();
            this.lbTestingRate = new System.Windows.Forms.Label();
            this.lbTestingPicCount = new System.Windows.Forms.Label();
            this.tbUnrecOnly = new System.Windows.Forms.TextBox();
            this.tbTestingPicCount = new System.Windows.Forms.TextBox();
            this.progressTesting = new System.Windows.Forms.ProgressBar();
            this.checkUnrecognizeOnly = new System.Windows.Forms.CheckBox();
            this.btnTestingLoad = new System.Windows.Forms.Button();
            this.btnTestingSelect = new System.Windows.Forms.Button();
            this.lbTestingDIR = new System.Windows.Forms.Label();
            this.tbTestingDIR = new System.Windows.Forms.TextBox();
            this.gbNetwork = new System.Windows.Forms.GroupBox();
            this.btnSaveNeuro = new System.Windows.Forms.Button();
            this.btnLoadNeuro = new System.Windows.Forms.Button();
            this.btnCreateNeuro = new System.Windows.Forms.Button();
            this.lbRangeDash = new System.Windows.Forms.Label();
            this.tbArchitect = new System.Windows.Forms.TextBox();
            this.tbRangeMax = new System.Windows.Forms.TextBox();
            this.tbAlpha = new System.Windows.Forms.TextBox();
            this.tbArchitectBackground = new System.Windows.Forms.TextBox();
            this.tbRangeMin = new System.Windows.Forms.TextBox();
            this.lbArchitect = new System.Windows.Forms.Label();
            this.lbAlpha = new System.Windows.Forms.Label();
            this.lbNeuroRange = new System.Windows.Forms.Label();
            this.gbProgress = new System.Windows.Forms.GroupBox();
            this.tbEpochProgress = new System.Windows.Forms.TextBox();
            this.tbTimeProgress = new System.Windows.Forms.TextBox();
            this.tbLearningErrorProgress = new System.Windows.Forms.TextBox();
            this.tbTestingErrorProgress = new System.Windows.Forms.TextBox();
            this.gbStopConditions = new System.Windows.Forms.GroupBox();
            this.tbTime = new System.Windows.Forms.MaskedTextBox();
            this.tbLearningError = new System.Windows.Forms.TextBox();
            this.tbEpoch = new System.Windows.Forms.TextBox();
            this.tbTestingError = new System.Windows.Forms.TextBox();
            this.checkEpoch = new System.Windows.Forms.CheckBox();
            this.checkTime = new System.Windows.Forms.CheckBox();
            this.checkLearningError = new System.Windows.Forms.CheckBox();
            this.checkTestError = new System.Windows.Forms.CheckBox();
            this.gbLearningParams = new System.Windows.Forms.GroupBox();
            this.lbMomentum = new System.Windows.Forms.Label();
            this.lbLearningRate = new System.Windows.Forms.Label();
            this.tbMomentum = new System.Windows.Forms.TextBox();
            this.tbLearningRate = new System.Windows.Forms.TextBox();
            this.checkMixingWhenLearning = new System.Windows.Forms.CheckBox();
            this.checkChart = new System.Windows.Forms.CheckBox();
            this.btnLearning = new System.Windows.Forms.Button();
            this.chartLearning = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.folderSelectTraining = new System.Windows.Forms.FolderBrowserDialog();
            this.folderSelectTesting = new System.Windows.Forms.FolderBrowserDialog();
            this.fileSelectNetwork = new System.Windows.Forms.OpenFileDialog();
            this.bwTrainingLoaderAsync = new System.ComponentModel.BackgroundWorker();
            this.bwTestingLoaderAsync = new System.ComponentModel.BackgroundWorker();
            this.bwMix = new System.ComponentModel.BackgroundWorker();
            this.fileSaveNetwork = new System.Windows.Forms.SaveFileDialog();
            this.bwUnrec = new System.ComponentModel.BackgroundWorker();
            this.imglistUnrec = new System.Windows.Forms.ImageList(this.components);
            this.bwLearning = new System.ComponentModel.BackgroundWorker();
            this.gbTrainingDIR.SuspendLayout();
            this.gbTestingDIR.SuspendLayout();
            this.gbNetwork.SuspendLayout();
            this.gbProgress.SuspendLayout();
            this.gbStopConditions.SuspendLayout();
            this.gbLearningParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartLearning)).BeginInit();
            this.SuspendLayout();
            // 
            // imglistTraning
            // 
            this.imglistTraning.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imglistTraning.ImageSize = new System.Drawing.Size(28, 28);
            this.imglistTraning.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listTraining
            // 
            this.listTraining.AutoArrange = false;
            this.listTraining.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listTraining.LabelWrap = false;
            this.listTraining.LargeImageList = this.imglistTraning;
            this.listTraining.Location = new System.Drawing.Point(9, 73);
            this.listTraining.MultiSelect = false;
            this.listTraining.Name = "listTraining";
            this.listTraining.ShowGroups = false;
            this.listTraining.Size = new System.Drawing.Size(464, 208);
            this.listTraining.TabIndex = 0;
            this.listTraining.TileSize = new System.Drawing.Size(110, 32);
            this.listTraining.UseCompatibleStateImageBehavior = false;
            // 
            // listTesting
            // 
            this.listTesting.AutoArrange = false;
            this.listTesting.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listTesting.LargeImageList = this.imglistTesting;
            this.listTesting.Location = new System.Drawing.Point(9, 75);
            this.listTesting.MultiSelect = false;
            this.listTesting.Name = "listTesting";
            this.listTesting.ShowGroups = false;
            this.listTesting.Size = new System.Drawing.Size(464, 208);
            this.listTesting.TabIndex = 1;
            this.listTesting.TileSize = new System.Drawing.Size(110, 32);
            this.listTesting.UseCompatibleStateImageBehavior = false;
            // 
            // imglistTesting
            // 
            this.imglistTesting.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imglistTesting.ImageSize = new System.Drawing.Size(28, 28);
            this.imglistTesting.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // gbTrainingDIR
            // 
            this.gbTrainingDIR.Controls.Add(this.progressTraining);
            this.gbTrainingDIR.Controls.Add(this.btnTrainingMix);
            this.gbTrainingDIR.Controls.Add(this.listTraining);
            this.gbTrainingDIR.Controls.Add(this.lbTrainingPicCount);
            this.gbTrainingDIR.Controls.Add(this.tbTrainingPicCount);
            this.gbTrainingDIR.Controls.Add(this.btnTrainingLoad);
            this.gbTrainingDIR.Controls.Add(this.btnTrainingSelect);
            this.gbTrainingDIR.Controls.Add(this.tbTrainingDIR);
            this.gbTrainingDIR.Controls.Add(this.lbTrainingDIR);
            this.gbTrainingDIR.Location = new System.Drawing.Point(12, 12);
            this.gbTrainingDIR.Name = "gbTrainingDIR";
            this.gbTrainingDIR.Size = new System.Drawing.Size(478, 292);
            this.gbTrainingDIR.TabIndex = 2;
            this.gbTrainingDIR.TabStop = false;
            this.gbTrainingDIR.Text = "Зображення для навчання";
            // 
            // progressTraining
            // 
            this.progressTraining.Location = new System.Drawing.Point(233, 45);
            this.progressTraining.Name = "progressTraining";
            this.progressTraining.Size = new System.Drawing.Size(239, 23);
            this.progressTraining.Step = 2;
            this.progressTraining.TabIndex = 7;
            // 
            // btnTrainingMix
            // 
            this.btnTrainingMix.Enabled = false;
            this.btnTrainingMix.Location = new System.Drawing.Point(154, 45);
            this.btnTrainingMix.Name = "btnTrainingMix";
            this.btnTrainingMix.Size = new System.Drawing.Size(75, 23);
            this.btnTrainingMix.TabIndex = 6;
            this.btnTrainingMix.Text = "Змішати";
            this.btnTrainingMix.UseVisualStyleBackColor = true;
            this.btnTrainingMix.Click += new System.EventHandler(this.btnTrainingMix_Click);
            // 
            // lbTrainingPicCount
            // 
            this.lbTrainingPicCount.AutoSize = true;
            this.lbTrainingPicCount.Location = new System.Drawing.Point(6, 50);
            this.lbTrainingPicCount.Name = "lbTrainingPicCount";
            this.lbTrainingPicCount.Size = new System.Drawing.Size(105, 13);
            this.lbTrainingPicCount.TabIndex = 5;
            this.lbTrainingPicCount.Text = "Всього зображень:";
            // 
            // tbTrainingPicCount
            // 
            this.tbTrainingPicCount.Location = new System.Drawing.Point(112, 47);
            this.tbTrainingPicCount.Name = "tbTrainingPicCount";
            this.tbTrainingPicCount.ReadOnly = true;
            this.tbTrainingPicCount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbTrainingPicCount.Size = new System.Drawing.Size(38, 20);
            this.tbTrainingPicCount.TabIndex = 4;
            this.tbTrainingPicCount.Text = "0";
            this.tbTrainingPicCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnTrainingLoad
            // 
            this.btnTrainingLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnTrainingLoad.Location = new System.Drawing.Point(386, 18);
            this.btnTrainingLoad.Name = "btnTrainingLoad";
            this.btnTrainingLoad.Size = new System.Drawing.Size(86, 23);
            this.btnTrainingLoad.TabIndex = 3;
            this.btnTrainingLoad.Text = "Завантажити";
            this.btnTrainingLoad.UseVisualStyleBackColor = true;
            this.btnTrainingLoad.Click += new System.EventHandler(this.btnTrainingLoad_Click);
            // 
            // btnTrainingSelect
            // 
            this.btnTrainingSelect.Location = new System.Drawing.Point(324, 19);
            this.btnTrainingSelect.Name = "btnTrainingSelect";
            this.btnTrainingSelect.Size = new System.Drawing.Size(56, 23);
            this.btnTrainingSelect.TabIndex = 2;
            this.btnTrainingSelect.Text = "Обрати";
            this.btnTrainingSelect.UseVisualStyleBackColor = true;
            this.btnTrainingSelect.Click += new System.EventHandler(this.btnTrainingSelect_Click);
            // 
            // tbTrainingDIR
            // 
            this.tbTrainingDIR.Location = new System.Drawing.Point(57, 20);
            this.tbTrainingDIR.Name = "tbTrainingDIR";
            this.tbTrainingDIR.Size = new System.Drawing.Size(261, 20);
            this.tbTrainingDIR.TabIndex = 1;
            // 
            // lbTrainingDIR
            // 
            this.lbTrainingDIR.AutoSize = true;
            this.lbTrainingDIR.Location = new System.Drawing.Point(6, 23);
            this.lbTrainingDIR.Name = "lbTrainingDIR";
            this.lbTrainingDIR.Size = new System.Drawing.Size(42, 13);
            this.lbTrainingDIR.TabIndex = 0;
            this.lbTrainingDIR.Text = "Папка:";
            // 
            // gbTestingDIR
            // 
            this.gbTestingDIR.Controls.Add(this.tbTestingRate);
            this.gbTestingDIR.Controls.Add(this.lbTestingRate);
            this.gbTestingDIR.Controls.Add(this.lbTestingPicCount);
            this.gbTestingDIR.Controls.Add(this.tbUnrecOnly);
            this.gbTestingDIR.Controls.Add(this.tbTestingPicCount);
            this.gbTestingDIR.Controls.Add(this.progressTesting);
            this.gbTestingDIR.Controls.Add(this.listTesting);
            this.gbTestingDIR.Controls.Add(this.checkUnrecognizeOnly);
            this.gbTestingDIR.Controls.Add(this.btnTestingLoad);
            this.gbTestingDIR.Controls.Add(this.btnTestingSelect);
            this.gbTestingDIR.Controls.Add(this.lbTestingDIR);
            this.gbTestingDIR.Controls.Add(this.tbTestingDIR);
            this.gbTestingDIR.Location = new System.Drawing.Point(12, 310);
            this.gbTestingDIR.Name = "gbTestingDIR";
            this.gbTestingDIR.Size = new System.Drawing.Size(478, 339);
            this.gbTestingDIR.TabIndex = 3;
            this.gbTestingDIR.TabStop = false;
            this.gbTestingDIR.Text = "Зображення для тестування";
            // 
            // tbTestingRate
            // 
            this.tbTestingRate.Location = new System.Drawing.Point(300, 296);
            this.tbTestingRate.Name = "tbTestingRate";
            this.tbTestingRate.ReadOnly = true;
            this.tbTestingRate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbTestingRate.Size = new System.Drawing.Size(38, 20);
            this.tbTestingRate.TabIndex = 12;
            this.tbTestingRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbTestingRate
            // 
            this.lbTestingRate.AutoSize = true;
            this.lbTestingRate.Location = new System.Drawing.Point(181, 299);
            this.lbTestingRate.Name = "lbTestingRate";
            this.lbTestingRate.Size = new System.Drawing.Size(113, 13);
            this.lbTestingRate.TabIndex = 13;
            this.lbTestingRate.Text = "Частка помилок у %:";
            // 
            // lbTestingPicCount
            // 
            this.lbTestingPicCount.AutoSize = true;
            this.lbTestingPicCount.Location = new System.Drawing.Point(6, 299);
            this.lbTestingPicCount.Name = "lbTestingPicCount";
            this.lbTestingPicCount.Size = new System.Drawing.Size(105, 13);
            this.lbTestingPicCount.TabIndex = 14;
            this.lbTestingPicCount.Text = "Всього зображень:";
            // 
            // tbUnrecOnly
            // 
            this.tbUnrecOnly.Location = new System.Drawing.Point(189, 49);
            this.tbUnrecOnly.Name = "tbUnrecOnly";
            this.tbUnrecOnly.ReadOnly = true;
            this.tbUnrecOnly.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbUnrecOnly.Size = new System.Drawing.Size(38, 20);
            this.tbUnrecOnly.TabIndex = 10;
            this.tbUnrecOnly.Text = "0";
            this.tbUnrecOnly.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbTestingPicCount
            // 
            this.tbTestingPicCount.Location = new System.Drawing.Point(111, 296);
            this.tbTestingPicCount.Name = "tbTestingPicCount";
            this.tbTestingPicCount.ReadOnly = true;
            this.tbTestingPicCount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbTestingPicCount.Size = new System.Drawing.Size(38, 20);
            this.tbTestingPicCount.TabIndex = 11;
            this.tbTestingPicCount.Text = "0";
            this.tbTestingPicCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // progressTesting
            // 
            this.progressTesting.Location = new System.Drawing.Point(233, 46);
            this.progressTesting.Name = "progressTesting";
            this.progressTesting.Size = new System.Drawing.Size(239, 23);
            this.progressTesting.Step = 2;
            this.progressTesting.TabIndex = 9;
            // 
            // checkUnrecognizeOnly
            // 
            this.checkUnrecognizeOnly.AutoSize = true;
            this.checkUnrecognizeOnly.Checked = true;
            this.checkUnrecognizeOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkUnrecognizeOnly.Location = new System.Drawing.Point(9, 52);
            this.checkUnrecognizeOnly.Name = "checkUnrecognizeOnly";
            this.checkUnrecognizeOnly.Size = new System.Drawing.Size(184, 17);
            this.checkUnrecognizeOnly.TabIndex = 8;
            this.checkUnrecognizeOnly.Text = "лише нерозпізнані зображення";
            this.checkUnrecognizeOnly.UseVisualStyleBackColor = true;
            this.checkUnrecognizeOnly.CheckedChanged += new System.EventHandler(this.checkUnrecognizeOnly_CheckedChanged);
            // 
            // btnTestingLoad
            // 
            this.btnTestingLoad.Location = new System.Drawing.Point(386, 17);
            this.btnTestingLoad.Name = "btnTestingLoad";
            this.btnTestingLoad.Size = new System.Drawing.Size(86, 23);
            this.btnTestingLoad.TabIndex = 7;
            this.btnTestingLoad.Text = "Завантажити";
            this.btnTestingLoad.UseVisualStyleBackColor = true;
            this.btnTestingLoad.Click += new System.EventHandler(this.btnTestingLoad_Click);
            // 
            // btnTestingSelect
            // 
            this.btnTestingSelect.Location = new System.Drawing.Point(324, 17);
            this.btnTestingSelect.Name = "btnTestingSelect";
            this.btnTestingSelect.Size = new System.Drawing.Size(56, 23);
            this.btnTestingSelect.TabIndex = 6;
            this.btnTestingSelect.Text = "Обрати";
            this.btnTestingSelect.UseVisualStyleBackColor = true;
            this.btnTestingSelect.Click += new System.EventHandler(this.btnTestingSelect_Click);
            // 
            // lbTestingDIR
            // 
            this.lbTestingDIR.AutoSize = true;
            this.lbTestingDIR.Location = new System.Drawing.Point(6, 21);
            this.lbTestingDIR.Name = "lbTestingDIR";
            this.lbTestingDIR.Size = new System.Drawing.Size(42, 13);
            this.lbTestingDIR.TabIndex = 5;
            this.lbTestingDIR.Text = "Папка:";
            // 
            // tbTestingDIR
            // 
            this.tbTestingDIR.Location = new System.Drawing.Point(57, 19);
            this.tbTestingDIR.Name = "tbTestingDIR";
            this.tbTestingDIR.Size = new System.Drawing.Size(261, 20);
            this.tbTestingDIR.TabIndex = 4;
            // 
            // gbNetwork
            // 
            this.gbNetwork.Controls.Add(this.btnSaveNeuro);
            this.gbNetwork.Controls.Add(this.btnLoadNeuro);
            this.gbNetwork.Controls.Add(this.btnCreateNeuro);
            this.gbNetwork.Controls.Add(this.lbRangeDash);
            this.gbNetwork.Controls.Add(this.tbArchitect);
            this.gbNetwork.Controls.Add(this.tbRangeMax);
            this.gbNetwork.Controls.Add(this.tbAlpha);
            this.gbNetwork.Controls.Add(this.tbArchitectBackground);
            this.gbNetwork.Controls.Add(this.tbRangeMin);
            this.gbNetwork.Controls.Add(this.lbArchitect);
            this.gbNetwork.Controls.Add(this.lbAlpha);
            this.gbNetwork.Controls.Add(this.lbNeuroRange);
            this.gbNetwork.Location = new System.Drawing.Point(496, 12);
            this.gbNetwork.Name = "gbNetwork";
            this.gbNetwork.Size = new System.Drawing.Size(237, 128);
            this.gbNetwork.TabIndex = 15;
            this.gbNetwork.TabStop = false;
            this.gbNetwork.Text = "Нейромережа";
            // 
            // btnSaveNeuro
            // 
            this.btnSaveNeuro.Enabled = false;
            this.btnSaveNeuro.Location = new System.Drawing.Point(158, 95);
            this.btnSaveNeuro.Name = "btnSaveNeuro";
            this.btnSaveNeuro.Size = new System.Drawing.Size(75, 23);
            this.btnSaveNeuro.TabIndex = 11;
            this.btnSaveNeuro.Text = "Зберегти";
            this.btnSaveNeuro.UseVisualStyleBackColor = true;
            this.btnSaveNeuro.Click += new System.EventHandler(this.btnSaveNeuro_Click);
            // 
            // btnLoadNeuro
            // 
            this.btnLoadNeuro.Location = new System.Drawing.Point(77, 95);
            this.btnLoadNeuro.Name = "btnLoadNeuro";
            this.btnLoadNeuro.Size = new System.Drawing.Size(82, 23);
            this.btnLoadNeuro.TabIndex = 10;
            this.btnLoadNeuro.Text = "Завантажити";
            this.btnLoadNeuro.UseVisualStyleBackColor = true;
            this.btnLoadNeuro.Click += new System.EventHandler(this.btnLoadNeuro_Click);
            // 
            // btnCreateNeuro
            // 
            this.btnCreateNeuro.Location = new System.Drawing.Point(6, 95);
            this.btnCreateNeuro.Name = "btnCreateNeuro";
            this.btnCreateNeuro.Size = new System.Drawing.Size(70, 23);
            this.btnCreateNeuro.TabIndex = 9;
            this.btnCreateNeuro.Text = "Створити";
            this.btnCreateNeuro.UseVisualStyleBackColor = true;
            this.btnCreateNeuro.Click += new System.EventHandler(this.btnCreateNeuro_Click);
            // 
            // lbRangeDash
            // 
            this.lbRangeDash.AutoSize = true;
            this.lbRangeDash.Location = new System.Drawing.Point(184, 20);
            this.lbRangeDash.Name = "lbRangeDash";
            this.lbRangeDash.Size = new System.Drawing.Size(10, 13);
            this.lbRangeDash.TabIndex = 8;
            this.lbRangeDash.Text = "-";
            this.lbRangeDash.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tbArchitect
            // 
            this.tbArchitect.Location = new System.Drawing.Point(146, 69);
            this.tbArchitect.Name = "tbArchitect";
            this.tbArchitect.Size = new System.Drawing.Size(66, 20);
            this.tbArchitect.TabIndex = 7;
            this.tbArchitect.Text = "15-10";
            this.tbArchitect.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbRangeMax
            // 
            this.tbRangeMax.Location = new System.Drawing.Point(194, 17);
            this.tbRangeMax.Name = "tbRangeMax";
            this.tbRangeMax.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbRangeMax.Size = new System.Drawing.Size(37, 20);
            this.tbRangeMax.TabIndex = 6;
            this.tbRangeMax.Text = "0.1";
            this.tbRangeMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbAlpha
            // 
            this.tbAlpha.Location = new System.Drawing.Point(194, 43);
            this.tbAlpha.Name = "tbAlpha";
            this.tbAlpha.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbAlpha.Size = new System.Drawing.Size(37, 20);
            this.tbAlpha.TabIndex = 5;
            this.tbAlpha.Text = "1.0";
            this.tbAlpha.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbArchitectBackground
            // 
            this.tbArchitectBackground.Location = new System.Drawing.Point(115, 69);
            this.tbArchitectBackground.Name = "tbArchitectBackground";
            this.tbArchitectBackground.ReadOnly = true;
            this.tbArchitectBackground.Size = new System.Drawing.Size(118, 20);
            this.tbArchitectBackground.TabIndex = 4;
            this.tbArchitectBackground.Text = "784-                        -10";
            this.tbArchitectBackground.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbRangeMin
            // 
            this.tbRangeMin.Location = new System.Drawing.Point(145, 17);
            this.tbRangeMin.Name = "tbRangeMin";
            this.tbRangeMin.Size = new System.Drawing.Size(37, 20);
            this.tbRangeMin.TabIndex = 3;
            this.tbRangeMin.Text = "-0.1";
            this.tbRangeMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbArchitect
            // 
            this.lbArchitect.AutoSize = true;
            this.lbArchitect.Location = new System.Drawing.Point(8, 74);
            this.lbArchitect.Name = "lbArchitect";
            this.lbArchitect.Size = new System.Drawing.Size(108, 13);
            this.lbArchitect.TabIndex = 2;
            this.lbArchitect.Text = "Архітектура мережі:";
            this.lbArchitect.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbAlpha
            // 
            this.lbAlpha.AutoSize = true;
            this.lbAlpha.Location = new System.Drawing.Point(3, 47);
            this.lbAlpha.Name = "lbAlpha";
            this.lbAlpha.Size = new System.Drawing.Size(194, 13);
            this.lbAlpha.TabIndex = 1;
            this.lbAlpha.Text = "Коефіцієнт крутизни сигмоїди (alpha)";
            // 
            // lbNeuroRange
            // 
            this.lbNeuroRange.AutoSize = true;
            this.lbNeuroRange.Location = new System.Drawing.Point(10, 20);
            this.lbNeuroRange.Name = "lbNeuroRange";
            this.lbNeuroRange.Size = new System.Drawing.Size(137, 13);
            this.lbNeuroRange.TabIndex = 0;
            this.lbNeuroRange.Text = "Діапазон початкових ваг:";
            this.lbNeuroRange.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // gbProgress
            // 
            this.gbProgress.Controls.Add(this.tbEpochProgress);
            this.gbProgress.Controls.Add(this.tbTimeProgress);
            this.gbProgress.Controls.Add(this.tbLearningErrorProgress);
            this.gbProgress.Controls.Add(this.tbTestingErrorProgress);
            this.gbProgress.Location = new System.Drawing.Point(913, 12);
            this.gbProgress.Name = "gbProgress";
            this.gbProgress.Size = new System.Drawing.Size(67, 128);
            this.gbProgress.TabIndex = 0;
            this.gbProgress.TabStop = false;
            this.gbProgress.Text = "Поточні";
            // 
            // tbEpochProgress
            // 
            this.tbEpochProgress.Location = new System.Drawing.Point(4, 19);
            this.tbEpochProgress.Name = "tbEpochProgress";
            this.tbEpochProgress.ReadOnly = true;
            this.tbEpochProgress.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbEpochProgress.Size = new System.Drawing.Size(59, 20);
            this.tbEpochProgress.TabIndex = 23;
            this.tbEpochProgress.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbTimeProgress
            // 
            this.tbTimeProgress.Location = new System.Drawing.Point(4, 44);
            this.tbTimeProgress.Name = "tbTimeProgress";
            this.tbTimeProgress.ReadOnly = true;
            this.tbTimeProgress.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbTimeProgress.Size = new System.Drawing.Size(59, 20);
            this.tbTimeProgress.TabIndex = 24;
            this.tbTimeProgress.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbLearningErrorProgress
            // 
            this.tbLearningErrorProgress.Location = new System.Drawing.Point(4, 68);
            this.tbLearningErrorProgress.Name = "tbLearningErrorProgress";
            this.tbLearningErrorProgress.ReadOnly = true;
            this.tbLearningErrorProgress.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbLearningErrorProgress.Size = new System.Drawing.Size(59, 20);
            this.tbLearningErrorProgress.TabIndex = 25;
            this.tbLearningErrorProgress.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbTestingErrorProgress
            // 
            this.tbTestingErrorProgress.Location = new System.Drawing.Point(4, 93);
            this.tbTestingErrorProgress.Name = "tbTestingErrorProgress";
            this.tbTestingErrorProgress.ReadOnly = true;
            this.tbTestingErrorProgress.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbTestingErrorProgress.Size = new System.Drawing.Size(59, 20);
            this.tbTestingErrorProgress.TabIndex = 26;
            this.tbTestingErrorProgress.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // gbStopConditions
            // 
            this.gbStopConditions.Controls.Add(this.tbTime);
            this.gbStopConditions.Controls.Add(this.tbLearningError);
            this.gbStopConditions.Controls.Add(this.tbEpoch);
            this.gbStopConditions.Controls.Add(this.tbTestingError);
            this.gbStopConditions.Controls.Add(this.checkEpoch);
            this.gbStopConditions.Controls.Add(this.checkTime);
            this.gbStopConditions.Controls.Add(this.checkLearningError);
            this.gbStopConditions.Controls.Add(this.checkTestError);
            this.gbStopConditions.Location = new System.Drawing.Point(735, 12);
            this.gbStopConditions.Name = "gbStopConditions";
            this.gbStopConditions.Size = new System.Drawing.Size(179, 128);
            this.gbStopConditions.TabIndex = 0;
            this.gbStopConditions.TabStop = false;
            this.gbStopConditions.Text = "Критерії зупинки навчання";
            // 
            // tbTime
            // 
            this.tbTime.Location = new System.Drawing.Point(124, 43);
            this.tbTime.Mask = "00:00:00";
            this.tbTime.Name = "tbTime";
            this.tbTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbTime.Size = new System.Drawing.Size(50, 20);
            this.tbTime.TabIndex = 20;
            this.tbTime.Text = "000500";
            this.tbTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbLearningError
            // 
            this.tbLearningError.Location = new System.Drawing.Point(124, 67);
            this.tbLearningError.Name = "tbLearningError";
            this.tbLearningError.Size = new System.Drawing.Size(50, 20);
            this.tbLearningError.TabIndex = 22;
            this.tbLearningError.Text = "0.01";
            this.tbLearningError.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbEpoch
            // 
            this.tbEpoch.Location = new System.Drawing.Point(124, 19);
            this.tbEpoch.Name = "tbEpoch";
            this.tbEpoch.Size = new System.Drawing.Size(50, 20);
            this.tbEpoch.TabIndex = 20;
            this.tbEpoch.Text = "1000";
            this.tbEpoch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbTestingError
            // 
            this.tbTestingError.Location = new System.Drawing.Point(134, 93);
            this.tbTestingError.Name = "tbTestingError";
            this.tbTestingError.Size = new System.Drawing.Size(40, 20);
            this.tbTestingError.TabIndex = 19;
            this.tbTestingError.Text = "5";
            this.tbTestingError.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // checkEpoch
            // 
            this.checkEpoch.AutoSize = true;
            this.checkEpoch.Checked = true;
            this.checkEpoch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkEpoch.Enabled = false;
            this.checkEpoch.Location = new System.Drawing.Point(6, 20);
            this.checkEpoch.Name = "checkEpoch";
            this.checkEpoch.Size = new System.Drawing.Size(104, 17);
            this.checkEpoch.TabIndex = 16;
            this.checkEpoch.Text = "  Кількість епох";
            this.checkEpoch.UseVisualStyleBackColor = true;
            // 
            // checkTime
            // 
            this.checkTime.AutoSize = true;
            this.checkTime.Checked = true;
            this.checkTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkTime.Enabled = false;
            this.checkTime.Location = new System.Drawing.Point(6, 46);
            this.checkTime.Name = "checkTime";
            this.checkTime.Size = new System.Drawing.Size(94, 17);
            this.checkTime.TabIndex = 17;
            this.checkTime.Text = "    Тривалість";
            this.checkTime.UseVisualStyleBackColor = true;
            // 
            // checkLearningError
            // 
            this.checkLearningError.AutoSize = true;
            this.checkLearningError.Checked = true;
            this.checkLearningError.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkLearningError.Enabled = false;
            this.checkLearningError.Location = new System.Drawing.Point(6, 72);
            this.checkLearningError.Name = "checkLearningError";
            this.checkLearningError.Size = new System.Drawing.Size(121, 17);
            this.checkLearningError.TabIndex = 18;
            this.checkLearningError.Text = "Похибка на 1 вихід";
            this.checkLearningError.UseVisualStyleBackColor = true;
            // 
            // checkTestError
            // 
            this.checkTestError.AutoSize = true;
            this.checkTestError.Checked = true;
            this.checkTestError.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkTestError.Enabled = false;
            this.checkTestError.Location = new System.Drawing.Point(6, 96);
            this.checkTestError.Name = "checkTestError";
            this.checkTestError.Size = new System.Drawing.Size(129, 17);
            this.checkTestError.TabIndex = 0;
            this.checkTestError.Text = "Частка помилок у %";
            this.checkTestError.UseVisualStyleBackColor = true;
            // 
            // gbLearningParams
            // 
            this.gbLearningParams.Controls.Add(this.lbMomentum);
            this.gbLearningParams.Controls.Add(this.lbLearningRate);
            this.gbLearningParams.Controls.Add(this.tbMomentum);
            this.gbLearningParams.Controls.Add(this.tbLearningRate);
            this.gbLearningParams.Controls.Add(this.checkMixingWhenLearning);
            this.gbLearningParams.Location = new System.Drawing.Point(496, 146);
            this.gbLearningParams.Name = "gbLearningParams";
            this.gbLearningParams.Size = new System.Drawing.Size(237, 72);
            this.gbLearningParams.TabIndex = 16;
            this.gbLearningParams.TabStop = false;
            this.gbLearningParams.Text = "Навчання";
            // 
            // lbMomentum
            // 
            this.lbMomentum.AutoSize = true;
            this.lbMomentum.Location = new System.Drawing.Point(63, 34);
            this.lbMomentum.Name = "lbMomentum";
            this.lbMomentum.Size = new System.Drawing.Size(132, 13);
            this.lbMomentum.TabIndex = 4;
            this.lbMomentum.Text = "Момент (Momentum [0,1])";
            // 
            // lbLearningRate
            // 
            this.lbLearningRate.AutoSize = true;
            this.lbLearningRate.Location = new System.Drawing.Point(0, 15);
            this.lbLearningRate.Name = "lbLearningRate";
            this.lbLearningRate.Size = new System.Drawing.Size(195, 13);
            this.lbLearningRate.TabIndex = 3;
            this.lbLearningRate.Text = "Швидкість навчання (LearnRate [0, 1])";
            // 
            // tbMomentum
            // 
            this.tbMomentum.Location = new System.Drawing.Point(194, 30);
            this.tbMomentum.Name = "tbMomentum";
            this.tbMomentum.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbMomentum.Size = new System.Drawing.Size(37, 20);
            this.tbMomentum.TabIndex = 2;
            this.tbMomentum.Text = "0.0";
            this.tbMomentum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbLearningRate
            // 
            this.tbLearningRate.Location = new System.Drawing.Point(194, 9);
            this.tbLearningRate.Name = "tbLearningRate";
            this.tbLearningRate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbLearningRate.Size = new System.Drawing.Size(37, 20);
            this.tbLearningRate.TabIndex = 1;
            this.tbLearningRate.Text = "0.1";
            this.tbLearningRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // checkMixingWhenLearning
            // 
            this.checkMixingWhenLearning.AutoSize = true;
            this.checkMixingWhenLearning.Enabled = false;
            this.checkMixingWhenLearning.Location = new System.Drawing.Point(2, 51);
            this.checkMixingWhenLearning.Name = "checkMixingWhenLearning";
            this.checkMixingWhenLearning.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkMixingWhenLearning.Size = new System.Drawing.Size(229, 17);
            this.checkMixingWhenLearning.TabIndex = 0;
            this.checkMixingWhenLearning.Text = "Міксувати зображення під час навчання";
            this.checkMixingWhenLearning.UseVisualStyleBackColor = true;
            // 
            // checkChart
            // 
            this.checkChart.AutoSize = true;
            this.checkChart.Enabled = false;
            this.checkChart.Location = new System.Drawing.Point(741, 197);
            this.checkChart.Name = "checkChart";
            this.checkChart.Size = new System.Drawing.Size(138, 17);
            this.checkChart.TabIndex = 17;
            this.checkChart.Text = "Розраховувати графік";
            this.checkChart.UseVisualStyleBackColor = true;
            // 
            // btnLearning
            // 
            this.btnLearning.Enabled = false;
            this.btnLearning.Location = new System.Drawing.Point(739, 154);
            this.btnLearning.Name = "btnLearning";
            this.btnLearning.Size = new System.Drawing.Size(241, 39);
            this.btnLearning.TabIndex = 18;
            this.btnLearning.Text = "Навчати";
            this.btnLearning.UseVisualStyleBackColor = true;
            this.btnLearning.Click += new System.EventHandler(this.btnLearning_Click);
            // 
            // chartLearning
            // 
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisX.Title = "Epoch";
            chartArea2.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea2.AxisY.Title = "Learn err";
            chartArea2.AxisY.TitleForeColor = System.Drawing.Color.DarkBlue;
            chartArea2.AxisY2.Title = "rete %";
            chartArea2.AxisY2.TitleForeColor = System.Drawing.Color.DarkRed;
            chartArea2.Name = "ChartArea1";
            this.chartLearning.ChartAreas.Add(chartArea2);
            this.chartLearning.Location = new System.Drawing.Point(496, 222);
            this.chartLearning.Name = "chartLearning";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series3.LabelForeColor = System.Drawing.Color.DarkBlue;
            series3.LegendText = "11";
            series3.Name = "Series1";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series4.LabelForeColor = System.Drawing.Color.Brown;
            series4.LegendText = "22";
            series4.Name = "Series2";
            series4.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            this.chartLearning.Series.Add(series3);
            this.chartLearning.Series.Add(series4);
            this.chartLearning.Size = new System.Drawing.Size(480, 425);
            this.chartLearning.TabIndex = 19;
            this.chartLearning.Text = "chart1";
            // 
            // folderSelectTraining
            // 
            this.folderSelectTraining.Description = "Оберіть папку із зображеннями для навчання";
            this.folderSelectTraining.ShowNewFolderButton = false;
            this.folderSelectTraining.Tag = "Гы";
            // 
            // folderSelectTesting
            // 
            this.folderSelectTesting.Description = "Оберіть папку із зображеннями для тестування";
            // 
            // fileSelectNetwork
            // 
            this.fileSelectNetwork.FileName = "15-10";
            // 
            // bwTrainingLoaderAsync
            // 
            this.bwTrainingLoaderAsync.WorkerReportsProgress = true;
            this.bwTrainingLoaderAsync.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwTrainingLoaderAsync_DoWork);
            this.bwTrainingLoaderAsync.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwTrainingLoaderAsync_ProgressChanged);
            this.bwTrainingLoaderAsync.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwTrainingLoaderAsync_RunWorkerCompleted);
            // 
            // bwTestingLoaderAsync
            // 
            this.bwTestingLoaderAsync.WorkerReportsProgress = true;
            this.bwTestingLoaderAsync.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwTestingLoaderAsync_DoWork);
            this.bwTestingLoaderAsync.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwTestingLoaderAsync_ProgressChanged);
            this.bwTestingLoaderAsync.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwTestingLoaderAsync_RunWorkerCompleted);
            // 
            // bwMix
            // 
            this.bwMix.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwMix_DoWork);
            this.bwMix.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwMix_RunWorkerCompleted);
            // 
            // fileSaveNetwork
            // 
            this.fileSaveNetwork.DefaultExt = "bin";
            // 
            // bwUnrec
            // 
            this.bwUnrec.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwUnrec_DoWork);
            this.bwUnrec.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwUnrec_RunWorkerCompleted);
            // 
            // imglistUnrec
            // 
            this.imglistUnrec.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imglistUnrec.ImageSize = new System.Drawing.Size(28, 28);
            this.imglistUnrec.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // bwLearning
            // 
            this.bwLearning.WorkerReportsProgress = true;
            this.bwLearning.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLearning_DoWork);
            this.bwLearning.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwLearning_ProgressChanged);
            this.bwLearning.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLearning_RunWorkerCompleted);
            // 
            // MultilayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.chartLearning);
            this.Controls.Add(this.btnLearning);
            this.Controls.Add(this.checkChart);
            this.Controls.Add(this.gbLearningParams);
            this.Controls.Add(this.gbNetwork);
            this.Controls.Add(this.gbProgress);
            this.Controls.Add(this.gbStopConditions);
            this.Controls.Add(this.gbTestingDIR);
            this.Controls.Add(this.gbTrainingDIR);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1000, 700);
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "MultilayerForm";
            this.Text = "Digit Recognition, Makaruk Vlad";
            this.gbTrainingDIR.ResumeLayout(false);
            this.gbTrainingDIR.PerformLayout();
            this.gbTestingDIR.ResumeLayout(false);
            this.gbTestingDIR.PerformLayout();
            this.gbNetwork.ResumeLayout(false);
            this.gbNetwork.PerformLayout();
            this.gbProgress.ResumeLayout(false);
            this.gbProgress.PerformLayout();
            this.gbStopConditions.ResumeLayout(false);
            this.gbStopConditions.PerformLayout();
            this.gbLearningParams.ResumeLayout(false);
            this.gbLearningParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartLearning)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imglistTraning;
        private System.Windows.Forms.ListView listTraining;
        private System.Windows.Forms.ListView listTesting;
        private System.Windows.Forms.GroupBox gbTrainingDIR;
        private System.Windows.Forms.TextBox tbTrainingPicCount;
        private System.Windows.Forms.Button btnTrainingLoad;
        private System.Windows.Forms.Button btnTrainingSelect;
        private System.Windows.Forms.TextBox tbTrainingDIR;
        private System.Windows.Forms.Label lbTrainingDIR;
        private System.Windows.Forms.ProgressBar progressTraining;
        private System.Windows.Forms.Button btnTrainingMix;
        private System.Windows.Forms.Label lbTrainingPicCount;
        private System.Windows.Forms.GroupBox gbTestingDIR;
        private System.Windows.Forms.TextBox tbTestingRate;
        private System.Windows.Forms.Label lbTestingRate;
        private System.Windows.Forms.Label lbTestingPicCount;
        private System.Windows.Forms.TextBox tbUnrecOnly;
        private System.Windows.Forms.TextBox tbTestingPicCount;
        private System.Windows.Forms.ProgressBar progressTesting;
        private System.Windows.Forms.CheckBox checkUnrecognizeOnly;
        private System.Windows.Forms.Button btnTestingLoad;
        private System.Windows.Forms.Button btnTestingSelect;
        private System.Windows.Forms.Label lbTestingDIR;
        private System.Windows.Forms.TextBox tbTestingDIR;
        private System.Windows.Forms.GroupBox gbNetwork;
        private System.Windows.Forms.Button btnSaveNeuro;
        private System.Windows.Forms.Button btnLoadNeuro;
        private System.Windows.Forms.Button btnCreateNeuro;
        private System.Windows.Forms.Label lbRangeDash;
        private System.Windows.Forms.TextBox tbArchitect;
        private System.Windows.Forms.TextBox tbRangeMax;
        private System.Windows.Forms.TextBox tbAlpha;
        private System.Windows.Forms.TextBox tbArchitectBackground;
        private System.Windows.Forms.TextBox tbRangeMin;
        private System.Windows.Forms.Label lbArchitect;
        private System.Windows.Forms.Label lbAlpha;
        private System.Windows.Forms.Label lbNeuroRange;
        private System.Windows.Forms.GroupBox gbProgress;
        private System.Windows.Forms.TextBox tbEpochProgress;
        private System.Windows.Forms.TextBox tbTimeProgress;
        private System.Windows.Forms.TextBox tbLearningErrorProgress;
        private System.Windows.Forms.TextBox tbTestingErrorProgress;
        private System.Windows.Forms.GroupBox gbStopConditions;
        private System.Windows.Forms.TextBox tbLearningError;
        private System.Windows.Forms.TextBox tbEpoch;
        private System.Windows.Forms.TextBox tbTestingError;
        private System.Windows.Forms.CheckBox checkEpoch;
        private System.Windows.Forms.CheckBox checkTime;
        private System.Windows.Forms.CheckBox checkLearningError;
        private System.Windows.Forms.CheckBox checkTestError;
        private System.Windows.Forms.GroupBox gbLearningParams;
        private System.Windows.Forms.Label lbMomentum;
        private System.Windows.Forms.Label lbLearningRate;
        private System.Windows.Forms.TextBox tbMomentum;
        private System.Windows.Forms.TextBox tbLearningRate;
        private System.Windows.Forms.CheckBox checkMixingWhenLearning;
        private System.Windows.Forms.CheckBox checkChart;
        private System.Windows.Forms.Button btnLearning;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartLearning;
        private System.Windows.Forms.FolderBrowserDialog folderSelectTraining;
        private System.Windows.Forms.ImageList imglistTesting;
        private System.Windows.Forms.FolderBrowserDialog folderSelectTesting;
        private System.Windows.Forms.MaskedTextBox tbTime;
        private System.Windows.Forms.OpenFileDialog fileSelectNetwork;
        private System.ComponentModel.BackgroundWorker bwTrainingLoaderAsync;
        private System.ComponentModel.BackgroundWorker bwTestingLoaderAsync;
        private System.ComponentModel.BackgroundWorker bwMix;
        private System.Windows.Forms.SaveFileDialog fileSaveNetwork;
        private System.ComponentModel.BackgroundWorker bwUnrec;
        private System.Windows.Forms.ImageList imglistUnrec;
        private System.ComponentModel.BackgroundWorker bwLearning;
    }
}

