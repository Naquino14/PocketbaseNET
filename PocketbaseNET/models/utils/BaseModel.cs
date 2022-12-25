using PocketbaseNET.utils;

namespace PocketbaseNET.models.utils
{
    public abstract class BaseModel
    {
        protected Dictionary<string, object> data;

        /// <summary>
        /// The ID of the model. 
        /// </summary>
        public string ID
        {
            get => Get<string>("id");
            private set => this["id"] = value;
        }

        /// <summary>
        /// The timestamp of the model.
        /// </summary>
        public string CreatedAt
        {
            get => Get<string>("created");
            private set => this["created"] = value;
        }

        /// <summary>
        /// The timestamp that the model was last modifed at.
        /// </summary>
        public string UpdatedAt
        {
            get => Get<string>("updated");
            private set => this["updated"] = value;
        }

        protected T Get<T>(string k) => (T)this[k];

        /// <summary>
        /// The index accessor of the model. This is used to access the data dictionary.
        /// </summary>
        /// <param name="key">The string key of the object.</param>
        /// <returns>The object associated with that key.</returns>
        public object this[string key]
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
                Load(data);
        }

        /// <summary>
        /// Loads <b>data</b> into the current model.
        /// </summary>
        /// <param name="data">The dictionary of the data to load into the model.</param>
        protected void Load(Dictionary<string, object> data)
            => data.Keys.ToList().ForEach(k => this[k] = data[k] ?? "");

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
        public abstract BaseModel Clone();

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