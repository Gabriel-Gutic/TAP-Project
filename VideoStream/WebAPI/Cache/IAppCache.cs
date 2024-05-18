using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Cache
{
    public interface IAppCache
    {
        public void Store(string key, object value, int seconds);
        public bool TryGet(string key, out object? value);
    }
}
