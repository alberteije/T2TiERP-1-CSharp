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
using SpedClient.ViewModel.Sped;
using System.Diagnostics;
using System.IO;

namespace SpedClient.View.Sped
{
    /// <summary>
    /// Interaction logic for SpedPrincipal.xaml
    /// </summary>
    public partial class PreviewPrincipal : UserControl
    {
        public PreviewPrincipal()
        {
            InitializeComponent();
            CarregaArquivo();
        }

        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            SpedFiscalPrincipal.JanelaPreview.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void CarregaArquivo()
        {
            string fileName = SpedFiscalPrincipal.CaminhoArquivo;
            TextRange range;
            FileStream fStream;
            if (File.Exists(fileName))
            {
                range = new TextRange(richTextBoxArquivo.Document.ContentStart, richTextBoxArquivo.Document.ContentEnd);
                fStream = new FileStream(fileName, FileMode.OpenOrCreate);
                range.Load(fStream, DataFormats.Text);
                fStream.Close();
            }
        }

    }
}
