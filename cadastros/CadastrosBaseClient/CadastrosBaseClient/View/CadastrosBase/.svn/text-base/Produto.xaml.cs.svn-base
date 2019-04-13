using System;
using System.Windows;
using System.Windows.Controls;
using CadastrosBaseClient.ViewModel.CadastrosBase;

namespace CadastrosBaseClient.View.CadastrosBase
{
    /// <summary>
    /// Interaction logic for Produto.xaml
    /// </summary>
    public partial class Produto : UserControl
    {
        public Produto()
        {
            InitializeComponent();
        }
        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ProdutoViewModel)this.DataContext).IsEditar= false;
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
                ((ProdutoViewModel)this.DataContext).salvarAtualizarProduto();
                MessageBox.Show("Salvo com sucesso!", "Informação do sistema");
                ((ProdutoViewModel)this.DataContext).atualizarListaProduto(0);
                ((ProdutoViewModel)this.DataContext).IsEditar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

		private void btPesquisarProdutoSubGrupo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ProdutoViewModel)DataContext).pesquisarProdutoSubGrupo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }


		
		private void btPesquisarProdutoMarca_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ProdutoViewModel)DataContext).pesquisarProdutoMarca();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }


		
		private void btPesquisarTributGrupoTributario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ProdutoViewModel)DataContext).pesquisarTributGrupoTributario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }


		
		private void btPesquisarAlmoxarifado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ProdutoViewModel)DataContext).pesquisarAlmoxarifado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }


		
		private void btPesquisarUnidadeProduto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ProdutoViewModel)DataContext).pesquisarUnidadeProduto();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }


		
		private void btPesquisarTributIcmsCustomCab_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ProdutoViewModel)DataContext).pesquisarTributIcmsCustomCab();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }


    }
}
