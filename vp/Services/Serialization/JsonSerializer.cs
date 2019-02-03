using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace vp.Services.Serialization
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize<T>(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T Deserialize<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}
