using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Windows.Forms;
using System.Collections.Generic;

namespace UniNFeLibrary
{
    /// <summary>
    /// Classe de validação dos XML´s
    /// </summary>
    public class ValidarXMLs
    {
        public int Retorno { get; private set; }
        public string RetornoString { get; private set; }
        public int nRetornoTipoArq { get; private set; }
        public string cRetornoTipoArq { get; private set; }
        public string cArquivoSchema { get; private set; }
        /// <summary>
        /// Pasta dos schemas para validação do XML
        /// </summary>
        private string PastaSchema = InfoApp.PastaSchemas();
        /// <summary>
        /// Tag que deve ser assinada no XML, se o conteúdo estiver em branco é por que o XML não deve ser assinado
        /// </summary>
        public string TagAssinar { get; private set; }

        private string cErro;

        /// <summary>
        /// Método responsável por validar os arquivos XML de acordo com a estrutura determinada pelos schemas (XSD)
        /// </summary>
        /// <param name="cRotaArqXML">
        /// Rota e nome do arquivo XML que é para ser validado. Exemplo:
        /// c:\unimake\uninfe\envio\teste-nfe.xml
        /// </param>
        /// <param name="cRotaArqSchema">
        /// Rota e nome do arquivo XSD que é para ser utilizado para validar o XML. Exemplo:
        /// c:\unimake\uninfe\schemas\nfe_v1.10.xsd
        /// </param>
        /// <example>
        /// ValidadorXMLClass oValidarXML = new ValidadorXMLClass;
        /// oValidarXML(@"c:\unimake\uninfe\teste-nfe.xml", @"c:\unimake\uninfe\schemas\nfe_v1.10.xsd")
        /// if (this.Retorno == 0)
        /// {
        ///    MessageBox.Show("Validado com sucesso")
        /// }
        /// else
        /// {
        ///    MessageBox.Show("Ocorreu erro na validação: \r\n\r\n" + this.RetornoString)
        /// }
        /// </example>
        /// <date>31/07/2008</date>
        /// <by>Wandrey Mundin Ferreira</by>
        public void Validar(string cRotaArqXML, string cRotaArqSchema)
        {
            bool lArqXML = File.Exists(cRotaArqXML);
            var caminhoDoSchema = this.PastaSchema + "\\" + cRotaArqSchema;
            bool lArqXSD = File.Exists(caminhoDoSchema);

            if (lArqXML && lArqXSD)
            {
                //TODO: V3.0 - Colocar Try Catch
                StreamReader cStreamReader = new StreamReader(cRotaArqXML);
                XmlTextReader cXmlTextReader = new XmlTextReader(cStreamReader);
                XmlValidatingReader reader = new XmlValidatingReader(cXmlTextReader);

                // Criar um coleção de schema, adicionar o XSD para ela
                XmlSchemaCollection schemaCollection = new XmlSchemaCollection();
                schemaCollection.Add(ConfiguracaoApp.nsURI, caminhoDoSchema);

                // Adicionar a coleção de schema para o XmlValidatingReader
                reader.Schemas.Add(schemaCollection);

                // Wire up the call back.  The ValidationEvent is fired when the
                // XmlValidatingReader hits an issue validating a section of the xml
                reader.ValidationEventHandler += new ValidationEventHandler(reader_ValidationEventHandler);

                // Iterate through the xml document
                this.cErro = "";
                try
                {
                    while (reader.Read()) { }
                }
                catch (Exception ex)
                {
                    this.cErro = ex.Message;
                }

                reader.Close();

                this.Retorno = 0;
                this.RetornoString = "";
                if (cErro != "")
                {
                    this.Retorno = 1;
                    this.RetornoString = "Início da validação...\r\n\r\n";
                    this.RetornoString += "Arquivo XML: " + cRotaArqXML + "\r\n";
                    this.RetornoString += "Arquivo SCHEMA: " + caminhoDoSchema + "\r\n\r\n";
                    this.RetornoString += this.cErro;
                    this.RetornoString += "\r\n...Final da validação";
                }
            }
            else
            {
                if (lArqXML == false)
                {
                    this.Retorno = 2;
                    this.RetornoString = "Arquivo XML não foi encontrato";
                }
                else if (lArqXSD == false)
                {
                    this.Retorno = 3;
                    this.RetornoString = "Arquivo XSD (schema) não foi encontrado em " + caminhoDoSchema;
                }
            }
        }

        private void reader_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            this.cErro += "Linha: " + e.Exception.LineNumber + " Coluna: " + e.Exception.LinePosition + " Erro: " + e.Exception.Message + "\r\n";
        }

        /// <summary>
        /// Método responsável por retornar de que tipo é o arquivo XML.
        /// A principio o método retorna o tipo somente se é um XML for de:
        /// - Nota Fiscal Eletrônica
        /// - XML de envio de Lote de Notas Fiscais Eletrônicas
        /// - Cancelamento de Nota Fiscal Eletrônica
        /// - Inutilização de Numeração de Notas Fiscais Eletrônicas
        /// - Consulta da Situação da Nota Fiscal Eletrônica
        /// - Consulta do Recibo do Lote das Notas Fiscais Eletrônicas
        /// - Consulta do Status do Serviço da Nota Fiscal Eletrônica
        /// </summary>
        /// <param name="cRotaArqXML">
        /// Rota e nome do arquivo XML que é para ser retornado o tipo. Exemplo:
        /// c:\unimake\uninfe\envio\teste-nfe.xml
        /// </param>
        /// <remarks>
        /// Algumas propriedades são atualizadas para serem utilizadas como retorno, veja abaixo:
        /// 
        /// A propriedade this.nRetornoTipoArq vai receber um número identificando se foi possível identificar o arquivo ou não
        /// 1=Nota Fiscal Eletrônica
        /// 2=XML de envio de Lote de Notas Fiscais Eletrônicas
        /// 3=Cancelamento de Nota Fiscal Eletrônica
        /// 4=Inutilização de Numeração de Notas Fiscais Eletrônicas
        /// 5=Consulta da Situação da Nota Fiscal Eletrônica
        /// 6=Consulta Recibo da Nota Fiscal Eletrônica
        /// 7=Consulta do Status do Serviço da Nota Fiscal Eletrônica
        /// 
        /// 100=Arquivo XML não foi encontrato
        /// 101=Arquivo não foi identificado
        /// </remarks>
        /// <example>
        /// 
        /// oObj.TipoArquivoXML(@"c:\unimake\uninfe\teste-nfe.xml")
        /// if (oObj.nRetornoTipoArq == 1)
        /// {
        ///     MessageBox.Show("Nota Fiscal Eletrônica");
        /// }
        /// </example>
        /// <date>31/07/2008</date>
        /// <by>Wandrey Mundin Ferreira</by>
        public void TipoArquivoXML(string cRotaArqXML)
        {
            this.nRetornoTipoArq = 0;
            this.cRetornoTipoArq = string.Empty;
            this.cArquivoSchema = string.Empty;
            this.TagAssinar = string.Empty;

            try
            {
                if (File.Exists(cRotaArqXML))
                {
                    //Carregar os dados do arquivo XML de configurações do UniNfe
                    XmlTextReader oLerXml = null;

                    try
                    {
                        oLerXml = new XmlTextReader(cRotaArqXML);

                        while (oLerXml.Read())
                        {
                            if (oLerXml.NodeType == XmlNodeType.Element)
                            {
                                for (int i = 0; i < SchemaXML.lstXMLTag.Count; i++)
                                {
                                    if (SchemaXML.lstXMLTag[i] == oLerXml.Name)
                                    {
                                        this.nRetornoTipoArq = SchemaXML.lstXMLID[i];
                                        this.cRetornoTipoArq = SchemaXML.lstXMLTextoID[i];
                                        this.cArquivoSchema = SchemaXML.lstXMLSchema[i];
                                        this.TagAssinar = SchemaXML.lstXMLTagAssinar[i];
                                        break;
                                    }
                                }

                                if (this.nRetornoTipoArq != 0) //Arquivo XML já foi identificado
                                {
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        this.nRetornoTipoArq = 102;
                        this.cRetornoTipoArq = ex.Message;
                    }
                    finally
                    {
                        if (oLerXml != null)
                        {
                            if (oLerXml.ReadState != ReadState.Closed)
                            {
                                oLerXml.Close();
                            }
                        }
                    }
                }
                else
                {
                    this.nRetornoTipoArq = 100;
                    this.cRetornoTipoArq = "Arquivo XML não foi encontrado";
                }
            }
            catch (Exception ex)
            {
                this.nRetornoTipoArq = 103;
                this.cRetornoTipoArq = ex.Message;
            }

            if (this.nRetornoTipoArq == 0)
            {
                this.nRetornoTipoArq = 101;
                this.cRetornoTipoArq = "Não foi possível identificar o arquivo XML";
            }
        }
    }
}