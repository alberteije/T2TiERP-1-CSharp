using System;
using System.Windows;
using System.Windows.Controls;

namespace CadastrosBaseClient.View.CadastrosBase
{
    /// <summary>
    /// Interaction logic for Fornecedor.xaml
    /// </summary>
    public partial class Fornecedor : UserControl
    {
        public Fornecedor()
        {
            InitializeComponent();
        }
        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((FornecedorViewModel)this.DataContext).IsEditar= false;
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
                ((FornecedorViewModel)this.DataContext).salvarAtualizarFornecedor();
                MessageBox.Show("Salvo com sucesso!", "Informação do sistema");
                ((FornecedorViewModel)this.DataContext).atualizarListaFornecedor(0);
                ((FornecedorViewModel)this.DataContext).IsEditar = false;
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
                ((FornecedorViewModel)DataContext).pesquisarSituacaoForCli();
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
                ((FornecedorViewModel)DataContext).pesquisarAtividadeForCli();
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
                ((FornecedorViewModel)DataContext).pesquisarPessoa();
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
                ((FornecedorViewModel)DataContext).pesquisarContabilConta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }


    }
}
