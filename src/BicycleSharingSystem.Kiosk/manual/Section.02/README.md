## 프로젝트 설정
1. `Avalonia .NET MVVM App` 템플릿 선택
2. `고급설정` - `Usecompiled Bindings` 체크 X
3. `고급설정` - `Remove Avalonia ViewLocator` 체크 X
3. `MVVMToolkit` - CommunityToolkit 선택

## 현재 템플릿 라이브러리 버전 정보
1. Avalonia - 11.1.3
2. CommunityToolkit - 8.2.2
3. Projektanker.Icons.Avalonia - 9.4.0
3. Projektanker.Icons.Avalonia.MaterialDesign - 9.4.0

## Assets
1. BMW Logo.png

## 폴더트리
`Assets` 

`Components`

`Pages`

&nbsp;&nbsp; &nbsp;&nbsp;ㄴ`Bike`

&nbsp;&nbsp; &nbsp;&nbsp;ㄴ`Home`

&nbsp;&nbsp; &nbsp;&nbsp;ㄴ`RentalOffice`

`ViewModels`

## ViewLocator 클래스 수정
 기존 -> Views/{화면이름}View.xaml

 변경 -> Pages/{화면이름}/Index.xaml 