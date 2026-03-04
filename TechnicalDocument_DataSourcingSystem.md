# [기술 문서] ScriptableObject 기반 데이터 주도 설계 및 관리 (Data & Prefab Management)

## 1. 시스템 개요 (System Overview)

* **기능 도입 배경:** 
  게임의 밸런스 데이터와 에셋 참조를 코드에서 분리하여 관리 효율을 높이기 위한 시스템입니다. 기획자가 코드 수정 없이 인스펙터 환경에서 수치를 조정하고 결과를 확인할 수 있는 환경을 마련하여 협업 생산성을 향상시키고자 했습니다.
  
* **구현 목표:**
  * **데이터 의존성 분리:** 수치 및 리소스 설정을 코드와 분리하여 관리의 편의성 확보.
  * **메모리 사용 최적화:** 동일한 데이터를 공유하는 인스턴스들이 단일 데이터 에셋(SO)을 참조하도록 설계.
  * **에셋 관리 일관성:** 프리팹 및 관련 리소스를 중앙 데이터베이스 형태로 구조화.

## 2. 시스템 구조 (Architecture)

모든 데이터는 `PrefabSoBase`를 상속받은 `ScriptableObject` 에셋 형태로 저장되며, 필요한 객체에서 이를 참조합니다.

```mermaid
classDiagram
    class PrefabSoBase { <<ScriptableObject>> +string Name, +string Description }
    class AbilityDataSO { +float[] value, +float coolTime }
    class EnemyPrefabSO { +Enemy Prefab, +EnemyStats defaultStats }
    class WaveDataSO { +WaveInfo[] waves }

    PrefabSoBase <|-- AbilityDataSO
    PrefabSoBase <|-- EnemyPrefabSO
    PrefabSoBase <|-- WaveDataSO
    PrefabSoBase <|-- ArrowPrefabSO
```

**[구조 상세 설명]**
*   **데이터 원본 관리:** 모든 리소스 데이터의 최상위 클래스인 `PrefabSoBase`를 통해 이름, 설명 등 공통 필드를 일관되게 관리합니다.
*   **강한 형식의 데이터 컨테이너:** 능력(`AbilityDataSO`), 유닛(`EnemyPrefabSO`), 웨이브(`WaveDataSO`) 등 각 도메인에 특화된 SO 클래스를 설계하여 타입 안정성을 확보했습니다.
*   **에셋 참조의 직렬화:** 프리팹(GameObject), 스탯 데이터 등을 SO 내에 직접 포함시켜 유니티 엔진의 에셋 시스템을 통한 시각적인 관리가 가능하도록 했습니다.

## 3. 핵심 기능 및 동작 방식 (Core Logic)

### [데이터 참조 기반의 구조적 분리]
인스턴스화된 객체는 데이터 원본을 직접 소유하지 않고 SO를 참조합니다. 이를 통해 데이터 수정이 실시간으로 모든 객체에 적용되며, 메모리 효율성을 높일 수 있습니다.

```csharp
// 데이터 참조 활용 예시
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyPrefabSO _dataSO; // 데이터 에셋 할당
    private float _currentHp;

    void Start()
    {
        // 원본 데이터는 보존하고 런타임 상태값만 별도 관리
        _currentHp = _dataSO.defaultStats.hp; 
    }
}
```

## 4. 고민과 선택 (Trade-offs)

구현 과정에서 **'상속을 통한 통합 관리'**와 **'인터페이스를 통한 기능 분리'**의 효율성을 비교하였습니다.

| 비교 항목 | A안: 상속 기반 (Base Class) | B안: 인터페이스 기반 (Interface) |
| :--- | :--- | :--- |
| **데이터 구조** | 공통 필드를 부모 SO에서 관리하여 데이터 일관성을 유지하기 좋음. | 데이터 접근 방식을 인터페이스로 추상화하여 다양한 데이터 소스(CSV, SO, JSON)에 대응 가능. |
| **결합도** | 특정 데이터 클래스에 대한 직접적인 참조가 필요함. | 인터페이스를 통해 데이터 로드 및 제공 방식을 분리하여 시스템 간 결합도를 낮춤. |
| **유연성** | 상속 계층이 깊어질수록 데이터 구조 변경 시 수정 범위가 넓어짐. | 데이터 필드와 기능적 요구사항을 분리하여 기획 변경에 유연하게 대처 가능. |

*   **선택 이유:** **A안(상속 기반)**의 데이터 관리 편의성과 **B안(인터페이스 기반)**의 기능적 확장성을 조합했습니다. `PrefabSoBase` 상속을 통해 유니티 에셋 시스템과의 호환성을 유지하면서도, 데이터 접근 로직은 인터페이스로 추상화하여 향후 데이터 소스가 변경되더라도 시스템 전반에 미치는 영향을 최소화했습니다.

## 5. 회고 (Retrospective)

* **문제 인식:** 
  모든 데이터를 SO에 상주시킬 경우 데이터 규모에 따라 메모리 부담이 증가할 수 있으며, 런타임 중에 SO의 값을 직접 수정할 경우 원본 에셋이 변조될 위험이 있습니다.

* **향후 개선 방안:** 
  1. **Addressables 도입:** 필요한 데이터셋만 선별적으로 로드하여 메모리 관리를 고도화할 계획입니다.
  2. **복제본 생성 로직:** 런타임 중에 데이터 수정이 필요한 경우, 원본 SO의 인스턴스를 생성하여 사용하여 원본 데이터 오염을 방지할 예정입니다.
