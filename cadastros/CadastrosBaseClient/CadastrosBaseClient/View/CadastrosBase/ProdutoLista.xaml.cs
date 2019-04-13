using System;
using System.Windows;
using System.Windows.Controls;
using CadastrosBaseClient.ViewModel;
using ExportarParaArquivo.Control;
using CadastrosBaseClient.ViewModel.CadastrosBase;

namespace CadastrosBaseClient.View.CadastrosBase
{
    /// <summary>
    /// Interaction logic for Produto.xaml
	/// 
	/// The MIT License
	///
	/// Copyright: Copyright (C) 2010 T2Ti.COM
	///
	/// Permission is hereby granted, free of charge, to any person
	/// obtaining a copy of this software and associated documentation
	/// files (the "Software"), to deal in the Software without
	/// restriction, including without limitation the rights to use,
	/// copy, modify, merge, publish, distribute, sublicense, and/or sell
	/// copies of the Software, and to permit persons to whom the
	/// Software is furnished to do so, subject to the following
	/// conditions:
	///
	/// The above copyright notice and this permission notice shall be
	/// included in all copies or substantial portions of the Software.
	///
	/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
	/// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
	/// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
	/// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
	/// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
	/// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
	/// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
	/// OTHER DEALINGS IN THE SOFTWARE.
	///
	///        The author may be contacted at:
	///            t2ti.com@gmail.com
	///
	/// Autor: Albert Eije (t2ti.com@gmail.com)
	/// Version: 1.0
    /// </summary>
    public partial class ProdutoLista : UserControl
    {
        public ProdutoLista()
        {
            InitializeComponent();
        }

        private void ButtonExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ERPViewModelBase)DataContext).exportarDataGrid((ExportarParaArquivo.Formato)(
            (ButtonExport)sender).ExportFileFormat,
            (DataGrid)((ButtonExport)sender).ExportDataGridSource);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta do sistema");
            }
        }

        private void btRelatorio_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.SelectedItem = dataGrid.Items[0];
            int offset = ((ProdutoViewModel)DataContext).ProdutoSelected.Id - 1;

            string ConsultaSQL = 
                                "select p.* ,a.nome as almoxarifado, tgt.descricao as grupotributario, mp.nome as marca, gp.nome as grupoproduto,  sgp.nome as subgrupoproduto,  up.sigla as unidade "+
                                "from produto p "+
                                "   left join almoxarifado a on (a.id = p.id_almoxarifado) "+
                                "   left join tribut_grupo_tributario tgt on (tgt.id = p.id_grupo_tributario) "+
                                "   left join produto_marca mp on (mp.id = p.id_marca_produto) "+
                                "   inner join produto_sub_grupo sgp on (sgp.id = p.id_sub_grupo) "+
                                "   inner join produto_grupo gp on (gp.id = sgp.id_grupo) "+
                                "   inner join unidade_produto up on (up.id = p.id_unidade_produto)  "+
                                "order by p.nome limit " + ERPViewModelBase.QUANTIDADE_PAGINA + " offset " + offset;

            ((ERPViewModelBase)DataContext).exibirRelatorio("Produto", "Produtos", ConsultaSQL);

        }
    }
}
