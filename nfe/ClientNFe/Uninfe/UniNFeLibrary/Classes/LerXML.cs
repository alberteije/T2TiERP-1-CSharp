//------------------------------------------------------------------------------ 
// <copyright file="UniLerXMLClass.cs" company="Unimake"> 
// 
// Copyright (c) 2008 Unimake Softwares. All rights reserved.
//
// Programador: Wandrey Mundin Ferreira
// 
// </copyright> 
//------------------------------------------------------------------------------ 

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading;

namespace UniNFeLibrary
{
    /// <summary>
    /// Classe responsável por ler os diversos XML´s utilizados na nota fiscal eletrônica
    /// e dispor as informações em propriedades para facilitar a leitura.
    /// </summary>
    public class LerXML
    {
        #region Classes

        #region Classe com os Dados do XML da Consulta Cadastro do Contribuinte
        public class DadosConsCad
        {
            private string mUF;

            public DadosConsCad()
            {
                this.tpAmb = TipoAmbiente.taProducao;// "1";
            }

            /// <summary>
            /// Unidade Federativa (UF) - Sigla
            /// </summary>
            public string UF
            {
                get
                {
                    return this.mUF;
                }
                set
                {
                    this.mUF = value;
                    this.cUF = 0;// string.Empty;

                    switch (this.mUF.ToUpper().Trim())
                    {
                        case "AC":
                            this.cUF = 12;
                            break;

                        case "AL":
                            this.cUF = 27;
                            break;

                        case "AP":
                            this.cUF = 16;
                            break;

                        case "AM":
                            this.cUF = 13;
                            break;

                        case "BA":
                            this.cUF = 29;
                            break;

                        case "CE":
                            this.cUF = 23;
                            break;

                        case "DF":
                            this.cUF = 53;
                            break;

                        case "ES":
                            this.cUF = 32;
                            break;

                        case "GO":
                            this.cUF = 52;
                            break;

                        case "MA":
                            this.cUF = 21;
                            break;

                        case "MG":
                            this.cUF = 31;
                            break;

                        case "MS":
                            this.cUF = 50;
                            break;

                        case "MT":
                            this.cUF = 51;
                            break;

                        case "PA":
                            this.cUF = 15;
                            break;

                        case "PB":
                            this.cUF = 25;
                            break;

                        case "PE":
                            this.cUF = 26;
                            break;

                        case "PI":
                            this.cUF = 22;
                            break;

                        case "PR":
                            this.cUF = 41;
                            break;

                        case "RJ":
                            this.cUF = 33;
                            break;

                        case "RN":
                            this.cUF = 24;
                            break;

                        case "RO":
                            this.cUF = 11;
                            break;

                        case "RR":
                            this.cUF = 14;
                            break;

                        case "RS":
                            this.cUF = 43;
                            break;

                        case "SC":
                            this.cUF = 42;
                            break;

                        case "SE":
                            this.cUF = 28;
                            break;

                        case "SP":
                            this.cUF = 35;
                            break;

                        case "TO":
                            this.cUF = 17;
                            break;
                    }
                }
            }
            /// <summary>
            /// CPF
            /// </summary>
            public string CPF { get; set; }
            /// <summary>
            /// CNPJ
            /// </summary>
            public string CNPJ { get; set; }
            /// <summary>
            /// Inscrição Estadual
            /// </summary>
            public string IE { get; set; }
            /// <summary>
            /// Unidade Federativa (UF) - Código
            /// </summary>
            public int cUF { get; private set; }
            /// <summary>
            /// Ambiente (2-Homologação 1-Produção)
            /// </summary>
            public int tpAmb { get; private set; }
        }
        /// <summary>
        /// Esta herança que deve ser utilizada fora da classe para obter os valores das tag´s da consulta do cadastro do contribuinte
        /// </summary>
        public DadosConsCad oDadosConsCad = new DadosConsCad();
        #endregion

        #region Classe com os dados do XML da NFe
        /// <summary>
        /// Esta classe possui as propriedades que vai receber o conteúdo
        /// do XML da nota fiscal eletrônica
        /// </summary>
        public class DadosNFeClass
        {
            /// <summary>
            /// Chave da nota fisca
            /// </summary>
            public string chavenfe { get; set; }
            /// <summary>
            /// Data de emissão
            /// </summary>
            public DateTime dEmi { get; set; }
            /// <summary>
            /// Tipo de emissão 1-Normal 2-Contigência em papel de segurança 3-Contigência SCAN
            /// </summary>
            public string tpEmis { get; set; }
            /// <summary>
            /// Tipo de Ambiente 1-Produção 2-Homologação
            /// </summary>
            public string tpAmb { get; set; }
            /// <summary>
            /// Lote que a NFe faz parte
            /// </summary>
            public string idLote { get; set; }
            /// <summary>
            /// Série da NFe
            /// </summary>
            public string serie { get; set; }
            /// <summary>
            /// UF do Emitente
            /// </summary>
            public string cUF { get; set; }
            /// <summary>
            /// Número randomico da chave da nfe
            /// </summary>
            public string cNF { get; set; }
            /// <summary>
            /// Modelo da nota fiscal
            /// </summary>
            public string mod { get; set; }
            /// <summary>
            /// Número da nota fiscal
            /// </summary>
            public string nNF { get; set; }
            /// <summary>
            /// Dígito verificador da chave da nfe
            /// </summary>
            public string cDV { get; set; }
            /// <summary>
            /// CNPJ do emitente
            /// </summary>
            public string CNPJ { get; set; }
        }
        /// <summary>
        /// Esta herança que deve ser utilizada fora da classe para obter os valores das tag´s da nota fiscal eletrônica
        /// </summary>
        public DadosNFeClass oDadosNfe = new DadosNFeClass();
        #endregion

        #region Classe com os dados do XML do pedido de consulta do recibo do lote de nfe enviado
        /// <summary>
        /// Classe com os dados do XML do pedido de consulta do recibo do lote de nfe enviado
        /// </summary>
        public class DadosPedRecClass
        {
            /// <summary>
            /// Tipo de ambiente: 1-Produção 2-Homologação
            /// </summary>
            public int tpAmb { get; set; }
            /// <summary>
            /// Número do recibo do lote de NFe enviado
            /// </summary>
            public string nRec { get; set; }
            /// <summary>
            /// Tipo de Emissão: 1-Normal 2-Contingência FS 3-Contingência SCAN 4-Contingência DEPEC 5-Contingência FS-DA
            /// </summary>
            public int tpEmis { get; set; }
            /// <summary>
            /// Código da Unidade Federativa (UF)
            /// </summary>
            public int cUF { get; set; }
        }
        /// <summary>
        /// Esta Herança que deve ser utilizada fora da classe para obter os valores das tag´s do pedido de consulta do recibo do lote de NFe enviado
        /// </summary>
        public DadosPedRecClass oDadosPedRec = new DadosPedRecClass();
        #endregion

        #region Classe com os dados do XML do retorno do envio do Lote de NFe
        /// <summary>
        /// Esta classe possui as propriedades que vai receber o conteúdo do XML do recibo do lote
        /// </summary>
        public class DadosRecClass
        {
            /// <summary>
            /// Recibo do lote de notas fiscais enviado
            /// </summary>
            public string nRec { get; set; }
            /// <summary>
            /// Status do Lote
            /// </summary>
            public string cStat { get; set; }
            /// <summary>
            /// Tempo médio de resposta em segundos
            /// </summary>
            public int tMed { get; set; }
        }
        /// <summary>
        /// Esta herança que deve ser utilizada fora da classe para obter os valores das tag´s do recibo do lote
        /// </summary>
        public DadosRecClass oDadosRec = new DadosRecClass();
        #endregion

        #region Classe com os dados do XML da consulta do pedido de cancelamento
        /// <summary>
        /// Classe com os dados do XML da consulta do pedido de cancelamento
        /// </summary>
        public class DadosPedCanc
        {
            private string mchNFe;

            public int tpAmb { get; set; }
            public int tpEmis { get; set; }
            public int cUF { get; private set; }
            public string chNFe
            {
                get
                {
                    return this.mchNFe;
                }
                set
                {
                    this.mchNFe = value;
                    string serie = this.mchNFe.Substring(22, 3);
                    this.tpEmis = (Convert.ToInt32(serie) >= 900 ? TipoEmissao.teSCAN : this.tpEmis);
                    this.cUF = Convert.ToInt32(this.mchNFe.Substring(0, 2));
                }
            }
            public string nProt { get; set; }
            public string xJust { get; set; }

            public DadosPedCanc()
            {
                int emp = new FindEmpresaThread(Thread.CurrentThread).Index;
                this.tpEmis = Empresa.Configuracoes[emp].tpEmis;
            }
        }
        /// <summary>
        /// Esta herança que deve ser utilizada fora da classe para obter os valores das tag´s do pedido de cancelamento
        /// </summary>
        public DadosPedCanc oDadosPedCanc = new DadosPedCanc();
        #endregion

        #region Classe com os dados do XML do pedido de inutilização de números de NF
        /// <summary>
        /// Classe com os dados do XML do pedido de inutilização de números de NF
        /// </summary>
        public class DadosPedInut
        {
            private int mSerie;
            public int tpAmb { get; set; }
            public int tpEmis { get; set; }
            public int cUF { get; set; }
            public int ano { get; set; }
            public string CNPJ { get; set; }
            public int mod { get; set; }
            public int serie
            {
                get
                {
                    return this.mSerie;
                }
                set
                {
                    this.mSerie = value;
                    this.tpEmis = (value >= 900 ? TipoEmissao.teSCAN : this.tpEmis);
                }
            }
            public int nNFIni { get; set; }
            public int nNFFin { get; set; }
            public string xJust { get; set; }

            public DadosPedInut()
            {
                int emp = new FindEmpresaThread(Thread.CurrentThread).Index;
                this.tpEmis = Empresa.Configuracoes[emp].tpEmis;
            }
        }
        /// <summary>
        /// Esta herança que deve ser utilizada fora da classe para obter os valores das tag´s do pedido de inutilizacao
        /// </summary>
        public DadosPedInut oDadosPedInut = new DadosPedInut();
        #endregion

        #region Classe com os dados do XML da pedido de consulta da situação da NFe
        /// <summary>
        /// Classe com os dados do XML da pedido de consulta da situação da NFe
        /// </summary>
        public class DadosPedSit
        {
            private string mchNFe;

            /// <summary>
            /// Ambiente (2-Homologação ou 1-Produção)
            /// </summary>
            public int tpAmb { get; set; }
            /// <summary>
            /// Chave do documento fiscal
            /// </summary>
            public string chNFe
            {
                get
                {
                    return this.mchNFe;
                }
                set
                {
                    this.mchNFe = value;
                    if (this.mchNFe != string.Empty)
                    {
                        this.cUF = Convert.ToInt32(this.mchNFe.Substring(0, 2));
                        int serie = Convert.ToInt32(this.mchNFe.Substring(22, 3));
                        this.tpEmis = (serie >= 900 ? TipoEmissao.teSCAN : this.tpEmis);
                    }
                }
            }
            /// <summary>
            /// Código da Unidade Federativa (UF)
            /// </summary>
            public int cUF { get; private set; }
            /// <summary>
            /// Série da NFe que está sendo consultada a situação
            /// </summary>
            //            public string serie { get; private set; }
            /// <summary>
            /// Tipo de emissão para saber para onde será enviado a consulta da situação da nota
            /// </summary>
            public int tpEmis { get; set; }
            public int versaoNFe { get; set; }

            public DadosPedSit()
            {
                this.cUF = 0;
                //this.serie = string.Empty;
                this.tpEmis = TipoEmissao.teNormal;// ConfiguracaoApp.tpEmis;
            }
        }
        /// <summary>
        /// Esta herança que deve ser utilizada fora da classe para obter os valores das tag´s da consulta da situação da nota
        /// </summary>
        public DadosPedSit oDadosPedSit = new DadosPedSit();
        #endregion

        #region Classe com os dados do XML da consulta do status do serviço da NFe
        /// <summary>
        /// Classe com os dados do XML da consulta do status do serviço da NFe
        /// </summary>
        public class DadosPedSta
        {
            /// <summary>
            /// Ambiente (2-Homologação ou 1-Produção)
            /// </summary>
            public int tpAmb { get; set; }
            /// <summary>
            /// Código da Unidade Federativa (UF)
            /// </summary>
            public int cUF { get; set; }
            /// <summary>
            /// Tipo de Emissao (1-Normal, 2-Contingencia, 3-SCAN, ...
            /// </summary>
            public int tpEmis { get; set; }
        }
        /// <summary>
        /// Esta herança que deve ser utilizada fora da classe para obter os valores das tag´s do status do serviço
        /// </summary>
        public DadosPedSta oDadosPedSta = new DadosPedSta();
        #endregion

        #region Classe com os dados do XML de registro do DPEC
        /// <summary>
        /// Classe com os dados do XML de registro do DPEC
        /// </summary>
        public class DadosEnvDPEC
        {
            /// <summary>
            /// Ambiente (2-Homologação ou 1-Produção)
            /// </summary>
            public int tpAmb { get; set; }
            /// <summary>
            /// Código da Unidade Federativa (UF)
            /// </summary>
            public int cUF { get; set; }
            /// <summary>
            /// Tipo de Emissao (1-Normal, 2-Contingencia, 3-SCAN, ...
            /// </summary>
            public int tpEmis { get; set; }

            public string CNPJ { get; set; }
            public string IE { get; set; }
            public string verProc { get; set; }
            public string chNFe {get; set;}
            public string CNPJCPF { get; set; }
            public string UF { get; set; }
            public string vNF { get; set; }
            public string vICMS { get; set; }
            public string vST { get; set; }
        }
        /// <summary>
        /// Esta herança que deve ser utilizada fora da classe para obter os valores das tag´s do registro do DPEC
        /// </summary>
        public DadosEnvDPEC dadosEnvDPEC = new DadosEnvDPEC();
        #endregion

        #region Classe com os dados do XML de consulta do registro do DPEC
        /// <summary>
        /// Classe com os dados do XML de registro do DPEC
        /// </summary>
        public class DadosConsDPEC
        {
            /// <summary>
            /// Ambiente (2-Homologação ou 1-Produção)
            /// </summary>
            public int tpAmb { get; set; }
            /// <summary>
            /// Código da Unidade Federativa (UF)
            /// </summary>
            //public int cUF { get; set; }
            /// <summary>
            /// Tipo de Emissao (1-Normal, 2-Contingencia, 3-SCAN, ...
            /// </summary>
            public int tpEmis { get; set; }

            public string chNFe { get; set; }
            public string nRegDPEC { get; set; }
            public string verAplic { get; set; }
        }
        /// <summary>
        /// Esta herança que deve ser utilizada fora da classe para obter os valores das tag´s do registro do DPEC
        /// </summary>
        public DadosConsDPEC dadosConsDPEC = new DadosConsDPEC();
        #endregion

        #endregion

        #region Metodos

        #region ConsCad()
        /// <summary>
        /// Faz a leitura do XML de consulta do cadastro do contribuinte e disponibiliza os valores de algumas tag´s
        /// </summary>
        /// <param name="cArquivoXML">Caminho e nome do arquivo XML da consulta do cadastro do contribuinte a ser lido</param>
        public void ConsCad(string cArquivoXML)
        {
            this.oDadosConsCad.CNPJ = string.Empty;
            this.oDadosConsCad.IE = string.Empty;
            this.oDadosConsCad.UF = string.Empty;

            try
            {
                if (Path.GetExtension(cArquivoXML).ToLower() == ".txt")
                {
                    List<string> cLinhas = new Auxiliar().LerArquivo(cArquivoXML);
                    foreach (string cTexto in cLinhas)
                    {
                        string[] dados = cTexto.Split('|');
                        switch (dados[0].ToLower())
                        {
                            case "cnpj":
                                this.oDadosConsCad.CNPJ = dados[1].Trim();
                                break;
                            case "cpf":
                                this.oDadosConsCad.CPF = dados[1].Trim();
                                break;
                            case "ie":
                                this.oDadosConsCad.IE = dados[1].Trim();
                                break;
                            case "uf":
                                this.oDadosConsCad.UF = dados[1].Trim();
                                break;
                        }
                    }
                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(cArquivoXML);

                    XmlNodeList ConsCadList = doc.GetElementsByTagName("ConsCad");
                    foreach (XmlNode ConsCadNode in ConsCadList)
                    {
                        XmlElement ConsCadElemento = (XmlElement)ConsCadNode;

                        XmlNodeList infConsList = ConsCadElemento.GetElementsByTagName("infCons");

                        foreach (XmlNode infConsNode in infConsList)
                        {
                            XmlElement infConsElemento = (XmlElement)infConsNode;

                            if (infConsElemento.GetElementsByTagName("CNPJ")[0] != null)
                            {
                                this.oDadosConsCad.CNPJ = infConsElemento.GetElementsByTagName("CNPJ")[0].InnerText;
                            }
                            if (infConsElemento.GetElementsByTagName("CPF")[0] != null)
                            {
                                this.oDadosConsCad.CPF = infConsElemento.GetElementsByTagName("CPF")[0].InnerText;
                            }
                            if (infConsElemento.GetElementsByTagName("UF")[0] != null)
                            {
                                this.oDadosConsCad.UF = infConsElemento.GetElementsByTagName("UF")[0].InnerText;
                            }
                            if (infConsElemento.GetElementsByTagName("IE")[0] != null)
                            {
                                this.oDadosConsCad.IE = infConsElemento.GetElementsByTagName("IE")[0].InnerText;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region Nfe()
        /// <summary>
        /// Faz a leitura do XML da nota fiscal eletrônica e disponibiliza os valores
        /// de algumas tag´s
        /// </summary>
        /// <param name="cArquivoXML">Caminho e nome do arquivo XML da NFe a ser lido</param>
        /// <example>
        /// UniLerXmlClass oLerXml = new UniLerXmlClass();
        /// oLerXml.Nfe( cPasta_Nome_ArquivoXML );
        /// DateTime dEmi = oLerXml.Nfe.oDadosNfe.dEmi;
        /// </example>
        /// <remarks>
        /// Gera exception em caso de problemas na leitura
        /// </remarks>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>10/01/2009</date>
        public void Nfe(string cArquivoXML)
        {
            this.oDadosNfe.chavenfe = string.Empty;
            this.oDadosNfe.idLote = string.Empty;
            this.oDadosNfe.tpAmb = string.Empty;
            this.oDadosNfe.tpEmis = string.Empty;
            this.oDadosNfe.serie = string.Empty;
            this.oDadosNfe.cUF = string.Empty;
            this.oDadosNfe.cNF = string.Empty;
            this.oDadosNfe.mod = string.Empty;
            this.oDadosNfe.nNF = string.Empty;
            this.oDadosNfe.cDV = string.Empty;
            this.oDadosNfe.CNPJ = string.Empty;

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(cArquivoXML);

                XmlNodeList infNFeList = null;
                switch (ConfiguracaoApp.TipoAplicativo)
                {
                    case UniNFeLibrary.Enums.TipoAplicativo.Cte:
                        infNFeList = doc.GetElementsByTagName("infCte");
                        break;

                    case UniNFeLibrary.Enums.TipoAplicativo.Nfe:
                        infNFeList = doc.GetElementsByTagName("infNFe");
                        break;

                    default:
                        break;
                }

                foreach (XmlNode infNFeNode in infNFeList)
                {
                    XmlElement infNFeElemento = (XmlElement)infNFeNode;

                    //Pegar a chave da NF-e
                    if (infNFeElemento.HasAttributes)
                    {
                        this.oDadosNfe.chavenfe = infNFeElemento.Attributes["Id"].InnerText;
                    }

                    //Montar lista de tag´s da tag <ide>
                    XmlNodeList ideList = infNFeElemento.GetElementsByTagName("ide");

                    foreach (XmlNode ideNode in ideList)
                    {
                        XmlElement ideElemento = (XmlElement)ideNode;

                        switch (ConfiguracaoApp.TipoAplicativo)
                        {
                            case UniNFeLibrary.Enums.TipoAplicativo.Cte:
                                this.oDadosNfe.dEmi = Convert.ToDateTime(ideElemento.GetElementsByTagName("dhEmi")[0].InnerText);
                                this.oDadosNfe.cNF = ideElemento.GetElementsByTagName("cCT")[0].InnerText;
                                this.oDadosNfe.nNF = ideElemento.GetElementsByTagName("nCT")[0].InnerText;
                                goto default;

                            case UniNFeLibrary.Enums.TipoAplicativo.Nfe:
                                this.oDadosNfe.dEmi = Convert.ToDateTime(ideElemento.GetElementsByTagName("dEmi")[0].InnerText);
                                this.oDadosNfe.cNF = ideElemento.GetElementsByTagName("cNF")[0].InnerText;
                                this.oDadosNfe.nNF = ideElemento.GetElementsByTagName("nNF")[0].InnerText;
                                goto default;

                            default:
                                this.oDadosNfe.tpEmis = ideElemento.GetElementsByTagName("tpEmis")[0].InnerText;
                                this.oDadosNfe.tpAmb = ideElemento.GetElementsByTagName("tpAmb")[0].InnerText;
                                this.oDadosNfe.serie = ideElemento.GetElementsByTagName("serie")[0].InnerText;
                                this.oDadosNfe.cUF = ideElemento.GetElementsByTagName("cUF")[0].InnerText;
                                this.oDadosNfe.mod = ideElemento.GetElementsByTagName("mod")[0].InnerText;
                                this.oDadosNfe.cDV = ideElemento.GetElementsByTagName("cDV")[0].InnerText;
                                break;
                        }
                    }

                    //Montar lista de tag´s da tag <emit>
                    XmlNodeList emitList = infNFeElemento.GetElementsByTagName("emit");

                    foreach (XmlNode emitNode in emitList)
                    {
                        XmlElement emitElemento = (XmlElement)emitNode;

                        this.oDadosNfe.CNPJ = emitElemento.GetElementsByTagName("CNPJ")[0].InnerText;
                    }
                }

                //Tentar detectar a tag de lote, se tiver vai atualizar o atributo do lote que a nota faz parte
                XmlNodeList enviNFeList = null;

                switch (ConfiguracaoApp.TipoAplicativo)
                {
                    case UniNFeLibrary.Enums.TipoAplicativo.Cte:
                        enviNFeList = doc.GetElementsByTagName("enviCTe");
                        break;

                    case UniNFeLibrary.Enums.TipoAplicativo.Nfe:
                        enviNFeList = doc.GetElementsByTagName("enviNFe");
                        break;

                    default:
                        break;
                }

                foreach (XmlNode enviNFeNode in enviNFeList)
                {
                    XmlElement enviNFeElemento = (XmlElement)enviNFeNode;

                    this.oDadosNfe.idLote = enviNFeElemento.GetElementsByTagName("idLote")[0].InnerText;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region PedCanc()
        /// <summary>
        /// PedCan(string cArquivoXML)
        /// </summary>
        /// <param name="cArquivoXML"></param>
        public void PedCanc(string cArquivoXML)
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index; 

            this.oDadosPedCanc.tpAmb = Empresa.Configuracoes[emp].tpAmb;
            this.oDadosPedCanc.tpEmis = Empresa.Configuracoes[emp].tpEmis;

            if (Path.GetExtension(cArquivoXML).ToLower() == ".txt")
            {
                //      tpAmb|2
                //      chNFe|35080699999090910270550000000000011234567890
                //      nProt|135080000000001
                //      xJust|Teste do WS de Cancelamento
                //      tpEmis|1                                    <<< opcional >>>
                List<string> cLinhas = new Auxiliar().LerArquivo(cArquivoXML);
                foreach (string cTexto in cLinhas)
                {
                    string[] dados = cTexto.Split('|');
                    switch (dados[0].ToLower())
                    {
                        case "tpamb":
                            this.oDadosPedCanc.tpAmb = Convert.ToInt32("0" + dados[1].Trim());
                            break;
                        case "chnfe":
                            this.oDadosPedCanc.chNFe = dados[1].Trim();
                            break;
                        case "nprot":
                            this.oDadosPedCanc.nProt = dados[1].Trim();
                            break;
                        case "xjust":
                            this.oDadosPedCanc.xJust = dados[1].Trim();
                            break;
                        case "tpemis":
                            this.oDadosPedCanc.tpEmis = Convert.ToInt32("0" + dados[1].Trim());
                            break;
                    }
                }
            }
            else
            {
                //<?xml version="1.0" encoding="UTF-8"?>
                //<cancNFe xmlns="http://www.portalfiscal.inf.br/nfe" versao="1.07">
                //  <infCanc Id="ID35080699999090910270550000000000011234567890">
                //      <tpAmb>2</tpAmb>
                //      <xServ>CANCELAR</xServ>
                //      <chNFe>35080699999090910270550000000000011234567890</chNFe>
                //      <nProt>135080000000001</nProt>
                //      <xJust>Teste do WS de Cancelamento</xJust>
                //      <tpEmis>1</tpEmis>                                      <<< opcional >>>
                //  </infCanc>}
                //</cancNFe>
                XmlDocument doc = new XmlDocument();
                doc.Load(cArquivoXML);

                XmlNodeList infCancList = doc.GetElementsByTagName("infCanc");

                foreach (XmlNode infCancNode in infCancList)
                {
                    XmlElement infCancElemento = (XmlElement)infCancNode;

                    this.oDadosPedCanc.tpAmb = Convert.ToInt32("0" + infCancElemento.GetElementsByTagName("tpAmb")[0].InnerText);

                    switch (ConfiguracaoApp.TipoAplicativo)
                    {
                        case UniNFeLibrary.Enums.TipoAplicativo.Cte:
                            if (infCancElemento.GetElementsByTagName("chCTe").Count != 0)
                                this.oDadosPedCanc.chNFe = infCancElemento.GetElementsByTagName("chCTe")[0].InnerText;
                            break;

                        case UniNFeLibrary.Enums.TipoAplicativo.Nfe:
                            if (infCancElemento.GetElementsByTagName("chNFe").Count != 0)
                                this.oDadosPedCanc.chNFe = infCancElemento.GetElementsByTagName("chNFe")[0].InnerText;
                            break;

                        default:
                            break;
                    }

                    ///
                    /// danasa 12-9-2009
                    /// 
                    if (infCancElemento.GetElementsByTagName("tpEmis").Count != 0)
                    {
                        this.oDadosPedCanc.tpEmis = Convert.ToInt16(infCancElemento.GetElementsByTagName("tpEmis")[0].InnerText);
                        /// para que o validador não rejeite, excluo a tag <tpEmis>
                        doc.DocumentElement["infCanc"].RemoveChild(infCancElemento.GetElementsByTagName("tpEmis")[0]);

                        /// salvo o arquivo modificado
                        doc.Save(cArquivoXML);
                    }
                }
            }
        }
        #endregion

        #region PedInut()
        /// <summary>
        /// PedInut(string cArquivoXML)
        /// </summary>
        /// <param name="cArquivoXML"></param>
        public void PedInut(string cArquivoXML)
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index; 

            this.oDadosPedInut.tpAmb = Empresa.Configuracoes[emp].tpAmb;
            this.oDadosPedInut.tpEmis = Empresa.Configuracoes[emp].tpEmis;

            try
            {
                if (Path.GetExtension(cArquivoXML).ToLower() == ".txt")
                {
                    //      tpAmb|2
                    //      tpEmis|1                <<< opcional >>>
                    //      cUF|35
                    //      ano|08
                    //      CNPJ|99999090910270
                    //      mod|55
                    //      serie|0
                    //      nNFIni|1
                    //      nNFFin|1
                    //      xJust|Teste do WS de Inutilizacao
                    List<string> cLinhas = new Auxiliar().LerArquivo(cArquivoXML);
                    foreach (string cTexto in cLinhas)
                    {
                        string[] dados = cTexto.Split('|');
                        switch (dados[0].ToLower())
                        {
                            case "tpamb":
                                this.oDadosPedInut.tpAmb = Convert.ToInt32("0" + dados[1].Trim());
                                break;
                            case "tpemis":
                                this.oDadosPedInut.tpEmis = Convert.ToInt32("0" + dados[1].Trim());
                                break;
                            case "cuf":
                                this.oDadosPedInut.cUF = Convert.ToInt32("0" + dados[1].Trim());
                                break;
                            case "ano":
                                this.oDadosPedInut.ano = Convert.ToInt32("0" + dados[1].Trim());
                                break;
                            case "cnpj":
                                this.oDadosPedInut.CNPJ = dados[1].Trim();
                                break;
                            case "mod":
                                this.oDadosPedInut.mod = Convert.ToInt32("0" + dados[1].Trim());
                                break;
                            case "serie":
                                this.oDadosPedInut.serie = Convert.ToInt32("0" + dados[1].Trim());
                                break;
                            case "nnfini":
                                this.oDadosPedInut.nNFIni = Convert.ToInt32("0" + dados[1].Trim());
                                break;
                            case "nnffin":
                                this.oDadosPedInut.nNFFin = Convert.ToInt32("0" + dados[1].Trim());
                                break;
                            case "xjust":
                                this.oDadosPedInut.xJust = dados[1].Trim();
                                break;
                        }
                    }
                }
                else
                {
                    //<?xml version="1.0" encoding="UTF-8"?>
                    //<inutNFe xmlns="http://www.portalfiscal.inf.br/nfe" versao="1.07">
                    //  <infInut Id="ID359999909091027055000000000001000000001">
                    //      <tpAmb>2</tpAmb>
                    //      <tpEmis>1</tpEmis>                  <<< opcional >>>
                    //      <xServ>INUTILIZAR</xServ>
                    //      <cUF>35</cUF>
                    //      <ano>08</ano>
                    //      <CNPJ>99999090910270</CNPJ>
                    //      <mod>55</mod>
                    //      <serie>0</serie>
                    //      <nNFIni>1</nNFIni>
                    //      <nNFFin>1</nNFFin>
                    //      <xJust>Teste do WS de InutilizaÃ§Ã£o</xJust>
                    //  </infInut>
                    //</inutNFe>
                    XmlDocument doc = new XmlDocument();
                    doc.Load(cArquivoXML);

                    XmlNodeList InutNFeList = null;

                    switch (ConfiguracaoApp.TipoAplicativo)
                    {
                        case UniNFeLibrary.Enums.TipoAplicativo.Cte:
                            InutNFeList = doc.GetElementsByTagName("inutCTe");
                            break;

                        case UniNFeLibrary.Enums.TipoAplicativo.Nfe:
                            InutNFeList = doc.GetElementsByTagName("inutNFe");
                            break;

                        default:
                            break;
                    }

                    foreach (XmlNode InutNFeNode in InutNFeList)
                    {
                        XmlElement InutNFeElemento = (XmlElement)InutNFeNode;

                        XmlNodeList infInutList = InutNFeElemento.GetElementsByTagName("infInut");

                        foreach (XmlNode infInutNode in infInutList)
                        {
                            XmlElement infInutElemento = (XmlElement)infInutNode;

                            if (infInutElemento.GetElementsByTagName("tpAmb")[0] != null)
                                this.oDadosPedInut.tpAmb = Convert.ToInt32("0" + infInutElemento.GetElementsByTagName("tpAmb")[0].InnerText);

                            if (infInutElemento.GetElementsByTagName("cUF")[0] != null)
                                this.oDadosPedInut.cUF = Convert.ToInt32("0" + infInutElemento.GetElementsByTagName("cUF")[0].InnerText);

                            if (infInutElemento.GetElementsByTagName("ano")[0] != null)
                                this.oDadosPedInut.ano = Convert.ToInt32("0" + infInutElemento.GetElementsByTagName("ano")[0].InnerText);

                            if (infInutElemento.GetElementsByTagName("CNPJ")[0] != null)
                                this.oDadosPedInut.CNPJ = infInutElemento.GetElementsByTagName("CNPJ")[0].InnerText;

                            if (infInutElemento.GetElementsByTagName("mod")[0] != null)
                                this.oDadosPedInut.mod = Convert.ToInt32("0" + infInutElemento.GetElementsByTagName("mod")[0].InnerText);

                            if (infInutElemento.GetElementsByTagName("serie")[0] != null)
                                this.oDadosPedInut.serie = Convert.ToInt32("0" + infInutElemento.GetElementsByTagName("serie")[0].InnerText);

                            switch (ConfiguracaoApp.TipoAplicativo)
                            {
                                case UniNFeLibrary.Enums.TipoAplicativo.Cte:
                                    if (infInutElemento.GetElementsByTagName("nCTIni")[0] != null)
                                        this.oDadosPedInut.nNFIni = Convert.ToInt32("0" + infInutElemento.GetElementsByTagName("nCTIni")[0].InnerText);

                                    if (infInutElemento.GetElementsByTagName("nCTFin")[0] != null)
                                        this.oDadosPedInut.nNFFin = Convert.ToInt32("0" + infInutElemento.GetElementsByTagName("nCTFin")[0].InnerText);
                                    break;

                                case UniNFeLibrary.Enums.TipoAplicativo.Nfe:
                                    if (infInutElemento.GetElementsByTagName("nNFIni")[0] != null)
                                        this.oDadosPedInut.nNFIni = Convert.ToInt32("0" + infInutElemento.GetElementsByTagName("nNFIni")[0].InnerText);

                                    if (infInutElemento.GetElementsByTagName("nNFFin")[0] != null)
                                        this.oDadosPedInut.nNFFin = Convert.ToInt32("0" + infInutElemento.GetElementsByTagName("nNFFin")[0].InnerText);
                                    break;

                                default:
                                    break;
                            }

                            ///
                            /// danasa 12-9-2009
                            /// 
                            if (infInutElemento.GetElementsByTagName("tpEmis").Count != 0)
                            {
                                this.oDadosPedInut.tpEmis = Convert.ToInt16(infInutElemento.GetElementsByTagName("tpEmis")[0].InnerText);
                                /// para que o validador não rejeite, excluo a tag <tpEmis>
                                doc.DocumentElement["infInut"].RemoveChild(infInutElemento.GetElementsByTagName("tpEmis")[0]);
                                /// salvo o arquivo modificado
                                doc.Save(cArquivoXML);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region PedSit()
        /// <summary>
        /// Faz a leitura do XML de pedido de consulta da situação da NFe
        /// </summary>
        /// <param name="cArquivoXML">Nome do XML a ser lido</param>
        /// <by>Wandrey Mundin Ferreira</by>
        public void PedSit(string cArquivoXML)
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index; 

            this.oDadosPedSit.tpAmb = Empresa.Configuracoes[emp].tpAmb;// string.Empty;
            this.oDadosPedSit.chNFe = string.Empty;

            try
            {
                if (Path.GetExtension(cArquivoXML).ToLower() == ".txt")
                {
                    switch (ConfiguracaoApp.TipoAplicativo)
                    {
                        case UniNFeLibrary.Enums.TipoAplicativo.Cte:
                            break;

                        case UniNFeLibrary.Enums.TipoAplicativo.Nfe:
                            //      tpAmb|2
                            //      tpEmis|1                <<< opcional >>>
                            //      chNFe|35080600000000000000550000000000010000000000
                            List<string> cLinhas = new Auxiliar().LerArquivo(cArquivoXML);
                            foreach (string cTexto in cLinhas)
                            {
                                string[] dados = cTexto.Split('|');
                                switch (dados[0].ToLower())
                                {
                                    case "tpamb":
                                        this.oDadosPedSit.tpAmb = Convert.ToInt32("0" + dados[1].Trim());
                                        break;
                                    case "tpemis":
                                        this.oDadosPedSit.tpEmis = Convert.ToInt32("0" + dados[1].Trim());
                                        break;
                                    case "chnfe":
                                        this.oDadosPedSit.chNFe = dados[1].Trim();
                                        break;
                                }
                            }
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(cArquivoXML);

                    XmlNodeList consSitNFeList = null;

                    switch (ConfiguracaoApp.TipoAplicativo)
                    {
                        case UniNFeLibrary.Enums.TipoAplicativo.Cte:
                            consSitNFeList = doc.GetElementsByTagName("consSitCTe");
                            break;

                        case UniNFeLibrary.Enums.TipoAplicativo.Nfe:
                            consSitNFeList = doc.GetElementsByTagName("consSitNFe");
                            break;

                        default:
                            break;
                    }

                    foreach (XmlNode consSitNFeNode in consSitNFeList)
                    {
                        XmlElement consSitNFeElemento = (XmlElement)consSitNFeNode;

                        this.oDadosPedSit.tpAmb = Convert.ToInt32("0" + consSitNFeElemento.GetElementsByTagName("tpAmb")[0].InnerText);
                        switch (ConfiguracaoApp.TipoAplicativo)
                        {
                            case UniNFeLibrary.Enums.TipoAplicativo.Cte:
                                this.oDadosPedSit.chNFe = consSitNFeElemento.GetElementsByTagName("chCTe")[0].InnerText;
                                break;

                            case UniNFeLibrary.Enums.TipoAplicativo.Nfe:
                                this.oDadosPedSit.chNFe = consSitNFeElemento.GetElementsByTagName("chNFe")[0].InnerText;

                                //Definir a versão do XML para resolver o problema do Estado do Paraná e Goiás que não migrou o banco de dados
                                //da versão 1.0 para a 2.0, sendo assim a consulta situação de notas fiscais tem que ser feita cada uma em seu 
                                //ambiente. Wandrey 23/03/2011
                                if (consSitNFeElemento.GetAttribute("versao") == "1.07" && (this.oDadosPedSit.cUF == 41 || this.oDadosPedSit.cUF == 52))
                                    this.oDadosPedSit.versaoNFe = 1;
                                else
                                    this.oDadosPedSit.versaoNFe = 2;

                                break;

                            default:
                                break;
                        }

                        ///
                        /// danasa 12-9-2009
                        /// 
                        if (consSitNFeElemento.GetElementsByTagName("tpEmis").Count != 0)
                        {
                            this.oDadosPedSit.tpEmis = Convert.ToInt16(consSitNFeElemento.GetElementsByTagName("tpEmis")[0].InnerText);
                            /// para que o validador não rejeite, excluo a tag <tpEmis>
                            doc.DocumentElement.RemoveChild(consSitNFeElemento.GetElementsByTagName("tpEmis")[0]);
                            /// salvo o arquivo modificado
                            doc.Save(cArquivoXML);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region PedSta()
        /// <summary>
        /// Faz a leitura do XML de pedido do status de serviço
        /// </summary>
        /// <param name="cArquivoXml">Nome do XML a ser lido</param>
        /// <by>Wandrey Mundin Ferreira</by>
        public void PedSta(string cArquivoXML)
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index; 

            this.oDadosPedSta.tpAmb = 0;
            this.oDadosPedSta.cUF = Empresa.Configuracoes[emp].UFCod;
            ///
            /// danasa 9-2009
            /// Assume o que está na configuracao
            /// 
            this.oDadosPedSta.tpEmis = Empresa.Configuracoes[emp].tpEmis;

            try
            {
                ///
                /// danasa 12-9-2009
                /// 
                if (Path.GetExtension(cArquivoXML).ToLower() == ".txt")
                {
                    switch (ConfiguracaoApp.TipoAplicativo)
                    {
                        case UniNFeLibrary.Enums.TipoAplicativo.Cte:
                            break;

                        case UniNFeLibrary.Enums.TipoAplicativo.Nfe:
                            // tpEmis|1						<<< opcional >>>
                            // tpAmb|1
                            // cUF|35
                            List<string> cLinhas = new Auxiliar().LerArquivo(cArquivoXML);
                            foreach (string cTexto in cLinhas)
                            {
                                string[] dados = cTexto.Split('|');
                                switch (dados[0].ToLower())
                                {
                                    case "tpamb":
                                        this.oDadosPedSta.tpAmb = Convert.ToInt32("0" + dados[1].Trim());
                                        break;
                                    case "cuf":
                                        this.oDadosPedSta.cUF = Convert.ToInt32("0" + dados[1].Trim());
                                        break;
                                    case "tpemis":
                                        this.oDadosPedSta.tpEmis = Convert.ToInt32("0" + dados[1].Trim());
                                        break;
                                }
                            }
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    //<?xml version="1.0" encoding="UTF-8"?>
                    //<consStatServ xmlns="http://www.portalfiscal.inf.br/nfe" versao="1.07">
                    //  <tpAmb>2</tpAmb>
                    //  <cUF>35</cUF>
                    //  <xServ>STATUS</xServ>
                    //</consStatServ>                    

                    XmlDocument doc = new XmlDocument();
                    doc.Load(cArquivoXML);

                    XmlNodeList consStatServList = null;

                    switch (ConfiguracaoApp.TipoAplicativo)
                    {
                        case UniNFeLibrary.Enums.TipoAplicativo.Cte:
                            consStatServList = doc.GetElementsByTagName("consStatServCte");
                            break;

                        case UniNFeLibrary.Enums.TipoAplicativo.Nfe:
                            consStatServList = doc.GetElementsByTagName("consStatServ");
                            break;

                        default:
                            break;
                    }

                    foreach (XmlNode consStatServNode in consStatServList)
                    {
                        XmlElement consStatServElemento = (XmlElement)consStatServNode;

                        this.oDadosPedSta.tpAmb = Convert.ToInt32("0" + consStatServElemento.GetElementsByTagName("tpAmb")[0].InnerText);

                        if (consStatServElemento.GetElementsByTagName("cUF").Count != 0)
                        {
                            this.oDadosPedSta.cUF = Convert.ToInt32("0" + consStatServElemento.GetElementsByTagName("cUF")[0].InnerText);

                            //Se for o UniCTe tem que remover a tag da UF
                            if (ConfiguracaoApp.TipoAplicativo == UniNFeLibrary.Enums.TipoAplicativo.Cte)
                            {
                                doc.DocumentElement.RemoveChild(consStatServElemento.GetElementsByTagName("cUF")[0]);
                            }
                        }

                        if (consStatServElemento.GetElementsByTagName("tpEmis").Count != 0)
                        {
                            this.oDadosPedSta.tpEmis = Convert.ToInt16(consStatServElemento.GetElementsByTagName("tpEmis")[0].InnerText);
                            /// para que o validador não rejeite, excluo a tag <tpEmis>
                            doc.DocumentElement.RemoveChild(consStatServElemento.GetElementsByTagName("tpEmis")[0]);
                            /// salvo o arquivo modificado
                            doc.Save(cArquivoXML);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region Recibo
        /// <summary>
        /// Faz a leitura do XML do Recibo do lote enviado e disponibiliza os valores
        /// de algumas tag´s
        /// </summary>
        /// <param name="cArquivoXML">Caminho e nome do arquivo XML da NFe a ser lido</param>
        /// <param name="strXml">String contendo o XML</param>
        /// <example>
        /// UniLerXmlClass oLerXml = new UniLerXmlClass();
        /// oLerXml.Recibo( strXml );
        /// String nRec = oLerXml.oDadosRec.nRec;
        /// </example>
        /// <remarks>
        /// Gera exception em caso de problemas na leitura
        /// </remarks>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>20/04/2009</date>
        public void Recibo(string strXml)
        {
            MemoryStream memoryStream = Auxiliar.StringXmlToStream(strXml);

            this.oDadosRec.cStat = string.Empty;
            this.oDadosRec.nRec = string.Empty;
            this.oDadosRec.tMed = 0;

            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(memoryStream);

                XmlNodeList retEnviNFeList = null;

                switch (ConfiguracaoApp.TipoAplicativo)
                {
                    case UniNFeLibrary.Enums.TipoAplicativo.Cte:
                        retEnviNFeList = xml.GetElementsByTagName("retEnviCte");
                        break;

                    case UniNFeLibrary.Enums.TipoAplicativo.Nfe:
                        retEnviNFeList = xml.GetElementsByTagName("retEnviNFe");
                        break;

                    default:
                        break;
                }


                foreach (XmlNode retEnviNFeNode in retEnviNFeList)
                {
                    XmlElement retEnviNFeElemento = (XmlElement)retEnviNFeNode;

                    this.oDadosRec.cStat = retEnviNFeElemento.GetElementsByTagName("cStat")[0].InnerText;

                    XmlNodeList infRecList = xml.GetElementsByTagName("infRec");

                    foreach (XmlNode infRecNode in infRecList)
                    {
                        XmlElement infRecElemento = (XmlElement)infRecNode;

                        this.oDadosRec.nRec = infRecElemento.GetElementsByTagName("nRec")[0].InnerText;
                        this.oDadosRec.tMed = Convert.ToInt32(infRecElemento.GetElementsByTagName("tMed")[0].InnerText);
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region PedRec()
        /// <summary>
        /// Faz a leitura do XML de pedido da consulta do recibo do lote de notas enviadas
        /// </summary>
        /// <param name="cArquivoXml">Nome do XML a ser lido</param>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 16/03/2010
        /// </remarks>
        public void PedRec(string cArquivoXML)
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index; 

            this.oDadosPedRec.tpAmb = 0;
            this.oDadosPedRec.tpEmis = Empresa.Configuracoes[emp].tpEmis;
            this.oDadosPedRec.cUF = Empresa.Configuracoes[emp].UFCod;
            this.oDadosPedRec.nRec = string.Empty;

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(cArquivoXML);

                XmlNodeList consReciNFeList = null;

                switch (ConfiguracaoApp.TipoAplicativo)
                {
                    case UniNFeLibrary.Enums.TipoAplicativo.Cte:
                        consReciNFeList = doc.GetElementsByTagName("consReciCTe");
                        break;

                    case UniNFeLibrary.Enums.TipoAplicativo.Nfe:
                        consReciNFeList = doc.GetElementsByTagName("consReciNFe");
                        break;

                    default:
                        break;
                }

                foreach (XmlNode consReciNFeNode in consReciNFeList)
                {
                    XmlElement consReciNFeElemento = (XmlElement)consReciNFeNode;

                    this.oDadosPedRec.tpAmb = Convert.ToInt32("0" + consReciNFeElemento.GetElementsByTagName("tpAmb")[0].InnerText);
                    this.oDadosPedRec.nRec = consReciNFeElemento.GetElementsByTagName("nRec")[0].InnerText;

                    if (consReciNFeElemento.GetElementsByTagName("cUF").Count != 0)
                    {
                        this.oDadosPedRec.cUF = Convert.ToInt32("0" + consReciNFeElemento.GetElementsByTagName("cUF")[0].InnerText);
                        /// Para que o validador não rejeite, excluo a tag <cUF>
                        doc.DocumentElement.RemoveChild(consReciNFeElemento.GetElementsByTagName("cUF")[0]);
                        /// Salvo o arquivo modificado
                        doc.Save(cArquivoXML);
                    }
                    if (consReciNFeElemento.GetElementsByTagName("tpEmis").Count != 0)
                    {
                        this.oDadosPedRec.tpEmis = Convert.ToInt16(consReciNFeElemento.GetElementsByTagName("tpEmis")[0].InnerText);
                        /// Para que o validador não rejeite, excluo a tag <tpEmis>
                        doc.DocumentElement.RemoveChild(consReciNFeElemento.GetElementsByTagName("tpEmis")[0]);
                        /// Salvo o arquivo modificado
                        doc.Save(cArquivoXML);
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region EnvDPEC()
        /// <summary>
        /// Efetua a leitura do XML de registro do DPEC
        /// </summary>
        /// <param name="arquivoXML">Arquivo XML de registro do DPEC</param>
        public void EnvDPEC(int emp, string arquivoXML)
        {
            //int emp = Empresa.FindEmpresaThread(Thread.CurrentThread);

            this.dadosEnvDPEC.tpAmb = Empresa.Configuracoes[emp].tpAmb;
            this.dadosEnvDPEC.tpEmis = TipoEmissao.teDPEC;
            this.dadosEnvDPEC.cUF = Empresa.Configuracoes[emp].UFCod;

            ///
            /// danasa 21/10/2010
            /// 
            if (Path.GetExtension(arquivoXML).ToLower() == ".txt")
            {
                switch (ConfiguracaoApp.TipoAplicativo)
                {
                    case UniNFeLibrary.Enums.TipoAplicativo.Cte:
                        break;

                    case UniNFeLibrary.Enums.TipoAplicativo.Nfe:
                        ///cUF|31                   |
                        ///tpAmb|2                  | opcional
                        ///verProc|1.0.0
                        ///CNPJ|10238568000360
                        ///IE|148230665114
                        ///------
                        ///chNFe|31101010238568000360550010000001011000001011
                        ///CNPJCPF|05481336000137   | se UF=EX->Branco
                        ///UF|SP
                        ///vNF|123456.00
                        ///vICMS|18.00
                        ///vST|121.99
                        List<string> cLinhas = new Auxiliar().LerArquivo(arquivoXML);
                        foreach (string cTexto in cLinhas)
                        {
                            string[] dados = cTexto.Split('|');
                            if (dados.GetLength(0) == 1) continue;

                            switch (dados[0].ToLower())
                            {
                                case "tpamb":
                                    this.dadosEnvDPEC.tpAmb = Convert.ToInt32("0" + dados[1].Trim());
                                    break;
                                case "cuf":
                                    this.dadosEnvDPEC.cUF = Convert.ToInt32("0" + dados[1].Trim());
                                    break;
                                case "verproc":
                                    this.dadosEnvDPEC.verProc = dados[1].Trim();
                                    break;
                                case "cnpj":
                                    this.dadosEnvDPEC.CNPJ = (string)Auxiliar.OnlyNumbers(dados[1].Trim());
                                    break;
                                case "ie":
                                    this.dadosEnvDPEC.IE = (string)Auxiliar.OnlyNumbers(dados[1].Trim());
                                    break;
                                case "chnfe":
                                    this.dadosEnvDPEC.chNFe = dados[1].Trim();
                                    break;
                                case "cnpjcpf":
                                    this.dadosEnvDPEC.CNPJCPF = (string)Auxiliar.OnlyNumbers(dados[1].Trim());
                                    break;
                                case "uf":
                                    this.dadosEnvDPEC.UF =  dados[1].Trim();
                                    break;
                                case "vicms":
                                    this.dadosEnvDPEC.vICMS = dados[1].Trim();
                                    break;
                                case "vst":
                                    this.dadosEnvDPEC.vST = dados[1].Trim();
                                    break;
                                case "vnf":
                                    this.dadosEnvDPEC.vNF = dados[1].Trim();
                                    break;
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(arquivoXML);

                XmlNodeList infDPECList = doc.GetElementsByTagName("infDPEC");

                foreach (XmlNode infDPECNode in infDPECList)
                {
                    XmlElement infDPECElemento = (XmlElement)infDPECNode;

                    this.dadosEnvDPEC.tpAmb = Convert.ToInt32("0" + infDPECElemento.GetElementsByTagName("tpAmb")[0].InnerText);
                    this.dadosEnvDPEC.cUF = Convert.ToInt32("0" + infDPECElemento.GetElementsByTagName("cUF")[0].InnerText);
                }
            }
        }
        #endregion

        #region ConsDPEC()
        public void ConsDPEC(int emp, string arquivoXML)
        {
            this.dadosConsDPEC.tpAmb = Empresa.Configuracoes[emp].tpAmb;
            this.dadosConsDPEC.tpEmis = TipoEmissao.teDPEC;

            ///
            /// danasa 21/10/2010
            /// 
            if (Path.GetExtension(arquivoXML).ToLower() == ".txt")
            {
                switch (ConfiguracaoApp.TipoAplicativo)
                {
                    case UniNFeLibrary.Enums.TipoAplicativo.Cte:
                        break;

                    case UniNFeLibrary.Enums.TipoAplicativo.Nfe:
                        ///cUF|31                   |
                        ///tpAmb|2                  | opcional
                        ///verProc|1.0.0
                        ///CNPJ|10238568000360
                        ///IE|148230665114
                        ///------
                        ///chNFe|31101010238568000360550010000001011000001011
                        ///CNPJCPF|05481336000137   | se UF=EX->Branco
                        ///UF|SP
                        ///vNF|123456.00
                        ///vICMS|18.00
                        ///vST|121.99
                        List<string> cLinhas = new Auxiliar().LerArquivo(arquivoXML);
                        foreach (string cTexto in cLinhas)
                        {
                            string[] dados = cTexto.Split('|');
                            if (dados.GetLength(0) == 1) continue;

                            switch (dados[0].ToLower())
                            {
                                case "tpamb":
                                    this.dadosConsDPEC.tpAmb = Convert.ToInt32("0" + dados[1].Trim());
                                    break;
                                case "veraplic":
                                    this.dadosConsDPEC.verAplic = dados[1].Trim();
                                    break;
                                case "chnfe":
                                    this.dadosConsDPEC.chNFe = dados[1].Trim();
                                    break;
                                case "nregdpec":
                                    this.dadosConsDPEC.nRegDPEC = dados[1].Trim();
                                    break;
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(arquivoXML);

                XmlNodeList consDPECList = doc.GetElementsByTagName("consDPEC");

                foreach (XmlNode consDPECNode in consDPECList)
                {
                    XmlElement consDPECElemento = (XmlElement)consDPECNode;

                    this.dadosConsDPEC.tpAmb = Convert.ToInt32("0" + consDPECElemento.GetElementsByTagName("tpAmb")[0].InnerText);
                }
            }
        }
        #endregion

        #endregion
    }
}