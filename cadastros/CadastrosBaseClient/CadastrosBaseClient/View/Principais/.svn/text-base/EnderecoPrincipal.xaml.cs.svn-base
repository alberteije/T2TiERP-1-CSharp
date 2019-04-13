using System;
using System.Windows;
using System.Windows.Controls;
using CadastrosBaseClient.CadastrosPrincipaisReference;
using CadastrosBaseClient.ViewModel.CadastrosBase;

namespace CadastrosBaseClient.View.CadastrosBase
{
    /// <summary>
    /// Interaction logic for DetalhePrincipal.xaml
    /// </summary>
    public partial class EnderecoPrincipal : UserControl
    {
        public EnderecoPrincipal()
        {
            InitializeComponent();
        }

        private void btIncluir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EnderecoDTO detalheDTO = new EnderecoDTO();
                detalheDTO.IdPessoa = ((PessoaViewModel)DataContext).PessoaSelected.Id;

                ((PessoaViewModel)DataContext).EnderecoSelected = detalheDTO;
                Endereco viewDetalhe = new Endereco();
                viewDetalhe.btSair.Click += new RoutedEventHandler(btSair_Click);
                viewDetalhe.btGravar.Click += new RoutedEventHandler(btGravar_Click);
                tabDetalhe.Content = viewDetalhe;
                tabDetalhe.IsSelected = true;
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
                if (((PessoaViewModel)DataContext).PessoaSelected != null)
                {
                    ((PessoaViewModel)DataContext).PessoaSelected.
                        ListaEndereco.Remove(
                        ((PessoaViewModel)DataContext).EnderecoSelected);
                    viewLista.dataGrid.Items.Refresh();
                }                
                else
                    MessageBox.Show("Selecione um elemento na lista.", "Alerta do sistema");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

        private void btConsultar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((PessoaViewModel)DataContext).EnderecoSelected != null)
                {
                    tabDetalhe.IsSelected = true;
                    Endereco viewDetalhe = new Endereco();
                    viewDetalhe.btSair.Click += new RoutedEventHandler(btSair_Click);
                    viewDetalhe.btGravar.Click += new RoutedEventHandler(btGravar_Click);
                    tabDetalhe.Content = viewDetalhe;
                }                
                else
                    MessageBox.Show("Selecione um elemento na lista.", "Alerta do sistema");
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        void btGravar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((PessoaViewModel)DataContext).PessoaSelected.
                    ListaEndereco.Add(((PessoaViewModel)DataContext).
                    EnderecoSelected);
                viewLista.dataGrid.Items.Refresh();
                tabDetalheLista.IsSelected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

        void btSair_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                viewLista.dataGrid.Items.Refresh();
                tabDetalheLista.IsSelected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

    }
}
