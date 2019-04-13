using System;
using System.Collections.Generic;
using System.Text;

namespace UniNFeLibrary
{
    #region Classe Extensões dos XML´s ou TXT´s de envio
    /// <summary>
    /// Classe das Extensões dos XML´s ou TXT´s de envio
    /// </summary>
    public class ExtXml
    {
        public const string AltCon = "-alt-con.xml";
        public const string AltCon_TXT = "-alt-con.txt";
        public const string ConsCad = "-cons-cad.xml";
        public const string ConsCad_TXT = "-cons-cad.txt";
        public const string ConsInf = "-cons-inf.xml";
        public const string ConsInf_TXT = "-cons-inf.txt";
        public const string GerarChaveNFe_XML = "-gerar-chave.xml";
        public const string GerarChaveNFe_TXT = "-gerar-chave.txt";
        public const string MontarLote = "-montar-lote.xml";
        public const string EnvLot = "-env-lot.xml";
        public static string Nfe = "-nfe.xml";
        public const string PedCan = "-ped-can.xml";
        public const string PedCan_TXT = "-ped-can.txt";
        public const string PedInu = "-ped-inu.xml";
        public const string PedInu_TXT = "-ped-inu.txt";
        public const string PedRec = "-ped-rec.xml";
        public const string PedSit = "-ped-sit.xml";
        public const string PedSit_TXT = "-ped-sit.txt";
        public const string PedSta = "-ped-sta.xml";
        public const string PedSta_TXT = "-ped-sta.txt";
        /// <summary>
        /// XML de envio do DPEC
        /// </summary>
        public const string EnvDPEC = "-env-DPEC.xml";
        public const string EnvDPEC_TXT = "-env-DPEC.txt";
        /// <summary>
        /// XML de consulta do registro do DPEC
        /// </summary>
        public const string ConsDPEC = "-cons-DPEC.xml";
        public const string ConsDPEC_TXT = "-cons-DPEC.txt";
    }
    #endregion

    #region Classe das Extensões dos XML´s e TXT´s de retorno
    /// <summary>
    /// Classe das extensões dos XML´s de retorno
    /// </summary>
    public class ExtXmlRet
    {
        /// <summary>
        /// Retorno da consulta situação da NFe (-sit.xml)
        /// </summary>
        public const string Sit = "-sit.xml";
        /// <summary>
        /// Retorno da consulta situação da NFe quando com erro (-sit.err)
        /// </summary>
        public const string Sit_ERR = "-sit.err";
        /// <summary>
        /// Retorno da consulta do cadastro do contribuinte (-ret-cons-cad.xml)
        /// </summary>
        public const string ConsCad = "-ret-cons-cad.xml";
        /// <summary>
        /// Retorno da consulta do cadastro do contribuinte quando com erro (-ret-cons-cad.err)
        /// </summary>
        public const string ConsCad_ERR = "-ret-cons-cad.err";
        /// <summary>
        /// Retorno da consulta do recibo do lote das nfe´s (-pro-rec.xml)
        /// </summary>
        public const string ProRec = "-pro-rec.xml";
        /// <summary>
        /// Retorno da consulta do recibo do lote das nfe´s quando com erro (-pro-rec.err)
        /// </summary>
        public const string ProRec_ERR = "-pro-rec.err";
        /// <summary>
        /// Retorno do status do serviço da nfe (-sta.xml)
        /// </summary>
        public const string Sta = "-sta.xml";
        /// <summary>
        /// Retorno do status do serviço da nfe quando com erro (-sta.err)
        /// </summary>
        public const string Sta_ERR = "-sta.err";
        /// <summary>
        /// Retorno do cancelamento da nfe quando com erro (-can.err)
        /// </summary>
        public const string Can_ERR = "-can.err";
        /// <summary>
        /// Retorno da inutilizacao de números de NFe quando com erro (-inu.err)
        /// </summary>
        public const string Inu_ERR = "-inu.err";
        /// <summary>
        /// Retorno da validação da NFe quando com erro (-nfe.err)
        /// </summary>
        public static string Nfe_ERR = "-nfe.err";
        /// <summary>
        /// Retorno do recibo no envio do lote de NFe quando com erro (-rec.err)
        /// </summary>
        public static string Rec_ERR = "-rec.err";
        /// <summary>
        /// XML de Distribuição da NFe (-procNFe.xml)
        /// </summary>
        public static string ProcNFe = "-procNFe.xml";
        /// <summary>
        /// XML de Distribuição do cancelamento da NFe
        /// </summary>
        public static string ProcCancNFe = "-procCancNFe.xml";
        /// <summary>
        /// XML de Distribuição da inutilização de números da NFe
        /// </summary>
        public static string ProcInutNFe = "-procInutNFe.xml";
        /// <summary>
        /// XML de retorno do registro do DPEC
        /// </summary>
        public const string retDPEC = "-ret-DPEC.xml";
        /// <summary>
        /// Retorno do registro do DPEC quando ocorre algum erro
        /// </summary>
        public const string retDPEC_ERR = "-ret-DPEC.err";
        /// <summary>
        /// XML de retorno da consulta do registro do DPEC
        /// </summary>
        public const string retConsDPEC = "-ret-Cons-DPEC.xml";
        /// <summary>
        /// Retorno da consulta do registro do DPEC quando ocorre algum erro
        /// </summary>
        public const string retConsDPEC_ERR = "-ret-Cons-DPEC.err";
    }
    #endregion

    #region Classe dos tipos de emissão da NFe
    /// <summary>
    /// Tipo de emissão da NFe - danasa 8-2009
    /// </summary>
    public class TipoEmissao
    {
        public const int teNormal = 1;
        public const int teContingencia = 2;
        public const int teSCAN = 3;
        public const int teDPEC = 4;
        public const int teFSDA = 5;
    }
    #endregion

    #region Classe dos tipos de ambiente da NFe
    /// <summary>
    /// Tipos de ambientes da NFe - danasa 8-2009
    /// </summary>
    public class TipoAmbiente
    {
        public const int taProducao = 1;
        public const int taHomologacao = 2;
    }
    #endregion

    #region Classe dos Parmâmetros necessários para o envio dos XML´s
    /// <summary>
    /// Parâmetros necessários para o envio dos XML´s
    /// </summary>
    public class ParametroEnvioXML
    {
        public int tpAmb { get; set; }
        public int tpEmis { get; set; }
        public int UFCod { get; set; }
    }
    #endregion
}
