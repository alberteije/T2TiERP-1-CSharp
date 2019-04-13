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
using SintegraClient.ViewModel.SintegraERP;

namespace SintegraClient.View.SintegraERP
{
    /// <summary>
    /// Interaction logic for SintegraPrincipal.xaml
    /// </summary>
    public partial class SintegraView : UserControl
    {
        private SintegraViewModel viewModel;
        public SintegraView()
        {
            InitializeComponent();

            viewModel = new SintegraViewModel();
        }

        private void btGerar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                viewModel.gerarArquivoSintegra((DateTime)dpDataInicio.SelectedDate,
                    (DateTime)dpDataFim.SelectedDate,
                    int.Parse(((ComboBoxItem)cbFinalidadeArquivo.SelectedItem).Tag.ToString()),
                    int.Parse(((ComboBoxItem)cbNaturezaInformacao.SelectedItem).Tag.ToString()),
                    int.Parse(((ComboBoxItem)cbCodigoConvenio.SelectedItem).Tag.ToString()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro do sistema");
            }
        }

        private void dpDataInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DateTime dataFim = ((DateTime)dpDataInicio.SelectedDate).AddMonths(1);
                dpDataFim.SelectedDate = dataFim;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro do sistema");
            }
        }
    }
}
