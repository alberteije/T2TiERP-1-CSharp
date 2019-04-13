using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SearchWindow.Attributes;
using CadastrosBaseClient.CadastrosPrincipaisReference;


namespace CadastrosBaseClient.Model
{
    public class ServicoCadastrosPrincipais : ServicoCadastrosPrincipaisClient
    {
        [SearchWindowDataSource(typeof(PessoaDTO))]
        public new List<PessoaDTO> selectPessoa(PessoaDTO Pessoa)
        {
            return base.selectPessoa(Pessoa);
        }
    }

}
