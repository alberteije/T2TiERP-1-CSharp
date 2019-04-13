using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading;

namespace UniNFeLibrary
{
    #region Classe FluxoNfe
    /// <summary>
    /// Classe de controle do fluxo das notas fiscais eletrônicas que estão em processo de envio
    /// </summary>
    public class FluxoNfe
    {
        #region Construtores
        public FluxoNfe()
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index;
            NomeXmlControleFluxo = Empresa.Configuracoes[emp].PastaEmpresa + "\\fluxonfe.xml";
        }
        #endregion

        #region Enumeradores

        #region ElementoEditavel
        /// <summary>
        /// Enumerador das tag´s editáveis do XML de controle do fluxo
        /// </summary>
        public enum ElementoEditavel
        {
            /// <summary>
            /// Tag que contém o número do lote
            /// </summary>
            idLote,
            /// <summary>
            /// Tag que contém o número do Recibo
            /// </summary>
            nRec,
            /// <summary>
            /// Tag que contém o status da NFe
            /// </summary>
            cStat,
            /// <summary>
            /// Data e hora da ultima consulta do recibo do lote de nfe enviado
            /// </summary>
            dPedRec,
            tMed
        }
        #endregion

        #region ElementoFixo
        /// <summary>
        /// Enumerador das tag´s e atributos fixos do XML de controle do fluxo
        /// </summary>
        public enum ElementoFixo
        {
            /// <summary>
            /// Tag principal Documentos NFe
            /// </summary>
            DocumentosNFe,
            /// <summary>
            /// Tag Documento - Uma para cada NFe em processamento
            /// </summary>
            Documento,
            /// <summary>
            /// Tag com a ChaveNFe
            /// </summary>
            ChaveNFe,
            /// <summary>
            /// Tag com o nome do arquivo NFe
            /// </summary>
            ArqNFe
        }
        #endregion

        #endregion

        #region Propriedades
        /// <summary>
        /// Nome do arquivo XML onde é gravado o controle do fluxo
        /// </summary>
        private string NomeXmlControleFluxo { get; set; }
        #endregion

        #region métodos gerais

        #region CriarXml()
        public void CriarXml()
        {
            try
            {
                this.CriarXml(false);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region CriarXml()
        /// <summary>
        /// Cria o arquivo XML para o controle do fluxo
        /// </summary>
        /// <param name="forcar">Força criar o arquivo mesmo que já exista</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>17/04/2009</date>
        public void CriarXml(bool VerificaEstruturaXml)
        {
            XmlWriter xtw = null; // criar instância para xmltextwriter. 
            try
            {
                #region Testar para ver se o XML não tá danificado, ou seja, sem as tag´s iniciais, se tiver força recriar ele
                bool ForcarCriar = false;
                if (VerificaEstruturaXml)
                {
                    XmlDocument doc = null;
                    FileStream fsArquivo = null;
                    try
                    {
                        fsArquivo = new FileStream(NomeXmlControleFluxo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite); //Abrir um arquivo XML usando FileStream

                        if (File.Exists(NomeXmlControleFluxo))
                        {
                            doc = new XmlDocument();
                            doc.Load(NomeXmlControleFluxo);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (doc != null)
                        {
                            if (doc.DocumentElement == null)
                            {
                                ForcarCriar = true;
                            }
                        }
                    }
                    finally
                    {
                        if (fsArquivo != null)
                        {
                            fsArquivo.Close();
                        }
                    }
                }
                #endregion

                if (!File.Exists(NomeXmlControleFluxo) || ForcarCriar)
                {
                    ///
                    /// danasa 20-9-2010
                    /// 
                    bool goCriaArquivoDeFluxo = true;
                    if (File.Exists(NomeXmlControleFluxo))
                        if (Auxiliar.FileInUse(NomeXmlControleFluxo))
                            ///
                            /// O metodo "BuscarXML" acessa o metodo para criar o xml de fluxo, só que como ele é acessado várias vezes
                            /// e como o arquivo está sendo criado, é exibida várias mensagens de erro de acesso ao arquivo de fluxo
                            goCriaArquivoDeFluxo = false;

                    if (goCriaArquivoDeFluxo)
                    {
                        XmlWriterSettings oSettings = new XmlWriterSettings();
                        UTF8Encoding c = new UTF8Encoding(false);

                        oSettings.Encoding = c;
                        oSettings.Indent = true;
                        oSettings.IndentChars = "";
                        oSettings.NewLineOnAttributes = false;
                        oSettings.OmitXmlDeclaration = false;

                        xtw = XmlWriter.Create(NomeXmlControleFluxo, oSettings); //atribuir arquivo, caminho e codificação 
                        xtw.WriteStartDocument(); //comaçar a escrever o documento 
                        xtw.WriteStartElement(ElementoFixo.DocumentosNFe.ToString()); //Criar elemento raiz
                        xtw.WriteEndElement(); //encerrar tag DocumentosNFe
                        xtw.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (xtw != null)
                {
                    if (xtw.WriteState != WriteState.Closed)
                    {
                        xtw.Close(); //Fechar o arquivo e salvar
                    }
                }
            }
        }
        #endregion

        #region InserirNfeFluxo()
        /// <summary>
        /// Insere a NFe no fluxo em processo
        /// </summary>
        /// <param name="strChaveNFe">Chave da NFe</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>17/04/2009</date>
        public void InserirNfeFluxo(string strChaveNFe, string strNomeArqNFe)
        {
            if (!this.NfeExiste(strChaveNFe))
            {
                DateTime startTime;
                DateTime stopTime;
                TimeSpan elapsedTime;

                long elapsedMillieconds;
                startTime = DateTime.Now;

                while (true)
                {
                    stopTime = DateTime.Now;
                    elapsedTime = stopTime.Subtract(startTime);
                    elapsedMillieconds = (int)elapsedTime.TotalMilliseconds;

                    FileStream lfile = null;
                    try
                    {
                        XmlDocument xd = new XmlDocument(); //Criar instância do XmlDocument Class

                        lfile = new FileStream(NomeXmlControleFluxo, FileMode.Open, FileAccess.ReadWrite, FileShare.Read); //Abrir um arquivo XML usando FileStream

                        xd.Load(lfile); //Carregar o arquivo aberto no XmlDocument
                        XmlElement cl = xd.CreateElement(ElementoFixo.Documento.ToString()); //Criar um Elemento chamado Documento
                        cl.SetAttribute(ElementoFixo.ChaveNFe.ToString(), strChaveNFe); // Setar atributo para o Elemento Documento

                        //Tag ArqNFeAssinado
                        this.CriarTag(xd, cl, ElementoFixo.ArqNFe.ToString(), strNomeArqNFe);

                        //Tag idLote
                        this.CriarTag(xd, cl, ElementoEditavel.idLote.ToString(), string.Empty);

                        //Tag nRec
                        this.CriarTag(xd, cl, ElementoEditavel.nRec.ToString(), string.Empty);

                        //Tag cStat
                        this.CriarTag(xd, cl, ElementoEditavel.cStat.ToString(), string.Empty);

                        //Tag tMed
                        this.CriarTag(xd, cl, ElementoEditavel.tMed.ToString(), string.Empty);

                        //Tag dPedRec
                        this.CriarTag(xd, cl, ElementoEditavel.dPedRec.ToString(), DateTime.Now.ToString());

                        //Fechar o arquovo e gravar o conteúdo no HD
                        lfile.Close(); //Fechar o FileStream
                        xd.Save(NomeXmlControleFluxo); //Salvar o conteudo do XmlDocument para o arquivo  

                        break;
                    }
                    catch (Exception ex)
                    {
                        if (lfile != null)
                        {
                            lfile.Close();
                        }

                        if (elapsedMillieconds >= 120000) //120.000 ms que corresponde á 120 segundos que corresponde a 2 minuto
                        {
                            throw (ex);
                        }
                    }

                    Thread.Sleep(200);
                }
            }
        }
        #endregion

        #region CriarTag()
        /// <summary>
        /// Criar Tag no XML de fluxo
        /// </summary>
        /// <param name="xd">Objeto XmlDocument</param>
        /// <param name="cl">Objeto XmlElement</param>
        /// <param name="strTag">Nome da Tag</param>
        /// <param name="strConteudo">Conteúdo da Tag</param>
        private void CriarTag(XmlDocument xd, XmlElement cl, string strTag, string strConteudo)
        {
            try
            {
                XmlElement na = xd.CreateElement(strTag);
                XmlText natext = xd.CreateTextNode(strConteudo);
                na.AppendChild(natext); //Gravar o texto da unidade para o nó Unidade
                cl.AppendChild(na); //Gravar nó Unidade para o elemento Produto
                xd.DocumentElement.AppendChild(cl); //Gravar o elemento raiz para o XmlDocument
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region ExcluirNfeFluxo()
        /// <summary>
        /// Excluir a NFe do fluxo em processamento através da chave da NFe
        /// </summary>
        /// <param name="strChaveNFe">Chave da NFe</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>17/04/2009</date>
        public void ExcluirNfeFluxo(string strChaveNFe)
        {
            DateTime startTime;
            DateTime stopTime;
            TimeSpan elapsedTime;

            long elapsedMillieconds;
            startTime = DateTime.Now;

            while (true)
            {
                stopTime = DateTime.Now;
                elapsedTime = stopTime.Subtract(startTime);
                elapsedMillieconds = (int)elapsedTime.TotalMilliseconds;
                FileStream fsArquivo = null;

                try
                {
                    XmlDocument xdXml = new XmlDocument(); //Criar instância do XmlDocument Class
                    fsArquivo = new FileStream(NomeXmlControleFluxo, FileMode.Open, FileAccess.ReadWrite, FileShare.Read); //Abrir um arquivo XML usando FileStream
                    xdXml.Load(fsArquivo); //Carregar o arquivo aberto no XmlDocument

                    XmlNodeList list = xdXml.GetElementsByTagName(ElementoFixo.Documento.ToString()); //Pesquisar o elemento Documento no arquivo XML
                    for (int i = 0; i < list.Count; i++) //Navegar em todos os elementos do nó Documento
                    {
                        XmlElement cl = (XmlElement)xdXml.GetElementsByTagName(ElementoFixo.Documento.ToString())[i]; //Recuperar o conteúdo da tag Documento
                        if (cl.GetAttribute(ElementoFixo.ChaveNFe.ToString()) == strChaveNFe)
                        {
                            xdXml.DocumentElement.RemoveChild(cl); //Remove o elemento do documento
                        }
                    }
                    fsArquivo.Close(); //Fecha o arquivo XML
                    xdXml.Save(NomeXmlControleFluxo); //Grava o arquivo XML                
                    break;

                }
                catch (Exception ex)
                {
                    if (fsArquivo != null)
                    {
                        fsArquivo.Close();
                    }

                    if (elapsedMillieconds >= 120000) //120.000 ms que corresponde á 120 segundos que corresponde a 2 minuto
                    {
                        throw (ex);
                    }
                }

                Thread.Sleep(200);
            }
        }
        #endregion

        #region ExcluirNfeFluxoRec()
        /// <summary>
        /// Excluir as NFe´s no fluxo através do recibo. Ótimo para retirar todas as notas de um único lote de uma única vez.
        /// </summary>
        /// <param name="nRec">Número do recibo do lote enviado</param>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Date: 20/07/2010
        /// </remarks>
        public void ExcluirNfeFluxoRec(string nRec)
        {
            DateTime startTime;
            DateTime stopTime;
            TimeSpan elapsedTime;

            long elapsedMillieconds;
            startTime = DateTime.Now;

            while (true)
            {
                stopTime = DateTime.Now;
                elapsedTime = stopTime.Subtract(startTime);
                elapsedMillieconds = (int)elapsedTime.TotalMilliseconds;
                FileStream fsArquivo = null;

                try
                {
                    XmlDocument xdXml = new XmlDocument(); //Criar instância do XmlDocument Class
                    fsArquivo = new FileStream(NomeXmlControleFluxo, FileMode.Open, FileAccess.ReadWrite, FileShare.Read); //Abrir um arquivo XML usando FileStream
                    xdXml.Load(fsArquivo); //Carregar o arquivo aberto no XmlDocument

                    XmlNodeList documentosList = xdXml.GetElementsByTagName(ElementoFixo.DocumentosNFe.ToString()); //Pesquisar o elemento Documento no arquivo XML
                    foreach (XmlNode documentosNode in documentosList)
                    {
                        XmlElement documentosElemento = (XmlElement)documentosNode;

                        List<XmlNode> nodeExcluir = new List<XmlNode>();

                        XmlNodeList documentoList = documentosElemento.GetElementsByTagName(ElementoFixo.Documento.ToString());
                        for (int i = 0; i < documentoList.Count; i++)
                        {
                            var documentoNode = documentoList[i];
                            var documentoElemento = (XmlElement)documentoNode;
                            var tagRec = documentoElemento.GetElementsByTagName(ElementoEditavel.nRec.ToString())[0].InnerText.Trim(); //Recupera o conteúdo da tag de nRec
                            if (tagRec == nRec)
                            {
                                nodeExcluir.Add(documentoNode);
                            }
                        }

                        for (int i = 0; i < nodeExcluir.Count; i++)
                        {
                            xdXml.DocumentElement.RemoveChild(nodeExcluir[i]);
                        }
                    }

                    fsArquivo.Close(); //Fecha o arquivo XML
                    xdXml.Save(NomeXmlControleFluxo); //Grava o arquivo XML                
                    break;
                }
                catch (Exception ex)
                {
                    if (fsArquivo != null)
                    {
                        fsArquivo.Close();
                    }

                    if (elapsedMillieconds >= 120000) //120.000 ms que corresponde á 120 segundos que corresponde a 2 minuto
                    {
                        throw (ex);
                    }
                }

                Thread.Sleep(200);
            }
        }
        #endregion

        #region NfeExiste()
        /// <summary>
        /// Verifica se a NFe já existe no arquivo XML de controle do fluxo.
        /// </summary>
        /// <returns>true = Existe</returns>
        public Boolean NfeExiste(string strChaveNFe)
        {
            Boolean booExiste = false;

            try
            {
                XmlDocument xdXml = new XmlDocument(); //Criar instância do XmlDocument Class
                FileStream fsArquivo = new FileStream(NomeXmlControleFluxo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite); //Abrir um arquivo XML usando FileStream
                xdXml.Load(fsArquivo); //Carregar o arquivo aberto no XmlDocument

                XmlNodeList list = xdXml.GetElementsByTagName(ElementoFixo.Documento.ToString()); //Pesquisar o elemento Documento no arquivo XML
                for (int i = 0; i < list.Count; i++) //Navegar em todos os elementos do nó Documento
                {
                    XmlElement cl = (XmlElement)xdXml.GetElementsByTagName(ElementoFixo.Documento.ToString())[i]; //Recuperar o conteúdo da tag Documento
                    if (cl.GetAttribute(ElementoFixo.ChaveNFe.ToString()) == strChaveNFe)
                    {
                        booExiste = true;
                    }
                }
                fsArquivo.Close(); //Fecha o arquivo XML
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return booExiste;
        }
        #endregion

        #region NFeComLote()
        /// <summary>
        /// Verifica se a NFE já foi incluida em um lote de NFe
        /// </summary>
        /// <param name="strChaveNFe">Chave da Nota Fiscal Eletrônica a ser Verificado</param>
        /// <returns>true = Já está em um lote</returns>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>20/04/2009</date>
        public Boolean NFeComLote(string strChaveNFe)
        {
            Boolean booComLote = false;
            FileStream fsArquivo = null;

            try
            {
                XmlDocument xdXml = new XmlDocument(); //Criar instância do XmlDocument Class
                fsArquivo = new FileStream(NomeXmlControleFluxo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite); //Abrir um arquivo XML usando FileStream
                xdXml.Load(fsArquivo); //Carregar o arquivo aberto no XmlDocument

                XmlNodeList list = xdXml.GetElementsByTagName(ElementoFixo.Documento.ToString()); //Pesquisar o elemento Documento no arquivo XML
                for (int i = 0; i < list.Count; i++) //Navegar em todos os elementos do nó Documento
                {
                    XmlElement cl = (XmlElement)xdXml.GetElementsByTagName(ElementoFixo.Documento.ToString())[i]; //Recuperar o conteúdo da tag Documento
                    XmlElement xeTagLote = (XmlElement)xdXml.GetElementsByTagName(ElementoEditavel.idLote.ToString())[i]; //Recupera o conteúdo da tag de Lote
                    if (cl.GetAttribute(ElementoFixo.ChaveNFe.ToString()) == strChaveNFe)
                    {
                        if (xeTagLote.InnerText != string.Empty)
                        {
                            booComLote = true;
                        }
                    }
                }
                fsArquivo.Close(); //Fecha o arquivo XML
            }
            catch (Exception ex)
            {
                if (fsArquivo != null)
                {
                    fsArquivo.Close();
                }

                throw (ex);
            }

            return booComLote;
        }
        #endregion

        #region AtualizarTag()
        /// <summary>
        /// Atualizar o conteúdo das Tag´s do XML de controle do Fluxo
        /// </summary>
        /// <param name="strChaveNFe">Chave da NFe</param>
        /// <param name="strTag">Tag a ser atualizada</param>
        /// <param name="strNovoConteudo">Novo conteúdo para a tag</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>17/04/2009</date>
        public void AtualizarTag(string strChaveNFe, ElementoEditavel Tag, string strNovoConteudo)
        {
            DateTime startTime;
            DateTime stopTime;
            TimeSpan elapsedTime;

            long elapsedMillieconds;
            startTime = DateTime.Now;

            while (true)
            {
                stopTime = DateTime.Now;
                elapsedTime = stopTime.Subtract(startTime);
                elapsedMillieconds = (int)elapsedTime.TotalMilliseconds;

                FileStream fsArquivo = null;

                try
                {
                    XmlDocument xdXml = new XmlDocument(); //Criar instância do XmlDocument Class
                    fsArquivo = new FileStream(NomeXmlControleFluxo, FileMode.Open, FileAccess.ReadWrite, FileShare.Read); //Abrir um arquivo XML usando FileStream

                    xdXml.Load(fsArquivo); //Carregar o arquivo aberto no XmlDocument
                    XmlNodeList list = xdXml.GetElementsByTagName(ElementoFixo.Documento.ToString()); //Pesquisar o elemento Documento no arquivo XML
                    for (int i = 0; i < list.Count; i++) //Navegar em todos os elementos do nó Documento
                    {
                        XmlElement xeDoc = (XmlElement)xdXml.GetElementsByTagName(ElementoFixo.Documento.ToString())[i]; //Recuperar o conteúdo da tag Produto
                        XmlElement xeTag = (XmlElement)xdXml.GetElementsByTagName(Tag.ToString())[i]; //Recuperar o conteúdo da tag
                        if (xeDoc.GetAttribute(ElementoFixo.ChaveNFe.ToString()) == strChaveNFe)
                        {
                            if (xeTag != null)
                            {
                                xeTag.InnerText = strNovoConteudo; //Setar o novo conteúdo para a tag
                            }
                            break;
                        }
                    }
                    fsArquivo.Close(); //Fecha o arquivo XML
                    xdXml.Save(NomeXmlControleFluxo); //Grava o arquivo xml

                    break;
                }
                catch (Exception ex)
                {
                    if (fsArquivo != null)
                    {
                        fsArquivo.Close();
                    }

                    if (elapsedMillieconds >= 120000) //120.000 ms que corresponde á 120 segundos que corresponde a 2 minuto
                    {
                        throw (ex);
                    }
                }

                Thread.Sleep(200);
            }
        }
        #endregion

        #region AtualizarTagRec()
        /// <summary>
        /// Atualiza a tag nRec de todas as NFe´s do lote passado por parâmetro
        /// </summary>
        /// <param name="strLote">Lote que é para atualziar o número do recibo</param>
        /// <param name="strNovoConteudo">Número do recibo</param>
        public void AtualizarTagRec(string strLote, string strRecibo)
        {
            DateTime startTime;
            DateTime stopTime;
            TimeSpan elapsedTime;

            long elapsedMillieconds;
            startTime = DateTime.Now;

            while (true)
            {
                stopTime = DateTime.Now;
                elapsedTime = stopTime.Subtract(startTime);
                elapsedMillieconds = (int)elapsedTime.TotalMilliseconds;

                FileStream fsArquivo = null;

                try
                {
                    XmlDocument doc = new XmlDocument(); //Criar instância do XmlDocument Class
                    fsArquivo = new FileStream(NomeXmlControleFluxo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    doc.Load(fsArquivo); //Carregar o arquivo aberto no XmlDocument

                    XmlNodeList documentosList = doc.GetElementsByTagName(ElementoFixo.DocumentosNFe.ToString()); //Pesquisar o elemento Documento no arquivo XML
                    foreach (XmlNode documentosNode in documentosList)
                    {
                        XmlElement documentosElemento = (XmlElement)documentosNode;

                        XmlNodeList documentoList = documentosElemento.GetElementsByTagName(ElementoFixo.Documento.ToString());
                        foreach (XmlNode documentoNode in documentoList)
                        {
                            XmlElement documentoElemento = (XmlElement)documentoNode;

                            string strChaveNFe = string.Empty;
                            if (documentoElemento.HasAttributes)
                            {
                                strChaveNFe = documentoElemento.Attributes["ChaveNFe"].InnerText;
                            }

                            if (documentoElemento.GetElementsByTagName("idLote")[0].InnerText == strLote)
                            {
                                this.AtualizarTag(strChaveNFe, ElementoEditavel.dPedRec, DateTime.Now.ToString());
                                this.AtualizarTag(strChaveNFe, ElementoEditavel.nRec, strRecibo);
                            }
                        }
                    }

                    fsArquivo.Close();

                    break;
                }
                catch (Exception ex)
                {
                    if (fsArquivo != null)
                    {
                        fsArquivo.Close();
                    }

                    if (elapsedMillieconds >= 120000) //120.000 ms que corresponde á 120 segundos que corresponde a 2 minuto
                    {
                        throw (ex);
                    }
                }

                Thread.Sleep(200);
            }
        }
        #endregion

        #region LerTag()
        /// <summary>
        /// Ler conteúdo da Tag de uma determinada NFe que já está no controle de fluxo de notas sendo enviadas
        /// </summary>
        /// <param name="strChaveNFe">Chave da NFe que é para ler a tag</param>
        /// <param name="Tag">Nome da tag a ser lida</param>
        /// <returns>Retorna o conteúdo da TAG</returns>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>20/04/2009</date>
        private string LerTag(string strChaveNFe, string Tag)
        {
            string strConteudo = string.Empty;
            FileStream fsArquivo = null;

            try
            {
                XmlDocument doc = new XmlDocument(); //Criar instância do XmlDocument Class
                fsArquivo = new FileStream(NomeXmlControleFluxo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite); //Abrir um arquivo XML usando FileStream
                doc.Load(fsArquivo); //Carregar o arquivo aberto no XmlDocument

                XmlNodeList documentoList = doc.GetElementsByTagName(ElementoFixo.Documento.ToString()); //Pesquisar o elemento Documento no arquivo XML
                foreach (XmlNode documentoNode in documentoList)
                {
                    XmlElement documentoElemento = (XmlElement)documentoNode;

                    if (documentoElemento.GetAttribute(ElementoFixo.ChaveNFe.ToString()) == strChaveNFe)
                    {
                        if (documentoElemento.GetElementsByTagName(Tag)[0] != null) //null significa que não encontrou a TAG, comparo para evitar erros
                        {
                            strConteudo = documentoElemento.GetElementsByTagName(Tag)[0].InnerText;
                        }

                        break;
                    }
                }

                fsArquivo.Close(); //Fecha o arquivo XML
            }
            catch (Exception ex)
            {
                if (fsArquivo != null)
                {
                    fsArquivo.Close();
                }

                throw (ex);
            }

            return strConteudo;
        }
        #endregion

        #region LerTag() - Sobrecarga
        /// <summary>
        /// Ler conteúdo da Tag de uma determinada NFe que já está no controle de fluxo de notas sendo enviadas
        /// </summary>
        /// <param name="strChaveNFe">Chave da NFe que é para ler a tag</param>
        /// <param name="Tag">Nome da tag a ser lida</param>
        /// <returns>Retorna o conteúdo da TAG</returns>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>20/04/2009</date>
        public string LerTag(string strChaveNFe, ElementoEditavel Tag)
        {
            string strConteudo = string.Empty;

            try
            {
                strConteudo = LerTag(strChaveNFe, Tag.ToString());
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return strConteudo;
        }
        #endregion

        #region LerTag() - Sobrecarga
        /// <summary>
        /// Ler conteúdo da Tag de uma determinada NFe que já está no controle de fluxo de notas sendo enviadas
        /// </summary>
        /// <param name="strChaveNFe">Chave da NFe que é para ler a tag</param>
        /// <param name="Tag">Nome da tag a ser lida</param>
        /// <returns>Retorna o conteúdo da TAG</returns>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>20/04/2009</date>
        public string LerTag(string strChaveNFe, ElementoFixo Tag)
        {
            string strConteudo = string.Empty;

            try
            {
                strConteudo = LerTag(strChaveNFe, Tag.ToString());
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return strConteudo;
        }
        #endregion

        #region CriarListaRec()
        /// <summary>
        /// Criar uma lista com os recibos a serem consultados no servidor do SEFAZ
        /// </summary>
        /// <returns>Lista dos recibos</returns>
        /// <by>Wandrey Mundin Ferreira</by>
        public List<ReciboCons> CriarListaRec()
        {
            List<ReciboCons> lstRecibo = new List<ReciboCons>();
            List<string> lstNumRec = new List<string>();

            FileStream fsArquivo = null;

            try
            {
                XmlDocument doc = new XmlDocument(); //Criar instância do XmlDocument Class
                fsArquivo = new FileStream(NomeXmlControleFluxo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite); //Abrir um arquivo XML usando FileStream
                doc.Load(fsArquivo); //Carregar o arquivo aberto no XmlDocument

                XmlNodeList documentoList = doc.GetElementsByTagName(ElementoFixo.Documento.ToString()); //Pesquisar o elemento Documento no arquivo XML
                foreach (XmlNode documentoNode in documentoList)
                {
                    XmlElement documentoElemento = (XmlElement)documentoNode;

                    string nRec = documentoElemento.GetElementsByTagName(ElementoEditavel.nRec.ToString())[0].InnerText;
                    int tMed = 3; //3 segundos
                    DateTime dPedRec = DateTime.Now.AddMinutes(-60);

                    if (documentoElemento.GetElementsByTagName(ElementoEditavel.tMed.ToString())[0] != null &&
                        documentoElemento.GetElementsByTagName(ElementoEditavel.tMed.ToString())[0].InnerText != string.Empty)
                    {
                        tMed = Convert.ToInt32(documentoElemento.GetElementsByTagName(ElementoEditavel.tMed.ToString())[0].InnerText);
                    }

                    if (documentoElemento.GetElementsByTagName(ElementoEditavel.dPedRec.ToString())[0] != null &&
                        documentoElemento.GetElementsByTagName(ElementoEditavel.dPedRec.ToString())[0].InnerText != string.Empty)
                    {
                        dPedRec = Convert.ToDateTime(documentoElemento.GetElementsByTagName(ElementoEditavel.dPedRec.ToString())[0].InnerText);
                    }

                    if (nRec != string.Empty && !lstNumRec.Contains(nRec))
                    {
                        lstNumRec.Add(nRec);

                        ReciboCons oReciboCons = new ReciboCons();
                        oReciboCons.dPedRec = dPedRec;
                        oReciboCons.nRec = nRec;
                        oReciboCons.tMed = tMed;
                        lstRecibo.Add(oReciboCons);
                    }

                    //Se tiver mais de 2 dias no fluxo, vou excluir a nota dele.
                    //Não faz sentido uma nota ficar no fluxo todo este tempo, então vou fazer uma limpeza
                    //Wandrey 11/09/2009
                    if (DateTime.Now.Subtract(dPedRec).Days >= 2)
                    {
                        string ChaveNFe = documentoElemento.GetAttribute(ElementoFixo.ChaveNFe.ToString());
                        string NomeArquivo = documentoElemento.GetElementsByTagName(ElementoFixo.ArqNFe.ToString())[0].InnerText;
                        int emp = new FindEmpresaThread(Thread.CurrentThread).Index; 

                        //Deletar o arquivo da pasta em processamento
                        Auxiliar oAux = new Auxiliar();
                        oAux.MoveArqErro(Empresa.Configuracoes[emp].PastaEnviado + "\\" + Enums.PastaEnviados.EmProcessamento.ToString() + "\\" + NomeArquivo);

                        //Deletar a NFE do arquivo de controle de fluxo
                        this.ExcluirNfeFluxo(ChaveNFe);
                    }
                }

                fsArquivo.Close(); //Fecha o arquivo XML
            }
            catch (XmlException ex)
            {
                if (fsArquivo != null)
                {
                    fsArquivo.Close();
                }

                throw (ex);
            }
            catch (Exception ex)
            {
                if (fsArquivo != null)
                {
                    fsArquivo.Close();
                }

                throw (ex);
            }

            return lstRecibo;
        }
        #endregion

        #region AtualizarTagDPedRec()
        /// <summary>
        /// Atualiza a tag nRec de todas as NFe´s do lote passado por parâmetro
        /// </summary>
        /// <param name="strLote">Lote que é para atualziar o número do recibo</param>
        /// <param name="strNovoConteudo">Número do recibo</param>
        /// <by>Wandrey Mundin Ferreira</by>
        public void AtualizarDPedRec(string strRec, DateTime dtData)
        {
            DateTime startTime;
            DateTime stopTime;
            TimeSpan elapsedTime;

            long elapsedMillieconds;
            startTime = DateTime.Now;

            while (true)
            {
                stopTime = DateTime.Now;
                elapsedTime = stopTime.Subtract(startTime);
                elapsedMillieconds = (int)elapsedTime.TotalMilliseconds;

                FileStream fsArquivo = null;

                try
                {
                    XmlDocument doc = new XmlDocument(); //Criar instância do XmlDocument Class
                    fsArquivo = new FileStream(NomeXmlControleFluxo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    doc.Load(fsArquivo); //Carregar o arquivo aberto no XmlDocument

                    XmlNodeList documentosList = doc.GetElementsByTagName(ElementoFixo.DocumentosNFe.ToString()); //Pesquisar o elemento Documento no arquivo XML
                    foreach (XmlNode documentosNode in documentosList)
                    {
                        XmlElement documentosElemento = (XmlElement)documentosNode;

                        XmlNodeList documentoList = documentosElemento.GetElementsByTagName(ElementoFixo.Documento.ToString());
                        foreach (XmlNode documentoNode in documentoList)
                        {
                            XmlElement documentoElemento = (XmlElement)documentoNode;

                            string strChaveNFe = string.Empty;
                            if (documentoElemento.HasAttributes)
                            {
                                strChaveNFe = documentoElemento.Attributes["ChaveNFe"].InnerText;
                            }

                            if (documentoElemento.GetElementsByTagName("nRec")[0].InnerText == strRec)
                            {
                                this.AtualizarTag(strChaveNFe, ElementoEditavel.dPedRec, dtData.ToString());
                            }
                        }
                    }

                    fsArquivo.Close();

                    break;
                }
                catch (Exception ex)
                {
                    if (fsArquivo != null)
                    {
                        fsArquivo.Close();
                    }

                    if (elapsedMillieconds >= 120000) //120.000 ms que corresponde á 120 segundos que corresponde a 2 minuto
                    {
                        throw (ex);
                    }
                }

                Thread.Sleep(200);
            }
        }
        #endregion

        #endregion
    }
    #endregion

    #region ReciboCons
    /// <summary>
    /// Classe para auxiliar retornos dos recibos a serem consultados no SEFAZ
    /// </summary>
    public class ReciboCons
    {
        /// <summary>
        /// Número do recibo
        /// </summary>
        public string nRec = string.Empty;
        /// <summary>
        /// Tempo médio de resposta do SEFAZ
        /// </summary>
        public int tMed = 0;
        /// <summary>
        /// Data e hora da ultima consulta do recibo efetuada
        /// </summary>
        public DateTime dPedRec;
    }
    #endregion
}
