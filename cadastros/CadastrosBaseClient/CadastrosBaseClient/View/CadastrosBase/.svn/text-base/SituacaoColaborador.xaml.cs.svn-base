using System;
using System.Windows;
using System.Windows.Controls;

namespace CadastrosBaseClient.View.CadastrosBase
{
    /// <summary>
    /// Interaction logic for SituacaoColaborador.xaml
    /// </summary>
    public partial class SituacaoColaborador : UserControl
    {
        public SituacaoColaborador()
        {
            InitializeComponent();
        }
        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((SituacaoColaboradorViewModel)this.DataContext).IsEditar= false;
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
                ((SituacaoColaboradorViewModel)this.DataContext).salvarAtualizarSituacaoColaborador();
                MessageBox.Show("Salvo com sucesso!", "Informação do sistema");
                ((SituacaoColaboradorViewModel)this.DataContext).atualizarListaSituacaoColaborador(0);
                ((SituacaoColaboradorViewModel)this.DataContext).IsEditar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

    }
}
