   M S B A M L   `   `   `  ���� N  JExportarParaArquivo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullfFclr-namespace:ExportarParaArquivo.Control;assembly=ExportarParaArquivoExportarParaArquivo.Control  V>clr-namespace:ExportarParaArquivo;assembly=ExportarParaArquivoExportarParaArquivo  D @ClientNFe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ClientNFe.View.NFeClientNFe.View.NFe 7 clr-namespace:ClientNFe.View.NFeClientNFe.View.NFe /clr-namespace:ClientNFe.UtilClientNFe.Util O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem     ClientNFe.View.NFe.NFeView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     VexpFclr-namespace:ExportarParaArquivo.Control;assembly=ExportarParaArquivo     Qexpfmt>clr-namespace:ExportarParaArquivo;assembly=ExportarParaArquivo     2local clr-namespace:ClientNFe.View.NFe     1view clr-namespace:ClientNFe.View.NFe     -utilclr-namespace:ClientNFe.Util     6sys&clr-namespace:System;assembly=mscorlib       � 	Resources     ClientNFe.Util.MoedaFormat   moedaFormat  
formatoExp S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters M� TypeName��%Q   &	        &	        S� $ 	GetValues��"   M� $ expfmt:Formato��.���� .@��� -   $��tabLista�� �� 
IsSelected �� *isSelectedTabLista+����  �� Orientation$ 
Horizontal=�.+��� $��/Imagem/nfe32.png��$��23���� $
��NF-e��$��3,0,0,0q�$��Center=�.��]� ��. .+��� $ 
Horizontal=�$	��Top=�$��Right=�.+��� -   $��comboExp�� �� SelectedIndex$ 0��$��60������  �� Source# [ .   (ExportarParaArquivo.Control.ButtonExport  -   	   ExportDataGridSource	 �� 
 �� ElementName$
 dataGrid��   ExportFileFormat �� $
 comboExp�� �� Path$ SelectedValue�.���� $ 
Horizontal=�.+��� $��/Imagem/documento32.png��$��20���� $��Bottom=�$��Right=�$ 
Horizontal=�$��0,5,0,0q�.+��� -   $��btNovo��$	��100��.���� $ 
Horizontal=�$
��Left=�.+��� $ ��/Imagem/botaoConfirmar.png��$��23���� $
��Novo��$��3,0,0,0q�$��Center=��� -   $��btConsultar��$	��100��.���� $ 
Horizontal=�.+��� $!��/Imagem/21botaoPesquisa.png��$��23���� $��	Consultar��$��3,0,0,0q�$��Center=�&   System.Windows.Controls.DataGrid  -   $��dataGrid��   AutoGenerateColumns .     
IsReadOnly .    CanUserAddRows .     CanUserDeleteRows .  ���� *
listaNFe+ �� SelectedItem �� *nfeSelected+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Série��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� $ serie�   	CellStyle �� .���  �� Property � �� �� Value$ Center=�  $ Número��$
 Auto  �� $ numero�  $
 Data��$
 Auto  �� $ dataEmissao� �� StringFormat$ 
dd/MM/yyyy��  $ Pedido��$
 Auto  �� $ pedido�  $ Valor��$
 Auto  �� $ 
valorTotal� �� 	Converter# [   �� .���  � ��$ Right=��� -   $��tabDados��$y�	Collapsed=� �� *isSelectedTabDados+.���� ���� *contentPresenterTabDados+