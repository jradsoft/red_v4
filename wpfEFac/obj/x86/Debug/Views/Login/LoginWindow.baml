   M S B A M L   `   `   `  ����  wpfEFac%    wpfEFac.Views.Login.LoginWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Login��5   	   $	��237��6   $	��300��6$   $��14(�60    � WindowStyle$ 
ToolWindow=�5   	    � Icon$3 -/wpfEFac;component/Images/IconoEFacWindow.png��69    � Topmost . .��5      � -   5        $��
LayoutRoot��5      $	��200��6   $	��300��6-   $	��240��6<   $	��150��6K   ��5   
   �� 5       �� ImageSource$. (/wpfEFac;component/Images/Marquesina.jpg��6    � ColumnDefinitions 5
   
   �� 5      $
��100* �6   �� 5      $
��178* �6    � RowDefinitions 5   
   �� 5      $��53 �6   �� 5      $��53 �6   �� 5      $	��54* �6   �� 5      $	��38* �6   �� 5      $��2* �6   .+�5   
   �� -   5        $��
txbUsuario��5   `   $��	Usuario: ��6   $��1��6%   $
��Auto��66   $��Right=�6D   $��Center=�6r   
�������6�   �� 5   
   -   5        $��
txtUsuario��5   x   $��1��6   $��23��6"   $
��Left=�6.   $��Center=�6I   $��
10,16,0,14q�6d   $	��150��6�   $��1��6�    � ToolTip$ Ingrese su usuario��6�   6�   �� 5   
   -   5        $��txbContraseña��5   >   $
��Auto��6   $��Right=�6"   $��Contraseña: ��6S   $��Center=�6g   $��1��6�   
�������6�   G� 5   
   -   5        $��pwbContraseña��5   O   $��23��6   $
��Left=�6"   $��10,0,0,0q�6=   $��Center=�6d   $	��150��6   $��1��6�   $��1��6�   $��2��6�   $ Ingrese su contraseña��6�   �� 5   
   -   5        $��	bttCancel��5   k   $��Cancelar��6   $��1��6$   $��3��64   $
��Auto��6A   $��Right=�6O   $��Center=�6|   $��75��6�   $��0,1,0,11q�6�   $��5��6�   �� 5   
   -   5        $��bttOK��5   k   $��Ingresar��6   $��1��6$   $��3��64   $
��Auto��6A   $��Right=�6O   $��Center=�6x   $��75��6�   $��	0,1,81,11q�6�   $��4��6�    �� 	IsDefault . 6�   �� 5"   
   -	   5        $��
lblUsuario��5"   c   $�� ��6   $��28��6   $
��Left=�6'   $��	46,30,0,0q�6B   $��10(�6U   $	��Top=�6u   $��2��6�   $��2��6�   $	��214��6�   �� 5#   
   -
   5        $��lblPassword��5#   p   $�� ��6   $��1��6   $��28��6(   $
��Left=�64   $��	46,30,0,0q�6O   $��10(�6b   $	��Top=�6�   $��2��6�   $��2��6�   $	��214��6�   �� 5%   
   -   5        $��
txbEmpresa��5%   6   
�������$
��Auto��6'   $��	Empresa: ��6H   $��Center=�6Y   $��2��6t   $��Right=�6�   �� 5&   
   -   5        $��
cmbEmpresa��5&   Z   $��1��6   $��2��6#   $��23��60   $��Stretch=�6<   $��Center=�6l   $��3��6�   5'      5(      