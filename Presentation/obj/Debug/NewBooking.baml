   M S B A M L   `   `   `  ����G  CPresentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PresentationPresentation  5   	       Presentation.NewBooking   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Presentation     5   	     � Title$  
NewBooking��5   	   $	��300��6   $	��300��6)   .��5	      � $��0,0,3.333,-0.333q�6    � ColumnDefinitions 5
   
   �� 5      $��8* �6   �� 5      $
��285* �6   .+�5   
   �� $��	Check-in:��6   $
��Left=�6$   $��37.611,72,0,0q�6?   $	��Top=�6V   $��27��6n   $��65��6z   $��1��6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ dateInPicker��5      $
��Left=�6+   $��106.611,72,0,0q�6F   $	��Top=�6^   $��1��6v   �� 5   
   $��
Check-out:��6   $
��Left=�6%   $��37.611,120,0,0q�6@   $	��Top=�6X   $��27��6p   $��65��6|   $��1��6�     5   
   -   5        $ dateOutPicker��5      $
��Left=�6,   $��107,120,0,0q�6G   $	��Top=�6\   $��1��6t   $	��128��6�   �� 5   
   -   5         ��Name$ btnNext��5      $
��Next��6"   $
��Left=�61   $��160,182,0,0q�6L   $	��Top=�6a   $��75��6y   $��1��6�   �� 5   
   -   5        $ btnBack��5      $
��Back��6"   $��1��61   $
��Left=�6A   $��
38,182,0,0q�6\   $	��Top=�6p   $��75��6�   6�   �� 5   
   $��103,218,-4,2q�6   $��uni logo.jpg��6&   $
��Fill=�6<   $��1��6K   5      5      