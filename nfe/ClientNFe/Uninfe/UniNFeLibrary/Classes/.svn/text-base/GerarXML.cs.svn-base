using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using UniNFeLibrary.Enums;
using System.Xml;

namespace UniNFeLibrary
{
    /// <summary>
    /// Classe abstrata para gerar os XML´s da nota fiscal eletrônica
    /// </summary>
    public class GerarXML
    {
        #region Atributos
        /// <summary>
        /// Index da empresa selecionada
        /// </summary>
        protected int EmpIndex { get; set; }
        /// <summary>
        /// Atributo que vai receber a string do XML de lote de NFe´s para que este conteúdo seja gravado após finalizado em arquivo físico no HD
        /// </summary>
        protected string strXMLLoteNfe;
        /// <summary>     
        /// Nome do arquivo para controle da numeração sequencial do lote.
        /// </summary>
        protected string NomeArqXmlLote;
        /// <summary>
        /// Nome do arquivo 1 de backup de segurança do arquivo de controle da numeração sequencial do lote
        /// </summary>
        protected string Bkp1NomeArqXmlLote;
        /// <summary>
        /// Nome do arquivo 2 de backup de segurança do arquivo de controle da numeração sequencial do lote
        /// </summary>
        protected string Bkp2NomeArqXmlLote;
        /// <summary>
        /// Nome do arquivo 3 de backup de segurança do arquivo de controle da numeração sequencial do lote
        /// </summary>
        protected string Bkp3NomeArqXmlLote;
        #endregion

        #region Propriedades
        /// <summary>
        /// Nome do arquivo XML que está sendo enviado para os webservices
        /// </summary>
        public string NomeXMLDadosMsg { get; set; }
        /// <summary>
        /// Serviço que está sendo executado (Envio de NFE, Cancelamento, consultas, etc...)
        /// </summary>
        public Servicos Servico { get; set; }
        #endregion

        #region Objetos
        protected Auxiliar oAux = new Auxiliar();
        #endregion

        #region Construtures
        public GerarXML(int empIndex)
        {
            EmpIndex = empIndex;
        }
        #endregion

        #region Métodos

        #region Métodos para gerar o Lote de Notas Fiscais Eletrônicas

        #region LoteNfe()
        /// <summary>
        /// Gera o Lote das Notas Fiscais passada por parâmetro na pasta de envio
        /// </summary>
        /// <param name="lstArquivosNFe">Lista dos XML´s de Notas Fiscais a serem gerados os lotes</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>15/04/2009</date>
        public void LoteNfe(List<string> lstArquivosNFe)
        {
            bool excluirFluxo = true;

            try
            {
                bool booLiberado = false;
                //Vamos verificar se todos os XML´s estão disponíveis
                for (int i = 0; i < lstArquivosNFe.Count; i++)
                {
                    booLiberado = false;
                    //Verificar se consegue abrir o arquivo em modo exclusivo
                    if (!Auxiliar.FileInUse(lstArquivosNFe[i]))
                    {
                        booLiberado = true;

                        Thread.Sleep(100);
                    }
                }

                if (booLiberado)
                {
                    //Buscar o número do lote a ser utilizado
                    Int32 intNumeroLote = 0;

                    long TamArqLote = 0;
                    bool IniciouLote = false;
                    int ContaNfe = 0;
                    List<string> lstArquivoInseridoLote = new List<string>();

                    for (int i = 0; i < lstArquivosNFe.Count; i++)
                    {
                        //Encerra o lote se o tamanho do arquivo de lote for maior ou igual a 450000 bytes (450 kbytes)
                        if (IniciouLote && TamArqLote >= 450000)
                        {
                            this.EncerrarLoteNfe(intNumeroLote);
                            this.FinalizacaoLote(intNumeroLote, lstArquivoInseridoLote);

                            //Limpar as variáveis, atributos depois de totalmente finalizado o lote, pois o conteúdo
                            //de aglumas variáveis são utilizados na finalização.
                            lstArquivoInseridoLote.Clear();
                            ContaNfe = 0;
                            TamArqLote = 0;
                            IniciouLote = false;
                        }

                        //Iniciar o Lote de NFe
                        if (!IniciouLote)
                        {
                            intNumeroLote = this.ProximoNumeroLote();

                            this.IniciarLoteNfe(intNumeroLote);

                            IniciouLote = true;
                        }

                        //Inserir o arquivo de XML da NFe na string do lote
                        this.InserirNFeLote(lstArquivosNFe[i]);
                        lstArquivoInseridoLote.Add(lstArquivosNFe[i]);
                        ContaNfe++;
                        FileInfo oArq = new FileInfo(lstArquivosNFe[i]);
                        TamArqLote += oArq.Length;

                        Thread.Sleep(100);

                        //Encerrar o Lote se já passou por todas as notas
                        //Encerrar o lote se já tiver incluido 50 notas (Quantidade máxima permitida pelo SEFAZ)
                        if ((i + 1) == lstArquivosNFe.Count || ContaNfe == 50)
                        {
                            //Encerra o lote
                            this.EncerrarLoteNfe(intNumeroLote);

                            //Se já encerrou o lote não pode mais tirar do fluxo se der erro daqui para baixo
                            excluirFluxo = false;

                            //Finalizar o lote gerando retornos para o ERP.
                            this.FinalizacaoLote(intNumeroLote, lstArquivoInseridoLote);

                            //Limpar as variáveis, atributos depois de totalmente finalizado o lote, pois o conteúdo
                            //de aglumas variáveis são utilizados na finalização.
                            lstArquivoInseridoLote.Clear();
                            ContaNfe = 0;
                            TamArqLote = 0;
                            IniciouLote = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (excluirFluxo)
                {
                    for (int i = 0; i < lstArquivosNFe.Count; i++)
                    {
                        //Efetua a leitura do XML da NFe
                        LerXML.DadosNFeClass oDadosNfe = this.LerXMLNFe(lstArquivosNFe[i]);

                        //Excluir a nota fiscal do fluxo pois deu algum erro neste ponto
                        FluxoNfe oFluxoNfe = new FluxoNfe();
                        oFluxoNfe.ExcluirNfeFluxo(oDadosNfe.chavenfe);
                    }
                }

                throw (ex);
            }
        }
        #endregion

        #region FinalizacaoLote()
        /// <summary>
        /// Executa alguns procedimentos para finalizar o processo de montagem de 1 lote de notas
        /// </summary>
        /// <by>Wandrey Mundin Ferreira</by>
        private void FinalizacaoLote(int intNumeroLote, List<string> lstArquivosNFe)
        {
            try
            {
                //Vou atualizar os lotes das NFE´s no fluxo de envio somente depois de encerrado o lote onde eu 
                //tenho certeza que ele foi gerado e que nenhum erro aconteceu, pois desta forma, se falhar somente na 
                //atualização eu tenho como fazer o UniNFe se recuperar de um erro. Assim sendo não mude de ponto.

                FluxoNfe oFluxoNfe = new FluxoNfe();
                for (int i = 0; i < lstArquivosNFe.Count; i++)
                {
                    //Efetua a leitura do XML, tem que acontecer antes de mover o arquivo
                    LerXML.DadosNFeClass oDadosNfe = this.LerXMLNFe(lstArquivosNFe[i]);

                    //Mover o XML da NFE para a pasta de enviados em processamento
                    oAux.MoverArquivo(lstArquivosNFe[i], PastaEnviados.EmProcessamento);

                    //Atualiza o arquivo de controle de fluxo
                    oFluxoNfe.AtualizarTag(oDadosNfe.chavenfe, FluxoNfe.ElementoEditavel.idLote, intNumeroLote.ToString("000000000000000"));

                    //Gravar o XML de retorno do número do lote para o ERP
                    this.GravarXMLLoteRetERP(intNumeroLote, lstArquivosNFe[i]);

                    Thread.Sleep(100);
                }
            }
            catch (IOException ex)
            {
                throw (ex);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region LoteNfe() - Sobrecarga
        /// <summary>
        /// Gera lote da nota fiscal eletrônica com somente uma nota fiscal
        /// </summary>
        /// <param name="strArquivoNfe">Nome do arquivo XML da Nota Fiscal</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>15/04/2009</date>
        public void LoteNfe(string strArquivoNfe)
        {
            List<string> lstArquivo = new List<string>();

            lstArquivo.Add(strArquivoNfe);

            try
            {
                this.LoteNfe(lstArquivo);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region IniciarLoteNfe()
        /// <summary>
        /// Inicia a string do XML do Lote de notas fiscais
        /// </summary>
        /// <param name="intNumeroLote">Número do lote que será enviado</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>15/04/2009</date>
        protected void IniciarLoteNfe(Int32 intNumeroLote)
        {
            strXMLLoteNfe = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

            switch (ConfiguracaoApp.TipoAplicativo)
            {
                case TipoAplicativo.Cte:
                    strXMLLoteNfe += "<enviCTe xmlns=\"http://www.portalfiscal.inf.br/cte\" versao=\"" + "1.03" + "\">";
                    break;

                case TipoAplicativo.Nfe:
                    strXMLLoteNfe += "<enviNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"" + "2.00" + "\">";
                    break;

                default:
                    break;
            }

            strXMLLoteNfe += "<idLote>" + intNumeroLote.ToString("000000000000000") + "</idLote>";
        }

        #endregion

        #region InserirNFeLote()
        /// <summary>
        /// Insere o XML de Nota Fiscal passado por parâmetro na string do XML de Lote de NFe
        /// </summary>
        /// <param name="strArquivoNfe">Nome do arquivo XML de nota fiscal eletrônica a ser inserido no lote</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>15/04/2009</date>
        protected void InserirNFeLote(string strArquivoNfe)
        {
            try
            {
                string vNfeDadosMsg = this.oAux.XmlToString(strArquivoNfe);

                string tipo = string.Empty;
                switch (ConfiguracaoApp.TipoAplicativo)
                {
                    case TipoAplicativo.Cte:
                        tipo = "<CTe";
                        break;

                    case TipoAplicativo.Nfe:
                        tipo = "<NFe";
                        break;

                    default:
                        break;
                }

                //Separar somente o conteúdo a partir da tag <NFe> até </NFe>
                Int32 nPosI = vNfeDadosMsg.IndexOf(tipo);
                Int32 nPosF = vNfeDadosMsg.Length - nPosI;
                strXMLLoteNfe += vNfeDadosMsg.Substring(nPosI, nPosF);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region EncerrarLoteNfe()
        /// <summary>
        /// Encerra a string do XML de lote de notas fiscais eletrônicas
        /// </summary>
        /// <param name="intNumeroLote">Número do lote que será enviado</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>15/04/2009</date>
        protected void EncerrarLoteNfe(Int32 intNumeroLote)
        {
            switch (ConfiguracaoApp.TipoAplicativo)
            {
                case TipoAplicativo.Cte:
                    strXMLLoteNfe += "</enviCTe>";
                    break;

                case TipoAplicativo.Nfe:
                    strXMLLoteNfe += "</enviNFe>";
                    break;

                default:
                    break;
            }

            try
            {
                this.GerarXMLLote(intNumeroLote);
            }
            catch (IOException ex)
            {
                throw (ex);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region GerarXMLLote()
        /// <summary>
        /// Grava o XML de lote de notas fiscais eletrônicas fisicamente no HD na pasta de envio
        /// </summary>
        /// <param name="intNumeroLote">Número do lote que será enviado</param>
        /// <date>15/04/2009</date>
        /// <by>Wandrey Mundin Ferreira</by>
        protected void GerarXMLLote(Int32 intNumeroLote)
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index; 

            //Gravar o XML do lote das notas fiscais
            string vNomeArqLoteNfe = Empresa.Configuracoes[emp].PastaEnvio + "\\" +
                                     intNumeroLote.ToString("000000000000000") +
                                     ExtXml.EnvLot;

            StreamWriter SW_2 = null;

            try
            {
                SW_2 = File.CreateText(vNomeArqLoteNfe);
                SW_2.Write(strXMLLoteNfe);
                SW_2.Close();
            }
            catch (IOException ex)
            {
                throw (ex);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                SW_2.Close();
            }
        }
        #endregion

        /// <summary>
        /// Popular a propriedade do nome do arquivo de controle da numeração do lote
        /// </summary>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 20/08/2010
        /// </remarks>
        private void PopulateNomeArqLote()
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index; 

            NomeArqXmlLote = Empresa.Configuracoes[emp].PastaEmpresa + "\\UniNfeLote.xml";
            Bkp1NomeArqXmlLote = Empresa.Configuracoes[emp].PastaEmpresa + "\\Bkp1_UniNfeLote.xml";
            Bkp2NomeArqXmlLote = Empresa.Configuracoes[emp].PastaEmpresa + "\\Bkp2_UniNfeLote.xml";
            Bkp3NomeArqXmlLote = Empresa.Configuracoes[emp].PastaEmpresa + "\\Bkp3_UniNfeLote.xml";
        }

        #region ProximoNumeroLote()
        /// <summary>
        /// Pega o ultimo número de lote utilizado e acrescenta mais 1 para novo envio
        /// </summary>
        /// <returns>Retorna o um novo número de lote a ser utilizado nos envios das notas fiscais</returns>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>15/04/2009</date>
        private Int32 ProximoNumeroLote()
        {
            PopulateNomeArqLote();

            Int32 intNumeroLote = 1;
            bool deuErro = false;

            for (int i = 0; i < 4; i++)
            {
                XmlTextReader oLerXml = null;

                try
                {
                    if (File.Exists(NomeArqXmlLote))
                    {
                        //Carregar os dados do arquivo XML de configurações do UniNfe
                        oLerXml = new XmlTextReader(NomeArqXmlLote);

                        while (oLerXml.Read())
                        {
                            if (oLerXml.NodeType == XmlNodeType.Element)
                            {
                                if (oLerXml.Name == "UltimoLoteEnviado")
                                {
                                    oLerXml.Read(); intNumeroLote = Convert.ToInt32(oLerXml.Value) + 1;

                                    //Vou somar uns 3 números para frente para evitar repetir os números.
                                    if (deuErro)
                                        intNumeroLote += 3;

                                    break;
                                }
                            }
                        }

                        oLerXml.Close();
                    }

                    this.SalvarNumeroLoteUtilizado(intNumeroLote);

                    break;
                }
                catch (Exception ex)
                {
                    deuErro = true;
                    //Fechar o arquivo se o mesmo estiver aberto
                    if (oLerXml != null)
                        if (oLerXml.ReadState != ReadState.Closed)
                            oLerXml.Close();

                    switch (i)
                    {
                        case 0:
                            if (File.Exists(Bkp1NomeArqXmlLote))
                                File.Copy(Bkp1NomeArqXmlLote, NomeArqXmlLote, true);
                            break;

                        case 1:
                            if (File.Exists(Bkp2NomeArqXmlLote))
                                File.Copy(Bkp2NomeArqXmlLote, NomeArqXmlLote, true);
                            break;

                        case 2:
                            if (File.Exists(Bkp3NomeArqXmlLote))
                                File.Copy(Bkp3NomeArqXmlLote, NomeArqXmlLote, true);
                            break;

                        case 3:
                            throw new Exception("Não foi possível efetuar a leitura do arquivo " + NomeArqXmlLote + ". Verifique se o mesmo não está com sua estrutura de XML danificada."); //Se tentou 4 vezes e deu errado, vamos retornar o erro e não tem o que ser feito.

                        default:
                            break;
                    }
                }
                finally
                {
                    //Fechar o arquivo se o mesmo estiver aberto - Wandrey 20/04/2010
                    if (oLerXml != null)
                        if (oLerXml.ReadState != ReadState.Closed)
                            oLerXml.Close();
                }
            }

            return intNumeroLote;
        }
        #endregion

        #region SalvarNumeroLoteUtilizado()
        /// <summary>
        /// Salva em XML o número do ultimo lote utilizado para envio
        /// </summary>
        /// <param name="intNumeroLote">Numero do lote a ser salvo</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>15/04/2009</date>
        private void SalvarNumeroLoteUtilizado(Int32 intNumeroLote)
        {
            XmlWriterSettings oSettings = new XmlWriterSettings();
            UTF8Encoding c = new UTF8Encoding(false);

            oSettings.Encoding = c;
            oSettings.Indent = true;
            oSettings.IndentChars = "";
            oSettings.NewLineOnAttributes = false;
            oSettings.OmitXmlDeclaration = false;
            XmlWriter oXmlGravar = null;

            try
            {
                oXmlGravar = XmlWriter.Create(NomeArqXmlLote, oSettings);
                oXmlGravar.WriteStartDocument();
                oXmlGravar.WriteStartElement("DadosLoteNfe");
                oXmlGravar.WriteElementString("UltimoLoteEnviado", intNumeroLote.ToString());
                oXmlGravar.WriteEndElement(); //DadosLoteNfe
                oXmlGravar.WriteEndDocument();
                oXmlGravar.Flush();
                oXmlGravar.Close();

                //Criar 3 copias de segurança deste XML para voltar ele caso de algum problema com o mesmo.
                File.Copy(NomeArqXmlLote, Bkp1NomeArqXmlLote, true);
                File.Copy(NomeArqXmlLote, Bkp2NomeArqXmlLote, true);
                File.Copy(NomeArqXmlLote, Bkp3NomeArqXmlLote, true);
            }
            catch (XmlException ex)
            {
                throw (ex);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //Fechar o arquivo se o mesmo ainda estiver aberto - Wandrey 20/04/2010
                if (oXmlGravar != null)
                    if (oXmlGravar.WriteState != WriteState.Closed)
                        oXmlGravar.Close();
            }
        }
        #endregion

        #region GravarXMLLoteRetERP()
        /// <summary>
        /// Grava um XML com o número de lote utilizado na pasta de retorno para que o ERP possa pegar este número
        /// </summary>
        /// <param name="intNumeroLote">Número do lote a ser gravado no retorno para o ERP</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>15/04/2009</date>
        private void GravarXMLLoteRetERP(Int32 intNumeroLote, string NomeArquivoXML)
        {
            XmlWriterSettings oSettings = new XmlWriterSettings();
            UTF8Encoding c = new UTF8Encoding(false);

            oSettings.Encoding = c;
            oSettings.Indent = true;
            oSettings.IndentChars = "";
            oSettings.NewLineOnAttributes = false;
            oSettings.OmitXmlDeclaration = false;
            oSettings.Encoding = Encoding.UTF8;
            XmlWriter oXmlLoteERP = null;

            string cArqLoteRetorno = this.NomeArqLoteRetERP(NomeArquivoXML);

            try
            {
                oXmlLoteERP = XmlWriter.Create(cArqLoteRetorno, oSettings);

                oXmlLoteERP.WriteStartDocument();
                oXmlLoteERP.WriteStartElement("DadosLoteNfe");
                oXmlLoteERP.WriteElementString("NumeroLoteGerado", intNumeroLote.ToString());
                oXmlLoteERP.WriteEndElement(); //DadosLoteNfe
                oXmlLoteERP.WriteEndDocument();
                oXmlLoteERP.Flush();
                oXmlLoteERP.Close();
            }
            catch (XmlException ex)
            {
                throw (ex);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //Fechar o arquivo se o mesmo ainda estiver aberto - Wandrey 20/04/2010
                if (oXmlLoteERP != null)
                    if (oXmlLoteERP.WriteState != WriteState.Closed)
                        oXmlLoteERP.Close();
            }
        }
        #endregion

        #endregion

        #region Métodos para gerar o XML´s diversos

        #region Cancelamento()
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pFinalArqEnvio"></param>
        /// <param name="tpAmb"></param>
        /// <param name="tpEmis"></param>
        /// <param name="chNFe"></param>
        /// <param name="nProt"></param>
        /// <param name="xJust"></param>
        public void Cancelamento(string pFinalArqEnvio, int tpAmb, int tpEmis, string chNFe, string nProt, string xJust)
        {
            string tipo = string.Empty;
            switch (ConfiguracaoApp.TipoAplicativo)
            {
                case TipoAplicativo.Cte:
                    tipo = "CT";
                    break;

                case TipoAplicativo.Nfe:
                    tipo = "NF";
                    break;

                default:
                    break;
            }

            StringBuilder aXML = new StringBuilder();
            aXML.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            aXML.Append("<canc" + tipo + "e xmlns=\"" + ConfiguracaoApp.nsURI + "\" versao=\"" + ConfiguracaoApp.VersaoXMLCanc + "\">");
            aXML.AppendFormat("<infCanc Id=\"ID{0}\">", chNFe);
            aXML.AppendFormat("<tpAmb>{0}</tpAmb>", tpAmb);
            aXML.Append("<xServ>CANCELAR</xServ>");
            aXML.AppendFormat("<ch" + tipo + "e>{0}</ch" + tipo + "e>", chNFe);
            aXML.AppendFormat("<nProt>{0}</nProt>", nProt);
            aXML.AppendFormat("<xJust>{0}</xJust>", xJust);
            aXML.AppendFormat("<tpEmis>{0}</tpEmis>", tpEmis);
            aXML.Append("</infCanc>");
            aXML.Append("</canc" + tipo + "e>");

            try
            {
                GravarArquivoParaEnvio(pFinalArqEnvio, aXML.ToString());
            }
            catch (XmlException ex)
            {
                throw (ex);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region Consulta
        public void Consulta(string pFinalArqEnvio, int tpAmb, int tpEmis, string chNFe)
        {
            string tipo = string.Empty;
            switch (ConfiguracaoApp.TipoAplicativo)
            {
                case TipoAplicativo.Cte:
                    tipo = "CT";
                    break;

                case TipoAplicativo.Nfe:
                    tipo = "NF";
                    break;

                default:
                    break;
            }

            StringBuilder aXML = new StringBuilder();
            aXML.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            aXML.Append("<consSit" + tipo + "e xmlns=\"" + ConfiguracaoApp.nsURI + "\" versao=\"" + ConfiguracaoApp.VersaoXMLPedSit + "\">");
            aXML.AppendFormat("<tpAmb>{0}</tpAmb>", tpAmb);
            aXML.AppendFormat("<tpEmis>{0}</tpEmis>", tpEmis);  //<<< opcional >>>
            aXML.Append("<xServ>CONSULTAR</xServ>");
            aXML.AppendFormat("<ch" + tipo + "e>{0}</ch" + tipo + "e>", chNFe);
            aXML.Append("</consSit" + tipo + "e>");

            GravarArquivoParaEnvio(pFinalArqEnvio, aXML.ToString());
        }
        #endregion

        #region ConsultaCadastro()
        /// <summary>
        /// Cria um arquivo XML com a estrutura necessária para consultar um cadastro
        /// Voce deve preencher o estado e mais um dos tres itens: CPNJ, IE ou CPF
        /// </summary>
        /// <param name="uf">Sigla do UF do cadastro a ser consultado. Tem que ter duas letras. SU para suframa.</param>
        /// <param name="cnpj"></param>
        /// <param name="ie"></param>
        /// <param name="cpf"></param>
        /// <returns>Retorna o caminho e nome do arquivo criado</returns>
        /// <by>Marcos Diez</by>
        /// <date>29/08/2009</date>
        public string ConsultaCadastro(string pArquivo, string uf, string cnpj, string ie, string cpf)
        {
            int emp = EmpIndex;

            string header = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
                "<ConsCad xmlns=\"" + ConfiguracaoApp.nsURI +
                "\" versao=\"" + ConfiguracaoApp.VersaoXMLConsCad + "\"><infCons><xServ>CONS-CAD</xServ>";

            cnpj = OnlyNumbers(cnpj);
            ie = OnlyNumbers(ie);
            cpf = OnlyNumbers(cpf);

            StringBuilder saida = new StringBuilder();
            saida.Append(header);
            saida.AppendFormat("<UF>{0}</UF>", uf);
            if (!string.IsNullOrEmpty(cnpj))
            {
                saida.AppendFormat("<CNPJ>{0}</CNPJ>", cnpj);
            }
            else
                if (!string.IsNullOrEmpty(ie))
                {
                    saida.AppendFormat("<IE>{0}</IE>", ie);
                }
                else
                    if (!string.IsNullOrEmpty(cpf))
                    {
                        saida.AppendFormat("<CPF>{0}</CPF>", cpf);
                    }
            saida.Append("</infCons></ConsCad>");

            string _arquivo_saida = (string.IsNullOrEmpty(pArquivo) ? DateTime.Now.ToString("yyyyMMddThhmmss") + ExtXml.ConsCad : pArquivo);

            GravarArquivoParaEnvio(_arquivo_saida, saida.ToString());

            return Empresa.Configuracoes[emp].PastaEnvio + "\\" + _arquivo_saida;
        }

        /// <summary>
        /// retorna uma string contendo apenas os digitos da entrada
        /// </summary>
        /// <by>Marcos Diez</by>
        /// <date>29/08/2009</date>
        private static string OnlyNumbers(string entrada)
        {
            if (string.IsNullOrEmpty(entrada)) return null;
            StringBuilder saida = new StringBuilder(entrada.Length);
            foreach (char c in entrada)
            {
                if (char.IsDigit(c))
                {
                    saida.Append(c);
                }
            }
            return saida.ToString();
        }
        #endregion

        #region Inutilizacao
        public void Inutilizacao(string pFinalArqEnvio, int tpAmb, int tpEmis, int cUF, int ano, string CNPJ, int mod, int serie, int nNFIni, int nNFFin, string xJust)
        {
            string tipo = string.Empty;

            switch (ConfiguracaoApp.TipoAplicativo)
            {
                case TipoAplicativo.Cte:
                    tipo = "CT";
                    break;
                case TipoAplicativo.Nfe:
                    tipo = "NF";
                    break;
                default:
                    break;
            }

            StringBuilder aXML = new StringBuilder();
            aXML.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            aXML.Append("<inut" + tipo + "e xmlns=\"" + ConfiguracaoApp.nsURI + "\" versao=\"" + ConfiguracaoApp.VersaoXMLInut + "\">");
            aXML.AppendFormat("<infInut Id=\"ID{0}{1}{2}{3}{4}{5}{6}\">", cUF.ToString("00"), ano.ToString("00"), CNPJ, mod.ToString("00"), serie.ToString("000"), nNFIni.ToString("000000000"), nNFFin.ToString("000000000"));
            aXML.AppendFormat("<tpAmb>{0}</tpAmb>", tpAmb);
            aXML.AppendFormat("<tpEmis>{0}</tpEmis>", tpEmis);
            aXML.Append("<xServ>INUTILIZAR</xServ>");
            aXML.AppendFormat("<cUF>{0}</cUF>", cUF.ToString("00"));
            aXML.AppendFormat("<ano>{0}</ano>", ano.ToString("00"));
            aXML.AppendFormat("<CNPJ>{0}</CNPJ>", CNPJ);
            aXML.AppendFormat("<mod>{0}</mod>", mod.ToString("00"));
            aXML.AppendFormat("<serie>{0}</serie>", serie);
            aXML.AppendFormat("<n" + tipo + "Ini>{0}</n" + tipo + "Ini>", nNFIni);
            aXML.AppendFormat("<n" + tipo + "Fin>{0}</n" + tipo + "Fin>", nNFFin);
            aXML.AppendFormat("<xJust>{0}</xJust>", xJust);
            aXML.Append("</infInut>");
            aXML.Append("</inut" + tipo + "e>");

            GravarArquivoParaEnvio(pFinalArqEnvio, aXML.ToString());
        }
        #endregion

        #region StatusServico() - Sobrecarga
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tpEmis"></param>
        /// <param name="cUF"></param>
        /// <returns></returns>
        public string StatusServico(int tpEmis, int cUF, int amb)
        {
            string _arquivo_saida = DateTime.Now.ToString("yyyyMMddThhmmss") + ExtXml.PedSta;

            this.StatusServico(_arquivo_saida, amb, tpEmis, cUF);

            return _arquivo_saida;
        }
        #endregion

        #region StatusServico() - Sobrecarga
        /// <summary>
        /// Gera o XML de consulta status do serviço da NFe
        /// </summary>
        /// <param name="pArquivo">Caminho e nome do arquivo que é para ser gerado</param>
        /// <param name="tpAmb">Ambiente da consulta</param>
        /// <param name="tpEmis">Tipo de emissão da consulta</param>
        /// <param name="cUF">Estado para a consulta</param>
        public void StatusServico(string pArquivo, int tpAmb, int tpEmis, int cUF)
        {
            string tipo = string.Empty;
            switch (ConfiguracaoApp.TipoAplicativo)
            {
                case TipoAplicativo.Cte:
                    tipo = "Cte";
                    break;
                case TipoAplicativo.Nfe:
                    tipo = "";
                    break;

                default:
                    break;
            }

            StringBuilder vDadosMsg = new StringBuilder();
            vDadosMsg.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            vDadosMsg.Append("<consStatServ" + tipo + " versao=\"" + ConfiguracaoApp.VersaoXMLStatusServico + "\" xmlns=\"" + ConfiguracaoApp.nsURI + "\">");
            vDadosMsg.AppendFormat("<tpAmb>{0}</tpAmb>", tpAmb);
            vDadosMsg.AppendFormat("<cUF>{0}</cUF>", cUF);
            vDadosMsg.AppendFormat("<tpEmis>{0}</tpEmis>", tpEmis);
            vDadosMsg.Append("<xServ>STATUS</xServ>");
            vDadosMsg.Append("</consStatServ" + tipo + ">");

            try
            {
                GravarArquivoParaEnvio(pArquivo, vDadosMsg.ToString());
            }
            catch (XmlException ex)
            {
                throw (ex);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        public void ConsultaDPEC(string pArquivo, LerXML.DadosConsDPEC dadosConsDPEC)
        {
            StringBuilder vDadosMsg = new StringBuilder();
            vDadosMsg.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            vDadosMsg.Append("<consDPEC versao=\"" + ConfiguracaoApp.VersaoXMLConsDPEC + "\" xmlns=\"" + ConfiguracaoApp.nsURI + "\">");
            vDadosMsg.AppendFormat("<tpAmb>{0}</tpAmb>", dadosConsDPEC.tpAmb);
            vDadosMsg.AppendFormat("<verAplic>{0}</verAplic>", dadosConsDPEC.verAplic);
            if (!string.IsNullOrEmpty(dadosConsDPEC.chNFe))
                vDadosMsg.AppendFormat("<chNFe>{0}</chNFe>", dadosConsDPEC.chNFe);
            else
                vDadosMsg.AppendFormat("<nRegDPEC>{0}</nRegDPEC>", dadosConsDPEC.nRegDPEC);
            vDadosMsg.Append("</consDPEC>");

            try
            {
                GravarArquivoParaEnvio(pArquivo, vDadosMsg.ToString());
            }
            catch (XmlException ex)
            {
                throw (ex);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void EnvioDPEC(string pArquivo, LerXML.DadosEnvDPEC dadosEnvDPEC)
        {
            StringBuilder vDadosMsg = new StringBuilder();
            vDadosMsg.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            vDadosMsg.Append("<envDPEC versao=\"" + ConfiguracaoApp.VersaoXMLEnvDPEC + "\" xmlns=\"" + ConfiguracaoApp.nsURI + "\">");
            vDadosMsg.AppendFormat("<infDPEC Id=\"DPEC{0}\">", dadosEnvDPEC.CNPJ);
            vDadosMsg.Append("<ideDec>");
            vDadosMsg.AppendFormat("<cUF>{0}</cUF>", dadosEnvDPEC.cUF);
            vDadosMsg.AppendFormat("<tpAmb>{0}</tpAmb>", dadosEnvDPEC.tpAmb);
            vDadosMsg.AppendFormat("<verProc>{0}</verProc>", dadosEnvDPEC.verProc);
            vDadosMsg.AppendFormat("<CNPJ>{0}</CNPJ>", dadosEnvDPEC.CNPJ);
            vDadosMsg.AppendFormat("<IE>{0}</IE>", dadosEnvDPEC.IE);
            vDadosMsg.Append("</ideDec>");
            vDadosMsg.Append("<resNFe>");
            vDadosMsg.AppendFormat("<chNFe>{0}</chNFe>", dadosEnvDPEC.chNFe);
            if (dadosEnvDPEC.UF == "EX" || dadosEnvDPEC.CNPJCPF.Length == 0)
                vDadosMsg.Append("<CNPJ />");
            else
                if (dadosEnvDPEC.CNPJCPF.Length == 14)
                    vDadosMsg.AppendFormat("<CNPJ>{0}</CNPJ>", dadosEnvDPEC.CNPJCPF);
                else
                    vDadosMsg.AppendFormat("<CPF>{0}</CPF>", dadosEnvDPEC.CNPJCPF);
            vDadosMsg.AppendFormat("<UF>{0}</UF>", dadosEnvDPEC.UF);
            vDadosMsg.AppendFormat("<vNF>{0}</vNF>", dadosEnvDPEC.vNF);
            vDadosMsg.AppendFormat("<vICMS>{0}</vICMS>", dadosEnvDPEC.vICMS);
            vDadosMsg.AppendFormat("<vST>{0}</vST>", dadosEnvDPEC.vST);
            vDadosMsg.Append("</resNFe>");
            vDadosMsg.Append("</infDPEC>");
            vDadosMsg.Append("</envDPEC>");

            try
            {
                GravarArquivoParaEnvio(pArquivo, vDadosMsg.ToString());
            }
            catch (XmlException ex)
            {
                throw (ex);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #region XmlRetorno()
        /// <summary>
        /// Grava o XML com os dados do retorno dos webservices e deleta o XML de solicitação do serviço.
        /// </summary>
        /// <param name="finalArqEnvio">Final do nome do arquivo de solicitação do serviço.</param>
        /// <param name="finalArqRetorno">Final do nome do arquivo que é para ser gravado o retorno.</param>
        /// <param name="conteudoXMLRetorno">Conteúdo do XML a ser gerado</param>
        /// <example>
        /// // Arquivo de envio: 20080619T19113320-ped-sta.xml
        /// // Arquivo de retorno que vai ser gravado: 20080619T19113320-sta.xml
        /// this.GravarXmlRetorno("-ped-sta.xml", "-sta.xml");
        /// </example>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// </remarks>        
        public void XmlRetorno(string finalArqEnvio, string finalArqRetorno, string conteudoXMLRetorno)
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index; 

            try
            {
                XmlRetorno(finalArqEnvio, finalArqRetorno, conteudoXMLRetorno, Empresa.Configuracoes[emp].PastaRetorno);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region XmlRetorno()
        /// <summary>
        /// Grava o XML com os dados do retorno dos webservices e deleta o XML de solicitação do serviço.
        /// </summary>
        /// <param name="finalArqEnvio">Final do nome do arquivo de solicitação do serviço.</param>
        /// <param name="finalArqRetorno">Final do nome do arquivo que é para ser gravado o retorno.</param>
        /// <param name="conteudoXMLRetorno">Conteúdo do XML a ser gerado</param>
        /// <param name="pastaGravar">Pasta onde é para ser gravado o XML de Retorno</param>
        /// <example>
        /// // Arquivo de envio: 20080619T19113320-ped-sta.xml
        /// // Arquivo de retorno que vai ser gravado: 20080619T19113320-sta.xml
        /// this.GravarXmlRetorno("-ped-sta.xml", "-sta.xml");
        /// </example>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 25/11/2010
        /// </remarks>        
        public void XmlRetorno(string finalArqEnvio, string finalArqRetorno, string conteudoXMLRetorno, string pastaGravar)
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index; 

            StreamWriter SW = null;

            try
            {
                //Deletar o arquivo XML da pasta de temporários de XML´s com erros se 
                //o mesmo existir
                oAux.DeletarArqXMLErro(Empresa.Configuracoes[emp].PastaErro + "\\" + oAux.ExtrairNomeArq(this.NomeXMLDadosMsg, ".xml") + ".xml");

                //Gravar o arquivo XML de retorno
                string ArqXMLRetorno = pastaGravar + "\\" +
                                       oAux.ExtrairNomeArq(this.NomeXMLDadosMsg, finalArqEnvio) +
                                       finalArqRetorno;
                SW = File.CreateText(ArqXMLRetorno);
                SW.Write(conteudoXMLRetorno);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (SW != null)
                {
                    SW.Close();
                }
            }

            //Gravar o XML de retorno também no formato TXT
            if (Empresa.Configuracoes[emp].GravarRetornoTXTNFe)
            {
                try
                {
                    this.TXTRetorno(finalArqEnvio, finalArqRetorno, conteudoXMLRetorno);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }
        #endregion

        #region GravarRetornoEmTXT()
        //TODO: Documentar este método
        protected void TXTRetorno(string pFinalArqEnvio, string pFinalArqRetorno, string ConteudoXMLRetorno)
        {
            if (ConfiguracaoApp.TipoAplicativo == TipoAplicativo.Cte)
                return;

            int emp = EmpIndex;
            string ConteudoRetorno = string.Empty;

            MemoryStream msXml = Auxiliar.StringXmlToStream(ConteudoXMLRetorno);
            try
            {
                switch (Servico)
                {
                    case Servicos.EnviarLoteNfe:
                        {
                            XmlDocument docRec = new XmlDocument();
                            docRec.Load(msXml);

                            XmlNodeList retEnviNFeList = docRec.GetElementsByTagName("retEnviNFe");
                            if (retEnviNFeList != null) //danasa 23-9-2009
                            {
                                if (retEnviNFeList.Count > 0)   //danasa 23-9-2009
                                {
                                    XmlElement retEnviNFeElemento = (XmlElement)retEnviNFeList.Item(0);
                                    if (retEnviNFeElemento != null)   //danasa 23-9-2009
                                    {
                                        ConteudoRetorno += oAux.LerTag(retEnviNFeElemento, "cStat");
                                        ConteudoRetorno += oAux.LerTag(retEnviNFeElemento, "xMotivo");

                                        XmlNodeList infRecList = retEnviNFeElemento.GetElementsByTagName("infRec");
                                        if (infRecList != null)
                                        {
                                            if (infRecList.Count > 0)   //danasa 23-9-2009
                                            {
                                                XmlElement infRecElemento = (XmlElement)infRecList.Item(0);
                                                if (infRecElemento != null)   //danasa 23-9-2009
                                                {
                                                    ConteudoRetorno += oAux.LerTag(infRecElemento, "nRec");
                                                    ConteudoRetorno += oAux.LerTag(infRecElemento, "dhRecbto");
                                                    ConteudoRetorno += oAux.LerTag(infRecElemento, "tMed");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case Servicos.PedidoSituacaoLoteNFe:
                        {
                            XmlDocument docProRec = new XmlDocument();
                            docProRec.Load(msXml);

                            XmlNodeList retConsReciNFeList = docProRec.GetElementsByTagName("retConsReciNFe");
                            if (retConsReciNFeList != null) //danasa 23-9-2009
                            {
                                if (retConsReciNFeList.Count > 0)   //danasa 23-9-2009
                                {
                                    XmlElement retConsReciNFeElemento = (XmlElement)retConsReciNFeList.Item(0);
                                    if (retConsReciNFeElemento != null)   //danasa 23-9-2009
                                    {
                                        ConteudoRetorno += oAux.LerTag(retConsReciNFeElemento, "nRec");
                                        ConteudoRetorno += oAux.LerTag(retConsReciNFeElemento, "cStat");
                                        ConteudoRetorno += oAux.LerTag(retConsReciNFeElemento, "xMotivo");
                                        ConteudoRetorno += "\r\n";

                                        XmlNodeList protNFeList = retConsReciNFeElemento.GetElementsByTagName("protNFe");
                                        if (protNFeList != null)    //danasa 23-9-2009
                                        {
                                            if (protNFeList.Count > 0)   //danasa 23-9-2009
                                            {
                                                XmlElement protNFeElemento = (XmlElement)protNFeList.Item(0);
                                                if (protNFeElemento != null)
                                                {
                                                    if (protNFeElemento.ChildNodes.Count > 0)
                                                    {
                                                        XmlNodeList infProtList = protNFeElemento.GetElementsByTagName("infProt");

                                                        foreach (XmlNode infProtNode in infProtList)
                                                        {
                                                            XmlElement infProtElemento = (XmlElement)infProtNode;
                                                            string chNFe = oAux.LerTag(infProtElemento, "chNFe");

                                                            ConteudoRetorno += chNFe.Substring(6, 14) + ";";
                                                            ConteudoRetorno += chNFe.Substring(25, 9) + ";";
                                                            ConteudoRetorno += chNFe;
                                                            ConteudoRetorno += oAux.LerTag(infProtElemento, "dhRecbto");
                                                            ConteudoRetorno += oAux.LerTag(infProtElemento, "nProt");
                                                            ConteudoRetorno += oAux.LerTag(infProtElemento, "digVal");
                                                            ConteudoRetorno += oAux.LerTag(infProtElemento, "cStat");
                                                            ConteudoRetorno += oAux.LerTag(infProtElemento, "xMotivo");
                                                            ConteudoRetorno += "\r\n";
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case Servicos.CancelarNFe:  //danasa 19-9-2009
                        {
                            XmlDocument docretCanc = new XmlDocument();
                            docretCanc.Load(msXml);

                            XmlNodeList retCancList = docretCanc.GetElementsByTagName("retCancNFe");
                            if (retCancList != null)
                            {
                                if (retCancList.Count > 0)
                                {
                                    XmlElement retCancElemento = (XmlElement)retCancList.Item(0);
                                    if (retCancElemento != null)
                                    {
                                        if (retCancElemento.ChildNodes.Count > 0)
                                        {
                                            XmlNodeList infCancList = retCancElemento.GetElementsByTagName("infCanc");
                                            if (infCancList != null)
                                            {
                                                foreach (XmlNode infCancNode in infCancList)
                                                {
                                                    XmlElement infCancElemento = (XmlElement)infCancNode;

                                                    ConteudoRetorno += oAux.LerTag(infCancElemento, "tpAmb");
                                                    ConteudoRetorno += oAux.LerTag(infCancElemento, "cStat");
                                                    ConteudoRetorno += oAux.LerTag(infCancElemento, "xMotivo");
                                                    ConteudoRetorno += "\r\n";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case Servicos.InutilizarNumerosNFe: //danasa 19-9-2009
                        {
                            XmlDocument docretInut = new XmlDocument();
                            docretInut.Load(msXml);

                            XmlNodeList retInutList = docretInut.GetElementsByTagName("retInutNFe");
                            if (retInutList != null)
                            {
                                if (retInutList.Count > 0)
                                {
                                    XmlElement retInutElemento = (XmlElement)retInutList.Item(0);
                                    if (retInutElemento != null)
                                    {
                                        if (retInutElemento.ChildNodes.Count > 0)
                                        {
                                            XmlNodeList infInutList = retInutElemento.GetElementsByTagName("infInut");
                                            if (infInutList != null)
                                            {
                                                foreach (XmlNode infInutNode in infInutList)
                                                {
                                                    XmlElement infInutElemento = (XmlElement)infInutNode;

                                                    ConteudoRetorno += oAux.LerTag(infInutElemento, "tpAmb");
                                                    ConteudoRetorno += oAux.LerTag(infInutElemento, "cStat");
                                                    ConteudoRetorno += oAux.LerTag(infInutElemento, "xMotivo");
                                                    ConteudoRetorno += oAux.LerTag(infInutElemento, "cUF");
                                                    ConteudoRetorno += "\r\n";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case Servicos.PedidoConsultaSituacaoNFe:   //danasa 19-9-2009
                        {
                            XmlDocument docretConsSit = new XmlDocument();
                            docretConsSit.Load(msXml);

                            XmlNodeList retConsSitList = docretConsSit.GetElementsByTagName("retConsSitNFe");
                            if (retConsSitList != null)
                            {
                                if (retConsSitList.Count > 0)
                                {
                                    XmlElement retConsSitElemento = (XmlElement)retConsSitList.Item(0);
                                    if (retConsSitElemento != null)
                                    {
                                        if (retConsSitElemento.ChildNodes.Count > 0)
                                        {
                                            XmlNodeList infConsSitList = retConsSitElemento.GetElementsByTagName("infProt");
                                            if (infConsSitList != null)
                                            {
                                                foreach (XmlNode infConsSitNode in infConsSitList)
                                                {
                                                    XmlElement infConsSitElemento = (XmlElement)infConsSitNode;

                                                    ConteudoRetorno += oAux.LerTag(infConsSitElemento, "tpAmb");
                                                    ConteudoRetorno += oAux.LerTag(infConsSitElemento, "cStat");
                                                    ConteudoRetorno += oAux.LerTag(infConsSitElemento, "xMotivo");
                                                    ConteudoRetorno += oAux.LerTag(infConsSitElemento, "cUF");
                                                    ConteudoRetorno += "\r\n";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case Servicos.PedidoConsultaStatusServicoNFe:   //danasa 19-9-2009
                        {
                            XmlDocument docConsStat = new XmlDocument();
                            docConsStat.Load(msXml);

                            XmlNodeList retConsStatServList = docConsStat.GetElementsByTagName("retConsStatServ");
                            if (retConsStatServList != null)
                            {
                                if (retConsStatServList.Count > 0)
                                {
                                    XmlElement retConsStatServElemento = (XmlElement)retConsStatServList.Item(0);
                                    if (retConsStatServElemento != null)
                                    {
                                        ConteudoRetorno += oAux.LerTag(retConsStatServElemento, "tpAmb");
                                        ConteudoRetorno += oAux.LerTag(retConsStatServElemento, "cStat");
                                        ConteudoRetorno += oAux.LerTag(retConsStatServElemento, "xMotivo");
                                        ConteudoRetorno += oAux.LerTag(retConsStatServElemento, "cUF");
                                        ConteudoRetorno += oAux.LerTag(retConsStatServElemento, "dhRecbto");
                                        ConteudoRetorno += oAux.LerTag(retConsStatServElemento, "tMed");
                                        ConteudoRetorno += "\r\n";
                                    }
                                }
                            }
                        }
                        break;

                    case Servicos.ConsultaCadastroContribuinte: //danasa 19-9-2009
                        {
                            ///
                            /// Retorna o texto conforme o manual do Sefaz versao 3.0
                            /// 
                            RetConsCad rconscad = ProcessaConsultaCadastro(msXml);
                            if (rconscad != null)
                            {
                                ConteudoRetorno = rconscad.cStat.ToString("000") + ";";
                                ConteudoRetorno += rconscad.xMotivo.Replace(";", " ") + ";";
                                ConteudoRetorno += rconscad.UF + ";";
                                ConteudoRetorno += rconscad.IE + ";";
                                ConteudoRetorno += rconscad.CNPJ + ";";
                                ConteudoRetorno += rconscad.CPF + ";";
                                ConteudoRetorno += rconscad.dhCons + ";";
                                ConteudoRetorno += rconscad.cUF.ToString("00") + ";";
                                ConteudoRetorno += "\r\r";
                                foreach (infCad infCadNode in rconscad.infCad)
                                {
                                    ConteudoRetorno += infCadNode.IE + ";";
                                    ConteudoRetorno += infCadNode.CNPJ + ";";
                                    ConteudoRetorno += infCadNode.CPF + ";";
                                    ConteudoRetorno += infCadNode.UF + ";";
                                    ConteudoRetorno += infCadNode.cSit + ";";
                                    ConteudoRetorno += infCadNode.xNome.Replace(";", " ") + ";";
                                    ConteudoRetorno += infCadNode.xFant.Replace(";", " ") + ";";
                                    ConteudoRetorno += infCadNode.xRegApur.Replace(";", " ") + ";";
                                    ConteudoRetorno += infCadNode.CNAE.ToString() + ";";
                                    ConteudoRetorno += infCadNode.dIniAtiv + ";";
                                    ConteudoRetorno += infCadNode.dUltSit + ";";
                                    ConteudoRetorno += infCadNode.IEUnica.Replace(";", " ") + ";";
                                    ConteudoRetorno += infCadNode.IEAtual.Replace(";", " ") + ";";
                                    ConteudoRetorno += infCadNode.ender.xLgr.Replace(";", " ") + ";";
                                    ConteudoRetorno += infCadNode.ender.nro.Replace(";", " ") + ";";
                                    ConteudoRetorno += infCadNode.ender.xCpl.Replace(";", " ") + ";";
                                    ConteudoRetorno += infCadNode.ender.xBairro.Replace(";", " ") + ";";
                                    ConteudoRetorno += infCadNode.ender.cMun.ToString("0000000") + ";";
                                    ConteudoRetorno += infCadNode.ender.xMun.Replace(";", " ") + ";";
                                    ConteudoRetorno += infCadNode.ender.CEP.ToString("00000000") + ";";
                                    ConteudoRetorno += "\r\r";
                                }
                            }
                        }
                        break;

                    case Servicos.ConsultaInformacoesUniNFe:
                        break;
                }
                //Gravar o TXT de retorno para o ERP
                if (!string.IsNullOrEmpty(ConteudoRetorno))
                {
                    string TXTRetorno = string.Empty;
                    TXTRetorno = oAux.ExtrairNomeArq(this.NomeXMLDadosMsg, pFinalArqEnvio) + pFinalArqRetorno;
                    TXTRetorno = Empresa.Configuracoes[emp].PastaRetorno + "\\" + oAux.ExtrairNomeArq(TXTRetorno, ".xml") + ".txt";

                    File.WriteAllText(TXTRetorno, ConteudoRetorno, Encoding.Default);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #endregion

        #region Métodos para gerar os XML´s de distribuição

        #region XMLDistInut()
        /// <summary>
        /// Criar o arquivo XML de distribuição das Inutilizações de Números de NFe´s com o protocolo de autorização anexado
        /// </summary>
        /// <param name="strArqInut">Nome arquivo XML de Inutilização</param>
        /// <param name="strProtNfe">String contendo a parte do XML do protocolo a ser anexado</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>21/04/2009</date>
        public void XmlDistInut(string strArqInut, string strRetInutNFe)
        {
            string tipo = string.Empty;
            switch (ConfiguracaoApp.TipoAplicativo)
            {
                case TipoAplicativo.Cte:
                    tipo = "CT";
                    break;

                case TipoAplicativo.Nfe:
                    tipo = "NF";
                    break;

                default:
                    break;
            }

            int emp = EmpIndex;
            StreamWriter swProc = null;

            try
            {
                //Separar as tag´s da NFe que interessa <NFe> até </NFe>
                XmlDocument doc = new XmlDocument();

                doc.Load(strArqInut);

                XmlNodeList InutNFeList = doc.GetElementsByTagName("inut" + tipo + "e");
                XmlNode InutNFeNode = InutNFeList[0];
                string strInutNFe = InutNFeNode.OuterXml;

                //Montar o XML -procCancNFe.xml
                string strXmlProcInutNfe = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
                    "<procInut" + tipo + "e xmlns=\"" + ConfiguracaoApp.nsURI + "\" versao=\"" + ConfiguracaoApp.VersaoXMLInut + "\">" +
                    strInutNFe +
                    strRetInutNFe +
                    "</procInut" + tipo + "e>";

                //Montar o nome do arquivo -proc-NFe.xml
                string strNomeArqProcInutNFe = Empresa.Configuracoes[emp].PastaEnviado + "\\" +
                                               PastaEnviados.EmProcessamento.ToString() + "\\" +
                                               oAux.ExtrairNomeArq(strArqInut, ExtXml.PedInu/*"-ped-inu.xml"*/) +
                                               ExtXmlRet.ProcInutNFe;// "-procInutNFe.xml";

                //Gravar o XML em uma linha só (sem quebrar as tag's linha a linha) ou dá erro na hora de validar o XML pelos Schemas. Wandrey 13/05/2009
                swProc = File.CreateText(strNomeArqProcInutNFe);
                swProc.Write(strXmlProcInutNfe);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (swProc != null)
                {
                    swProc.Close();
                }
            }

        }
        #endregion

        #region XMLDistCanc()
        /// <summary>
        /// Criar o arquivo XML de distribuição dos Cancelamentos com o protocolo de autorização anexado
        /// </summary>
        /// <param name="strArqCanc">Nome arquivo XML de Cancelamento</param>
        /// <param name="strProtNfe">String contendo a parte do XML do protocolo a ser anexado</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>21/04/2009</date>
        public void XmlDistCanc(string strArqCanc, string strRetCancNFe)
        {
            string tipo = string.Empty;
            switch (ConfiguracaoApp.TipoAplicativo)
            {
                case TipoAplicativo.Cte:
                    tipo = "CT";
                    break;

                case TipoAplicativo.Nfe:
                    tipo = "NF";
                    break;

                default:
                    break;
            }

            int emp = EmpIndex;
            StreamWriter swProc = null;

            try
            {
                //Separar as tag´s da NFe que interessa <NFe> até </NFe>
                XmlDocument doc = new XmlDocument();

                doc.Load(strArqCanc);

                XmlNodeList CancNFeList = doc.GetElementsByTagName("canc" + tipo + "e");
                XmlNode CancNFeNode = CancNFeList[0];
                string strCancNFe = CancNFeNode.OuterXml;

                //Montar o XML -procCancNFe.xml
                string strXmlProcCancNfe = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
                    "<procCanc" + tipo + "e xmlns=\"" + ConfiguracaoApp.nsURI + "\" versao=\"" + ConfiguracaoApp.VersaoXMLCanc + "\">" +
                    strCancNFe +
                    strRetCancNFe +
                    "</procCanc" + tipo + "e>";

                //Montar o nome do arquivo -proc-NFe.xml
                string strNomeArqProcCancNFe = Empresa.Configuracoes[emp].PastaEnviado + "\\" +
                                                PastaEnviados.EmProcessamento.ToString() + "\\" +
                                                oAux.ExtrairNomeArq(strArqCanc, ExtXml.PedCan/*"-ped-can.xml"*/) +
                                                ExtXmlRet.ProcCancNFe;// "-procCancNFe.xml";

                //Gravar o XML em uma linha só (sem quebrar as tag's linha a linha) ou dá erro na hora de validar o XML pelos Schemas. Wandrey 13/05/2009
                swProc = File.CreateText(strNomeArqProcCancNFe);
                swProc.Write(strXmlProcCancNfe);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (swProc != null)
                {
                    swProc.Close();
                }
            }
        }
        #endregion

        #region XmlPedRec()
        /// <summary>
        /// Gera o XML de pedido de analise do recibo do lote
        /// </summary>
        /// <param name="strRecibo">Número do recibo a ser consultado o lote</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>21/04/2009</date>
        public void XmlPedRec(string strRecibo)
        {
            int emp = EmpIndex;
            string strNomeArqPedRec = Empresa.Configuracoes[emp].PastaEnvio + "\\" + strRecibo + ExtXml.PedRec;
            if (!File.Exists(strNomeArqPedRec))
            {
                string tipo = string.Empty;
                switch (ConfiguracaoApp.TipoAplicativo)
                {
                    case TipoAplicativo.Cte:
                        tipo = "CT";
                        break;

                    case TipoAplicativo.Nfe:
                        tipo = "NF";
                        break;

                    default:
                        break;
                }

                string strXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                    "<consReci" + tipo + "e versao=\"" + ConfiguracaoApp.VersaoXMLPedRec + "\" xmlns=\"" + ConfiguracaoApp.nsURI + "\">" +
                    "<tpAmb>" + Empresa.Configuracoes[emp].tpAmb.ToString() + "</tpAmb>" +
                    "<nRec>" + strRecibo + "</nRec>" +
                    "</consReci" + tipo + "e>";

                //Gravar o XML
                GravarArquivoParaEnvio(strNomeArqPedRec, strXml);
            }
        }
        #endregion

        #region XMLDistNFe()
        /// <summary>
        /// Criar o arquivo XML de distribuição das NFE com o protocolo de autorização anexado
        /// </summary>
        /// <param name="strArqNFe">Nome arquivo XML da NFe</param>
        /// <param name="strProtNfe">String contendo a parte do XML do protocolo a ser anexado</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>20/04/2009</date>
        public void XmlDistNFe(string strArqNFe, string strProtNfe)
        {
            int emp = EmpIndex;
            StreamWriter swProc = null;

            try
            {
                if (File.Exists(strArqNFe))
                {
                    string tipo = string.Empty;
                    switch (ConfiguracaoApp.TipoAplicativo)
                    {
                        case TipoAplicativo.Cte:
                            tipo = "ct";
                            break;

                        case TipoAplicativo.Nfe:
                            tipo = "nf";
                            break;

                        default:
                            break;
                    }

                    //Separar as tag´s da NFe que interessa <NFe> até </NFe>
                    XmlDocument doc = new XmlDocument();

                    doc.Load(strArqNFe);

                    XmlNodeList NFeList = doc.GetElementsByTagName(tipo.ToUpper() + "e");
                    XmlNode NFeNode = NFeList[0];
                    string strNFe = NFeNode.OuterXml;

                    //Montar a string contendo o XML -proc-NFe.xml
                    string strXmlProcNfe = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
                        "<" + tipo + "eProc xmlns=\"" + ConfiguracaoApp.nsURI + "\" versao=\"" + ConfiguracaoApp.VersaoXMLNFe + "\">" +
                        strNFe +
                        strProtNfe +
                        "</" + tipo + "eProc>";

                    //Montar o nome do arquivo -proc-NFe.xml
                    string strNomeArqProcNFe = Empresa.Configuracoes[emp].PastaEnviado + "\\" +
                                                PastaEnviados.EmProcessamento.ToString() + "\\" +
                                                oAux.ExtrairNomeArq(strArqNFe, ExtXml.Nfe) +
                                                ExtXmlRet.ProcNFe;

                    //Gravar o XML em uma linha só (sem quebrar as tag´s linha a linha) ou dá erro na hora de 
                    //validar o XML pelos Schemas. Wandrey 13/05/2009
                    swProc = File.CreateText(strNomeArqProcNFe);
                    swProc.Write(strXmlProcNfe);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (swProc != null)
                {
                    swProc.Close();
                }
            }
        }
        #endregion

        #endregion

        #region Métodos auxiliares

        #region LerXMLNfe()
        /// <summary>
        /// Le o conteudo do XML da NFe
        /// </summary>
        /// <param name="Arquivo">Arquivo do XML da NFe</param>
        /// <returns>Dados do XML da NFe</returns>
        private LerXML.DadosNFeClass LerXMLNFe(string Arquivo)
        {
            LerXML oLerXML = new LerXML();

            try
            {
                oLerXML.Nfe(Arquivo);
            }
            catch (XmlException ex)
            {
                throw (ex);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return oLerXML.oDadosNfe;
        }
        #endregion

        #region NomeArqLoteRetERP()
        protected string NomeArqLoteRetERP(string NomeArquivoXML)
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index; 

            return Empresa.Configuracoes[emp].PastaRetorno + "\\" +
                oAux.ExtrairNomeArq(NomeArquivoXML, ExtXml.Nfe) +
                "-num-lot.xml";
        }
        #endregion

        #region GravarArquivoParaEnvio
        /// <summary>
        /// grava um arquivo na pasta de envio
        /// </summary>
        /// <param name="Arquivo"></param>
        /// <param name="Conteudo"></param>
        protected void GravarArquivoParaEnvio(string Arquivo, string Conteudo)
        {
            try
            {
                //Gravar o XML
                MemoryStream oMemoryStream = Auxiliar.StringXmlToStream(Conteudo);
                XmlDocument docProc = new XmlDocument();
                docProc.Load(oMemoryStream);
                docProc.Save(Empresa.Configuracoes[EmpIndex].PastaEnvio + "\\" + Path.GetFileName(Arquivo));
            }
            catch (XmlException ex)
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

        #endregion

        #region ProcessaConsultaCadastro()
        /// <summary>
        /// utilizada pela GerarXML
        /// </summary>
        /// <param name="msXml"></param>
        /// <returns></returns>
        public RetConsCad ProcessaConsultaCadastro(XmlDocument doc)
        {
            if (doc.GetElementsByTagName("infCad") == null)
                return null;

            RetConsCad vRetorno = new RetConsCad();

            foreach (XmlNode node in doc.ChildNodes)
            {
                if (node.Name == "retConsCad")
                {
                    foreach (XmlNode noderetConsCad in node.ChildNodes)
                    {
                        if (noderetConsCad.Name == "infCons")
                        {
                            foreach (XmlNode nodeinfCons in noderetConsCad.ChildNodes)
                            {
                                if (nodeinfCons.Name == "infCad")
                                {
                                    vRetorno.infCad.Add(new infCad());
                                    vRetorno.infCad[vRetorno.infCad.Count - 1].CNPJ = vRetorno.CNPJ;
                                    vRetorno.infCad[vRetorno.infCad.Count - 1].CPF = vRetorno.CPF;
                                    vRetorno.infCad[vRetorno.infCad.Count - 1].IE = vRetorno.IE;
                                    vRetorno.infCad[vRetorno.infCad.Count - 1].UF = vRetorno.UF;

                                    foreach (XmlNode nodeinfCad in nodeinfCons.ChildNodes)
                                    {
                                        switch (nodeinfCad.Name)
                                        {
                                            case "UF":
                                                vRetorno.infCad[vRetorno.infCad.Count - 1].UF = nodeinfCad.InnerText;
                                                break;
                                            case "IE":
                                                vRetorno.infCad[vRetorno.infCad.Count - 1].IE = nodeinfCad.InnerText;
                                                break;
                                            case "CNPJ":
                                                vRetorno.infCad[vRetorno.infCad.Count - 1].CNPJ = nodeinfCad.InnerText;
                                                break;
                                            case "CPF":
                                                vRetorno.infCad[vRetorno.infCad.Count - 1].CNPJ = nodeinfCad.InnerText;
                                                break;
                                            case "xNome":
                                                vRetorno.infCad[vRetorno.infCad.Count - 1].xNome = nodeinfCad.InnerText;
                                                break;
                                            case "xFant":
                                                vRetorno.infCad[vRetorno.infCad.Count - 1].xFant = nodeinfCad.InnerText;
                                                break;

                                            case "ender":
                                                foreach (XmlNode nodeinfConsEnder in nodeinfCad.ChildNodes)
                                                {
                                                    switch (nodeinfConsEnder.Name)
                                                    {
                                                        case "xLgr":
                                                            vRetorno.infCad[vRetorno.infCad.Count - 1].ender.xLgr = nodeinfConsEnder.InnerText;
                                                            break;
                                                        case "nro":
                                                            vRetorno.infCad[vRetorno.infCad.Count - 1].ender.nro = nodeinfConsEnder.InnerText;
                                                            break;
                                                        case "xCpl":
                                                            vRetorno.infCad[vRetorno.infCad.Count - 1].ender.xCpl = nodeinfConsEnder.InnerText;
                                                            break;
                                                        case "xBairro":
                                                            vRetorno.infCad[vRetorno.infCad.Count - 1].ender.xBairro = nodeinfConsEnder.InnerText;
                                                            break;
                                                        case "xMun":
                                                            vRetorno.infCad[vRetorno.infCad.Count - 1].ender.xMun = nodeinfConsEnder.InnerText;
                                                            break;
                                                        case "cMun":
                                                            vRetorno.infCad[vRetorno.infCad.Count - 1].ender.cMun = Convert.ToInt32("0" + nodeinfConsEnder.InnerText);
                                                            break;
                                                        case "CEP":
                                                            vRetorno.infCad[vRetorno.infCad.Count - 1].ender.CEP = Convert.ToInt32("0" + nodeinfConsEnder.InnerText);
                                                            break;
                                                    }
                                                }
                                                break;

                                            case "IEAtual":
                                                vRetorno.infCad[vRetorno.infCad.Count - 1].IEAtual = nodeinfCad.InnerText;
                                                break;
                                            case "IEUnica":
                                                vRetorno.infCad[vRetorno.infCad.Count - 1].IEUnica = nodeinfCad.InnerText;
                                                break;
                                            case "dBaixa":
                                                vRetorno.infCad[vRetorno.infCad.Count - 1].dBaixa = Auxiliar.getDateTime(nodeinfCad.InnerText);
                                                break;
                                            case "dUltSit":
                                                vRetorno.infCad[vRetorno.infCad.Count - 1].dUltSit = Auxiliar.getDateTime(nodeinfCad.InnerText);
                                                break;
                                            case "dIniAtiv":
                                                vRetorno.infCad[vRetorno.infCad.Count - 1].dIniAtiv = Auxiliar.getDateTime(nodeinfCad.InnerText);
                                                break;
                                            case "CNAE":
                                                vRetorno.infCad[vRetorno.infCad.Count - 1].CNAE = Convert.ToInt32("0" + nodeinfCad.InnerText);
                                                break;
                                            case "xRegApur":
                                                vRetorno.infCad[vRetorno.infCad.Count - 1].xRegApur = nodeinfCad.InnerText;
                                                break;
                                            case "cSit":
                                                if (nodeinfCad.InnerText == "0")
                                                    vRetorno.infCad[vRetorno.infCad.Count - 1].cSit = "Contribuinte não habilitado";
                                                else if (nodeinfCad.InnerText == "1")
                                                    vRetorno.infCad[vRetorno.infCad.Count - 1].cSit = "Contribuinte habilitado";
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    switch (nodeinfCons.Name)
                                    {
                                        case "cStat":
                                            vRetorno.cStat = Convert.ToInt32("0" + nodeinfCons.InnerText);
                                            break;
                                        case "xMotivo":
                                            vRetorno.xMotivo = nodeinfCons.InnerText;
                                            break;
                                        case "UF":
                                            vRetorno.UF = nodeinfCons.InnerText;
                                            break;
                                        case "IE":
                                            vRetorno.IE = nodeinfCons.InnerText;
                                            break;
                                        case "CNPJ":
                                            vRetorno.CNPJ = nodeinfCons.InnerText;
                                            break;
                                        case "CPF":
                                            vRetorno.CPF = nodeinfCons.InnerText;
                                            break;
                                        case "dhCons":
                                            vRetorno.dhCons = Auxiliar.getDateTime(nodeinfCons.InnerText);
                                            break;
                                        case "cUF":
                                            vRetorno.cUF = Convert.ToInt32("0" + nodeinfCons.InnerText);
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return vRetorno;
        }
        #endregion

        #region ProcessaConsultaCadastro()
        public RetConsCad ProcessaConsultaCadastro(MemoryStream msXml)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(msXml);
            return ProcessaConsultaCadastro(doc);
        }
        #endregion

        #region ProcessaConsultaCadastro()
        /// <summary>
        /// Função Callback que analisa a resposta do Status do Servido
        /// </summary>
        /// <param name="elem"></param>
        /// <by>Marcos Diez</by>
        /// <date>30/8/2009</date>
        /// <returns></returns>
        /// <summary>
        /// utilizada internamente pela VerConsultaCadastroClass
        /// </summary>
        /// <param name="cArquivoXML"></param>
        /// <returns></returns>
        public RetConsCad ProcessaConsultaCadastro(string cArquivoXML)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(cArquivoXML);
            return ProcessaConsultaCadastro(doc);
        }
        #endregion
    }
}
