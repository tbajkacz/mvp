using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vp.Services.Serialization
{
    public interface ISerializer
    {
        string Serialize<T>(object obj);

        T Deserialize<T>(string str);
    }
}
