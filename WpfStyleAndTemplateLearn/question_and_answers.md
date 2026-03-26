# WpfStyleAndTemplateLearn Question And Answers

## 1. VS Code에서 F5로 이 프로젝트가 실행되게 하려면?

질문:
VS Code에서 F5를 눌렀을 때 `WpfStyleAndTemplateLearn`이 실행되도록 하려면 어떻게 해야 하는가?

답변:
솔루션 파일의 프로젝트 순서로 실행 대상이 결정되지는 않는다.
VS Code의 디버그 설정인 `.vscode/launch.json`과 `.vscode/tasks.json`이 어떤 프로젝트를 빌드하고 실행할지 결정한다.
따라서 `program`, `cwd`, `preLaunchTask`를 `WpfStyleAndTemplateLearn.csproj` 기준으로 설정해야 한다.

## 2. App.xaml에 전역 스타일을 두면 왜 모든 윈도우에 적용되나?

질문:
`App.xaml` 파일에 `TextBlock`, `TextBox` 공통 스타일을 정의하면 왜 모든 윈도우에 자동 적용되는가?

답변:
`Application.Resources` 안에 `x:Key` 없이 `TargetType`만 지정한 `Style`은 암시적 스타일이다.
이 스타일은 앱 범위 전체에서 해당 타입의 컨트롤에 기본 스타일처럼 적용된다.
그래서 각 윈도우에서 별도로 `Style`을 지정하지 않아도 자동으로 적용된다.

## 3. ControlTemplate은 기본값을 유지하다가 IsChecked일 때만 바뀌는가?

질문:
`ToggleButton`의 `ControlTemplate`은 기본 모양을 유지하다가 `IsChecked=True`일 때만 바뀌는 것인가?

답변:
맞다.
템플릿 안에서 기본 배경, 기본 아이콘, 기본 테두리 등을 먼저 정의하고,
`ControlTemplate.Triggers`의 `Trigger Property="IsChecked" Value="True"`에서 필요한 속성만 덮어쓴다.
체크가 해제되면 다시 기본값으로 돌아간다.

## 4. ListBox 항목을 클릭하면 파란 배경이 되는 코드는 어디에 있나?

질문:
`ListBox` 항목을 클릭하면 파란색 배경이 되는 관련 코드는 어디에 있는가?

답변:
프로젝트 안에는 직접 작성된 코드가 없다.
이 동작은 WPF 기본 테마의 `ListBoxItem` 기본 스타일과 기본 `ControlTemplate`에서 온다.
사용자가 항목을 클릭하면 내부적으로 해당 `ListBoxItem`의 `IsSelected`가 `True`가 되고,
기본 스타일이 선택 배경을 파란 계열로 표시한다.

## 5. ListBoxItem.IsSelected = true 코드는 어디에 있나?

질문:
`ListBoxItem.IsSelected = true` 코드는 어디에 있는가?

답변:
프로젝트 소스에는 없다.
사용자가 `ListBox` 항목을 클릭하면 WPF 프레임워크 내부에서 해당 `ListBoxItem`의 `IsSelected`를 자동으로 `True`로 바꾼다.
즉, 우리가 직접 작성한 코드가 아니라 `ListBox` 컨트롤의 기본 동작이다.

## 6. DataTemplate으로 이름과 이메일을 각각 다른 TextBlock에 표시하려면?

질문:
`User(Name, Email)` 컬렉션을 `ListBox`에 바인딩하고, `ListBox.ItemTemplate`으로 이름과 이메일을 각각 다른 `TextBlock`에 표시하려면 어떻게 하는가?

답변:
`User` 클래스를 만들고 `Name`, `Email` 속성을 둔다.
`ObservableCollection<User>` 컬렉션을 만든 다음 `DataContext`에 연결한다.
`ListBox.ItemsSource`를 컬렉션에 바인딩하고,
`ListBox.ItemTemplate` 안에 `DataTemplate`을 정의해서 첫 번째 `TextBlock`은 `Name`, 두 번째 `TextBlock`은 `Email`에 바인딩하면 된다.

## 7. DataContext = this 의 의미는 무엇인가?

질문:
`DataContext = this;` 의 의미는 무엇인가?

답변:
현재 `MainWindow` 객체 자신을 바인딩의 기본 데이터 원본으로 사용하겠다는 뜻이다.
그래서 XAML에서 `{Binding Users}`라고 쓰면 `MainWindow` 클래스의 `Users` 속성을 찾는다.
작은 예제에서는 단순하고 편하지만, 규모가 커지면 보통 별도의 `ViewModel` 객체를 `DataContext`로 둔다.

## 8. MVVM에서 Binding과 ViewModel의 차이는 무엇인가?

질문:
MVVM에서는 왜 `Binding` 대신 `ViewModel`을 쓰는가? 둘의 차이는 무엇인가?

답변:
둘은 대체 관계가 아니라 역할이 다르다.
`Binding`은 화면과 데이터를 연결하는 기술이고,
`ViewModel`은 그 바인딩이 바라볼 데이터와 상태, 명령을 담는 객체다.
즉 MVVM은 `Binding`을 없애는 구조가 아니라,
`Binding`의 대상 객체를 `Window` 자신 대신 `ViewModel`로 분리하는 구조다.
이렇게 하면 역할 분리, 테스트 용이성, 유지보수성이 좋아진다.