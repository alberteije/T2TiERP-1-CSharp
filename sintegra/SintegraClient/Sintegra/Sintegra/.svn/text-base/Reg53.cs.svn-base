using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sintegra
{
    public class Reg53
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
        public decimal baseCalculoICMS { get; set; }
        public decimal icmsRetido { get; set; }
        public decimal DespesasAcessorias { get; set; }
        public decimal Situacao { get; set; }
        public decimal codigoAntecipacao { get; set; }
        public string brancos { get; set; }

        public Reg53()
        {
            tipo = 53;
        }

        public string gerarLinhaTexto()
        {
            string retorno = "";

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
            retorno += Funcoes.Formatar(baseCalculoICMS.ToString("N2"), 13, false, '0');
            retorno += Funcoes.Formatar(icmsRetido.ToString("N2"), 13, false, '0');
            retorno += Funcoes.Formatar(DespesasAcessorias.ToString("N2"), 13, false, '0');
            retorno += Funcoes.Formatar(Situacao.ToString("N2"), 1, true, ' ');
            retorno += Funcoes.Formatar(codigoAntecipacao.ToString(), 1, true, ' ');
            retorno += Funcoes.Formatar(" ", 29, true, ' ');

            return retorno;
        }
    }
}
