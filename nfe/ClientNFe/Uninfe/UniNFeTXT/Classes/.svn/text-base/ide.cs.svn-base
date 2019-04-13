using System;
using System.Collections.Generic;
using System.Text;

namespace UniNFeTXT
{
    public class Ide
    {
        public int cUF { get; set; }
        public int cNF { get; set; }
        public string natOp { get; set; }
        public int indPag { get; set; }
        public int mod { get; set; }
        public int serie { get; set; }
        public int nNF { get; set; }
        public DateTime dEmi { get; set; }
        public DateTime dSaiEnt { get; set; }
        public int tpNF { get; set; }
        public int cMunFG { get; set; }
        public int tpImp { get; set; }
        public int tpEmis { get; set; }
        public int cDV { get; set; }
        public int tpAmb { get; set; }
        public int finNFe { get; set; }
        public string procEmi { get; set; }
        public string verProc { get; set; }
        public List<NFref> NFref { get; set; }

        public Ide()
        {
            NFref = new List<NFref>();
        }
    }

    public class RefNF
    {
        public int cUF;
        public string AAMM;
        public string CNPJ;
        public string mod;
        public int serie;
        public int nNF;
    }

    public class NFref
    {
        public string refNFe { get; set; } 
        public RefNF RefNF { get; set; }

        public NFref()
        {
            RefNF = new RefNF();
        }
        public NFref(string nfe)
        {
            this.refNFe = nfe;
        }
    }


}
