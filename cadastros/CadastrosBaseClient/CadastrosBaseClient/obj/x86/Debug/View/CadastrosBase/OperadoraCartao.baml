   M S B A M L   `   `   `  ����  CadastrosBaseClient<    6CadastrosBaseClient.View.CadastrosBase.OperadoraCartao   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      ]� ��. .+�5	   
   �� $��Bottom=�6   $��Right=�6-     �� Orientation$  
Horizontal=�6I   .+�5
      �� -   5        $��	btnSalvar��5
      $	��100��6&   .��5      �� $  
Horizontal=�6   $
��Left=�66   .+�5      �� $ ��/Imagem/botaoConfirmar.png��6   $��23��6@   6N   �� 5      $��Salvar��6    $��3,0,0,0q�6.   $��Center=�6?   5      5      �� 5      -   5        $��	btnVoltar��5      $	��100��6&   .��5      �� $  
Horizontal=�6   .+�5      �� $��/Imagem/21botaoSair.png��6   $��23��6=   6K   �� 5      $��Voltar��6    $��3,0,0,0q�6.   $��Center=�6?   5      5      5      � 5   
   .+�5      �� -   5        $��lblContabilConta��5   h   $��Contábil Conta:��6   $��28��6.   $
��Left=�6:   $��	40,26,0,0q�6U   $	��Top=�6�   �� 5      -   5        $��lblContaCaixa��5   e   $��Conta Caixa:��6   $��28��6+   $
��Left=�67   $��	57,55,0,0q�6R   $	��Top=�6z   �� 5      -   5        $��lblBandeira��5   b   $��	Bandeira:��6   $��28��6(   $
��Left=�64   $��	74,83,0,0q�6O   $	��Top=�6u   �� 5      -   5        $��lblNome��5   K   $��Nome:��6   $��28��6$   $
��Left=�60   $	��Top=�6Z   $��
89,113,0,0q�6r   �� 5      -   5        $��
lblTaxaAdm��5   c   $��	Taxa Adm:��6   $��28��6(   $
��Left=�64   $��
67,142,0,0q�6O   $	��Top=�6u   �� 5      -   5        $��lblTaxaAdmDebito��5   j   $��Taxa Adm Débito:��6   $��28��6/   $
��Left=�6;   $��
26,169,0,0q�6V   $	��Top=�6�   �� 5      -	   5        $��lblValorAluguelPosPin��5   h   $��Valor Aluguel:��6   $��28��6-   $
��Left=�69   $��
46,205,0,0q�6T   $	��Top=�6�   �� 5       -
   5        $��lblVencimentoAluguel��5    e   $��Vencimento:��6   $��28��6*   $
��Left=�66   $��
57,229,0,0q�6Q   $	��Top=�6�   �� 5!      -   5        $��lblFone1��5!   e   $��Telefone 1:��6   $��28��6*   $
��Left=�66   $��
63,258,0,0q�6Q   $	��Top=�6u   �� 5"      -   5        $��lblFone2��5"   e   $��Telefone 2:��6   $��28��6*   $
��Left=�66   $��
65,287,0,0q�6Q   $	��Top=�6u   �� 5#      -   5        $��txtIdContabilConta��5#   �   $��23��6O   $
��Left=�6[   $��
143,28,0,0q�6v   $	��Top=�6�   $	��120��6�   $��1��6�   ��6   �� *)'operadoracartaoSelected.IdContabilConta+6�   �� 5$      -   5        $��txtIdContaCaixa��5$   �   $��23��6L   $
��Left=�6X   $��
143,57,0,0q�6s   $	��Top=�6�   $	��120��6�   $��2��6�   ��6   �� *&$operadoracartaoSelected.IdContaCaixa+6�   �� 5%      -   5        $��txtBandeira��5%   �   $��23��6H   $
��Left=�6T   $��
143,86,0,0q�6o   $	��Top=�6�   $	��120��6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   ��6   �� *" operadoracartaoSelected.Bandeira+6�   �� 5&      -   5        $��txtNome��5&   �   $��23��6D   $
��Left=�6P   $��143,115,0,0q�6k   $	��Top=�6�   $	��451��6�   ��6   �� *operadoracartaoSelected.Nome+6�   �� 5'      -   5        $��
txtTaxaAdm��5'   �   $��23��6G   $
��Left=�6S   $��143,144,0,0q�6n   $	��Top=�6�   $	��120��6�   ��6   �� *!operadoracartaoSelected.TaxaAdm+6�   �� 5(      -   5        $��txtTaxaAdmDebito��5(   �   $��23��6M   $
��Left=�6Y   $��143,173,0,0q�6t   $	��Top=�6�   $	��120��6�   ��6   �� *'%operadoracartaoSelected.TaxaAdmDebito+6�   �� 5)      -   5        $��txtValorAluguelPosPin��5)   �   $��23��6R   $
��Left=�6^   $��143,202,0,0q�6y   $	��Top=�6�   $	��120��6�   ��6   �� *,*operadoracartaoSelected.ValorAluguelPosPin+6�   �� 5*      -   5        $��txtVencimentoAluguel��5*   �   $��23��6Q   $
��Left=�6]   $��143,231,0,0q�6x   $	��Top=�6�   $	��120��6�   ��6   �� *+)operadoracartaoSelected.VencimentoAluguel+6�   �� 5+      -   5        $��txtFone1��5+   �   $��23��6E   $
��Left=�6Q   $��143,260,0,0q�6l   $	��Top=�6�   $	��120��6�   ��6   �� *operadoracartaoSelected.Fone1+6�   �� 5,      -   5        $��txtFone2��5,   �   $��23��6E   $
��Left=�6Q   $��143,289,0,0q�6l   $	��Top=�6�   $	��120��6�   ��6   �� *operadoracartaoSelected.Fone2+6�   5-      5.      5/      