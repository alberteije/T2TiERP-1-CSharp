using System;
using System.Windows;
using System.Windows.Controls;

namespace CadastrosBaseClient.View.CadastrosBase
{
    /// <summary>
    /// Interaction logic for Cliente.xaml
    /// </summary>
    public partial class Cliente : UserControl
    {
        public Cliente()
        {
            InitializeComponent();
        }
        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ClienteViewModel)this.DataContext).IsEditar= false;
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
                ((ClienteViewModel)this.DataContext).salvarAtualizarCliente();
                MessageBox.Show("Salvo com sucesso!", "Informação do sistema");
                ((ClienteViewModel)this.DataContext).atualizarListaCliente(0);
                ((ClienteViewModel)this.DataContext).IsEditar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

		private void btPesquisarSituacaoForCli_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ClienteViewModel)DataContext).pesquisarSituacaoForCli();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }


		
		private void btPesquisarAtividadeForCli_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ClienteViewModel)DataContext).pesquisarAtividadeForCli();
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
                ((ClienteViewModel)DataContext).pesquisarPessoa();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }


		
		private void btPesquisarTributOperacaoFiscal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ClienteViewModel)DataContext).pesquisarTributOperacaoFiscal();
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
                ((ClienteViewModel)DataContext).pesquisarContabilConta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }


    }
}
