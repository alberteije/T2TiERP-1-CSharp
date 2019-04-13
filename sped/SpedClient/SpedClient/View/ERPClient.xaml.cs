using System.Windows;
using System.Windows.Controls;
using SpedClient.View.Sped;
using Microsoft.Windows.Controls.Ribbon;

namespace SpedClient.View
{
    /// <summary>
    /// Interaction logic for ERPClient.xaml
    /// </summary>
    public partial class ERPClient : RibbonWindow
    {
        public static Window JanelaSpedContabil;
        public static Window JanelaSpedFiscal;
        public static Window JanelaLogin;


        public ERPClient()
        {
            InitializeComponent();
            dockModulo.Children.Clear();
            dockModulo.Children.Add(new SpedPrincipal());
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

        private void BotaoContabil_Click(object sender, RoutedEventArgs e)
        {
            SpedContabilPrincipal janela = new SpedContabilPrincipal();
            Window window = new Window()
            {
                Title = "Sped Contábil",
                ShowInTaskbar = false,
                Topmost = false,
                ResizeMode = ResizeMode.NoResize,
                Width = 510,
                Height = 340,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen
            };
            window.Content = janela;
            JanelaSpedContabil = window;
            window.ShowDialog();
        }

        private void BotaoFiscal_Click(object sender, RoutedEventArgs e)
        {
            SpedFiscalPrincipal janela = new SpedFiscalPrincipal();
            Window window = new Window()
            {
                Title = "Sped Fiscal",
                ShowInTaskbar = false,
                Topmost = false,
                ResizeMode = ResizeMode.NoResize,
                Width = 510,
                Height = 390,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen
            };
            window.Content = janela;
            JanelaSpedFiscal = window;
            window.ShowDialog();
        }


    }
}
