﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using JosephM.Core.Security;

namespace JosephM.Core.Test
{
    [TestClass]
    public class StringEncryptorTests
    {
        [TestMethod]
        public void StringEncryptorEncryptDecryptTest()
        {
            const string initial = "THESTRING";
            var encrypted = StringEncryptor.Encrypt(initial);
            var decrypted = StringEncryptor.Decrypt(encrypted);
            Assert.IsFalse(initial == encrypted);
            Assert.IsTrue(initial == decrypted);
        }

        [TestMethod]
        public void StringEncryptorHashStringTest()
        {
            const string initial = "THESTRING";
            var hashed = StringEncryptor.HashString(initial);
            Assert.IsFalse(hashed.Contains(initial));
        }
    }
}