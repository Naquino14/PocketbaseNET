using PocketbaseNET.models.utils;
using PocketbaseNET.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketbaseNET.models
{
    /// <summary>
    /// A record represents a single row in a table.
    /// </summary>
    public class Record : BaseModel
    {
        /// <summary>
        /// The id of the collection this record belongs to.
        /// </summary>
        public string CollectionID
        {
            get => Get<string>("collectionId")!;
            set => this["collectionId"] = value;
        }

        /// <summary>
        /// The collection this record belongs to.
        /// </summary>
        public string CollectionName
        {
            get => Get<string>("collectionName")!;
            set => this["collectionName"] = value;
        }

        /// <summary>
        /// The relational data attatched to this record.
        /// </summary>
        public NullableDictionary<string, Record[]?> Expand
        {
            get => Get<NullableDictionary<string, Record[]?>>("expand")!;
            set => this["expand"] = value;        
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="data"></param>
        protected override void Load(NullableDictionary<string, object> data)
        {
            base.Load(data);

            CollectionID = (string)(data["collectionId"] ?? "");
            CollectionName = (string)(data["collectionName"] ?? "");

            Expand = new();
            LoadExpand((NullableDictionary<string, Record[]?>)(data["expand"] ?? new NullableDictionary<string, Record[]?>()));
        }

        private void LoadExpand(NullableDictionary<string, Record[]?> expand) // i can see this potentially causing
            => expand.Keys.ToList().ForEach(k => Expand[k] = expand[k]); // a million problems
    }
}
