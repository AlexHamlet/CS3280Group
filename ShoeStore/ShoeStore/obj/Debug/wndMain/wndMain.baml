   M S B A M L   `   `   `  ����D  @ShoeStore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:mainWindow
mainWindow  5   	       mainWindow.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:mainWindow     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��714��6)   .��5	      � -   5         �Name$
 Save��5	      $��0,0,2,0q�6    � ColumnDefinitions 5
   
   �� 5      �� 5      $��0* �6   .+�5   
   �� -   5         ��Name$ ___No_Name_��5      $��Items��6(   $
��Left=�67   $	��245��6R   $��
22,130,0,0q�6_   $	��Top=�6s   $	��672��6�   .��5      �� -   5         ��Name$ DeleteItemButton��5      $��Delete Item��6/   $
��Left=�6E   $��578,122,0,0q�6`   $	��Top=�6u   $��75��6�   6�   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgAll_Items��5      $
��Left=�6(   $	��162��6C   $��310,170,0,0q�6P   $	��Top=�6e   $	��290��6}      
IsReadOnly . 6�   �� 5   
   -   5        $ CreateInvoice��5      $��Create��6(   $
��Left=�69   $��
360,65,0,0q�6T   $	��Top=�6h   $��75��6�   6�   �� 5   
   -   5        $ EditInvoice��5      $
��Edit��6&   $
��Left=�65   $��
474,65,0,0q�6P   $	��Top=�6d   $��76��6|   6�   �� 5   
   -   5        $ DeleteInvoice��5      $��Delete��6(   $
��Left=�69   $��474,106,0,0q�6T   $	��Top=�6i   $��75��6�   6�   o� 5   
   -   5         o�Name$ MenuTabs��5      $
��Left=�6!   $��29��6<   $��	10,10,0,0q�6H   $	��Top=�6[   $��176.5��6s   $��15(�6�   .@�5      m� -	   5         m�Name$ 	searchTab��5      $��Search��6*   $��91��6:   	 m� StaysOpenOnClick	 . 6]   m� 5      -
   5        $ itemsTab��5      $��Items��6)   $
��86.5��68   6E   5      (  "System.Windows.Controls.DatePicker  5   
   -   5        
  Name$
 MainWndDateTimePicker��5      $
��Left=�64   $��
145,67,0,0q�6O   $	��Top=�6c    J� RenderTransformOrigin$ -0.029,1.042&�6{   �� 5   
   -   5         ��Name$ InvoiceIdLabel��5      $��	InvoiceId��6(   $
��Left=�6<   $��	22,67,0,0q�6W   $	��Top=�6j   $ 0.658,-0.192&�6�   �� 5   
   -   5         ��Name$ cbItems��5      $
��Left=�6$   $��
42,170,0,0q�6?   $	��Top=�6S   $	��205��6k   6w   �� 5    
   -   5         ��Name$ AmountOfItems��5       $
��Left=�6)   $��23��6D   $��110,247,0,0q�6P    �� TextWrapping$
 Wrap=�6e   $	��Top=�6y   $	��137��6�   6�   �� 5!   
   -   5        $ SaveItem��5!      $��Add Item��6#   $
��Left=�66   $��605,247,0,0q�6Q   $	��Top=�6f   $��75��6~   6�   �� 5"   
   -   5        $ SaveInvoice��5"      $��Save Invoice��6&   $
��Left=�6=   $��360,107,0,0q�6X   $	��Top=�6m   $��75��6�   ��.  $ 0.067,0&�6�   6�   �� 5#   
   -   5        $ TotalCostTextBox��5#      $
��Left=�6,   $��23��6G   $��110,292,0,0q�6S   $
 Wrap=�6h   $	��Top=�6|   $	��137��6�    �� 
IsReadOnly . 6�   �� 5$   
   -   5        $ TotalCostLabel��5$      $��Total Cost: ��6(   $
��Left=�6?   $��
42,292,0,0q�6Z   $	��Top=�6n   �� 5%   
   -   5        $ ItemCostTextBox��5%      $
��Left=�6+   $��23��6F   $��110,207,0,0q�6R   $
 Wrap=�6g   $	��Top=�6{   $	��137��6�    . 6�   �� 5&   
   -   5        $ QuantityLabel��5&      $��	Quantity:��6'   $
��Left=�6;   $��
42,254,0,0q�6V   $	��Top=�6j   �� 5'   
   -   5        $ ItemCost��5'      $��
Item Cost:��6"   $
��Left=�67   $��
42,214,0,0q�6R   $	��Top=�6f   5*      5+      