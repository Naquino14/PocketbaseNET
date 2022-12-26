using Microsoft.VisualStudio.TestTools.UnitTesting;
using PocketbaseNET.models.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketbaseNET.models.utils.Tests
{
    [TestClass()]
    public class SchemaFieldTests
    {
        [TestMethod()]
        public void SchemaFieldTest()
        {
            var settings = new Dictionary<string, object>();

            var id = "abcdefg123456";
            settings["id"] = id;

            var name = "test";
            settings["name"] = name;

            // var type = "text";
            // settings["type"] = "text";
            
            bool system = true;
            settings["system"] = system;
            
            bool required = true;
            settings["required"] = required;

            bool unique = true;
            settings["unique"] = unique;

            var testSchema = new SchemaField(settings);

            Assert.AreEqual(id, testSchema.ID);
            Assert.AreEqual(name, testSchema.Name);
            Assert.AreEqual(system, testSchema.System);
            Assert.AreEqual(required, testSchema.Required);
            Assert.AreEqual(unique, testSchema.Unique);
        }
    }
}