using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LuKaSo.RabbitMqSagaExample.Common.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Deep clone of object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepClone<T>(this T obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                stream.Seek(0, SeekOrigin.Begin);
                //.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
