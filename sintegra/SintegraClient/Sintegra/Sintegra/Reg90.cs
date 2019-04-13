using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sintegra
{
    public class Reg90
    {
        public int tipo { get; set; }
        public string cnpj { get; set; }
        public string inscricaoEstadual { get; set; }
        public int tipoTotalizado { get; set; }
        public int totalRegistos { get; set; }
        public int totalGeralRegistros { get; set; }
        public string numeroRegistrosTipo90 { get; set; }

        public Reg90()
        {
            tipo = 90;
        }

        public string gerarLinhaTexto()
        {
            string retorno = "";
            retorno += Funcoes.Formatar(tipo.ToString(), 2, true, '0');
            retorno += Funcoes.Formatar(cnpj, 14, false, '0');
            retorno += Funcoes.Formatar(inscricaoEstadual, 14, true, ' ');
            retorno += Funcoes.Formatar(tipoTotalizado.ToString(), 2, true, '0');
            retorno += Funcoes.Formatar(totalRegistos.ToString(), 8, false, '0');
            retorno += Funcoes.Formatar(totalGeralRegistros.ToString(), 8, false, '0');
            retorno += Funcoes.Formatar(" ", 77, true, ' ');
            retorno += Funcoes.Formatar(numeroRegistrosTipo90, 1, false, '0');
            return retorno;
        }
    }
}
