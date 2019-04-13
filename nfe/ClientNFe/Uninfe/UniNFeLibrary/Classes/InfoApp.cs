using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace UniNFeLibrary
{
    /// <summary>
    /// Classe possui métodos que retoram informações sobre o Aplicativo
    /// </summary>
    public class InfoApp
    {
        #region Constantes
        /// <summary>
        /// Nome para a pasta dos XML assinados
        /// </summary>
        public const string NomePastaXMLAssinado = "\\Assinado";
        public const string NomeArqERRUniNFe = "UniNFeErro_{0}.err";
        /// <summary>
        /// Nome do arquivo XML de configurações
        /// </summary>
        public const string NomeArqConfig = "UniNfeConfig.xml";
        /// <summary>
        /// Nome do arquivo XML que é gravado as empresas cadastradas
        /// </summary>
        public static readonly string NomeArqEmpresa = PastaExecutavel() + "\\UniNfeEmpresa.xml";
        /// <summary>     
        /// Nome do arquivo para controle da numeração sequencial do lote.
        /// </summary>
        public const string NomeArqXmlLote = "UniNfeLote.xml";
        /// <summary>
        /// Nome do arquivo 1 de backup de segurança do arquivo de controle da numeração sequencial do lote
        /// </summary>
        public const string NomeArqXmlLoteBkp1 = "Bkp1_UniNfeLote.xml";
        /// <summary>
        /// Nome do arquivo 2 de backup de segurança do arquivo de controle da numeração sequencial do lote
        /// </summary>
        public const string NomeArqXmlLoteBkp2 = "Bkp2_UniNfeLote.xml";
        /// <summary>
        /// Nome do arquivo 3 de backup de segurança do arquivo de controle da numeração sequencial do lote
        /// </summary>
        public const string NomeArqXmlLoteBkp3 = "Bkp3_UniNfeLote.xml";
        /// <summary>
        /// Nome do arquivo que grava as notas fiscais em fluxo de envio
        /// </summary>
        public const string NomeArqXmlFluxoNfe = "fluxonfe.xml";
        #endregion

        #region Propriedades Estaticas
        /// <summary>
        /// Sempre na execução do aplicativo (EXE) já defina este campo estático ou a classe não conseguirá pegar
        /// as informações do executável, tais como: Versão, Nome da aplicação, etc.
        /// Defina sempre com o conteúdo: Assembly.GetExecutingAssembly
        /// </summary>
        public static Assembly oAssemblyEXE;
        #endregion

        /// <summary>
        /// Retorna a versão do aplicativo 
        /// </summary>
        /// <param name="oAssembly">Passar sempre: Assembly.GetExecutingAssembly() pois ele vai pegar o Assembly do EXE ou DLL de onde está sendo chamado o método</param>
        /// <returns>string contendo a versão do Aplicativo</returns>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>29/01/2009</date>
        public static string Versao()
        {
            //Montar a versão do programa
            string versao;

            foreach (Attribute attr in Attribute.GetCustomAttributes(oAssemblyEXE))
            {
                if (attr.GetType() == typeof(AssemblyVersionAttribute))
                {
                    versao = ((AssemblyVersionAttribute)attr).Version;
                    break;
                }
            }
            string delimStr = ",=";
            char[] delimiter = delimStr.ToCharArray();
            string[] strAssembly = oAssemblyEXE.ToString().Split(delimiter);
            versao = strAssembly[2];

            return versao;
        }

        /// <summary>
        /// Retorna o nome do aplicativo 
        /// </summary>
        /// <param name="oAssembly">Passar sempre: Assembly.GetExecutingAssembly() pois ele vai pegar o Assembly do EXE ou DLL de onde está sendo chamado o método</param>
        /// <returns>string contendo o nome do Aplicativo</returns>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>31/07/2009</date>
        public static string NomeAplicacao()
        {
            //Montar o nome da aplicação
            string Produto = string.Empty;

            foreach (Attribute attr in Attribute.GetCustomAttributes(oAssemblyEXE))
            {
                if (attr.GetType() == typeof(AssemblyProductAttribute))
                {
                    Produto = ((AssemblyProductAttribute)attr).Product;
                    break;
                }
            }

            return Produto;
        }

        /// <summary>
        /// Retorna a pasta do executável
        /// </summary>
        /// <returns>Retorna a pasta onde está o executável</returns>
        public static string PastaExecutavel()
        {
            return System.IO.Path.GetDirectoryName(Application.ExecutablePath);
        }

        /// <summary>
        /// Retorna a pasta dos schemas para validar os XML´s
        /// </summary>
        /// <returns></returns>
        public static string PastaSchemas()
        {
            return PastaExecutavel() + "\\schemas";
        }

        /// <summary>
        /// Retorna o XML para salvar os parametros das telas
        /// </summary>
        public static string NomeArqXMLParams()
        {
            return PastaExecutavel() + "\\UniNFeParams.xml";
        }

        /// <summary>
        /// Grava XML com algumas informações do aplicativo, 
        /// dentre elas os dados do certificado digital configurado nos parâmetros, 
        /// versão, última modificação, etc.
        /// </summary>
        /// <param name="sArquivo">Pasta e nome do arquivo XML a ser gravado com as informações</param>
        /// <param name="oAssembly">Passar sempre: Assembly.GetExecutingAssembly() pois ele vai pegar o Assembly do EXE ou DLL de onde está sendo chamado o método</param>
        /// <by>Wandrey Mundin Ferreira</by>
        /// <date>29/01/2009</date>
        public void GravarXMLInformacoes(string sArquivo)
        {
            int emp = new FindEmpresaThread(Thread.CurrentThread).Index; 

            string cStat = "1";
            string xMotivo = "Consulta efetuada com sucesso";

            //Ler os dados do certificado digital
            string sSubject = "";
            string sValIni = "";
            string sValFin = "";

            CertificadoDigital oDigCert = new CertificadoDigital();

            oDigCert.PrepInfCertificado(Empresa.Configuracoes[emp].X509Certificado);

            if (oDigCert.lLocalizouCertificado == true)
            {
                sSubject = oDigCert.sSubject;
                sValIni = oDigCert.dValidadeInicial.ToString();
                sValFin = oDigCert.dValidadeFinal.ToString();
            }
            else
            {
                cStat = "2";
                xMotivo = "Certificado digital não foi localizado";
            }

            //Gravar o XML com as informações do aplicativo
            try
            {
                if (Path.GetExtension(sArquivo).ToLower() == ".txt")
                {
                    StringBuilder aTXT = new StringBuilder();
                    aTXT.AppendLine("cStat|" + cStat);
                    aTXT.AppendLine("xMotivo|" + xMotivo);
                    //Dados do certificado digital
                    aTXT.AppendLine("sSubject|" + sSubject);
                    aTXT.AppendLine("dValIni|" + sValIni);
                    aTXT.AppendLine("dValFin|" + sValFin);
                    //Dados gerais do Aplicativo
                    aTXT.AppendLine("versao|" + InfoApp.Versao());
                    aTXT.AppendLine("dUltModif|" + File.GetLastWriteTimeUtc(Application.ExecutablePath).ToString("dd/MM/yyyy hh:mm:ss"));
                    aTXT.AppendLine("PastaExecutavel|" + InfoApp.PastaExecutavel());
                    aTXT.AppendLine("NomeComputador|" + Environment.MachineName);
                    //Dados das configurações do aplicativo
                    aTXT.AppendLine("PastaBackup|" + (string.IsNullOrEmpty(Empresa.Configuracoes[emp].PastaBackup) ? "" : Empresa.Configuracoes[emp].PastaBackup));
                    aTXT.AppendLine("PastaXmlEmLote|" + (string.IsNullOrEmpty(Empresa.Configuracoes[emp].PastaEnvioEmLote) ? "" : Empresa.Configuracoes[emp].PastaEnvioEmLote));
                    aTXT.AppendLine("PastaXmlAssinado|" + (string.IsNullOrEmpty(InfoApp.NomePastaXMLAssinado) ? "" : InfoApp.NomePastaXMLAssinado));
                    aTXT.AppendLine("PastaValidar|" + (string.IsNullOrEmpty(Empresa.Configuracoes[emp].PastaValidar) ? "" : Empresa.Configuracoes[emp].PastaValidar));
                    aTXT.AppendLine("PastaXmlEnviado|" + (string.IsNullOrEmpty(Empresa.Configuracoes[emp].PastaEnviado) ? "" : Empresa.Configuracoes[emp].PastaEnviado));
                    aTXT.AppendLine("PastaXmlEnvio|" + (string.IsNullOrEmpty(Empresa.Configuracoes[emp].PastaEnvio) ? "" : Empresa.Configuracoes[emp].PastaEnvio));
                    aTXT.AppendLine("PastaXmlErro|" + (string.IsNullOrEmpty(Empresa.Configuracoes[emp].PastaErro) ? "" : Empresa.Configuracoes[emp].PastaErro));
                    aTXT.AppendLine("PastaXmlRetorno|" + (string.IsNullOrEmpty(Empresa.Configuracoes[emp].PastaRetorno) ? "" : Empresa.Configuracoes[emp].PastaRetorno));
                    aTXT.AppendLine("DiasParaLimpeza|" + Empresa.Configuracoes[emp].DiasLimpeza.ToString());
                    aTXT.AppendLine("DiretorioSalvarComo|" + Empresa.Configuracoes[emp].DiretorioSalvarComo.ToString());
                    aTXT.AppendLine("GravarRetornoTXTNFe|" + Empresa.Configuracoes[emp].GravarRetornoTXTNFe.ToString());
                    aTXT.AppendLine("AmbienteCodigo|" + Empresa.Configuracoes[emp].tpAmb.ToString());
                    aTXT.AppendLine("tpEmis|" + Empresa.Configuracoes[emp].tpEmis.ToString());
                    aTXT.AppendLine("UnidadeFederativaCodigo|" + Empresa.Configuracoes[emp].UFCod.ToString());

                    File.WriteAllText(sArquivo, aTXT.ToString(), Encoding.Default);
                }
                else
                {
                    XmlWriterSettings oSettings = new XmlWriterSettings();
                    UTF8Encoding c = new UTF8Encoding(false);

                    //Para começar, vamos criar um XmlWriterSettings para configurar nosso XML
                    oSettings.Encoding = c;
                    oSettings.Indent = true;
                    oSettings.IndentChars = "";
                    oSettings.NewLineOnAttributes = false;
                    oSettings.OmitXmlDeclaration = false;

                    //Agora vamos criar um XML Writer
                    XmlWriter oXmlGravar = XmlWriter.Create(sArquivo, oSettings);

                    //Abrir o XML
                    oXmlGravar.WriteStartDocument();
                    oXmlGravar.WriteStartElement("retConsInf");
                    oXmlGravar.WriteElementString("cStat", cStat);
                    oXmlGravar.WriteElementString("xMotivo", xMotivo);

                    //Dados do certificado digital
                    oXmlGravar.WriteStartElement("DadosCertificado");
                    oXmlGravar.WriteElementString("sSubject", sSubject);
                    oXmlGravar.WriteElementString("dValIni", sValIni);
                    oXmlGravar.WriteElementString("dValFin", sValFin);
                    oXmlGravar.WriteEndElement(); //DadosCertificado                

                    //Dados gerais do Aplicativo
                    oXmlGravar.WriteStartElement("DadosUniNfe");
                    oXmlGravar.WriteElementString("versao", InfoApp.Versao());
                    oXmlGravar.WriteElementString("dUltModif", File.GetLastWriteTimeUtc(Application.ExecutablePath).ToString("dd/MM/yyyy hh:mm:ss"));
                    oXmlGravar.WriteElementString("PastaExecutavel", InfoApp.PastaExecutavel());
                    oXmlGravar.WriteElementString("NomeComputador", Environment.MachineName);
                    oXmlGravar.WriteEndElement(); //DadosUniNfe

                    //Dados das configurações do aplicativo
                    oXmlGravar.WriteStartElement("nfe_configuracoes");
                    oXmlGravar.WriteElementString("PastaBackup", (string.IsNullOrEmpty(Empresa.Configuracoes[emp].PastaBackup) ? "" : Empresa.Configuracoes[emp].PastaBackup));
                    oXmlGravar.WriteElementString("PastaXmlEmLote", (string.IsNullOrEmpty(Empresa.Configuracoes[emp].PastaEnvioEmLote) ? "" : Empresa.Configuracoes[emp].PastaEnvioEmLote));
                    oXmlGravar.WriteElementString("PastaXmlAssinado", (string.IsNullOrEmpty(InfoApp.NomePastaXMLAssinado) ? "" : InfoApp.NomePastaXMLAssinado));
                    oXmlGravar.WriteElementString("PastaValidar", (string.IsNullOrEmpty(Empresa.Configuracoes[emp].PastaValidar) ? "" : Empresa.Configuracoes[emp].PastaValidar));
                    oXmlGravar.WriteElementString("PastaXmlEnviado", (string.IsNullOrEmpty(Empresa.Configuracoes[emp].PastaEnviado) ? "" : Empresa.Configuracoes[emp].PastaEnviado));
                    oXmlGravar.WriteElementString("PastaXmlEnvio", (string.IsNullOrEmpty(Empresa.Configuracoes[emp].PastaEnvio) ? "" : Empresa.Configuracoes[emp].PastaEnvio));
                    oXmlGravar.WriteElementString("PastaXmlErro", (string.IsNullOrEmpty(Empresa.Configuracoes[emp].PastaErro) ? "" : Empresa.Configuracoes[emp].PastaErro));
                    oXmlGravar.WriteElementString("PastaXmlRetorno", (string.IsNullOrEmpty(Empresa.Configuracoes[emp].PastaRetorno) ? "" : Empresa.Configuracoes[emp].PastaRetorno));
                    oXmlGravar.WriteElementString("DiasParaLimpeza", Empresa.Configuracoes[emp].DiasLimpeza.ToString());
                    oXmlGravar.WriteElementString("DiretorioSalvarComo", Empresa.Configuracoes[emp].DiretorioSalvarComo.ToString());
                    oXmlGravar.WriteElementString("GravarRetornoTXTNFe", Empresa.Configuracoes[emp].GravarRetornoTXTNFe.ToString());
                    oXmlGravar.WriteElementString("AmbienteCodigo", Empresa.Configuracoes[emp].tpAmb.ToString());
                    oXmlGravar.WriteElementString("tpEmis", Empresa.Configuracoes[emp].tpEmis.ToString());
                    oXmlGravar.WriteElementString("UnidadeFederativaCodigo", Empresa.Configuracoes[emp].UFCod.ToString());
                    oXmlGravar.WriteEndElement(); //nfe_configuracoes

                    //Finalizar o XML
                    oXmlGravar.WriteEndElement(); //retConsInf
                    oXmlGravar.WriteEndDocument();
                    oXmlGravar.Flush();
                    oXmlGravar.Close();
                }
            }
            catch (Exception ex)
            {
                ///
                /// danasa 8-2009
                /// 
                Auxiliar oAux = new Auxiliar();
                oAux.GravarArqErroERP(Path.GetFileNameWithoutExtension(sArquivo) + ".err", ex.Message);
            }
        }

        /// <summary>
        /// Verifica se a aplicação já está executando ou não
        /// </summary>
        /// <param name="oOneMutex">Objeto do tipo Mutex para ter como retorno para conseguir encerrar o mutex na finalização do aplicativo</param>
        /// <returns>True=Aplicação está executando</returns>
        /// <date>31/07/2009</date>
        /// <by>Wandrey Mundin Ferreira</by>
        public static Boolean AppExecutando(ref System.Threading.Mutex oOneMutex)
        {
            bool executando = false;
            String nomePastaEnvio = "";
            String nomePastaEnvioDemo = "";

            Empresa oEmpresa = null;
            try
            {
                Empresa.CarregaConfiguracao();

                if (Empresa.Configuracoes.Count > 0)
                    oEmpresa = Empresa.Configuracoes[0];

                //Pegar a pasta de envio, se já tiver algum UniNFe configurado para uma determinada pasta de envio os demais não podem
                if (oEmpresa.PastaEnvio != "")
                {
                    nomePastaEnvio = oEmpresa.PastaEnvio;

                    //Tirar a unidade e os dois pontos do nome da pasta
                    int iPos = nomePastaEnvio.IndexOf(':') + 1;
                    if (iPos >= 0)
                    {
                        nomePastaEnvio = nomePastaEnvio.Substring(iPos, nomePastaEnvio.Length - iPos);
                    }
                    nomePastaEnvioDemo = nomePastaEnvio;

                    //tirar as barras de separação de pastas e subpastas
                    nomePastaEnvio = nomePastaEnvio.Replace("\\", "").Replace("/", "").ToUpper();
                }

                // Verificar se o aplicativo já está rodando. Se estiver vai emitir um aviso e abortar
                // Pois só pode executar o aplicativo uma única vez para cada pasta de envio.
                // Wandrey 27/11/2008
                System.Threading.Mutex oneMutex = null;
                String nomeMutex = InfoApp.NomeAplicacao().ToUpper() + nomePastaEnvio.Trim();

                try
                {
                    oneMutex = System.Threading.Mutex.OpenExisting(nomeMutex);
                }
                catch (System.Threading.WaitHandleCannotBeOpenedException)
                {

                }

                if (oneMutex == null)
                {
                    oneMutex = new System.Threading.Mutex(false, nomeMutex);
                    oOneMutex = oneMutex;
                    executando = false;
                }
                else
                {
                    oneMutex.Close();
                    executando = true;
                }
            }
            catch
            {
                //Não preciso retornar nada, somente evito fechar o aplicativo
                //Esta exceção pode ocorrer quando não existe nenhuma empresa cadastrada
                //ainda, ou seja, é a primeira vez que estamos entrando no aplicativo
            }

            if (executando)
            {
                MessageBox.Show("Somente uma instância do " + InfoApp.NomeAplicacao() + " pode ser executada com a seguinte pasta de envio configurada: \r\n\r\n" +
                                "Pasta Envio: " + nomePastaEnvioDemo + "\r\n\r\n" +
                                "Já existe uma instância em execução.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return executando;
        }
    }
}