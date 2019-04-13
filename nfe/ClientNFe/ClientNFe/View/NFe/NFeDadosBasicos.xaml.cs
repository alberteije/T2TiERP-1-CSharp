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

namespace ClientNFe.View.NFe
{
    /// <summary>
    /// Interaction logic for NFeDadosBasicos.xaml
    /// </summary>
    public partial class NFeDadosBasicos : UserControl
    {
        public NFeDadosBasicos()
        {
            InitializeComponent();
        }

        private void btPesquisarProduto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((NFeViewModel)DataContext).pesquisarOperacaoFiscal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }
    }
}
