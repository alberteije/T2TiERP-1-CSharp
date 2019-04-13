using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Threading;

namespace UniNFeLibrary
{
    /// <summary>
    /// Classe para invocar os métodos e propriedades das classes dos webservices da NFE
    /// </summary>
    public class InvocarObjeto
    {
        #region Objetos
        private Auxiliar oAux = new Auxiliar();
        #endregion

        #region Métodos

        #region Invocar()
        /// <summary>
        /// Metodo responsável por invocar o serviço do WebService do SEFAZ
        /// </summary>
        /// <param name="oWSProxy">Objeto da classe construida do WSDL</param>
        /// <param name="oServicoWS">Objeto da classe de envio do XML</param>
        /// <param name="cMetodo">Método da classe de envio do XML que faz o envio</param>
        /// <param name="oCabecMsg">Objeto da classe de cabecalho do serviço</param>
        /// <param name="oServicoNFe">Objeto do Serviço de envio da NFE do UniNFe</param>
        /// <param name="cFinalArqEnvio">string do final do arquivo a ser enviado. Sem a extensão ".xml"</param>
        /// <param name="cFinalArqRetorno">string do final do arquivo a ser gravado com o conteúdo do retorno. Sem a extensão ".xml"</param>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 17/03/2010
        /// </remarks>
        public void Invocar(WebServiceProxy oWSProxy,
                            object oServicoWS,
                            string cMetodo,
                            object oCabecMsg,
                            object oServicoNFe,
                            string cFinalArqEnvio,
                            string cFinalArqRetorno)
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index;

            XmlDocument docXML = new XmlDocument();

            // Definir o tipo de serviço da NFe
            Type typeServicoNFe = oServicoNFe.GetType();

            // Resgatar o nome do arquivo XML a ser enviado para o webservice
            string XmlNfeDadosMsg = (string)(typeServicoNFe.InvokeMember("vXmlNfeDadosMsg", System.Reflection.BindingFlags.GetProperty, null, oServicoNFe, null));

            try
            {
                //Verificar se o certificado digital está vencido, se tiver vai forçar uma exceção
                CertificadoDigital CertDig = new CertificadoDigital();
                CertDig.PrepInfCertificado(Empresa.Configuracoes[emp].X509Certificado);

                if (CertDig.lLocalizouCertificado == true)
                {
                    if (DateTime.Compare(DateTime.Now, CertDig.dValidadeFinal) > 0)
                    {
                        throw new ExceptionInvocarObjeto(ErroPadrao.CertificadoVencido, "(" + CertDig.dValidadeInicial.ToString() + " a " + CertDig.dValidadeFinal.ToString() + ")");
                    }
                }

                // Exclui o Arquivo de Erro
                oAux.DeletarArquivo(Empresa.Configuracoes[emp].PastaRetorno + "\\" + oAux.ExtrairNomeArq(XmlNfeDadosMsg, cFinalArqEnvio + ".xml") + cFinalArqRetorno + ".err");

                // Validar o Arquivo XML
                string cResultadoValidacao = oAux.ValidarArqXML(XmlNfeDadosMsg);
                if (cResultadoValidacao != "")
                {
                    throw new Exception(cResultadoValidacao);
                }

                // Montar o XML de Lote de envio de Notas fiscais
                docXML.Load(XmlNfeDadosMsg);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            // Definir Proxy
            if (ConfiguracaoApp.Proxy)
            {
                oWSProxy.SetProp(oServicoWS, "Proxy", Auxiliar.DefinirProxy());
            }

            // Limpa a variável de retorno
            XmlNode XmlRetorno;

            //Vou mudar o timeout para evitar que demore a resposta e o uninfe aborte antes de recebe-la. Wandrey 17/09/2009
            //Isso talvez evite de não conseguir o número do recibo se o serviço do SEFAZ estiver lento.
            oWSProxy.SetProp(oServicoWS, "Timeout", 60000);

            try
            {
                //Verificar antes se tem conexão com a internet, se não tiver já gera uma exceção no padrão já esperado pelo ERP
                if (!InternetCS.IsConnectedToInternet())
                {
                    //Registrar o erro da validação para o sistema ERP
                    throw new ExceptionInvocarObjeto(ErroPadrao.FalhaInternet, "\r\nArquivo: " + XmlNfeDadosMsg);
                }

                //Atribuir conteúdo para uma propriedade da classe NfeStatusServico2
                if (cMetodo.Substring(0, 3).ToLower() == "sce") // DPEC
                {
                    oWSProxy.SetProp(oServicoWS, "sceCabecMsgValue", oCabecMsg);
                }
                else
                {
                    switch (ConfiguracaoApp.TipoAplicativo)
                    {
                        case UniNFeLibrary.Enums.TipoAplicativo.Cte:
                            oWSProxy.SetProp(oServicoWS, "cteCabecMsgValue", oCabecMsg);
                            break;

                        case UniNFeLibrary.Enums.TipoAplicativo.Nfe:
                            oWSProxy.SetProp(oServicoWS, "nfeCabecMsgValue", oCabecMsg);
                            break;

                        default:
                            break;
                    }
                }


                try
                {
                    //Invocar o membro
                    XmlRetorno = (XmlNode)oWSProxy.InvokeXML(oServicoWS, cMetodo, new object[] { docXML });
                }
                catch (Exception ex)
                {
                    if (cMetodo.Substring(0, 3).ToLower() == "sce") //danasa 21/10/2010
                        throw new ExceptionEnvioXML(ErroPadrao.FalhaEnvioXmlWSDPEC, "\r\nArquivo " + XmlNfeDadosMsg + "\r\nMessage Exception: " + ex.Message);

                    //Se for XML da NFe a mensagem é padronizada, caso contrário é uma mensagem geral. Wandrey 25/02/2011
                    if (cMetodo == "nfeRecepcaoLote2")
                        throw new ExceptionEnvioXML(ErroPadrao.FalhaEnvioXmlNFeWS, "\r\nArquivo " + XmlNfeDadosMsg + "\r\nMessage Exception: " + ex.Message);
                    else
                        throw new ExceptionEnvioXML(ErroPadrao.FalhaEnvioXmlWS, "\r\nArquivo " + XmlNfeDadosMsg + "\r\nMessage Exception: " + ex.Message);
                }

                //Atualizar o atributo do serviço da Nfe com o conteúdo retornado do webservice do sefaz                  
                typeServicoNFe.InvokeMember("vStrXmlRetorno", System.Reflection.BindingFlags.SetProperty, null, oServicoNFe, new object[] { XmlRetorno.OuterXml });

                // Registra o retorno de acordo com o status obtido
                if (cFinalArqEnvio != string.Empty && cFinalArqRetorno != string.Empty)
                {
                    typeServicoNFe.InvokeMember("XmlRetorno", System.Reflection.BindingFlags.InvokeMethod, null, oServicoNFe, new Object[] { cFinalArqEnvio + ".xml", cFinalArqRetorno + ".xml" });
                }
            }
            catch (ExceptionEnvioXML ex)
            {
                throw (ex);
            }
            catch (ExceptionInvocarObjeto ex)
            {
                throw (ex);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region Invocar()
        /// <summary>
        /// Metodo responsável por invocar o serviço do WebService do SEFAZ
        /// </summary>
        /// <param name="oWSProxy">Objeto da classe construida do WSDL</param>
        /// <param name="oServicoWS">Objeto da classe de envio do XML</param>
        /// <param name="cMetodo">Método da classe de envio do XML que faz o envio</param>
        /// <param name="oCabecMsg">Objeto da classe de cabecalho do serviço</param>
        /// <param name="oServicoNFe">Objeto do Serviço de envio da NFE do UniNFe</param>
        /// <remarks>
        /// Observaçoes: Como esta sobrecarga não tem os parâmetros "cFinalArqEnvio e cFinalArqRetorno", 
        /// não será gerado o arquivo de retorno do webservice, 
        /// sendo assim no ponto onde este foi chamado deve-se manualmente fazer a gravação do retorno se for do interesse
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 17/03/2010
        /// </remarks>
        public void Invocar(WebServiceProxy oWSProxy,
                            object oServicoWS,
                            string cMetodo,
                            object oCabecMsg,
                            object oServicoNFe)
        {
            try
            {
                this.Invocar(oWSProxy, oServicoWS, cMetodo, oCabecMsg, oServicoNFe, string.Empty, string.Empty);
            }
            catch (ExceptionEnvioXML ex)
            {
                throw (ex);
            }
            catch (ExceptionInvocarObjeto ex)
            {
                throw (ex);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region Invocar() - NFe Versão 1.10
        /// <summary>
        /// Metodo responsável por invocar o serviço do WebService do SEFAZ - NFe Versão 1.10
        /// </summary>
        /// <param name="oWSProxy">Objeto da classe construida do WSDL</param>
        /// <param name="oServicoWS">Objeto da classe de envio do XML</param>
        /// <param name="cMetodo">Método da classe de envio do XML que faz o envio</param>
        /// <param name="oCabecMsg">Objeto da classe de cabecalho do serviço</param>
        /// <param name="oServicoNFe">Objeto do Serviço de envio da NFE do UniNFe</param>
        /// <param name="cFinalArqEnvio">string do final do arquivo a ser enviado. Sem a extensão ".xml"</param>
        /// <param name="cFinalArqRetorno">string do final do arquivo a ser gravado com o conteúdo do retorno. Sem a extensão ".xml"</param>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 17/03/2010
        /// </remarks>
        public void Invocar(WebServiceProxy oWSProxy,
                            object oServicoWS,
                            string cMetodo,
                            object oServicoNFe,
                            string cFinalArqEnvio,
                            string cFinalArqRetorno)
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index;

            // Definir o tipo de serviço da NFe
            Type typeServicoNFe = oServicoNFe.GetType();

            // Resgatar o nome do arquivo XML a ser enviado para o webservice
            string XmlNfeDadosMsg = (string)(typeServicoNFe.InvokeMember("vXmlNfeDadosMsg", System.Reflection.BindingFlags.GetProperty, null, oServicoNFe, null));

            try
            {
                //Verificar se o certificado digital está vencido, se tiver vai forçar uma exceção
                CertificadoDigital CertDig = new CertificadoDigital();
                CertDig.PrepInfCertificado(Empresa.Configuracoes[emp].X509Certificado);

                if (CertDig.lLocalizouCertificado == true)
                {
                    if (DateTime.Compare(DateTime.Now, CertDig.dValidadeFinal) > 0)
                    {
                        throw new ExceptionInvocarObjeto(ErroPadrao.CertificadoVencido, "(" + CertDig.dValidadeInicial.ToString() + " a " + CertDig.dValidadeFinal.ToString() + ")");
                    }
                }

                // Exclui o Arquivo de Erro
                oAux.DeletarArquivo(Empresa.Configuracoes[emp].PastaRetorno + "\\" + oAux.ExtrairNomeArq(XmlNfeDadosMsg, cFinalArqEnvio + ".xml") + cFinalArqRetorno + ".err");

                // Validar o Arquivo XML
                //string cResultadoValidacao = oAux.ValidarArqXML(XmlNfeDadosMsg);
                //if (cResultadoValidacao != "")
                //{
                //    throw new Exception(cResultadoValidacao);
                //}
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            // Definir Proxy
            if (ConfiguracaoApp.Proxy)
            {
                oWSProxy.SetProp(oServicoWS, "Proxy", Auxiliar.DefinirProxy());
            }

            // Limpa a variável de retorno
            string XmlRetorno;

            //Vou mudar o timeout para evitar que demore a resposta e o uninfe aborte antes de recebe-la. Wandrey 17/09/2009
            //Isso talvez evite de não conseguir o número do recibo se o serviço do SEFAZ estiver lento.
            oWSProxy.SetProp(oServicoWS, "Timeout", 60000);

            try
            {
                //Verificar antes se tem conexão com a internet, se não tiver já gera uma exceção no padrão já esperado pelo ERP
                if (!InternetCS.IsConnectedToInternet())
                {
                    //Registrar o erro da validação para o sistema ERP
                    throw new ExceptionInvocarObjeto(ErroPadrao.FalhaInternet, "\r\nArquivo: " + XmlNfeDadosMsg);
                }

                string CabecMsg = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><cabecMsg xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"1.02\"><versaoDados>1.07</versaoDados></cabecMsg>";
                string NFeDadosMsg = oAux.XmlToString(XmlNfeDadosMsg);

                try
                {
                    //Invocar o membro
                    XmlRetorno = (string)oWSProxy.InvokeStr(oServicoWS, cMetodo, new object[] { CabecMsg, NFeDadosMsg });
                }
                catch (Exception ex)
                {
                    throw new ExceptionEnvioXML(ErroPadrao.FalhaEnvioXmlWS, "\r\nArquivo " + XmlNfeDadosMsg + "\r\nMessage Exception: " + ex.Message);
                }

                //Atualizar o atributo do serviço da Nfe com o conteúdo retornado do webservice do sefaz                  
                typeServicoNFe.InvokeMember("vStrXmlRetorno", System.Reflection.BindingFlags.SetProperty, null, oServicoNFe, new object[] { XmlRetorno });

                // Registra o retorno de acordo com o status obtido
                if (cFinalArqEnvio != string.Empty && cFinalArqRetorno != string.Empty)
                {
                    typeServicoNFe.InvokeMember("XmlRetorno", System.Reflection.BindingFlags.InvokeMethod, null, oServicoNFe, new Object[] { cFinalArqEnvio + ".xml", cFinalArqRetorno + ".xml" });
                }

            }
            catch (ExceptionEnvioXML ex)
            {
                throw (ex);
            }
            catch (ExceptionInvocarObjeto ex)
            {
                throw (ex);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region Invocar() - NFe Versão 1.10
        public void Invocar(WebServiceProxy oWSProxy,
                            object oServicoWS,
                            string cMetodo,
                            object oServicoNFe)
        {
            try
            {
                this.Invocar(oWSProxy, oServicoWS, cMetodo, oServicoNFe, string.Empty, string.Empty);
            }
            catch (ExceptionEnvioXML ex)
            {
                throw (ex);
            }
            catch (ExceptionInvocarObjeto ex)
            {
                throw (ex);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion


        #endregion
    }

    /// <summary>
    /// Classe para tratamento de exceções da classe Invocar Objeto
    /// </summary>
    public class ExceptionInvocarObjeto : Exception
    {
        public ErroPadrao ErrorCode { get; private set; }

        /// <summary>
        /// Construtor que já define uma mensagem pré-definida de exceção
        /// </summary>
        /// <param name="CodigoErro">Código da mensagem de erro (Classe MsgErro)</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>24/11/2009</date>
        public ExceptionInvocarObjeto(ErroPadrao Erro)
            : base(MsgErro.ErroPreDefinido(Erro))
        {
            this.ErrorCode = Erro;
        }

        /// <summary>
        /// Construtor que ´já define uma mensagem pré-definida de exceção com possibilidade de complemento da mensagem
        /// </summary>
        /// <param name="CodigoErro">Código da mensagem de erro (Classe MsgErro)</param>
        /// <param name="ComplementoMensagem">Complemento da mensagem de exceção</param>
        public ExceptionInvocarObjeto(ErroPadrao Erro, string ComplementoMensagem)
            : base(MsgErro.ErroPreDefinido(Erro, ComplementoMensagem))
        {
            this.ErrorCode = Erro;
        }
    }

    /// <summary>
    /// Classe para tratamento de exceções da classe Invocar Objeto, mas exatamente no ponto em que vai enviar o XML para o SEFAZ
    /// </summary>
    public class ExceptionEnvioXML : Exception
    {
        public ErroPadrao ErrorCode { get; private set; }

        /// <summary>
        /// Construtor que já define uma mensagem pré-definida de exceção
        /// </summary>
        /// <param name="CodigoErro">Código da mensagem de erro (Classe MsgErro)</param>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 16/03/2010
        /// </remarks>
        public ExceptionEnvioXML(ErroPadrao Erro)
            : base(MsgErro.ErroPreDefinido(Erro))
        {
            this.ErrorCode = Erro;
        }

        /// <summary>
        /// Construtor que ´já define uma mensagem pré-definida de exceção com possibilidade de complemento da mensagem
        /// </summary>
        /// <param name="CodigoErro">Código da mensagem de erro (Classe MsgErro)</param>
        /// <param name="ComplementoMensagem">Complemento da mensagem de exceção</param>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 16/03/2010
        /// </remarks>
        public ExceptionEnvioXML(ErroPadrao Erro, string ComplementoMensagem)
            : base(MsgErro.ErroPreDefinido(Erro, ComplementoMensagem))
        {
            this.ErrorCode = Erro;
        }
    }
}
