using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrialChecker
{
    /// <summary>
    /// Настройки проверки валидности лицензии
    /// </summary>
    public class TrialCheckerConfiguration
    {
        /// <summary>
        /// Использовать GUI элементы для вывода сообщений
        /// </summary>
        public bool UseGui { get; set; }
        /// <summary>
        /// Путь до файла с лицензией
        /// </summary>
        public string LicenseFilePath { get; set; }
        /// <summary>
        /// Выполнять проверку манипулирования датами
        /// </summary>
        public bool CheckDateManipulation { get; set; }
        /// <summary>
        /// Выполнять проверку при каждом вызове (в противном случае значение закэшируется)
        /// </summary>
        public bool EveryCallCheck { get; set; }
        /// <summary>
        /// Открытый ключ шифра
        /// </summary>
        public string RSAPublicKey { get; set; }
        /// <summary>
        /// Подавлять Exception'ы (при возникновении Exception просто возвращать false) 
        /// </summary>
        public bool SuppressExceptions { get; set; } = false;
    }
}
