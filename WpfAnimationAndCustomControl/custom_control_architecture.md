# RectangleProgressBar 커스텀 컨트롤 아키텍처

## 전체 연동 다이어그램

```
┌─────────────────────────────────────────────────────────────────────────────┐
│  MainWindow.xaml  (View 계층 — 컨트롤 배치 & 바인딩 선언)                      │
│                                                                             │
│   ┌──────────────────┐        ElementName Binding          ┌─────────────┐ │
│   │  Slider           │ ──────────────────────────────────▶ │ Rectangle   │ │
│   │  x:Name=          │   Value="{Binding                   │ ProgressBar │ │
│   │  "ValueSlider"    │    ElementName=ValueSlider,         │ x:Name=     │ │
│   │                   │    Path=Value}"                     │ "Custom     │ │
│   │  Value: 0~100     │                                     │  ProgressBar│ │
│   └──────────────────┘                                     │"            │ │
│           │                                                 └──────┬──────┘ │
│           │                                                        │        │
│           ▼                                                        │        │
│   ┌──────────────────┐                                             │        │
│   │  TextBlock        │  Text="{Binding ElementName=               │        │
│   │  (퍼센트 표시)     │   ValueSlider, Path=Value,                 │        │
│   │                   │   StringFormat={}{0:F0}%}"                  │        │
│   └──────────────────┘                                             │        │
│                                                                    │        │
│   ┌──────────────────┐   Click → Code-Behind에서                    │        │
│   │ Blue/Green/Red    │   CustomProgressBar.BarBrush 직접 설정  ────┘        │
│   │ 색상 변경 버튼들   │                                                      │
│   └──────────────────┘                                                      │
└─────────────────────────────────────────────────────────────────────────────┘
```

---

## 컨트롤 초기화 & 템플릿 로딩 흐름

```
 앱 시작
   │
   ▼
 ① AssemblyInfo.cs
    [assembly: ThemeInfo(..., ResourceDictionaryLocation.SourceAssembly)]
    └─ "이 어셈블리의 Generic.xaml에서 기본 테마 스타일을 찾아라" 라고 WPF에 알림
   │
   ▼
 ② RectangleProgressBar 정적 생성자
    DefaultStyleKeyProperty.OverrideMetadata(typeof(RectangleProgressBar), ...)
    └─ WPF에게 "내 기본 스타일 키는 RectangleProgressBar 타입이야" 라고 등록
   │
   ▼
 ③ WPF 런타임이 Themes/Generic.xaml 자동 탐색
    └─ TargetType="{x:Type controls:RectangleProgressBar}" 인 Style 발견
    └─ 해당 Style의 ControlTemplate을 컨트롤에 적용
   │
   ▼
 ④ OnApplyTemplate() 호출
    └─ GetTemplateChild("PART_Fill") 로 템플릿 내부의 채움 Border를 가져옴
    └─ _fillElement 에 저장
    └─ UpdateFillWidth() 최초 호출 → 초기 너비 설정
```

---

## Value 변경 시 데이터 흐름 (Slider → ProgressBar)

```
 사용자가 Slider 드래그
   │
   ▼
 ① ValueSlider.Value 변경 (예: 35 → 70)
   │
   │  ElementName Binding (OneWay 기본)
   ▼
 ② RectangleProgressBar.Value DP에 새 값 도착
   │
   ▼
 ③ CoerceValue 콜백 실행
   │  └─ Math.Clamp(value, 0, 100) → 범위 보정
   │
   ▼
 ④ OnValueChanged 콜백 실행
   │  └─ UpdateFillWidth() 호출
   │
   ▼
 ⑤ UpdateFillWidth()
   │  ratio = Value / 100.0          (예: 70 / 100 = 0.7)
   │  _fillElement.Width = ActualWidth * ratio
   │  └─ 컨트롤 전체 너비가 500px이면 → PART_Fill.Width = 350px
   │
   ▼
 ⑥ WPF 렌더링 엔진이 PART_Fill Border의 너비 변경을 감지
    └─ 화면에 채움 막대가 즉시 다시 그려짐
```

---

## Generic.xaml ControlTemplate 구조

```
 ControlTemplate (TargetType = RectangleProgressBar)
 │
 └─ Border (배경 트랙)
      ├─ Background ← {TemplateBinding Background}   ← "#E5E7EB" (회색)
      ├─ CornerRadius = 6
      ├─ ClipToBounds = True   ← 채움이 배경 밖으로 넘치지 않도록
      │
      └─ Border  x:Name="PART_Fill" (채움 바)
           ├─ HorizontalAlignment = Left
           ├─ Background ← {TemplateBinding BarBrush}  ← "#3B82F6" (파란색)
           ├─ CornerRadius = 6
           └─ Width = 0  (초기값, 이후 코드에서 동적으로 조절)
                  ▲
                  │
                  └─ UpdateFillWidth() 가 이 값을 변경
```

---

## BarBrush 색상 변경 흐름 (버튼 클릭)

```
 사용자가 "Green" 버튼 클릭
   │
   ▼
 ① MainWindow.xaml.cs → OnGreenClick 이벤트 핸들러
   │  CustomProgressBar.BarBrush = new SolidColorBrush(#22C55E)
   │
   ▼
 ② BarBrush 의존성 속성 값 변경
   │
   ▼
 ③ ControlTemplate의 {TemplateBinding BarBrush}
   │  └─ PART_Fill의 Background가 새 Brush로 업데이트
   │
   ▼
 ④ WPF 렌더링 엔진이 자동으로 다시 그림
    └─ 채움 막대 색상이 파란색 → 초록색으로 변경
```

---

## 파일별 역할 요약

| 파일 | 역할 | 핵심 내용 |
|------|------|-----------|
| **AssemblyInfo.cs** | 테마 리소스 위치 선언 | `ResourceDictionaryLocation.SourceAssembly` → WPF가 `Themes/Generic.xaml`을 자동 탐색하도록 지시 |
| **Themes/Generic.xaml** | 기본 **ControlTemplate** 정의 | 배경 Border(트랙) + `PART_Fill` Border(채움 바), `TemplateBinding`으로 속성 연결 |
| **Controls/RectangleProgressBar.cs** | **CustomControl 본체** | `Control` 상속, `Value`/`BarBrush` 의존성 속성, `OnApplyTemplate`에서 `PART_Fill` 참조, `UpdateFillWidth()`로 너비 계산 |
| **MainWindow.xaml** | **UI 조합 & 바인딩** | `Slider` ↔ `RectangleProgressBar.Value` ElementName 바인딩, 색상 변경 버튼 배치 |
| **MainWindow.xaml.cs** | **코드비하인드** | 색상 버튼 클릭 → `BarBrush` 속성 직접 설정 |

---

## CustomControl vs UserControl 핵심 차이 (이 프로젝트 기준)

```
 CustomControl (RectangleProgressBar)          UserControl
 ──────────────────────────────────            ──────────────────
 ✅ Control을 직접 상속                        ✅ UserControl 상속
 ✅ 외형을 Generic.xaml에서 분리 정의           ❌ XAML이 코드와 결합
 ✅ ControlTemplate 교체로 완전한 외형 변경 가능  ❌ 외형 변경 제한적
 ✅ PART_xxx 규약으로 템플릿과 로직 연결         ❌ x:Name으로 직접 참조
 ✅ 재사용·배포에 최적                          ✅ 빠른 프로토타이핑에 적합
```

---

## 의존성 속성(Dependency Property) 동작 상세

```
                     ┌────────────────────────────┐
                     │   WPF Property System       │
                     │                             │
  SetValue(ValueDP)──▶  ① Validate (없음)          │
                     │  ② CoerceValue              │
                     │     └─ Math.Clamp(0~100)    │
                     │  ③ 값이 실제로 바뀌었나?      │
                     │     ├─ 아니오 → 종료          │
                     │     └─ 예 ↓                  │
                     │  ④ OnValueChanged 콜백       │
                     │     └─ UpdateFillWidth()     │
                     │  ⑤ AffectsRender 플래그      │
                     │     └─ InvalidateVisual()    │
                     └────────────────────────────┘
```
