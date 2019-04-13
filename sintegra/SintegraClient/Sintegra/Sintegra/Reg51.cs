using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Sintegra
{
    public class Reg51
    {
        public int tipo { get; set; }
        public string cnpj { get; set; }
        public string inscricaoestadual { get; set; }
        public DateTime dataEmissaoRecebimento { get; set; }
        public string uf { get; set; }        
        public string serie { get; set; }
        public int numero { get; set; }
        public string cfop { get; set; }        
        public decimal valorTotal { get; set; }
        public decimal valorIPI { get; set; }
        public decimal IsentaOuNaoTribIPI { get; set; }       
        public decimal outrasIpi { get; set; }        
        public string situacaoCancelamento { get; set; }

        public Reg51()
        {
            tipo = 51;
        }

        public string gerarLinhaTexto()
        {
            string retorno = "";

            CultureInfo ci = new CultureInfo("pt-BR");

            retorno += Funcoes.Formatar(tipo.ToString(), 2, true, '0');
            retorno += Funcoes.Formatar(cnpj, 14, false, '0');
            retorno += Funcoes.Formatar(inscricaoestadual, 14, true, ' ');
            retorno += Funcoes.Formatar(dataEmissaoRecebimento.ToString("yyyyMMdd"), 8, true, ' ');
            retorno += Funcoes.Formatar(uf, 2, true, ' ');            
            retorno += Funcoes.Formatar(serie, 3, true, ' ');
            retorno += Funcoes.Formatar(numero.ToString(), 6, false, '0');
            retorno += Funcoes.Formatar(cfop, 4, true, ' ');
            retorno += Funcoes.Formatar(valorTotal.ToString("0.00", ci), 13, false, '0');
            retorno += Funcoes.Formatar(valorIPI.ToString("0.00", ci), 13, false, '0');
            retorno += Funcoes.Formatar(IsentaOuNaoTribIPI.ToString("0.00", ci), 13, false, '0');
            retorno += Funcoes.Formatar(outrasIpi.ToString("0.00", ci), 13, false, '0');
            retorno += Funcoes.Formatar(" ", 20, true, ' ');
            retorno += Funcoes.Formatar(situacaoCancelamento, 1, true, ' ');

            return retorno;
        }
    }
}
