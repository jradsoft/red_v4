   M S B A M L   `   `   `  ����   wpfEFac/    )wpfEFac.Views.ImportarDatos.ImportarDatos   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       N� Title$	  CFD��.��� -   $��
LayoutRoot��.+���  �� Orientation$ Vertical=�.+��� -   $��grbCertificados��$+��%Importar Datos de EXCEL (formato CSV)��$��Stretch=�$��Stretch=�.���  � ColumnDefinitions �� $
��100* ��� $	��400 ��� $
��100* � � RowDefinitions �� $	��30* ��� $	��10* �.+��� $��1��$��1��.+��� '%Seleccione su Archivo en Excel (.csv)�� $ 
Horizontal=�.+��� -   $��txtCsvFilePath��$	��250���� -   $��btnExaminarCsv��$��Examinar...��$��10,0,0,0q��� $ 
Horizontal=�.+��� " Tipo de informacion a importar :	� -   $��rdbProductos��$��Productos  ��	� -   $��rdbClientes��$��	Clientes ���� -   $��
cmdGuardar��$��Guardar��$��3��$��1��$��70��$��25���� -   $��grdImportarProdComer��$-��'Importar de otros productos comerciales��$��Stretch=�$��Stretch=�.���  �� $
��100* ��� $	��400 ��� $
��100* � �� $	��30* ��� $	��10* �.+��� $��1��$��1��.+��� Ruta de la base de datos�� $ 
Horizontal=�.+��� -	   $��txtFilePathOther��$	��250���� -
   $��btnExaminarOther��$��Examinar...��$��10,0,0,0q��� $ 
Horizontal=�.+��� $��35��
Versión	� -   $��rdbSAE26��$��	SAE 2.6  ��	� -   $��rdbSAE30��$��	SAE 3.0  ��	� -   $��rdbSAE40��$��	SAE 4.0  ��	� -   $��rdbAdminPack��$��	AdminPack���� -   $��cmdGuardarOther��$��Guardar��$��3��$��1��$��70��$��25���� -   $��grdImportarOtros��$(��"Importar de otras fuentes de datos��$��Stretch=�$��Stretch=�.���  �� $
��100* ��� $	��400 ��� $
��100* � �� $	��30* ��� $	��10* �.+��� $��1��$��1��.+��� $ 
Horizontal=�.+��� 
Servidor :�� -   $��	txtServer��$	��100���� $ 
Horizontal=�.+��� 	Usuario :�� -   $��
txtUsuario��$	��100���� $ 
Horizontal=�.+��� 	Password:�� -   $��txtPassword��$	��100���� $ Vertical=�.+��� $��35��Servidor de Base de Datos	� -   $��rdbSQL��$��Microsoft SQL Server��	� -   $��rdbMySQL��$��MySQL��	� -   $��	rdbOracle��$��Oracle���� -   $��cmdGuardarOtherSql��$��Guardar��$��3��$��1��$��70��$��25��