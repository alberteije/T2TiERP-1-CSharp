using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sintegra
{
    public class Reg74
    {
        public int tipo { get; set; }
        public DateTime dataInventario { get; set; }
        public int codigoProduto { get; set; }
        public decimal qtd { get; set; }
        public decimal valorProduto { get; set; }
        public decimal codigoPoseMercadoriaInventariadas { get; set; }
        public string cnpjPossuidorMercadoria { get; set; }
        public string inscricaoEstadualProprietario { get; set; }
        public string ufProprietario { get; set; }

        public Reg74()
        {
            tipo = 74;
        }

        public string gerarLinhaTexto()
        {
            string retorno = "";

            retorno += Funcoes.Formatar(tipo.ToString(), 2, true, '0');
            retorno += Funcoes.Formatar(dataInventario.ToString("yyyyMMdd"), 8, true, '0');
            retorno += Funcoes.Formatar(codigoProduto.ToString(), 14, true, ' ');
            retorno += Funcoes.Formatar(qtd.ToString("N2"), 13, false, '0');
            retorno += Funcoes.Formatar(valorProduto.ToString("N2"), 13, false, '0');
            retorno += Funcoes.Formatar(codigoPoseMercadoriaInventariadas.ToString(), 1, false, '0');            
            
            if (codigoPoseMercadoriaInventariadas == 1)
                retorno += Funcoes.Formatar("0", 14, false, '0');
            else
                retorno += Funcoes.Formatar(cnpjPossuidorMercadoria, 14, false, '0');

            if (codigoPoseMercadoriaInventariadas == 1)
                retorno += Funcoes.Formatar(" ", 14, true, ' ');
            else
                retorno += Funcoes.Formatar(inscricaoEstadualProprietario, 14, true, ' ');
            retorno += Funcoes.Formatar(ufProprietario, 2, true, ' ');
            retorno += Funcoes.Formatar(" ", 45, true, ' ');

            return retorno;            
        }

    }
}
