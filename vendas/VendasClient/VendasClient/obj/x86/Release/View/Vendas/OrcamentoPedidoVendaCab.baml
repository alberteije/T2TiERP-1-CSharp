   M S B A M L   `   `   `  ���� G  CVendasClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:VendasClient.View.VendasVendasClient.View.Vendas  6    0VendasClient.View.Vendas.OrcamentoPedidoVendaCab   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     7view&clr-namespace:VendasClient.View.Vendas     .��]� ��. .+��� $��Bottom=�$��Right=�  �� Orientation$  
Horizontal=�.+��� -   $��btSalvar��$	��100��.���� $  
Horizontal=�$
��Left=�.+��� $ ��/Imagem/botaoConfirmar.png��$��23���� $��Salvar��$��3,0,0,0q�$��Center=��� -   $��btSair��$	��100��.���� $  
Horizontal=�.+��� $��/Imagem/21botaoSair.png��$��23���� $��Voltar��$��3,0,0,0q�$��Center=�]� ��. .+�� $	��Top=�$	��218��.+��� $
��Left=�$	��Top=�$��23��$��24��$��6,25,0,0q� �� 
IsReadOnly . {�.  ���� *$"OrcamentoPedidoVendaCabSelected.Id+�� $��Id��$
��Left=�$	��Top=�$��6,6,0,0q��� $
��Left=�$	��Top=� . $��23��$	��126��$��	36,25,0,0q����� *B@OrcamentoPedidoVendaCabSelected.Vendedor.Colaborador.Pessoa.Nome+�� -   $��btPesquisarVendedor��$
��Left=�$	��Top=�$��23��$	��...��$��30��$��
168,24,0,0q��� $��Vendedor��$
��Left=�$	��Top=�$��36,6,0,0q��� $
��Left=�$	��Top=� . $��23��$	��170��$��
430,25,0,0q����� *<:OrcamentoPedidoVendaCabSelected.Transportadora.Pessoa.Nome+�� -   $��btPesquisarTransportadora��$
��Left=�$	��Top=�$��23��$	��...��$��30��$��
606,24,0,0q��� $��Transportadora��$
��Left=�$	��Top=�$��	430,7,0,0q��� $
��Left=�$	��Top=� . $��23��$	��184��$��
204,25,0,0q����� *53OrcamentoPedidoVendaCabSelected.Cliente.Pessoa.Nome+�� -   $��btPesquisarCliente��$
��Left=�$	��Top=�$��23��$	��...��$��30��$��
394,24,0,0q��� $��Cliente��$
��Left=�$	��Top=�$��	204,7,0,0q��� $
��Left=�$	��Top=� . $��23��$	��100��$��500,169,0,0q����� *75OrcamentoPedidoVendaCabSelected.CondicoesPagamento.Id+�� -   $#��btPesquisarCondicoesPagamento��$
��Left=�$	��Top=�$��23��$	��...��$��30��$��606,168,0,0q��� $��Condições Pag.��$
��Left=�$	��Top=�$��500,150,0,0q��� $
��Left=�$	��Top=�$��23��$	��100�� �� SelectedValuePath$	 Tag��$��394,169,0,0q� �� SelectedValue �� *&$OrcamentoPedidoVendaCabSelected.Tipo+.@��� $��
Orçamento��
 � Tag$ O���� $��Pedido��$ P���� $
��Tipo��$
��Left=�$	��Top=�$��394,150,0,0q��� $
��Left=�$	��Top=�$��23��$	��100��$��6,73,0,0q����� *(&OrcamentoPedidoVendaCabSelected.Codigo+�� $��Codigo��$
��Left=�$	��Top=�$��6,54,0,0q�(  "System.Windows.Controls.DatePicker  $
��Left=�$	��Top=�$��23��$	��100��$��
112,73,0,0q�   SelectedDate �� *.,OrcamentoPedidoVendaCabSelected.DataCadastro+�� $��Data Cadastro��$
��Left=�$	��Top=�$��
112,54,0,0q�  $
��Left=�$	��Top=�$��23��$	��100��$��
218,73,0,0q� �� *-+OrcamentoPedidoVendaCabSelected.DataEntrega+�� $��Data Entrega��$
��Left=�$	��Top=�$��
218,54,0,0q�  $
��Left=�$	��Top=�$��23��$	��100��$��
324,73,0,0q� �� **(OrcamentoPedidoVendaCabSelected.Validade+�� $��Validade��$
��Left=�$	��Top=�$��
324,54,0,0q��� $
��Left=�$	��Top=�$��23��$	��100��$	 Tag��$��
430,73,0,0q� �� *+)OrcamentoPedidoVendaCabSelected.TipoFrete+.@��� $	��CIF��$ C���� $	��FOB��$ F���� $��
Tipo Frete��$
��Left=�$	��Top=�$��
430,54,0,0q��� $
��Left=�$	��Top=�$��23��$	��100��$��
536,73,0,0q����� */-OrcamentoPedidoVendaCabSelected.ValorSubtotal+�� $��Valor Subtotal��$
��Left=�$	��Top=�$��
536,54,0,0q��� $
��Left=�$	��Top=�$��23��$	��100��$��	6,121,0,0q����� *,*OrcamentoPedidoVendaCabSelected.ValorFrete+�� $��Valor Frete��$
��Left=�$	��Top=�$��	6,102,0,0q��� $
��Left=�$	��Top=�$��23��$	��100��$��112,121,0,0q����� *.,OrcamentoPedidoVendaCabSelected.TaxaComissao+�� $��Taxa Comissao��$
��Left=�$	��Top=�$��112,102,0,0q��� $
��Left=�$	��Top=�$��23��$	��100��$��218,121,0,0q����� */-OrcamentoPedidoVendaCabSelected.ValorComissao+�� $��Valor Comissao��$
��Left=�$	��Top=�$��218,102,0,0q��� $
��Left=�$	��Top=�$��23��$	��100��$��324,121,0,0q����� *.,OrcamentoPedidoVendaCabSelected.TaxaDesconto+�� $��Taxa Desconto��$
��Left=�$	��Top=�$��324,102,0,0q��� $
��Left=�$	��Top=�$��23��$	��100��$��430,121,0,0q����� */-OrcamentoPedidoVendaCabSelected.ValorDesconto+�� $��Valor Desconto��$
��Left=�$	��Top=�$��430,102,0,0q��� $
��Left=�$	��Top=�$��23��$	��100��$��536,121,0,0q����� *,*OrcamentoPedidoVendaCabSelected.ValorTotal+�� $��Valor Total��$
��Left=�$	��Top=�$��536,102,0,0q��� $
��Left=�$	��Top=�$��23��$	��276��$��	6,169,0,0q����� *,*OrcamentoPedidoVendaCabSelected.Observacao+�� $��Observação��$
��Left=�$	��Top=�$��	6,150,0,0q��� $
��Left=�$	��Top=�$��23��$	��100��$	 Tag��$��288,169,0,0q� �� *.,OrcamentoPedidoVendaCabSelected.StatusPedido+.@��� $��Digitação��$ 1���� $��
Produção��$ 2���� $��Expedição��$ 3���� $��Faturado��$ 4���� $��Entregue��$ 5���� $��Status Pedido��$
��Left=�$	��Top=�$��288,150,0,0q�?   9VendasClient.View.Vendas.OrcamentoPedidoVendaDetPrincipal  