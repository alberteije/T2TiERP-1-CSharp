using System;
using System.Windows;
using System.Windows.Controls;
using CadastrosBaseClient.ViewModel.CadastrosBase;

namespace CadastrosBaseClient.View.CadastrosBase
{
    /// <summary>
    /// Interaction logic for Contador.xaml
    /// </summary>
    public partial class Contador : UserControl
    {
        public Contador()
        {
            InitializeComponent();
        }
        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ContadorViewModel)this.DataContext).IsEditar= false;
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
                ((ContadorViewModel)this.DataContext).salvarAtualizarContador();
                MessageBox.Show("Salvo com sucesso!", "Informação do sistema");
                ((ContadorViewModel)this.DataContext).atualizarListaContador(0);
                ((ContadorViewModel)this.DataContext).IsEditar = false;
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
                ((ContadorViewModel)DataContext).pesquisarPessoa();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }


    }
}
