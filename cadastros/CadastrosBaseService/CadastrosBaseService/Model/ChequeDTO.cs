using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace CadastrosBaseService.Model
{
    [DataContract]
    public class ChequeDTO
    {
        #region Propriedades
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int IdTalonarioCheque { get; set; }
        [DataMember]
        public int? Numero { get; set; }
        [DataMember]
        public string StatusCheque { get; set; }
        [DataMember]
        public DateTime? DataStatus { get; set; }
        #endregion
    }
}