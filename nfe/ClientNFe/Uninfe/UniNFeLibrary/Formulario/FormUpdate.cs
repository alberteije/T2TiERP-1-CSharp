using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.IO;

namespace UniNFeLibrary.Formulario
{
    public partial class FormUpdate : Form
    {
        #region Campos
        // The stream of data retrieved from the web server
        /// <summary>
        /// Fluxo de dados obtidos a partir do servidor web
        /// </summary>
        private Stream strResponse;
        /// <summary>
        /// Fluxo de dados que será gravado em seguida no HardDisk
        /// </summary>
        private Stream strLocal;
        /// <summary>
        /// A solicitação para o servidor web das informações sobre o arquivo
        /// </summary>
        private HttpWebRequest webRequest;
        /// <summary>
        /// A resposta do servidor web que contém as informações sobre o arquivo
        /// </summary>
        private HttpWebResponse webResponse;
        /// <summary>
        /// Progresso do download em percentual
        /// </summary>
        private static int PercProgress;
        /// <summary>
        /// Delegate que chamaremos a partir da thread para atualizar o progresso
        /// </summary>
        /// <param name="BytesRead">Bytes a serem lidos</param>
        /// <param name="TotalBytes">Total de bytes (tamanho) do arquivo que está sendo efetuado o download</param>
        private delegate void UpdateProgessCallback(Int64 BytesRead, Int64 TotalBytes);
        /// <summary>
        /// URL de onde é para ser efetuado o download do arquivo
        /// </summary>
        private string URL = "http://www.unimake.com.br/downloads/iuninfe.exe";
        /// <summary>
        /// Caminho local onde é para ser gravado o arquivo que está sendo efetuado o download
        /// </summary>
        private string LocalArq;
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor do formulário do Update
        /// </summary>
        /// <param name="nomeInstalador">Nome executável do instalador de atualização do aplicativo</param>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 20/05/2010
        /// </remarks>
        public FormUpdate(string NomeInstalador)
        {
            InitializeComponent();

            this.LocalArq = Application.StartupPath + "\\" + NomeInstalador;
            this.URL = "http://www.unimake.com.br/downloads/" + NomeInstalador;
        }
        #endregion

        #region Métodos de eventos
        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Após o download a aplicação será encerrada para a execução do instalador do aplicativo.\r\n\r\nDeseja continuar com a atualização?",
                                "Atenção",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                // Habilitar alguns controles
                lblProgresso.Visible = true;
                Application.DoEvents();

                //Executar o download
                this.Download();

                //Executar o instalador do uninfe
                if (File.Exists(LocalArq))
                {
                    System.Diagnostics.Process.Start(this.LocalArq);

                    //Forçar o encerramento da aplicação
                    Auxiliar.EncerrarApp = true;
                    this.MdiParent.Close();
                }
                else
                    MessageBox.Show("Não foi possível localizar o instalador da atualização", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Métodos gerais

        #region Download()
        /// <summary>
        /// Efetua o download do instalador do aplicativo
        /// </summary>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Date: 20/05/2010
        /// </remarks>
        private void Download()
        {
            using (WebClient Client = new WebClient())
            {
                try
                {
                    // Criar um pedido do arquivo que será baixado
                    webRequest = (HttpWebRequest)WebRequest.Create(URL);

                    // Atribuir autenticação padrão para a recuperação do arquivo
                    webRequest.Credentials = CredentialCache.DefaultCredentials;

                    // Obter a resposta do servidor
                    webResponse = (HttpWebResponse)webRequest.GetResponse();

                    //TODO: Fazer a parte do proxy da atualização do UNINFE

                    // Perguntar ao servidor o tamanho do arquivo que será baixado
                    Int64 fileSize = webResponse.ContentLength;

                    // Abrir a URL para download                    
                    strResponse = Client.OpenRead(URL);

                    // Criar um novo arquivo a partir do fluxo de dados que será salvo na local disk
                    strLocal = new FileStream(LocalArq, FileMode.Create, FileAccess.Write, FileShare.None);

                    // Ele irá armazenar o número atual de bytes recuperados do servidor
                    int bytesSize = 0;

                    // Um buffer para armazenar e gravar os dados recuperados do servidor
                    byte[] downBuffer = new byte[2048];

                    // Loop através do buffer - Até que o buffer esteja vazio
                    while ((bytesSize = strResponse.Read(downBuffer, 0, downBuffer.Length)) > 0)
                    {
                        // Gravar os dados do buffer no disco rigido
                        strLocal.Write(downBuffer, 0, bytesSize);

                        // Invocar um método para atualizar a barra de progresso
                        this.Invoke(new UpdateProgessCallback(this.UpdateProgress), new object[] { strLocal.Length, fileSize });
                    }

                    this.Invoke(new UpdateProgessCallback(this.UpdateProgress), new object[] { fileSize, fileSize });
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Ocorreu um erro ao tentar fazer o download do instalador do aplicativo.\r\n\r\nErro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (WebException ex)
                {
                    MessageBox.Show("Ocorreu um erro ao tentar fazer o download do instalador do aplicativo.\r\n\r\nErro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao tentar fazer o download do instalador do aplicativo.\r\n\r\nErro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Encerrar as streams
                    if (strResponse != null)
                        strResponse.Close();

                    if (strLocal != null)
                        strLocal.Close();
                }
            }
        }
        #endregion

        #region UpdateProgress()
        /// <summary>
        /// Atualizar a barra de progresso do download
        /// </summary>
        /// <param name="BytesRead">Quantidade de bytes lidos</param>
        /// <param name="TotalBytes">Quantidade total de bytes do arquivo</param>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 20/05/2010
        /// </remarks>
        private void UpdateProgress(Int64 BytesRead, Int64 TotalBytes)
        {
            // Calcular o percentual do download em progresso
            PercProgress = Convert.ToInt32((BytesRead * 100) / TotalBytes);

            // Atualizar a barra de progresso
            prgDownload.Value = PercProgress;

            // Demonstrar o progresso atual do download
            lblProgresso.Text = "Baixado " + BytesRead + " de " + TotalBytes + " (" + PercProgress + "%)";

            Application.DoEvents();
        }
        #endregion

        #endregion
    }
}
