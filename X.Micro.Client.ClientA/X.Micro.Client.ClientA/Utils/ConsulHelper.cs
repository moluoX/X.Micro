using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace X.Micro.Client.ClientA.Utils
{
    public class ConsulHelper
    {
        public static string GetServiceUrl()
        {
            using (var client = new ConsulClient(config =>
            {
                config.Address = new Uri("http://localhost:8500");
                config.Datacenter = "dc1";
            }))
            {
                var services = client.Catalog.Service("X.Micro.Service.ServiceA").Result.Response;
                if (services != null && services.Any())
                {
                    var service = services.ElementAt(new Random().Next(services.Count()));
                    return $"http://{service.ServiceAddress}:{service.ServicePort}/";
                }
                throw new Exception("未找到服务");
            }
        }
    }
}
