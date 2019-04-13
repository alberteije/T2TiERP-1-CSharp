using System;
using System.Text;
using System.Collections.Generic;


namespace SpedService.Model {
    
    public class ViewPessoaContadorDTO {
        public ViewPessoaContadorDTO() { }
        public int Id { get; set; }
        public string InscricaoCrc { get; set; }
        public int IdPessoa { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public int MunicipioIbge { get; set; }
        public string Uf { get; set; }
        public string Fone { get; set; }
        public string Fax { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public string CpfCnpj { get; set; }
    }
}
