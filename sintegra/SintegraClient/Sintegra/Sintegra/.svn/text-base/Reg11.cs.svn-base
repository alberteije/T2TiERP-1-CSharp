using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sintegra
{
    public class Reg11
    {
        public int tipo { get; set; }
        public string logradouro { get; set; }
        public int numero { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
        public string nomeContato { get; set; }
        public string telefone { get; set; }
        

        public Reg11()
        {
            tipo = 11;
        }

        public string gerarLinhaTexto()
        {
            string retorno = "";

            retorno += Funcoes.Formatar(tipo.ToString(), 2, true, '0');
            retorno += Funcoes.Formatar(logradouro, 34, true, ' ');
            retorno += Funcoes.Formatar(numero.ToString(), 5, true, '0');
            retorno += Funcoes.Formatar(complemento, 22, true, ' ');
            retorno += Funcoes.Formatar(bairro, 15, true, ' ');
            retorno += Funcoes.Formatar(cep, 8, true, ' ');
            retorno += Funcoes.Formatar(nomeContato, 28, true, ' ');
            retorno += Funcoes.Formatar(telefone, 12, false, '0');
            
            return retorno;
        }
    }
}
