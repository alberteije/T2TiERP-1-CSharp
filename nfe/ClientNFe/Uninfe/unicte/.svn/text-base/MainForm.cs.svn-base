using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UniNFeLibrary;
using unicte.Formulario;
using uninfe;
using UniNFeLibrary.Enums;
using UniNFeLibrary.Formulario;

namespace unicte
{
    #region Classe MainForm
    public partial class MainForm : Form
    {
        /// <summary>
        /// executa a limpeza das pastas temp e retorno
        /// </summary>
        /// <by>http://desenvolvedores.net/marcelo</by>
        Thread oThreadLimpeza;

        private Dictionary<ServicoUniNFe, Servicos> servicosUniNfe = new Dictionary<ServicoUniNFe, Servicos>();
        private Dictionary<Thread, ParametroThread> threads = new Dictionary<Thread, ParametroThread>();

        #region MainForm()
        public MainForm()
        {
            InitializeComponent();

            try
            {
                // Executar as conversões de atualizações de versão quando tiver
                string nomeEmpresa = Auxiliar.ConversaoNovaVersao(string.Empty);
                if (!string.IsNullOrEmpty(nomeEmpresa))
                {
                    /// danasa 20-9-2010
                    /// exibe a mensagem de erro
                    MessageBox.Show("Não foi possível localizar o CNPJ da empresa no certificado configurado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    /// e pede o CNPJ
                    FormCNPJ fcnpj = new FormCNPJ(nomeEmpresa);
                    if (fcnpj.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        /// tenta processar já com o CNPJ definido
                        Auxiliar.ConversaoNovaVersao(fcnpj.Cnpj);
                }
                // Carregar as configurações de todas as empresas
                //Empresa.CarregaConfiguracao();    //danasa 20-9-2010 - Em InfoApp já é carregada
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            ///
            /// danasa 9-2009
            /// 
            XMLIniFile iniFile = new XMLIniFile(InfoApp.NomeArqXMLParams());
            iniFile.LoadForm(this, "");

            //Trazer minimizado e no systray
            notifyIcon1.Visible = true;
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            notifyIcon1.ShowBalloonTip(6000);

            this.MinimumSize = new Size(750, 600);

            #region Executar os serviços em novas threads
            //Carregar as configurações antes de executar os serviços do UNINFE
            ConfiguracaoApp.TipoAplicativo = TipoAplicativo.Cte;
            ConfiguracaoApp.CarregarDados();
            ConfiguracaoApp.VersaoXMLCanc = "1.03";
            ConfiguracaoApp.VersaoXMLConsCad = "1.03";
            ConfiguracaoApp.VersaoXMLInut = "1.03";
            ConfiguracaoApp.VersaoXMLNFe = "1.03";
            ConfiguracaoApp.VersaoXMLPedRec = "1.03";
            ConfiguracaoApp.VersaoXMLPedSit = "1.03";
            ConfiguracaoApp.VersaoXMLStatusServico = "1.03";
            ConfiguracaoApp.VersaoXMLCabecMsg = "1.03";
            ConfiguracaoApp.nsURI = "http://www.portalfiscal.inf.br/cte";
            SchemaXMLCte.CriarListaIDXML();

            //Substituir a extensão de alguns arquivos da NFe para o padrão CTE, ou 
            //os serviços do UNICTE não vai enxergar os XML´s na pasta de envio
            ExtXml.Nfe = "-cte.xml";
            ExtXmlRet.Nfe_ERR = "-cte.err";
            ExtXmlRet.ProcCancNFe = "-procCancCTe.xml";
            ExtXmlRet.ProcInutNFe = "-procInutCTe.xml";
            ExtXmlRet.ProcNFe = "-procCTe.xml";
            #endregion
        }
        #endregion

        #region Métodos gerais

        #region PararServicos()
        /// <summary>
        /// Encerrar todas as thread´s de serviços da nfe
        /// </summary>
        /// <remarks>
        /// Autor: Wandrey Mundin Ferreira
        /// Data: 01/08/2010
        /// </remarks>
        private void PararServicos()
        {
            foreach (KeyValuePair<Thread, int> t in Auxiliar.threads)
            {
                Thread thread = t.Key;

                thread.Abort();
            }
        }
        #endregion

        #region ExecutaServicos()
        /// <summary>
        /// Metodo responsável por iniciar os serviços do UniNFe em threads diferentes
        /// </summary>
        private void ExecutaServicos()
        {
            Auxiliar.threads.Clear();
            threads.Clear();

            //Primeiro eu preparo as thread´s a serem executadas, atualizo a
            //lista de thread´s e a empresa que está sendo executada nela
            //para depois iniciá-las, ou gera erros nas pesquisas pela empresa da
            //thread. Wandrey 02/08/2010
            for (int i = 0; i < Empresa.Configuracoes.Count; i++)
            {
                if (Empresa.Configuracoes[i].Certificado == string.Empty)
                    continue;

                //Criar uma lista dos serviços a serem executados
                servicosUniNfe.Clear();
                servicosUniNfe.Add(new ServicoUniNFe(), Servicos.EnviarLoteNfe);
                servicosUniNfe.Add(new ServicoUniNFe(), Servicos.AssinarNFePastaEnvio);
                servicosUniNfe.Add(new ServicoUniNFe(), Servicos.MontarLoteUmaNFe);
                servicosUniNfe.Add(new ServicoUniNFe(), Servicos.PedidoSituacaoLoteNFe);
                servicosUniNfe.Add(new ServicoUniNFe(), Servicos.PedidoConsultaSituacaoNFe);
                servicosUniNfe.Add(new ServicoUniNFe(), Servicos.AssinarNFePastaEnvioEmLote);
                servicosUniNfe.Add(new ServicoUniNFe(), Servicos.MontarLoteVariasNFe);
                servicosUniNfe.Add(new ServicoUniNFe(), Servicos.ValidarAssinar);
                servicosUniNfe.Add(new ServicoUniNFe(), Servicos.CancelarNFe);
                servicosUniNfe.Add(new ServicoUniNFe(), Servicos.InutilizarNumerosNFe);
                servicosUniNfe.Add(new ServicoUniNFe(), Servicos.PedidoConsultaStatusServicoNFe);
                servicosUniNfe.Add(new ServicoUniNFe(), Servicos.ConsultaCadastroContribuinte);
                servicosUniNfe.Add(new ServicoUniNFe(), Servicos.ConsultaInformacoesUniNFe);
                servicosUniNfe.Add(new ServicoUniNFe(), Servicos.AlterarConfiguracoesUniNFe);
                servicosUniNfe.Add(new ServicoUniNFe(), Servicos.GerarChaveNFe);
                servicosUniNfe.Add(new ServicoUniNFe(), Servicos.EmProcessamento);
                servicosUniNfe.Add(new ServicoUniNFe(), Servicos.ConverterTXTparaXML);
                if (Empresa.Configuracoes[i].DiasLimpeza != 0)  //danasa 27-2-2011
                    servicosUniNfe.Add(new ServicoUniNFe(), Servicos.LimpezaTemporario);

                //Preparar as thread´s a serem executadas
                foreach (KeyValuePair<ServicoUniNFe, Servicos> item in servicosUniNfe)
                {
                    ServicoUniNFe servico = item.Key;
                    Thread t = new Thread(new ParameterizedThreadStart(servico.BuscaXML));
                    t.Name = (item.Value.ToString().Trim() + Empresa.Configuracoes[i].CNPJ.Trim()).ToUpper();

                    //Atualiza a coleção de thread´s e a empresa que será executada enal
                    Auxiliar.threads.Add(t, i);

                    //Atualizar a coleção das thread´s a serem executadas.
                    threads.Add(t, new ParametroThread(item.Value));
                }
            }
            //Executar as thread´s de todas as empresas
            foreach (KeyValuePair<Thread, ParametroThread> item in threads)
            {
                Thread t = item.Key;
                t.Start(item.Value);
                if (Empresa.Configuracoes.Count > 1)
                    Thread.Sleep(100);  //danasa 9-2010
            }
            //Limpar para tirar o conteúdo da memória pois não vamos mais precisar
            threads.Clear();
        }
        #endregion

        #endregion

        #region Métodos de eventos

        private void MainForm_Resize(object sender, EventArgs e)
        {
            ///
            /// danasa 9-2009
            /// 
            if (this.WindowState != FormWindowState.Minimized)
            {
                XMLIniFile iniFile = new XMLIniFile(InfoApp.NomeArqXMLParams());
                iniFile.SaveForm(this, "");
                iniFile.Save();
            }
            //Faz a aplicação sumir da barra de tarefas
            //danasa
            //  Se usuario mudar o tamanho da janela, não pode desaparece-la da tasknar
            if (this.WindowState == FormWindowState.Minimized)
                this.ShowInTaskbar = false;

            //Mostrar o balão com as informações que selecionamos
            //O parâmetro passado refere-se ao tempo (ms)
            // em que ficará aparecendo. Coloque "0" se quiser
            // que ele feche somente quando o usuário clicar

            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.ShowBalloonTip(6000);
            }
            //Ativar o ícone na área de notificação
            //para isso a propriedade Visible deveria ser setada
            //como false, mas prefiro deixar o ícone lá.
            //notifyIcon1.Visible = true;
        }

        #region -- Show desktop
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.notifyIcon1_MouseDoubleClick(sender, null);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Voltar a janela em seu estado normal
            this.WindowState = FormWindowState.Normal;

            // Faz a aplicação aparecer na barra de tarefas.            
            this.ShowInTaskbar = true;

            // Levando o Form de volta para a tela.

            this.WindowState = FormWindowState.Normal;
            this.Visible = true;

            // Faz desaparecer o ícone na área de notificação,
            // para isso a propriedade Visible deveria ser setada 
            // como true no evento Resize do Form.

            // notifyIcon1.Visible=false;
        }
        #endregion

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            PararServicos();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //
            // TODO: Aqui, deveriamos verificar se ainda existe alguma Thread pendente antes de fechar
            //
            if (e.CloseReason == CloseReason.UserClosing && !Auxiliar.EncerrarApp)
            {
                ///
                /// danasa 9-2009
                /// 
                if (this.WindowState != FormWindowState.Minimized)
                {
                    XMLIniFile iniFile = new XMLIniFile(InfoApp.NomeArqXMLParams());
                    iniFile.SaveForm(this, "");
                    iniFile.Save();
                }
                // se o botão de fechar for pressionado pelo usuário, o mainform não será fechado em sim minimizado.
                e.Cancel = true;
                this.Visible = false;
                this.OnResize(e);
                notifyIcon1.ShowBalloonTip(6000);
            }
            else
            {
                e.Cancel = false;  //se o PC for desligado o windows o fecha automaticamente.
            }
        }

        #region -- Sobre o UniNFe
        private void toolStripButton_sobre_Click(object sender, EventArgs e)
        {
            this.toolStripButton_sobre.Enabled =
                this.sobreOUniNFeToolStripMenuItem.Enabled = false;
            using (FormSobre oSobre = new FormSobre())
            {
                oSobre.MinimizeBox =
                    oSobre.ShowInTaskbar = !(sender is ToolStripButton);
                oSobre.ShowDialog();
            }
            this.sobreOUniNFeToolStripMenuItem.Enabled =
                this.toolStripButton_sobre.Enabled = true;
        }
        #endregion

        #region -- Consulta servico e cadastro
        private int CadastroAtivo()
        {
            FormConsultaCadastro oCadastro = null;
            //danasa 
            foreach (Form fg in this.MdiChildren)
            {
                if (fg is FormConsultaCadastro)
                {
                    ///
                    /// configuracão já está ativa como MDI
                    /// 
                    this.notifyIcon1_MouseDoubleClick(null, null);
                    oCadastro = fg as FormConsultaCadastro;
                    oCadastro.WindowState = FormWindowState.Normal;
                    return 1;
                }
            }
            foreach (Form fg in Application.OpenForms)
            {
                if (fg is FormConsultaCadastro)
                {
                    oCadastro = fg as FormConsultaCadastro;
                    oCadastro.WindowState = FormWindowState.Normal;
                    return 0;
                }
            }
            return -1;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (Empresa.Configuracoes.Count <= 0)
            {
                MessageBox.Show("É necessário cadastrar e configurar as empresas que serão gerenciadas pelo aplicativo.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (CadastroAtivo())
            {
                case 0:
                    ///
                    /// configuracao ja existe como Modal
                    /// minimiza o MainForm para que a tela de configuracao esteja visivel
                    /// 
                    this.WindowState = FormWindowState.Minimized;
                    break;

                case -1:
                    {
                        FormConsultaCadastro consultaCadastro = new FormConsultaCadastro();
                        consultaCadastro.MdiParent = this;
                        consultaCadastro.MinimizeBox = false;
                        consultaCadastro.Show();
                    }
                    break;
            }
        }

        private void cmConsultaCadastroServico_Click(object sender, EventArgs e)
        {
            if (Empresa.Configuracoes.Count <= 0)
            {
                MessageBox.Show("É necessário cadastrar e configurar as empresas que serão gerenciadas pelo aplicativo.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (CadastroAtivo())
            {
                case -1:
                    ///
                    /// tela principal está visivel?
                    /// 
                    if (this.WindowState != FormWindowState.Minimized)
                        ///
                        /// então abre o cadastro como MDI
                        /// 
                        this.toolStripButton1_Click(sender, e);
                    else
                        using (FormConsultaCadastro consultaCadastro = new FormConsultaCadastro())
                        {
                            consultaCadastro.MinimizeBox = true;
                            consultaCadastro.ShowInTaskbar = true;
                            consultaCadastro.ShowDialog();
                        }
                    break;
            }
            //this.DemonstrarStatusServico();
        }
        #endregion

        #region -- Validar
        private void toolStripButton_validarxml_Click(object sender, EventArgs e)
        {
            if (Empresa.Configuracoes.Count <= 0)
            {
                MessageBox.Show("É necessário cadastrar e configurar as empresas que serão gerenciadas pelo aplicativo.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ValidarXML oValidarXML = new ValidarXML();
            oValidarXML.MdiParent = this;
            oValidarXML.MinimizeBox = false;
            oValidarXML.Show();
        }

        private void vaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Empresa.Configuracoes.Count <= 0)
            {
                MessageBox.Show("É necessário cadastrar e configurar as empresas que serão gerenciadas pelo aplicativo.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (ValidarXML oValidarXML = new ValidarXML())
            {
                oValidarXML.ShowInTaskbar = true;
                oValidarXML.MinimizeBox = true;
                oValidarXML.ShowDialog();
            }
        }
        #endregion

        #region -- Configuracao
        private int ConfiguracaoAtiva()
        {
            Configuracao oConfig = null;
            //danasa 
            foreach (Form fg in this.MdiChildren)
            {
                if (fg is Configuracao)
                {
                    ///
                    /// configuracão já está ativa como MDI
                    /// 
                    this.notifyIcon1_MouseDoubleClick(null, null);
                    oConfig = fg as Configuracao;
                    oConfig.WindowState = FormWindowState.Normal;
                    return 1;
                }
            }
            foreach (Form fg in Application.OpenForms)
            {
                if (fg is Configuracao)
                {
                    oConfig = fg as Configuracao;
                    oConfig.WindowState = FormWindowState.Normal;
                    return 0;
                }
            }
            return -1;
        }

        ///
        /// danasa 9-2010
        private void onCloseConfiguracao(object sender, EventArgs e)
        {
            /// danasa 20-9-2010
            FormWait fw = new FormWait();
            this.Cursor = Cursors.WaitCursor;
            try
            {
                fw.Show();
                fw.DisplayMessage("Parando os serviços");
                this.PararServicos();
                fw.DisplayMessage("Iniciando os serviços");
                this.ExecutaServicos();
            }
            finally
            {
                this.Cursor = Cursors.Default;
                fw.Dispose();
            }
        }

        private void toolStripButton_config_Click(object sender, EventArgs e)
        {
            if (Empresa.Configuracoes.Count <= 0)
            {
                MessageBox.Show("É necessário cadastrar e configurar as empresas que serão gerenciadas pelo aplicativo.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (ConfiguracaoAtiva())
            {
                case 0:
                    ///
                    /// configuracao ja existe como Modal
                    /// minimiza o MainForm para que a tela de configuracao esteja visivel
                    /// 
                    this.WindowState = FormWindowState.Minimized;
                    break;

                case -1:
                    {
                        try
                        {
                            Configuracao oConfig = new Configuracao(onCloseConfiguracao);
                            oConfig.MdiParent = this;
                            oConfig.MinimizeBox = false;
                            oConfig.Show();
                        }
                        catch
                        {
                        }
                    }
                    break;
            }
        }

        private void configuraçõesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Empresa.Configuracoes.Count <= 0)
            {
                MessageBox.Show("É necessário cadastrar e configurar as empresas que serão gerenciadas pelo aplicativo.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (ConfiguracaoAtiva())
            {
                case -1:
                    ///
                    /// tela principal está visivel?
                    /// 
                    if (this.WindowState != FormWindowState.Minimized)
                        ///
                        /// então abre a configuração como MDI
                        /// 
                        toolStripButton_config_Click(sender, e);
                    else
                        using (Configuracao oConfig = new Configuracao(onCloseConfiguracao))
                        {
                            oConfig.MinimizeBox = true;
                            oConfig.ShowDialog();
                        }
                    break;
            }
        }
        #endregion

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Auxiliar.EncerrarApp = true;

            this.Close();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Application.StartupPath + "\\" + InfoApp.NomeAplicacao() + ".pdf"))
                {
                    System.Diagnostics.Process.Start(Application.StartupPath + InfoApp.NomeAplicacao() + ".pdf");
                }
                else
                {
                    MessageBox.Show("Não foi possível localizar o arquivo de manual do sistema.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void toolStripBtnUpdate_Click(object sender, EventArgs e)
        {
            FormUpdate FormUp = new FormUpdate("i" + InfoApp.NomeAplicacao().ToLower() + ".exe");
            FormUp.MdiParent = this;
            FormUp.MinimizeBox = false;
            FormUp.Show();
        }

        ///
        /// danasa 9-2010
        private void onCloseEmpresas(object sender, EventArgs e)
        {
            /// danasa 20-9-2010
            FormWait fw = new FormWait();
            this.Cursor = Cursors.WaitCursor;
            try
            {
                fw.Show();
                fw.DisplayMessage("Parando os serviços");
                this.PararServicos();
                fw.DisplayMessage("Lendo as empresas");
                Empresa.CarregaConfiguracao();
                fw.DisplayMessage("Iniciando os serviços");
                this.ExecutaServicos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                fw.Dispose();
                this.Cursor = Cursors.Default;
            }
        }

        private void tsbEmpresa_Click(object sender, EventArgs e)
        {
            FormEmpresa frmEmpresa = new FormEmpresa(onCloseEmpresas);  //danasa 20-9-2010
            frmEmpresa.MdiParent = this;
            frmEmpresa.MinimizeBox = false;
            frmEmpresa.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Executar os serviços do UniNFe em novas threads
            //Tem que ser carregado depois que o formulário da MainForm estiver totalmente carregado para evitar Erros. Wandrey 19/10/2010
            this.ExecutaServicos();
        }
    }
    #endregion
}