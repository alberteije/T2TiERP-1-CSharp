   M S B A M L   `   `   `  ����N  JCadastrosBaseClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:CadastrosBaseClient.UtilCadastrosBaseClient.Util  5      N JExportarParaArquivo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullfFclr-namespace:ExportarParaArquivo.Control;assembly=ExportarParaArquivoExportarParaArquivo.Control 5      5    /CadastrosBaseClient.View.CadastrosBase.CepLista   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7util&clr-namespace:CadastrosBaseClient.Util     5      VexpFclr-namespace:ExportarParaArquivo.Control;assembly=ExportarParaArquivo     5      .��5      ]� ��. .+�5   
   ]� ��. .+�5      �� $��Bottom=�6   $��Center=�61     �� Orientation$  
Horizontal=�6N   .+�5      �� $	��4,0q�6   $��<��6&   ��65   �� *paginaAnteriorCommand+�� 5      $	��4,0q�6   $��>��6'   ��66   �� *paginaSeguinteCommand+5      &   System.Windows.Controls.DataGrid  5      -   5        $��dataGrid��5         AutoGenerateColumns .     
IsReadOnly .    CanUserAddRows .     CanUserDeleteRows .  ��6�   �� *
listaCep+ �� SelectedItem 6�   �� *cepSelected+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$	 Cep��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 66   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6C   �� 
 �� Path$	
 Cep�6b     5      $ 
Logradouro��6)   $
 Auto 6=   	 6J   �� $
 
Logradouro�6p     5      $ Complemento��6)   $
 Auto 6>   	 6K   �� $
 Complemento�6r     5      $ Bairro��6)   $
 Auto 69   	 6F   �� $
 Bairro�6h     5      $ 
Município��6)   $
 Auto 6<   	 6I   �� $
 	Municipio�6n     5      $ Uf��6)   $
 Auto 65   	 6B   �� $
 Uf�6`     5      $ Código IBGE��6)   $
 Auto 6>   	 6K   �� $
 CodigoIbgeMunicipio�6z   5      5      5      5      