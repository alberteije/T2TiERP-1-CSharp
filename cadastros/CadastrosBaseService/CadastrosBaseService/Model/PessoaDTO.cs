using System;
using System.Text;
using System.Collections.Generic;


namespace CadastrosBaseService.Model {
    
    public class PessoaDTO {
        public PessoaDTO() { }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public string Cliente { get; set; }
        public string Fornecedor { get; set; }
        public string Colaborador { get; set; }
        public string Convenio { get; set; }
        public string Contador { get; set; }
        public string Transportadora { get; set; }

        public PessoaFisicaDTO PessoaFisica { get; set; }
        public PessoaJuridicaDTO PessoaJuridica { get; set; }

        public IList<ContatoDTO> ListaContato { get; set; }
        public IList<EnderecoDTO> ListaEndereco { get; set; }
    }
}
