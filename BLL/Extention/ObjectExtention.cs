using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Extention
{
    public static class ObjectExtensions
    {
        public static bool AreAllPropertiesNull(this object obj)
        {
            if (obj == null)
                return true;

            var properties = obj.GetType().GetProperties();

            return properties.All(p => p.GetValue(obj) == null);
        }
    }
}
