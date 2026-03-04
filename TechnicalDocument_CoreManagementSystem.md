# [기술 문서] 중앙 관리 및 오브젝트 풀링 시스템 (Core Management & Pooling)

## 1. 시스템 개요 (System Overview)

* **기능 도입 배경:** 
  디펜스 게임 특성상 대량의 유닛과 투사체가 빈번하게 생성되고 파괴됩니다. `Instantiate`와 `Destroy`의 반복적인 호출로 인한 성능 저하와 가비지 컬렉션(GC) 부하를 관리하기 위해 오브젝트 풀링과 이를 통제하는 중앙 관리 시스템을 설계했습니다.
  
* **구현 목표:**
  * **성능 안정성 확보:** 자원 재사용을 통해 프레임 저하를 완화하고 일관된 플레이 환경 제공.
  * **중앙 집중형 관리:** 싱글톤 기반 매니저를 통해 객체 생명주기를 일관되게 관리.
  * **코드 재사용성:** Generic 기반 설계를 도입하여 다양한 객체 유형에 대해 동일한 풀링 로직 적용.

## 2. 시스템 구조 (Architecture)

싱글톤 매니저가 하위 생성기들을 통제하고, 각 생성기는 전용 오브젝트 풀을 통해 객체를 관리하는 계층 구조입니다.

```mermaid
classDiagram
    class SingletonBase_T_ { <<Generic>> +Instance }
    class GeneratorBase_T_ { <<Generic>> }
    class ObjectPool_T_ { +Get(), +Release() }

    SingletonBase_T_ <|-- GameManager
    SingletonBase_T_ <|-- GeneratorManager
    
    GeneratorBase_T_ <|-- EnemyGenerator
    GeneratorBase_T_ <|-- ArrowGenerator
    
    GeneratorManager o-- EnemyGenerator
    GeneratorManager o-- ArrowGenerator
    EnemyGenerator o-- ObjectPool_T_
```

**[구조 상세 설명]**
*   **중앙 관리 (Centralized Control):** `GeneratorManager`는 씬(Scene) 내의 모든 객체 생성 활동을 감독하며, `EnemyGenerator`, `ArrowGenerator` 등 구체적인 생성 로직을 캡슐화한 하위 매니저들을 관리합니다.
*   **Generic 싱글톤:** `SingletonBase<T>`를 통해 반복되는 싱글톤 구현 코드를 줄이고, 어디서든 전역 매니저에 안전하게 접근할 수 있는 인터페이스를 제공합니다.
*   **객체 재활용 계층:** 각 생성기는 내부적으로 `ObjectPool<T>`을 소유하여 특정 타입에 특화된 객체 재활용 및 초기화 로직을 독립적으로 수행합니다.

## 3. 핵심 기능 및 동작 방식 (Core Logic)

### [Generic 싱글톤 및 풀링 관리]
반복되는 관리 로직을 Generic 클래스로 템플릿화하여 구현 오류를 줄이고, `GeneratorManager`가 게임 내 모든 동적 객체의 상태를 관제합니다.

```csharp
// 오브젝트 풀링 적용 로직
public class ArrowGenerator : GeneratorBase<Arrow>
{
    private ObjectPool<Arrow> _pool; // 유니티 내장 ObjectPool 활용

    public Arrow GetArrow()
    {
        var arrow = _pool.Get(); // 풀에서 객체 확보
        arrow.OnObjectSpawn();   // 초기화 인터페이스 호출
        return arrow;
    }
}
```

## 4. 고민과 선택 (Trade-offs)

구현 과정에서 **'상속을 통한 통합 관리'**와 **'인터페이스를 통한 기능 분리'**의 효율성을 비교하였습니다.

| 비교 항목 | A안: 상속 기반 (Base Class) | B안: 인터페이스 기반 (Interface) |
| :--- | :--- | :--- |
| **객체 제어** | 베이스 매니저 클래스에서 모든 객체의 생명주기를 강제로 제어함. | `IPoolable` 등의 인터페이스를 통해 객체 스스로가 자신의 초기화 및 해제 로직을 정의함. |
| **확장성** | 새로운 매니저 추가 시 정해진 상속 구조를 따라야 하므로 유연성이 떨어짐. | 인터페이스를 통해 다양한 형태의 매니저나 객체 풀을 독립적으로 확장 가능함. |
| **의존성** | 매니저와 관리 대상 객체 간의 클래스 수준 결합도가 높음. | 인터페이스를 매개로 상호작용하여 시스템 간의 의존성을 최소화함. |

*   **선택 이유:** **B안(인터페이스 기반)**을 핵심 설계 원칙으로 삼았습니다. 특히 오브젝트 풀링 시스템에서 각 객체가 생성/해제될 때 수행해야 할 고유한 로직을 인터페이스(`IPoolable`)로 분리함으로써, 중앙 매니저가 객체의 세부 타입을 몰라도 안전하게 상태를 관리할 수 있도록 구현했습니다.

## 5. 회고 (Retrospective)

* **문제 인식:** 
  현재 정적으로 관리되는 풀 크기는 예상치를 초과하는 대량의 객체 발생 시 유연하게 대응하기 어려울 수 있으며, 씬 로딩 직후 초기 생성 시점에 일시적인 지연이 발생할 수 있습니다.

* **향후 개선 방안:** 
  1. **동적 확장 로직:** 런타임 상황에 맞춰 풀의 크기를 자동으로 조절하는 기능을 보완할 예정입니다.
  2. **비동기 사전 생성:** 로딩 단계에서 `async/await`을 활용하여 객체를 비동기적으로 미리 생성(Pre-warm)함으로써 초기 지연을 최소화할 계획입니다.
