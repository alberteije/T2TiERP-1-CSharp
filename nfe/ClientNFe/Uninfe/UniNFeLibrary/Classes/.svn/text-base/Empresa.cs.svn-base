using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.IO;
using System.Threading;
using UniNFeLibrary.Enums;

namespace UniNFeLibrary
{
    /// <summary>
    /// Classe contém os dados da empresa e suas configurações
    /// </summary>
    /// <remarks>
    /// Autor: Wandrey Mundin Ferreira
    /// Data: 28/07/2010
    /// </remarks>
    public class Empresa
    {
        #region Propriedades

        #region Propriedades das pastas configuradas para utilização pelo UniNFe
        /// <summary>
        /// Pasta onde deve ser gravado os XML´s a serem enviados
        /// </summary>
        public string PastaEnvio { get; set; }
        /// <summary>
        /// Pasta onde será gravado os XML´s de retorno para o ERP
        /// </summary>
        public string PastaRetorno { get; set; }
        /// <summary>
        /// Pasta onde será gravado os XML´s enviados
        /// </summary>
        public string PastaEnviado { get; set; }
        /// <summary>
        /// Pasta onde será gravado os XML´s que apresentaram algum tipo de erro em sua estrutura
        /// </summary>
        public string PastaErro { get; set; }
        /// <summary>
        /// Pasta onde será gravado como forma de backup os XML´s enviados
        /// </summary>
        public string PastaBackup { get; set; }
        /// <summary>
        /// Pasta onde deve ser gravado os XML´s de notas fiscais a serem enviadas em lote
        /// </summary>
        public string PastaEnvioEmLote { get; set; }
        /// <summary>
        /// Pasta onde é gravado os XML´s da NFE somente para validação
        /// </summary>
        public string PastaValidar { get; set; }
        /// <summary>
        /// Pasta para onde será gravado os XML´s de NFe para o DANFEMon fazer a impressão do DANFe
        /// </summary>
        public string PastaDanfeMon { get; set; }
        #endregion

        #region Propriedades diversas
        /// <summary>
        /// CNPJ da Empresa
        /// </summary>
        public string CNPJ { get; set; }
        /// <summary>
        /// Nome da Empresa
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Código da unidade Federativa da Empresa
        /// </summary>
        public int UFCod { get; set; }
        /// <summary>
        /// Ambiente a ser utilizado para a emissão da nota fiscal eletrônica
        /// </summary>
        public int tpAmb { get; set; }
        /// <summary>
        /// Tipo de emissão a ser utilizado para a emissão da nota fiscal eletrônica
        /// </summary>
        public int tpEmis { get; set; }
        /// <summary>
        /// Certificado digital
        /// </summary>
        public string Certificado { get; set; }
        /// <summary>
        /// Certificado digital
        /// </summary>
        public X509Certificate2 X509Certificado { get; set; }
        /// <summary>
        /// Gravar o retorno da NFe também em TXT
        /// </summary>
        public bool GravarRetornoTXTNFe { get; set; }
        /// <summary>
        /// dias em que se deve manter os arquivos nas pastas retorno e temporario
        /// <para>coloque 0 para infinito</para>
        /// </summary>
        /// <by>http://desenvolvedores.net/marcelo</by>
        public int DiasLimpeza { get; set; }
        /// <summary>
        /// Ultima execução da verificação das notas presas na pasta EmProcessamento
        /// </summary>
        public DateTime UltimaVerificacaoEmProcessamento { get; set; }  //danasa 10-2009
        #endregion

        #region Propriedades da parte das configurações por empresa
        /// <summary>
        /// Nome da pasta onde é gravado as configurações e informações da Empresa
        /// </summary>
        public string PastaEmpresa { get; set; }
        /// <summary>
        /// Nome do arquivo XML das configurações da empresa
        /// </summary>
        public string NomeArquivoConfig { get; set; }

        public bool CriaPastasAutomaticamente { get; set; }
        #endregion

        #region Propriedades para controle da impressão do DANFE
        /// <summary>
        /// Pasta do executável do UniDanfe
        /// </summary>
        public string PastaExeUniDanfe { get; set; }
        /// <summary>
        /// Pasta do arquivo de configurações do UniDanfe (Tem que ser sem o \dados)
        /// </summary>
        public string PastaConfigUniDanfe { get; set; }
        /// <summary>
        /// Copiar o XML da NFe (-nfe.xml) para a pasta do danfemon? 
        /// </summary>
        public bool XMLDanfeMonNFe { get; set; }
        /// <summary>
        /// Copiar o XML de Distribuição da NFe (-procNfe.xml) para a pasta do danfemon?
        /// </summary>
        public bool XMLDanfeMonProcNFe { get; set; }
        #endregion

        #region Propriedade para controle do nome da pasta a serem salvos os XML´s enviados
        private DiretorioSalvarComo mDiretorioSalvarComo = "";
        /// <summary>
        /// Define como devem ser salvos os diretórios dentro do Uninfe.
        /// <para>por enqto apenas usa a data e os valores possíveis para definir são:</para>
        /// <para>    A para ANO</para>
        /// <para>    M para MES</para>
        /// <para>    D para DIA</para>
        /// <para>    pode se passar como desejar</para>
        /// <para>    Ex:</para>
        /// <para>        AMD   para criar a pasta como ..\Enviados\Autorizados\ANOMESDIA\nfe.xml</para>
        /// <para>        AM    para criar a pasta como ..\Enviados\Autorizados\ANOMES\nfe.xml</para>
        /// <para>        AD    para criar a pasta como ..\Enviados\Autorizados\ANODIA\nfe.xml</para>
        /// <para>        A\M\D para criar a pasta como ..\Enviados\Autorizados\ANO\MES\DIA\nfe.xml</para>
        /// <para>        podem ser criadas outras combinações, ficando a critério do usuário</para>
        /// </summary>
        /// <by>http://desenvolvedores.net/marcelo</by>
        public DiretorioSalvarComo DiretorioSalvarComo
        {
            get
            {
                if (string.IsNullOrEmpty(mDiretorioSalvarComo))
                    mDiretorioSalvarComo = "AM";//padrão

                return mDiretorioSalvarComo;
            }

            set { mDiretorioSalvarComo = value; }
        }
        #endregion

        #endregion

        #region Coleções
        /// <summary>
        /// Configurações por empresa
        /// </summary>
        public static List<Empresa> Configuracoes = new List<Empresa>();
        /// <summary>
        /// Objetos dos serviços da NFe
        /// </summary>
        public Dictionary<string, WebServiceProxy> WSProxy = new Dictionary<string, WebServiceProxy>();
        #endregion

        /// <summary>
        /// Empresa
        /// danasa 20-9-2010
        /// </summary>
        public Empresa() { this.CriaPastasAutomaticamente = false; }

        #region CarregaConfiguracao()
        /// <summary>
        /// Carregar as configurações de todas as empresas na coleção "Configuracoes" 
        /// </summary>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 29/07/2010
        /// </remarks>
        public static void CarregaConfiguracao()
        {
            Empresa.Configuracoes.Clear();

            if (File.Exists(InfoApp.NomeArqEmpresa))
            {
                FileStream arqXml = null;

                try
                {
                    arqXml = new FileStream(InfoApp.NomeArqEmpresa, FileMode.Open, FileAccess.Read, FileShare.Read); //Abrir um arquivo XML usando FileStream

                    var xml = new XmlDocument();
                    xml.Load(arqXml);

                    var empresaList = xml.GetElementsByTagName("Empresa");

                    foreach (XmlNode empresaNode in empresaList)
                    {
                        var empresaElemento = (XmlElement)empresaNode;

                        var registroList = xml.GetElementsByTagName("Registro");

                        for (int i = 0; i < registroList.Count; i++)
                        {
                            Empresa empresa = new Empresa();

                            var registroNode = registroList[i];
                            var registroElemento = (XmlElement)registroNode;

                            empresa.CNPJ = registroElemento.GetAttribute("CNPJ").Trim();
                            empresa.Nome = registroElemento.GetElementsByTagName("Nome")[0].InnerText.Trim();
                            empresa.PastaEmpresa = InfoApp.PastaExecutavel() + "\\" + empresa.CNPJ.Trim();
                            empresa.NomeArquivoConfig = empresa.PastaEmpresa + "\\" + InfoApp.NomeArqConfig;

                            try
                            {
                                BuscaConfiguracao(empresa);
                            }
                            catch (Exception ex)
                            {
                                ///
                                /// nao acessar o metodo Auxiliar.GravarArqErroERP(string Arquivo, string Erro) já que nela tem a pesquisa da empresa
                                /// com base em "int emp = new FindEmpresaThread(Thread.CurrentThread).Index;" e neste ponto ainda não foi criada
                                /// as thread's
                                string cArqErro;
                                if (string.IsNullOrEmpty(empresa.PastaRetorno))
                                    cArqErro = Path.Combine(InfoApp.PastaExecutavel(), string.Format(InfoApp.NomeArqERRUniNFe, DateTime.Now.ToString("yyyyMMddThhmmss")));
                                else
                                    cArqErro = Path.Combine(empresa.PastaRetorno, string.Format(InfoApp.NomeArqERRUniNFe, DateTime.Now.ToString("yyyyMMddThhmmss")));

                                try
                                {
                                    //Grava arquivo de ERRO para o ERP
                                    File.WriteAllText(cArqErro, ex.Message, Encoding.Default);
                                }
                                catch { }
                            }
                            ///
                            /// mesmo com erro, adicionar a lista para que o usuário possa altera-la
                            Configuracoes.Add(empresa);
                        }
                    }

                    arqXml.Close();
                    arqXml = null;

                    Empresa.CriarPasta();
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {
                    if (arqXml != null)
                        arqXml.Close();
                }
            }
        }
        #endregion

        #region BuscaConfiguracao()
        /// <summary>
        /// Busca as configurações da empresa dentro de sua pasta gravadas em um XML chamado UniNfeConfig.Xml
        /// </summary>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 28/07/2010
        /// </remarks>
        private static void BuscaConfiguracao(Empresa empresa)
        {
            #region Criar diretório das configurações e dados da empresa
            if (!Directory.Exists(empresa.PastaEmpresa))
            {
                Directory.CreateDirectory(empresa.PastaEmpresa);
            }
            #endregion

            #region Limpar conteúdo dos atributos de configurações da empresa
            empresa.PastaEnvio = string.Empty;
            empresa.PastaRetorno = string.Empty;
            empresa.PastaEnviado = string.Empty;
            empresa.PastaErro = string.Empty;
            empresa.PastaBackup = string.Empty;
            empresa.PastaEnvioEmLote = string.Empty;
            empresa.PastaValidar = string.Empty;
            empresa.PastaDanfeMon = string.Empty;
            empresa.PastaExeUniDanfe = string.Empty;
            empresa.PastaConfigUniDanfe = string.Empty;
            empresa.Certificado = string.Empty;
            empresa.X509Certificado = null;

            empresa.UFCod = 0;
            empresa.DiasLimpeza = 0;

            empresa.tpAmb = TipoAmbiente.taHomologacao; //2
            empresa.tpEmis = TipoEmissao.teNormal; //1

            empresa.GravarRetornoTXTNFe = false;
            empresa.XMLDanfeMonNFe = false;
            empresa.XMLDanfeMonProcNFe = false;
            empresa.DiretorioSalvarComo = "AM";
            #endregion

            #region Carregar as configurações do XML UniNFeConfig da Empresa
            FileStream arqXml = null;

            if (File.Exists(empresa.NomeArquivoConfig))
            {
                try
                {
                    arqXml = new FileStream(empresa.NomeArquivoConfig, FileMode.Open, FileAccess.Read, FileShare.Read); //Abrir um arquivo XML usando FileStrem
                    var xml = new XmlDocument();
                    xml.Load(arqXml);

                    var configList = xml.GetElementsByTagName("nfe_configuracoes");
                    foreach (XmlNode configNode in configList)
                    {
                        var configElemento = (XmlElement)configNode;

                        if (configElemento.GetElementsByTagName("PastaXmlEnvio")[0] != null)
                            empresa.PastaEnvio = configElemento.GetElementsByTagName("PastaXmlEnvio")[0].InnerText.Trim();
                        if (configElemento.GetElementsByTagName("PastaXmlRetorno")[0] != null)
                            empresa.PastaRetorno = configElemento.GetElementsByTagName("PastaXmlRetorno")[0].InnerText.Trim();
                        if (configElemento.GetElementsByTagName("PastaXmlEnviado")[0] != null)
                            empresa.PastaEnviado = configElemento.GetElementsByTagName("PastaXmlEnviado")[0].InnerText.Trim();
                        if (configElemento.GetElementsByTagName("PastaXmlErro")[0] != null)
                            empresa.PastaErro = configElemento.GetElementsByTagName("PastaXmlErro")[0].InnerText.Trim();
                        if (configElemento.GetElementsByTagName("UnidadeFederativaCodigo")[0] != null)
                            empresa.UFCod = Convert.ToInt32(configElemento.GetElementsByTagName("UnidadeFederativaCodigo")[0].InnerText);
                        if (configElemento.GetElementsByTagName("AmbienteCodigo")[0] != null)
                            empresa.tpAmb = Convert.ToInt32(configElemento.GetElementsByTagName("AmbienteCodigo")[0].InnerText.Trim());
                        if (configElemento.GetElementsByTagName("CertificadoDigital")[0] != null)
                            empresa.Certificado = configElemento.GetElementsByTagName("CertificadoDigital")[0].InnerText.Trim();
                        if (configElemento.GetElementsByTagName("tpEmis")[0] != null)
                            empresa.tpEmis = Convert.ToInt32(configElemento.GetElementsByTagName("tpEmis")[0].InnerText.Trim());
                        if (configElemento.GetElementsByTagName("PastaBackup")[0] != null)
                            empresa.PastaBackup = configElemento.GetElementsByTagName("PastaBackup")[0].InnerText.Trim();
                        if (configElemento.GetElementsByTagName("PastaXmlEmLote")[0] != null)
                            empresa.PastaEnvioEmLote = configElemento.GetElementsByTagName("PastaXmlEmLote")[0].InnerText.Trim();
                        if (configElemento.GetElementsByTagName("PastaValidar")[0] != null)
                            empresa.PastaValidar = configElemento.GetElementsByTagName("PastaValidar")[0].InnerText.Trim();
                        if (configElemento.GetElementsByTagName("GravarRetornoTXTNFe")[0] != null)
                            empresa.GravarRetornoTXTNFe = Convert.ToBoolean(configElemento.GetElementsByTagName("GravarRetornoTXTNFe")[0].InnerText.Trim());
                        if (configElemento.GetElementsByTagName("DiretorioSalvarComo")[0] != null)
                            empresa.DiretorioSalvarComo = configElemento.GetElementsByTagName("DiretorioSalvarComo")[0].InnerText.Trim();
                        if (configElemento.GetElementsByTagName("DiasLimpeza")[0] != null)
                            empresa.DiasLimpeza = Convert.ToInt32(configElemento.GetElementsByTagName("DiasLimpeza")[0].InnerText.Trim());
                        if (configElemento.GetElementsByTagName("PastaExeUniDanfe")[0] != null)
                            empresa.PastaExeUniDanfe = configElemento.GetElementsByTagName("PastaExeUniDanfe")[0].InnerText.Trim();
                        if (configElemento.GetElementsByTagName("PastaConfigUniDanfe")[0] != null)
                            empresa.PastaConfigUniDanfe = configElemento.GetElementsByTagName("PastaConfigUniDanfe")[0].InnerText.Trim();
                        if (configElemento.GetElementsByTagName("PastaDanfeMon")[0] != null)
                            empresa.PastaDanfeMon = configElemento.GetElementsByTagName("PastaDanfeMon")[0].InnerText.Trim();
                        if (configElemento.GetElementsByTagName("XMLDanfeMonNFe")[0] != null)
                            empresa.XMLDanfeMonNFe = Convert.ToBoolean(configElemento.GetElementsByTagName("XMLDanfeMonNFe")[0].InnerText.Trim());
                        if (configElemento.GetElementsByTagName("XMLDanfeMonProcNFe")[0] != null)
                            empresa.XMLDanfeMonProcNFe = Convert.ToBoolean(configElemento.GetElementsByTagName("XMLDanfeMonProcNFe")[0].InnerText.Trim());
                    }

                    //Ajustar o certificado digital de String para o tipo X509Certificate2
                    X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
                    store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                    X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                    X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindBySubjectDistinguishedName, empresa.Certificado, false);

                    empresa.X509Certificado = null;
                    for (int i = 0; i < collection1.Count; i++)
                    {
                        //Verificar a validade do certificado
                        if (DateTime.Compare(DateTime.Now, collection1[i].NotAfter) == -1)
                        {
                            empresa.X509Certificado = collection1[i];
                            break;
                        }
                    }

                    //Se não encontrou nenhum certificado com validade correta, vou pegar o primeiro certificado, porem vai travar na hora de tentar enviar a nota fiscal, por conta da validade. Wandrey 06/04/2011
                    if (empresa.X509Certificado == null)
                        empresa.X509Certificado = collection1[0];
                }
                catch (Exception ex)
                {
                    empresa.Certificado = string.Empty;
                    throw new Exception("Ocorreu um erro ao efetuar a leitura das configurações da empresa " + empresa.Nome.Trim() + ". Por favor entre na tela de configurações desta empresa e reconfigure.\r\n\r\nErro: " + ex.Message);
                }
                finally
                {
                    if (arqXml != null)
                        arqXml.Close();
                }
            }
            #endregion
        }
        #endregion

        #region FindConfEmpresa()
        /// <summary>
        /// Procurar o cnpj na coleção das empresas
        /// </summary>
        /// <param name="cnpj">CNPJ a ser pesquisado</param>
        /// <returns>objeto empresa localizado, null se nada for localizado</returns>
        public static Empresa FindConfEmpresa(string cnpj)
        {
            Empresa retorna = null;
            foreach (Empresa empresa in Empresa.Configuracoes)
            {
                if (empresa.CNPJ.Equals(cnpj))
                {
                    retorna = empresa;
                    break;
                }
            }

            return retorna;
        }
        #endregion

        #region FindConfEmpresaIndex()
        /// <summary>
        /// Procurar o cnpj na coleção das empresas
        /// </summary>
        /// <param name="cnpj">CNPJ a ser pesquisado</param>
        /// <returns>Retorna o index do objeto localizado ou null se nada for localizado</returns>
        public static int FindConfEmpresaIndex(string cnpj)
        {
            int retorna = -1;

            for (int i = 0; i < Empresa.Configuracoes.Count; i++)
            {
                Empresa empresa = Empresa.Configuracoes[i];

                if (empresa.CNPJ.Equals(cnpj))
                {
                    retorna = i;
                    break;
                }
            }

            return retorna;
        }
        #endregion

        #region Valid()
        /// <summary>
        /// Retorna se o indice da coleção que foi pesquisado é valido ou não
        /// </summary>
        /// <param name="index">Indice a ser validado</param>
        /// <returns>Retorna true or false</returns>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 30/07/2010
        /// </remarks>
        public static bool Valid(int index)
        {
            bool retorna = true;
            if (index.Equals(-1))
                retorna = false;

            return retorna;
        }
        #endregion

        #region Valid()
        /// <summary>
        /// Retorna se o objeto da coleção que foi pesquisado é valido ou não
        /// </summary>
        /// <param name="empresa">Objeto da empresa</param>
        /// <returns>Retorna true or false</returns>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 30/07/2010
        /// </remarks>
        public static bool Valid(Empresa empresa)
        {
            bool retorna = true;
            if (empresa.Equals(null))
                retorna = false;

            return retorna;
        }
        #endregion

        #region CriarPasta()
        /// <summary>
        /// Criar as pastas para todas as empresas cadastradas e configuradas no sistema se as mesmas não existirem
        /// </summary>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>29/09/2009</date>
        private static void CriarPasta()
        {
            try
            {
                foreach (Empresa empresa in Empresa.Configuracoes)
                {

                    //Criar pasta de envio
                    if (!string.IsNullOrEmpty(empresa.PastaEnvio))
                    {
                        if (!Directory.Exists(empresa.PastaEnvio))
                        {
                            Directory.CreateDirectory(empresa.PastaEnvio);
                        }
                    }

                    //Criar pasta de Envio em Lote
                    if (!string.IsNullOrEmpty(empresa.PastaEnvioEmLote))
                    {
                        if (!Directory.Exists(empresa.PastaEnvioEmLote))
                        {
                            Directory.CreateDirectory(empresa.PastaEnvioEmLote);
                        }
                    }

                    //Criar pasta de Retorno
                    if (!string.IsNullOrEmpty(empresa.PastaRetorno))
                    {
                        if (!Directory.Exists(empresa.PastaRetorno))
                        {
                            Directory.CreateDirectory(empresa.PastaRetorno);
                        }
                    }

                    //Criar pasta Enviado
                    if (!string.IsNullOrEmpty(empresa.PastaEnviado))
                    {
                        if (!Directory.Exists(empresa.PastaEnviado))
                        {
                            Directory.CreateDirectory(empresa.PastaEnviado);
                        }
                    }

                    //Criar pasta de XML´s com erro
                    if (!string.IsNullOrEmpty(empresa.PastaErro))
                    {
                        if (!Directory.Exists(empresa.PastaErro))
                        {
                            Directory.CreateDirectory(empresa.PastaErro);
                        }
                    }

                    //Criar pasta de Backup
                    if (!string.IsNullOrEmpty(empresa.PastaBackup))
                    {
                        if (!Directory.Exists(empresa.PastaBackup))
                        {
                            Directory.CreateDirectory(empresa.PastaBackup);
                        }
                    }

                    //Criar pasta para somente validação de XML´s
                    if (!string.IsNullOrEmpty(empresa.PastaValidar))
                    {
                        if (!Directory.Exists(empresa.PastaValidar))
                        {
                            Directory.CreateDirectory(empresa.PastaValidar);
                        }
                    }

                    //Criar subpasta Assinado na pasta de envio individual de nfe
                    if (!string.IsNullOrEmpty(empresa.PastaEnvio))
                    {
                        if (!Directory.Exists(empresa.PastaEnvio + InfoApp.NomePastaXMLAssinado))
                        {
                            System.IO.Directory.CreateDirectory(empresa.PastaEnvio + InfoApp.NomePastaXMLAssinado);
                        }
                    }

                    //Criar subpasta Assinado na pasta de envio em lote de nfe
                    if (!string.IsNullOrEmpty(empresa.PastaEnvioEmLote))
                    {
                        if (!Directory.Exists(empresa.PastaEnvioEmLote + InfoApp.NomePastaXMLAssinado))
                        {
                            System.IO.Directory.CreateDirectory(empresa.PastaEnvioEmLote + InfoApp.NomePastaXMLAssinado);
                        }
                    }

                    //Criar pasta para monitoramento do DANFEMon e impressão do DANFE
                    if (!string.IsNullOrEmpty(empresa.PastaDanfeMon))
                    {
                        if (!Directory.Exists(empresa.PastaDanfeMon))
                        {
                            System.IO.Directory.CreateDirectory(empresa.PastaDanfeMon);
                        }
                    }
                }

                Empresa.CriarSubPastaEnviado();
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

        #region CriarSubPastaEnviado()
        /// <summary>
        /// Criar as subpastas (Autorizados/Denegados/EmProcessamento) dentro da pasta dos XML´s enviados para todas as empresas cadastradas e configuradas
        /// </summary>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Date: 20/04/2010
        /// </remarks>
        private static void CriarSubPastaEnviado()
        {
            try
            {
                for (int i = 0; i < Empresa.Configuracoes.Count; i++)
                {
                    Empresa.CriarSubPastaEnviado(i);
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

        #region CriarSubPastaEnviado()
        /// <summary>
        /// Criar as subpastas (Autorizados/Denegados/EmProcessamento) dentro da pasta dos XML´s enviados para a empresa passada por parâmetro
        /// </summary>
        /// <param name="indexEmpresa">Index da Empresa a ser pesquisado na coleção de configurações das empresas cadastradas</param>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Date: 20/04/2010
        /// </remarks>
        public static void CriarSubPastaEnviado(int indexEmpresa)
        {
            Empresa empresa = Empresa.Configuracoes[indexEmpresa];

            try
            {
                if (!string.IsNullOrEmpty(empresa.PastaEnviado))
                {
                    //Criar a pasta EmProcessamento
                    if (!Directory.Exists(empresa.PastaEnviado + "\\" + PastaEnviados.EmProcessamento.ToString()))
                    {
                        System.IO.Directory.CreateDirectory(empresa.PastaEnviado + "\\" + PastaEnviados.EmProcessamento.ToString());
                    }

                    //Criar a Pasta Autorizado
                    if (!Directory.Exists(empresa.PastaEnviado + "\\" + PastaEnviados.Autorizados.ToString()))
                    {
                        System.IO.Directory.CreateDirectory(empresa.PastaEnviado + "\\" + PastaEnviados.Autorizados.ToString());
                    }

                    //Criar a Pasta Denegado
                    if (!Directory.Exists(empresa.PastaEnviado + "\\" + PastaEnviados.Denegados.ToString()))
                    {
                        System.IO.Directory.CreateDirectory(empresa.PastaEnviado + "\\" + PastaEnviados.Denegados.ToString());
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
    }

    public class FindEmpresaThread
    {
        public int Index = -1;

        public FindEmpresaThread(Thread currentThread)
        {
            Index = Auxiliar.threads[currentThread];
        }
    }
}
