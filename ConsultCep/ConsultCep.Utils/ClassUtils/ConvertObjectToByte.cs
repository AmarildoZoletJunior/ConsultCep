using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ConsultCep.Utils.ClassUtils
{
    public static class ConvertObjectToByte
    {
        public static byte[] ObjectToByteArray(Object obj)
        {
            var body = JsonConvert.SerializeObject(obj);
            var byteTrade = Encoding.UTF8.GetBytes(body);
            return byteTrade;
        }
    }
}
