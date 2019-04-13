using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Windows.Forms;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;

namespace UniNFeLibrary
{
    public class AssinaturaDigital
    {
        /// <summary>
        /// String do XML assinado
        /// </summary>
        public string vXMLStringAssinado { get; private set; }
        
        private XmlDocument XMLDoc;

        /// <summary>
        /// O método assina digitalmente o arquivo XML passado por parâmetro e 
        /// grava o XML assinado com o mesmo nome, sobreponto o XML informado por parâmetro.
        /// Disponibiliza também uma propriedade com uma string do xml assinado (this.vXmlStringAssinado)
        /// </summary>
        /// <param name="strArqXMLAssinar">Nome do arquivo XML a ser assinado</param>
        /// <param name="strUri">URI (TAG) a ser assinada</param>
        /// <param name="x509Certificado">Certificado a ser utilizado na assinatura</param>
        /// <param name="strArqXMLAssinado">Nome do arquivo XML assinado a ser gravado</param>
        /// <remarks>
        /// Podemos pegar como retorno do método as seguintes propriedades:
        /// 
        /// - Atualiza a propriedade this.vXMLStringAssinado com a string de
        ///   xml já assinada
        ///   
        /// - Grava o XML assinado com o nome passado para o parâmetro strArqXMLAssinado
        ///   
        /// - Aguns excessões que podem ser geradas
        ///   1 - Erro: Problema ao acessar o certificado digital - %exceção%
        ///   2 - Problemas no certificado digital
        ///   3 - XML mal formado + %exceção%
        ///   4 - A tag de assinatura %pUri% não existe 
        ///   5 - A tag de assinatura %pUri% não é unica
        ///   6 - Erro ao assinar o documento - %exceção%
        ///   7 - Falha ao tentar abrir o arquivo XML - %exceção%
        /// </remarks>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>04/06/2008</date>
        public void Assinar(string strArqXMLAssinar, string strUri, X509Certificate2 x509Certificado, string strArqXMLAssinado)
        {
            StreamReader SR = null;

            try
            {
                //Abrir o arquivo XML a ser assinado e ler o seu conteúdo
                SR = File.OpenText(strArqXMLAssinar);
                string vXMLString = SR.ReadToEnd();
                SR.Close();

                try
                {
                    // Verifica o certificado a ser utilizado na assinatura
                    string _xnome = "";
                    if (x509Certificado != null)
                    {
                        _xnome = x509Certificado.Subject.ToString();
                    }

                    X509Certificate2 _X509Cert = new X509Certificate2();
                    X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
                    store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                    X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                    X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindBySubjectDistinguishedName, _xnome, false);

                    if (collection1.Count == 0)
                    {
                        throw new Exception("O UniNFe não conseguiu localizar nenhum certificado digital para assinatura e/ou envio do XML. Verifique as configurações.");
                    }
                    else
                    {
                        //Verificar a validade do certificado
                        _X509Cert = null;
                        for (int i = 0; i < collection1.Count; i++)
                        {
                            //Verificar a validade do certificado
                            if (DateTime.Compare(DateTime.Now, collection1[i].NotAfter) == -1)
                            {
                                _X509Cert = collection1[i];
                                break;
                            }
                        }

                        //Se não encontrou nenhum certificado com validade correta, vou pegar o primeiro certificado, porem vai travar na hora de tentar enviar a nota fiscal, por conta da validade. Wandrey 06/04/2011
                        if (_X509Cert == null)
                            _X509Cert = collection1[0];

                        string x;
                        x = _X509Cert.GetKeyAlgorithm().ToString();

                        //Normalmente não consegue acessar no certificado A3, por que falta a digitação do PIN
                        //if (_X509Cert.PrivateKey == null)
                        //    throw new Exception("Não foi possível acessar a chave privada do certificado digital.");

                        // Create a new XML document.
                        XmlDocument doc = new XmlDocument();

                        // Format the document to ignore white spaces.
                        doc.PreserveWhitespace = false;

                        // Load the passed XML file using it’s name.
                        try
                        {
                            doc.LoadXml(vXMLString);

                            // Verifica se a tag a ser assinada existe é única
                            int qtdeRefUri = doc.GetElementsByTagName(strUri).Count;

                            if (qtdeRefUri == 0)
                            {
                                // a URI indicada não existe
                                throw new Exception("A tag de assinatura " + strUri.Trim() + " não existe no XML. (Código do Erro: 4)");
                            }
                            // Exsiste mais de uma tag a ser assinada
                            else
                            {
                                if (qtdeRefUri > 1)
                                {
                                    // existe mais de uma URI indicada
                                    throw new Exception("A tag de assinatura " + strUri.Trim() + " não é unica. (Código do Erro: 5)");
                                }
                                else
                                {
                                    if (doc.GetElementsByTagName("Signature").Count == 0) //Documento ainda não assinado (Se já tiver assinado não vamos fazer nada, somente retornar ok para continuar o envio). Wandrey 12/05/2009
                                    {
                                        try
                                        {
                                            // Create a SignedXml object.
                                            SignedXml signedXml = new SignedXml(doc);

                                            // Add the key to the SignedXml document
                                            signedXml.SigningKey = _X509Cert.PrivateKey;

                                            // Create a reference to be signed
                                            Reference reference = new Reference();

                                            // pega o uri que deve ser assinada
                                            XmlAttributeCollection _Uri = doc.GetElementsByTagName(strUri).Item(0).Attributes;
                                            foreach (XmlAttribute _atributo in _Uri)
                                            {
                                                if (_atributo.Name == "Id")
                                                {
                                                    reference.Uri = "#" + _atributo.InnerText;
                                                }
                                            }

                                            // Add an enveloped transformation to the reference.
                                            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
                                            reference.AddTransform(env);

                                            XmlDsigC14NTransform c14 = new XmlDsigC14NTransform();
                                            reference.AddTransform(c14);

                                            // Add the reference to the SignedXml object.
                                            signedXml.AddReference(reference);

                                            // Create a new KeyInfo object
                                            KeyInfo keyInfo = new KeyInfo();

                                            // Load the certificate into a KeyInfoX509Data object
                                            // and add it to the KeyInfo object.
                                            keyInfo.AddClause(new KeyInfoX509Data(_X509Cert));

                                            // Add the KeyInfo object to the SignedXml object.
                                            signedXml.KeyInfo = keyInfo;
                                            signedXml.ComputeSignature();

                                            // Get the XML representation of the signature and save
                                            // it to an XmlElement object.
                                            XmlElement xmlDigitalSignature = signedXml.GetXml();

                                            // Gravar o elemento no documento XML
                                            doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));
                                            XMLDoc = new XmlDocument();
                                            XMLDoc.PreserveWhitespace = false;
                                            XMLDoc = doc;

                                            // Atualizar a string do XML já assinada
                                            this.vXMLStringAssinado = XMLDoc.OuterXml;

                                            // Gravar o XML Assinado no HD
                                            StreamWriter SW_2 = File.CreateText(strArqXMLAssinado);
                                            SW_2.Write(this.vXMLStringAssinado);
                                            SW_2.Close();
                                        }
                                        catch (Exception caught)
                                        {
                                            throw (caught);
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception caught)
                        {
                            throw (caught);
                        }
                    }
                }
                catch (Exception caught)
                {
                    throw (caught);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                SR.Close();
            }
        }

        /// <summary>
        /// Assina o XML sobrepondo-o
        /// </summary>
        /// <param name="strArqXMLAssinar">Nome do arquivo XML a ser assinado</param>
        /// <param name="strUri">URI (TAG) a ser assinada</param>
        /// <param name="x509Certificado">Certificado a ser utilizado na assinatura</param>
        /// <remarks>Veja anotações na sobrecarga deste método</remarks>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>16/04/2009</date>
        public void Assinar(string strArqXMLAssinar, string strUri, X509Certificate2 x509Certificado)
        {
            try
            {
                this.Assinar(strArqXMLAssinar, strUri, x509Certificado, strArqXMLAssinar);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
