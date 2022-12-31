using PocketbaseNET.utils;
using System.Collections;

namespace PocketbaseNET.models.utils
{
    /// <summary>
    /// The superclass of all models.
    /// </summary>
    public abstract class BaseModel
    {
        /// <summary>
        /// The model data dictionary.
        /// </summary>
        protected NullableDictionary<string, object?> data;

        /// <summary>
        /// The ID of the model. 
        /// </summary>
        public string ID
        {
            get => Get<string>("id")!;
            private set => this["id"] = value;
        }

        /// <summary>
        /// The timestamp of the model.
        /// </summary>
        public string CreatedAt
        {
            get => Get<string>("created")!;
            private set => this["created"] = value;
        }

        /// <summary>
        /// The timestamp that the model was last modifed at.
        /// </summary>
        public string UpdatedAt
        {
            get => Get<string>("updated")!;
            private set => this["updated"] = value;
        }

        /// <summary>
        /// Auto cast and get indexed data from the model.
        /// </summary>
        /// <typeparam name="T">The data type to cast to.</typeparam>
        /// <param name="k">The key to index.</param>
        /// <returns></returns>
        protected T? Get<T>(string k) => (T?)this[k];

        /// <summary>
        /// The index accessor of the model. This is used to access the data dictionary.
        /// </summary>
        /// <param name="key">The string key of the object.</param>
        /// <returns>The object associated with that key.</returns>
        public object? this[string key]
        {
            get => data[key];
            set => data[key] = value;
        }

        /// <summary>
        /// The constructor for the model.
        /// </summary>
        /// <param name="data">The dictionary of the data to load into the model.</param>
        public BaseModel(Dictionary<string, object>? data = null)
        {
            this.data = new();
            if (data is not null)
                Load(NullableDictionary.FromDictToNullableDictDeepClone(data));
        }

        /// <summary>
        /// Loads <b>data</b> into the current model.
        /// </summary>
        /// <param name="data">The dictionary of the data to load into the model.</param>
        protected virtual void Load(NullableDictionary<string, object> data)
        {
            data.Keys.ToList().ForEach(k => this[k] = data[k]);

            ID = (string)(data["id"] ?? throw new ArgumentException("Property ID cannot be null when loading data into a model."));
            CreatedAt = (string)(data["created"] ?? throw new ArgumentException("Property CreatedAt cannot be null when loading data into a model."));
            UpdatedAt = (string)(data["updated"] ?? throw new ArgumentException("Property UpdatedAt cannot be null when loading data into a model."));
        }

        /// <summary>
        /// Returns whether the current loaded data represent a stored db record.
        /// </summary>
        /// <returns></returns>
        public bool IsNew() => ID == "";

        /// <summary>
        /// Creates a deep clone of the current model.
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, object> CloneModel()
        {
            Dictionary<string, object> data = new();
            this.data.Keys.ToList().ForEach(k => {
                object? clone = Cloner.ReflectiveClone(this[k]);
                if (clone is not null)
                    data[k] = clone;
            });
            return data;
        }

        /// <summary>
        /// Creates a deep clone of the current object.
        /// </summary>
        /// <returns></returns>
        public virtual BaseModel Clone() => (BaseModel)Cloner.ReflectiveClone(this)!;

        /// <summary>
        /// Exports all model properties as a new plain object.
        /// </summary>
        /// <returns></returns>
        public object Export()
        {
            var data = new
            {
                id = ID,
                created = CreatedAt,
                updated = UpdatedAt
            };
            return data;
        }
    }
}