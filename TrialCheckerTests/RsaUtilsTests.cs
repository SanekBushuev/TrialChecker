using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrialChecker;

namespace TrialCheckerTests
{
    /// <summary>
    /// Набор тестов для RsaUtils
    /// </summary>
    [TestClass]
    public class RsaUtilsTests
    {

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
        }
    }
}
