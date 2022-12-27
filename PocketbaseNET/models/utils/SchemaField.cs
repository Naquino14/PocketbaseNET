using PocketbaseNET.utils;
using PocketbaseNETTests.models.utils;

namespace PocketbaseNET.models.utils
{
    /// <summary>
    /// The schema field class. (TODO: add more info)
    /// </summary>
    public class SchemaField
    {
        /// <summary>
        /// The id of the schema.
        /// </summary>
        public string ID 
        {
            get => (string)Options["id"]!;
            private set => Options["id"] = value; 
        }
        
        /// <summary>
        /// The name of the schema.
        /// </summary>
        public string Name 
        {
            get => (string)Options["name"]!;
            private set => Options["name"] = value; 
        }
        
        /// <summary>
        /// The type of the schema.
        /// </summary>
        public string Type 
        {
            get => (string)Options["type"]!;
            private set => Options["type"] = value; 
        }
        
        /// <summary>
        /// Returns true if the schema is a system.
        /// </summary>
        public bool System 
        {
            get => (bool)Options["system"]!;
            private set => Options["system"] = value; 
        }

        /// <summary>
        /// Im actually not sure what this is for.
        /// </summary>
        public bool Required 
        {
            get => (bool)Options["required"]!; 
            private set => Options["required"] = value; 
        }
        
        /// <summary>
        /// Returns true if the schema is unique.
        /// </summary>
        public bool Unique 
        { 
            get => (bool)Options["unique"]!;
            private set => Options["unique"] = value;
        }

        /// <summary>
        /// The options dictionary of the schema.
        /// </summary>
        public NullableDictionary<string, object> Options { get; private set; }

        /// <summary>
        /// Create a new Schema Field.
        /// </summary>
        /// <param name="data">The data to be loaded into the schema.</param>
        public SchemaField(Dictionary<string, object> data) 
        {
            Options = new();
            Load(data);
        }

        private void Load(Dictionary<string, object>? data)
        {
            data ??= new();
            var _data = NullableDictionary.FromDictToNullableDict(data);
            ID = (string)(_data["id"] ?? "");
            Name = (string)(_data["name"] ?? "");
            Type = (string)(_data["type"] ?? "text");
            System = (bool)(_data["system"] ?? false);
            Required = (bool)(_data["required"] ?? false);
            Unique = (bool)(_data["unique"] ?? false);

            _data.Keys.ToList().ForEach(k => {
                if (!Options.ContainsKey(k)) 
                    Options.Add(k, _data[k]);
            });
        }
    }
}
