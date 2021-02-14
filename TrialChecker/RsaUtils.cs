using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TrialChecker
{
    /// <summary>
    /// Набор утилит для шифрования с помощью RSA
    /// </summary>
    public class RsaUtils
    {
        #region Static Interface
        #endregion Static Interface

        #region Fields
        /// <summary>
        /// Реализация RSA
        /// </summary>
        RSACryptoServiceProvider _rsa;
        #endregion Fields


        public RsaUtils()
        {
            _rsa = new RSACryptoServiceProvider();
        }

        /// <summary>
        /// Generate public and private (withPrivateKey = true) keys in XML-string
        /// </summary>
        /// <returns></returns>
        public string GetKeyInformation(bool withPrivateKey)
        {
            return _rsa.ToXmlString(withPrivateKey);
        }

        /// <summary>
        /// Инициализация с приватным и публичным ключами
        /// </summary>
        /// <param name="inKeys"></param>
        public void SetKeys(string inKeys)
        {
            _rsa.FromXmlString(inKeys);
        }

        /// <summary>
        /// Подпись данных
        /// </summary>
        /// <param name="inData">Данные</param>
        /// <returns>Подпись</returns>
        public string SignData(string inData)
        {
            byte[] dataBytes = System.Text.Encoding.UTF8.GetBytes(inData);
            byte[] signBytes = _rsa.SignData(dataBytes, SHA256.Create());
            return Convert.ToBase64String(signBytes);
        }

        /// <summary>
        /// Проверка соответствия подписи данным
        /// </summary>
        /// <param name="inData">Данные</param>
        /// <param name="inSign">Подпись</param>
        /// <returns>True - если подпись соответствует данным</returns>
        public bool CheckSign(string inData, string inSign)
        {
            byte[] dataBytes = System.Text.Encoding.UTF8.GetBytes(inData);
            byte[] signBytes = Convert.FromBase64String(inSign);
            return _rsa.VerifyData(dataBytes, SHA256.Create(), signBytes);
        }
    }///
}


/*
Private and public: <RSAKeyValue><Modulus>2OO32RsAa73GjFw171YUkwOTyguKeT3N1zjAZY04/f4TdpRABfNBs1aHICepzhQ1gy1TBnYX3K95b+qD7u6CczU65JzormbqlgY1rweG+HNwGcn36g0M66k7qwfFSjZhbmlGTvedD7xo4L/pSf91p7KwjzLt6ac8+UAdqVfKsu0=</Modulus><Exponent>AQAB</Exponent><P>5P3vNrTqC9JeXmyJSQrfmQN3tYdr5Kc7OoRb0HqK11tc1grGZ1rDmSTQ6OFtqpzCiJyz2oH1BHA16WC2UbVftw==</P><Q>8nherC7UVWiuG8suixZtYL5CTK/CJVpf4GVrRZOb2slbL0sojgq7QQG5sZfQ4PIFHhfzgVM4uTkHNaFJUTv6ew==</Q><DP>zTNg8b0dHordVfAc9f9wb0XUOC+qV7QkN0P0otWtJV8RyhzNvkBvlwSO0KFyDLl8+b6yzNQ4JEoJhaDMwFuv8Q==</DP><DQ>rhlR2Q4y9jSYt2o3vDTZSpqyHkAhBhMTPptz39xyDzF/YMD1mLAJ7k5f2C2rFKmSlR4bgSORWiWhkJeDfmmzmw==</DQ><InverseQ>NLinNiXw75D8kOGhz2Z9hJR6tU0t1fuh9xVwAQSZomMFqTYEaGwekFIWA/7olAqR5b/oigGndS8F5L1tVDWqrg==</InverseQ><D>AmH+ExSI3KYpGEGrFgYME9FmPkICM3LKYCLvei4jAidYb2URjv8tKZs5wNCuvOTb8aT91IRXnsUOwHwDFwznwfcVwdIpW7m0Q/6Vu7GE3G1pzQOUynv+E9R8cdSl/m3lfNt8OJHNqnnR4MBoirerQargCZwDWY0nYlm6xBKIkME=</D></RSAKeyValue>
Public: <RSAKeyValue><Modulus>2OO32RsAa73GjFw171YUkwOTyguKeT3N1zjAZY04/f4TdpRABfNBs1aHICepzhQ1gy1TBnYX3K95b+qD7u6CczU65JzormbqlgY1rweG+HNwGcn36g0M66k7qwfFSjZhbmlGTvedD7xo4L/pSf91p7KwjzLt6ac8+UAdqVfKsu0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>
     */
