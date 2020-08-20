using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace X.Micro.Service.ServiceA.Utils
{
    public static class ConsulRegisterExtensions
    {
        public static void RegisterConsul(this IConfiguration configuration, IHostApplicationLifetime lifetime)
        {
            var consulOption = configuration.GetSection("Consul").Get<ConsulOption>();
            var client = new ConsulClient(config =>
            {
                config.Address = new Uri(consulOption.Address);
                config.Datacenter = "dc1";
            });
            var ip = configuration["ip"] ?? consulOption.ServiceIP;
            var port = !string.IsNullOrWhiteSpace(configuration["port"]) ? int.Parse(configuration["port"]) : consulOption.ServicePort.Value;
            var healthCheckUrl = $"http://{ip}:{port}/api/healthcheck";
            var registration = new AgentServiceRegistration
            {
                ID = $"{Guid.NewGuid()}",
                Name = "X.Micro.Service.ServiceA",
                Address = ip,
                Port = port,
                Tags = null,
                Check = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔
                    HTTP = healthCheckUrl,//健康检查地址
                    Timeout = TimeSpan.FromSeconds(5)
                }
            };
            client.Agent.ServiceRegister(registration);

            // 应用程序终止时，服务取消注册
            lifetime.ApplicationStopping.Register(() =>
            {
                client.Agent.ServiceDeregister(registration.ID).Wait();
            });
        }
    }
}
