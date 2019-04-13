using System;
using System.Windows;
using System.Windows.Controls;

namespace CadastrosBaseClient.View.CadastrosBase
{
    /// <summary>
    /// Interaction logic for TipoRelacionamento.xaml
    /// </summary>
    public partial class TipoRelacionamento : UserControl
    {
        public TipoRelacionamento()
        {
            InitializeComponent();
        }
        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((TipoRelacionamentoViewModel)this.DataContext).IsEditar= false;
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
                ((TipoRelacionamentoViewModel)this.DataContext).salvarAtualizarTipoRelacionamento();
                MessageBox.Show("Salvo com sucesso!", "Informação do sistema");
                ((TipoRelacionamentoViewModel)this.DataContext).atualizarListaTipoRelacionamento(0);
                ((TipoRelacionamentoViewModel)this.DataContext).IsEditar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

    }
}
