# SECTION.02 : 화면 전환하기

---

## 홈 화면 만들기
1. Pages\Home\Index.axaml의 Content 부분에 다음 코드를 삽입합니다.
```xml
    <Image Source="avares://BicycleSharingSystem.Kiosk/Assets/BMW Logo.png"  Height="312" Width="312" ></Image>
```
## 대여소 화면 초안 만들기
1. Pages\RentalOffice\Index.axml Content 부분에 Home화면과 구분을 짓기위해서 텍스트 수정 
```
RentalOffice 화면입니다.
```

---
# 화면 전환 하기
## 1. xaml 작업
1. MainWindow.axml 화면에 다음 코드를 삽입합니다.
```xml
<components:TopBanner />
<ContentControl Content="{Binding CurrentPage}" Grid.Row="1"/>
<components:NavigationBar Grid.Row="2" />
<components:BottomBanner Grid.Row="3" />
```
## 2. 뷰모델 작업
1. 다음 코드를 삽입해줍니다.
```csharp
[ObservableProperty]
private ViewModelBase _currentPage;

private readonly ViewModelBase[] Pages =
{
    new HomeViewModel(),
    new RentalOfficeViewModel(),
    new BicycleViewModel(),
};
```

2. 가장 맨 첫 번째 화면을 설정해줍니다.
```csharp
public MainWindowViewModel()
{
    this.CurrentPage = Pages[0];
}
```

3. 커맨드 명령에 맞는 화면 전환을 설정해줍니다.
```csharp
[RelayCommand]
private void HomeButton()
{
    this.CurrentPage = Pages[0];
}
[RelayCommand]
private void RentalOfficeButton()
{
    this.CurrentPage = Pages[1];
}
```
4. 빌드를 통해 화면 전환 확인해보기

## 3. 전환 애니메이션 넣어보기
1. 코드 수정 (contentcontrol -> TransitioningContentControl 애니메이션 추가)
```xml
<TransitioningContentControl Content="{Binding CurrentPage}" Grid.Row="1">
    <TransitioningContentControl.PageTransition>
        <PageSlide Duration="0:00:00.500" Orientation="Vertical" />
    </TransitioningContentControl.PageTransition>
</TransitioningContentControl>
```



[이전](https://github.com/blazorstudy/bicycle-sharing-system-workshop/tree/main/src/BicycleSharingSystem.Kiosk/manual/Section.01)
