# SECTION.01 : 메인 화면 만들기

---

## 컴포넌트(TopBannder, NavigationBar, BottomBanner) 만들기
### TopBannder
1. Components 폴더에 `Avalonia Template Control` 생성 TopBanner
2. PreivewWith 설정하기
```xml
<Design.PreviewWith>
    <Grid
        Background="#F9F9F9"
        RowDefinitions="27, *, 90, 38"
        Width="540">
        <controls:TopBanner />
    </Grid>
</Design.PreviewWith>
```
3. Icon을 사용하기 위해 접두어 추가 
```xml
xmlns:i="https://github.com/projektanker/icons.avalonia"
```
4. 화면 요소 추가
```xml
<Setter Property="Template">
    <ControlTemplate>
        <Grid Background="White" ColumnDefinitions="95,*">
            <Border Grid.Column="1" Padding="10,0">
                <TextBlock Text="BMW와 함께 하는 안전한 라이딩" />
            </Border>
            <Label
                Background="{StaticResource ColorBlack}"
                Grid.Column="0"
                HorizontalContentAlignment="Center">
                <Label.Content>
                    <StackPanel Orientation="Horizontal" Spacing="2">
                        <i:Icon
                            FontSize="18"
                            Foreground="{StaticResource ColorWhite}"
                            Value="mdi-microphone" />
                        <TextBlock Foreground="{StaticResource ColorWhite}" Text="공지사항" />
                    </StackPanel>
                </Label.Content>
            </Label>
        </Grid>
    </ControlTemplate>
</Setter>
```
5. 텍스트 슬라이딩 Style 추가
```xml
 <Grid Background="White" ColumnDefinitions="95,*">
    <Grid.Styles>
        <Style Selector="TextBlock">
            <Setter Property="VerticalAlignment" Value="Center" />
            <Setter Property="FontSize" Value="15" />
        </Style>
        <Style Selector="TextBlock.Message">
            <Style.Animations>
                <Animation
                    Delay="0:0:1"
                    Duration="0:0:15"
                    IterationCount="INFINITE">
                    <KeyFrame Cue="0%">
                        <Setter Property="TranslateTransform.X" Value="540" />
                    </KeyFrame>
                    <KeyFrame Cue="100%">
                        <Setter Property="TranslateTransform.X" Value="-540" />
                    </KeyFrame>
                </Animation>
            </Style.Animations>
        </Style>
    </Grid.Styles>
    .
    .
    .
</Grid>
```
6. Classes를 통한 Style 적용하기
```xml
<TextBlock Classes="Message" Text="BMW와 함께 하는 안전한 라이딩" />
```
### NavigationBar
1. Components 폴더에  `Avalonia Template Control` 생성 NavigationBar
2. PreivewWith 설정하기
```xml
<Design.PreviewWith>
    <Grid
        Background="#F9F9F9"
        RowDefinitions="27, *, 90, 38"
        Width="540">
        <controls:NavigationBar Grid.Row="2" />
    </Grid>
</Design.PreviewWith>
```
3. Icon을 사용하기 위해 접두어 추가
```xml
xmlns:i="https://github.com/projektanker/icons.avalonia"
```
4. 화면 요소 추가
```xml
<Setter Property="Template">
    <ControlTemplate>
        <Border
            Background="White"
            BorderBrush="DarkGray"
            BorderThickness="1"
            CornerRadius="10 10 0 0"
            Margin="0.6,0"
            Padding="5">
            <UniformGrid Columns="4">
                <Button
                    Content="처음화면"
                    Tag="mdi-home" />
                <Button
                    Content="대여소"
                    Tag="mdi-bike" />
                <Button
                    Content="서비스 준비중"
                    Tag="mdi-wrench" />
                <Button
                    Content="서비스 준비중"
                    Tag="mdi-wrench" />
            </UniformGrid>
        </Border>
    </ControlTemplate>
</Setter>
```

5. Button Theme 만들기 - Style로도 가능하지만 다른방법 있다라는 걸 알려주기 위해서
```xml
<!-- ResourceDicionary로 생성 된 경우-->
<ControlTheme x:Key="NaviItem" TargetType="Button">
    <Setter Property="Background" Value="Transparent" />
    <Setter Property="Template">
        <ControlTemplate>
            <Border
                    Width="90"
                    Background="{TemplateBinding Background}"
                    CornerRadius="5">
                <StackPanel VerticalAlignment="Center" Orientation="Vertical">
                    <i:Icon FontSize="40" Value="{TemplateBinding Tag}" />
                    <TextBlock
                            HorizontalAlignment="Center"
                            FontWeight="Bold"
                            Foreground="{StaticResource ColorBlack}"
                            Text="{TemplateBinding Content}" />
                </StackPanel>
            </Border>
        </ControlTemplate>
    </Setter>
</ControlTheme>
--------------------------------------------------------------------------------------
<!-- Styles 로 생성된 경우-->
<Styles.Resources>
    <ControlTheme TargetType="Button" x:Key="NaviItem">
        <Setter Property="Background" Value="Transparent" />
        <Setter Property="Template">
            <ControlTemplate>
                <Border
                    Background="{TemplateBinding Background}"
                    CornerRadius="5"
                    Width="90">
                    <StackPanel Orientation="Vertical" VerticalAlignment="Center">
                        <i:Icon FontSize="40" Value="{TemplateBinding Tag}" />
                        <TextBlock
                            FontWeight="Bold"
                            Foreground="{StaticResource ColorBlack}"
                            HorizontalAlignment="Center"
                            Text="{TemplateBinding Content}" />
                    </StackPanel>
                </Border>
            </ControlTemplate>
        </Setter>
    </ControlTheme>
</Styles.Resources>
```
6. 테마 적용해보기
```xml
Theme="{StaticResource NaviItem}" 
```
```xml
<Button
    Content="처음화면"
    Tag="mdi-home"
    Theme="{StaticResource NaviItem}" />
<Button
    Content="대여소"
    Tag="mdi-bike"
    Theme="{StaticResource NaviItem}" />
<Button
    Content="서비스 준비중"
    Tag="mdi-wrench"
    Theme="{StaticResource NaviItem}" />
<Button
    Content="서비스 준비중"
    Tag="mdi-wrench"
    Theme="{StaticResource NaviItem}" />
```
7. Command Binding 써놓기
```xml
Command="{Binding HomeButtonCommand}"
.
.
Command="{Binding RentalOfficeButtonCommand}"
```

```xml
 <Button
    Command="{Binding HomeButtonCommand}"
    Content="처음화면"
    Tag="mdi-home"
    Theme="{StaticResource NaviItem}" />
<Button
    Command="{Binding RentalOfficeButtonCommand}"
    Content="대여소"
    Tag="mdi-bike"
    Theme="{StaticResource NaviItem}" />
```
8. 마우스 올렸을 때 색상 변하게 하기
```xml
<Style Selector="^ /template/ Button:pointerover">
    <Setter Property="Background" Value="#e6e6e6" />
</Style>
```
### Bottom Banner
1. Components 폴더에  `Avalonia Template Control` 생성 BottomBanner
2. PreivewWith 설정하기
```xml
<Design.PreviewWith>
    <Grid
        Background="#F9F9F9"
        RowDefinitions="27, *, 90, 38"
        Width="540">
        <controls:BottomBanner Grid.Row="3" />
    </Grid>
</Design.PreviewWith>
```
3. xaml에서 실시간 시간 바인딩 시키기 위해 접두어 추가
```xml
xmlns:sys="clr-namespace:System;assembly=System.Runtime"
```
4. 화면요소 추가
```xml
    <Setter Property="Template">
        <ControlTemplate>
            <Border
                Background="{StaticResource ColorBlack}"
                Grid.Row="2"
                Padding="10,0">
                <TextBlock
                    Foreground="{StaticResource ColorWhite}"
                    Text="{Binding Source={x:Static sys:DateTime.Now}, StringFormat='{}{0:yyyy.MM.dd HH:mm}', Mode=OneWay}"
                    VerticalAlignment="Center" />
            </Border>
        </ControlTemplate>
    </Setter>
``` 
---
## 합쳐보기
1. 사이즈 고정 하기
```xml
Height="960"
MaxHeight="960"
MaxWidth="540"
MinHeight="960"
MinWidth="540"
Width="540"
d:DesignHeight="960"
d:DesignWidth="540"
```
2. componet Style 또는 ResourceDictionary 추가하기
```xml
xmlns:components="clr-namespace:BicycleSharingSystem.Kiosk.Components"
    
 .
 .
 .
    <!-- ResourceDicionary로 생성 된 경우-->
    <Window.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceInclude Source="Components/TopBanner.axaml" />
                <ResourceInclude Source="Components/NavigationBar.axaml" />
                <ResourceInclude Source="Components/BottomBanner.axaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Window.Resources>
--------------------------------------------------------------------------------------
    <!-- Styles 로 생성된 경우-->
   <Window.Styles>
       <StyleInclude Source="Components/TopBanner.axaml" />
       <StyleInclude Source="Components/NavigationBar.axaml" />
       <StyleInclude Source="Components/BottomBanner.axaml" />
   </Window.Styles>
```

3. 화면 요소 추가
```xml
<Grid RowDefinitions="27, *, 90, 38">
    <components:TopBanner />       
    <components:NavigationBar Grid.Row="2" />
    <components:BottomBanner Grid.Row="3" />
</Grid>
``` 
[이전](https://github.com/blazorstudy/bicycle-sharing-system-workshop/tree/main/src/BicycleSharingSystem.Kiosk/manual/Section.00)
&emsp; [다음](https://github.com/blazorstudy/bicycle-sharing-system-workshop/tree/main/src/BicycleSharingSystem.Kiosk/manual/Section.02)
