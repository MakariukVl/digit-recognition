using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Neuro;
using AForge.Neuro.Learning;
using System.Runtime.Serialization;

namespace lab3_v2
{
    /// <summary>
    /// Структура що описує розмір растрового зображення у пікселях
    /// </summary>
    internal struct PictureSize
    {
        /// <summary>
        /// Висота растрового зображення у пікселях
        /// </summary>
        internal int Height;
        /// <summary>
        /// Ширина растрового зображення у пікселях
        /// </summary>
        internal int Width;
    }

    /// <summary>
    /// Структура що описує чи використовується перемішування, та порядок зображень при перемішуванні
    /// </summary>
    internal struct MixOrder
    {
        /// <summary>
        /// Чи використовується перемішування (Так/Ні)
        /// </summary>
        internal bool isMixed;
        /// <summary>
        /// Порядок перемішування (ключ - int, значення - ім'я файлу). Порядок визначається ключем
        /// </summary>
        internal Dictionary<int, string> ORDER;       
    }

    /// <summary>
    /// Структура, що описує вивід нейромережі як пару (Цифра):(Ймовірність)
    /// </summary>
    internal struct NeuroOut
    {
        /// <summary>
        /// Цифра (у межах 0..9)
        /// </summary>
        internal int Number;
        /// <summary>
        /// Ймовірність (у межах -1..1)
        /// </summary>
        internal double Probability;
    }

    [SerializableAttribute]
    public class MyActivationNetwork : ActivationNetwork
    {
        public double Alpha;

        public MyActivationNetwork(
            double alpha,
            int inputsCount,
            params int[] neuronsCount)
            : base(new BipolarSigmoidFunction(alpha), inputsCount, neuronsCount)
        {
            Alpha = alpha;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal struct CurrentLearningState
    {
        /// <summary>
        /// 
        /// </summary>
        internal int Epochs;
        /// <summary>
        /// ms
        /// </summary>
        internal TimeSpan Duration;
        /// <summary>
        /// 
        /// </summary>
        internal double LearningErr;
        /// <summary>
        /// 
        /// </summary>
        internal double TestingErr;
    }
}
