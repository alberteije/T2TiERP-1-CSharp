using System;
using System.Windows;
using System.Windows.Controls;

namespace CadastrosBaseClient.View.CadastrosBase
{
    /// <summary>
    /// Interaction logic for TipoAdmissao.xaml
    /// </summary>
    public partial class TipoAdmissao : UserControl
    {
        public TipoAdmissao()
        {
            InitializeComponent();
        }
        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((TipoAdmissaoViewModel)this.DataContext).IsEditar= false;
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
                ((TipoAdmissaoViewModel)this.DataContext).salvarAtualizarTipoAdmissao();
                MessageBox.Show("Salvo com sucesso!", "Informação do sistema");
                ((TipoAdmissaoViewModel)this.DataContext).atualizarListaTipoAdmissao(0);
                ((TipoAdmissaoViewModel)this.DataContext).IsEditar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

    }
}
