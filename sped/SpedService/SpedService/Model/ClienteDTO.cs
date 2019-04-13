using System;
using System.Text;
using System.Collections.Generic;


namespace SpedService.Model {
    
    public class ClienteDTO {
        public ClienteDTO() { }
        public int Id { get; set; }
        public PessoaDTO Pessoa { get; set; }
        public System.DateTime Desde { get; set; }
        public System.DateTime DataCadastro { get; set; }
        public string Observacao { get; set; }
        public string ContaTomador { get; set; }
        public string GeraFinanceiro { get; set; }
        public string IndicadorPreco { get; set; }
        public decimal PorcentoDesconto { get; set; }
        public string FormaDesconto { get; set; }
        public decimal LimiteCredito { get; set; }
        public string TipoFrete { get; set; }
    }
}
