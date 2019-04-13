using System;
using System.Windows.Controls;
using CadastrosBaseClient.View.CadastrosBase;
using CloseableTabItemDemo;

namespace CadastrosBaseClient.ViewModel.CadastrosBase
{
    public class CadastrosBaseViewModel : ERPViewModelBase
    {
       public CadastrosBaseViewModel()
        {
        }

        public void novaPagina(UserControl janela, String cabecalho)
        {
            Boolean achou = false;

            CloseableTabItem tabItem = new CloseableTabItem();
            tabItem.Header = cabecalho;
            tabItem.Content = janela;

            foreach (TabItem tab in CadastrosBasePrincipal.TabPrincipal.Items)
            {
                if (tab.Header == tabItem.Header)
                {
                    achou = true;
                    tab.Focus();
                }
            }

            if (!achou)
            {
                CadastrosBasePrincipal.TabPrincipal.Items.Add(tabItem);
                tabItem.Focus();
            }
        }

    }
}
