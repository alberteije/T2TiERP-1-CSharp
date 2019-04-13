using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CadastrosBaseService.Model
{
    [DataContract]
    public class CodigoGpsDTO
    {
        #region Propriedades      
        [DataMember]
        public Int32? Id {get; set;}
        [DataMember]
        public Int32? Codigo {get; set;}
        [DataMember]
        public string Descricao {get; set; }
        #endregion
    }
}