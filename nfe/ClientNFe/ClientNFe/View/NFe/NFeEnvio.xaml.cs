using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Threading;

namespace ClientNFe.View.NFe
{
    /// <summary>
    /// Interaction logic for NFeEnvio.xaml
    /// </summary>
    public partial class NFeEnvio : Window
    {
        private NFeViewModel nfeViewModel;
        private readonly BackgroundWorker bw = new BackgroundWorker();
        private Dispatcher lbDispatcher;
        AtualizarListBoxDelegate lbDelegate;
        private int numLote;

        public NFeEnvio(NFeViewModel nfeVM)
        {
            InitializeComponent();

            nfeViewModel = nfeVM;
            nfeViewModel.ativarServicosNFE();

            bw.DoWork += worker_DoWork;
            bw.RunWorkerCompleted += worker_RunWorkerCompleted;
            bw.ProgressChanged += worker_ProgressChanged;

            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            lbDispatcher = lbEnvio.Dispatcher;
            lbDelegate = new AtualizarListBoxDelegate(atualizarListBox);
        }

        public void envioNFe()
        {
            try
            {
                bw.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }


        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbEnvio.Value = e.ProgressPercentage;
        }
        private delegate void AtualizarListBoxDelegate(string texto);
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            bw.ReportProgress(5);
            lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, "Iniciar envio.");

            nfeViewModel.excluirArquivos();

            bw.ReportProgress(10);
            lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, "Consulta status do serviço.");
            if (this.consultarStatus() == 0)
            {
                bw.ReportProgress(25);
                if (this.prepararNFe() == 0)
                {
                    bw.ReportProgress(35);
                    if (this.enviarNFe() == 0)
                    {
                        bw.ReportProgress(50);
                        if (this.processarMensagemLoteNFe() == 0)
                        {
                            bw.ReportProgress(60);
                            if (this.processarMensagemRecebimentoLoteNFe() == 0)
                            {
                                bw.ReportProgress(80);
                                if (this.processarMensagemRecebimentoProcLoteNFe() == 0)
                                {
                                    bw.ReportProgress(100);
                                    lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, "Processamento concluído.");
                                }
                            }
                        }
                    }
                }
            }
            
        }



        private void atualizarListBox(string text)
        {
            lbEnvio.Items.Add(DateTime.Now.ToString() + " - " + text);
        }

        private void worker_RunWorkerCompleted(object sender,
                                               RunWorkerCompletedEventArgs e)
        {
            btSair.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(
                delegate() {
                    btSair.Visibility = Visibility.Visible;
                }));
        }

        private int processarMensagemRecebimentoProcLoteNFe()
        {
            int resultado = -1;
            try
            {
                lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, "NF-e enviada. Aguardando processamento.");
                if (nfeViewModel.receberMensagemProcLoteNFe() && nfeViewModel.retConsReci != null)
                {
                    lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, "==== LOTE PROCESSADO ====");
                    lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, nfeViewModel.retConsReci.protNFe[0].infProt.dhRecbto.ToString());
                    lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, "Resultado: " + nfeViewModel.retConsReci.protNFe[0].infProt.xMotivo);
                    if(!string.IsNullOrEmpty(nfeViewModel.retConsReci.protNFe[0].infProt.nProt))
                        lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, "Protocolo - "+ nfeViewModel.retConsReci.protNFe[0].infProt.nProt);
                    lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, "=========================");

                    resultado = 0;
                }
                else
                {
                    lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, "Processamento lote falhou.");
                    resultado = -1;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                bw.CancelAsync();
                MessageBox.Show(ex.Message, "Alerta do sistema");
                return -1;
            }
            return resultado;
        }
        private int processarMensagemRecebimentoLoteNFe()
        {
            int resultado = -1;
            try
            {
                lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, "Aguardando confirmação do recebimento.");
                if (nfeViewModel.receberMensagemLoteNFe(numLote) && nfeViewModel.retEnviNFe != null )
                {
                    lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, "==== LOTE RECEBIDO ====");
                    lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, nfeViewModel.retEnviNFe.dhRecbto.ToString());
                    lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, nfeViewModel.retEnviNFe.xMotivo);
                    if(!string.IsNullOrEmpty(nfeViewModel.retEnviNFe.infRec.nRec))
                        lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, "Nr. recebimento: "+nfeViewModel.retEnviNFe.infRec.nRec);
                    lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, "=======================");
                    resultado = 0;
                }
                else
                {
                    lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, "Processamento lote falhou.");
                    resultado = -1;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                bw.CancelAsync();
                MessageBox.Show(ex.Message, "Alerta do sistema");
                return -1;
            }
            return resultado;
        }
        private int processarMensagemLoteNFe()
        {
            int resultado = -1;
            try
            {
                lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, "Enviando arquivo.");
                string numeroLote = "";
                if(nfeViewModel.receberMensagemNrLoteNFe(out numeroLote))
                {
                    lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, "Nr. do lote: "+numeroLote);
                    numLote = int.Parse(numeroLote);
                    resultado = 0;
                }else
                {
                    lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, "Envio falhou:");
                    lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, numeroLote);
                    resultado = -1;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                bw.CancelAsync();
                MessageBox.Show(ex.Message, "Alerta do sistema");
                return -1;
            }
            return resultado;
        }
        private int enviarNFe()
        {
            int resultado = 0;
            try
            {
                lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, "Enviando NF-e.");
                nfeViewModel.enviarNFe();
            }
            catch (Exception ex)
            {
                bw.CancelAsync();
                MessageBox.Show(ex.Message, "Alerta do sistema");
                return -1;
            }
            return resultado;
        }
        private int prepararNFe()
        {
            try
            {
                bool resultadoOK = false;
                resultadoOK = nfeViewModel.montarNFe();

                if (resultadoOK)
                {
                    lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, "Arquivo XML preparado com sucesso.");
                    return 0;
                }
                else
                {
                    lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, "Falha na montagem do arquivo XML.");
                    bw.CancelAsync();
                    return -1;
                }
            }
            catch (Exception ex)
            {
                bw.CancelAsync();
                MessageBox.Show(ex.Message, "Alerta do sistema");
                return -1;
            }
        }

        private int consultarStatus()
        {
            try
            {
                bool resultadoOK = false;
                object retorno;
                resultadoOK = nfeViewModel.consultarStatusNFe(out retorno);

                lbDispatcher.BeginInvoke(DispatcherPriority.Normal, lbDelegate, "Status: " + retorno.ToString());

                if (!resultadoOK)
                {
                    bw.CancelAsync();
                    return -1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                bw.CancelAsync();
                MessageBox.Show(ex.Message, "Alerta do sistema");
                return -1;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                nfeViewModel.desativarServicosNFE();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

    }
}
