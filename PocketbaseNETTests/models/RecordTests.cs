using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PocketbaseNET.models.Tests
{
    [TestClass()]
    public class RecordTests
    {
        [TestMethod()]
        public void RecordTest()
        {
            var data = new Dictionary<string, object>
            {
                { "id", "testid12345" },
                { "created", "12/31/22" },
                { "updated", "" },
                { "collectionId", "testCollectionId12345" },
                { "collectionName", "testCollectionName12345" }
            };

            var record = new Record(data);

            Assert.AreEqual("testid12345", record.ID);
            Assert.AreEqual("12/31/22", record.CreatedAt);
            Assert.AreEqual("", record.UpdatedAt);
            Assert.AreEqual("testCollectionId12345", record.CollectionID);
            Assert.AreEqual("testCollectionName12345", record.CollectionName);
        }

        [TestMethod()]
        public void LoadExpandTest()
        {
            var data = new Dictionary<string, object>
            {
                { "id", "testid12345" },
                { "created", "12/31/22" },
                { "updated", "" },
                { "collectionId", "testCollectionId12345" },
                { "collectionName", "testCollectionName12345" },
                { "identity", "test@test.com" }
            };

            var expand = new Dictionary<string, object?>
            {
                { "id", "testid12345" },
                { "created", "12/21/22" },
                { "updated", "" },
                { "status", "online" },
                { "state", 1 },
                { "picture", null }
            };

            var record = new Record(data);
            var expandRecord = new Record(expand!);
            var expandData = new Dictionary<string, Record[]?>() { { "userStatus", new Record[] { expandRecord } } };

            record.LoadExpand(expandData);

            Assert.IsNotNull(record.Expand["userStatus"]);

            var getExpand = record.Expand["userStatus"]![0];
            Assert.AreEqual(getExpand.ID, "testid12345");
            Assert.AreEqual(getExpand["status"], "online");
            Assert.AreEqual(getExpand["state"], 1);
            Assert.AreEqual(getExpand["picture"], null);
            Assert.AreEqual(getExpand["iamnotpartofexpand"], null);
        }
    }
}