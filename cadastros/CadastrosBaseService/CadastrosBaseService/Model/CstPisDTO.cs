using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace CadastrosBaseService.Model
{
    [DataContract]
    public class CstPisDTO
    {
        #region Propriedades
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string Codigo { get; set; }
        [DataMember]
        public string Descricao { get; set; }
        #endregion
    }
}