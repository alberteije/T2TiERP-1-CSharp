using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace OrcamentoService.Model
{
    [DataContract]
    public class ParcelaPagarDTO
    {
        [DataMember]
        public int? id { get; set; }

        [DataMember]
        public int? idStatusParcelaPagar { get; set; }
        
        [DataMember]
        public LancamentoPagarDTO lancamentoPagar { get; set; }

        [DataMember]
        public DateTime? dataEmissao { get; set; }
        [DataMember]
        public DateTime? dataVencimento { get; set; }
        [DataMember]
        public DateTime? descontoAte { get; set; }
        [DataMember]
        public char? sofreRetencao { get; set; }
        [DataMember]
        public decimal? valor { get; set; }
        [DataMember]
        public decimal? taxaJuro { get; set; }
        [DataMember]
        public decimal? taxaMulta { get; set; }
        [DataMember]
        public decimal? taxaDesconto { get; set; }
        [DataMember]
        public decimal? valorJuro { get; set; }
        [DataMember]
        public decimal? valorMulta { get; set; }
        [DataMember]
        public decimal? valorDesconto { get; set; }
        [DataMember]
        public int? numeroParcela{ get; set; }
    }
}