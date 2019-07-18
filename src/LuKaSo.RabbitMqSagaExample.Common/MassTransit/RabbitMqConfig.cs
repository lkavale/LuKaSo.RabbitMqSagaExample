using System;

namespace LuKaSo.RabbitMqSagaExample.Common.MassTransit
{
    public class RabbitMqConfig
    {
        /// <summary>
        /// Server address
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Server port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Get complete url
        /// </summary>
        /// <returns></returns>
        public Uri GetServiceUri()
        {
            return new Uri($"rabbitmq://{Server}:{Port}/");
        }

        /// <summary>
        /// Stringify connection information
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"server {GetServiceUri()} connection configuration, username {User} {(string.IsNullOrEmpty(Password) ? "without" : "with")} password";
        }
    }
}
