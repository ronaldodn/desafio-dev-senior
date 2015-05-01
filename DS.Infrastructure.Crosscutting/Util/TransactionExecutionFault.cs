using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DS.Infrastructure.Crosscutting.Util
{
   [DataContract]
   public class TransactionExecutionFault
   {
      #region Membros privados

      private string _message = string.Empty;

      #endregion

      #region Propriedades

      [DataMember]
      public string Message
      {
         get { return _message; }
         set { _message = value; }
      }

      #endregion

      #region Construtores

      public TransactionExecutionFault()
      {
         this._message = "Error executing Business Process on back-end.";
      }

      #endregion

      #region Métodos privados
      #endregion

      #region Métodos públicos

      public TransactionExecutionFault(string message)
      {
         this._message = message;
      }

      #endregion
   }

}
