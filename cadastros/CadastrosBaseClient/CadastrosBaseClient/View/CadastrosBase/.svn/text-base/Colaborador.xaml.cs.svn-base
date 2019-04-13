using System;
using System.Windows;
using System.Windows.Controls;
using CadastrosBaseClient.ViewModel.CadastrosBase;

namespace CadastrosBaseClient.View.CadastrosBase
{
    /// <summary>
    /// Interaction logic for Colaborador.xaml
    /// </summary>
    public partial class Colaborador : UserControl
    {
        public Colaborador()
        {
            InitializeComponent();
        }
        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ColaboradorViewModel)this.DataContext).IsEditar= false;
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
                ((ColaboradorViewModel)this.DataContext).salvarAtualizarColaborador();
                MessageBox.Show("Salvo com sucesso!", "Informação do sistema");
                ((ColaboradorViewModel)this.DataContext).atualizarListaColaborador(0);
                ((ColaboradorViewModel)this.DataContext).IsEditar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

		
		private void btPesquisarSetor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ColaboradorViewModel)DataContext).pesquisarSetor();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }


		
		private void btPesquisarCargo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ColaboradorViewModel)DataContext).pesquisarCargo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }


		
		private void btPesquisarNivelFormacao_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ColaboradorViewModel)DataContext).pesquisarNivelFormacao();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }


		
		private void btPesquisarTipoColaborador_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ColaboradorViewModel)DataContext).pesquisarTipoColaborador();
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
                ((ColaboradorViewModel)DataContext).pesquisarPessoa();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }


		
		private void btPesquisarSituacaoColaborador_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ColaboradorViewModel)DataContext).pesquisarSituacaoColaborador();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }


			
		private void btPesquisarSindicato_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ColaboradorViewModel)DataContext).pesquisarSindicato();
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
                ((ColaboradorViewModel)DataContext).pesquisarContabilConta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }


    }
}
