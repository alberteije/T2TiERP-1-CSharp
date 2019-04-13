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
    /// Interaction logic for Salario_Minimo.xaml
    /// </summary>
    public partial class SalarioMinimo : UserControl
    {
        public SalarioMinimo()
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
                ((SalarioMinimoViewModel)this.DataContext).salvarAtualizarSalarioMinimo();
                MessageBox.Show("Salvo com sucesso!", "Informação do sistema");
                ((SalarioMinimoViewModel)this.DataContext).atualizarListaSalarioMinimo(0);
                ((SalarioMinimoViewModel)this.DataContext).IsEditar = false;
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
                ((SalarioMinimoViewModel)this.DataContext).IsEditar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

    }
}
