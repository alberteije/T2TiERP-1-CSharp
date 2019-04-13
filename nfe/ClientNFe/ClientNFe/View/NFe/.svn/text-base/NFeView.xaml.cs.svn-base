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
using ExportarParaArquivo.Control;
using ClientNFe.NFeServiceReference;

namespace ClientNFe.View.NFe
{
    /// <summary>
    /// Interaction logic for NFeView.xaml
    /// </summary>
    public partial class NFeView : UserControl
    {
        NFeViewModel nfeViewModel = new NFeViewModel();

        public NFeView()
        {
            InitializeComponent();
            DataContext = nfeViewModel;
            nfeViewModel.carregarTabLista();
        }

        private void ButtonExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                nfeViewModel.exportarDataGrid((ExportarParaArquivo.Formato)(
            (ButtonExport)sender).ExportFileFormat,
            (DataGrid)((ButtonExport)sender).ExportDataGridSource);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

        private void carregarViewDados()
        {
            try
            {
                tabDados.Content = new NFeDados();
                tabDados.IsSelected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }
        private void btNovo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                nfeViewModel.nfeSelected = new NFeCabecalhoDTO();
                nfeViewModel.carregarTabDados();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

        private void btConsultar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (nfeViewModel.nfeSelected != null)
                {
                    nfeViewModel.carregarTabDados();
                }
                else
                    MessageBox.Show("Selecione a NF-e para consulta", "Informação do sistema");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

    }
}
