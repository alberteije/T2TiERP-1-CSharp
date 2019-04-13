using System;
using System.Collections.Generic;
using System.Text;

namespace UniNFeTXT
{
    public struct Adi
    {
        public int nAdicao;
        public int nSeqAdi;
        public string cFabricante;
        public double vDescDI;
    }

    public class Arma
    {
        public string tpArma;
        public int nSerie;
        public int nCano;
        public string descr;
    }

    public struct CIDE
    {
        public double qBCprod;
        public double vAliqProd;
        public double vCIDE;
    }

    public struct Comb
    {
        public int cProdANP;
        public string CODIF;
        public double qTemp;
        public CIDE CIDE;
        public ICMSComb ICMS;
        public ICMSInter ICMSInter;
        public ICMSCons ICMSCons;
    }

    public class Det
    {
        public Prod Prod;
        public Imposto Imposto;
        public string infAdProd;

        public Det()
        {
            Prod = new Prod();
            Imposto = new Imposto();
        }
    }

    public class DI
    {
        public string nDi;
        public DateTime dDi;
        public string xLocDesemb;
        public string UFDesemb;
        public DateTime dDesemb;
        public string cExportador;
        public List<Adi> adi;

        public DI()
        {
            adi = new List<Adi>();
        }
    }

    public struct ICMSComb
    {
        public double vBCICMS;
        public double vICMS;
        public double vBCICMSST;
        public double vICMSST;
    }

    public struct ICMSInter
    {
        public double vBCICMSSTDest;
        public double vICMSSTDest;
    }

    public struct ICMSCons
    {
        public double vBCICMSSTCons;
        public double vICMSSTCons;
        public string UFcons;
    }

    public class Imposto
    {
        public IPI IPI;
        public ICMS ICMS;
        public II II;
        public PIS PIS;
        public PISST PISST;
        public COFINS COFINS;
        public COFINSST COFINSST;
        public ISSQN ISSQN;
        public ICMSTot ICMSTot;
        public ISSQNtot ISSQNtot;
        public retTrib retTrib;
    }

    public class Med
    {
        public string nLote;
        public double qLote;
        public DateTime dFab;
        public DateTime dVal;
        public double vPMC;
    }

    public struct veicProd
    {
        public string tpOP;
        public string chassi;
        public string cCor;
        public string xCor;
        public string pot;
        public string CM3;
        public string pesoL;
        public string pesoB;
        public string nSerie;
        public string tpComb;
        public string nMotor;
        public string CMKG;
        public string dist;
        public string RENAVAM;
        public int anoMod;
        public int anoFab;
        public string tpPint;
        public int tpVeic;
        public int espVeic;
        public string VIN;
        public string condVeic;
        public string cMod;
    }

    public class Prod
    {
        public string cProd;
        public int nItem;
        public string cEAN;
        public string xProd;
        public string NCM;
        public string EXTIPI;
        public int genero;
        public string CFOP;
        public string uCom;
        public double qCom;
        public double vUnCom;
        public double vProd;
        public string cEANTrib;
        public string uTrib;
        public double qTrib;
        public double vUnTrib;
        public double vFrete;
        public double vSeg;
        public double vDesc;
        public List<DI> DI;
        public veicProd veicProd;
        public List<Med> med;
        public List<Arma> arma;
        public Comb comb;

        public Prod()
        {
            DI = new List<DI>();
            med = new List<Med>();
            arma = new List<Arma>();
        }
    }
}
