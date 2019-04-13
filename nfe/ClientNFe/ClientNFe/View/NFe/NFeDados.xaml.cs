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

namespace ClientNFe.View.NFe
{
    /// <summary>
    /// Interaction logic for NFeDados.xaml
    /// </summary>
    public partial class NFeDados : UserControl
    {
        public NFeDados()
        {
            try
            {
                InitializeComponent();
                tabDadosBasicos.Content = new NFeDadosBasicos();
                tabDestinatario.Content = new NFeDestinatario();
                tabCupomVinculado.Content = new NFeCupomVinculado();
                tabEntregaRetirada.Content = new NFeEntregaRetirada();
                tabTransporte.Content = new NFeTransporte();
                tabCobranca.Content = new NFeFatura();
                tabProdutos.Content = new NFeProduto();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");                
            }
        }

        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((NFeViewModel)this.DataContext).carregarTabLista();
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
                ((NFeViewModel)this.DataContext).salvarNFe();
                MessageBox.Show("Operação efetuada com sucesso.", "Mensagem do sistema");
                ((NFeViewModel)this.DataContext).carregarTabLista();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

        private void btEnviar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NFeEnvio viewEnvio = new NFeEnvio(((NFeViewModel)this.DataContext));
                viewEnvio.envioNFe();
                viewEnvio.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

        private void btImprimirDanfe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((NFeViewModel)this.DataContext).imprimirDANFE();
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
                bool resultadoOK = false;
                object retorno;
                ((NFeViewModel)this.DataContext).ativarServicosNFE();
                resultadoOK = ((NFeViewModel)this.DataContext).consultarStatusNFe(out retorno);
                ((NFeViewModel)this.DataContext).desativarServicosNFE();

                MessageBox.Show(retorno.ToString(), "Consulta Web Service - SEFAZ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }

        }

    }
}
