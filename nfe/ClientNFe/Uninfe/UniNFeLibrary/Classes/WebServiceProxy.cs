using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services.Description;
using System.Xml;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Globalization;
using Microsoft.CSharp;
using System.Reflection;
using System.Xml.Serialization;

namespace UniNFeLibrary
{
    public class WebServiceProxy
    {
        #region Propriedades

        /// <summary>
        /// Descrição do serviço (WSDL)
        /// </summary>
        private ServiceDescription serviceDescription { get; set; }
        /// <summary>
        /// Código assembly do serviço
        /// </summary>
        private Assembly serviceAssemby { get; set; }
        /// <summary>
        /// Certificado digital a ser utilizado no consumo dos serviços
        /// </summary>
        private X509Certificate2 oCertificado { get; set; }

        #endregion

        #region Construtores

        public WebServiceProxy(Uri requestUri, X509Certificate2 Certificado)
        {
            //Definir o certificado digital que será utilizado na conexão com os serviços
            this.oCertificado = Certificado;

            //Confirmar a solicitação SSL automaticamente
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CertificateValidation);

            try
            {
                //Obeter a descrção do serviço (WSDL)
                this.DescricaoServico(requestUri, this.oCertificado);

                //Gerar e compilar a classe
                this.GerarClasse();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public WebServiceProxy(string arquivoWSDL, X509Certificate2 Certificado)
        {
            //Definir o certificado digital que será utilizado na conexão com os serviços
            this.oCertificado = Certificado;

            //Confirmar a solicitação SSL automaticamente
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CertificateValidation);

            try
            {
                //Obeter a descrção do serviço (WSDL)
                this.DescricaoServico(arquivoWSDL);

                //Gerar e compilar a classe
                this.GerarClasse();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion

        #region Métodos públicos

        #region ReturnArray()
        /// <summary>
        /// Método que verifica se o tipo de retornjo de uma operação/método é array ou não
        /// </summary>
        /// <param name="Instance">Instancia do objeto</param>
        /// <param name="methodName">Nome do método</param>
        /// <returns>true se o tipo de retorno do método passado por parâmetro for um array</returns>
        public bool ReturnArray(object Instance, string methodName)
        {
            Type tipoInstance = Instance.GetType();

            return tipoInstance.GetMethod(methodName).ReturnType.IsSubclassOf(typeof(Array));
        }
        #endregion

        #region Invoke()
        /// <summary>
        /// Invocar o método da classe
        /// </summary>
        /// <param name="Instance">Instância do objeto</param>
        /// <param name="methodName">Nome do método</param>
        /// <param name="parameters">Objeto com o conteúdo dos parâmetros do método</param>
        /// <returns>Objeto - Um objeto somente, podendo ser primário ou complexo</returns>
        public object Invoke(object Instance, string methodName, object[] parameters)
        {
            //Relacionar o certificado digital que será utilizado no serviço que será consumido do webservice
            Type tipoInstance = Instance.GetType();
            object oClientCertificates = tipoInstance.InvokeMember("ClientCertificates", System.Reflection.BindingFlags.GetProperty, null, Instance, new Object[] { });
            Type tipoClientCertificates = oClientCertificates.GetType();
            tipoClientCertificates.InvokeMember("Add", System.Reflection.BindingFlags.InvokeMethod, null, oClientCertificates, new Object[] { this.oCertificado });

            //Invocar método do serviço
            return tipoInstance.GetMethod(methodName).Invoke(Instance, parameters);
        }
        #endregion

        #region InvokeXML()
        /// <summary>
        /// Invocar o método da classe
        /// </summary>
        /// <param name="Instance">Instância do objeto</param>
        /// <param name="methodName">Nome do método</param>
        /// <param name="parameters">Objeto com o conteúdo dos parâmetros do método</param>
        /// <returns>Um objeto do tipo XML</returns>
        public XmlNode InvokeXML(object Instance, string methodName, object[] parameters)
        {
            //Relacionar o certificado digital que será utilizado no serviço que será consumido do webservice
            Type tipoInstance = Instance.GetType();
            object oClientCertificates = tipoInstance.InvokeMember("ClientCertificates", System.Reflection.BindingFlags.GetProperty, null, Instance, new Object[] { });
            Type tipoClientCertificates = oClientCertificates.GetType();
            tipoClientCertificates.InvokeMember("Add", System.Reflection.BindingFlags.InvokeMethod, null, oClientCertificates, new Object[] { this.oCertificado });

            //Invocar método do serviço
            return (XmlNode)tipoInstance.GetMethod(methodName).Invoke(Instance, parameters);
        }
        #endregion

        #region InvokeXML()
        /// <summary>
        /// Invocar o método da classe
        /// </summary>
        /// <param name="Instance">Instância do objeto</param>
        /// <param name="methodName">Nome do método</param>
        /// <param name="parameters">Objeto com o conteúdo dos parâmetros do método</param>
        /// <returns>Um objeto do tipo string</returns>
        public string InvokeStr(object Instance, string methodName, object[] parameters)
        {
            //Relacionar o certificado digital que será utilizado no serviço que será consumido do webservice
            Type tipoInstance = Instance.GetType();
            object oClientCertificates = tipoInstance.InvokeMember("ClientCertificates", System.Reflection.BindingFlags.GetProperty, null, Instance, new Object[] { });
            Type tipoClientCertificates = oClientCertificates.GetType();
            tipoClientCertificates.InvokeMember("Add", System.Reflection.BindingFlags.InvokeMethod, null, oClientCertificates, new Object[] { this.oCertificado });

            //Invocar método do serviço
            return (string)tipoInstance.GetMethod(methodName).Invoke(Instance, parameters);
        }
        #endregion

        #region SetProp()
        /// <summary>
        /// Alterar valor das propriedades da classe
        /// </summary>
        /// <param name="Instance">Instância do objeto</param>
        /// <param name="fieldName">Nome da propriedade</param>
        /// <param name="novoValor">Novo valor para ser gravado na propriedade</param>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 09/02/2010
        /// </remarks>
        public void SetProp(object Instance, string propertyName, object novoValor)
        {
            Type tipoInstance = Instance.GetType();
            PropertyInfo property = tipoInstance.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

            property.SetValue(Instance, novoValor, null);
        }
        #endregion

        #region InvokeArray()
        /// <summary>
        /// Invocar o método da classe
        /// </summary>
        /// <param name="Instance">Instância do objeto</param>
        /// <param name="methodName">Nome do método</param>
        /// <param name="parameters">Objeto com o conteúdo dos parâmetros do método</param>
        /// <returns>Vetor de objetos - uma lista de objetos primários ou complexos</returns>
        public object[] InvokeArray(object Instance, string methodName, object[] parameters)
        {
            //Relacionar o certificado digital que será utilizado no serviço que será consumido do webservice
            Type tipoInstance = Instance.GetType();
            object oClientCertificates = tipoInstance.InvokeMember("ClientCertificates", System.Reflection.BindingFlags.GetProperty, null, Instance, new Object[] { });
            Type tipoClientCertificates;
            tipoClientCertificates = oClientCertificates.GetType();
            tipoClientCertificates.InvokeMember("Add", System.Reflection.BindingFlags.InvokeMethod, null, oClientCertificates, new Object[] { this.oCertificado });

            //Invocar método do serviço
            return (object[])tipoInstance.GetMethod(methodName).Invoke(Instance, parameters);
        }
        #endregion

        #region CertificateValidation
        /// <summary>
        /// Responsável por retornar uma confirmação verdadeira para a proriedade ServerCertificateValidationCallback 
        /// da classe ServicePointManager para confirmar a solicitação SSL automaticamente.
        /// </summary>
        /// <returns>Retornará sempre true</returns>
        public bool CertificateValidation(object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErros)
        {
            return true;
        }
        #endregion

        #region CriarObjeto()
        /// <summary>
        /// Criar objeto das classes do serviço
        /// </summary>
        /// <param name="NomeClasse">Nome da classe que é para ser instanciado um novo objeto</param>
        /// <returns>Retorna o objeto</returns>
        public object CriarObjeto(string NomeClasse)
        {
            try
            {
                return Activator.CreateInstance(this.serviceAssemby.GetType(NomeClasse));
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #endregion

        #region Métodos privados

        #region DescricaoServico()
        /// <summary>
        /// Obter a descrição completa do serviço, ou seja, o WSDL do webservice a partir de uma URL
        /// </summary>
        /// <param name="requestUri">Uri (endereço https) para obter o WSDL</param>
        /// <param name="Certificado">Certificado digital</param>
        private void DescricaoServico(Uri requestUri, X509Certificate2 Certificado)
        {
            try
            {
                //Forçar utilizar o protocolo SSL 3.0 que está de acordo com o manual de integração do SEFAZ
                //Wandrey 31/03/2010
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

                //Definir o endereço para a requisição do wsdl
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);

                //Definir dados para conexão com Proxy. Wandrey 22/11/2010
                if (ConfiguracaoApp.Proxy)
                {
                    request.Proxy = Auxiliar.DefinirProxy();
                }

                //Definir o certificado digital que deve ser utilizado na requisição do wsdl
                request.ClientCertificates.Add(Certificado);

                //Requisitar o WSDL e gravar em um stream                
                Stream stream = request.GetResponse().GetResponseStream();

                //Definir a descrição completa do servido (WSDL)
                this.serviceDescription = ServiceDescription.Read(stream);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region DescricaoServico()
        /// <summary>
        /// Obter a descrição completa do serviço, ou seja, o WSDL do webservice de um arquivo local
        /// </summary>
        /// <param name="arquivoWSDL">Local e nome do arquivo WDDL</param>
        /// <param name="Certificado">Certificado digital</param>
        private void DescricaoServico(string arquivoWSDL)
        {
            try
            {
                //Forçar utilizar o protocolo SSL 3.0 que está de acordo com o manual de integração do SEFAZ
                //Wandrey 31/03/2010
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

                //Definir a descrição completa do servido (WSDL)
                //this.serviceDescription = ServiceDescription.Read(stream);
                this.serviceDescription = ServiceDescription.Read(arquivoWSDL);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region GerarClasse()
        /// <summary>
        /// Gerar o source code do serviço
        /// </summary>
        private void GerarClasse()
        {
            #region Gerar o código da classe
            StringWriter writer = new StringWriter(CultureInfo.CurrentCulture);
            CSharpCodeProvider provider = new CSharpCodeProvider();
            provider.GenerateCodeFromNamespace(GerarGrafo(), writer, null);
            #endregion

            string codigoClasse = writer.ToString();

            #region Compilar o código da classe
            CompilerResults results = provider.CompileAssemblyFromSource(ParametroCompilacao(), codigoClasse);
            serviceAssemby = results.CompiledAssembly;
            #endregion
        }
        #endregion

        #region ParametroCompilacao
        /// <summary>
        /// Montar os parâmetros para a compilação da classe
        /// </summary>
        /// <returns>Retorna os parâmetros</returns>
        private CompilerParameters ParametroCompilacao()
        {
            CompilerParameters parameters = new CompilerParameters(new string[] { "System.dll", "System.Xml.dll", "System.Web.Services.dll", "System.Data.dll" });
            parameters.GenerateExecutable = false;
            parameters.GenerateInMemory = true;
            parameters.TreatWarningsAsErrors = false;
            parameters.WarningLevel = 4;

            return parameters;
        }
        #endregion

        #region GerarGrafo()
        /// <summary>
        /// Gerar a estrutura e o grafo da classe
        /// </summary>
        private CodeNamespace GerarGrafo()
        {
            #region Gerar a estrutura da classe do serviço
            //Gerar a estrutura da classe do serviço
            ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
            importer.AddServiceDescription(this.serviceDescription, string.Empty, string.Empty);

            //Definir o nome do protocolo a ser utilizado
            //Não posso definir, tenho que deixar por conta do WSDL definir, ou pode dar erro em alguns estados
            //importer.ProtocolName = "Soap12";
            //importer.ProtocolName = "Soap";

            //Tipos deste serviço devem ser gerados como propriedades e não como simples campos
            importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties;
            #endregion

            #region Gerar o o grafo da classe para depois gerar o código
            CodeNamespace @namespace = new CodeNamespace();
            CodeCompileUnit unit = new CodeCompileUnit();
            unit.Namespaces.Add(@namespace);
            ServiceDescriptionImportWarnings warmings = importer.Import(@namespace, unit);
            #endregion

            return @namespace;
        }
        #endregion

        #region RelacCertificado
        /// <summary>
        /// Relacionar o certificado digital com o serviço que será consumido do webservice
        /// </summary>
        /// <param name="instance">Objeto do serviço que será consumido</param>
        private void RelacCertificado(object instance)
        {
            Type tipoInstance = instance.GetType();
            object oClientCertificates = tipoInstance.InvokeMember("ClientCertificates", System.Reflection.BindingFlags.GetProperty, null, instance, new Object[] { });
            Type tipoClientCertificates;
            tipoClientCertificates = oClientCertificates.GetType();
            tipoClientCertificates.InvokeMember("Add", System.Reflection.BindingFlags.InvokeMethod, null, oClientCertificates, new Object[] { this.oCertificado });
        }
        #endregion

        #endregion
    }
}
