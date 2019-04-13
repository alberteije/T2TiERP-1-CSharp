using System.Windows;
using System.Windows.Controls;
using VendasClient.View.Vendas;
using Microsoft.Windows.Controls.Ribbon;
using VendasClient.ViewModel.Vendas;

namespace VendasClient.View
{
    /// <summary>
    /// Interaction logic for ERPClient.xaml
    /// </summary>
    public partial class ERPClient : RibbonWindow
    {
        public static Window JanelaRegistroVendas;

        VendasViewModel viewModel = new VendasViewModel();
        public static Window JanelaLogin;

        public ERPClient()
        {
            InitializeComponent();
            dockModulo.Children.Clear();
            dockModulo.Children.Add(new VendasPrincipal());
            doLogin();
        }

        private void doLogin()
        {
            Login janela = new Login();
            Window window = new Window()
            {
                Title = "Login",
                ShowInTaskbar = false,
                Topmost = false,
                ResizeMode = ResizeMode.NoResize,
                Width = 525,
                Height = 222,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen
            };
            window.Content = janela;
            JanelaLogin = window;
            window.ShowDialog();

            if (Login.logado == false)
            {
                MessageBox.Show("Aplicação será Encerrada.", "Informação do Sistema", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }

        private void MenuItem1_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Tem Certeza Que Deseja Sair do Sistema?", "Sair do Sistema", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }

        private void BotaoTipoNf_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new TipoNotaFiscalPrincipal(), "Tipo de Nota Fiscal");
        }

        private void BotaoCondicoesPagamento_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new CondicoesPagamentoPrincipal(), "Condições de Pagamento");
        }

        private void BotaoOrcamento_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new OrcamentoPedidoVendaCabPrincipal(), "Orçamento de Venda");
        }

        private void BotaoVenda_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new VendaCabecalhoPrincipal(), "Venda");
        }

        private void BotaoFrete_Click(object sender, RoutedEventArgs e)
        {
            viewModel.novaPagina(new FreteVendaPrincipal(), "Frete");
        }

    }
}
