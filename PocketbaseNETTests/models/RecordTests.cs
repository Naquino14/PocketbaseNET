using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PocketbaseNET.models.Tests
{
    [TestClass()]
    public class RecordTests
    {
        Dictionary<string, object> data = new()
        {
            { "id", "testid12345" },
            { "created", "12/31/22" },
            { "updated", "" },
            { "collectionId", "testCollectionId12345" },
            { "collectionName", "testCollectionName12345" },
            { "identity", "test@test.com" }
        };

        Dictionary<string, object?> statusExpand = new()
        {
            { "id", "testid12345" },
            { "created", "12/21/22" },
            { "updated", "" },
            { "status", "online" },
            { "state", 1 },
            { "picture", null }
        };

        Dictionary<string, object> usernameChangesExpand = new()
        {
            { "id", "otherid12345" },
            { "created", "12/31/22" },
            { "updated", "12/31/22" },
            { "collectionId", "usernameChangesId123345" },
            { "collectionName", "usernameChangesName123345" },
            { "identity", "test@test.com" },
            { "numChanges", 3 },
            { "previousNames", new string[] { "test2022", "naquino14", "bazinga" } }
        };
        
        [TestMethod()]
        public void RecordTest()
        {
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
            var record = new Record(data);
            var expandRecord = new Record(statusExpand!);
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

        [TestMethod()]
        public void LoadExpandTest1() // parallel records
        {
            var record = new Record(data);
            var statusRecord = new Record(statusExpand!);
            var usernameChangesRecord = new Record(usernameChangesExpand);

            record.LoadExpand(new Dictionary<string, Record[]?>()
                { { "userinfo", new Record[] { statusRecord, usernameChangesRecord } } });

            Assert.IsNotNull(record.Expand["userinfo"]);
            Record[] expand = record.Expand["userinfo"]!;
            Assert.AreEqual(expand.Length, 2);

            var getStatus = expand[0];
            Assert.AreEqual(getStatus.ID, "testid12345");
            Assert.IsNotNull(getStatus.CollectionName);
            Assert.AreEqual(getStatus["status"], "online");
            Assert.AreEqual(getStatus["state"], 1);
            Assert.AreEqual(getStatus["picture"], null);
            Assert.AreEqual(getStatus["iamnotpartofexpand"], null);

            var getUsernameChanges = expand[1];
            Assert.AreEqual(getUsernameChanges.ID, "otherid12345");
            Assert.IsNotNull(getUsernameChanges.CollectionName);
            Assert.AreEqual(getUsernameChanges["numChanges"], 3);
            Assert.AreEqual((getUsernameChanges["previousNames"] as string[])![0], "test2022");
            Assert.AreEqual((getUsernameChanges["previousNames"] as string[])![1], "naquino14");
            Assert.AreEqual((getUsernameChanges["previousNames"] as string[])![2], "bazinga");
            Assert.AreEqual(getUsernameChanges["iamnotpartofexpand"], null);
        }

        [TestMethod()]
        public void LoadExpandTest2() // nested records
        {
            var record = new Record(data);
            var statusRecord = new Record(statusExpand!);
            var usernameChangesRecord = new Record(usernameChangesExpand);

            statusRecord.LoadExpand(new() {
                { "usernameChanges", new[] { usernameChangesRecord } }
            });

            record.LoadExpand(new() { 
                { "status", new[] { statusRecord } }
            });

            Assert.IsNotNull(record.Expand["status"]);
            Record[] expand = record.Expand["status"]!;
            Assert.AreEqual(expand.Length, 1);

            var getStatus = expand[0];
            Assert.AreEqual(getStatus.ID, "testid12345");
            Assert.IsNotNull(getStatus.CollectionName);
            Assert.AreEqual(getStatus["status"], "online");
            Assert.AreEqual(getStatus["state"], 1);
            Assert.AreEqual(getStatus["picture"], null);
            Assert.AreEqual(getStatus["iamnotpartofexpand"], null);

            Assert.IsNotNull(getStatus.Expand["usernameChanges"]);
            Record[] expand2 = getStatus.Expand["usernameChanges"]!;
            Assert.AreEqual(expand2.Length, 1);

            var getUsernameChanges = expand2[0];
            Assert.AreEqual(getUsernameChanges.ID, "otherid12345");
            Assert.IsNotNull(getUsernameChanges.CollectionName);
            Assert.AreEqual(getUsernameChanges["numChanges"], 3);
            Assert.AreEqual((getUsernameChanges["previousNames"] as string[])![0], "test2022");
            Assert.AreEqual((getUsernameChanges["previousNames"] as string[])![1], "naquino14");
            Assert.AreEqual((getUsernameChanges["previousNames"] as string[])![2], "bazinga");
            Assert.AreEqual(getUsernameChanges["iamnotpartofexpand"], null);
        }
    }
}