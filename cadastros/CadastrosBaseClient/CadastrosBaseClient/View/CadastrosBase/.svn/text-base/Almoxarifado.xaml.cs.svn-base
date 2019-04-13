using System;
using System.Windows;
using System.Windows.Controls;
using CadastrosBaseClient.ViewModel.CadastrosBase;

namespace CadastrosBaseClient.View.CadastrosBase
{
    /// <summary>
    /// Interaction logic for Almoxarifado.xaml
    /// </summary>
    public partial class Almoxarifado : UserControl
    {
        public Almoxarifado()
        {
            InitializeComponent();
        }
        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((AlmoxarifadoViewModel)this.DataContext).IsEditar= false;
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
                ((AlmoxarifadoViewModel)this.DataContext).salvarAtualizarAlmoxarifado();
                MessageBox.Show("Salvo com sucesso!", "Informação do sistema");
                ((AlmoxarifadoViewModel)this.DataContext).atualizarListaAlmoxarifado(0);
                ((AlmoxarifadoViewModel)this.DataContext).IsEditar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }


    }
}
