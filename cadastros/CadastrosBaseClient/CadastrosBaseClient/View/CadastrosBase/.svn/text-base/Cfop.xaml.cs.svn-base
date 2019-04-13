using System;
using System.Windows;
using System.Windows.Controls;
using CadastrosBaseClient.ViewModel.CadastrosBase;

namespace CadastrosBaseClient.View.CadastrosBase
{
    /// <summary>
    /// Interaction logic for Cfop.xaml
    /// </summary>
    public partial class Cfop : UserControl
    {
        public Cfop()
        {
            InitializeComponent();
        }
        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((CfopViewModel)this.DataContext).IsEditar= false;
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
                ((CfopViewModel)this.DataContext).salvarAtualizarCfop();
                MessageBox.Show("Salvo com sucesso!", "Informação do sistema");
                ((CfopViewModel)this.DataContext).atualizarListaCfop(0);
                ((CfopViewModel)this.DataContext).IsEditar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

		
    }
}
