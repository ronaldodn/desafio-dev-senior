using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DS.Infrastructure.Crosscutting.Util;

namespace DS.DistributedServices.Seekwork
{
   public class ServiceProxy
   {
      public const string EXCEPTION_TIMEOUT = "A operação do serviço expirou (timeout) {0}";
      public const string EXCEPTION_FAULT = "A operação do serviço gerou TransactionExecutionFault {0}";
      public const string EXCEPTION_COMMUNICATION = "Ocorreu um problema de comunicação com o serviço {0}";

      public static T GetProxy<T>(string bindingName, string urlAddress)
      {
         NetTcpBinding binding = new NetTcpBinding();
         TimeSpan ts = new TimeSpan(0, 10, 0);
         binding.CloseTimeout = ts;
         binding.ListenBacklog = 500;
         binding.MaxBufferPoolSize = 1048576;
         binding.MaxBufferSize = 10485760;
         binding.MaxConnections = 500;
         binding.MaxReceivedMessageSize = 10485760;
         binding.Name = bindingName;
         binding.OpenTimeout = ts;

         XmlDictionaryReaderQuotas quotas = new XmlDictionaryReaderQuotas();
         quotas.MaxArrayLength = int.MaxValue;
         quotas.MaxBytesPerRead = int.MaxValue;
         quotas.MaxDepth = int.MaxValue;
         quotas.MaxNameTableCharCount = int.MaxValue;
         quotas.MaxStringContentLength = int.MaxValue;
         binding.ReaderQuotas = quotas;

         binding.ReceiveTimeout = ts;
         binding.Security.Mode = SecurityMode.None;
         binding.SendTimeout = ts;

         EndpointAddress address = new EndpointAddress(urlAddress);
         ChannelFactory<T> channelFactory = new ChannelFactory<T>(binding, address);
         ReplaceSerializerOperationBehavior.ReplaceSerializer(channelFactory.Endpoint);
         T channel = channelFactory.CreateChannel();
         return channel;
      }

      public static ServiceHost GetServiceHost<C, I>(string bindingName, string baseAddress)
      {
         ServiceHost serviceHost;
         NetTcpBinding binding = new NetTcpBinding();
         TimeSpan ts = new TimeSpan(0, 10, 0);
         binding.CloseTimeout = ts;
         binding.ListenBacklog = 500;
         binding.MaxBufferPoolSize = 1048576;
         binding.MaxBufferSize = 10485760;
         binding.MaxConnections = 500;
         binding.MaxReceivedMessageSize = 10485760;
         binding.Name = bindingName;
         binding.OpenTimeout = ts;

         XmlDictionaryReaderQuotas quotas = new XmlDictionaryReaderQuotas();
         quotas.MaxArrayLength = int.MaxValue;
         quotas.MaxBytesPerRead = int.MaxValue;
         quotas.MaxDepth = int.MaxValue;
         quotas.MaxNameTableCharCount = int.MaxValue;
         quotas.MaxStringContentLength = int.MaxValue;
         binding.ReaderQuotas = quotas;

         binding.ReceiveTimeout = ts;
         binding.Security.Mode = SecurityMode.None;
         binding.SendTimeout = ts;

         Uri uriBaseAddress = new Uri(baseAddress);

         serviceHost = new ServiceHost(typeof(C), uriBaseAddress);

         ServiceMetadataBehavior metadataBehavior = new ServiceMetadataBehavior();
         metadataBehavior.HttpGetEnabled = false;
         serviceHost.Description.Behaviors.Add(metadataBehavior);
         serviceHost.AddServiceEndpoint(typeof(IMetadataExchange), binding, "mex");
         //serviceHost.AddServiceEndpoint(typeof(I), binding, uriBaseAddress);
         var endpoint = serviceHost.AddServiceEndpoint(typeof(I), binding, uriBaseAddress);

         ReplaceSerializerOperationBehavior.ReplaceSerializer(endpoint);

         return serviceHost;
      }
   }
}
