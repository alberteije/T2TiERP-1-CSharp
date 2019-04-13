using System;
using System.Windows;
using System.Windows.Controls;
using CadastrosBaseClient.ViewModel.CadastrosBase;

namespace CadastrosBaseClient.View.CadastrosBase
{
    /// <summary>
    /// Interaction logic for EstadoCivil.xaml
    /// </summary>
    public partial class EstadoCivil : UserControl
    {
        public EstadoCivil()
        {
            InitializeComponent();
        }
        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((EstadoCivilViewModel)this.DataContext).IsEditar= false;
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
                ((EstadoCivilViewModel)this.DataContext).salvarAtualizarEstadoCivil();
                MessageBox.Show("Salvo com sucesso!", "Informação do sistema");
                ((EstadoCivilViewModel)this.DataContext).atualizarListaEstadoCivil(0);
                ((EstadoCivilViewModel)this.DataContext).IsEditar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

    }
}
