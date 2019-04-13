using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Sintegra
{
    public class Reg50
    {
        public int tipo { get; set; }
        public string cnpj { get; set; }
        public string inscricaoestadual { get; set; }
        public DateTime dataEmissaoRecebimento { get; set; }
        public string uf { get; set; }
        public string modelo { get; set; }
        public string serie { get; set; }
        public int numero { get; set; }
        public string cfop { get; set; }
        public string emitente { get; set; }
        public decimal valorTotal { get; set; }
        public decimal baseCalculoICMS { get; set; }
        public decimal valorICMS { get; set; }
        public decimal isentaOuNaoTributada { get; set; }
        public decimal outras { get; set; }
        public decimal aliquota { get; set; }
        public string situacaoCancelamento { get; set; }

        public Reg50()
        {
            tipo = 50;
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
            retorno += Funcoes.Formatar(modelo, 2, true, ' ');
            retorno += Funcoes.Formatar(serie, 3, true, ' ');
            retorno += Funcoes.Formatar(numero.ToString(), 6, false, '0');
            retorno += Funcoes.Formatar(cfop, 4, true, ' ');
            retorno += Funcoes.Formatar(emitente, 1, true, ' ');
            retorno += Funcoes.Formatar(valorTotal.ToString("0.00",ci), 13, false, '0');
            retorno += Funcoes.Formatar(baseCalculoICMS.ToString("0.00", ci), 13, false, '0');
            retorno += Funcoes.Formatar(valorICMS.ToString("0.00", ci), 13, false, '0');
            retorno += Funcoes.Formatar(isentaOuNaoTributada.ToString("0.00", ci), 13, false, '0');
            retorno += Funcoes.Formatar(outras.ToString("0.00", ci), 13, false, '0');
            retorno += Funcoes.Formatar(aliquota.ToString("0.00", ci), 4, false, '0');
            retorno += Funcoes.Formatar(situacaoCancelamento, 1, true, ' ');

            return retorno;
        }
    }
}
