using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using UniNFeLibrary.Enums;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Threading;
using System.Windows.Forms;

namespace UniNFeLibrary
{
    public class Auxiliar
    {
        #region Atributos
        public static bool EncerrarApp = false;
        /// <summary>
        /// Lista das threads que estão sendo executadas e o Index da empresa da thread
        /// </summary>
        public static Dictionary<Thread, int> threads = new Dictionary<Thread, int>();
        #endregion

        #region DeletarArquivo()
        /// <summary>
        /// Excluir arquivos do HD
        /// </summary>
        /// <param name="Arquivo">Nome do arquivo a ser excluido.</param>
        /// <date>05/08/2009</date>
        /// <by>Wandrey Mundin Ferreira</by>
        public void DeletarArquivo(string Arquivo)
        {
            try
            {
                if (File.Exists(Arquivo))
                {
                    FileInfo oArquivo = new FileInfo(Arquivo);
                    oArquivo.Delete();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region DeletarArqXMLErro()
        /// <summary>
        /// Deleta o XML da pata temporária dos arquivos com erro se o mesmo existir.
        /// </summary>
        public void DeletarArqXMLErro(string Arquivo)
        {
            try
            {
                this.DeletarArquivo(Arquivo);
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

        #region ExtrairNomeArq()
        /// <summary>
        /// Extrai somente o nome do arquivo de uma string; para ser utilizado na situação desejada. Veja os exemplos na documentação do código.
        /// </summary>
        /// <param name="pPastaArq">String contendo o caminho e nome do arquivo que é para ser extraido o nome.</param>
        /// <param name="pFinalArq">String contendo o final do nome do arquivo até onde é para ser extraído.</param>
        /// <returns>Retorna somente o nome do arquivo de acordo com os parâmetros passados - veja exemplos.</returns>
        /// <example>
        /// MessageBox.Show(this.ExtrairNomeArq("C:\\TESTE\\NFE\\ENVIO\\ArqSituacao-ped-sta.xml", "-ped-sta.xml"));
        /// //Será demonstrado no message a string "ArqSituacao"
        /// 
        /// MessageBox.Show(this.ExtrairNomeArq("C:\\TESTE\\NFE\\ENVIO\\ArqSituacao-ped-sta.xml", ".xml"));
        /// //Será demonstrado no message a string "ArqSituacao-ped-sta"
        /// </example>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>19/06/2008</date>
        public string ExtrairNomeArq(string pPastaArq, string pFinalArq)
        {
            FileInfo fi = new FileInfo(pPastaArq);
            string ret = fi.Name;
            ret = ret.Substring(0, ret.Length - pFinalArq.Length);
            return ret;
        }
        #endregion

        #region FileInUse()
        /// <summary>
        /// detectar se o arquivo está em uso
        /// </summary>
        /// <param name="file">caminho do arquivo</param>
        /// <returns>true se estiver em uso</returns>
        /// <by>http://desenvolvedores.net/marcelo</by>
        public static bool FileInUse(string file)
        {
            bool ret = false;

            try
            {
                using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
                {
                    fs.Close();//fechar o arquivo para nao dar erro em outras aplicações
                }
            }
            catch (IOException ex)
            {
                ret = true;
            }
            catch (Exception ex)
            {
                ret = true;
            }

            return ret;
        }
        #endregion

        #region GerarChaveNFe
        /// <summary>
        /// MontaChave
        /// Cria a chave de acesso da NFe
        /// </summary>
        /// <param name="ArqXMLPedido"></param>
        public void GerarChaveNFe(string ArqPedido, Boolean xml)
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index;

            // XML - pedido
            // Filename: XXXXXXXX-gerar-chave.xml
            // -------------------------------------------------------------------
            // <?xml version="1.0" encoding="UTF-8"?>
            // <gerarChave>
            //      <UF>35</UF>                 //se não informado, assume a da configuração
            //      <tpEmis>1</tpEmis>          //se não informado, assume a da configuração. Wandrey 22/03/2010
            //      <nNF>1000</nNF>
            //      <cNF>0</cNF>                //se não informado, eu gero
            //      <serie>1</serie>
            //      <AAMM>0912</AAMM>
            //      <CNPJ>55801377000131</CNPJ>
            // </gerarChave>
            //
            // XML - resposta
            // Filename: XXXXXXXX-ret-gerar-chave.xml
            // -------------------------------------------------------------------
            // <?xml version="1.0" encoding="UTF-8"?>
            // <retGerarChave>
            //      <chaveNFe>350912</chaveNFe>
            // </retGerarChave>
            //

            // TXT - pedido
            // Filename: XXXXXXXX-gerar-chave.txt
            // -------------------------------------------------------------------
            // UF|35
            // tpEmis|1
            // nNF|1000
            // cNF|0
            // serie|1
            // AAMM|0912
            // CNPJ|55801377000131
            //
            // TXT - resposta
            // Filename: XXXXXXXX-ret-gerar-chave.txt
            // -------------------------------------------------------------------
            // 35091255801377000131550010000000010000176506
            //
            // -------------------------------------------------------------------
            // ERR - resposta
            // Filename: XXXXXXXX-gerar-chave.err

            string ArqXMLRetorno = Empresa.Configuracoes[emp].PastaRetorno + "\\" + (xml ? this.ExtrairNomeArq(ArqPedido, ExtXml.GerarChaveNFe_XML) + "-ret-gerar-chave.xml" : this.ExtrairNomeArq(ArqPedido, ExtXml.GerarChaveNFe_TXT) + "-ret-gerar-chave.txt");
            string ArqERRRetorno = Empresa.Configuracoes[emp].PastaRetorno + "\\" + (xml ? this.ExtrairNomeArq(ArqPedido, ExtXml.GerarChaveNFe_XML) + "-gerar-chave.err" : this.ExtrairNomeArq(ArqPedido, ExtXml.GerarChaveNFe_TXT) + "-gerar-chave.err");

            try
            {
                this.DeletarArquivo(ArqXMLRetorno);
                this.DeletarArquivo(ArqERRRetorno);
                this.DeletarArquivo(Empresa.Configuracoes[emp].PastaErro + "\\" + ArqPedido);

                if (!File.Exists(ArqPedido))
                {
                    throw new Exception("Arquivo " + ArqPedido + " não encontrado");
                }

                UnitxtTOxmlClass oUniTxtToXml = new UnitxtTOxmlClass();

                if (!Auxiliar.FileInUse(ArqPedido))
                {
                    int serie = 0;
                    int tpEmis = Empresa.Configuracoes[emp].tpEmis;
                    int nNF = 0;
                    int cNF = 0;
                    int cUF = Empresa.Configuracoes[emp].UFCod;
                    string cAAMM = "0000";
                    string cChave = "";
                    string cCNPJ = "";
                    string cError = "";

                    if (xml)
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(ArqPedido);

                        XmlNodeList mChaveList = doc.GetElementsByTagName("gerarChave");

                        foreach (XmlNode mChaveNode in mChaveList)
                        {
                            XmlElement mChaveElemento = (XmlElement)mChaveNode;

                            if (mChaveElemento.GetElementsByTagName("UF").Count != 0)
                                cUF = Convert.ToInt32("0" + mChaveElemento.GetElementsByTagName("UF")[0].InnerText);

                            if (mChaveElemento.GetElementsByTagName("tpEmis").Count != 0)
                                tpEmis = Convert.ToInt32("0" + mChaveElemento.GetElementsByTagName("tpEmis")[0].InnerText);

                            if (mChaveElemento.GetElementsByTagName("nNF").Count != 0)
                                nNF = Convert.ToInt32("0" + mChaveElemento.GetElementsByTagName("nNF")[0].InnerText);

                            if (mChaveElemento.GetElementsByTagName("cNF").Count != 0)
                                cNF = Convert.ToInt32("0" + mChaveElemento.GetElementsByTagName("cNF")[0].InnerText);

                            if (mChaveElemento.GetElementsByTagName("serie").Count != 0)
                                serie = Convert.ToInt32("0" + mChaveElemento.GetElementsByTagName("serie")[0].InnerText);

                            if (mChaveElemento.GetElementsByTagName("AAMM").Count != 0)
                                cAAMM = mChaveElemento.GetElementsByTagName("AAMM")[0].InnerText;

                            if (mChaveElemento.GetElementsByTagName("CNPJ").Count != 0)
                                cCNPJ = mChaveElemento.GetElementsByTagName("CNPJ")[0].InnerText;
                        }
                    }
                    else
                    {
                        List<string> cLinhas = this.LerArquivo(ArqPedido);
                        string[] dados;
                        foreach (string cLinha in cLinhas)
                        {
                            dados = cLinha.Split('|');
                            dados[0] = dados[0].ToUpper();
                            if (dados.GetLength(0) == 1)
                                cError += "Segmento [" + dados[0] + "] inválido" + Environment.NewLine;
                            else
                                switch (dados[0].ToLower())
                                {
                                    case "uf":
                                        cUF = Convert.ToInt32("0" + dados[1]);
                                        break;
                                    case "tpemis":
                                        tpEmis = Convert.ToInt32("0" + dados[1]);
                                        break;
                                    case "nnf":
                                        nNF = Convert.ToInt32("0" + dados[1]);
                                        break;
                                    case "cnf":
                                        cNF = Convert.ToInt32("0" + dados[1]);
                                        break;
                                    case "serie":
                                        serie = Convert.ToInt32("0" + dados[1]);
                                        break;
                                    case "aamm":
                                        cAAMM = dados[1];
                                        break;
                                    case "cnpj":
                                        cCNPJ = dados[1];
                                        break;
                                }
                        }
                    }

                    if (nNF == 0)
                        cError = "Número da nota fiscal deve ser informado" + Environment.NewLine;

                    if (string.IsNullOrEmpty(cAAMM))
                        cError += "Ano e mês da emissão deve ser informado" + Environment.NewLine;

                    if (string.IsNullOrEmpty(cCNPJ))
                        cError += "CNPJ deve ser informado" + Environment.NewLine;

                    if (cAAMM.Substring(0, 2) == "00")
                        cError += "Ano da emissão inválido" + Environment.NewLine;

                    if (Convert.ToInt32(cAAMM.Substring(2, 2)) <= 0 || Convert.ToInt32(cAAMM.Substring(2, 2)) > 12)
                        cError += "Mês da emissão inválido" + Environment.NewLine;

                    if (cError != "")
                        throw new Exception(cError);

                    Int64 iTmp = Convert.ToInt64("0" + cCNPJ);
                    cChave = cUF.ToString("00") + cAAMM + iTmp.ToString("00000000000000") + "55";

                    if (cNF == 0)
                    {
                        ///
                        /// gera codigo aleatorio
                        /// 
                        cNF = oUniTxtToXml.GerarCodigoNumerico(nNF);
                    }

                    ///
                    /// calcula do digito verificador
                    /// 
                    string ccChave = cChave + serie.ToString("000") + nNF.ToString("000000000") + tpEmis.ToString("0") + cNF.ToString("00000000");
                    int cDV = oUniTxtToXml.GerarDigito(ccChave);

                    ///
                    /// monta a chave da NFe
                    /// 
                    cChave += serie.ToString("000") + nNF.ToString("000000000") + tpEmis.ToString("0") + cNF.ToString("00000000") + cDV.ToString("0");

                    ///
                    /// grava o XML/TXT de resposta
                    /// 
                    string vMsgRetorno = (xml ? "<?xml version=\"1.0\" encoding=\"UTF-8\"?><retGerarChave><chaveNFe>" + cChave + "</chaveNFe></retGerarChave>" : cChave);
                    File.WriteAllText(ArqXMLRetorno, vMsgRetorno, Encoding.Default);

                    ///
                    /// exclui o XML/TXT de pedido
                    /// 
                    this.DeletarArquivo(ArqPedido);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    this.MoveArqErro(ArqPedido);

                    File.WriteAllText(ArqERRRetorno, "Arquivo " + ArqERRRetorno + Environment.NewLine + ex.Message, Encoding.Default);
                }
                catch
                {
                    //Se der algum erro na hora de gravar o arquivo de erro para o ERP, infelizmente não vamos poder fazer nada, visto que 
                    //pode ser algum problema com a rede, hd, permissões, etc... Wandrey 22/03/2010
                }
            }
        }
        #endregion

        #region GravarArqErroERP
        /// <summary>
        /// grava um arquivo de erro ao ERP
        /// </summary>
        /// <param name="Arquivo"></param>
        /// <param name="Erro"></param>
        public void GravarArqErroERP(string Arquivo, string Erro)
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index;
            if (!string.IsNullOrEmpty(Arquivo))
            {
                try
                {
                    if (Empresa.Configuracoes[emp].PastaRetorno != string.Empty)
                    {
                        //Grava arquivo de ERRO para o ERP
                        string cArqErro = Empresa.Configuracoes[emp].PastaRetorno + "\\" + Path.GetFileName(Arquivo);
                        File.WriteAllText(cArqErro, Erro, Encoding.Default);
                    }
                }
                catch
                {
                    //TODO: V3.0 - Não deveriamos retornar a exeção com throw?
                }
            }
        }
        #endregion

        #region GravarArqErroServico()
        /// <summary>
        /// Grava um arquivo texto com um erro ocorrido na invocação dos WebServices ou na execusão de alguma
        /// rotina de validação, etc. Este arquivo é gravado para que o sistema ERP tenha condições de interagir
        /// com o usuário.
        /// </summary>
        /// <param name="Arquivo">Nome do arquivo XML que foi enviado para os WebServices</param>
        /// <param name="PastaXMLRetorno">Pasta de retorno para gravar o XML de erro</param>
        /// <param name="FinalArqEnvio">Final do nome do arquivo de solicitação do serviço</param>
        /// <param name="FinalArqErro">Final do nome do arquivo que é para ser gravado o erro</param>
        /// <param name="Erro">Texto do erro ocorrido a ser gravado no arquivo</param>
        /// <param name="PastaXMLErro">Pasta para mover o XML com erro</param>
        /// <by>Wandrey Mundin Ferreira</by>
        public void GravarArqErroServico(string Arquivo, string FinalArqEnvio, string FinalArqErro, string Erro)
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index;

            try
            {
                //Qualquer erro ocorrido o aplicativo vai mover o XML com falha da pasta de envio
                //para a pasta de XML´s com erros. Futuramente ele é excluido quando outro igual
                //for gerado corretamente.
                this.MoveArqErro(Arquivo);

                //Grava arquivo de ERRO para o ERP
                string cArqErro = Empresa.Configuracoes[emp].PastaRetorno + "\\" +
                                  this.ExtrairNomeArq(Arquivo, FinalArqEnvio) +
                                  FinalArqErro;

                File.WriteAllText(cArqErro, Erro, Encoding.Default);
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

        #region GravarXMLRetornoValidacao()
        /// <summary>
        /// Na tentativa de somente validar ou assinar o XML se encontrar um erro vai ser retornado um XML para o ERP
        /// </summary>
        /// <param name="Arquivo">Nome do arquivo XML validado</param>
        /// <param name="PastaXMLRetorno">Pasta de retorno para ser gravado o XML</param>
        /// <param name="cStat">Status da validação</param>
        /// <param name="xMotivo">Status descritivo da validação</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>28/05/2009</date>
        private void GravarXMLRetornoValidacao(string Arquivo, string cStat, string xMotivo)
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index;

            //Definir o nome do arquivo de retorno
            string ArquivoRetorno = this.ExtrairNomeArq(Arquivo, ".xml") + "-ret.xml";

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
                oXmlGravar = XmlWriter.Create(Empresa.Configuracoes[emp].PastaRetorno + "\\" + ArquivoRetorno);

                //Agora vamos gravar os dados
                oXmlGravar.WriteStartDocument();
                oXmlGravar.WriteStartElement("Validacao");
                oXmlGravar.WriteElementString("cStat", cStat);
                oXmlGravar.WriteElementString("xMotivo", xMotivo);
                oXmlGravar.WriteEndElement(); //nfe_configuracoes
                oXmlGravar.WriteEndDocument();
                oXmlGravar.Flush();
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
                if (oXmlGravar != null)
                    oXmlGravar.Close();
            }
        }
        #endregion

        #region LerArquivo()
        /// <summary>
        /// Le arquivos no formato TXT
        /// Retorna uma lista do conteudo do arquivo
        /// </summary>
        /// <param name="cArquivo"></param>
        /// <returns></returns>
        public List<string> LerArquivo(string cArquivo)
        {
            List<string> lstRetorno = new List<string>();
            if (File.Exists(cArquivo))
            {
                TextReader txt = new StreamReader(cArquivo);
                try
                {
                    string cLinhaTXT = txt.ReadLine();
                    while (cLinhaTXT != null)
                    {
                        string[] dados = cLinhaTXT.Split('|');
                        if (dados.GetLength(0) == 2)
                        {
                            lstRetorno.Add(cLinhaTXT);
                        }
                        cLinhaTXT = txt.ReadLine();
                    }
                }
                finally
                {
                    txt.Close();
                }
                if (lstRetorno.Count == 0)
                    throw new Exception("Arquivo: " + cArquivo + " vazio");
            }
            return lstRetorno;
        }
        #endregion

        #region LerTag()
        /// <summary>
        /// Busca o nome de uma determinada TAG em um Elemento do XML para ver se existe, se existir retorna seu conteúdo com um ponto e vírgula no final do conteúdo.
        /// </summary>
        /// <param name="Elemento">Elemento a ser pesquisado o Nome da TAG</param>
        /// <param name="NomeTag">Nome da Tag</param>
        /// <returns>Conteúdo da tag</returns>
        /// <date>05/08/2009</date>
        /// <by>Wandrey Mundin Ferreira</by>
        public string LerTag(XmlElement Elemento, string NomeTag)
        {
            return this.LerTag(Elemento, NomeTag, true);
        }
        #endregion

        #region LerTag()
        /// <summary>
        /// Busca o nome de uma determinada TAG em um Elemento do XML para ver se existe, se existir retorna seu conteúdo, com ou sem um ponto e vírgula no final do conteúdo.
        /// </summary>
        /// <param name="Elemento">Elemento a ser pesquisado o Nome da TAG</param>
        /// <param name="NomeTag">Nome da Tag</param>
        /// <param name="RetornaPontoVirgula">Retorna com ponto e vírgula no final do conteúdo da tag</param>
        /// <returns>Conteúdo da tag</returns>
        /// <date>05/08/2009</date>
        /// <by>Wandrey Mundin Ferreira</by>
        public string LerTag(XmlElement Elemento, string NomeTag, bool RetornaPontoVirgula)
        {
            string Retorno = string.Empty;

            if (Elemento.GetElementsByTagName(NomeTag).Count != 0)
            {
                if (RetornaPontoVirgula)
                {
                    Retorno = Elemento.GetElementsByTagName(NomeTag)[0].InnerText.Replace(";", " ");  //danasa 19-9-2009
                    Retorno += ";";
                }
                else
                {
                    Retorno = Elemento.GetElementsByTagName(NomeTag)[0].InnerText;  //Wandrey 07/10/2009
                }
            }
            return Retorno;
        }
        #endregion

        #region MemoryStream
        /// <summary>
        /// Método responsável por converter uma String contendo a estrutura de um XML em uma Stream para
        /// ser lida pela XMLDocument
        /// </summary>
        /// <returns>String convertida em Stream</returns>
        /// <remarks>Conteúdo do método foi fornecido pelo Marcelo da desenvolvedores.net</remarks>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>20/04/2009</date>
        public static MemoryStream StringXmlToStream(string strXml)
        {
            byte[] byteArray = new byte[strXml.Length];
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byteArray = encoding.GetBytes(strXml);
            MemoryStream memoryStream = new MemoryStream(byteArray);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }
        #endregion

        #region MoveArqErro()
        /// <summary>
        /// Move arquivos com a extensão informada e que está com erro para uma pasta de xml´s/arquivos com erro configurados no UniNFe.
        /// </summary>
        /// <param name="cArquivo">Nome do arquivo a ser movido para a pasta de XML´s com erro</param>
        /// <param name="ExtensaoArq">Extensão do arquivo que vai ser movido. Ex: .xml</param>
        /// <example>this.MoveArqErro(this.vXmlNfeDadosMsg, ".xml")</example>
        public void MoveArqErro(string Arquivo, string ExtensaoArq)
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index;

            try
            {
                if (File.Exists(Arquivo))
                {
                    FileInfo oArquivo = new FileInfo(Arquivo);

                    if (Directory.Exists(Empresa.Configuracoes[emp].PastaErro))
                    {
                        string vNomeArquivo = Empresa.Configuracoes[emp].PastaErro + "\\" + this.ExtrairNomeArq(Arquivo, ExtensaoArq) + ExtensaoArq;

                        //Deletar o arquivo da pasta de XML com erro se o mesmo existir lá para evitar erros na hora de mover. Wandrey
                        if (File.Exists(vNomeArquivo))
                            this.DeletarArquivo(vNomeArquivo);

                        //Mover o arquivo da nota fiscal para a pasta do XML com erro
                        oArquivo.MoveTo(vNomeArquivo);
                    }
                    else
                    {
                        //Antes estava deletando o arquivo, agora vou retornar uma mensagem de erro
                        //pois não podemos excluir, pode ser coisa importante. Wandrey 25/02/2011
                        throw new Exception("A pasta de XML´s com erro informada nas configurações não existe, por favor verifique.");
                        //oArquivo.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region MoveArqErro
        /// <summary>
        /// Move arquivos XML com erro para uma pasta de xml´s com erro configurados no UniNFe.
        /// </summary>
        /// <param name="cArquivo">Nome do arquivo a ser movido para a pasta de XML´s com erro</param>
        /// <example>this.MoveArqErro(this.vXmlNfeDadosMsg)</example>
        public void MoveArqErro(string Arquivo)
        {
            try
            {
                this.MoveArqErro(Arquivo, Path.GetExtension(Arquivo));
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

        #region MoverArquivo()
        /// <summary>
        /// Move o arquivo de pasta
        /// </summary>
        /// <param name="Arquivo">Pasta e nome do arquivo a ser movido</param>
        /// <param name="strDestinoArquivo">Pasta de destino do arquivo</param>
        /// <remarks>
        /// Autor: Wandrey
        /// Data: 23/03/2010
        /// </remarks>
        public void MoverArquivo(string Arquivo, string strDestinoArquivo)
        {
            try
            {
                if (File.Exists(Arquivo))   //danasa 10-2009
                {
                    //Mover o arquivo original para a pasta de destino
                    this.DeletarArquivo(strDestinoArquivo);

                    //Definir o arquivo que vai ser deletado ou movido para outra pasta
                    FileInfo oArquivo = new FileInfo(Arquivo);
                    oArquivo.MoveTo(strDestinoArquivo);
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
        /// <summary>
        /// Move arquivos da nota fiscal eletrônica para suas respectivas pastas
        /// </summary>
        /// <param name="Arquivo">Nome do arquivo a ser movido</param>
        /// <param name="PastaXMLEnviado">Pasta de XML´s enviados para onde será movido o arquivo</param>
        /// <param name="SubPastaXMLEnviado">SubPasta de XML´s enviados para onde será movido o arquivo</param>
        /// <param name="PastaBackup">Pasta para Backup dos XML´s enviados</param>
        /// <param name="Emissao">Data de emissão da Nota Fiscal ou Data Atual do envio do XML para separação dos XML´s em subpastas por Ano e Mês</param>
        /// <date>16/07/2008</date>
        /// <by>Wandrey Mundin Ferreira</by>
        public void MoverArquivo(string Arquivo, PastaEnviados SubPastaXMLEnviado, DateTime Emissao)
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index;

            try
            {
                //Definir o arquivo que vai ser deletado ou movido para outra pasta
                FileInfo oArquivo = new FileInfo(Arquivo);

                //Criar subpastas da pasta dos XML´s enviados
                Empresa.CriarSubPastaEnviado(emp);

                //Criar Pasta do Mês para gravar arquivos enviados autorizados ou denegados
                string strNomePastaEnviado = string.Empty;
                string strDestinoArquivo = string.Empty;
                switch (SubPastaXMLEnviado)
                {
                    case PastaEnviados.EmProcessamento:
                        strNomePastaEnviado = Empresa.Configuracoes[emp].PastaEnviado + "\\" + PastaEnviados.EmProcessamento.ToString();
                        strDestinoArquivo = strNomePastaEnviado + "\\" + this.ExtrairNomeArq(Arquivo, ".xml") + ".xml";
                        break;

                    case PastaEnviados.Autorizados:
                        strNomePastaEnviado = Empresa.Configuracoes[emp].PastaEnviado + "\\" + PastaEnviados.Autorizados.ToString() + "\\" + Empresa.Configuracoes[emp].DiretorioSalvarComo.ToString(Emissao);
                        strDestinoArquivo = strNomePastaEnviado + "\\" + this.ExtrairNomeArq(Arquivo, ".xml") + ".xml";
                        goto default;

                    case PastaEnviados.Denegados:
                        strNomePastaEnviado = Empresa.Configuracoes[emp].PastaEnviado + "\\" + PastaEnviados.Denegados.ToString() + "\\" + Empresa.Configuracoes[emp].DiretorioSalvarComo.ToString(Emissao);
                        strDestinoArquivo = strNomePastaEnviado + "\\" + this.ExtrairNomeArq(Arquivo, "-nfe.xml") + "-den.xml";
                        goto default;

                    default:
                        if (!Directory.Exists(strNomePastaEnviado))
                        {
                            System.IO.Directory.CreateDirectory(strNomePastaEnviado);
                        }
                        break;
                }

                //Se conseguiu criar a pasta ele move o arquivo, caso contrário
                if (Directory.Exists(strNomePastaEnviado) == true)
                {
                    #region Mover o XML para a pasta de XML´s enviados
                    //Mover o arquivo da nota fiscal para a pasta dos enviados
                    if (File.Exists(strDestinoArquivo))
                    {
                        FileInfo oArqDestino = new FileInfo(strDestinoArquivo);
                        oArqDestino.Delete();
                    }
                    oArquivo.MoveTo(strDestinoArquivo);
                    #endregion

                    if (SubPastaXMLEnviado == PastaEnviados.Autorizados || SubPastaXMLEnviado == PastaEnviados.Denegados)
                    {
                        #region Copiar XML para a pasta de BACKUP
                        //Fazer um backup do XML que foi copiado para a pasta de enviados
                        //para uma outra pasta para termos uma maior segurança no arquivamento
                        //Normalmente esta pasta é em um outro computador ou HD
                        if (Empresa.Configuracoes[emp].PastaBackup.Trim() != "")
                        {
                            //Criar Pasta do Mês para gravar arquivos enviados
                            string strNomePastaBackup = string.Empty;
                            switch (SubPastaXMLEnviado)
                            {
                                case PastaEnviados.Autorizados:
                                    strNomePastaBackup = Empresa.Configuracoes[emp].PastaBackup + "\\" + PastaEnviados.Autorizados + "\\" + Empresa.Configuracoes[emp].DiretorioSalvarComo.ToString(Emissao);
                                    goto default;

                                case PastaEnviados.Denegados:
                                    strNomePastaBackup = Empresa.Configuracoes[emp].PastaBackup + "\\" + PastaEnviados.Denegados + "\\" + Empresa.Configuracoes[emp].DiretorioSalvarComo.ToString(Emissao);
                                    goto default;

                                default:
                                    if (Directory.Exists(strNomePastaBackup) == false)
                                    {
                                        System.IO.Directory.CreateDirectory(strNomePastaBackup);
                                    }
                                    break;
                            }

                            //Se conseguiu criar a pasta ele move o arquivo, caso contrário
                            if (Directory.Exists(strNomePastaBackup) == true)
                            {
                                //Mover o arquivo da nota fiscal para a pasta de backup
                                string strNomeArquivoBkp = strNomePastaBackup + "\\" + this.ExtrairNomeArq(Arquivo, ".xml") + ".xml";
                                if (File.Exists(strNomeArquivoBkp))
                                {
                                    FileInfo oArqDestinoBkp = new FileInfo(strNomeArquivoBkp);
                                    oArqDestinoBkp.Delete();
                                }
                                FileInfo oArquivoBkp = new FileInfo(strDestinoArquivo);

                                oArquivoBkp.CopyTo(strNomeArquivoBkp, true);
                            }
                            else
                            {
                                throw new Exception("Pasta de backup informada nas configurações não existe. (Pasta: " + strNomePastaBackup + ")");
                            }
                        }
                        #endregion

                        #region Copiar o XML para a pasta do DanfeMon, se configurado para isso
                        this.CopiarXMLPastaDanfeMon(strDestinoArquivo);
                        #endregion
                    }
                }
                else
                {
                    throw new Exception("Pasta para arquivamento dos XML´s enviados não existe. (Pasta: " + strNomePastaEnviado + ")");
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region CopiarXMLPastaDanfeMon()
        /// <summary>
        /// Copia o XML da NFe para a pasta monitorada pelo DANFEMon para que o mesmo imprima o DANFe.
        /// A copia só é efetuada de o UniNFe estiver configurado para isso.
        /// </summary>
        /// <param name="arquivoCopiar">Nome do arquivo com as pastas e subpastas a ser copiado</param>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 20/04/2010
        /// </remarks>
        public void CopiarXMLPastaDanfeMon(string arquivoCopiar)
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index;

            try
            {
                if (!string.IsNullOrEmpty(Empresa.Configuracoes[emp].PastaDanfeMon))
                {
                    if (Directory.Exists(Empresa.Configuracoes[emp].PastaDanfeMon))
                    {
                        if ((arquivoCopiar.ToLower().Contains("-nfe.xml") && Empresa.Configuracoes[emp].XMLDanfeMonNFe) || (arquivoCopiar.ToLower().Contains("-procnfe.xml") && Empresa.Configuracoes[emp].XMLDanfeMonProcNFe))
                        {
                            //Montar o nome do arquivo de destino
                            string arqDestino = Empresa.Configuracoes[emp].PastaDanfeMon + "\\" + this.ExtrairNomeArq(arquivoCopiar, ".xml") + ".xml";

                            //Copiar o arquivo para o destino
                            FileInfo oArquivo = new FileInfo(arquivoCopiar);
                            oArquivo.CopyTo(arqDestino, true);
                        }
                    }
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

        #region MoverArquivo() - Sobrecarga
        /// <summary>
        /// Move arquivos da nota fiscal eletrônica para suas respectivas pastas
        /// </summary>
        /// <param name="Arquivo">Nome do arquivo a ser movido</param>
        /// <param name="PastaXMLEnviado">Pasta de XML´s enviados para onde será movido o arquivo</param>
        /// <param name="SubPastaXMLEnviado">SubPasta de XML´s enviados para onde será movido o arquivo</param>
        /// <date>05/08/2009</date>
        /// <by>Wandrey Mundin Ferreira</by>
        public void MoverArquivo(string Arquivo, PastaEnviados SubPastaXMLEnviado)
        {
            try
            {
                this.MoverArquivo(Arquivo, SubPastaXMLEnviado, DateTime.Now);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region RenomearArquivo
        // danasa 10-2009
        public void RenomearArquivo(string oldFileName, string newFileName)
        {
            try
            {
                this.DeletarArquivo(newFileName);
                if (File.Exists(oldFileName))
                {
                    FileInfo oArquivo = new FileInfo(oldFileName);
                    oArquivo.MoveTo(newFileName);
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

        #region ValidarArqXML()
        /// <summary>
        /// Valida o arquivo XML 
        /// </summary>
        /// <param name="Arquivo">Nome do arquivo XML a ser validado</param>
        /// <returns>
        /// Se retornar uma string em branco, significa que o XML foi 
        /// validado com sucesso, ou seja, não tem nenhum erro. Se o retorno
        /// tiver algo, algum erro ocorreu na validação.
        /// </returns>
        /// <example>
        /// string cResultadoValidacao = this.ValidarArqXML();
        /// 
        /// if (cResultadoValidacao == "")
        /// {
        ///     MessageBox.Show( "Arquivo validado com sucesso" );
        /// }
        /// else
        /// {
        ///     MessageBox.Show( cResultadoValidacao );
        /// }
        /// </example>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>31/07/2008</date>         
        public string ValidarArqXML(string Arquivo)
        {
            string cRetorna = "";

            // Validar Arquivo XML
            ValidarXMLs oValidador = new ValidarXMLs();
            oValidador.TipoArquivoXML(Arquivo);

            if (oValidador.nRetornoTipoArq >= 1 && oValidador.nRetornoTipoArq <= SchemaXML.MaxID)
            {
                oValidador.Validar(Arquivo, oValidador.cArquivoSchema);
                if (oValidador.Retorno != 0)
                {
                    cRetorna = "XML INCONSISTENTE!\r\n\r\n" + oValidador.RetornoString;
                }
            }
            else
            {
                cRetorna = "XML INCONSISTENTE!\r\n\r\n" + oValidador.cRetornoTipoArq;
            }

            return cRetorna;
        }
        #endregion

        #region ValidarAssinarXML()
        /// <summary>
        /// Efetua a validação de qualquer XML, NFE, Cancelamento, Inutilização, etc..., e retorna se está ok ou não
        /// </summary>
        /// <param name="Arquivo">Nome do arquivo XML a ser validado e assinado</param>
        /// <param name="PastaValidar">Nome da pasta onde fica os arquivos a serem validados</param>
        /// <param name="PastaXMLErro">Nome da pasta onde é para gravar os XML´s validados que apresentaram erro.</param>
        /// <param name="PastaXMLRetorno">Nome da pasta de retorno onde será gravado o XML com o status da validação</param>
        /// <param name="Certificado">Certificado digital a ser utilizado na validação</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>28/05/2009</date>
        public void ValidarAssinarXML(string Arquivo)
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index;

            Boolean Assinou = true;
            ValidarXMLs oValidador = new ValidarXMLs();
            oValidador.TipoArquivoXML(Arquivo);

            //Assinar o XML se tiver tag para assinar
            if (oValidador.TagAssinar != string.Empty)
            {
                AssinaturaDigital oAD = new AssinaturaDigital();

                try
                {
                    oAD.Assinar(Arquivo, oValidador.TagAssinar, Empresa.Configuracoes[emp].X509Certificado);

                    Assinou = true;
                }
                catch (Exception ex)
                {
                    Assinou = false;
                    try
                    {
                        this.GravarXMLRetornoValidacao(Arquivo, "2", "Ocorreu um erro ao assinar o XML: " + ex.Message);
                        this.MoveArqErro(Arquivo);
                    }
                    catch
                    {
                        //Se deu algum erro na hora de gravar o retorno do erro para o ERP, infelizmente não posso fazer nada.
                        //Isso pode acontecer se falhar rede, hd, problema de permissão de pastas, etc... Wandrey 23/03/2010
                    }
                }
            }


            if (Assinou)
            {
                // Validar o Arquivo XML
                if (oValidador.nRetornoTipoArq >= 1 && oValidador.nRetornoTipoArq <= SchemaXML.MaxID)
                {
                    try
                    {
                        oValidador.Validar(Arquivo, oValidador.cArquivoSchema);
                        if (oValidador.Retorno != 0)
                        {
                            this.GravarXMLRetornoValidacao(Arquivo, "3", "Ocorreu um erro ao validar o XML: " + oValidador.RetornoString);
                            this.MoveArqErro(Arquivo);
                        }
                        else
                        {
                            if (!Directory.Exists(Empresa.Configuracoes[emp].PastaValidar + "\\Validado"))
                            {
                                Directory.CreateDirectory(Empresa.Configuracoes[emp].PastaValidar + "\\Validado");
                            }

                            string ArquivoNovo = Empresa.Configuracoes[emp].PastaValidar + "\\Validado\\" + this.ExtrairNomeArq(Arquivo, ".xml") + ".xml";

                            if (File.Exists(ArquivoNovo))
                            {
                                FileInfo oArqNovo = new FileInfo(ArquivoNovo);
                                oArqNovo.Delete();
                            }

                            FileInfo oArquivo = new FileInfo(Arquivo);
                            oArquivo.MoveTo(ArquivoNovo);

                            this.GravarXMLRetornoValidacao(Arquivo, "1", "XML assinado e validado com sucesso.");
                        }
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            this.GravarXMLRetornoValidacao(Arquivo, "4", "Ocorreu um erro ao validar o XML: " + ex.Message);
                            this.MoveArqErro(Arquivo);
                        }
                        catch
                        {
                            //Se deu algum erro na hora de gravar o retorno do erro para o ERP, infelizmente não posso fazer nada.
                            //Isso pode acontecer se falhar rede, hd, problema de permissão de pastas, etc... Wandrey 23/03/2010
                        }
                    }
                }
                else
                {
                    try
                    {
                        this.GravarXMLRetornoValidacao(Arquivo, "5", "Ocorreu um erro ao validar o XML: " + oValidador.cRetornoTipoArq);
                        this.MoveArqErro(Arquivo);
                    }
                    catch
                    {
                        //Se deu algum erro na hora de gravar o retorno do erro para o ERP, infelizmente não posso fazer nada.
                        //Isso pode acontecer se falhar rede, hd, problema de permissão de pastas, etc... Wandrey 23/03/2010
                    }
                }
            }
        }
        #endregion

        #region XmlToString()
        /// <summary>
        /// Método responsável por ler o conteúdo de um XML e retornar em uma string
        /// </summary>
        /// <param name="parNomeArquivo">Caminho e nome do arquivo XML que é para pegar o conteúdo e retornar na string.</param>
        /// <returns>Retorna uma string com o conteúdo do arquivo XML</returns>
        /// <example>
        /// string ConteudoXML;
        /// ConteudoXML = THIS.XmltoString( @"c:\arquivo.xml" );
        /// MessageBox.Show( ConteudoXML );
        /// </example>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>04/06/2008</date>
        public string XmlToString(string parNomeArquivo)
        {
            string conteudo_xml = string.Empty;

            StreamReader SR = null;
            try
            {
                SR = File.OpenText(parNomeArquivo);
                conteudo_xml = SR.ReadToEnd();
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
                SR.Close();
            }

            return conteudo_xml;
        }
        #endregion

        #region EstaAutorizada()
        /// <summary>
        /// Verifica se o XML de Distribuição da Nota Fiscal (-procNFe) já está na pasta de Notas Autorizadas
        /// </summary>
        /// <param name="Arquivo">Arquivo XML a ser verificado</param>
        /// <param name="Emissao">Data de emissão da NFe</param>
        /// <param name="Extensao">Extensão a ser verificada (ExtXml.Nfe ou ExtXmlRet.ProcNFe)</param>
        /// <returns>Se está na pasta de XML´s autorizados</returns>
        public bool EstaAutorizada(string Arquivo, DateTime Emissao, string Extensao)
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index;

            string strNomePastaEnviado = Empresa.Configuracoes[emp].PastaEnviado + "\\" + PastaEnviados.Autorizados.ToString() + "\\" + Empresa.Configuracoes[emp].DiretorioSalvarComo.ToString(Emissao);
            return File.Exists(strNomePastaEnviado + "\\" + this.ExtrairNomeArq(Arquivo, ExtXml.Nfe) + Extensao);
        }
        #endregion

        #region EstaDenegada()
        /// <summary>
        /// Verifica se o XML da nota fiscal já está na pasta de Notas Denegadas
        /// </summary>
        /// <param name="Arquivo">Arquivo XML a ser verificado</param>
        /// <param name="Emissao">Data de emissão da NFe</param>
        /// <returns>Se está na pasta de XML´s denegados</returns>
        public bool EstaDenegada(string Arquivo, DateTime Emissao)
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index;
            string strNomePastaEnviado = Empresa.Configuracoes[emp].PastaEnviado + "\\" + PastaEnviados.Denegados.ToString() + "\\" + Empresa.Configuracoes[emp].DiretorioSalvarComo.ToString(Emissao);
            return File.Exists(strNomePastaEnviado + "\\" + this.ExtrairNomeArq(Arquivo, ExtXml.Nfe) + "-den.xml");
        }
        #endregion

        #region ExecutaUniDanfe()
        /// <summary>
        /// Executa o aplicativo UniDanfe para gerar/imprimir o DANFE
        /// </summary>
        /// <param name="NomeArqXMLNFe">Nome do arquivo XML da NFe (final -nfe.xml)</param>
        /// <param name="DataEmissaoNFe">Data de emissão da NFe</param>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 03/02/2010
        /// </remarks>
        public void ExecutaUniDanfe(string NomeArqXMLNFe, DateTime DataEmissaoNFe)
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index;

            //Disparar a geração/impressçao do UniDanfe. 03/02/2010 - Wandrey
            if (Empresa.Configuracoes[emp].PastaExeUniDanfe != string.Empty &&
                File.Exists(Empresa.Configuracoes[emp].PastaExeUniDanfe + "\\unidanfe.exe"))
            {
                string strNomePastaEnviado = Empresa.Configuracoes[emp].PastaEnviado + "\\" + PastaEnviados.Autorizados.ToString() + "\\" + Empresa.Configuracoes[emp].DiretorioSalvarComo.ToString(DataEmissaoNFe);
                string strArqProcNFe = strNomePastaEnviado + "\\" + this.ExtrairNomeArq(this.ExtrairNomeArq(NomeArqXMLNFe, ExtXml.Nfe) + ExtXmlRet.ProcNFe, ".xml") + ".xml";

                if (File.Exists(strArqProcNFe))
                {
                    string Args = "A=\"" + strArqProcNFe + "\"";
                    if (Empresa.Configuracoes[emp].PastaConfigUniDanfe != string.Empty)
                    {
                        Args += " PC=\"" + Empresa.Configuracoes[emp].PastaConfigUniDanfe + "\"";
                    }

                    System.Diagnostics.Process.Start(Empresa.Configuracoes[emp].PastaExeUniDanfe + "\\unidanfe.exe", Args);
                }
            }
        }
        #endregion

        #region CarregaUF()
        /// <summary>
        /// Carrega os Estados que possuem serviço de NFE já disponível. Estes Estados são carregados a partir do XML Webservice.xml que fica na pasta do executável do UNINFE
        /// </summary>
        /// <returns>Retorna a lista de UF e seus ID´s</returns>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 01/03/2010
        /// </remarks>
        public static ArrayList CarregaUF()
        {
            ArrayList UF = new ArrayList();

            string ArqXML = InfoApp.PastaExecutavel() + "\\Webservice.xml";

            if (File.Exists(ArqXML))
            {
                XmlTextReader oLerXml = null;
                try
                {
                    //Carregar os dados do arquivo XML de configurações da Aplicação
                    oLerXml = new XmlTextReader(ArqXML);

                    while (oLerXml.Read())
                    {
                        if (oLerXml.NodeType == XmlNodeType.Element)
                        {
                            if (oLerXml.Name == "Estado" && Convert.ToInt32(oLerXml.GetAttribute("ID")) < 900)
                            {
                                UF.Add(new ComboElem(oLerXml.GetAttribute("UF"), Convert.ToInt32(oLerXml.GetAttribute("ID")), oLerXml.GetAttribute("Nome")));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {
                    if (oLerXml != null)
                        oLerXml.Close();
                }
            }

            UF.Sort(new OrdenacaoPorNome());

            return UF;
        }

        #region CarregaEmpresa()
        /// <summary>
        /// Carrega as Emoresas que foram cadastradas e estão gravadas no XML
        /// </summary>
        /// <returns>Retorna uma ArrayList das empresas cadastradas</returns>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 28/07/2010
        /// </remarks>
        public static ArrayList CarregaEmpresa()
        {
            ArrayList empresa = new ArrayList();

            string arqXML = InfoApp.NomeArqEmpresa;

            if (File.Exists(arqXML))
            {
                XmlTextReader oLerXml = null;
                try
                {
                    //Carregar os dados do arquivo XML de configurações da Aplicação
                    oLerXml = new XmlTextReader(arqXML);

                    while (oLerXml.Read())
                    {
                        if (oLerXml.NodeType == XmlNodeType.Element)
                        {
                            if (oLerXml.Name.Equals("Registro"))
                            {
                                string cnpj = oLerXml.GetAttribute("CNPJ");

                                while (oLerXml.Read())
                                {
                                    if (oLerXml.NodeType == XmlNodeType.Element && oLerXml.Name.Equals("Nome"))
                                    {
                                        oLerXml.Read();
                                        string nome = oLerXml.Value;
                                        empresa.Add(new ComboElem(cnpj, 1, nome));
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {
                    if (oLerXml != null)
                        oLerXml.Close();
                }
            }

            empresa.Sort(new OrdenacaoPorNome());

            return empresa;
        }
        #endregion

        /// <summary>
        /// Carrega os Estados que possuem serviço de NFE já disponível. Estes Estados são carregados a partir do XML Webservice.xml que fica na pasta do executável do UNINFE
        /// Método modificado do original para retornar uma lista da classe UF que foi criada.
        /// </summary>
        /// <returns>Retorna a lista de UF</returns>
        /// <remarks>
        /// Autor: Márcio Fábio Althmann
        /// Data: 06/04/2010
        /// </remarks>
        public static List<UF> CarregarUF()
        {
            List<UF> listaUf = new List<UF>();

            string arquivoXml = String.Format(@"{0}\Webservice.xml", InfoApp.PastaExecutavel());

            if (!File.Exists(arquivoXml))
                return null;

            using (XmlTextReader xml = new XmlTextReader(arquivoXml))
            {
                while (xml.Read())
                {
                    if (xml.NodeType == XmlNodeType.Element)
                    {
                        if (xml.Name.Equals("Estado") && int.Parse(xml.GetAttribute("ID")) < 900)
                        {
                            UF uf = new UF();
                            uf.Id = int.Parse(xml.GetAttribute("ID"));
                            uf.Uf = xml.GetAttribute("UF");
                            uf.Nome = xml.GetAttribute("Nome");
                            listaUf.Add(uf);
                        }
                    }
                }
            }

            if (listaUf.Count > 0)
                listaUf.Sort(delegate(UF uf, UF uf2)
                {
                    return uf.Nome.CompareTo(uf2.Nome);
                });

            return listaUf;
        }

        #endregion

        #region getDateTime()
        public static DateTime getDateTime(string value)
        {
            if (string.IsNullOrEmpty(value))
                return DateTime.MinValue;

            int _ano = Convert.ToInt16(value.Substring(0, 4));
            int _mes = Convert.ToInt16(value.Substring(5, 2));
            int _dia = Convert.ToInt16(value.Substring(8, 2));
            if (value.Contains("T") && value.Contains(":"))
            {
                int _hora = Convert.ToInt16(value.Substring(11, 2));
                int _min = Convert.ToInt16(value.Substring(14, 2));
                int _seg = Convert.ToInt16(value.Substring(17, 2));
                return new DateTime(_ano, _mes, _dia, _hora, _min, _seg);
            }
            return new DateTime(_ano, _mes, _dia);
        }
        #endregion

        #region OnlyNumbers
        /// <summary>
        /// Remove caracteres não-numéricos e retorna.
        /// </summary>
        /// <param name="text">valor a ser convertido</param>
        /// <returns>somente números com decimais</returns>
        public static object OnlyNumbers(object text)
        {
            bool flagNeg = false;

            if (text == null || text.ToString().Length == 0) return 0;
            string ret = "";

            foreach (char c in text.ToString().ToCharArray())
            {
                if (c.Equals('.') == true || c.Equals(',') == true || char.IsNumber(c) == true)
                    ret += c.ToString();
                else if (c.Equals('-') == true)
                    flagNeg = true;
            }

            if (flagNeg == true) ret = "-" + ret;

            return ret;
        }
        #endregion

        #region OnlyNumbers - Sobrecarga
        /// <summary>
        /// Remove caracteres não-numéricos e retorna.
        /// </summary>
        /// <param name="text">valor a ser convertido</param>
        /// <param name="additionalChars">caracteres adicionais a serem removidos</param>
        /// <returns>somente números com decimais</returns>
        public static object OnlyNumbers(object text, string removeChars)
        {
            string ret = OnlyNumbers(text).ToString();

            foreach (char c in removeChars.ToCharArray())
            {
                ret = ret.Replace(c.ToString(), "");
            }

            return ret;
        }
        #endregion

        #region ConversaoNovaVersao()
        /// <summary>
        /// Conversões que são executadas quando atualizado o aplicativo.
        /// Alguns ajustes que são necessários serem executados automaticamente
        /// para evitar falhas no aplicativo
        /// </summary>
        public static string ConversaoNovaVersao(string cnpjEmpresa)    //danasa 20-9-2010
        {
            #region Conversão referente a parte de Multi-Empresas
            try
            {
                if (!File.Exists(InfoApp.NomeArqEmpresa) && File.Exists(InfoApp.PastaExecutavel() + "\\" + InfoApp.NomeArqConfig))
                {
                    #region Localizar o CNPJ da empresa no certificado
                    string certificado = string.Empty;
                    string nomeEmpresa = string.Empty;  //danasa 20-9-2010

                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(InfoApp.PastaExecutavel() + "\\" + InfoApp.NomeArqConfig);
                    var configList = xmlDoc.GetElementsByTagName("nfe_configuracoes");
                    foreach (XmlNode configNode in configList)
                    {
                        var configElemento = (XmlElement)configNode;

                        if (configElemento.GetElementsByTagName("CertificadoDigital")[0] != null)
                            certificado = configElemento.GetElementsByTagName("CertificadoDigital")[0].InnerText;
                    }

                    string[] dados = certificado.Split(new char[] { ',', ':' });
                    foreach (string dado in dados)
                    {
                        if (cnpjEmpresa == string.Empty)  //danasa 20-9-2010
                            if (CNPJ.Validate((string)Auxiliar.OnlyNumbers(dado.TrimStart())))
                            {
                                cnpjEmpresa = (string)Auxiliar.OnlyNumbers(dado.TrimStart());
                            }

                        /// danasa 20-9-2010
                        /// use o TrimStart() pois em "dado" está retornando branco no inicio
                        if (dado.TrimStart().StartsWith("CN="))
                        {
                            nomeEmpresa = dado.TrimStart().Substring(3, dado.TrimStart().Length - 3);
                        }
                    }
                    if (cnpjEmpresa == string.Empty || nomeEmpresa == string.Empty) //danasa 20-9-2010
                    {
                        if (nomeEmpresa == string.Empty)
                            throw new Exception("Não foi possível localizar o CNPJ da empresa no certificado configurado, sendo assim as configurações do aplicativo deverão ser realizadas novamente.");

                        /// danasa 20-9-2010
                        /// retorna o nome da empresa ao MainForm para exibir na tela de solicitacao do CNPJ
                        return nomeEmpresa;
                    }
                    #endregion

                    #region Criar o diretório das configurações da empresa
                    string dirEmpresa = InfoApp.PastaExecutavel().Trim() + "\\" + cnpjEmpresa;
                    if (!Directory.Exists(dirEmpresa))
                        Directory.CreateDirectory(dirEmpresa);
                    #endregion

                    #region Copiar o arquivo de configurações para a pasta da empresa
                    string arqConfigOrigem = InfoApp.PastaExecutavel() + "\\" + InfoApp.NomeArqConfig;
                    string arqConfigDestino = dirEmpresa + "\\" + InfoApp.NomeArqConfig;
                    if (!File.Exists(arqConfigDestino))
                    {
                        File.Copy(arqConfigOrigem, arqConfigDestino);

                        if (File.Exists(InfoApp.PastaExecutavel() + "\\" + InfoApp.NomeArqXmlLote))
                        {
                            File.Copy(InfoApp.PastaExecutavel() + "\\" + InfoApp.NomeArqXmlLote, dirEmpresa + "\\" + InfoApp.NomeArqXmlLote, true);
                            File.Delete(InfoApp.PastaExecutavel() + "\\" + InfoApp.NomeArqXmlLote);
                        }

                        if (File.Exists(InfoApp.PastaExecutavel() + "\\" + InfoApp.NomeArqXmlLoteBkp1))
                        {
                            File.Copy(InfoApp.PastaExecutavel() + "\\" + InfoApp.NomeArqXmlLoteBkp1, dirEmpresa + "\\" + InfoApp.NomeArqXmlLoteBkp1, true);
                            File.Delete(InfoApp.PastaExecutavel() + "\\" + InfoApp.NomeArqXmlLoteBkp1);
                        }

                        if (File.Exists(InfoApp.PastaExecutavel() + "\\" + InfoApp.NomeArqXmlLoteBkp2))
                        {
                            File.Copy(InfoApp.PastaExecutavel() + "\\" + InfoApp.NomeArqXmlLoteBkp2, dirEmpresa + "\\" + InfoApp.NomeArqXmlLoteBkp2, true);
                            File.Delete(InfoApp.PastaExecutavel() + "\\" + InfoApp.NomeArqXmlLoteBkp2);
                        }

                        if (File.Exists(InfoApp.PastaExecutavel() + "\\" + InfoApp.NomeArqXmlLoteBkp3))
                        {
                            File.Copy(InfoApp.PastaExecutavel() + "\\" + InfoApp.NomeArqXmlLoteBkp3, dirEmpresa + "\\" + InfoApp.NomeArqXmlLoteBkp3, true);
                            File.Delete(InfoApp.PastaExecutavel() + "\\" + InfoApp.NomeArqXmlLoteBkp3);
                        }

                        if (File.Exists(InfoApp.PastaExecutavel() + "\\" + InfoApp.NomeArqXmlFluxoNfe))
                        {
                            File.Copy(InfoApp.PastaExecutavel() + "\\" + InfoApp.NomeArqXmlFluxoNfe, dirEmpresa + "\\" + InfoApp.NomeArqXmlFluxoNfe, true);
                            File.Delete(InfoApp.PastaExecutavel() + "\\" + InfoApp.NomeArqXmlFluxoNfe);
                        }
                    }

                    #endregion

                    #region Criar o XML do cadastro de empresas
                    XmlWriterSettings oSettings = new XmlWriterSettings();
                    UTF8Encoding c = new UTF8Encoding(false);

                    //Para começar, vamos criar um XmlWriterSettings para configurar nosso XML
                    oSettings.Encoding = c;
                    oSettings.Indent = true;
                    oSettings.IndentChars = "";
                    oSettings.NewLineOnAttributes = false;
                    oSettings.OmitXmlDeclaration = false;

                    try
                    {
                        //Agora vamos criar um XML Writer
                        XmlWriter oXmlGravar = XmlWriter.Create(InfoApp.NomeArqEmpresa, oSettings);

                        //Agora vamos gravar os dados
                        oXmlGravar.WriteStartDocument();
                        oXmlGravar.WriteStartElement("Empresa");

                        try
                        {
                            //Abrir a tag <Registro>
                            oXmlGravar.WriteStartElement("Registro");

                            //Criar o atributo CNPJ dentro da tag Registro
                            oXmlGravar.WriteStartAttribute("CNPJ");

                            //Setar o conteúdo do atributo CNPJ
                            oXmlGravar.WriteString(cnpjEmpresa.Trim());

                            //Encerrar o atributo CNPJ
                            oXmlGravar.WriteEndAttribute(); // Encerrar o atributo CNPJ

                            //Criar a tag <Nome> com seu conteúdo </Nome>
                            oXmlGravar.WriteElementString("Nome", nomeEmpresa.Trim());

                            //Encerrar a tag </Registro>
                            oXmlGravar.WriteEndElement();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ocorreu um erro ao tentar gravar as empresas cadastradas.\r\n\r\nErro: " + ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        oXmlGravar.WriteEndElement(); //Encerrar o elemento Empresa
                        oXmlGravar.WriteEndDocument();
                        oXmlGravar.Flush();
                        oXmlGravar.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro ao tentar gravar as empresas cadastradas.\r\n\r\nErro: " + ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro na hora de converter o aplicativo para multiempresas.\r\n\r\nErro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return "";
            #endregion
        }
        #endregion

        #region DefinirProxy()
        /// <summary>
        /// Efetua as definições do proxy
        /// </summary>
        /// <returns>Retorna as definições do Proxy</returns>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>29/09/2009</date>
        public static System.Net.IWebProxy DefinirProxy()
        {
            System.Net.NetworkCredential Usuario = new System.Net.NetworkCredential(ConfiguracaoApp.ProxyUsuario, ConfiguracaoApp.ProxySenha);
            System.Net.IWebProxy Proxy;
            Proxy = new System.Net.WebProxy(ConfiguracaoApp.ProxyServidor, ConfiguracaoApp.ProxyPorta);

            if (!String.IsNullOrEmpty(ConfiguracaoApp.ProxyUsuario.Trim()) && ConfiguracaoApp.ProxyUsuario.Trim().Length > 0)
            {
                Proxy.Credentials = Usuario;
            }

            return Proxy;
        }
        #endregion
    }

    #region infCad & RetConsCad
    public class enderConsCadInf
    {
        public string xLgr { get; set; }
        public string nro { get; set; }
        public string xCpl { get; set; }
        public string xBairro { get; set; }
        public int cMun { get; set; }
        public string xMun { get; set; }
        public int CEP { get; set; }
    }
    public class infCad
    {
        public string IE { get; set; }
        public string CNPJ { get; set; }
        public string CPF { get; set; }
        public string UF { get; set; }
        public string xNome { get; set; }
        public string xFant { get; set; }
        public string IEAtual { get; set; }
        public string IEUnica { get; set; }
        public DateTime dBaixa { get; set; }
        public DateTime dUltSit { get; set; }
        public DateTime dIniAtiv { get; set; }
        public int CNAE { get; set; }
        public string xRegApur { get; set; }
        public string cSit { get; set; }
        public enderConsCadInf ender { get; set; }

        public infCad()
        {
            ender = new enderConsCadInf();
        }
    }

    public class RetConsCad
    {
        public int cStat { get; set; }
        public string xMotivo { get; set; }
        public string UF { get; set; }
        public string IE { get; set; }
        public string CNPJ { get; set; }
        public string CPF { get; set; }
        public DateTime dhCons { get; set; }
        public Int32 cUF { get; set; }
        public List<infCad> infCad { get; set; }

        public RetConsCad()
        {
            infCad = new List<infCad>();
        }
    }
    #endregion


    /// <summary>
    /// danasa 21/10/2010
    /// </summary>
    public class URLws
    {
        public string NFeRecepcao { get; set; }
        public string NFeRetRecepcao { get; set; }
        public string NFeCancelamento { get; set; }
        public string NFeInutilizacao { get; set; }
        /// <summary>
        /// Consulta da Situação da NFe versão 2.0
        /// </summary>
        public string NFeConsulta { get; set; }
        /// <summary>
        /// Consulta Situação da NFe na versão 1.10
        /// </summary>
        public string NFeConsulta1 { get; set; }
        public string NFeStatusServico { get; set; }
        public string NFeConsultaCadastro { get; set; }
    }

    public class webServices
    {
        public int ID { get; private set; }
        public string Nome { get; private set; }
        public string UF { get; private set; }
        public URLws URLHomologacao { get; private set; }
        public URLws URLProducao { get; private set; }
        public URLws LocalHomologacao { get; private set; }
        public URLws LocalProducao { get; private set; }

        public webServices(int id, string nome, string uf)
        {
            URLHomologacao = new URLws();
            URLProducao = new URLws();
            LocalHomologacao = new URLws();
            LocalProducao = new URLws();
            ID = id;
            Nome = nome;
            UF = uf;
        }
    }
}