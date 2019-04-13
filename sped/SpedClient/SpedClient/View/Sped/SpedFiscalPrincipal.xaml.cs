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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SpedClient.ViewModel.Sped;
using System.Diagnostics;
using SpedClient.ServicoSpedReference;
using System.IO;
using SpedClient.ViewModel;

namespace SpedClient.View.Sped
{
    /// <summary>
    /// Interaction logic for SpedPrincipal.xaml
    /// </summary>
    public partial class SpedFiscalPrincipal : UserControl
    {
        public ArquivoDTO documentoSelected;
        public static string CaminhoArquivo;
        public static Window JanelaPreview;


        public SpedFiscalPrincipal()
        {
            InitializeComponent();
        }

        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            ERPClient.JanelaSpedFiscal.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                ServicoSpedClient gedService = new ServicoSpedClient();
                documentoSelected = gedService.gerarSped(
                    dpDataInicio.Text, 
                    dpDataFim.Text, 
                    cbVersaoLayout.SelectedIndex, 
                    cbFinalidadeArquivo.SelectedIndex, 
                    cbPerfilApresentacao.SelectedIndex,
                    new ERPViewModelBase().Empresa.Id, 
                    cbInventario.SelectedIndex, 
                    1
                     );

                CaminhoArquivo = salvaArquivoTempLocal(documentoSelected);

                //System.Diagnostics.Process.Start(CaminhoArquivo); - se quiser abrir no editor padrão do windows

                PreviewPrincipal janela = new PreviewPrincipal();
                Window window = new Window()
                {
                    Title = "Preview",
                    ShowInTaskbar = false,
                    Topmost = false,
                    ResizeMode = ResizeMode.NoResize,
                    Width = 1010,
                    Height = 740,
                    WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen
                };
                window.Content = janela;
                JanelaPreview = window;
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string salvaArquivoTempLocal(ArquivoDTO arquivo)
        {
            try
            {
                FileInfo fi = arquivo.fileInfo;
                string caminhoTemp = System.IO.Path.GetTempPath() + arquivo.fileInfo.Name;

                if (!File.Exists(caminhoTemp))
                {
                    using (FileStream fs = new FileStream(caminhoTemp, FileMode.Create, FileAccess.ReadWrite))
                    {
                        arquivo.memoryStream.WriteTo(fs);
                        fs.Close();
                    }
                }
                return fi.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
