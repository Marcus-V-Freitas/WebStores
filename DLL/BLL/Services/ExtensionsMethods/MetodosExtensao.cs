using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace DLL.BLL.Services.ExtensionsMethods
{
    public static class MetodosExtensao
    {
        public static void Use<T>(this T item, Action<T> work)
        {
            work(item);

        }

        public static async Task With<T>(this T value, Func<T, Task> action)
        {
            await action(value);
        }


        public static dynamic Merge(this object item1, object item2)
        {
            if (item1 == null || item2 == null)
                return item1 ?? item2 ?? new ExpandoObject();

            dynamic expando = new ExpandoObject();
            var result = expando as IDictionary<string, object>;
            foreach (System.Reflection.PropertyInfo fi in item1.GetType().GetProperties())
            {
                result[fi.Name] = fi.GetValue(item1, null);
            }
            foreach (System.Reflection.PropertyInfo fi in item2.GetType().GetProperties())
            {
                result[fi.Name] = fi.GetValue(item2, null);
            }
            return result;
        }

        public static dynamic CombineDynamics(this object object1, object object2)
        {
            IDictionary<string, object> dictionary1 = GetKeyValueMap(object1);
            IDictionary<string, object> dictionary2 = GetKeyValueMap(object2);

            var result = new ExpandoObject();

            var d = result as IDictionary<string, object>;
            foreach (var pair in dictionary1.Concat(dictionary2))
            {
                d[pair.Key] = pair.Value;
            }

            return result;
        }

        private static IDictionary<string, object> GetKeyValueMap(object values)
        {
            if (values == null)
            {
                return new Dictionary<string, object>();
            }

            var map = values as IDictionary<string, object>;
            if (map != null)
            {
                return map;
            }

            map = new Dictionary<string, object>();
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(values))
            {
                map.Add(descriptor.Name, descriptor.GetValue(values));
            }

            return map;
        }
    }
}
