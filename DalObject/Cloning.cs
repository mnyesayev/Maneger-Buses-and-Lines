using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace Dal
{
    static class Cloning
    {
        internal static T Clone<T>(this T original)where T:new()
        {
            T target = (T)Activator.CreateInstance(original.GetType());
            foreach (var item in target.GetType().GetProperties())
            {
                item.SetValue(target, original.GetType().GetProperty(item.Name));
            }
            return target;
        }
    }
}
