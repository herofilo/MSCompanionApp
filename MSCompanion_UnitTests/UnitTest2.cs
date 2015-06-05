using System;
using jamoram62.tools.MSCompanion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSCompanion_UnitTests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            Compress.ZipArchiveFolder(
                "test.zip",
                @"C:\_Devel\Personal\MSCompanion\MSCompanion\TestData\UserDataDir\Movies\El Don - 003", false);

        }
    }
}
