   M S B A M L   `   `   `  ����E  ADataLogApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:DataLogApp.ViewsDataLogApp.Views  5      (    "DataLogApp.Views.RpmRangeSelection   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:DataLogApp.Views     5      
�������.��5
      �   � ColumnDefinitions  5   
   �� 5      $	��15* �6   6-   �� 5      $	��65* �6   6-   �� 5      $	��20* �6   6-    � RowDefinitions 5   
   �� 5      $	��17* �6   6+   �� 5      $	��17* �6   6+   �� 5      $	��17* �6   6+   �� 5      $	��17* �6   6+   �� 5      $	��17* �6   6+   �� 5      $	��16* �6   6+   .+�5   
   �� $��0��6   $��0��6    $��Center=�6-   $��Center=�6H   $��30��6e   $��Center=�6q   .��6�   
1500RPM:6�   �� 5   
   $��1��6   $��0��6"   $��23��60   $��Center=�6<   ��6W   ��  �� Path$ RpmPathDictionary[15]�6�   �� 5   
   $��2��6   $��0��6!   $��23��6.   $��50��6:   ��6E   �� $ Browse1500RpmPath�.��6p   Browse6x   �� 5   
   $��0��6   $��1��6    $��Center=�6-   $��Center=�6H   $��30��6e   $��Center=�6q   .��6�   
2000RPM:6�   �� 5   
   $��1��6   $��1��6"   $��23��61   $��Center=�6=   ��6X   �� $ Rpm2000Path�{�6z   �� $ NotUseMultiSelect�y�6�   �� $ UseMultiSelectVisibility�6�   �� 5   
   $��2��6   $��1��6!   $��23��6.   $��50��6:   ��6E   �� $ Browse2000RpmPath�y�6p   �� $ UseMultiSelectVisibility�.��6�   Browse6�   �� 5    
   $��0��6   $��2��6    $��Center=�6-   $��Center=�6H   $��30��6e   $��Center=�6q   .��6�   
2500RPM:6�   �� 5!   
   $��1��6   $��2��6"   $��23��61   $��Center=�6=   ��6X   �� $ Rpm2500Path�{�6z   �� $ NotUseMultiSelect�y�6�   �� $ UseMultiSelectVisibility�6�   �� 5"   
   $��2��6   $��2��6!   $��23��6.   $��50��6:   ��6E   �� $ Browse2500RpmPath�y�6p   �� $ UseMultiSelectVisibility�.��6�   Browse6�   �� 5$   
   $��0��6   $��3��6    $��Center=�6-   $��Center=�6H   $��30��6e   $��Center=�6q   .��6�   
3000RPM:6�   �� 5%   
   $��1��6   $��3��6"   $��23��61   $��Center=�6=   ��6X   �� $ Rpm3000Path�{�6z   �� $ NotUseMultiSelect�y�6�   �� $ UseMultiSelectVisibility�6�   �� 5&   
   $��2��6   $��3��6!   $��23��6.   $��50��6:   ��6E   �� $ Browse3000RpmPath�y�6p   �� $ UseMultiSelectVisibility�.��6�   Browse6�   �� 5(   
   $��0��6   $��4��6    $��Center=�6-   $��Center=�6H   $��30��6e   $��Center=�6q   .��6�   
3500RPM:6�   �� 5)   
   $��1��6   $��4��6"   $��23��61   $��Center=�6=   ��6X   �� $ Rpm3500Path�{�6z   �� $ NotUseMultiSelect�y�6�   �� $ UseMultiSelectVisibility�6�   �� 5*   
   $��2��6   $��4��6!   $��23��6.   $��50��6:   ��6E   �� $ Browse3500RpmPath�y�6p   �� $ UseMultiSelectVisibility�.��6�   Browse6�   �� 5,   
   $��0��6   $��5��6    $��Center=�6-   $��Center=�6H   $��30��6e   $��Center=�6q   .��6�   
4000RPM:6�   �� 5-   
   $��1��6   $��5��6"   $��23��61   $��Center=�6=   ��6X   �� $ Rpm4000Path�{�6z   �� $ NotUseMultiSelect�y�6�   �� $ UseMultiSelectVisibility�6�   �� 5.   
   $��2��6   $��5��6!   $��23��6.   $��50��6:   ��6E   �� $ Browse4000RpmPath�y�6p   �� $ UseMultiSelectVisibility�.��6�   Browse6�   50      51      