   M S B A M L   `   `   `  ����B  >wpfEFac, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:wpfEFac.ModelswpfEFac.Models  5   �   0    *wpfEFac.Views.PuntoVenta.AddProductoWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   L   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6�   +myclr-namespace:wpfEFac.Models     6�   -   5          � Title$  Agregar Producto��5   	   $	��370��6"   $	��990��6/    � Icon$3 -/wpfEFac;component/Images/IconoEFacWindow.png��6   � 
ResizeMode$ NoResize=�6D   � ShowInTaskbar .   � Topmost .   � WindowStyle$ ThreeDBorderWindow=�6�   � WindowStartupLocation$ Manual=�6�   � Left$	 290��6�  
 � Top$ 0��6�  .��5      � ��5   
   �� 5      	 �� EndPoint$	 0.5,1&�6"   
 �� 
StartPoint$
 0.5,0&�63   .��5      �  � Color$ 	#FF6693D1��6    � Offset$ 1Q�61   � 5	      $ White��6   $ 0.098Q�6-   � 5
      $ 	#FFEBF0F7��6   $ 0.607Q�61   5       � ColumnDefinitions 5   
   �� 5      $
��497* �6    � RowDefinitions 5   
   �� 5      $	��30* �6   �� 5      $
��201* �6   �� 5      $	��75* �6   �� 5      $	��25* �6   �� 5      $	��25* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��Buscar por Codigo��6   63   �� 5      -   5        $��txtCodigoProducto��5   ?   $��23��6   $��Center=�6"   $��0,0,0,0q�6X   $	��120��6j   6v   �� 5      $��Buscar por Nombre��6   63   �� 5      -   5        $��txtNombreProducto��5   ?   $��23��6   $��Center=�6"   $��0,0,0,0q�6X   $	��300��6j   6v   5!      &   System.Windows.Controls.DataGrid  5#   
   -   5        $��dtgProductos��5#   �   $��1��6      AutoGenerateColumns .     
IsReadOnly .    EnableRowVirtualization . $��0��6m   $��Stretch=�6}      RowDetailsVisibilityMode$ VisibleWhenSelected=�6�   $��Stretch=�6�      Columns 5$      0  *System.Windows.Controls.DataGridTextColumn  5%      -   5        strIDColumn��,  &System.Windows.Controls.DataGridColumn   Header$ str ID��5%   Y      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6i      
Visibility$ 	Collapsed=�6s   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6:   ��  �� Path$ strID�6s     5&      -   5        strCategoriaNombre��$ str ID Categoria��5&   o   $ 	Collapsed=�6�    6A   �� $ Categorias.strNombre�6�     5'      -   5        strCodigoColumn��$ Codigo��5'   a   $ * 6q    6>   �� $ 	strCodigo�6q     5(      -   5        strNombreColumn��$ Nombre��5(   a   $ 2* 6q    6>   �� $ 	strNombre�6q     5)      -	   5        strDescripcionColumn��$ Descripcion��5)   k   $ * 6�    6C   �� $ strDescripcion�6�     5+      -
   5        dcmPrecioColumn1��$ Precio 1��5+   c   $ * 6u    6?   �� $ 
dcmPrecio1�6u     5,      -   5        dcmPrecioColumn2��$ Precio 2��5,   c   $ * 6u    6?   �� $ 
dcmPrecio2�6u     5-      -   5        dcmPrecioColumn3��$ Precio 3��5-   c   $ * 6u    6?   �� $ 
dcmPrecio3�6u     5.      -   5        dcmPrecioColumn4��$ Precio 4��5.   c   $ * 6u    6?   �� $ 
dcmPrecio4�6u     5/      -   5        dcmPrecioColumn5��$ Precio 5��5/   c   $ * 6u    6?   �� $ 
dcmPrecio5�6u     51      -   5        
dcmIva��$	 Iva��51   V   $ * 6c    65   �� $ porcIva�6c     52      -   5        	dcmretIva��$ Ret IVA��52   \   $ * 6m    68   �� $ 
porcRetIva�6m     53      -   5        	dcmretIsr��$ Ret ISR��53   \   $ * 6m    68   �� $ 
porcRetIsr�6m     54      -   5        
dcmretIeps��$ Ret IEPS��54   [   $ * 6m    69   �� $ porcIeps�6m   56      � 58   
   $��2��6   .+�5:      �� $ 
Horizontal=�6   .+�5;      �� -   5        $��txbCantidad��5;   D   $��23��6   $��Right=�6(   $��
Cantidad: ��6W   �� 5<      -   5         ��Name$ txtCantidad��5<      $��23��6+   $��40��67   6k   �� 5>      $��23��6   $��
  Nombre: ��6)   �� 5?      -   5        $ 	txtNombre��5?      $��75��6-   $	��360��69    �� AcceptsReturn .  �� TextWrapping$
 Wrap=�6Z   �� 5D      $��23��6   $�� Precio Unit: ��6&   �� 5E      -   5        $ txtPrecioUnitario��5E      $��23��61   $��70��6=   6x   �� 5H      $��Center=�6   $�� DESC.��68   �� 5I      -   5        $ txtDescuento��5I      $��23��60   $��50��6c   6p   �� 5J      $��Center=�6   %6;   �� 5L      $��Center=�6   $��  Imp. DESC.��68   �� 5M      -   5        $ txtImporteDescuento��5M      $��23��67   $��50��6j   6w   �� 5P      $��  Importe: ��6   $��25��6/   �� 5Q      -   5        $ 
txtImporte��5Q      $	��100��6*    .  $��25��6L   6Z   5S      5T      �� 5V   
   $��3��6   $ 
Horizontal=�6"   .+�5W      �� -   5        $��	txbUnidad��5W   $   $��23��6   $��	 Unidad :��66   �� 5X      -   5        $ 	txtUnidad��5X      $��25��6*   $��50��66   $
 Wrap=�6A   �� 5Z      -   5        $��txbIVA��5Z   $   $��23��6   $��
     IVA :��62   �� 5[      -   5        $ txtIVA��5[      $��25��6&   $��50��62   $
 Wrap=�6=   �� 5]      -   5        $��	txbRetIVA��5]   $   $��23��6   $��     RET IVA :��66   �� 5^      -   5        $ 	txtRetIVA��5^      $��25��6)   $��50��65   $
 Wrap=�6@   �� 5`      -    5        $��	txbRetISR��5`   $   $��23��6   $��     RET ISR :��66   �� 5a      -!   5        $ 	txtRetISR��5a      $��25��6)   $��50��65   $
 Wrap=�6@   �� 5c      -"   5        $��
txbRetIEPS��5c   $   $��23��6   $��     RET IEPS :��66   �� 5d      -#   5        $ 
txtRetIEPS��5d      $��25��6*   $��50��66   $
 Wrap=�6A   �� 5e      -$   5         ��Name$ txbPeso��5e   $   $��23��6   $��     PESO KG :��65   �� 5f      -%   5        $ 	txtPesoKg��5f      $��25��6)   $	��0.0��65   $��50��6@   $
 Wrap=�6K   �� 5g      -&   5        $ txbidOrigen��5g      $��23��6-   $��  idOrigen : ��6:   �� 5h      -'   5        $ txtIdOrigen��5h      $��25��6+   $��OR000001��67   $��50��6G   $
 Wrap=�6R   �� 5i      -(   5        $ txbIdDestino��5i      $��23��6.   $��  idDestino : ��6;   �� 5j      -)   5        $ txtIdDestino��5j      $��25��6,   $��DE000001��68   $��50��6H   $
 Wrap=�6S   �� 5l      -*   5        $��txbMP��5l   5   $��Center=�6   $	��Top=�6B   $��Material Peligroso:��6Z   $��75��6u   �� 5m      -+   5         ��Name$ cmbMaterialPeligroso��5m   2   $��Center=�6   .@�5n      �� .��5o      �� No6$   5p      �� 5q      .��5r      �� Si6$   5s      5t      �� 5v      -,   5        $ 
txbClaveMP��5v      $��23��6,   $��Clave Mat Pel: ��69   �� 5w      --   5        $ 
txtClaveMP��5w      $��25��6*   $�� ��66   $��50��6>   $
 Wrap=�6I   �� 5y      -.   5        $ txbEmbalaje��5y      $��23��6-   $��
Embalaje: ��6:   �� 5z      -/   5        $ txtEmbalaje��5z      $��25��6+   $�� ��67   $��50��6?   $
 Wrap=�6J   5{      �� 5}   
   $��6��6   $��2��6"   $ 
Horizontal=�66   $��Center=�6O   $��Center=�6j   .+�5~      �� -0   5        $��btnAñadirContinuar��5~   y   $��Añadir y Continuar��6   $��6��62   $��23��6?   $
��Left=�6K   $��	20,0,20,0q�6f   $	��Top=�6�   $
��Auto��6�   6�   �� 5      -1   5        $��btnAñadirSalir��5   u   $��Añadir y Salir��6   $��6��6.   $��23��6;   $
��Left=�6G   $��	20,0,20,0q�6b   $	��Top=�6�   $
��Auto��6�   6�   �� 5�      -2   5        $��	btnCerrar��5�   m   $��Cerrar��6   $��6��6&   $��23��63   $
��Left=�6?   $��	20,0,20,0q�6Z   $	��Top=�6~   $
��Auto��6�   6�   	� 5�      -3   5        $��rdbPesos��5�   o   $��Pesos��6   $��1��6*   $��5��6:   $��16��6G   $��Right=�6S   $��Center=�6    d� 	IsChecked$
 TrueY�6�   $��0,0,70,0q�6�     	� 	GroupName$  Divisa��6�   	� 5�      -4   5        $��
rdbDolares��5�   q   $��Dolares��6   $��1��6,   $��5��6<   $��16��6I   $��Right=�6U   $��Center=�6�   $��0,0,70,0q�6�   $  Divisa��6�   {�.  6�   �� 5�      -5   5        $��chkMercancia��5�   p   $��	Mercancia��6   $��1��6+   $��5��6;   $��16��6H   $��Right=�6T   $��Center=�6�   �� 5�      -6   5        $��	chkSinIva��5�   n   $��Sin IVA��6   $��1��6)   $��5��69   $��16��6F   $��Right=�6R   $��Center=�6�   5�      5�      5�      