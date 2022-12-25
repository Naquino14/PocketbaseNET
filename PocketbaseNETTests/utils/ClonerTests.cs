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
    public class ClonerTests
    {
        class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            
            public Person(string name, int age)
            {
                Name = name;
                Age = age;
            }

            public Person() { }
        }
        
        [TestMethod()]
        public void ReflectiveCloneTest()
        {
            var obj1 = new Person("Nate", 18);
            var obj2 = (Person?)(Cloner.ReflectiveClone(obj1) ?? null);
            if (obj2 is null)
                Assert.Fail();
            Assert.AreEqual(obj1.Name, obj2.Name);
        }
    }
}