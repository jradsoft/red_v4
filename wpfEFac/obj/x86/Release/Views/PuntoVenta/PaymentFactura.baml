   M S B A M L   `   `   `  ����   wpfEFac-    'wpfEFac.Views.PuntoVenta.PaymentFactura   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$#  ADESOFT:: Complemento de pago��$
��1000��$	��600��.��� ����  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FF6693D1�� � Offset$ 1Q�� $ White��$ 0.098Q�� $ 	#FFEBF0F7��$ 0.607Q� � ColumnDefinitions �� $��* ��� $��* ��� $��* ��� $��* � � RowDefinitions �� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��35 ��� $��25 ��� $��35 ��� $��25 ��� $��25 ��� $	��38* �.+��� -   $��txbClientes��$��1��$��0��$��23��$
��Left=�$��
   Cliente��$��Center=��� -   $��cmbClientes��$��1��$��0��$��Right=�$��Center=�$	��158��
�������$��0,2,14,2q��� -   $��txbFolio��$��1��$��1��$��23��$
��Left=�$��
   Factura��$��Center=�$��18,1,0,1q�$��68���� -   $��cmbFacturas��$��1��$��1��$��Right=�$��Center=�$	��142��
�������$��0,3,14,1q��� -   $��btnAdd��$��6��$��3��$��Center=�$��
64,10,50,6q�$��19��.��]� .+��� $0��*/wpfEFac;component/Images/Icons/addCFD.ico���� $��Agregar Factura���� -    ��Name$ lblFecha��$
��Left=�$��1��$��2��$��70��Fecha Pago:(  "System.Windows.Controls.DatePicker  -   $��dtpFechaPago��$��1��$��2��$��	73,1,0,24q�$��2��$
��Left=�$	��120���� -   $��txthr��$��1��$��2��$��23��$��00��$��
199,1,26,1q��� -	   $��txtmin��$��1��$��2��$��23��$��00��$��	225,1,0,1q��� -
   $��txbMontoFactura��$��4��$��0��$��21��$
��Left=�$��   Monto Factura��$��Center=�$��Normal"�$��0,3,0,1q��� -   $��txtMontoFactura��$��4��$��0��$��23��$��Right=�$
��0.00��$	��158��$��0,6,14,6q�$
��Bold"�$��Arial)�$��14(� �� TextAlignment$ Center=����	#FFFC0D0D�� -   $��txbMonto��$��8��$��2��$��21��$
��Left=�$��   Gran Total:��$��Center=�$��Normal"�$��0,3,0,1q��� -   $��txtMonto��$��8��$��2��$��23��$��Right=�$
��0.00��$	��158��$��0,6,14,6q�$
��Bold"�$��Arial)�$��14(�$ Center=����	#FFFC0D0D�� -   $��txbSanterior��$��4��$��1��$��23��$
��Left=�$��   Saldo Anterior��$��Center=��� -   $��txtSanterior��$��4��$��1��$��23��$��Right=�$
��0.00��$	��142��$��0,6,14,6q�	 �� TextAlignment$	 Center=��� -   $��	txbPagado��$��4��$��2��$��23��$
��Left=�$��	   Pagado��$��Center=��� -   $��	txtPagado��$��4��$��2��$��23��$��Right=�$
��0.00��$	��142��$��0,6,14,6q�$	 Center=��� -   $��txbInsoluto��$��4��$��3��$��23��$
��Left=�$��   Saldo Insoluto��$��Center=��� -   $��
txtInsouto��$��4��$��3��$��23��$��Right=�$
��0.00��$	��142��$��0,6,14,6q�$	 Center=��� -   $��txbNuOperacion��$��6��$��0��$��23��$
��Left=�$��   # Operacion��$��Center=��� -   $��txtNuOperacion��$��6��$��0��$��23��$��Right=�$	 Center=�$��01��$	��142��$��0,6,14,6q��� -   $��txbParcialidad��$��6��$��1��$��23��$
��Left=�$��   Parcialidad��$��Center=��� -   $��txtParcialidad��$��6��$��1��$��23��$��Right=�$	��142��$��0,6,14,6q��� -   $ 	lblStatus��$��6��$��2��$y�	Collapsed=�$
��Left=�$	��100��$��
    Status��$��0,6,0,0q��� -   
 ��Name$
 cmbStausPago��$��6��$��2��$y�	Collapsed=�$��Right=� �� SelectedIndex$ 0��$	��168��$��0,6,14,6q�.@��� .���� PAGADA�� .���� PAGO PARCIAL�� $��6��$��2��$
��Left=�$��Center=�$��    Forma Pago:��$��86��$
��0,10q��� -   $
 cmbFormaPago��$��6��$��2��$��Center=�$ 0��$��	88,7,10,7q�.@��� .���� 01-EFECTIVO�� .���� 02-CHEQUE NOMINATIVO�� .���� (&03-TRANSFERENCIA ELECTRONICA DE FONDOS�� .���� 04-TARJETA DE CREDITO�� .���� 05-MONEDERO ELECTRONICO�� .���� 06-DINERO ELECTRONICO�� .���� 08-VALES DE DESPENSA�� .���� 12-DACION EN PAGO�� .���� 13-PAGO POR SUBROGACION�� .���� 14-PAGO POR CONSIGNACION�� .���� 15-CONDONACION�� .���� 17-COMPENSACION�� .���� 23-NOVACION�� .���� 24-CONFUSION�� .���� 25-REMISION DE DEUDA�� .���� 26-PREINSCRIPCION O CADUDIDAD�� .���� 27-A SATISFACCION DEL ACREDOR�� .���� 28-TARJETA DE DEBITO�� .���� 29-TARJETA DE SERVICIO�� .���� 30-APLICACION DE ANTICIPOS�� .���� 31-INTERMEDIARIOS PAGO�� .���� 99-POR DEFINIR�� -   $��
btnTimbrar��$y�	Collapsed=�$��8��$��Center=�$��Center=�$��	64,3,50,1q�$��7��$	��130��.��]� .+��� $4��./wpfEFac;component/Images/Icons/approveCFD.ico���� $��center=�Aprobar Pago�� -   $��btnImprimir��$y�	Collapsed=�$��8��$��1��$��Center=�$��Center=�$��	64,3,50,1q�$��7��$	��130��.��]� .+��� $2��,/wpfEFac;component/Images/Icons/printCFD.ico���� $��center=�
Imprimir�� -   $��btnEmail��$y�	Collapsed=�$��8��$��2��$��Center=�$��Center=�$��	64,3,50,1q�$��7��$	��130��.��]� .+��� $2��,/wpfEFac;component/Images/Icons/emailCFD.ico���� $��center=�Email�� -   $��
btnGuardar��$��8��$��3��$��Center=�$��Center=�$��	64,3,50,1q�$��7��$	��130��.��]� .+��� $.��(/wpfEFac;component/Images/Icons/save.ico���� $��center=�	Guardar�� $��8��$��0��$
��Left=�$��Center=�$�� Cta Ordenante:��$��86��$��0,0,0,5q�$��20���� -   $
 cmbRfcOrigen��$��8��$��Center=�$ 0��$��88,3,8,1q�$��21���� $��8��$��1��$
��Left=�$��Center=�$�� Cta Beneficiario:��$��86��$��0,0,0,5q�$��20���� -    $
 cmbRfcDestino��$��8��$��1��$��Center=�$ 0��$��89,3,8,1q�$��21��&   System.Windows.Controls.DataGrid  -!     Name$ dtgFacturasPago��$��9��$��4��$��Stretch=�$	��Top=�   AutoGenerateColumns .  ���� *MyData+   Columns 0  *System.Windows.Controls.DataGridTextColumn     
Foreground
 �   �,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    Header$ intId_factura��   
Visibility$ 	Collapsed=�   
IsReadOnly .  1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *intID+ �� Mode$ TwoWay=�  
 �   �$ * $ Serie��$ 	Collapsed=� .   �� *
strSerie+$ TwoWay=�  
 �   �$ * $ Folio�� .   �� *
strFolio+$ TwoWay=�  
 �   �$ * $
 UUID�� .   �� *	strUUID+$ TwoWay=�  
 �   �$ * $ Fecha�� .   �� *
dtmFecha+$ TwoWay=�  
 �   �$ * $ Importe factura�� .  �� *
dcmImporte+  
 �   �$ * $ Importe Pagado�� .  �� *	dcmPagado+  
 �   �$ * $ Saldo Pendiente�� .  �� *dcmPendiente+4  .System.Windows.Controls.DataGridTemplateColumn  $ Eliminar��-"   dtDeleteColumn��$ *    CellTemplate �� .R���  ��Name$ cmdEliminar�� �� Orientation$ 
Horizontal=�.+���  ��Name$ bttEliminarPrePago��-#   .��Borrar�� $y�	Collapsed=�$ 
Horizontal=�$��Right=�$��10��.+��� -$   $ 
cmdAceptar��$	��100��$��25��.��	Aceptar�� $y�	Collapsed=��� -%   $ cmdSalir��$	��100��$��25��.��
Cancelar