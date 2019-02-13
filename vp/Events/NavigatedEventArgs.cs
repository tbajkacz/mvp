using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vp.Events
{
    public class NavigatedEventArgs : EventArgs
    {
        public readonly string Key;

        public NavigatedEventArgs(string key)
        {
            Key = key;
        }
    }
}
