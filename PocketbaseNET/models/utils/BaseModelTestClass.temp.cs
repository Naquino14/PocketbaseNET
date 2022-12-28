using PocketbaseNET.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketbaseNET.models.utils
{
    public class BaseModelTestClass : BaseModel
    {
        public BaseModelTestClass(NullableDictionary<string, object> data) : base(data) { }
    }
}
