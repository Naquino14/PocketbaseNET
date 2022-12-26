using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketbaseNETTests.models.utils
{
    public class NullableDict<TKey, TValue> : Dictionary<TKey, TValue> where TKey : notnull
    {
        public new TValue? this[TKey k] 
        {
            get => ContainsKey(k) ? base[k] : default;
            set => base[k] = value!;
        }

        public NullableDict() : base() { }

        public NullableDict(IDictionary<TKey, TValue> dict) : base(dict) { }

        public NullableDict(int capacity) : base(capacity) { }
    }
}
