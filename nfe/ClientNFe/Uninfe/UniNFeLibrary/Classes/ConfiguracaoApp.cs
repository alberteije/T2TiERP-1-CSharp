using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using UniNFeLibrary.Enums;
using System.Threading;

namespace UniNFeLibrary
{
    #region Classe ConfiguracaoApp
    /// <summary>
    /// Classe responsável por realizar algumas tarefas na parte de configurações da aplicação.
    /// Arquivo de configurações: UniNfeConfig.xml
    /// </summary>
    public class ConfiguracaoApp
    {
        #region Propriedades

        #region Propriedades diversas
        /// <summary>
        /// Namespace URI associado (Endereço http dos schemas de XML)
        /// </summary>
        public static string nsURI { get; set; }
        public static Enums.TipoAplicativo TipoAplicativo { get; set; }
        #endregion

        #region Propriedades das versões dos XML´s da NFe
        public static string VersaoXMLStatusServico { get; set; }
        public static string VersaoXMLNFe { get; set; }
        public static string VersaoXMLPedRec { get; set; }
        public static string VersaoXMLCanc { get; set; }
        public static string VersaoXMLInut { get; set; }
        public static string VersaoXMLPedSit { get; set; }
        public static string VersaoXMLConsCad { get; set; }
        public static string VersaoXMLCabecMsg { get; set; }
        public static string VersaoXMLEnvDPEC { get; set; }
        public static string VersaoXMLConsDPEC { get; set; }
        #endregion

        #region Propriedades para controle de servidor proxy
        public static bool Proxy { get; set; }
        public static string ProxyServidor { get; set; }
        public static string ProxyUsuario { get; set; }
        public static string ProxySenha { get; set; }
        public static int ProxyPorta { get; set; }
        #endregion

        #endregion

        #region Métodos gerais

        #region CarregarDados()
        /// <summary>
        /// Carrega as configurações realizadas na Aplicação gravadas no XML UniNfeConfig.xml para
        /// propriedades, para facilitar a leitura das informações necessárias para as transações da NF-e.
        /// </summary>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// </remarks>
        public static void CarregarDados()
        {
            string vArquivoConfig = InfoApp.PastaExecutavel() + "\\" + InfoApp.NomeArqConfig;

            if (File.Exists(vArquivoConfig))
            {
                XmlTextReader oLerXml = null;
                try
                {
                    //Carregar os dados do arquivo XML de configurações da Aplicação
                    oLerXml = new XmlTextReader(vArquivoConfig);

                    while (oLerXml.Read())
                    {
                        if (oLerXml.NodeType == XmlNodeType.Element)
                        {
                            if (oLerXml.Name == "nfe_configuracoes")
                            {
                                while (oLerXml.Read())
                                {
                                    if (oLerXml.NodeType == XmlNodeType.Element)
                                    {
                                        //
                                        // carrega os dados para o proxy - danasa 10-2009
                                        // 
                                        if (oLerXml.Name == "Proxy") { oLerXml.Read(); ConfiguracaoApp.Proxy = Convert.ToBoolean(oLerXml.Value); }
                                        else if (oLerXml.Name == "ProxyServidor") { oLerXml.Read(); ConfiguracaoApp.ProxyServidor = oLerXml.Value.Trim(); }
                                        else if (oLerXml.Name == "ProxyUsuario") { oLerXml.Read(); ConfiguracaoApp.ProxyUsuario = oLerXml.Value.Trim(); }
                                        else if (oLerXml.Name == "ProxySenha") { oLerXml.Read(); ConfiguracaoApp.ProxySenha = oLerXml.Value.Trim(); }
                                        else if (oLerXml.Name == "ProxyPorta") { oLerXml.Read(); ConfiguracaoApp.ProxyPorta = Convert.ToInt32(oLerXml.Value.Trim()); }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ///
                    /// danasa 8-2009
                    /// como reportar ao usuario que houve erro de leitura do arquivo de configuracao?
                    /// tem um usuário que postou um erro de leitura deste arquivo e não sabia como resolver.
                    /// 
                    ///
                    /// danasa 8-2009
                    /// 
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (oLerXml != null)
                        oLerXml.Close();
                }

            }
        }
        #endregion

        ///  
        /// danasa 21/10/2010
        /// lista utilizada para armazenar os webservices
        private static List<webServices> webservicesList = null;

        /// <summary>
        /// Definir o webservice que será utilizado para o envio do XML
        /// </summary>
        /// <param name="servico">Serviço que será executado</param>
        /// <param name="emp">Index da empresa que será executado o serviço</param>
        /// <param name="cUF">Código da UF</param>
        /// <param name="tpAmb">Código do ambiente que será acessado</param>
        /// <param name="tpEmis">Tipo de emissão do XML</param>
        /// <param name="versaoNFe">Versão da NFe (1 ou 2)</param>
        /// <returns>Retorna o objeto do serviço</returns>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 04/04/2011
        /// </remarks>
        public static WebServiceProxy DefinirWS(Servicos servico, int emp, int cUF, int tpAmb, int tpEmis, int versaoNFe)
        {
            WebServiceProxy wsProxy = null;
            string key = servico + " " + cUF + " " + tpAmb + " " + tpEmis + " " + versaoNFe;
            try
            {
                wsProxy = Empresa.Configuracoes[emp].WSProxy[key];
            }
            catch
            {
                //Definir a URI para conexão com o Webservice
                string Url = ConfiguracaoApp.DefLocalWSDL(cUF, tpAmb, tpEmis, servico, versaoNFe);

                wsProxy = new WebServiceProxy(Url, Empresa.Configuracoes[emp].X509Certificado);
                Empresa.Configuracoes[emp].WSProxy.Add(key, wsProxy);
            }

            return wsProxy;
        }


        /// <summary>
        /// Definir o webservice que será utilizado para o envio do XML
        /// </summary>
        /// <param name="servico">Serviço que será executado</param>
        /// <param name="emp">Index da empresa que será executado o serviço</param>
        /// <param name="cUF">Código da UF</param>
        /// <param name="tpAmb">Código do ambiente que será acessado</param>
        /// <param name="tpEmis">Tipo de emissão do XML</param>
        /// <returns>Retorna o objeto do serviço</returns>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 04/04/2011
        /// </remarks>
        public static WebServiceProxy DefinirWS(Servicos servico, int emp, int cUF, int tpAmb, int tpEmis)
        {
            return DefinirWS(servico, emp, cUF, tpAmb, tpEmis, 2);
        }

        /// <summary>
        /// Definir o local do WSDL do webservice
        /// </summary>
        /// <param name="CodigoUF">Código da UF que é para pesquisar a URL do WSDL</param>
        /// <param name="tipoAmbiente">Tipo de ambiente da NFe</param>
        /// <param name="tipoEmissao">Tipo de Emissao da NFe</param>
        /// <param name="servico">Serviço da NFe que está sendo executado</param>
        /// <returns>Retorna a URL</returns>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 22/03/2011
        /// </remarks>
        private static string DefLocalWSDL(int CodigoUF, int tipoAmbiente, int tipoEmissao, Servicos servico)
        {
            return DefLocalWSDL(CodigoUF, tipoAmbiente, tipoEmissao, servico, 2);
        }

        #region DefLocalWSDL
        /// <summary>
        /// Definir o local do WSDL do webservice
        /// </summary>
        /// <param name="CodigoUF">Código da UF que é para pesquisar a URL do WSDL</param>
        /// <param name="tipoAmbiente">Tipo de ambiente da NFe</param>
        /// <param name="tipoEmissao">Tipo de Emissao da NFe</param>
        /// <param name="servico">Serviço da NFe que está sendo executado</param>
        /// <param name="versaoNFe">Versão da NFe</param>
        /// <returns>Retorna a URL</returns>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 08/02/2010
        /// </remarks>
        private static string DefLocalWSDL(int CodigoUF, int tipoAmbiente, int tipoEmissao, Servicos servico, int versaoNFe)
        {
            string WSDL = string.Empty;
            switch (tipoEmissao)
            {
                case TipoEmissao.teSCAN:
                    CodigoUF = 900;
                    break;

                case TipoEmissao.teDPEC:
                    if (servico == Servicos.ConsultarDPEC || servico == Servicos.EnviarDPEC)//danasa 21/10/2010
                        CodigoUF = 901;
                    break;

                default:
                    break;
            }
            string ufNome = CodigoUF.ToString();  //danasa 20-9-2010

            CarregaWebServicesList();

            #region --varre a lista de webservices baseado no codigo da UF

            foreach (webServices list in webservicesList)
            {
                if (list.ID == CodigoUF)
                {
                    switch (servico)
                    {
                        case Servicos.CancelarNFe:
                            WSDL = (tipoAmbiente == TipoAmbiente.taHomologacao ? list.LocalHomologacao.NFeCancelamento : list.LocalProducao.NFeCancelamento);
                            break;

                        case Servicos.ConsultaCadastroContribuinte:
                            WSDL = (tipoAmbiente == TipoAmbiente.taHomologacao ? list.LocalHomologacao.NFeConsultaCadastro : list.LocalProducao.NFeConsultaCadastro);
                            break;

                        case Servicos.EnviarLoteNfe:
                            WSDL = (tipoAmbiente == TipoAmbiente.taHomologacao ? list.LocalHomologacao.NFeRecepcao : list.LocalProducao.NFeRecepcao);
                            break;

                        case Servicos.EnviarDPEC:
                            WSDL = (tipoAmbiente == TipoAmbiente.taHomologacao ? list.LocalHomologacao.NFeRecepcao : list.LocalProducao.NFeRecepcao);
                            break;

                        case Servicos.InutilizarNumerosNFe:
                            WSDL = (tipoAmbiente == TipoAmbiente.taHomologacao ? list.LocalHomologacao.NFeInutilizacao : list.LocalProducao.NFeInutilizacao);
                            break;

                        case Servicos.PedidoConsultaSituacaoNFe:
                            if (versaoNFe.Equals(1))
                                WSDL = (tipoAmbiente == TipoAmbiente.taHomologacao ? list.LocalHomologacao.NFeConsulta1 : list.LocalProducao.NFeConsulta1);
                            else
                                WSDL = (tipoAmbiente == TipoAmbiente.taHomologacao ? list.LocalHomologacao.NFeConsulta : list.LocalProducao.NFeConsulta);
                            break;

                        case Servicos.ConsultarDPEC:
                            WSDL = (tipoAmbiente == TipoAmbiente.taHomologacao ? list.LocalHomologacao.NFeConsulta : list.LocalProducao.NFeConsulta);
                            break;

                        case Servicos.PedidoConsultaStatusServicoNFe:
                            WSDL = (tipoAmbiente == TipoAmbiente.taHomologacao ? list.LocalHomologacao.NFeStatusServico : list.LocalProducao.NFeStatusServico);
                            break;

                        case Servicos.PedidoSituacaoLoteNFe:
                            WSDL = (tipoAmbiente == TipoAmbiente.taHomologacao ? list.LocalHomologacao.NFeRetRecepcao : list.LocalProducao.NFeRetRecepcao);
                            break;
                    }
                    //if (URL != string.Empty)  //danasa 02/12/2010
                    {
                        if (tipoEmissao == TipoEmissao.teDPEC)  //danasa 21/10/2010
                            ufNome = "DPEC";
                        else
                            ufNome = "de " + list.Nome;  //danasa 20-9-2010

                        break;
                    }
                }
            }
            #endregion

            if (WSDL == string.Empty || !File.Exists(WSDL))
            {
                string Ambiente = string.Empty;
                switch (tipoAmbiente)
                {
                    case TipoAmbiente.taProducao:
                        Ambiente = "produção";
                        break;

                    case TipoAmbiente.taHomologacao:
                        Ambiente = "homologação";
                        break;

                    default:
                        break;
                }

                throw new Exception("O Estado " + ufNome + " ainda não dispõe deste serviço no layout 4.0.1 da NF-e para o ambiente de " + Ambiente + ".");
            }

            return WSDL;
        }
        #endregion

        #region CarregaWebServicesList()
        /// <summary>
        /// Carrega a lista de webservices definidos no arquivo WebService.XML
        /// </summary>
        private static void CarregaWebServicesList()
        {
            string ArqXML = InfoApp.PastaExecutavel() + "\\Webservice.xml";

            if (webservicesList == null)
            {
                webservicesList = new List<webServices>();
                if (File.Exists(ArqXML))
                {
                    XmlDocument doc = new XmlDocument();
                    try
                    {
                        doc.Load(ArqXML);
                        XmlNodeList estadoList = doc.GetElementsByTagName("Estado");
                        foreach (XmlNode estadoNode in estadoList)
                        {
                            XmlElement estadoElemento = (XmlElement)estadoNode;
                            if (estadoElemento.Attributes.Count > 0)
                            {
                                if (estadoElemento.Attributes[0].Value != "XX")
                                {
                                    int ID = Convert.ToInt32(estadoElemento.Attributes[0].Value);
                                    string Nome = estadoElemento.Attributes[1].Value;
                                    string UF = estadoElemento.Attributes[2].Value;

                                    webServices wsItem = new webServices(ID, Nome, UF);

                                    #region URL´s de Homologação
                                    XmlNodeList urlList = estadoElemento.GetElementsByTagName("URLHomologacao");
                                    for (int i = 0; i < urlList.Count; ++i)
                                    {
                                        for (int j = 0; j < urlList[i].ChildNodes.Count; ++j)
                                        {
                                            switch (urlList[i].ChildNodes[j].Name)
                                            {
                                                case "NFeCancelamento":
                                                    wsItem.URLHomologacao.NFeCancelamento = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeConsulta":
                                                    wsItem.URLHomologacao.NFeConsulta = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeConsultaCadastro":
                                                    wsItem.URLHomologacao.NFeConsultaCadastro = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeInutilizacao":
                                                    wsItem.URLHomologacao.NFeInutilizacao = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeRecepcao":
                                                    wsItem.URLHomologacao.NFeRecepcao = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeRetRecepcao":
                                                    wsItem.URLHomologacao.NFeRetRecepcao = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeStatusServico":
                                                    wsItem.URLHomologacao.NFeStatusServico = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeConsulta1":
                                                    wsItem.URLHomologacao.NFeConsulta1 = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                            }
                                        }
                                    }
                                    #endregion

                                    #region URL´s de produção
                                    urlList = estadoElemento.GetElementsByTagName("URLProducao");
                                    for (int i = 0; i < urlList.Count; ++i)
                                    {
                                        for (int j = 0; j < urlList[i].ChildNodes.Count; ++j)
                                        {
                                            switch (urlList[i].ChildNodes[j].Name)
                                            {
                                                case "NFeCancelamento":
                                                    wsItem.URLProducao.NFeCancelamento = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeConsulta":
                                                    wsItem.URLProducao.NFeConsulta = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeConsultaCadastro":
                                                    wsItem.URLProducao.NFeConsultaCadastro = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeInutilizacao":
                                                    wsItem.URLProducao.NFeInutilizacao = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeRecepcao":
                                                    wsItem.URLProducao.NFeRecepcao = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeRetRecepcao":
                                                    wsItem.URLProducao.NFeRetRecepcao = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeStatusServico":
                                                    wsItem.URLProducao.NFeStatusServico = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeConsulta1":
                                                    wsItem.URLProducao.NFeConsulta1 = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                            }
                                        }
                                    }
                                    #endregion

                                    #region WSDL´s locais de Homologação
                                    urlList = estadoElemento.GetElementsByTagName("LocalHomologacao");
                                    for (int i = 0; i < urlList.Count; ++i)
                                    {
                                        for (int j = 0; j < urlList[i].ChildNodes.Count; ++j)
                                        {
                                            switch (urlList[i].ChildNodes[j].Name)
                                            {
                                                case "NFeCancelamento":
                                                    wsItem.LocalHomologacao.NFeCancelamento = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeConsulta":
                                                    wsItem.LocalHomologacao.NFeConsulta = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeConsultaCadastro":
                                                    wsItem.LocalHomologacao.NFeConsultaCadastro = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeInutilizacao":
                                                    wsItem.LocalHomologacao.NFeInutilizacao = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeRecepcao":
                                                    wsItem.LocalHomologacao.NFeRecepcao = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeRetRecepcao":
                                                    wsItem.LocalHomologacao.NFeRetRecepcao = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeStatusServico":
                                                    wsItem.LocalHomologacao.NFeStatusServico = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeConsulta1":
                                                    wsItem.LocalHomologacao.NFeConsulta1 = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                            }
                                        }
                                    }
                                    #endregion

                                    #region WSDL´s locais de Produção
                                    urlList = estadoElemento.GetElementsByTagName("LocalProducao");
                                    for (int i = 0; i < urlList.Count; ++i)
                                    {
                                        for (int j = 0; j < urlList[i].ChildNodes.Count; ++j)
                                        {
                                            switch (urlList[i].ChildNodes[j].Name)
                                            {
                                                case "NFeCancelamento":
                                                    wsItem.LocalProducao.NFeCancelamento = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeConsulta":
                                                    wsItem.LocalProducao.NFeConsulta = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeConsultaCadastro":
                                                    wsItem.LocalProducao.NFeConsultaCadastro = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeInutilizacao":
                                                    wsItem.LocalProducao.NFeInutilizacao = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeRecepcao":
                                                    wsItem.LocalProducao.NFeRecepcao = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeRetRecepcao":
                                                    wsItem.LocalProducao.NFeRetRecepcao = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeStatusServico":
                                                    wsItem.LocalProducao.NFeStatusServico = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                                case "NFeConsulta1":
                                                    wsItem.LocalProducao.NFeConsulta1 = InfoApp.PastaExecutavel() + "\\" + urlList[i].ChildNodes[j].InnerText;
                                                    break;
                                            }
                                        }
                                    }
                                    #endregion

                                    webservicesList.Add(wsItem);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw (ex);
                    }
                }
            }
        }
        #endregion


        #region GravarConfig()
        /// <summary>
        /// Método responsável por gravar as configurações da Aplicação no arquivo "UniNfeConfig.xml"
        /// </summary>
        /// <returns>Retorna true se conseguiu gravar corretamente as configurações ou false se não conseguiu</returns>
        public void GravarConfig()
        {

            try
            {
                ValidarConfig();
                GravarConfigGeral();
                GravarConfigEmpresa();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region GravarConfigEmpresa()
        /// <summary>
        /// Gravar as configurações das empresas
        /// </summary>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 30/07/2010
        /// </remarks>
        private void GravarConfigEmpresa()
        {
            XmlWriterSettings oSettings = new XmlWriterSettings();
            UTF8Encoding c = new UTF8Encoding(false);

            //Para começar, vamos criar um XmlWriterSettings para configurar nosso XML
            oSettings.Encoding = c;
            oSettings.Indent = true;
            oSettings.IndentChars = "";
            oSettings.NewLineOnAttributes = false;
            oSettings.OmitXmlDeclaration = false;

            foreach (Empresa empresa in Empresa.Configuracoes)
            {
                XmlWriter oXmlGravar = null;

                try
                {
                    //Agora vamos criar um XML Writer
                    oXmlGravar = XmlWriter.Create(InfoApp.PastaExecutavel() + "\\" + empresa.CNPJ.Trim() + "\\" + InfoApp.NomeArqConfig, oSettings);

                    //Agora vamos gravar os dados
                    oXmlGravar.WriteStartDocument();
                    oXmlGravar.WriteStartElement("nfe_configuracoes");
                    oXmlGravar.WriteElementString("PastaXmlEnvio", empresa.PastaEnvio);
                    oXmlGravar.WriteElementString("PastaXmlRetorno", empresa.PastaRetorno);
                    oXmlGravar.WriteElementString("PastaXmlEnviado", empresa.PastaEnviado);
                    oXmlGravar.WriteElementString("PastaXmlErro", empresa.PastaErro);
                    oXmlGravar.WriteElementString("UnidadeFederativaCodigo", empresa.UFCod.ToString());
                    oXmlGravar.WriteElementString("AmbienteCodigo", empresa.tpAmb.ToString());
                    oXmlGravar.WriteElementString("CertificadoDigital", empresa.Certificado);
                    oXmlGravar.WriteElementString("tpEmis", empresa.tpEmis.ToString());
                    oXmlGravar.WriteElementString("PastaBackup", empresa.PastaBackup);
                    oXmlGravar.WriteElementString("PastaXmlEmLote", empresa.PastaEnvioEmLote);
                    oXmlGravar.WriteElementString("PastaValidar", empresa.PastaValidar);
                    oXmlGravar.WriteElementString("GravarRetornoTXTNFe", empresa.GravarRetornoTXTNFe.ToString());
                    oXmlGravar.WriteElementString("DiretorioSalvarComo", empresa.DiretorioSalvarComo.ToString());
                    oXmlGravar.WriteElementString("DiasLimpeza", empresa.DiasLimpeza.ToString());
                    oXmlGravar.WriteElementString("PastaExeUniDanfe", empresa.PastaExeUniDanfe.ToString());
                    oXmlGravar.WriteElementString("PastaConfigUniDanfe", empresa.PastaConfigUniDanfe.ToString());
                    oXmlGravar.WriteElementString("PastaDanfeMon", empresa.PastaDanfeMon.ToString());
                    oXmlGravar.WriteElementString("XMLDanfeMonNFe", empresa.XMLDanfeMonNFe.ToString());
                    oXmlGravar.WriteElementString("XMLDanfeMonProcNFe", empresa.XMLDanfeMonProcNFe.ToString());
                    oXmlGravar.WriteEndElement(); //nfe_configuracoes
                    oXmlGravar.WriteEndDocument();
                    oXmlGravar.Flush();
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {
                    if (oXmlGravar != null)
                        oXmlGravar.Close();
                }
            }
        }
        #endregion

        #region GravarConfigGeral()
        /// <summary>
        /// Gravar as configurações gerais
        /// </summary>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 30/07/2010
        /// </remarks>
        private void GravarConfigGeral()
        {
            XmlWriterSettings oSettings = new XmlWriterSettings();
            UTF8Encoding c = new UTF8Encoding(false);

            //Para começar, vamos criar um XmlWriterSettings para configurar nosso XML
            oSettings.Encoding = c;
            oSettings.Indent = true;
            oSettings.IndentChars = "";
            oSettings.NewLineOnAttributes = false;
            oSettings.OmitXmlDeclaration = false;

            XmlWriter oXmlGravar = null;

            try
            {
                //Agora vamos criar um XML Writer
                oXmlGravar = XmlWriter.Create(InfoApp.PastaExecutavel() + "\\" + InfoApp.NomeArqConfig, oSettings);

                //Agora vamos gravar os dados
                oXmlGravar.WriteStartDocument();
                oXmlGravar.WriteStartElement("nfe_configuracoes");
                oXmlGravar.WriteElementString("Proxy", ConfiguracaoApp.Proxy.ToString());
                oXmlGravar.WriteElementString("ProxyServidor", ConfiguracaoApp.ProxyServidor);
                oXmlGravar.WriteElementString("ProxyUsuario", ConfiguracaoApp.ProxyUsuario);
                oXmlGravar.WriteElementString("ProxySenha", ConfiguracaoApp.ProxySenha);
                oXmlGravar.WriteElementString("ProxyPorta", ConfiguracaoApp.ProxyPorta.ToString());
                oXmlGravar.WriteEndElement(); //nfe_configuracoes
                oXmlGravar.WriteEndDocument();
                oXmlGravar.Flush();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (oXmlGravar != null)
                    oXmlGravar.Close();
            }
        }
        #endregion

        #region ValidarConfig()
        /// <summary>
        /// Verifica se algumas das informações das configurações tem algum problema ou falha
        /// </summary>
        /// <returns>
        /// true - nenhum problema/falha
        /// false - encontrou algum problema
        /// </returns>
        private void ValidarConfig()
        {
            try
            {
                string erro = string.Empty;
                bool validou = true;

                #region Remover End Slash
                for (int i = 0; i < Empresa.Configuracoes.Count; i++)
                {
                    Empresa.Configuracoes[i].PastaEnvio = ConfiguracaoApp.RemoveEndSlash(Empresa.Configuracoes[i].PastaEnvio);
                    Empresa.Configuracoes[i].PastaEnviado = ConfiguracaoApp.RemoveEndSlash(Empresa.Configuracoes[i].PastaEnviado);
                    Empresa.Configuracoes[i].PastaErro = ConfiguracaoApp.RemoveEndSlash(Empresa.Configuracoes[i].PastaErro);
                    Empresa.Configuracoes[i].PastaRetorno = ConfiguracaoApp.RemoveEndSlash(Empresa.Configuracoes[i].PastaRetorno);
                    Empresa.Configuracoes[i].PastaBackup = ConfiguracaoApp.RemoveEndSlash(Empresa.Configuracoes[i].PastaBackup);
                    Empresa.Configuracoes[i].PastaEnvioEmLote = ConfiguracaoApp.RemoveEndSlash(Empresa.Configuracoes[i].PastaEnvioEmLote);
                    Empresa.Configuracoes[i].PastaValidar = ConfiguracaoApp.RemoveEndSlash(Empresa.Configuracoes[i].PastaValidar);
                    Empresa.Configuracoes[i].PastaDanfeMon = ConfiguracaoApp.RemoveEndSlash(Empresa.Configuracoes[i].PastaDanfeMon);
                    Empresa.Configuracoes[i].PastaExeUniDanfe = ConfiguracaoApp.RemoveEndSlash(Empresa.Configuracoes[i].PastaExeUniDanfe);
                    Empresa.Configuracoes[i].PastaConfigUniDanfe = ConfiguracaoApp.RemoveEndSlash(Empresa.Configuracoes[i].PastaConfigUniDanfe);
                }
                #endregion

                #region Verificar a duplicação de nome de pastas que não pode existir
                if (validou)
                {
                    List<folderCompare> fc = new List<folderCompare>();

                    for (int i = 0; i < Empresa.Configuracoes.Count; i++)
                    {
                        fc.Add(new folderCompare(i, Empresa.Configuracoes[i].PastaEnvio));
                        fc.Add(new folderCompare(i + 1, Empresa.Configuracoes[i].PastaEnvioEmLote));
                        fc.Add(new folderCompare(i + 2, Empresa.Configuracoes[i].PastaRetorno));
                        fc.Add(new folderCompare(i + 3, Empresa.Configuracoes[i].PastaEnviado));
                        fc.Add(new folderCompare(i + 4, Empresa.Configuracoes[i].PastaErro));
                        fc.Add(new folderCompare(i + 5, Empresa.Configuracoes[i].PastaBackup));
                        fc.Add(new folderCompare(i + 6, Empresa.Configuracoes[i].PastaValidar));
                    }

                    foreach (folderCompare fc1 in fc)
                    {
                        if (string.IsNullOrEmpty(fc1.folder))
                            continue;

                        foreach (folderCompare fc2 in fc)
                        {
                            if (fc1.id != fc2.id)
                            {
                                if (fc1.folder.ToLower().Equals(fc2.folder.ToLower()))
                                {
                                    erro = "Não é permitido a utilização de pasta idênticas na mesma ou entre as empresas..";
                                    validou = false;
                                    break;
                                }
                            }
                        }
                        if (!validou)
                            break;
                    }
                }
                #endregion

                if (validou)
                {
                    for (int i = 0; i < Empresa.Configuracoes.Count; i++)
                    {
                        Empresa empresa = Empresa.Configuracoes[i];
                        List<string> diretorios = new List<string>();
                        List<string> mensagens = new List<string>();

                        #region Verificar se tem alguma pasta em branco
                        diretorios.Clear(); mensagens.Clear();
                        diretorios.Add(empresa.PastaEnviado); mensagens.Add("Informe a pasta para arquivamento dos arquivos XML enviados.");
                        diretorios.Add(empresa.PastaEnvio); mensagens.Add("Informe a pasta de envio dos arquivos XML.");
                        diretorios.Add(empresa.PastaRetorno); mensagens.Add("Informe a pasta de retorno dos arquivos XML.");
                        diretorios.Add(empresa.PastaErro); mensagens.Add("Informe a pasta para arquivamento temporário dos arquivos XML que apresentaram erros.");
                        diretorios.Add(empresa.PastaBackup); mensagens.Add("Informe a pasta para o Backup dos XML enviados.");
                        diretorios.Add(empresa.PastaValidar); mensagens.Add("Informe a pasta onde será gravado os XML somente para ser validado pela Aplicação.");

                        for (int b = 0; b < diretorios.Count; b++)
                        {
                            if (diretorios[b].Equals(string.Empty))
                            {
                                erro = mensagens[b].Trim() + "\r\n" + Empresa.Configuracoes[i].Nome + "\r\n" + Empresa.Configuracoes[i].CNPJ;
                                validou = false;
                                break;
                            }
                        }
                        #endregion

                        #region Verificar se o certificado foi informado
                        if (validou)
                        {
                            if (empresa.Certificado.Equals(string.Empty))
                            {
                                erro = "Selecione o certificado digital a ser utilizado na autenticação dos serviços da nota fiscal eletrônica.\r\n" + Empresa.Configuracoes[i].Nome + "\r\n" + Empresa.Configuracoes[i].CNPJ;
                                validou = false;
                            }
                        }
                        #endregion

                        #region Verificar se as pastas informadas existem
                        if (validou)
                        {
                            //Fazer um pequeno ajuste na pasta de configuração do unidanfe antes de verificar sua existência
                            if (empresa.PastaConfigUniDanfe.Trim() != string.Empty)
                            {
                                if (!string.IsNullOrEmpty(empresa.PastaConfigUniDanfe))
                                {
                                    while (Empresa.Configuracoes[i].PastaConfigUniDanfe.Substring(Empresa.Configuracoes[i].PastaConfigUniDanfe.Length - 6, 6).ToLower() == @"\dados" && !string.IsNullOrEmpty(Empresa.Configuracoes[i].PastaConfigUniDanfe))
                                        Empresa.Configuracoes[i].PastaConfigUniDanfe = Empresa.Configuracoes[i].PastaConfigUniDanfe.Substring(0, Empresa.Configuracoes[i].PastaConfigUniDanfe.Length - 6);
                                }
                                Empresa.Configuracoes[i].PastaConfigUniDanfe = Empresa.Configuracoes[i].PastaConfigUniDanfe.Replace("\r\n", "").Trim();

                                empresa.PastaConfigUniDanfe = Empresa.Configuracoes[i].PastaConfigUniDanfe;
                            }

                            diretorios.Clear(); mensagens.Clear();
                            diretorios.Add(empresa.PastaEnvio.Trim()); mensagens.Add("A pasta de envio dos arquivos XML informada não existe.");
                            diretorios.Add(empresa.PastaRetorno.Trim()); mensagens.Add("A pasta de retorno dos arquivos XML informada não existe.");
                            diretorios.Add(empresa.PastaEnviado.Trim()); mensagens.Add("A pasta para arquivamento dos arquivos XML enviados informada não existe.");
                            diretorios.Add(empresa.PastaErro.Trim()); mensagens.Add("A pasta para arquivamento temporário dos arquivos XML com erro informada não existe.");
                            diretorios.Add(empresa.PastaBackup.Trim()); mensagens.Add("A pasta para backup dos XML enviados informada não existe.");
                            diretorios.Add(empresa.PastaValidar.Trim()); mensagens.Add("A pasta para validação de XML´s informada não existe.");
                            diretorios.Add(empresa.PastaEnvioEmLote.Trim()); mensagens.Add("A pasta de envio das notas fiscais eletrônicas em lote informada não existe.");
                            diretorios.Add(empresa.PastaDanfeMon.Trim()); mensagens.Add("A pasta informada para gravação do XML da NFe para o DANFeMon não existe.");
                            diretorios.Add(empresa.PastaExeUniDanfe.Trim()); mensagens.Add("A pasta do executável do UniDANFe informada não existe.");
                            diretorios.Add(empresa.PastaConfigUniDanfe.Trim()); mensagens.Add("A pasta do arquivo de configurações do UniDANFe informada não existe.");

                            for (int b = 0; b < diretorios.Count; b++)
                            {
                                if (diretorios[b] != string.Empty)
                                {
                                    if (!Directory.Exists(diretorios[b]))
                                    {
                                        if (empresa.CriaPastasAutomaticamente)
                                        {
                                            Directory.CreateDirectory(diretorios[b]);
                                        }
                                        else
                                        {
                                            erro = mensagens[b].Trim() + "\r\n" + Empresa.Configuracoes[i].Nome + "\r\n" + Empresa.Configuracoes[i].CNPJ;
                                            validou = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Verificar se as pastas configuradas do unidanfe estão corretas
                        if (validou && empresa.PastaExeUniDanfe.Trim() != string.Empty)
                        {
                            if (!File.Exists(empresa.PastaExeUniDanfe + "\\unidanfe.exe"))
                            {
                                erro = "O executável do UniDANFe não foi localizado na pasta informada.\r\n" + Empresa.Configuracoes[i].Nome + "\r\n" + Empresa.Configuracoes[i].CNPJ;
                                validou = false;
                            }
                        }

                        if (validou && empresa.PastaConfigUniDanfe.Trim() != string.Empty)
                        {
                            //Verificar a existência o arquivo de configuração
                            if (!File.Exists(empresa.PastaConfigUniDanfe + "\\dados\\config.tps"))
                            {
                                erro = "O arquivo de configuração do UniDANFe não foi localizado na pasta informada.\r\n" + Empresa.Configuracoes[i].Nome + "\r\n" + Empresa.Configuracoes[i].CNPJ;
                                validou = false;
                            }
                        }
                        #endregion
                    }
                }
                if (!validou)
                {
                    throw new Exception(erro);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region ReconfigurarUniNFe()
        /// <summary>
        /// Método responsável por reconfigurar automaticamente o UniNFe a partir de um XML com as 
        /// informações necessárias.
        /// O Método grava um arquivo na pasta de retorno do UniNFe com a informação se foi bem 
        /// sucedida a reconfiguração ou não.
        /// </summary>
        /// <param name="cArquivoXml">Nome e pasta do arquivo de configurações gerado pelo ERP para atualização
        /// das configurações do uninfe</param>        
        /// 
        public void ReconfigurarUniNFe(string cArquivoXml)
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index;

            string cStat = "";
            string xMotivo = "";
            bool lErro = false;
            bool lEncontrouTag = false;

            try
            {
                if (Path.GetExtension(cArquivoXml).ToLower() == ".txt")
                {
                    #region Formato TXT
                    List<string> cLinhas = new Auxiliar().LerArquivo(cArquivoXml);
                    foreach (string texto in cLinhas)
                    {
                        string[] dados = texto.Split('|');
                        int nElementos = dados.GetLength(0);
                        if (nElementos <= 1)
                            continue;
                        switch (dados[0].ToLower())
                        {
                            case "pastaxmlenvio":
                                Empresa.Configuracoes[emp].PastaEnvio = (nElementos == 2 ? ConfiguracaoApp.RemoveEndSlash(dados[1].Trim()) : "");
                                lEncontrouTag = true;
                                break;
                            case "pastaxmlemlote":
                                Empresa.Configuracoes[emp].PastaEnvioEmLote = (nElementos == 2 ? ConfiguracaoApp.RemoveEndSlash(dados[1].Trim()) : "");
                                lEncontrouTag = true;
                                break;
                            case "pastaxmlretorno":
                                Empresa.Configuracoes[emp].PastaRetorno = (nElementos == 2 ? ConfiguracaoApp.RemoveEndSlash(dados[1].Trim()) : "");
                                lEncontrouTag = true;
                                break;
                            case "pastaxmlenviado": //Se a tag <PastaXmlEnviado> existir ele pega no novo conteúdo
                                Empresa.Configuracoes[emp].PastaEnviado = (nElementos == 2 ? ConfiguracaoApp.RemoveEndSlash(dados[1].Trim()) : "");
                                lEncontrouTag = true;
                                break;
                            case "pastaxmlerro":    //Se a tag <PastaXmlErro> existir ele pega no novo conteúdo
                                Empresa.Configuracoes[emp].PastaErro = (nElementos == 2 ? ConfiguracaoApp.RemoveEndSlash(dados[1].Trim()) : "");
                                lEncontrouTag = true;
                                break;
                            case "unidadefederativacodigo": //Se a tag <UnidadeFederativaCodigo> existir ele pega no novo conteúdo
                                Empresa.Configuracoes[emp].UFCod = (nElementos == 2 ? Convert.ToInt32("0" + dados[1].Trim()) : 0);
                                lEncontrouTag = true;
                                break;
                            case "ambientecodigo":  //Se a tag <AmbienteCodigo> existir ele pega no novo conteúdo
                                Empresa.Configuracoes[emp].tpAmb = (nElementos == 2 ? Convert.ToInt32("0" + dados[1].Trim()) : 1);
                                lEncontrouTag = true;
                                break;
                            case "tpemis":  //Se a tag <tpEmis> existir ele pega no novo conteúdo
                                Empresa.Configuracoes[emp].tpEmis = (nElementos == 2 ? Convert.ToInt32("0" + dados[1].Trim()) : 0);
                                lEncontrouTag = true;
                                break;
                            case "pastabackup": //Se a tag <PastaBackup> existir ele pega no novo conteúdo
                                Empresa.Configuracoes[emp].PastaBackup = (nElementos == 2 ? ConfiguracaoApp.RemoveEndSlash(dados[1].Trim()) : "");
                                lEncontrouTag = true;
                                break;
                            case "pastavalidar":    //Se a tag <PastaValidar> existir ele pega no novo conteúdo
                                Empresa.Configuracoes[emp].PastaValidar = (nElementos == 2 ? ConfiguracaoApp.RemoveEndSlash(dados[1].Trim()) : "");
                                lEncontrouTag = true;
                                break;
                            case "gravarretornotxtnfe": //Se a tag <PastaValidar> existir ele pega no novo conteúdo
                                Empresa.Configuracoes[emp].GravarRetornoTXTNFe = (nElementos == 2 ? dados[1].Trim() == "True" : false);
                                lEncontrouTag = true;
                                break;
                            case "diretoriosalvarcomo": //Se a tag <DiretorioSalvarComo> existir ele pega no novo conteúdo
                                Empresa.Configuracoes[emp].DiretorioSalvarComo = (nElementos == 2 ? dados[1].Trim() : "");
                                lEncontrouTag = true;
                                break;
                            case "diaslimpeza": //Se a tag <DiasLimpeza> existir ele pega o novo conteúdo
                                Empresa.Configuracoes[emp].DiasLimpeza = (nElementos == 2 ? Convert.ToInt32("0" + dados[1].Trim()) : 0);
                                lEncontrouTag = true;
                                break;
                            case "proxy": //Se a tag <Proxy> existir ele pega o novo conteúdo
                                ConfiguracaoApp.Proxy = (nElementos == 2 ? Convert.ToBoolean(dados[1].Trim()) : false);
                                lEncontrouTag = true;
                                break;
                            case "proxyservidor": //Se a tag <ProxyServidor> existir ele pega o novo conteúdo
                                ConfiguracaoApp.ProxyServidor = (nElementos == 2 ? dados[1].Trim() : "");
                                lEncontrouTag = true;
                                break;
                            case "proxyusuario": //Se a tag <ProxyUsuario> existir ele pega o novo conteúdo
                                ConfiguracaoApp.ProxyUsuario = (nElementos == 2 ? dados[1].Trim() : "");
                                lEncontrouTag = true;
                                break;
                            case "proxysenha": //Se a tag <ProxySenha> existir ele pega o novo conteúdo
                                ConfiguracaoApp.ProxySenha = (nElementos == 2 ? dados[1].Trim() : "");
                                lEncontrouTag = true;
                                break;
                            case "proxyporta": //Se a tag <ProxyPorta> existir ele pega o novo conteúdo
                                ConfiguracaoApp.ProxyPorta = (nElementos == 2 ? Convert.ToInt32("0" + dados[1].Trim()) : 0);
                                lEncontrouTag = true;
                                break;
                            case "pastaexeunidanfe": //Se a tag <PastaExeUniDanfe> existir ele pega no novo conteúdo
                                Empresa.Configuracoes[emp].PastaExeUniDanfe = (nElementos == 2 ? ConfiguracaoApp.RemoveEndSlash(dados[1].Trim()) : "");
                                lEncontrouTag = true;
                                break;
                            case "pastaconfigunidanfe": //Se a tag <PastaConfigUniDanfe> existir ele pega no novo conteúdo
                                Empresa.Configuracoes[emp].PastaConfigUniDanfe = (nElementos == 2 ? ConfiguracaoApp.RemoveEndSlash(dados[1].Trim()) : "");
                                lEncontrouTag = true;
                                break;
                            case "pastadanfemon": //Se a tag <PastaDanfeMon> existir ele pega no novo conteúdo
                                Empresa.Configuracoes[emp].PastaDanfeMon = (nElementos == 2 ? ConfiguracaoApp.RemoveEndSlash(dados[1].Trim()) : "");
                                lEncontrouTag = true;
                                break;
                            case "xmldanfemonnfe": //Se a tag <XMLDanfeMonNFe> existir ele pega no novo conteúdo
                                Empresa.Configuracoes[emp].XMLDanfeMonNFe = (nElementos == 2 ? dados[1].Trim() == "True" : false);
                                lEncontrouTag = true;
                                break;
                            case "xmldanfemonprocnfe": //Se a tag <XMLDanfeMonProcNFe> existir ele pega no novo conteúdo
                                Empresa.Configuracoes[emp].XMLDanfeMonProcNFe = (nElementos == 2 ? dados[1].Trim() == "True" : false);
                                lEncontrouTag = true;
                                break;
                        }
                    }
                    #endregion
                }
                else
                {
                    #region Formato XML
                    XmlDocument doc = new XmlDocument();
                    doc.Load(cArquivoXml);

                    XmlNodeList ConfUniNfeList = doc.GetElementsByTagName("altConfUniNFe");

                    foreach (XmlNode ConfUniNfeNode in ConfUniNfeList)
                    {
                        XmlElement ConfUniNfeElemento = (XmlElement)ConfUniNfeNode;

                        //Se a tag <PastaXmlEnvio> existir ele pega no novo conteúdo
                        if (ConfUniNfeElemento.GetElementsByTagName("PastaXmlEnvio").Count != 0)
                        {
                            Empresa.Configuracoes[emp].PastaEnvio = ConfiguracaoApp.RemoveEndSlash(ConfUniNfeElemento.GetElementsByTagName("PastaXmlEnvio")[0].InnerText);
                            lEncontrouTag = true;
                        }
                        //Se a tag <PastaXmlEmLote> existir ele pega no novo conteúdo
                        if (ConfUniNfeElemento.GetElementsByTagName("PastaXmlEmLote").Count != 0)
                        {
                            Empresa.Configuracoes[emp].PastaEnvioEmLote = ConfiguracaoApp.RemoveEndSlash(ConfUniNfeElemento.GetElementsByTagName("PastaXmlEmLote")[0].InnerText);
                            lEncontrouTag = true;
                        }
                        //Se a tag <PastaXmlRetorno> existir ele pega no novo conteúdo
                        if (ConfUniNfeElemento.GetElementsByTagName("PastaXmlRetorno").Count != 0)
                        {
                            Empresa.Configuracoes[emp].PastaRetorno = ConfiguracaoApp.RemoveEndSlash(ConfUniNfeElemento.GetElementsByTagName("PastaXmlRetorno")[0].InnerText);
                            lEncontrouTag = true;
                        }
                        //Se a tag <PastaXmlEnviado> existir ele pega no novo conteúdo
                        if (ConfUniNfeElemento.GetElementsByTagName("PastaXmlEnviado").Count != 0)
                        {
                            Empresa.Configuracoes[emp].PastaEnviado = ConfiguracaoApp.RemoveEndSlash(ConfUniNfeElemento.GetElementsByTagName("PastaXmlEnviado")[0].InnerText);
                            lEncontrouTag = true;
                        }
                        //Se a tag <PastaXmlErro> existir ele pega no novo conteúdo
                        if (ConfUniNfeElemento.GetElementsByTagName("PastaXmlErro").Count != 0)
                        {
                            Empresa.Configuracoes[emp].PastaErro = ConfiguracaoApp.RemoveEndSlash(ConfUniNfeElemento.GetElementsByTagName("PastaXmlErro")[0].InnerText);
                            lEncontrouTag = true;
                        }
                        //Se a tag <UnidadeFederativaCodigo> existir ele pega no novo conteúdo
                        if (ConfUniNfeElemento.GetElementsByTagName("UnidadeFederativaCodigo").Count != 0)
                        {
                            Empresa.Configuracoes[emp].UFCod = Convert.ToInt32(ConfUniNfeElemento.GetElementsByTagName("UnidadeFederativaCodigo")[0].InnerText);
                            lEncontrouTag = true;
                        }
                        //Se a tag <AmbienteCodigo> existir ele pega no novo conteúdo
                        if (ConfUniNfeElemento.GetElementsByTagName("AmbienteCodigo").Count != 0)
                        {
                            Empresa.Configuracoes[emp].tpAmb = Convert.ToInt32(ConfUniNfeElemento.GetElementsByTagName("AmbienteCodigo")[0].InnerText);
                            lEncontrouTag = true;
                        }
                        //Se a tag <tpEmis> existir ele pega no novo conteúdo
                        if (ConfUniNfeElemento.GetElementsByTagName("tpEmis").Count != 0)
                        {
                            Empresa.Configuracoes[emp].tpEmis = Convert.ToInt32(ConfUniNfeElemento.GetElementsByTagName("tpEmis")[0].InnerText);
                            lEncontrouTag = true;
                        }
                        //Se a tag <PastaBackup> existir ele pega no novo conteúdo
                        if (ConfUniNfeElemento.GetElementsByTagName("PastaBackup").Count != 0)
                        {
                            Empresa.Configuracoes[emp].PastaBackup = ConfiguracaoApp.RemoveEndSlash(ConfUniNfeElemento.GetElementsByTagName("PastaBackup")[0].InnerText);
                            lEncontrouTag = true;
                        }
                        //Se a tag <PastaValidar> existir ele pega no novo conteúdo
                        if (ConfUniNfeElemento.GetElementsByTagName("PastaValidar").Count != 0)
                        {
                            Empresa.Configuracoes[emp].PastaValidar = ConfiguracaoApp.RemoveEndSlash(ConfUniNfeElemento.GetElementsByTagName("PastaValidar")[0].InnerText);
                            lEncontrouTag = true;
                        }
                        //Se a tag <PastaValidar> existir ele pega no novo conteúdo
                        if (ConfUniNfeElemento.GetElementsByTagName("GravarRetornoTXTNFe").Count != 0)
                        {
                            Empresa.Configuracoes[emp].GravarRetornoTXTNFe = Convert.ToBoolean(ConfUniNfeElemento.GetElementsByTagName("GravarRetornoTXTNFe")[0].InnerText);
                            lEncontrouTag = true;
                        }
                        //Se a tag <DiretorioSalvarComo> existir ele pega no novo conteúdo
                        if (ConfUniNfeElemento.GetElementsByTagName("DiretorioSalvarComo").Count != 0)
                        {
                            Empresa.Configuracoes[emp].DiretorioSalvarComo = Convert.ToString(ConfUniNfeElemento.GetElementsByTagName("DiretorioSalvarComo")[0].InnerText);
                            lEncontrouTag = true;
                        }
                        //Se a tag <DiasLimpeza> existir ele pega o novo conteúdo
                        if (ConfUniNfeElemento.GetElementsByTagName("DiasLimpeza").Count != 0)
                        {
                            Empresa.Configuracoes[emp].DiasLimpeza = Convert.ToInt32(ConfUniNfeElemento.GetElementsByTagName("DiasLimpeza")[0].InnerText);
                            lEncontrouTag = true;
                        }
                        //Se a tag <Proxy> existir ele pega o novo conteúdo
                        if (ConfUniNfeElemento.GetElementsByTagName("Proxy").Count != 0)
                        {
                            ConfiguracaoApp.Proxy = Convert.ToBoolean(ConfUniNfeElemento.GetElementsByTagName("Proxy")[0].InnerText);
                            lEncontrouTag = true;
                        }
                        //Se a tag <ProxyServidor> existir ele pega o novo conteúdo
                        if (ConfUniNfeElemento.GetElementsByTagName("ProxyServidor").Count != 0)
                        {
                            ConfiguracaoApp.ProxyServidor = ConfUniNfeElemento.GetElementsByTagName("ProxyServidor")[0].InnerText;
                            lEncontrouTag = true;
                        }
                        //Se a tag <ProxyUsuario> existir ele pega o novo conteúdo
                        if (ConfUniNfeElemento.GetElementsByTagName("ProxyUsuario").Count != 0)
                        {
                            ConfiguracaoApp.ProxyUsuario = ConfUniNfeElemento.GetElementsByTagName("ProxyUsuario")[0].InnerText;
                            lEncontrouTag = true;
                        }
                        //Se a tag <ProxySenha> existir ele pega o novo conteúdo
                        if (ConfUniNfeElemento.GetElementsByTagName("ProxySenha").Count != 0)
                        {
                            ConfiguracaoApp.ProxySenha = ConfUniNfeElemento.GetElementsByTagName("ProxySenha")[0].InnerText;
                            lEncontrouTag = true;
                        }
                        //Se a tag <ProxyPorta> existir ele pega o novo conteúdo
                        if (ConfUniNfeElemento.GetElementsByTagName("ProxyPorta").Count != 0)
                        {
                            ConfiguracaoApp.ProxyPorta = Convert.ToInt32("0" + ConfUniNfeElemento.GetElementsByTagName("ProxyPorta")[0].InnerText);
                            lEncontrouTag = true;
                        }
                        //Se a tag <PastaExeUniDanfe> existir ele pega no novo conteúdo
                        if (ConfUniNfeElemento.GetElementsByTagName("PastaExeUniDanfe").Count != 0)
                        {
                            Empresa.Configuracoes[emp].PastaExeUniDanfe = ConfiguracaoApp.RemoveEndSlash(ConfUniNfeElemento.GetElementsByTagName("PastaExeUniDanfe")[0].InnerText);
                            lEncontrouTag = true;
                        }
                        //Se a tag <PastaConfigUniDanfe> existir ele pega no novo conteúdo
                        if (ConfUniNfeElemento.GetElementsByTagName("PastaConfigUniDanfe").Count != 0)
                        {
                            Empresa.Configuracoes[emp].PastaConfigUniDanfe = ConfiguracaoApp.RemoveEndSlash(ConfUniNfeElemento.GetElementsByTagName("PastaConfigUniDanfe")[0].InnerText);
                            lEncontrouTag = true;
                        }
                        //Se a tag <PastaDanfeMon> existir ele pega no novo conteúdo
                        if (ConfUniNfeElemento.GetElementsByTagName("PastaDanfeMon").Count != 0)
                        {
                            Empresa.Configuracoes[emp].PastaDanfeMon = ConfiguracaoApp.RemoveEndSlash(ConfUniNfeElemento.GetElementsByTagName("PastaDanfeMon")[0].InnerText);
                            lEncontrouTag = true;
                        }
                        //Se a tag <XMLDanfeMonNFe> existir ele pega no novo conteúdo
                        if (ConfUniNfeElemento.GetElementsByTagName("XMLDanfeMonNFe").Count != 0)
                        {
                            Empresa.Configuracoes[emp].XMLDanfeMonNFe = Convert.ToBoolean(ConfiguracaoApp.RemoveEndSlash(ConfUniNfeElemento.GetElementsByTagName("XMLDanfeMonNFe")[0].InnerText));
                            lEncontrouTag = true;
                        }
                        //Se a tag <XMLDanfeMonNFe> existir ele pega no novo conteúdo
                        if (ConfUniNfeElemento.GetElementsByTagName("XMLDanfeMonProcNFe").Count != 0)
                        {
                            Empresa.Configuracoes[emp].XMLDanfeMonProcNFe = Convert.ToBoolean(ConfiguracaoApp.RemoveEndSlash(ConfUniNfeElemento.GetElementsByTagName("XMLDanfeMonProcNFe")[0].InnerText));
                            lEncontrouTag = true;
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                cStat = "2";
                xMotivo = "Ocorreu uma falha ao tentar alterar a configuracao do " + InfoApp.NomeAplicacao() + ": " + ex.Message;
                lErro = true;
            }

            if (lEncontrouTag == true)
            {
                if (lErro == false)
                {
                    try
                    {
                        this.GravarConfig();

                        cStat = "1";
                        xMotivo = "Configuracao do " + InfoApp.NomeAplicacao() + " alterada com sucesso";
                        lErro = false;
                    }
                    catch (Exception ex)
                    {
                        cStat = "2";
                        xMotivo = "Ocorreu uma falha ao tentar alterar a configuracao do " + InfoApp.NomeAplicacao() + ": " + ex.Message;
                        lErro = true;
                    }
                }
            }
            else
            {
                cStat = "2";
                xMotivo = "Ocorreu uma falha ao tentar alterar a configuracao do " + InfoApp.NomeAplicacao() + ": Nenhuma tag padrão de configuração foi localizada no XML";
                lErro = true;
            }

            //Se deu algum erro tenho que voltar as configurações como eram antes, ou seja
            //exatamente como estavam gravadas no XML de configuração
            if (lErro == true)
            {
                ConfiguracaoApp.CarregarDados();
            }

            //Gravar o XML de retorno com a informação do sucesso ou não na reconfiguração
            string cArqRetorno = Empresa.Configuracoes[emp].PastaRetorno + "\\uninfe-ret-alt-con.xml";
            if (Path.GetExtension(cArquivoXml).ToLower() == ".txt")
                cArqRetorno = Empresa.Configuracoes[emp].PastaRetorno + "\\uninfe-ret-alt-con.txt";

            try
            {
                FileInfo oArqRetorno = new FileInfo(cArqRetorno);
                if (oArqRetorno.Exists == true)
                {
                    oArqRetorno.Delete();
                }

                if (Path.GetExtension(cArquivoXml).ToLower() == ".txt")
                {
                    File.WriteAllText(cArqRetorno, "cStat|" + cStat + "\r\nxMotivo|" + xMotivo + "\r\n", Encoding.Default);
                }
                else
                {
                    XmlWriterSettings oSettings = new XmlWriterSettings();
                    UTF8Encoding c = new UTF8Encoding(false);

                    oSettings.Encoding = c;
                    oSettings.Indent = true;
                    oSettings.IndentChars = "";
                    oSettings.NewLineOnAttributes = false;
                    oSettings.OmitXmlDeclaration = false;

                    XmlWriter oXmlGravar = XmlWriter.Create(cArqRetorno, oSettings);

                    oXmlGravar.WriteStartDocument();
                    oXmlGravar.WriteStartElement("retAltConfUniNFe");
                    oXmlGravar.WriteElementString("cStat", cStat);
                    oXmlGravar.WriteElementString("xMotivo", xMotivo);
                    oXmlGravar.WriteEndElement(); //retAltConfUniNFe
                    oXmlGravar.WriteEndDocument();
                    oXmlGravar.Flush();
                    oXmlGravar.Close();
                }
            }
            catch (Exception ex)
            {
                //Ocorreu erro na hora de gerar o arquivo de erro para o ERP
                ///
                /// danasa 8-2009
                /// 
                Auxiliar oAux = new Auxiliar();
                oAux.GravarArqErroERP(Path.GetFileNameWithoutExtension(cArqRetorno) + ".err", xMotivo + Environment.NewLine + ex.Message);
            }

            try
            {
                //Deletar o arquivo de configurações automáticas gerado pelo ERP
                FileInfo oArqReconf = new FileInfo(cArquivoXml);
                oArqReconf.Delete();
            }
            catch
            {
                //Não vou fazer nada, so trato a exceção para evitar fechar o aplicativo. Wandrey 09/03/2010
            }
        }
        #endregion

        #region RemoveEndSlash
        /// <summary>
        /// danasa 8-2009
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveEndSlash(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                while (value.Substring(value.Length - 1, 1) == @"\" && !string.IsNullOrEmpty(value))
                    value = value.Substring(0, value.Length - 1);
            }
            else
            {
                value = string.Empty;
            }
            return value.Replace("\r\n", "").Trim();
        }
        #endregion

        #endregion
    }
    #endregion

    #region Classe interna para comparar as pastas informadas nas configurações do UniNFe
    /// <summary>
    /// danasa 8-2009
    /// classe interna para comparar as pastas informadas
    /// </summary>
    internal class folderCompare
    {
        public Int32 id { get; set; }
        public string folder { get; set; }

        public folderCompare(Int32 _id, string _folder)
        {
            this.id = _id;
            this.folder = _folder;
        }
    }
    #endregion
}

