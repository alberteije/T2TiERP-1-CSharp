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
using System.Diagnostics;
using ContabilidadeClient.View;
using reportman;
using ContabilidadeClient.ViewModel;

namespace ContabilidadeClient.View.Contabilidade
{
    /// <summary>
    /// Interaction logic for ContabilDfcPrincipal.xaml
    /// </summary>
    public partial class ContabilDfcPrincipal : UserControl
    {
        public ContabilDfcPrincipal()
        {
            InitializeComponent();
        }

        private void btGerar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string NomeArquivo = "DFC.rep";
                //
                string parTITULORELATORIO = "Relatório - DFC";
                string parTITULOSOFTHOUSE = "T2Ti.COM";
                string parTITULORODAPE = "T2Ti Tecnologia da Informação Ltda. - (61)3042.5277";
                //
                string ConsultaSQL = "";
                //
                string ConteudoServidor = NomeArquivo + "|" + ConsultaSQL + "|" + parTITULORELATORIO + "|" + parTITULOSOFTHOUSE + "|" + parTITULORODAPE;

                ReportManX rp = new ReportManX();

                rp.GetRemoteParams("localhost", 3060, "Admin", "", "T2Ti", NomeArquivo);
                rp.SetParamValue("PAR_PERIODO", textBox1.Text);
                rp.ExecuteRemote("localhost", 3060, "Admin", "", "T2Ti", ConteudoServidor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            ERPClient.JanelaDfc.Close();
        }

        
    }
}
