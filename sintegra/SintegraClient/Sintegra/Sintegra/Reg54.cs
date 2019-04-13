using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sintegra
{
    public class Reg54
    {
        public int tipo { get; set; }
        public string cnpj { get; set; }
        public string modelo { get; set; }
        public string serie { get; set; }
        public int numero { get; set; }
        public string cfop { get; set; }
        public string cst { get; set; }
        public string numeroItem { get; set; }
        public string codigoProduto { get; set; }
        public decimal qtd { get; set; }
        public decimal valorProduto { get; set; }
        public decimal valorDescontoDespAcessoria { get; set; }
        public decimal baseCalcICMS { get; set; }
        public decimal baseCalcIcmsST { get; set; }
        public decimal valorIPI { get; set; }
        public decimal aliqICMS { get; set; }        

        public Reg54()
        {
            tipo = 54;
        }

        public string gerarLinhaTexto()
        {
            string retorno = "";

            retorno += Funcoes.Formatar(tipo.ToString(), 2, true, '0');
            retorno += Funcoes.Formatar(cnpj, 14, false, '0');
            retorno += Funcoes.Formatar(modelo, 2, false, '0');
            retorno += Funcoes.Formatar(serie, 3, true, ' ');
            retorno += Funcoes.Formatar(numero.ToString(), 6, false, '0');
            retorno += Funcoes.Formatar(cfop, 4, false, '0');
            retorno += Funcoes.Formatar(cst, 3, false, '0');
            retorno += Funcoes.Formatar(numeroItem, 3, true, '0');
            retorno += Funcoes.Formatar(codigoProduto, 14, true, ' ');
            retorno += Funcoes.Formatar(qtd.ToString("N2"), 11, false, '0');
            retorno += Funcoes.Formatar(valorProduto.ToString("N2"), 12, false, '0');
            retorno += Funcoes.Formatar(valorDescontoDespAcessoria.ToString("N2"), 12, false, '0');
            retorno += Funcoes.Formatar(baseCalcICMS.ToString("N2"), 12, false, '0');
            retorno += Funcoes.Formatar(baseCalcIcmsST.ToString("N2"), 12, false, '0');
            retorno += Funcoes.Formatar(valorIPI.ToString("N2"), 12, false, '0');
            retorno += Funcoes.Formatar(aliqICMS.ToString("N2"), 4, false, '0');

            return retorno;
        }
    }
}
