   M S B A M L   `   `   `  ���� @  <Vesna, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Vesna.PagesVesna.Pages  H DMahApps.Metro, Version=1.6.0.0, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls     Vesna.Pages.Users   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     +localclr-namespace:Vesna.Pages     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     $��14(�  � 	Resources     ShowAdd �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value  HideAdd  ShowEditItem  HideEditItem��%8  &	        &	 <    &	 x    &	 �    �� .��U� $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�$
 grid��.\�  $ 0��$ 0Q�  $ 0:0:0.5��$ 1Q�U� $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�$
 grid��.\�  $ 0��$ 0Q�  $ 0:0:0.5��$ 1Q��� .��U� $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�$
 grid��.\�  $ 0��$ 1Q�  $ 0:0:0.5��$ 0Q�U� $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�$
 grid��.\�  $ 0��$ 1Q�  $ 0:0:0.5��$ 0Q��� .��U� $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�$ editItemGrid��.\�  $ 0��$ 0Q�  $ 0:0:0.5��$ 1Q�U� $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�$ editItemGrid��.\�  $ 0��$ 0Q�  $ 0:0:0.5��$ 1Q��� .��U� $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�$ editItemGrid��.\�  $ 0��$ 1Q�  $ 0:0:0.5��$ 0Q�U� $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�$ editItemGrid��.\�  $ 0��$ 1Q�  $ 0:0:0.5��$ 0Q�.���  � RowDefinitions �� $
��Auto ��� .+�� $��0��$��0,0,0,10q� � ColumnDefinitions �� $��* ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -   $��Обновить��$��1��$��20, 10, 20,0q�$��Right=��� -    ��Name$ addItem��$/��)Добавить пользователя��$��2��$��20, 10, 20, 0q�$��Right=��� -   $ editItem��$/��)Изменить пользователя��$��3��$��20, 10, 20, 0q�$��Right=��� -   *  $MahApps.Metro.Controls.TextBoxHelper   ClearTextButton . $��	20,10,0,0q�$��0��&   System.Windows.Controls.DataGrid  -   	  Name$	 dataGrid��$��1��
   CanUserDeleteRows
 .     SelectionMode$ Single=�   CanUserAddRows .     AutoGenerateColumns .     
IsReadOnly . ���� *UsersCollection+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ФИО��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *FIO+  $ Почтовый ящик�� �� *Email+  $ Телефон�� �� *Phone+  $ 
Логин�� �� *Login+  $ Пароль�� �� *
Password+� -    �Name$
 grid��$��1�� K� ZIndex$ 1��$��Center=�$��Center=� J� RenderTransformOrigin$ 0.5,0.5&�$y�	Collapsed=�z�V� .~��� �� �� T�  �� $
��Auto ��� �� �� �� �� �� �� .+��� $3��-Добавление пользователя��$��Center=�$��18(�� $��1��$��
10,10,10,0q� �� �� .+��� $��ФИО:��$��0��$��Center=�$��0,0,5,0q��� -    ��Name$ userFIO��$��1�� . $	��200��$	��300��� $��2��$��
10,10,10,0q� �� �� .+��� $ ��Почтовый ящик:��$��0��$��Center=��� -   $ 	userEMAIL��$��1�� . $	��150��� $��3��$��
10,10,10,0q� �� �� .+��� $��Телефон:��$��0��$��Center=��� -	   $ 	userPHONE��$��1�� . $	��150��� $��4��$��
10,10,10,0q� �� �� .+��� $��Логин:��$��0��$��Center=��� -
   $ 	userLOGIN��$��1�� . $	��150��� $��5��$��
10,10,10,0q� �� �� .+��� $��Пароль:��$��0��$��Center=�G� -    G�Name$ userPASSWORD��$��1�� . $	��150��� $��6��$��
10,10,10,0q� �� �� .+�	� -    	�Name$ userRoleMeneger�� 	� 	GroupName$
 Role�� d� 	IsChecked$
 TrueY�$��Менеджер��$��0��$��Center=�	� -   $ userRoleAdmin��$
 Role��$ ��Администратор��$��1��$��Center=�� -   $ AddCanselGrid��$��7��$��Right=�$��10,10,10,10q� �� �� .+��� -   $��Отмена��$��0���� -   $��Добавить��$��1��$��81,0,0,0q�� -   $ editItemGrid��$��1��$ 1��$��Center=�$��Center=�$y�	Collapsed=�$ 0.5,0.5&�z�V� .~��� �� �� T�  �� $
��Auto ��� �� �� �� �� �� �� .+��� $3��-Добавление пользователя��$��Center=�$��18(�� $��1��$��
10,10,10,0q� �� �� .+��� $��ФИО:��$��0��$��Center=�$��0,0,5,0q��� -   $ edituserFIO��$��1�� . $	��200��$	��300��� $��2��$��
10,10,10,0q� �� �� .+��� $ ��Почтовый ящик:��$��0��$��Center=��� -   $ edituserEMAIL��$��1�� . $	��150��� $��3��$��
10,10,10,0q� �� �� .+��� $��Телефон:��$��0��$��Center=��� -   $ edituserPHONE��$��1�� . $	��150��� $��4��$��
10,10,10,0q� �� �� .+��� $��Логин:��$��0��$��Center=��� -   $ edituserLOGIN��$��1�� . $	��150��� $��5��$��
10,10,10,0q� �� �� .+��� $��Пароль:��$��0��$��Center=�G� -   $ edituserPASSWORD��$��1�� . $	��150��� -   $ EditCanselGrid��$��7��$��Right=�$��10,10,10,10q� �� �� �� .+��� -   $��Удалить��$��0���� -   $��Отмена��$��1��$��41,0,0,0q��� -   $��Сохранить��$��2��$��40,0,0,0q�