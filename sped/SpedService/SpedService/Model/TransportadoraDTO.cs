using System;
using System.Text;
using System.Collections.Generic;


namespace SpedService.Model {
    
    public class TransportadoraDTO {
        public TransportadoraDTO() { }
        public int Id { get; set; }
        public PessoaDTO Pessoa { get; set; }
        public System.DateTime DataCadastro { get; set; }
        public string Observacao { get; set; }
    }
}
