   M S B A M L   `   `   `  ����B  >wpfEFac, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:wpfEFac.ModelswpfEFac.Models  5   	   /    )wpfEFac.Views.General.BuscarClienteWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +myclr-namespace:wpfEFac.Models     5   	   -   5          � Title$  Buscar Cliente��5   	   $	��500��6    $	��750��6-   .��5      � -   5         �Name$ 
LayoutRoot��5       � RowDefinitions 5	   
   �� 5
      $
��120* �6   �� 5      $
��300* �6   �� 5      $	��50* �6   .+�5   
   �� -   5        $��grbFiltrosBusqueda��5   `   $��1��6   $��Filtros de Busqueda��6$   $��Stretch=�6B   $��Stretch=�6z   .��5      �  � ColumnDefinitions 5      �� 5      $
��100* �6'   �� 5      $
��100* �6'   �� 5      $
��100* �6'   �� 5      $
��100* �6'    5      �� 5      $	��23* �6$   �� 5      $	��23* �6$   .+�5#      ��  �� Orientation$ 
Horizontal=�6   $��Center=�66   $��Center=�6S   .+�5$      �� $0��*/wpfEFac;component/Images/Icons/search.ico��6   $��25��6P   6^   �� 5%      
Buscar RFC5'      5(      �� 5*      -   5        $��txtRFC��5*   3   $��1��6   $��25��6'   $	��100��6A   6M   �� 5,      $ 
Horizontal=�6   $��Center=�66   $��Center=�6S   $��1��6n   .+�5-      �� $0��*/wpfEFac;component/Images/Icons/search.ico��6   $��25��6P   6^   �� 5.      Buscar Razon Social50      51      �� 53      -   5        $��txtRazonSocial��53   O   $��1��6   $��25��6'   $	��100��63   $��1��6?   6e   �� 55      $ 
Horizontal=�6   $��Center=�66   $��Center=�6S   $��2��6n   .+�56      �� $0��*/wpfEFac;component/Images/Icons/search.ico��6   $��25��6P   6^   �� 57      Buscar Nombre Comercial59      5:      �� 5=      -   5        $��txtNombreComercial��5=   O   $��1��6   $��25��6'   $	��100��63   $��2��6?   6i   �� 5?      $ 
Horizontal=�6   $��Center=�66   $��Center=�6S   $��3��6n   .+�5@      �� $.��(/wpfEFac;component/Images/CursorIcon.png��6   $��40��6N   $��40��6Z   6g   �� 5A      $
��Wrap=�6    $��70��64   !Seleccione Tipo de Inscripción5C      5D      �� 5F      -   5        $��cmbTipoInscripcion��5F   D   $��3��6   $��1��6+   $��25��68   6^   5H      5I      &   System.Windows.Controls.DataGrid  5K   
   -   5        $��dtgClientes��5K   l      
IsReadOnly .    AutoGenerateColumns .  $��1��6A   $��Stretch=�6N   $��Stretch=�6   $��1��6�      Columns 5L      0  *System.Windows.Controls.DataGridTextColumn  5M      -	   5        strIDColumn��,  &System.Windows.Controls.DataGridColumn   Header$ ID��5M   Y   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 SizeToHeader 6e   
   
Visibility$
 Hidden=�6z   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6:   ��  �� Path$ strID�6z     5N      -
   5        strRFCColumn��$	 RFC��5N   [   $	 * 6h    6;   �� $ strRFC�6h     5O      -   5        strRazonSocialColumn��$ Razon Social��5O   k   $	 * 6�    6C   �� $ strRazonSocial�6�     5P      -   5        strNombreComercialColumn��$ Nombre Comercial��5P   s   $	 * 6�    6G   �� $ strNombreComercial�6�     5Q      -   5        strGiroColumn��$
 Giro��5Q   ]   $	 * 6k    6<   �� $ strGiro�6k     5R      -   5        strTipodeInscripcionColumn��$ Tipode Inscripcion��5R   w   $	 * 6�    6I   �� $ strTipodeInscripcion�6�     5S      -   5        strContactoColumn��$ Contacto��5S   e   $	 * 6w    6@   �� $ strContacto�6w   5U      �� 5W   
   -   5        $��btnSeleccionar��5W   ^   $��Seleccionar��6   $��2��6'   $
��Auto��64   $��Right=�6B   $��Center=�6t   $��75��6�   $��0,0,82,0q�6�   6�   �� 5X   
   -   5        $��btnCancelar��5X   Z   $��Cancelar��6   $��2��6$   $��23��61   $��Right=�6=   $��Center=�6m   $��75��6�   6�   5Y      5Z      