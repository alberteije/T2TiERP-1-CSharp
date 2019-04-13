using System.Windows;
using System.Windows.Controls;
using CloseableTabItemDemo;

namespace ClientNFe.View.NFe
{
    /// <summary>
    /// Interaction logic for PontoPrincipal.xaml
    /// </summary>
    public partial class NFePrincipal : UserControl
    {
        public static TabControl TabPrincipal;
        public NFePrincipal()
        {
            InitializeComponent();
            TabPrincipal = TabControlPrincipal;
            TabPrincipal.AddHandler(CloseableTabItem.CloseTabEvent, new RoutedEventHandler(this.CloseTab));
        }

        private void CloseTab(object source, RoutedEventArgs args)
        {
            TabItem tabItem = args.Source as TabItem;
            if (tabItem != null)
            {
                TabControl tabControl = tabItem.Parent as TabControl;
                if (tabControl != null)
                    tabControl.Items.Remove(tabItem);
            }
        }

    }
}
