   M S B A M L   `   `   `  ����D  @ShoeStore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ShoeStore.SearchShoeStore.Search  5   	        ShoeStore.Search.wndSearch   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:ShoeStore.Search     5   	   -   5          � Title$  	wndSearch��5   	   $��344.4��6   $	��794��6*   .��5	      � .+�5
   
   �� $��Invoices��6   $
��Left=�6%   $��256.333��6@   $��	10,10,0,0q�6Q   $	��Top=�6d   $	��545��6|   .��5      &   System.Windows.Controls.DataGrid  -   5          Name$ 
dgInvoices��5      $
��Left=�6+   $��212.008��6F   $	��Top=�6W   $	��535��6o   $��0,10,-2,-4.968q�6{      SelectionMode$ Single=�6�      
IsReadOnly . 6�   5      �� 5   
   $��Filter��6   $
��Left=�6#   $��256.5��6>   $��
560,10,0,0q�6M   $	��Top=�6a   $	��203��6y   .��5      � $
��Left=�6   $	��238��6.   $	��Top=�6;   $	��193��6S   $��0,0,-2.6,-12.4q�6_   .+�5      �� -   5         ��Name$ cmbbxInvNum��5      $
��Left=�60   $��	10,52,0,0q�6K   $	��Top=�6^   $	��120��6v   6�   �� 5      $��Invoice Number��6   $
��Left=�61   $��	10,21,0,0q�6L   $	��Top=�6_   �� 5      -   5        $ cmbbxInvDate��5      $
��Left=�61   $��
10,110,0,0q�6L   $	��Top=�6`   $	��120��6x   6�   �� 5      $��Invoice Date��6   $
��Left=�6/   $��	10,79,0,0q�6J   $	��Top=�6]   �� 5      -   5        $ cmbbxInvTot��5      $
��Left=�60   $��
10,169,0,0q�6K   $	��Top=�6_   $	��120��6w   6�   �� 5      $��Total Charge��6   $
��Left=�6/   $��
10,138,0,0q�6J   $	��Top=�6^   �� 5      -   5         ��Name$ btnClear��5      $��Clear��6+   $
��Left=�6;   $��55,210.04,0,0q�6V   $	��Top=�6m   $��75��6�    J� RenderTransformOrigin$ 0.735,1.137&�6�   5      5      �� 5   
   -   5        $ 	btnSelect��5      $��Select��6$   $
��Left=�65   $��285,271.333,0,0q�6P   $	��Top=�6i   $��75��6�   6�   �� 5   
   -   5        $ 	btnCancel��5      $��Cancel��6$   $
��Left=�65   $��416.667,271.333,0,0q�6P   $	��Top=�6m   $��75��6�   6�   5      5      