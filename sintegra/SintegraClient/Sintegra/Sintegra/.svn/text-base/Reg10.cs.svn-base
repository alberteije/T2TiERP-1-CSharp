using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sintegra
{
    public class Reg10
    {
        public int tipo { get; set; }
        public string cnpj { get; set; }
        public string inscricaoestadual { get; set; }
        public string nomecontribuinte { get; set; }
        public string municipio { get; set; }
        public string uf { get; set; }
        public string fax { get; set; }
        public DateTime dataInicial { get; set; }
        public DateTime dataFinal { get; set; }
        public int codigoIdentificacaoConvenio { get; set; }
        public int codigoIdentificacaoNatOp { get; set; }
        public int codigoFinalidadeArqMagnetico { get; set; }

        public Reg10()
        {
            tipo = 10;
        }

        public string gerarLinhaTexto()
        {
            string retorno = "";

            retorno += Funcoes.Formatar(tipo.ToString(), 2, true, '0');
            retorno += Funcoes.Formatar(cnpj, 14, true, '0');
            retorno += Funcoes.Formatar(inscricaoestadual, 14, true, ' ');
            retorno += Funcoes.Formatar(nomecontribuinte, 35, true, ' ');
            retorno += Funcoes.Formatar(municipio, 30, true, ' ');
            retorno += Funcoes.Formatar(uf, 2, true, ' ');
            retorno += Funcoes.Formatar(fax, 10, false, '0');
            retorno += Funcoes.Formatar(dataInicial.ToString("yyyyMMdd"), 8, true, ' ');
            retorno += Funcoes.Formatar(dataFinal.ToString("yyyyMMdd"), 8, true, ' ');
            retorno += Funcoes.Formatar(codigoIdentificacaoConvenio.ToString(), 1, true, ' ');
            retorno += Funcoes.Formatar(codigoIdentificacaoNatOp.ToString(), 1, true, ' ');
            retorno += Funcoes.Formatar(codigoFinalidadeArqMagnetico.ToString(), 1, true, ' ');

            return retorno;
        }
        
    }
}
