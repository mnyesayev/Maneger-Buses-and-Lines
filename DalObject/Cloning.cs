using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DO;

namespace Dal
{
    static class Cloning
    {
        internal static T Clone<T>(this T original) where T : new()
        {
            T target = new T();
            //T copyToObject = (T)Activator.CreateInstance(typeof(T));

            foreach (PropertyInfo sourcePropertyInfo in typeof(T).GetProperties())
            {
                PropertyInfo destPropertyInfo = target.GetType().GetProperty(sourcePropertyInfo.Name);
                destPropertyInfo.SetValue(target, sourcePropertyInfo.GetValue(original, null), null);
            }
            return target;
        }
    }
}
