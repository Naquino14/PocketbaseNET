using Microsoft.VisualStudio.TestTools.UnitTesting;
using PocketbaseNET.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketbaseNET.utils.Tests
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

        [TestMethod()]
        public void NullableDictionaryTest2()
        {
            var dict = new NullableDictionary<string, int?>();
            int? a = 69, b = null, c = 420;

            dict["a"] = a;
            dict["b"] = b;
            dict["c"] = c;

            Assert.AreEqual(a, dict["a"]);
            Assert.AreEqual(b, dict["b"]);
            Assert.AreEqual(b, null);
            Assert.AreEqual(56, dict["b"] ??= 56);
            Assert.AreEqual(56, dict["b"]);
            Assert.AreEqual(c, dict["c"]);
        }

        [TestMethod()]
        public void NullableDictionaryTest3()
        {

        }

        [TestMethod()]
        public void NullableDictionaryAddTest()
        {
            var dict = new NullableDictionary<Exception, int>();
            var ex = new Exception("Hello");
            dict.Add(ex, 69);
            Assert.AreEqual(69, dict[ex]);
        }

        [TestMethod()]
        public void NullableDictionaryRemoveTest()
        {
            var dict = new NullableDictionary<Exception, int>();
            var ex = new Exception("Hello");
            var ex1 = new AbandonedMutexException("Foo");
            dict.Add(ex, 69);
            Assert.AreEqual(69, dict[ex]);
            dict.Add(ex1, 420);
            Assert.AreEqual(420, dict[ex1]);
            Assert.IsTrue(dict.Remove(ex) && dict.Remove(ex1));

        }
    }
}