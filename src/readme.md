# 준비 작업

## Backend 및 데이터 설정 방법

1. `BicycleSharingSystem.AppHost`를 시작 프로젝트로 설정 후 실행(또는 `dotnet run`)
1. [DataContext.http](./BicycleSharingSystem.WebApi/DataContext.http) 파일 열기
  1. Visual Studio Code인 경우 [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) 확장이 필요합니다.
1. 대여소와 자전거를 순차적으로 `Send request` 버튼 클릭


## 대여소가 잘 들어갔는지 확인하는 방법

1. [RentalOffice.WebApi.Tests.http](./BicycleSharingSystem.WebApi/RentalOffice.WebApi.Tests.http) 파일 열기
1. 아래 부분 하나씩 `Send request` 클릭 후 데이터 들어오는지 확인

```http
GET {{HostAddress}}/rentaloffice HTTP/1.1

GET {{HostAddress}}/rentaloffice/BMW HTTP/1.1

```


## 대여/반납 테스트

1. [Bicycle.WebApi.Tests.http](./BicycleSharingSystem.WebApi/Bicycle.WebApi.Tests.http) 파일 열기
1. 파일 최상단 `TestBicycleId` 부분에 이전 단계에서 조회한 임의의 자전거 데이터로 변경
1. 아래 부분 하나씩 `Send request` 클릭 후 데이터 들어오는지 확인

```http
PUT {{HostAddress}}/user/rental/{{TestBicycleId}} HTTP/1.1

PUT {{HostAddress}}/user/rental/{{TestBicycleId}}/180 HTTP/1.1

PUT {{HostAddress}}/user/return/C972DDDF-2288-4D7D-8F19-2845D699B605/{{TestBicycleId}} HTTP/1.1

```