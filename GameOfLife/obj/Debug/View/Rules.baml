   M S B A M L   `   `   `  ����E  AGameOfLife, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:GameOfLife
GameOfLife  5          GameOfLife.Rules   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:GameOfLife     5        N� 
Background  �#E6CDB5 N� Title$ Rules��5
       � 	Resources 5         TextBlockStyle �� 
TargetType �� Property �� Value��%�   &	        �� 5   
   " ��6'   .�5      ��  � ��$ 35(�6)   �� 5       � ��$ Book Antiqua)�6+   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��9* �6   �� 5      $	��41* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5!   
   �� $��1��6   $��1��6"   .+�5"      �� #��[  6   �� 5#      $
�• ��6    �� 5$      $V�PAny live cell with fewer than two live neighbours dies, as if by underpopulation��6   5%      5&      �� 5'   
   $��2��6   $��1��6"   .+�5(      �� #��[  6   �� 5)      $
�• ��6    �� 5*      $W�Q Any live cell with two or three live neighbours lives on to the next generation.��6   5+      5,      �� 5-   
   $��3��6   $��1��6"   .+�5.      �� #��[  6   �� 5/      $
�• ��6    �� 50      $X�R Any live cell with more than three live neighbours dies, as if by overpopulation.��6   51      52      �� 53   
   $��4��6   $��1��6"   .+�54      �� #��[  6   �� 55      $
�• ��6    �� 56      $c�] Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.��6   57      58      5:      5;      