   M S B A M L   `   `   `  ����  CadastrosBaseClient5    /CadastrosBaseClient.View.CadastrosBase.CboLista   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      ]� ��. .+�5	   
   ]� ��. .+�5
      �� $��Bottom=�6   $��Center=�61     �� Orientation$  
Horizontal=�6N   .+�5      �� $	��4,0q�6   $��<��6&   ��65   �� *paginaAnteriorCommand+�� 5      $	��4,0q�6   $��>��6'   ��66   �� *paginaSeguinteCommand+5      &   System.Windows.Controls.DataGrid  5      -   5        $��dataGrid��5         AutoGenerateColumns .     
IsReadOnly .    CanUserAddRows .     CanUserDeleteRows .  ��5      �� *
listaCbo+ �� SelectedItem 5      �� *cboSelected+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 Cód��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 66   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 5   =   �� 
 �� Path$
 codigo�5        5      $
 Nome��6)   $
 Auto 67   	 5   =   �� $

 nome�5        5      $ Observação��6)   $
 Auto 6=   	 5   =   �� $
 
observacao�5      5      5"      5$      5%      