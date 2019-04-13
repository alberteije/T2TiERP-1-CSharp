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
using CadastrosBaseClient.CadastrosBaseReference;

namespace CadastrosBaseClient.View.CadastrosBase
{
    /// <summary>
    /// Interaction logic for CsosnBPrincipal.xaml
    /// </summary>
    public partial class CsosnBPrincipal : UserControl
    {
        CsosnBViewModel ViewModel = new CsosnBViewModel();
        public CsosnBPrincipal()
        {
            InitializeComponent();
            this.DataContext = ViewModel;
            ViewModel.atualizarLista(0);
        }
        private void btConsultar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ViewModel.ItemSelecionado != null)
                    tabCfop.IsSelected = true;
                else
                    MessageBox.Show("Selecione um elemento na lista.", "Alerta do sistema");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

        private void btIncluir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.ItemSelecionado = new CsosnBDTO();
                tabCfop.IsSelected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

        private void btExcluir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ViewModel.ItemSelecionado != null)
                {
                    ViewModel.excluir();
                    MessageBox.Show("Exclusão efetuada com sucesso!", "Informação do sistema");
                    ViewModel.atualizarLista(0);
                }
                else
                    MessageBox.Show("Selecione um elemento na lista.", "Alerta do sistema");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }
    }
    }

