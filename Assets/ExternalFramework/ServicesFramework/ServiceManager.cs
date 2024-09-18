namespace ServiceFramework
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Linq;
    using System.Reflection;
	using System;
    public class ServiceManager : MonoBehaviour
    {
        public static ServiceManager instance;
        public Dictionary<string, IService> services;
        private void Awake()
        {
            instance = this;
            services = new Dictionary<string, IService>();
        }
        public void StopAllServices()
        {
            List<IService> tempServices = services.Values.ToList();
            for (int indexOfService = 0; indexOfService < tempServices.Count; indexOfService++)
            {
                tempServices[indexOfService].StopService();
            }
        }
        public void StartAllServices()
        {
            List<IService> tempServices = services.Values.ToList();
            for (int indexOfService = 0; indexOfService < tempServices.Count; indexOfService++)
            {
                tempServices[indexOfService].StartService();
            }
        }
        public void RegisterService(IService service)
        {
            if (!services.Keys.Contains(service.ServiceName))
            {
                services.Add(service.ServiceName, service);
                if(service.ShouldStartOnRegister)
                {
                    service.StartService();
                }
            }
        }
        public void UnRegisterService(IService service)
        {
            if (services.Keys.Contains(service.ServiceName))
            {
                services.Remove(service.ServiceName);
            }
        }
        public void StartService(string serviceName)
        {
            IService service = services[serviceName];
            service.StartService();
        }
        public void StopService(string serviceName)
        {
            IService service = services[serviceName];
            service.StopService();
        }
        public IService GetService(string serviceName) 
        {
            return services[serviceName];   
        }
    }
}
