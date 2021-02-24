using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Extensions
{
    public static class SessionExtension
    {
        public static int? GetInt(this ISession session, string key)
        {
            var data = session.Get(key);
            if (data == null)
            {
                return null;
            }
            return BitConverter.ToInt32(data, 0);
        }

        public static void SetInt(this ISession session, string key, int value)
        {
            session.Set(key, BitConverter.GetBytes(value));
        }


        //for objects such as List, arrays etc.
        public static T GetObject<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }
}
