using System;
using System.Windows;
using System.Windows.Controls;
using CadastrosBaseClient.ViewModel.CadastrosBase;

namespace CadastrosBaseClient.View.CadastrosBase
{
    /// <summary>
    /// Interaction logic for UnidadeProduto.xaml
    /// </summary>
    public partial class UnidadeProduto : UserControl
    {
        public UnidadeProduto()
        {
            InitializeComponent();
        }
        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((UnidadeProdutoViewModel)this.DataContext).IsEditar= false;
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
                ((UnidadeProdutoViewModel)this.DataContext).salvarAtualizarUnidadeProduto();
                MessageBox.Show("Salvo com sucesso!", "Informação do sistema");
                ((UnidadeProdutoViewModel)this.DataContext).atualizarListaUnidadeProduto(0);
                ((UnidadeProdutoViewModel)this.DataContext).IsEditar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

		
    }
}
