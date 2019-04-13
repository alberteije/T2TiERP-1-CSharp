using System;
using System.Windows;
using System.Windows.Controls;
using CadastrosBaseClient.ViewModel.CadastrosBase;

namespace CadastrosBaseClient.View.CadastrosBase
{
    /// <summary>
    /// Interaction logic for ProdutoSubGrupo.xaml
    /// </summary>
    public partial class ProdutoSubGrupo : UserControl
    {
        public ProdutoSubGrupo()
        {
            InitializeComponent();
        }
        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ProdutoSubGrupoViewModel)this.DataContext).IsEditar= false;
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
                ((ProdutoSubGrupoViewModel)this.DataContext).salvarAtualizarProdutoSubGrupo();
                MessageBox.Show("Salvo com sucesso!", "Informação do sistema");
                ((ProdutoSubGrupoViewModel)this.DataContext).atualizarListaProdutoSubGrupo(0);
                ((ProdutoSubGrupoViewModel)this.DataContext).IsEditar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

		private void btPesquisarProdutoGrupo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ProdutoSubGrupoViewModel)DataContext).pesquisarProdutoGrupo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }


    }
}
