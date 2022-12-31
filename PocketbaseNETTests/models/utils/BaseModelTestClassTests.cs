using Microsoft.VisualStudio.TestTools.UnitTesting;
using PocketbaseNET.models.utils;
using PocketbaseNET.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketbaseNET.models.utils.Tests
{
    [TestClass()]
    public class BaseModelTestClassTests
    {
        [TestMethod()]
        public void BaseModelTestClassTest()
        {
            var data = new Dictionary<string, object>
            {
                { "id", "testid12345" },
                { "created", "12/28/22" },
                { "updated", "" },
            };
            BaseModel model = new BaseModelTestClass(NullableDictionary.FromDictToNullableDictDeepClone(data));

            Assert.AreEqual("testid12345", model.ID);
            Assert.AreEqual("12/28/22", model.CreatedAt);
            Assert.AreEqual("", model.UpdatedAt);
        }

        [TestMethod()]
        public void BaseModelTestClassTest1()
        {
            var data = new Dictionary<string, object?>
            {
                { "id", "testid12345" },
                { "created", "12/28/22" },
                { "updated", null },
            };

            Assert.ThrowsException<ArgumentException>(() 
                => new BaseModelTestClass(NullableDictionary.FromDictToNullableDictDeepClone(data)!));
        }
    }
}