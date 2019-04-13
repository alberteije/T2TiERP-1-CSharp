using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sintegra
{
    public class Reg75
    {
        public int tipo { get; set; }
        public DateTime dataInicial { get; set; }
        public DateTime dataFinal { get; set; }
        public string codigoProduto { get; set; }
        public string codigoNCM { get; set; }
        public string descricao { get; set; }
        public string unidadeMedidaComercializacao { get; set; }
        public decimal aliqIPI { get; set; }
        public decimal aliqICMS { get; set; }
        public decimal redBaseCalcICMS { get; set; }
        public decimal baseCalcIcmsST { get; set; }

        public Reg75()
        {
            tipo = 75;
        }

        public string gerarLinhaTexto()
        {
            string retorno = "";

            retorno += Funcoes.Formatar(tipo.ToString(), 2, true, '0');
            retorno += Funcoes.Formatar(dataInicial.ToString("yyyyMMdd"), 8, true, '0');
            retorno += Funcoes.Formatar(dataFinal.ToString("yyyyMMdd"), 8, true, '0');
            retorno += Funcoes.Formatar(codigoProduto, 14, true, ' ');
            retorno += Funcoes.Formatar(codigoNCM, 8, true, ' ');
            retorno += Funcoes.Formatar(descricao, 53, true, ' ');
            retorno += Funcoes.Formatar(unidadeMedidaComercializacao, 6, true, ' ');
            retorno += Funcoes.Formatar(aliqIPI.ToString("N2"), 5, false, '0');
            retorno += Funcoes.Formatar(aliqICMS.ToString("N2"), 4, false, '0');
            retorno += Funcoes.Formatar(redBaseCalcICMS.ToString("N2"), 5, false, '0');
            retorno += Funcoes.Formatar(baseCalcIcmsST.ToString("N2"), 13, false, '0');

            return retorno;
        }
    }
}
