using Microsoft.VisualStudio.TestTools.UnitTesting;
using PocketbaseNETTests.models.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketbaseNETTests.models.utils.Tests
{
    [TestClass()]
    public class NullableDictionaryTests
    {
        [TestMethod()]
        public void NullableDictionaryTest()
        {
            var dict = new NullableDictionary<string, string?>();
            string a = "abc";
            string? b = null, c = "def";

            dict["a"] = a;
            dict["b"] = b;
            dict["c"] = c;

            Assert.AreEqual(a, dict["a"]);
            Assert.AreEqual(b, dict["b"]);
            Assert.AreEqual(c, dict["c"]);
        }

        [TestMethod()]
        public void NullableDictionaryTest1()
        {
            var dict = new NullableDictionary<string, string?>();
            string a = "abc";
            string? b = null, c = "def", d = null;

            dict["a"] = a;
            dict["b"] = b;
            dict["c"] = c;

            Assert.AreEqual(a, dict["a"]);
            Assert.AreEqual("bazinga", dict["b"] ?? "bazinga");
            Assert.AreEqual(c, dict["c"]);
            Assert.AreEqual("nate is cool", dict["d"] ??= "nate is cool");
            Assert.AreEqual("nate is cool", dict["d"]);
        }
    }
}