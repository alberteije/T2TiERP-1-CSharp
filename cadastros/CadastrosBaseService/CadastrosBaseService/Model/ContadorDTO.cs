using System;
using System.Text;
using System.Collections.Generic;


namespace CadastrosBaseService.Model {
    
    public class ContadorDTO {
        public ContadorDTO() { }
        public int Id { get; set; }
        public PessoaDTO Pessoa { get; set; }
        public string InscricaoCrc { get; set; }
        public string UfCrc { get; set; }
        public string Fone { get; set; }
        public string Fax { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public System.Nullable<int> MunicipioIbge { get; set; }
        public string Uf { get; set; }
        public string Email { get; set; }
    }
}
