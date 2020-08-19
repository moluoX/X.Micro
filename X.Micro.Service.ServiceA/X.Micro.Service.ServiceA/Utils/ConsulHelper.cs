using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace X.Micro.Service.ServiceA.Utils
{
    public static class ConsulHelper
    {
        public static void RegisterConsul(this IConfiguration configuration)//IHostApplicationLifetime lifetime
        {
            var client = new ConsulClient(config =>
            {
                config.Address = new Uri("http://localhost:8500");
                config.Datacenter = "dc1";
            });
            var registration = new AgentServiceRegistration
            {
                ID = $"X.Micro.Service.ServiceA_{Guid.NewGuid()}",
                Name = "X.Micro.Service.ServiceA",
                Address = configuration["ip"],
                Port = int.Parse(configuration["port"]),
                Tags = null,
                //Check = new AgentServiceCheck()
                //{
                //    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册
                //    Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔
                //    HTTP = "https://127.0.0.1:19000/api/student",//健康检查地址
                //    Timeout = TimeSpan.FromSeconds(5)
                //}
            };
            client.Agent.ServiceRegister(registration);

            // 应用程序终止时，服务取消注册
            //lifetime.ApplicationStopping.Register(() =>
            //{
            //    client.Agent.ServiceDeregister(registration.ID).Wait();
            //});
        }
    }
}
