using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum eStageEventType
{
    None,
    Rain,
    Poison,
    Fog,
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
    [SerializeField] private float spawnRadius; // 소환 범위
    [SerializeField] private float dectectRange = 1f; // 감지 범위
    [SerializeField] private float minSpawnRadius = 3f; // 플레이어와의 거리 최소값
    [SerializeField] private string tagName = "Building";

    [Header("=====> 소환 설정 <=====")]
    [SerializeField] private int maxSpawnCount = 0;
    [SerializeField] private float spawnRate = 1f; // 소환 시간?
    [SerializeField] private float stageTimer = 360f;

    [Header("=====> 자연재해 선택 버튼 <=====")]
    [SerializeField] private GameObject stageEventObject;
    [SerializeField] private SelectEventButtonUI selectEventButtonUI_1;
    [SerializeField] private SelectEventButtonUI selectEventButtonUI_2;
    [SerializeField] private SelectEventButtonUI selectEventButtonUI_3;

    [Header("=====> 자연재해 <=====")]
    [SerializeField] private GameObject rainPrefab;

    private List<float> stageEventTimerList = new List<float>();

    private eStageEventType stageEventType = eStageEventType.None;

    private List<int> randomIndexList = new List<int>();
    #endregion // 변수

    #region 프로퍼티
    public PlayerMain PlayerMain { get; set; }
    public int StageLevel { get; set; } = 0;
    public int SpawnCount { get; set; } = 0;

    public Dictionary<eStageEventType, bool> stageEventDict = new Dictionary<eStageEventType, bool>();
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
            stageEventTimerList.Add(stageTimer - (60 * i));
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
        StartCoroutine(StageCO());
    }

    /** 테스트 코드 */
    private IEnumerator StageCO()
    {
        while (true)
        {
            yield return new WaitUntil(() => SpawnCount < maxSpawnCount);

            // 플레이어 주변에서 소환 위치를 찾아 소환
            Vector3 spawnPosition = FindSpawnPosition();

            stageTimer -= Time.deltaTime;

            // Vector3.zero가 아닐 경우
            if (spawnPosition != Vector3.zero)
            {
                SpawnEnemy(spawnPosition);
                SpawnCount++;
                yield return new WaitForSeconds(spawnRate);

                // 스테이지 이벤트 처리
                for (int i = 1; i <= 6; i++)
                {
                    if (stageEventTimerList[i] >= stageTimer && StageLevel < i)
                    {
                        StageLevel = i;
                        Time.timeScale = 0;

                        // 선택창 실행
                        ShowStageEventSelect();

                        break; // 선택 창 실행 후에는 루프를 빠져나옴
                    }
                }

                if(StageLevel == 6)
                {
                    // 보스소환
                }
            }
        }
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
            Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
            randomDirection += playerPosition;
            randomDirection.y = playerPosition.y + correctPosY;

            // 플레이어와의 거리 계산
            float distance = Vector3.Distance(randomDirection, playerPosition);

            // 건물이 주변에 있는지 확인
            if (distance >= minSpawnRadius && !BuildingCheck(randomDirection))
            {
                // 위치 반환
                spawnPosition = randomDirection;
                break;
            }

            attempts--;
        }

        // Vector3.zero
        return spawnPosition;
    }

    /** 적을 소환한다 */
    private Enemy SpawnEnemy(Vector3 spawnPos)
    {
        GameObject enemy = GameManager.Instance.PoolManager.GetEnemy(0, spawnPos);
        enemy.GetComponent<Enemy>().Player = playerPos.GetComponent<PlayerMain>();
        enemy.GetComponent<Enemy>().SpawnManager = this;
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

    /** 스테이지 이벤트 선택창을 활성화/비활성화 한다 */
    private void ShowStageEventSelect()
    {
        stageEventObject.SetActive(!stageEventObject.activeSelf);

    }

    /** 스테이지 이벤트를 실행한다 */
    private void StageEventStart(eStageEventType stageEventType)
    {
        if(!stageEventDict.ContainsKey(stageEventType)) { return; }

        if (stageEventDict.ContainsKey(stageEventType))
        {
            if(stageEventDict[stageEventType] == true)
            {
                return;
            }
        }

        switch (stageEventType)
        {
            case eStageEventType.Rain:
                StageEventRain();
                break;
        }

        stageEventDict[stageEventType] = true;
    }

    /** 랜덤으로 이벤트 3개를 고른다 */
    private void RandomSelectEvent()
    {
        while(randomIndexList.Count < 3)
        {
            int randomIndex = Random.Range((int)eStageEventType.None + 1, (int)eStageEventType.MaxValue);

            if (!randomIndexList.Contains(randomIndex))
            {
                randomIndexList.Add(randomIndex);
            }
        }
    }

    /** 자연재해 "비"를 생성한다 */
    private void StageEventRain()
    {
        float x = Random.Range(stageSO.minPos.x, stageSO.maxPos.x);
        float z = Random.Range(stageSO.minPos.z, stageSO.maxPos.z);
        float y = 20f;

        Vector3 spawnPos = new Vector3(x, y, z);

        GameObject rainObject = Instantiate(rainPrefab);
        rainObject.transform.position = spawnPos;
    }

    /** 일시정시 해제 */
    public void TimeScaleStart()
    {
        Time.timeScale = 1;
    }
    #endregion // 함수
}

