   M S B A M L   `   `   `  ���� F  BQrCodeTools, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:QrCodeTools.PagesQrCodeTools.Pages  T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3ffCclr-namespace:MahApps.Metro.Controls.Dialogs;assembly=MahApps.MetroMahApps.Metro.Controls.Dialogs !    QrCodeTools.Pages.ShellView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     1localclr-namespace:QrCodeTools.Pages     A =Stylet, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null)s!https://github.com/canton7/Stylet  PDialogCclr-namespace:MahApps.Metro.Controls.Dialogs;assembly=MahApps.Metro  ;Controls,http://metro.mahapps.com/winfx/xaml/controls     AccentColorBrush(  "MahApps.Metro.Controls.MetroWindow    	GlowBrush#  �    � Title$! Stylet Start Project V_0424��$	��650��$	��900��8  2MahApps.Metro.Controls.Dialogs.DialogParticipation   Register ��  � 	Resources   ControlMargin  ControlMarginH ! CustomMetroToggleSwitchButton + 'MahApps.Metro.Styles.ToggleSwitchButton �� BasedOn �� 
TargetType/  )MahApps.Metro.Controls.ToggleSwitchButton �� Property �� Value��%�   &	       &	      &	      7  u�25 0 0 0q�u�5q��� 8   "  .���  �@ OffSwitchBrush
 �  ����  �@ OnSwitchBrush
 � d �.��� $��Stretch=�$��Stretch=�  AccentColorBrush4#���   � ColumnDefinitions �� $
��100* ��� $	��350 �.+�� $��0��	 � RowDefinitions	 �� $	��128 ��� $��8 ��� $
��100* �.+�� $��0��$y�Hidden=� �� $	��50* ��� $	��50* �.+��� -   
 ��Name$
 ImgLogContorl��$��Center=�$��5q�$��0������ *
LogBitmaps+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=��� $��1�� �� CornerRadius$ 5��._���  � DataContext �� *DrawingOptions+$ PropertyChanged=�$ TwoWay=�.+�� #��[ .+��� $��码眼1��#��[ �� -   $��TagColorOne��$��60��
���   �
 � Tag$ 1���� -   $	��128��$ 1��$��10,0,0,0q� �� SelectedItem �� *
qrCodeTag1+$ PropertyChanged=����� *QrCodeTagEnums+$ PropertyChanged=�� #��[ .+��� $��码眼2��#��[ �� -   $��TagColorTwo��$��60��
���   �$ 2���� -   $	��128��$ 2��$��10,0,0,0q� �� *
qrCodeTag2+$ PropertyChanged=����� *QrCodeTagEnums+� #��[ .+��� $��码眼3��#��[ �� -   $��TagColorThree��$��60��
���   �$ 3���� -   $	��128��$ 3��$��10,0,0,0q� �� *
qrCodeTag3+$ PropertyChanged=����� *QrCodeTagEnums+� #��[ .+��� $��点  阵��#��[ �� -   $��TagColorContent��$��60��
���   �$ 4���� .��	大小:�� -	   $	��100��$ 4�� �� *qrCodeSquareSize+$ PropertyChanged=����� *QrCodeSquareSizes+�� $��1���� $��2��$ 15��._��� -
   $
 
ImgContorl��$��Center=�$��15q�$��0��$
��None=����� *BitmapSource+$ PropertyChanged=�$ TwoWay=� � ContextMenu �� .@�m� -   $��
FiilSaveAs��$��	另存为���� $��1��$��15q�$ 15��  AccentColorBrush3#���  ._��� -   $��StackPanelTop��#��[ $��Stretch=� �� *DrawingOptions+$ PropertyChanged=�$ TwoWay=�.+��� #��[ $
��Left=�URL或其他文本:�� -   $��QrRichTextBox��$��85��#��[ $	��300�� �� TextWrapping$
 Wrap=�$
��Left=�$��Center=�*  $MahApps.Metro.Controls.TextBoxHelper   ClearTextButton .    	Watermark$- '请输入生成二维码的文本内容�� �� 	IsEnabled . ���� *QrText+$ PropertyChanged=�$ TwoWay=��� $
��Left=�$��添加图片(Log)��#��[ � #��[ {�.  .+��� -   $
��Left=� . $ 请输入LOGO文件路径�� �� 
IsReadOnly .  $	��193������ *LogoImgPath+$ PropertyChanged=��� $��填充��#��[  d� 	IsChecked �� *IsTopLogoImg+$ PropertyChanged=�$ TwoWay=�� #��[ .+�	� -   $��RadioButtonpxAotu��$
��Left=�$��自动尺寸:  �� 	� 	GroupName$ Group�� �� *IsQrCodeAutoSize+$ PropertyChanged=�$ TwoWay=��� $��85��#��[ {���  �� ElementName$ RadioButtonpxAotu�� �� Path$ 	IsChecked����� *	MudelSize+$ PropertyChanged=�� #��[ .+�	� -    	�Name$ RadioButtonpx��$
��Left=�$ Group��$��大小(像素):�� �� *IsTypeSizePX+�� -   $��	WidthPiex��$��85��#��[ {��� $ RadioButtonpx��$ 	IsChecked����� *QrCodePixelWidth+� #��[ .+�	� -   $��RadioButtonmm��$��Center=�$
��Left=�$ Group��$��尺寸(毫米):�� �� *IsTypeSizeMM+�� #��[ $
��Left=�$ 最小默认 100 像素��$��85������ *QrCodeWidthMM+ �� ValidatesOnDataErrors . $ PropertyChanged=� �� NotifyOnValidationError . {��� $ RadioButtonmm��$ 	IsChecked�� #��[ .+��� $��像素密度(DPI): ��$��Center=��� $��85��#��[ {��� $ RadioButtonmm��$ 	IsChecked����� *
DpiInchs+ �� *	DpiInch+�� #��[ .+��� $��二维码类型��$
��Left=��� $	��250��$
��Left=����� *QrCodeTypes+ �� *QrCodeTypeEnum+� #��[ .+��� .+��� $��白线宽度��$
��Left=��� $	��125��$
��Left=����� *ListdoubleFromats+$ PropertyChanged=� �� *widthdoubleFromat+�� #��[ .+��� $��白线分布密度��$
��Left=��� $	��125��$
��Left=����� *densitydoubleFromats+$ PropertyChanged=� �� *densitydoubleFromat+� #��[ .+��� .+��� $��图像格式��$
��Left=��� $	��125��$
��Left=����� *ImageFromats+$ PropertyChanged=� �� *ImageFromat+�� #��[ .+��� $��边缘留白��$
��Left=��� $
��Left=�$	��125������ *QuietZoneModuleComboBox+ �� *QuietZoneModule+$ PropertyChanged=�� #��[ .+��� .+��� $��纠错级别:��$
��Left=��� $
��Left=�$	��125������ *ErrorCorrectionLevelComboBox+ �� *errorCorrectionLevel+$ PropertyChanged=��� $y�Hidden=�#��[ .+��� 掩码�� $��Right=�$	��125������ *MaskPatternTypes+ �� *PatternType+$ PropertyChanged=�� #��[ .+��� -    ��Name$ 
EnQrButton��$��预览��$��35��$��80��$��10q��� -   $��	ExpButton��$��批量导出��$��35��$��80��$��10q��� -   $��ExpJsonButton��$��
导出Json��$y�Hidden=�$��35��$��80��$��10q��� -   $��
dasdasdasd��$y�Hidden=��� -   $��ExpButtonCommand��$y�Hidden=���!  Stylet.Xaml.ActionExtension  *Exportfiles+�� -   $��FileListButton��$y�Hidden=���  *GetFileList+�� -   $��LogBitmapButton��$y�Hidden=���  *ShowLogBitmap+�� -   $��ExpJsonCommand��$y�Hidden=���  *ExpJsonCommand+�� -   $��EnQrButtonCommand��$y�Hidden=���  *
EnQrCode+�� -   $��QrCodeTagOneButton��$y�Hidden=���  *QrCodeTagOneCommand+�� -   $��
SaveFileAs��$y�Hidden=���  *FiilSaveAsCommand+