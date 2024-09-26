# Uno-Platform 자전거 대여소 관리자 (Desktop Cross-Platform)

## Contents
- 설치 및 준비
- 작업 순서

## Uno-Platform 설치

```
dotnet new install Uno.ProjectTemplates.Dotnet
```

## 프레임워크 프로젝트 복사
```
\sessions\3. uno-platform\start\Jamesnet.Core
\sessions\3. uno-platform\start\Jamesnet.Uno
```


## 작업 순서

### 1. 메인 애플리케이션 프로젝트 생성

```
BicycleSharingSystem
```

- [x] 타겟프레임워크: net8.0-desktop
- [x] 대상 운영체제: Windows, MacOS, Linux

프로젝트 생성은 기본 기능들을 대부분 제외한 Blank 모드로 생성합니다.

### 2. XAML-Based 기반의 프로젝트 아키텍트를 위한 기술
.NET Standard 2.0으로 WPF/Uno/WinUI3 등의 XAML-Based 기반에서 사용하는 프레임워크를 동일하게 사용하기 위한 전략입니다.

_적용된 프로젝트 예시_
- [x] [leagueoflegends-wpf](https://GitHub.com/jamesnetgroup/leagueoflegends-wpf)
- [x] [leagueoflegends-uno](https://GitHub.com/jamesnetgroup/leagueoflegends-wpf)
- [ ] [leagueoflegends-winui3](https://GitHub.com/jamesnetgroup/leagueoflegends-winui3)

플랫폼 간의 프레임워크 기술을 공유하는 것은 WPF, WinUI3와 같은 특정 운영체제에 종속적인 기술 환경에서 벗어나 크로스플랫폼 전환을 위한 좋은 기반이 됩니다.


#### 세부 주요 기능
- MVVM: SetProperty
- RelayCommand: ICommand, CanExecute Attribute
- Dependency Injection: Container, Singleton/Instance
- ViewModel Initialization: Reflection, 생성자 주입
- IView: 프로젝트 분산화, (비 종속 모듈화)
- UnoContent: IViewLoadable, 뷰 로드 시점 콜백

### 3. AppBootstrapper

XAML-Based 기반의 환경에서 빠르게 DI, MVVM 등의 환경을 구축하고 시점을 효과적을 관리하기 위한 클래스입니다.

유연하게 확장이 가능하도록 Application에서 이 클래스를 직접 상속 받고, 추상화와 오버라이드를 유연하게 확장 또는 변경을 통해 관리하는 것이 핵심입니다.

```csharp
internal class BicycleSharingSystemBootstrapper : AppBootstrapper
{
    protected override void LayerInitializer(IContainer container, ILayerManager layer)
    {
        // 초기 모듈 주입 등의 레이어 등록된 후 시점
    }

    protected override void RegisterDependencies(IContainer container)
    {
        // 의존성 주입을 위한 싱글턴/인스턴스 유형 등록
    }

    protected override void RegisterViewModels(IViewModelMapper viewModelMapper)
    {
        // 뷰/뷰모델간 연결 시나리오 등록
    }
}
```


LayerInitializer,  RegisterDependencies, RegisterViewModels메서드는 Abstract 타입으로 구현이 강제되지만 여러분의 프로젝트 정책과 설계 방식에 따라 이를 변경할 수도 있습니다.

### 4. App.xaml.cs

Uno-Platform은 WPF부터 시작된 전통적인 기본 구성 프로젝트 구조 방식을 그대로 계승하고 있습니다. 따라서 WPF와 동일한 흐름으로 Application 인스턴스가 생성됩니다.

우리는 Application 인스턴스가 생성되는 과정에서 적절한 타이밍에 AppBootstrapper를 함께 생성하면 됩니다.

_OnLaunched_
```csharp
protected override void OnLaunched(LaunchActivatedEventArgs args)
{
    var bootstrapper = new BicycleSharingSystemBootstrapper();
    bootstrapper.Run();
}
```

AppBootstrapper는의존성 주입에서 사용할 싱글턴/인스턴스 그리고 뷰모델 연결을 위한 시나리오와 초기 화면 모듈 주입을 위한 목적이 강하기 때문에 시기적으로 메인 뷰(MainPage)를 로드하기 전의 시점에서 AppBootstrapper를 생성하는 것이 좋습니다.

### 5. 라이브러리 생성 (BicycleSharingSystem.Main)

메인 화면의 프로젝트 분산화는 애플리케이션의 목적을 효과적으로 단순화 시킬 수 있습니다.

- [x] 애플리케이션의 아키텍트화: 의존성주입, 모듈 초기셋팅 등을 담당
- [x] 복잡하고 변경이 빈번하게 발생하는 메인 레이아웃의 모듈화 관리

```
BicycleSharingSystem.Main
```


### 6. CustomControl 구현 규칙

- [x] Class: UI/Views/MainContent.cs
- [x] ResourceDictionary: Themes/Views/MainContent.xaml

CustomControl은 클래스와 리소스가 물리적으로 나뉘어 관리되기 때문에 폴더의 깊이를 같은 수준으로 맞추도록 설계하는 것이 관리, 확장적인 측면에서 효과적입니다.


#### Themes/Generic.xaml
WPF와 마찬가지로 Uno-Platform도 CustomControl 대상의 기본 리소스를 Generic.xaml 안에 포함시겨야 합니다.

```xaml
<ResourceDictionary Source="ms-appx:///BicycleSharingSystem.Main/Themes/Views/MainContent.xaml"/>
```

#### WPF와 Uno-Platform간의 차이

```
// WPF
"/BicycleSharingSystem.Main;component/Themes/Views/MainContent.xaml"
// Uno-Platform
"ms-appx:///BicycleSharingSystem.Main/Themes/Views/MainContent.xaml"
```

두 플랫폼간의 문법에 미묘한 차이가 있습니다. 또한, UWP와 WinUI3도 Uno-Platform과 같습니다.

#### MainContent.cs 클래스 추가
```
using Jamesnet.Uno;

namespace BicycleSharingSystem.Main.UI.Views;

public class MainContent : UnoView
{
    public MainContent()
    {
        DefaultStyleKey = typeof(MainContent);
    }
}
```

#### MainContent 구현 및 테스트를 위한 Slider 컨트롤 추가
```
<ResourceDictionary
  xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
  xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
  xmlns:views="using:BicycleSharingSystem.Main.UI.Views">

  <Style TargetType="views:MainContent">
    <Setter Property="Template">
      <Setter.Value>
        <ControlTemplate TargetType="views:MainContent">
          <Grid>
            <Slider/>
          </Grid>
        </ControlTemplate>
      </Setter.Value>
    </Setter>
  </Style>
</ResourceDictionary>
```

MainContent 클래스를 TargetType으로 지정하기 위해서는 다음 네임스페이스가 추가되어야 합니다.

```
xmlns:views="using:BicycleSharingSystem.Main.UI.Views"
```

### 7. 애플리케이션에서 뷰의 참조 추가

이제 애플리케이션에서, MainContent를 사용하기 위한 단계입니다.
- [x] 프로젝트 참조 추가: BicycleSharingSystem.Main 
- [x] 뷰 싱글턴 등록: MainContent
- [x] 뷰 초기 주입: MainContent
- [x] MainPage에서 UnoLayer 컨트롤 추가: "MainLayer"
```
protected override void LayerInitializer(IContainer container, ILayerManager layer)
{
    IView mainContent = container.Resolve<MainContent>();

    layer.Mapping("MainLayer", mainContent);
}
```

Resolve를 통해 등록된 뷰를 찾고 MainLayer에 주입합니다.

### 8. MainContent 뷰모델 구성
다음은 MainContent의 뷰모델을 생성하고 구성하는 단계입니다.
- [x] 뷰모델 생성: Local/ViewModels/MainContentViewModel.cs
- [x] 뷰모델 시나리오 등록: RegisterViewModels (abstract)
- [x] 디버깅 테스트

### 9. 라이브러리 생성 (.Navigate)
다음은 BicycleSharingSystem.Navigate를 구성하는 단계입니다.
- [x] UI/Views: MenuContent.cs 생성
- [x] Themes/Views: MainContent.xaml 생성
- [x] Themes: Generic.xaml 생성 및 MergedDictionaries 리소스 연결
- [x] Local:ViewModels: MenuContentViewModel.cs 뷰모델 생성
- [x] 라디오버튼 메뉴 구성
- [x] Command 구현 (CommandParameter)
- [x] 디버깅 테스트: Command


### 10. MenuContent 모듈 주입을 위한 과정
이제 MenuContent를 ~~계층적 참조가 아닌~~ 모듈 주입 방식으로 MainContent에 포함시키기 위한 단계입니다.

- [x] 애플리케이션에서 참조 추가: BicycleSharingSystem.Navigate 프로젝트
- [x] 싱글턴 등록: MenuContent
- [x] 뷰 초기 주입: MenuContent

```
protected override void LayerInitializer(IContainer container, ILayerManager layer)
{
    IView mainContent = container.Resolve<MainContent>();
    IView menuContent = container.Resolve<MenuContent>();

    layer.Mapping("MainLayer", mainContent);
    layer.Mapping("MenuLayer", menuContent);
}
```

### 11. 라이브러리 생성 (.Rental)

이번에는 자전거 대여소 정보 관리를 위한 화면을  만들 차례입니다.
- [x] RentalContent: UnoView 생성 (CustomControl

### 12. RentalContent 뷰 등록 및 메뉴에서 연결
이번에는 RentalContent 뷰를 등록하고 메뉴에서 연결하기 위한 과정입니다.

- [x] 이름으로 뷰 등록: "RentalContent"
- [x] MenuContentViewModel에서 RentalContent 뷰 주입

```
public void NavigateTo(string menu)
{
    var layerManager = _container.Resolve<ILayerManager>();
    var contentView = _container.Resolve<IView>($"{menu}Content");
    layerManager.Show("ContentLayer", contentView);
}
```

### 14. RentalContent 구현

- [x] RentalViewModel  생성
- [x] 등록/조회/수정/삭제 구현
  - [x] ListView 목록
  - [x] SelectedItem 세부 내용 바인딩 및 수정
  - [x] Command/CanExecute 구현

### 15. 라이브러리 생성 (.Support)

이번에는 API 서비스, 공용 컨트롤 등을 관리하기 위한 프로젝트를 구성하는 단계입니다.

- [x] UI/Units: BicRadioButton.cs 추가 (CustomControl)
- [x] Themes/Units: BicRadioButton.xaml 추가
- [x] Generic.xaml 추가 (MergedDictionaries)
- [x] API 연결을 위한 Service 추가
- [x] 데이터 모델 추가


### 16. MenuManager 설계
이번에는 메뉴를 좀 더 유연하고 확장성 있게 관리하기 위한 단계입니다.

- [x] Local/Services: MenuManager 생성
- [x] 기존 메뉴 로직 대체
- [x] IMenuManager  인터페이스 추가
