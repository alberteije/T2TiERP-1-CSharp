using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CadastrosBaseClient.ViewModel.CadastrosBase;

namespace CadastrosBaseClient.View.CadastrosBase
{
    /// <summary>
    /// Interaction logic for Sefip_Codigo_Movimentacao.xaml
    /// </summary>
    public partial class SefipCodigoMovimentacao : UserControl
    {
        public SefipCodigoMovimentacao()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((SefipCodigoMovimentacaoViewModel)this.DataContext).salvarAtualizarSefipCodigoMovimentacao();
                MessageBox.Show("Salvo com sucesso!", "Informação do sistema");
                ((SefipCodigoMovimentacaoViewModel)this.DataContext).atualizarListaSefipCodigoMovimentacao(0);
                ((SefipCodigoMovimentacaoViewModel)this.DataContext).IsEditar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

        private void btnSair_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((SefipCodigoMovimentacaoViewModel)this.DataContext).IsEditar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }
    }
}
