# 프로젝트 개요

## 워크샵 목표
### .NET으로 할 수 있는 다양한 개발 방법을 체험해 보기

- MAUI로 모바일 앱 만들기
  - 모바일 푸시 받아보기
- 다양한 크로스 플랫폼 어플리케이션 개발 경험
  - Uno-Platform
  - Avalonia
- .NET Backend 개발
  - Aspire
  - Minimal API
  - MySql - Running on container environment

### 다음에 해보는 걸로
- Blaozr MAUI Hybrid
- IoT 기기 사용

## 워크샵 운영
### 진행 멤버
| 이름 | 역할 | 비고 |
|:--------:|:--------|:--------:|
| 김진석 | 세션진행자/스폰서 | Blazor, 커피상품권 |
| 박구삼 | 세션진행자 | Aspire backend |
| 이재웅 | 세션진행자 | Uno-Platform |
| 이광석 | 세션진행자 | Avalonia |
| 조장원 | 세션진행자 | MAUI |
| 유현아 | 스폰서 | 한빛앤(장소) |
| 조동수 | 스폰서 | 인프라지스틱스(점심) |

### 타임 테이블
| # | 시간 | 내용 | 비고 |
|:--:|:--:|:--|:--:|
|   | 10:00 - 10:20 | 오프닝 및 후원사 소개 | 김진석, 유현아 |
| 1 | 10:20 - 10:50 | [Blazor](./sessions/1.%20blazor/readme.md) | 김진석  |
| 2 | 11:00 - 12:00 | [Backend & Aspire](./sessions/2.%20backend%20and%20aspire/readme.md) | 박구삼 |
|   | 12:00 - 12:45 | 점심 시간 |   |
| 3 | 12:45 - 14:15 | [Uno-Platform](./sessions/3.%20uno-platform/readme.md) | 이재웅 |
| 4 | 14:25 - 15:25 | [Avalonia](./sessions/4.%20avalonia/readme.md) | 이광석  |
| 5 | 15:35 - 17:05 | [MAUI](./sessions/5.%20maui/readme.md) | 조장원  |
|   | 17:10 - 17:30 | 클로징, 경품 추첨, 기념사진 | 김진석, 조동수 |

## 워크샵 컨텐츠

### 공유 자전거 플랫폼

| 항목 | 내용 | 기능 |
|:--------:|:--------|:--------:|
| Blazor | 공유 자전거 플랫폼 홍보 광고 페이지 | 플랫폼 및 개발하는 내용에 대한 설명에 대한 페이지 |
| Aspire | DB 및 Backend | DB CRUD 및 정보 관리  |
| Uno-Platform | 관리자 페이지 | 대여소 및 자전거의 등록, 조회 |
| Avalonia | 키오스크 앱 | 현재 대여소 목록 표시, 대여소 내 자전거 상태 목록 표시 |
| MAUI | 사용자 앱 | 자전기 빌리기 및 반납, 반납 요청 알림 |

### 레포지토리 내용
sessions : 각 세션의 내용이 정리되어 있는 폴더입니다.   
>0.prerequisites : 워크샵 사전 준비 내용이 정리된 폴더입니다.   

>1.blazor : blazor 세션 폴더입니다.   
>> complete : 세션이 끝나면 완성될 코드를 저장합니다.   
>> start : 세션을 시작할 때 열 폴더를 저장합니다.   
>> readme.md : 세션 진행 설명이 있으면 됩니다.   

>2.backend and aspire : backend and aspire 세션 폴더입니다.   
>> complete : 세션이 끝나면 완성될 코드를 저장합니다.   
>> start : 세션을 시작할 때 열 폴더를 저장합니다.   
>> readme.md : 세션 진행 설명이 있으면 됩니다.   

>3.uno-platform : uno-platform 세션 폴더입니다.   
>> complete : 세션이 끝나면 완성될 코드를 저장합니다.   
>> start : 세션을 시작할 때 열 폴더를 저장합니다.   
>> readme.md : 세션 진행 설명이 있으면 됩니다.   

>4.avalonia : avalonia 세션 폴더입니다.   
>> complete : 세션이 끝나면 완성될 코드를 저장합니다.   
>> start : 세션을 시작할 때 열 폴더를 저장합니다.   
>> readme.md : 세션 진행 설명이 있으면 됩니다.   

>5.maui : maui 세션 폴더입니다.   
>> complete : 세션이 끝나면 완성될 코드를 저장합니다.   
>> start : 세션을 시작할 때 열 폴더를 저장합니다.   
>> readme.md : 세션 진행 설명이 있으면 됩니다.   

### 컨텐츠 요구사항 > 각 세션 페이지로 이동

* 공통
  * 최소한의 디자인과 최소한의 API만 이용(쉽게 알려주기 위한 목적)
  * 보안 및 신원검증 단계는 X
* Aspire
  * 대여소 CRUD 구성
  * 자전거 CRUD 구성
  * 자전거 대여/반납 구성
  * 자전거의 상태는 소속 대여소와 대여중 여부만 존재
* Uno-Platform
  * TBD
* Avalonia
  * TBD
* MAUI
  * TBD
