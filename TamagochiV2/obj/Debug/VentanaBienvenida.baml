   M S B A M L   `   `   `  ����F  BTamagochiV2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:TamagochiV2TamagochiV2  5   	   #    TamagochiV2.VentanaBienvenida   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:TamagochiV2     5   	     � Title$  VentanaBienvenida��5   	   $	��300��6#   $	��500��60    � WindowStartupLocation$ CenterScreen=�6<    � WindowStyle$
 None=�6a    � 
ResizeMode$ NoResize=�6t    � Icon$ 
trofeo.png��6�   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��121* �6   �� 5      $	��65* �6   �� 5      $	��84* �6   ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    � MappingMode$ RelativeToBoundingBox=�63    �� 
StartPoint$ 0.5,0&�6W   .��5      � 	 � Color$	 	#FF050000��6   
 � Offset$
 1Q�61   � 5      $	 	#FFB41C1C��6   � 5      $	 Black��6   $
 1Q�6-   � 5      $	 	#FF153808��6   $
 0.774Q�61   5       � ColumnDefinitions 5   
   �� 5      $
��126* �6   �� 5      $
��272* �6   �� 5      $	��95* �6   .+�5   
   �� -   5         ��Name$ tbnombre��5      $��31.6,16,10.4,15.4q�6$    �� TextWrapping$
 Wrap=�6?   $��14(�6S   ���	#FF9C332A���	#FF762A15 �� SelectionBrush �	#FF6E2414$��1��6�   $
��Bold"�6�   $��1��6�   $��Center=�6�   $��Center=�6�   �� 5   
   -   5        $��Empezar��5      $��78.6,16.6,10.4,35.6q�6#   $
��Bold"�6U   ���	#FFAE4C4C$��2��6~   $��1��6�   (� 5   
   -   5         (�Name$ ViewBox1��5       (� Stretch$ Uniform=�6$    J� RenderTransformOrigin$ 0.5,0.5&�66   $��16.6,52,14.4,37q�6V   $��1��6o   z�5      V� 5       .~�5!      ��  �� ScaleY$ -1Q�6%   �� 5"      �� 5#      T� 5$      5%      .��5'      �� $
��Wrap=�6   $��20��6,   $	��112��68   $ 0.5,0.5&�6D   $��IPO2-TAMAGOTCHI��6d   $
��Bold"�6{   z�5(      V� 5)      .~�5*      ��  �� ScaleX$ -1Q�6)   �� 5+      �� 5,       �� Angle$ 180.253Q�6*   T� 5-      5.      5/   /   50      (� 51   
   $ Uniform=�6   $��	17,12,0,0q�6$   $��1��67   .��52      �� $
��Wrap=�6   $��Nombre del Tamagotchi��6,   $��36��6I   $��71��6U   $
��Bold"�6`   53      55      56      