using System;
using System.Collections.Generic;
using System.Text;

namespace UniNFeTXT
{
    public enum TpcnTipoCampo { tcStr, tcInt, tcDat, tcDatHor, tcEsp, tcDec2, tcDec3, tcDec4 } // tcEsp = String: somente numeros;
    //public enum TpcnFormatoGravacao { fgXML, fgTXT }
    //public enum TpcnTagAssinatura { taSempre, taNunca, taSomenteSeAssinada, taSomenteParaNaoAssinada }
    public enum TpcnIndicadorPagamento { ipVista, ipPrazo, ipOutras }
    public enum TpcnTipoNFe { tnEntrada, tnSaida }
    public enum TpcnTipoImpressao { tiRetrato, tiPaisagem }
    public enum TpcnTipoEmissao { teNormal, teContingencia, teSCAN, teDPEC, teFSDA }
    public enum TpcnTipoAmbiente { taProducao, taHomologacao }
    //public enum TpcnSituacaoEmissor { seHomologacao, seProducao }
    public enum TpcnFinalidadeNFe { fnNormal, fnComplementar, fnAjuste }
    //public enum TpcnProcessoEmissao { peAplicativoContribuinte, peAvulsaFisco, peAvulsaContribuinte, peContribuinteAplicativoFisco }
    //public enum TpcnTipoOperacao { toVendaConcessionaria, toFaturamentoDireto, toVendaDireta, toOutros }
    //public enum TpcnCondicaoVeiculo { cvAcabado, cvInacabado, cvSemiAcabado }
    //public enum TpcnTipoArma { taUsoPermitido, taUsoRestrito }
    //public enum TpcnOrigemMercadoria { oeNacional, oeEstrangeiraImportacaoDireta, oeEstrangeiraAdquiridaBrasil }
    //public enum TpcnCSTIcms { cst00, cst10, cst20, cst30, cst40, cst41, cst50, cst51, cst60, cst70, cst90 }
    //public enum TpcnDeterminacaoBaseIcms { dbiMargemValorAgregado, dbiPauta, dbiPrecoTabelado, dbiValorOperacao }
    //public enum TpcnDeterminacaoBaseIcmsST { dbisPrecoTabelado, dbisListaNegativa, dbisListaPositiva, dbisListaNeutra, dbisMargemValorAgregado, dbisPauta }
    //public enum TpcnCstIpi { ipi00, ipi49, ipi50, ipi99, ipi01, ipi02, ipi03, ipi04, ipi05, ipi51, ipi52, ipi53, ipi54, ipi55 }
    //public enum TpcnCstPis { pis01, pis02, pis03, pis04, pis06, pis07, pis08, pis09, pis99 }
    //public enum TpcnCstCofins { cof01, cof02, cof03, cof04, cof06, cof07, cof08, cof09, cof99 }
    //public enum TpcnModalidadeFrete { mfContaEmitente, mfContaDestinatario }
    //public enum TpcnIndicadorProcesso { ipSEFAZ, ipJusticaFederal, ipJusticaEstadual, ipSecexRFB, ipOutros }

    public struct ICMS
    {
        public int orig;
        public string CST;
        public string modBC;
        public double pRedBC;
        public double vBC;
        public double pICMS;
        public double vICMS;
        public string modBCST;
        public double pMVAST;
        public double pRedBCST;
        public double vBCST;
        public double pICMSST;
        public double vICMSST;
    }

    public struct IPI
    {
        public string clEnq;
        public string CNPJProd;
        public string cSelo;
        public int qSelo;
        public string cEnq;
        public string CST;
        public double vBC;
        public double qUnid;
        public double vUnid;
        public double pIPI;
        public double vIPI;
    }

    public struct II
    {
        public double vBC;
        public double vDespAdu;
        public double vII;
        public double vIOF;
    }

    public struct PIS 
    {
        public string CST;
        public double vBC;
        public double pPIS;
        public double vPIS;
        public double qBCProd;
        public double vAliqProd;
    }

    public struct PISST
    {
        public double vBC;
        public double pPis;
        public double qBCProd;
        public double vAliqProd;
        public double vPIS;
    }

    public struct COFINS
    {
        public string CST;
        public double vBC;
        public double pCOFINS;
        public double vCOFINS;
        public double vBCProd;
        public double vAliqProd;
        public double qBCProd;
    }

    public struct Total
    {
        public ICMSTot ICMSTot;
        public ISSQNtot ISSQNtot;
        public retTrib retTrib;
    }

    public struct ICMSTot
    {
        public double vBC;
        public double vICMS;
        public double vBCST;
        public double vST;
        public double vProd;
        public double vFrete;
        public double vSeg;
        public double vDesc;
        public double vII;
        public double vIPI;
        public double vPIS;
        public double vCOFINS;
        public double vOutro;
        public double vNF;
    }

    public struct ISSQNtot
    {
        public double vServ;
        public double vBC;
        public double vISS;
        public double vPIS;
        public double vCOFINS;
    }

    public struct retTrib
    {
        public double vRetPIS;
        public double vRetCOFINS;
        public double vRetCSLL;
        public double vBCIRRF;
        public double vIRRF;
        public double vBCRetPrev;
        public double vRetPrev;
    }

    public struct COFINSST
    {
        public double vBC;
        public double pCOFINS;
        public double qBCProd;
        public double vAliqProd;
        public double vCOFINS;
    }

    public struct ISSQN
    {
        public double vBC;
        public double vAliq;
        public double vISSQN;
        public int cMunFG;
        public int cListServ;
    }

    public class Transp
    {
        public int modFrete;
        public Transporta Transporta;
        public retTransp retTransp;
        public veicTransp veicTransp;
        public List<Vol> Vol;
        public List<Reboque> Reboque;

        public Transp()
        {
            Vol = new List<Vol>();
            Reboque = new List<Reboque>();
        }
    }

    public class Reboque
    {
        public string placa;
        public string UF;
        public string RNTC;
    }

    public struct Transporta
    {
        public string CNPJCPF;
        public string xNome;
        public string IE;
        public string xEnder;
        public string xMun;
        public string UF;
    }

    public class Vol
    {
        public int qVol;
        public string esp;
        public string marca;
        public string nVol;
        public double pesoL;
        public double pesoB;
        public List<Lacres> Lacres;

        public Vol()
        {
            this.Lacres = new List<Lacres>();
        }
    }

    public class Lacres
    {
        public string nLacre;
    }

    public class Cobr
    {
        public Fat Fat;
        public List<Dup> Dup;

        public Cobr()
        {
            Dup = new List<Dup>();
        }
    }

    public struct Fat
    {
        public string nFat;
        public double vOrig;
        public double vDesc;
        public double vLiq;
    }

    public class Dup
    {
        public string nDup;
        public DateTime dVenc;
        public double vDup;
    }

    public class InfAdic
    {
        public string infAdFisco;
        public string infCpl;
        public List<obsCont> obsCont;
        public List<obsFisco> obsFisco;
        public List<procRef> procRef;

        public InfAdic()
        {
            obsCont = new List<obsCont>();
            obsFisco = new List<obsFisco>();
            procRef = new List<procRef>();
        }
    }

    public class obsCont
    {
        public string xCampo;
        public string xTexto;
    }

    public class obsFisco
    {
        public string xCampo;
        public string xTexto;
    }

    public class procRef
    {
        public string nProc;
        public string indProc;
    }

    public struct Exporta 
    {
        public string UFEmbarq;
        public string xLocEmbarq;
    }

    public struct Compra
    {
        public string xNEmp;
        public string xPed;
        public string xCont;
    }

    public struct retTransp
    {
        public double vServ;
        public double vBCRet;
        public double pICMSRet;
        public double vICMSRet;
        public string CFOP;
        public int cMunFG;
    }

    public struct veicTransp
    {
        public string placa;
        public string UF;
        public string RNTC;
    }

    /// <summary>
    /// infNFe
    /// </summary>
    public struct infNFe
    {
        public string ID;
    }

    /// <summary>
    /// NFe
    /// </summary>
    public class NFe
    {
        public Ide ide { get; private set; }
        public Emit emit { get; private set; }
        public Dest dest { get; private set; }
        public Avulsa avulsa { get; private set; }
        public Entrega entrega { get; private set; }

        public infNFe infNFe;
        public Retirada retirada { get; private set; }
        public List<Det> det { get; private set; }
        public Total Total;
        public Transp Transp { get; private set; }
        public Cobr Cobr { get; private set; }
        public InfAdic InfAdic { get; private set; }
        public Exporta exporta;
        public Compra compra;

        public NFe()
        {
            ide = new Ide();
            emit = new Emit();
            dest = new Dest();
            avulsa = new Avulsa();
            entrega = new Entrega();
            retirada = new Retirada();
            det = new List<Det>();
            Transp = new Transp();
            Cobr = new Cobr();
            InfAdic = new InfAdic();
        }
    }
}
