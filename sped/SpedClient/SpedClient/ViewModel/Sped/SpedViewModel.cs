using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using SpedClient.View.Sped;
using CloseableTabItemDemo;

namespace SpedClient.ViewModel.Sped
{
    public class SpedViewModel
    {

        public SpedViewModel()
        {
        }

        public void novaPagina(UserControl janela, String cabecalho)
        {
            Boolean achou = false;

            CloseableTabItem tabItem = new CloseableTabItem();
            tabItem.Header = cabecalho;
            tabItem.Content = janela;

            foreach (TabItem tab in SpedPrincipal.TabPrincipal.Items)
            {
                if (tab.Header == tabItem.Header)
                {
                    achou = true;
                    tab.Focus();
                }
            }

            if (!achou)
            {
                SpedPrincipal.TabPrincipal.Items.Add(tabItem);
                tabItem.Focus();
            }
        }
    }
}
