using System;
using System.Collections.Generic;
using System.Text;

namespace UniNFeTXT
{
    public class Dest
    {
        public string CNPJCPF;
        public string xNome;
        public enderDest enderDest;
        public string IE;
        public string ISUF;

        public Dest()
        {
            this.enderDest = new enderDest();
        }
    }

    public class enderDest
    {
        public string xLgr;
        public string nro;
        public string xCpl;
        public string xBairro;
        public int cMun;
        public string xMun;
        public string UF;
        public int CEP;
        public int cPais;
        public string xPais;
        public string fone;
    }
}
