using System;
using System.Text;
using System.Collections.Generic;


namespace SpedService.Model {
    
    public class FornecedorDTO {
        public FornecedorDTO() { }
        public int Id { get; set; }
        public PessoaDTO Pessoa { get; set; }
        public System.DateTime Desde { get; set; }
        public string OptanteSimplesNacional { get; set; }
        public string Localizacao { get; set; }
        public System.DateTime DataCadastro { get; set; }
        public string SofreRetencao { get; set; }
        public string ChequeNominalA { get; set; }
        public string Observacao { get; set; }
        public string ContaRemetente { get; set; }
        public decimal PrazoMedioEntrega { get; set; }
        public string GeraFaturamento { get; set; }
        public int NumDiasPrimeiroVencimento { get; set; }
        public int NumDiasIntervalo { get; set; }
        public int QuantidadeParcelas { get; set; }
    }
}
