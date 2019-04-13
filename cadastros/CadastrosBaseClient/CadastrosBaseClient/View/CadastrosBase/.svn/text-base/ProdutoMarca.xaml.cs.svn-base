using System;
using System.Windows;
using System.Windows.Controls;
using CadastrosBaseClient.ViewModel.CadastrosBase;

namespace CadastrosBaseClient.View.CadastrosBase
{
    /// <summary>
    /// Interaction logic for ProdutoMarca.xaml
    /// </summary>
    public partial class ProdutoMarca : UserControl
    {
        public ProdutoMarca()
        {
            InitializeComponent();
        }
        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ProdutoMarcaViewModel)this.DataContext).IsEditar= false;
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
                ((ProdutoMarcaViewModel)this.DataContext).salvarAtualizarProdutoMarca();
                MessageBox.Show("Salvo com sucesso!", "Informação do sistema");
                ((ProdutoMarcaViewModel)this.DataContext).atualizarListaProdutoMarca(0);
                ((ProdutoMarcaViewModel)this.DataContext).IsEditar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

		
    }
}
