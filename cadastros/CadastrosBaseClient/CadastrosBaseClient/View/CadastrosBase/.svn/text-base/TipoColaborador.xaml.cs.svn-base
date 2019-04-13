using System;
using System.Windows;
using System.Windows.Controls;

namespace CadastrosBaseClient.View.CadastrosBase
{
    /// <summary>
    /// Interaction logic for TipoColaborador.xaml
    /// </summary>
    public partial class TipoColaborador : UserControl
    {
        public TipoColaborador()
        {
            InitializeComponent();
        }
        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((TipoColaboradorViewModel)this.DataContext).IsEditar= false;
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
                ((TipoColaboradorViewModel)this.DataContext).salvarAtualizarTipoColaborador();
                MessageBox.Show("Salvo com sucesso!", "Informação do sistema");
                ((TipoColaboradorViewModel)this.DataContext).atualizarListaTipoColaborador(0);
                ((TipoColaboradorViewModel)this.DataContext).IsEditar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

    }
}
