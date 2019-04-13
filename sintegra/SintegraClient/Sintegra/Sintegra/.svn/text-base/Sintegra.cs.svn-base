using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Sintegra;

namespace Sintegra
{
    public class Sintegra
    {

        private int _idEmitente;
        private DateTime _dtaIni;
        private DateTime _dtaFin;

        public Reg10 reg10 { get; set; }
        public Reg11 reg11 { get; set; }
        public List<Reg50> regs50 = new List<Reg50>();
        public List<Reg51> regs51 = new List<Reg51>();
        public List<Reg53> regs53 = new List<Reg53>();
        public List<Reg54> regs54 = new List<Reg54>();
        public List<Reg74> regs74 = new List<Reg74>();
        public List<Reg75> regs75 = new List<Reg75>();
        public List<Reg90> regs90 = new List<Reg90>();        

        public Sintegra(DateTime dtaIni, DateTime dtaFin, int idEmitente)
        {
            _idEmitente = idEmitente;
            _dtaIni = dtaIni;
            _dtaFin = dtaFin;
        }

        public void gerarArquivoSintegra(string salvarEm)
        {
            if ((reg10 == null) || (reg11 == null) || ((regs90 == null) || (regs90.Count == 0)))
            {
                throw new Exception("Os registros primários (reg10, reg11 e reg90) não foram preenchidos, não é possível gerar o SINTEGRA, verifique se os dados estão carregados");                
            }

            StringBuilder sb = new StringBuilder("");
            sb.AppendLine(reg10.gerarLinhaTexto());
            sb.AppendLine(reg11.gerarLinhaTexto());
            
            foreach (Reg50 reg50 in regs50)
            {
                sb.AppendLine(reg50.gerarLinhaTexto());
            }

            foreach (Reg51 reg51 in regs51)
            {
                sb.AppendLine(reg51.gerarLinhaTexto());
            }

            foreach (Reg53 reg53 in regs53)
            {
                sb.AppendLine(reg53.gerarLinhaTexto());
            }

            foreach (Reg54 reg54 in regs54)
            {
                sb.AppendLine(reg54.gerarLinhaTexto());
            }

            foreach (Reg74 reg74 in regs74.OrderBy(c => c.codigoProduto))
            {
                sb.AppendLine(reg74.gerarLinhaTexto());
            }

            foreach (Reg75 reg75 in regs75)
            {
                sb.AppendLine(reg75.gerarLinhaTexto());
            }

            //Suporte a apenas um registro 90 por enquanto
            string registro90 = "";
            registro90 += Funcoes.Formatar("90", 2, true, '0');
            registro90 += Funcoes.Formatar(regs90[0].cnpj, 14, false, '0');
            registro90 += Funcoes.Formatar(regs90[0].inscricaoEstadual, 14, true, ' ');

            foreach (Reg90 reg90 in regs90.OrderBy(c => c.tipoTotalizado))
            {
                registro90 += Funcoes.Formatar(reg90.tipoTotalizado.ToString(), 2, true, '0');
                registro90 += Funcoes.Formatar(reg90.totalRegistos.ToString(), 8, false, '0');
                registro90 += Funcoes.Formatar(99.ToString(), 2, true, '0');
                registro90 += Funcoes.Formatar(reg90.totalGeralRegistros.ToString(), 8, false, '0');                
            }
            registro90 += Funcoes.Formatar("1", 126 - registro90.Length, false, ' ');
            //registro90 += Funcoes.Formatar("1", (9-regs90.Count)*10 + 6, false, ' ');
            sb.AppendLine(registro90);

            TextWriter tw = new StreamWriter(salvarEm);
            tw.Write(sb.ToString());
            tw.Close();

            if (File.Exists(salvarEm))
                Process.Start(salvarEm);
        }

        public void CarregarDados()
        {
            //Utilize esta função para preencher os dados da nota
        }
    }
}
