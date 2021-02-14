using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TrialChecker;

namespace TrialCheckerTest
{
    /// <summary>
    /// Набор тестов для RsaUtils
    /// </summary>
    [TestClass]
    public class RsaUtilsTests
    {
        /// <summary>
        /// Проверка генерации разных типов ключей
        /// </summary>
        [TestMethod]
        public void TestKeysGeneration()
        {
            var _rsau = new RsaUtils();
            var keys = _rsau.GetKeyInformation(true);

            Assert.IsNotNull(keys);
            Assert.IsTrue(keys.Length > 0);

            var publicKey = _rsau.GetKeyInformation(false);
            Assert.IsNotNull(publicKey);
            Assert.IsTrue(publicKey.Length < keys.Length);

            Console.WriteLine($"Private and public: {keys}");
            Console.WriteLine($"Public: {publicKey}");

        }

        /// <summary>
        /// Проверка установки ключей
        /// </summary>
        [TestMethod]
        public void TestSetKeys()
        {
            var _rsau = new RsaUtils();
            var origKeys = _rsau.GetKeyInformation(true);

            var _rsau2 = new RsaUtils();
            var origKeys2 = _rsau2.GetKeyInformation(true);

            Assert.IsNotNull(origKeys);
            Assert.IsNotNull(origKeys2);
            Assert.IsFalse(origKeys.Equals(origKeys2));

            _rsau2.SetKeys(origKeys);
            var origKeys2_2 = _rsau2.GetKeyInformation(true);

            Assert.IsNotNull(origKeys2_2);
            Assert.IsFalse(origKeys2.Equals(origKeys2_2));
            Assert.IsTrue(origKeys.Equals(origKeys2_2));
        }

        /// <summary>
        /// Проверка установки только public ключа
        /// </summary>
        [TestMethod]
        public void TestSetPublicKey()
        {
            var _rsau = new RsaUtils();
            var origKeys = _rsau.GetKeyInformation(false);

            var _rsau2 = new RsaUtils();
            var origKeys2 = _rsau2.GetKeyInformation(false);

            Assert.IsNotNull(origKeys);
            Assert.IsNotNull(origKeys2);
            Assert.IsFalse(origKeys.Equals(origKeys2));

            _rsau2.SetKeys(origKeys);
            var origKeys2_2 = _rsau2.GetKeyInformation(false);

            Assert.IsNotNull(origKeys2_2);
            Assert.IsFalse(origKeys2.Equals(origKeys2_2));
            Assert.IsTrue(origKeys.Equals(origKeys2_2));
        }

        /// <summary>
        /// Проверка возможности подписи и проверки
        /// </summary>
        [TestMethod]
        public void TestSignAndCheckWithPublicKey()
        {
            var message = DateTime.Now.ToString();
            //"string to be encrypted";

            var _rsau = new RsaUtils();
            var signMessage = _rsau.SignData(message);

            Assert.IsNotNull(signMessage);
            Assert.IsTrue(signMessage.Length > 0);

            var _rsau2 = new RsaUtils();
            _rsau2.SetKeys(_rsau.GetKeyInformation(false));
            var verifyData = _rsau2.CheckSign(message, signMessage);

            Assert.IsTrue(verifyData);

        }
        
        [TestMethod]
        public void CreateTestLicenseFile ()
        {
            var rsau = new RsaUtils();
            rsau.SetKeys(@"<RSAKeyValue><Modulus>2OO32RsAa73GjFw171YUkwOTyguKeT3N1zjAZY04/f4TdpRABfNBs1aHICepzhQ1gy1TBnYX3K95b+qD7u6CczU65JzormbqlgY1rweG+HNwGcn36g0M66k7qwfFSjZhbmlGTvedD7xo4L/pSf91p7KwjzLt6ac8+UAdqVfKsu0=</Modulus><Exponent>AQAB</Exponent><P>5P3vNrTqC9JeXmyJSQrfmQN3tYdr5Kc7OoRb0HqK11tc1grGZ1rDmSTQ6OFtqpzCiJyz2oH1BHA16WC2UbVftw==</P><Q>8nherC7UVWiuG8suixZtYL5CTK/CJVpf4GVrRZOb2slbL0sojgq7QQG5sZfQ4PIFHhfzgVM4uTkHNaFJUTv6ew==</Q><DP>zTNg8b0dHordVfAc9f9wb0XUOC+qV7QkN0P0otWtJV8RyhzNvkBvlwSO0KFyDLl8+b6yzNQ4JEoJhaDMwFuv8Q==</DP><DQ>rhlR2Q4y9jSYt2o3vDTZSpqyHkAhBhMTPptz39xyDzF/YMD1mLAJ7k5f2C2rFKmSlR4bgSORWiWhkJeDfmmzmw==</DQ><InverseQ>NLinNiXw75D8kOGhz2Z9hJR6tU0t1fuh9xVwAQSZomMFqTYEaGwekFIWA/7olAqR5b/oigGndS8F5L1tVDWqrg==</InverseQ><D>AmH+ExSI3KYpGEGrFgYME9FmPkICM3LKYCLvei4jAidYb2URjv8tKZs5wNCuvOTb8aT91IRXnsUOwHwDFwznwfcVwdIpW7m0Q/6Vu7GE3G1pzQOUynv+E9R8cdSl/m3lfNt8OJHNqnnR4MBoirerQargCZwDWY0nYlm6xBKIkME=</D></RSAKeyValue>");

            var dt1 = (new DateTime(2022, 01, 01)).ToString("yyyyMMdd");
            var dt2 = (new DateTime(2021, 02, 10)).ToString("yyyyMMdd");

            var dt1Sign = rsau.SignData(dt1);
            var dt2Sigh = rsau.SignData(dt2);

            Console.WriteLine($"1: {dt1}:{dt1Sign}");
            Console.WriteLine($"2: {dt2}:{dt2Sigh}");

            var rsau2 = new RsaUtils();
            rsau2.SetKeys(@"<RSAKeyValue><Modulus>2OO32RsAa73GjFw171YUkwOTyguKeT3N1zjAZY04/f4TdpRABfNBs1aHICepzhQ1gy1TBnYX3K95b+qD7u6CczU65JzormbqlgY1rweG+HNwGcn36g0M66k7qwfFSjZhbmlGTvedD7xo4L/pSf91p7KwjzLt6ac8+UAdqVfKsu0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");

            Assert.IsTrue(rsau2.CheckSign(dt1, dt1Sign));
            Assert.IsTrue(rsau2.CheckSign(dt2, dt2Sigh));
        }
    }//
}
