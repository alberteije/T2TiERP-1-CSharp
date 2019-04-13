using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace CadastrosBaseService.Model
{
    [DataContract]
    public class OperadoraCartaoDTO
    {
        #region Propriedades
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int? IdContabilConta { get; set; }
        [DataMember]
        public int IdContaCaixa { get; set; }
        [DataMember]
        public string Bandeira { get; set; }
        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public decimal? TaxaAdm { get; set; }
        [DataMember]
        public decimal? TaxaAdmDebito { get; set; }
        [DataMember]
        public decimal? ValorAluguelPosPin { get; set; }
        [DataMember]
        public int? VencimentoAluguel { get; set; }
        [DataMember]
        public string Fone1 { get; set; }
        [DataMember]
        public string Fone2 { get; set; }
        #endregion
    }
}