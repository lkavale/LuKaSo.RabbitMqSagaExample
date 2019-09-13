using LuKaSo.RabbitMqSagaExample.Common.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace LuKaSo.RabbitMqSagaExample.Common.Configuration
{
    /// <summary>
    /// Datafeed configuration file reader
    /// </summary>
    public class FileConfigurationManager<T> : IConfigurationManager<T>
    {
        /// <summary>
        /// Path to configuration file
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// Constructor
        /// </summary>
        public FileConfigurationManager(string fileName) : this(Path.GetDirectoryName(Assembly.GetAssembly(typeof(FileConfigurationManager<>)).Location), fileName)
        {
        }

        /// <summary>
        /// Constructor with specified path to config file
        /// </summary>
        /// <param name="path"></param>
        public FileConfigurationManager(string path, string fileName)
        {
            _path = Path.Combine(path, fileName);

            if (!File.Exists(_path))
            {
                Write(default);
            }
        }

        /// <summary>
        /// Read configuration
        /// </summary>
        public T Read()
        {
            using (var stream = new FileStream(_path, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(stream, Encoding.ASCII))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var serializer = new JsonSerializer();
                return serializer.Deserialize<T>(jsonReader);
            }
        }

        /// <summary>
        /// Write configuration
        /// </summary>
        /// <param name="configuration"></param>
        public void Write(T configuration)
        {
            using (var stream = new FileStream(_path, FileMode.OpenOrCreate, FileAccess.Write))
            using (var writer = new StreamWriter(stream, Encoding.ASCII))
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(jsonWriter, configuration);
            }
        }
    }
}
