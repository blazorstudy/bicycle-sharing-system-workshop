# SECTION.03 : 스타일 넣어보기

## Test 데이터 생성 및 연동하기
1. Queries 폴더 생성 및 인터페이스 생성(IBicycleQuery, IRentalOfficeQuery)
```csharp
// IBicycleQuery.cs

namespace BicycleSharingSystem.Kiosk.Queries;
// DTO 모델
public sealed class BicycleDTO
{
    public string BicycleId { get; set; }
    public string RentalOfficeId { get; set; }
    public DateTime? StartRentalTime { get; set; }
    public string Name { get; set; }
    public DateTime? ExpireRentalTime { get; set; }
    public bool IsRental => StartRentalTime.HasValue;
}

public interface IBicycleQuery
{
    Task<List<BicycleDTO>> Get(string RentalOfficeId);
}

// 실제 연결 데이터 
public class BicycleQuery: IBicycleQuery
{
    public async Task<List<BicycleDTO>> Get(string RentalOfficeId)
    {
        throw new NotImplementedException();
    }
}

// 테스트 데이터 쿼리
public class TestBicycleQuery :IBicycleQuery
{
    public async Task<List<BicycleDTO>> Get(string RentalOfficeId)
    {
        return new List<BicycleDTO>()
        {
            new BicycleDTO()
            {
                BicycleId = Guid.NewGuid().ToString(),
                RentalOfficeId = RentalOfficeId,
                Name="Bike 1호기"
            },
            new BicycleDTO()
            {
                BicycleId = Guid.NewGuid().ToString(),
                RentalOfficeId = RentalOfficeId,
                Name="Bike 2호기"
            },
            new BicycleDTO()
            {
                BicycleId = Guid.NewGuid().ToString(),
                RentalOfficeId = RentalOfficeId,
                Name="Bike 3호기",
                StartRentalTime = DateTime.Now,
                ExpireRentalTime = DateTime.Now.AddMinutes(5)
            },
        };
    }
}    
```

```csharp
namespace BicycleSharingSystem.Kiosk.Queries;

// DTO 모델
public class RentalOfficeDTO
{
    public string OfficeId { get; set; }
    public string Name { get; set; }
    public string Region { get; set; } 
}

public interface IRentalOfficeQuery
{
    Task<List<RentalOfficeDTO>> Get();
}

// 실제 연결 데이터 
public class RentalOfficeQuery : IRentalOfficeQuery
{
    public async Task<List<RentalOfficeDTO>> Get()
    {
        throw new NotImplementedException();
    }
}
// 테스트 데이터 
public class TestRentalOfficeQuery : IRentalOfficeQuery
{
    public async Task<List<RentalOfficeDTO>> Get()
    {
        return new List<RentalOfficeDTO>
        {
            new RentalOfficeDTO() {OfficeId = Guid.NewGuid().ToString(), Name = "한빛대여소1", Region = "서울"},
            new RentalOfficeDTO() {OfficeId = Guid.NewGuid().ToString(), Name = "한빛대여소2", Region = "서울"},
            new RentalOfficeDTO() {OfficeId = Guid.NewGuid().ToString(), Name = "한빛대여소3", Region = "서울"},
            new RentalOfficeDTO() {OfficeId = Guid.NewGuid().ToString(), Name = "BMW대여소1", Region = "경기"},
            new RentalOfficeDTO() {OfficeId = Guid.NewGuid().ToString(), Name = "BMW대여소2", Region = "경기"},
        };
    }
}
```
2. MainWindowViewModel의 매개변수 추가 및 수정
```csharp
// App.axaml.cs

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(new TestRentalOfficeQuery(), new TestBicycleQuery()),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
```
```csharp
private ViewModelBase[] Pages;
public MainWindowViewModel(IRentalOfficeQuery rentalOfficeQuery,
                           IBicycleQuery bicycleQuery)
{
    Pages = new ViewModelBase[]{
        new HomeViewModel(),
        new RentalOfficeViewModel(rentalOfficeQuery),
        new BicycleViewModel(bicycleQuery),
    };
    this.CurrentPage = Pages[0];
}
```
3. 화면 데이터 모델 생성
- Pages/RentalOffice/Models/RentalOfficeModel.cs
- Pages/Bicycle/Models/BicycleModel.cs
```csharp
// RentalOfficeModel.cs
namespace BicycleSharingSystem.Kiosk.Pages.RentalOffice.Models;

public class RentalOfficeModel
{
    public string Number { get; set; }
    public string RegionName { get; set; }
    public string Name { get; set; }

    public RentalOfficeModel(RentalOfficeDTO dto)
    {
        Number = dto.OfficeId;
        Name = dto.Name;
        RegionName = dto.Region;
    }
}
```
```csharp
// BicycleModel.cs
public partial class BicycleModel : ObservableObject
{
    public string Name { get; set; }
    public string StartRentalTime { get; set; }
    public string RemainDateTime { get; set; }
    [ObservableProperty] private bool isRental = false;

    public BicycleModel(BicycleDTO dto)
    {
        this.Name = dto.Name;
        this.IsRental = dto.IsRental;
        if (this.IsRental)
        {
            this.StartRentalTime = dto.StartRentalTime.Value.ToString("HH:mm");
            TimeSpan timeDiff = dto.ExpireRentalTime.Value - DateTime.Now;
            this.RemainDateTime = $"{timeDiff.Minutes.ToString("D2")}:{timeDiff.Seconds.ToString("D2")}";
        }
    }
}
```
4. Load 함수 생성
```csharp
// ViewModels/ViewModelBase.cs

public class ViewModelBase : ObservableObject
{
    public virtual async Task Load()
    {
        
    }
}
```
5. ViewModel 수정
```csharp
// ViewModels/RentalOfficeViewModel.cs
public partial class RentalOfficeViewModel: ViewModelBase
{
    [ObservableProperty] ObservableCollection<RentalOfficeModel> rentalOffices;
    [ObservableProperty] ObservableCollection<RentalOfficeModel> filterRentalOffices;
    [ObservableProperty] ObservableCollection<string> regions;
    [ObservableProperty] string selectedRegions;
    private readonly IRentalOfficeQuery _rentalOfficeQuery;
    public RentalOfficeViewModel(IRentalOfficeQuery rentalOfficeQuery)
    {
        _rentalOfficeQuery = rentalOfficeQuery;
        this.RentalOffices = new();
        this.Regions = new();
        this.FilterRentalOffices = new();
    }
}
```
```csharp
// ViewModels/BicycleViewModel.cs
public partial class BicycleViewModel : ViewModelBase
{
    [ObservableProperty] private List<BicycleModel> bicycles;
    [ObservableProperty] private string renterShopName;
    private readonly IBicycleQuery _bicycleQuery;
    public BicycleViewModel(IBicycleQuery bicycleQuery)
    {
        this._bicycleQuery = bicycleQuery;
        this.Bicycles = new();
    }
}
```
---
## 대여소 화면 연동 및 꾸미기
1. Components 폴더에 `Avalonia Template Control` 로 RentalOfficePanel , RentalOfficeItem 생성 
```csharp
Visualstudio 인경우    
기존 : xmlns:controls="using:BicycleSharingSystem.Kiosk" 
변경 : xmlns:controls="using:BicycleSharingSystem.Kiosk.Pages.RentalOffice.Component" 수정 해줄 것
``` 
2. 디자인 초안만들기
```xml
<!-- RentalOfficeItem.axaml -->

<Design.PreviewWith>
    <Border Background="#F9F9F9" Padding="30">
        <controls:RentalOfficeItem />
    </Border>
</Design.PreviewWith>

.
.
.

<Setter Property="Template">
    <ControlTemplate>
        <Border
                Background="White"
                BorderBrush="#d2d2d2"
                BorderThickness="1"
                CornerRadius="5"
                Height="100"
                Padding="20"
                Width="500">
            <TextBlock VerticalAlignment="Center">
                <Run Text="{Binding RegionName}" />
                <LineBreak />
                <Run Text="{Binding Name}" />
            </TextBlock>
        </Border>
    </ControlTemplate>
</Setter>
```

2. Load 데이터 연동
```csharp
// Pages/RentalOffice/Index.axaml.cs
public partial class Index : UserControl
{
    public Index()
    {
        InitializeComponent();
        this.Loaded += (s, e) =>
        {
            ((ViewModelBase)this.DataContext).Load();
        };
    }
}
```
```csharp
// ViewModels/RentalOfficeViewModel.cs
    .
    .
    .
    // 추가할 것
    public override async Task Load()
    {
        RentalOffices.Clear();
        Regions.Clear();
        foreach (var item in await this._rentalOfficeQuery.Get())
        {
            RentalOffices.Add(new RentalOfficeModel(item));
        }
        
        Regions = new(RentalOffices
            .GroupBy(p => p.RegionName)
            .Select(g => g.Key).ToList());
        
        Regions.Insert(0, "전체");

        SelectedRegions = Regions.First();
    }
    .
    .
    .    
```