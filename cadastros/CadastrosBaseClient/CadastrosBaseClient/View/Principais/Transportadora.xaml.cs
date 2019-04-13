using System;
using System.Windows;
using System.Windows.Controls;

namespace CadastrosBaseClient.View.CadastrosBase
{
    /// <summary>
    /// Interaction logic for Transportadora.xaml
    /// </summary>
    public partial class Transportadora : UserControl
    {
        public Transportadora()
        {
            InitializeComponent();
        }
        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((TransportadoraViewModel)this.DataContext).IsEditar= false;
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
                ((TransportadoraViewModel)this.DataContext).salvarAtualizarTransportadora();
                MessageBox.Show("Salvo com sucesso!", "Informação do sistema");
                ((TransportadoraViewModel)this.DataContext).atualizarListaTransportadora(0);
                ((TransportadoraViewModel)this.DataContext).IsEditar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

		private void btPesquisarPessoa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((TransportadoraViewModel)DataContext).pesquisarPessoa();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }


		
		private void btPesquisarContabilConta_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((TransportadoraViewModel)DataContext).pesquisarContabilConta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }


    }
}
