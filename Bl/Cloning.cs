using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bl
{
    public static class Cloning
    {
        internal static T Clone<T>(this T original) where T : new()
        {
            T target = new T();
            foreach (PropertyInfo sourcePropertyInfo in typeof(T).GetProperties())
            {
                PropertyInfo destPropertyInfo = target.GetType().GetProperty(sourcePropertyInfo.Name);
                destPropertyInfo.SetValue(target, sourcePropertyInfo.GetValue(original, null), null);
            }
            return target;
        }

        public static void CopyPropertiesTo<T, S>(this S from, T to)
        {
            foreach (PropertyInfo propTo in to.GetType().GetProperties())
            {
                PropertyInfo propFrom = typeof(S).GetProperty(propTo.Name);
                if (propFrom == null)
                    continue;
                var value = propFrom.GetValue(from, null);
                if (value is ValueType || value is string)
                    propTo.SetValue(to, value);
            }
        }
        public static T CopyPropertiesToNew<T,S>(this S from)
        {
            T to =(T)Activator.CreateInstance(typeof(T)); // new object of T
            from.CopyPropertiesTo(to);
            return to;
        }
    }
}
