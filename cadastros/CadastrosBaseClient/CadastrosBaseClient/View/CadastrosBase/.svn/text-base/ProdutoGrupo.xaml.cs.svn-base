using System;
using System.Windows;
using System.Windows.Controls;
using CadastrosBaseClient.ViewModel.CadastrosBase;

namespace CadastrosBaseClient.View.CadastrosBase
{
    /// <summary>
    /// Interaction logic for ProdutoGrupo.xaml
    /// </summary>
    public partial class ProdutoGrupo : UserControl
    {
        public ProdutoGrupo()
        {
            InitializeComponent();
        }
        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ProdutoGrupoViewModel)this.DataContext).IsEditar= false;
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
                ((ProdutoGrupoViewModel)this.DataContext).salvarAtualizarProdutoGrupo();
                MessageBox.Show("Salvo com sucesso!", "Informação do sistema");
                ((ProdutoGrupoViewModel)this.DataContext).atualizarListaProdutoGrupo(0);
                ((ProdutoGrupoViewModel)this.DataContext).IsEditar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

		
    }
}
