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
using PatrimonioClient.ViewModel.Patrimonio;

namespace PatrimonioClient.View.Patrimonio
{
    /// <summary>
    /// Interaction logic for PatrimApoliceSeguro.xaml
    /// </summary>
    public partial class PatrimApoliceSeguro : UserControl
    {
        public PatrimApoliceSeguro()
        {
            InitializeComponent();
        }
        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((PatrimApoliceSeguroViewModel)this.DataContext).IsEditar= false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((PatrimApoliceSeguroViewModel)this.DataContext).salvarAtualizarPatrimApoliceSeguro();
                MessageBox.Show("Salvo com sucesso!", "Informação do sistema");
                ((PatrimApoliceSeguroViewModel)this.DataContext).atualizarListaPatrimApoliceSeguro(0);
                ((PatrimApoliceSeguroViewModel)this.DataContext).IsEditar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }
		
		private void btPesquisarSeguradora_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((PatrimApoliceSeguroViewModel)DataContext).pesquisarSeguradora();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }


		
		private void btPesquisarPatrimBem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((PatrimApoliceSeguroViewModel)DataContext).pesquisarPatrimBem();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }


    }
}
