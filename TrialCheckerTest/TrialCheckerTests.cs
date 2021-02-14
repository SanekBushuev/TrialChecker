using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrialChecker;

namespace TrialCheckerTest
{
    [TestClass]
    public class TrialCheckerTests
    {
        void ConfigurateTrialCheckerValid()
        {
            TrialCheckerConfiguration config = new TrialCheckerConfiguration()
            {
                CheckDateManipulation = false,
                EveryCallCheck = false,
                LicenseFilePath = @"C:\Users\Александр\source\repos\TrialChecker\TrialChecker\License2022Example.lic",
                RSAPublicKey = @"<RSAKeyValue><Modulus>2OO32RsAa73GjFw171YUkwOTyguKeT3N1zjAZY04/f4TdpRABfNBs1aHICepzhQ1gy1TBnYX3K95b+qD7u6CczU65JzormbqlgY1rweG+HNwGcn36g0M66k7qwfFSjZhbmlGTvedD7xo4L/pSf91p7KwjzLt6ac8+UAdqVfKsu0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>",
                UseGui = true
            };
            TrialChecker.TrialChecker.Reconfigurate(config);
        }

        void ConfigurateTrialCheckerInvalid()
        {
            TrialCheckerConfiguration config = new TrialCheckerConfiguration()
            {
                CheckDateManipulation = true,
                EveryCallCheck = false,
                LicenseFilePath = @"C:\Users\Александр\source\repos\TrialChecker\TrialChecker\License2021Example.lic",
                RSAPublicKey = @"<RSAKeyValue><Modulus>2OO32RsAa73GjFw171YUkwOTyguKeT3N1zjAZY04/f4TdpRABfNBs1aHICepzhQ1gy1TBnYX3K95b+qD7u6CczU65JzormbqlgY1rweG+HNwGcn36g0M66k7qwfFSjZhbmlGTvedD7xo4L/pSf91p7KwjzLt6ac8+UAdqVfKsu0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>",
                UseGui = true
            };
            TrialChecker.TrialChecker.Reconfigurate(config);
        }

        [TestMethod]
        public void TestValidLicense()
        {
            ConfigurateTrialCheckerValid();

            Assert.IsTrue(TrialChecker.TrialChecker.CheckValidity());
        }

        [TestMethod]
        public void TestInvalidLicense()
        {
            ConfigurateTrialCheckerInvalid();

            Assert.IsFalse(TrialChecker.TrialChecker.CheckValidity());
        }
    }
}
