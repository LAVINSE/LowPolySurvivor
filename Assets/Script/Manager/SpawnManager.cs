using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eStageEventType
{
    None,
    Rain,
    Poison,
    Fog,
    ItemPickRangeBuff,
    MaxValue,
}

public class SpawnManager : MonoBehaviour
{
    #region 변수
    [Header("=====> 스테이지 데이터 <=====")]
    [SerializeField] private StageSO stageSO;

    [Header("=====> 몬스터 소환 위치 <=====")]
    [SerializeField] private Transform playerPos;
    [SerializeField] private float correctPosY = 0.2f; // 보정값
    [SerializeField] private float spawnRadius = 30; // 소환 범위
    [SerializeField] private float dectectRange = 1.5f; // 건물 감지 범위
    [SerializeField] private float minSpawnRadius = 15f; // 플레이어와의 거리 최소값
    [SerializeField] private string tagName = "NoSpawn"; // 건물

    [Header("=====> 소환 설정 <=====")]
    [SerializeField] private int maxSpawnCount = 100;
    [SerializeField] private float spawnRate = 1f; // 소환 시간?
    [SerializeField] private float stageTimer = 360f;

    [Header("=====> 자연재해 선택 버튼 <=====")]
    [SerializeField] private GameObject stageEventObject;
    [SerializeField] private SelectEventButtonUI selectEventButtonUI_1;
    [SerializeField] private SelectEventButtonUI selectEventButtonUI_2;
    [SerializeField] private SelectEventButtonUI selectEventButtonUI_3;

    [Header("=====> 자연재해 <=====")]
    [SerializeField] private List<EventDataSO> eventList = new List<EventDataSO>();

    [Header("=====> 보스 몹 <=====")]
    [SerializeField] private GameObject bossPrefab;

    private List<float> stageEventTimerList = new List<float>();
    private List<int> randomIndexList = new List<int>();

    private bool isSpawnBoss = false;
    private Enemy bossEnemy;
    #endregion // 변수

    #region 프로퍼티
    public PlayerMain PlayerMain { get; set; }
    public PlayerStatusEffect PlayerStatusEffect { get; set; }

    public int StageLevel { get; set; } = 0;
    public int SpawnCount { get; set; } = 0;

    public Dictionary<eStageEventType, bool> stageEventDict = new Dictionary<eStageEventType, bool>();
    public List<EventDataSO> EventDataSOList { get; set; } = new List<EventDataSO>();
    #endregion // 프로퍼티

    #region 함수
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(playerPos.position, spawnRadius);

        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(playerPos.position, minSpawnRadius);
    }

    /** 초기화 */
    private void Awake()
    {
        for(int i = 0; i < 7; i++)
        {
            stageEventTimerList.Add(stageTimer - ((stageTimer/6) * i));
        }

        StageLevel = 0;

        for (int i = (int)eStageEventType.None; i < (int)eStageEventType.MaxValue; i++)
        {
            stageEventDict.Add((eStageEventType)i, false);
        }

        stageEventDict.Remove(eStageEventType.None);
    }

    /** 초기화 */
    private void Start()
    {
        playerPos = PlayerMain.transform;

        StartCoroutine(StageCO());
        StartCoroutine(StageEventCO());
    }

    /** 소환할 위치를 찾는다 */
    private Vector3 FindSpawnPosition()
    {
        // 시도 횟수 제한
        int attempts = 5;

        Vector3 spawnPosition = Vector3.zero;
        Vector3 playerPosition = playerPos.position;

        while (attempts > 0)
        {
            // 플레이어 주변 랜덤 위치 계산, 소환될 높이는 지정
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * spawnRadius;
            randomDirection += playerPosition;
            randomDirection.y = playerPosition.y + correctPosY;

            // 플레이어와의 거리 계산
            float distance = Vector3.Distance(randomDirection, playerPosition);

            // 바닥이 있는지 확인
            if (IsGrounded(randomDirection))
            {
                // 건물이 주변에 있는지 확인
                if (distance >= minSpawnRadius && !BuildingCheck(randomDirection))
                {
                    // 위치 반환
                    spawnPosition = randomDirection;
                    break;
                }
            }

            attempts--;
        }

        // Vector3.zero
        return spawnPosition;
    }

    /** 주어진 위치가 바닥에 있는지 확인 */
    private bool IsGrounded(Vector3 position)
    {
        // 아래쪽으로 레이캐스트를 쏴서 바닥 검사
        if (Physics.Raycast(position, Vector3.down, out RaycastHit hit, 1f))
        {
            // 바닥이 발견되었는지 확인
            return hit.collider.CompareTag("Ground");
        }

        return false;
    }

    /** 적을 소환한다 */
    private Enemy SpawnEnemy(Vector3 spawnPos)
    {
        int randomIndex = UnityEngine.Random.Range((int)PoolEnemyType.Beholder, (int)PoolEnemyType.Turtle + 1);
        GameObject enemy = GameManager.Instance.PoolManager.GetEnemy(randomIndex, spawnPos);
        enemy.GetComponent<Enemy>().Init(playerPos.GetComponent<PlayerMain>(), StageLevel);
        return enemy.GetComponent<Enemy>();
    }

    /** 적을 소환한다 */
    private Enemy SpawnBossEnemy(Vector3 spawnPos)
    {
        GameObject enemy = Instantiate(bossPrefab);
        enemy.transform.position = spawnPos;
        enemy.GetComponent<Enemy>().Init(playerPos.GetComponent<PlayerMain>(), 0);
        return enemy.GetComponent<Enemy>();
    }

    /** 주변에 건물이 있는지 확인한다 */
    private bool BuildingCheck(Vector3 pos)
    {
        // 주어진 위치 주변에 건물이 있는지 확인
        Collider[] colliders = Physics.OverlapSphere(pos, dectectRange);

        foreach (Collider collider in colliders)
        {
            // 건물의 태그를 "Building"으로 가정하고 사용
            if (collider.CompareTag(tagName))
            {
                // 건물이 발견됨
                return true; 
            }
        }

        // 주변에 건물이 없음
        return false; 
    }

    /** 랜덤으로 이벤트 3개를 고른다 */
    private bool RandomSelectEvent()
    {
        randomIndexList.Clear();
        EventDataSOList.Clear();

        int falseCount = 0;

        foreach(var count in stageEventDict)
        {
            if (count.Value == false)
            {
                falseCount++;
            }
        }

        if(falseCount < 3)
        {
            return false;
        }

        while (randomIndexList.Count < 3)
        {
            int randomIndex = UnityEngine.Random.Range((int)eStageEventType.None + 1, (int)eStageEventType.MaxValue);

            // stageEventDict가 true이면 해당 인덱스를 제외하고 다시 랜덤으로 선택
            if (!randomIndexList.Contains(randomIndex) && (!stageEventDict.ContainsKey((eStageEventType)randomIndex)
                || !stageEventDict[(eStageEventType)randomIndex]))
            {
                randomIndexList.Add(randomIndex);
            }
        }

        for (int i = 0; i < eventList.Count; i++)
        {
            for (int k = 0; k < randomIndexList.Count; k++)
            {
                if (eventList[i].stageEventType == (eStageEventType)randomIndexList[k])
                {
                    
                    EventDataSOList.Add(eventList[i]);
                }
            }
        }

        return true;
    }

    /** 자연재해 버튼 기본설정을 한다 */
    private void InitButton()
    {
        selectEventButtonUI_1.EventDataSO = EventDataSOList[0];
        selectEventButtonUI_1.Init();
        selectEventButtonUI_1.Button.onClick.AddListener(() => 
        {
            StageEventStart(selectEventButtonUI_1.EventDataSO);
            StageEventTimeScaleStart();
        });

        selectEventButtonUI_2.EventDataSO = EventDataSOList[1];
        selectEventButtonUI_2.Init();
        selectEventButtonUI_2.Button.onClick.AddListener(() =>
        {
            StageEventStart(selectEventButtonUI_2.EventDataSO);
            StageEventTimeScaleStart();
        });

        selectEventButtonUI_3.EventDataSO = EventDataSOList[2];
        selectEventButtonUI_3.Init();
        selectEventButtonUI_3.Button.onClick.AddListener(() =>
        {
            StageEventStart(selectEventButtonUI_3.EventDataSO);
            StageEventTimeScaleStart();
        });
    }

    /** 스테이지 이벤트를 실행한다 */
    private void StageEventStart(EventDataSO eventDataSO)
    {
        if(!stageEventDict.ContainsKey(eventDataSO.stageEventType)) { return; }

        if (stageEventDict.ContainsKey(eventDataSO.stageEventType))
        {
            if(stageEventDict[eventDataSO.stageEventType] == true)
            {
                return;
            }
        }

        switch (eventDataSO.stageEventType)
        {
            case eStageEventType.Rain:
                CreateStageEvent(eventDataSO, 20f);
                break;
            case eStageEventType.Poison:
                CreateStageEvent(eventDataSO, 3f);
                break;
            case eStageEventType.Fog:
                CreateStageEvent(eventDataSO, 3f);
                break;
            case eStageEventType.ItemPickRangeBuff:
                ItemPickRangeBuff(eventDataSO);
                break;
        }

        stageEventDict[eventDataSO.stageEventType] = true;
    }

    /** 스테이지 이벤트 선택창을 활성화한다 */
    private void ShowStageEventSelect()
    {
        if(RandomSelectEvent() == false) { Time.timeScale = 1; return; }

        InitButton();

        stageEventObject.SetActive(true);
    }

    /** 일시정시 해제 */
    public void StageEventTimeScaleStart()
    {
        Time.timeScale = 1;
        stageEventObject.SetActive(false);
    }

    /** 자연재해를 생성한다 */
    private void CreateStageEvent(EventDataSO eventDataSO, float posY)
    {
        float x = UnityEngine.Random.Range(stageSO.minPos.x, stageSO.maxPos.x);
        float z = UnityEngine.Random.Range(stageSO.minPos.z, stageSO.maxPos.z);

        Vector3 spawnPos = new Vector3(x, posY, z);

        GameObject rainObject = Instantiate(eventDataSO.prefab);
        rainObject.GetComponent<EventAttack>().PlayerMain = PlayerMain;
        rainObject.transform.position = spawnPos;
    }

    /** 버프를 사용한다 */
    private void ItemPickRangeBuff(EventDataSO eventDataSO)
    {
        if(PlayerStatusEffect == null) { return; }
        if(PlayerMain == null) { return; }

        PlayerStatusEffect.ApplyBuff(new StatusEffectItemPickRange(eventDataSO.duration, eventDataSO.delay, PlayerMain,
            eventDataSO.creaseValue));
    }
    #endregion // 함수

    #region 코루틴
    /** 스테이지 소환 */
    private IEnumerator StageCO()
    {
        while (true)
        {
            yield return new WaitUntil(() => SpawnCount < maxSpawnCount);

            // 플레이어 주변에서 소환 위치를 찾아 소환
            Vector3 spawnPosition = FindSpawnPosition();

            // Vector3.zero가 아닐 경우
            if (spawnPosition != Vector3.zero)
            {
                SpawnEnemy(spawnPosition);
                SpawnCount++;
                yield return new WaitForSeconds(spawnRate);
            }
        }
    }

    /** 스테이지 이벤트 타이머 */
    private IEnumerator StageEventCO()
    {
        while (true)
        {
            stageTimer -= Time.deltaTime;
            GameManager.Instance.InGameUI.UpdateTimerText(stageTimer);

            // 스테이지 이벤트 처리
            for (int i = 1; i <= 6; i++)
            {
                if (stageEventTimerList[i] >= stageTimer && StageLevel < i)
                {
                    StageLevel = i;
                    Time.timeScale = 0;

                    // 선택창 실행
                    ShowStageEventSelect();

                    yield return null;

                    break; // 선택 창 실행 후에는 루프를 빠져나옴
                }
            }

            if (StageLevel == 6)
            {
                while (isSpawnBoss == false)
                {
                    // 플레이어 주변에서 소환 위치를 찾아 소환
                    Vector3 spawnPosition = FindSpawnPosition();

                    // Vector3.zero가 아닐 경우
                    if (spawnPosition != Vector3.zero)
                    {
                        bossEnemy = SpawnBossEnemy(spawnPosition);
                        GameManager.Instance.InGameUI.ActiveBossHpbar(true);
                        AudioManager.Inst.PlaySFX("BossSpawnSFX");
                        isSpawnBoss = true;
                        yield break;
                    }
                }   
            }

            yield return null; // 다음 프레임까지 대기
        }
    }
    #endregion // 코루틴
}

