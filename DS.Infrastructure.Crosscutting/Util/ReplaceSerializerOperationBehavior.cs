using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Runtime.Serialization;

namespace DS.Infrastructure.Crosscutting.Util
{
   public class ReplaceSerializerOperationBehavior : DataContractSerializerOperationBehavior
   {
      public ReplaceSerializerOperationBehavior(OperationDescription operation)
         : base(operation)
      {
      }
      public override XmlObjectSerializer CreateSerializer(Type type, string name, string ns, IList<Type> knownTypes)
      {
         return new NetDataContractSerializer(name, ns);
      }
      public override XmlObjectSerializer CreateSerializer(Type type, XmlDictionaryString name, XmlDictionaryString ns, IList<Type> knownTypes)
      {
         return new NetDataContractSerializer(name, ns);
      }
      public static void ReplaceSerializer(ServiceEndpoint endpoint)
      {
         foreach (var operation in endpoint.Contract.Operations)
         {
            for (int i = 0; i < operation.Behaviors.Count; i++)
            {
               if (operation.Behaviors[i] is DataContractSerializerOperationBehavior)
               {
                  operation.Behaviors[i] = new ReplaceSerializerOperationBehavior(operation);
                  break;
               }
            }
         }
      }
   }
}
