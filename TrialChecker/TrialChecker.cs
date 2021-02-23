using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrialChecker
{
    /// <summary>
    /// Проверка актуальности периода пробного использования
    /// </summary>
    public static class TrialChecker
    {
        #region Fields
        /// <summary>
        /// Признак проведенной проверки
        /// </summary>
        static bool _isChecked = false;
        /// <summary>
        /// Закэшированное значение даты валидности
        /// </summary>
        static DateTime _validUpto = DateTime.MinValue;
        static RsaUtils _rsa;
        #endregion Fields

        /// <summary>
        /// Конструктор
        /// </summary>
        static TrialChecker()
        {
            _rsa = new RsaUtils();

            // Настройка конфигурации по умолчанию
            Configuration = new TrialCheckerConfiguration()
            {
                CheckDateManipulation = true,
                EveryCallCheck = false,
                LicenseFilePath = Path.Combine(Environment.CurrentDirectory, "license.lic"),
                RSAPublicKey = @"<RSAKeyValue><Modulus>2OO32RsAa73GjFw171YUkwOTyguKeT3N1zjAZY04/f4TdpRABfNBs1aHICepzhQ1gy1TBnYX3K95b+qD7u6CczU65JzormbqlgY1rweG+HNwGcn36g0M66k7qwfFSjZhbmlGTvedD7xo4L/pSf91p7KwjzLt6ac8+UAdqVfKsu0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>",
                SuppressExceptions = true
            };
            Reconfigurate(Configuration);
        }

        /// <summary>
        /// Конфигурация проверки
        /// </summary>
        public static TrialCheckerConfiguration Configuration { get; private set; }


        /// <summary>
        /// Перенастроить в соответствии с конфигурацией
        /// </summary>
        /// <param name="trialCheckerConfiguration"></param>
        public static void Reconfigurate(TrialCheckerConfiguration trialCheckerConfiguration)
        {
            if (trialCheckerConfiguration == null)
                throw new ArgumentNullException(nameof(trialCheckerConfiguration), $"Конфигурация не должна быть пустой");
            Configuration = trialCheckerConfiguration;
            _rsa.SetKeys(Configuration.RSAPublicKey);
            _isChecked = false;
        }
        /// <summary>
        /// Проверка валидности
        /// </summary>
        /// <returns></returns>
        public static bool CheckValidity()
        {
            try
            {
                if ((!Configuration.EveryCallCheck) && (_isChecked))
                {
                    return _validUpto >= DateTime.Now;
                }

                _validUpto = ReadLicenseFile();
                if (Configuration.CheckDateManipulation)
                {
                    var userDt = GetLastWriteDate(Path.GetTempPath());
                    if (userDt > DateTime.Now)
                        _validUpto = DateTime.MinValue;

                    userDt = GetLastWriteDate(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
                    if (userDt > DateTime.Now)
                        _validUpto = DateTime.MinValue;

                    userDt = GetLastWriteDate(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                    if (userDt > DateTime.Now)
                        _validUpto = DateTime.MinValue;

                    userDt = GetLastWriteDate(Environment.GetFolderPath(Environment.SpecialFolder.Recent));
                    if (userDt > DateTime.Now)
                        _validUpto = DateTime.MinValue;

                    userDt = GetLastWriteDate(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
                    if (userDt > DateTime.Now)
                        _validUpto = DateTime.MinValue;

                }
                _isChecked = true;

                return _validUpto >= DateTime.Now;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex}");
                ShowInformation(ex.Message);
                if (!Configuration.SuppressExceptions)
                    throw;
            }

            return false;
        }

        #region Checks functions
        /// <summary>
        /// Прочитать файл лицензии
        /// </summary>
        /// <returns></returns>
        private static DateTime ReadLicenseFile()
        {
            if (!File.Exists(Configuration.LicenseFilePath))
                throw new FileNotFoundException("Не найден файл лицензии", Configuration.LicenseFilePath);

            var fileContent = File.ReadAllText(Configuration.LicenseFilePath);
            if ((fileContent?.Length ?? 0) < 8)
                throw new FormatException($"Файл лицензии неверного формата");

            var date = fileContent.Substring(0, 8);
            var sign = fileContent.Substring(8);

            if (!_rsa.CheckSign(date, sign))
                throw new InvalidDataException($"Неверная подпись");

            DateTime outDate;
            if (!DateTime.TryParseExact(date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out outDate))
                throw new InvalidCastException($"Некорректный формат даты: {date}. Должен быть yyyyMMdd");

            return outDate;
        }

        /// <summary>
        /// Получение информации по последнему доступу/записи в папке
        /// </summary>
        /// <param name="inPath"></param>
        /// <returns></returns>
        private static DateTime GetLastWriteDate(string inPath)
        {
            if (!Directory.Exists(inPath))
                return DateTime.MinValue;

            var dt = Directory.GetLastAccessTime(inPath);
            var dt2 = Directory.GetLastWriteTime(inPath);
            if (dt2 > dt)
                dt = dt2;

            var dirinfo = new DirectoryInfo(inPath);
            var myfile = dirinfo.GetFiles()
                         .OrderByDescending(f => f.LastWriteTime)
                         .First();
            dt2 = myfile.LastAccessTime;
            if (dt2 > dt) dt = dt2;
            dt2 = myfile.LastWriteTime;
            if (dt2 > dt) dt = dt2;

            return dt;
        }

        #endregion Checks functions

        #region Export information
        static void ShowInformation(string inMessage)
        {
            Console.WriteLine($"TrialChecker: {inMessage}");

            if (Configuration.UseGui)
            {
                // Show GUI information 

            }
        }
        #endregion Export information

    }
}
