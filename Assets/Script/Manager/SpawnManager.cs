using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.PointerEventData;

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
    #region ����
    [Header("=====> �������� ������ <=====")]
    [SerializeField] private StageSO stageSO;

    [Header("=====> ���� ��ȯ ��ġ <=====")]
    [SerializeField] private Transform playerPos;
    [SerializeField] private float correctPosY = 0.2f; // ������
    [SerializeField] private float spawnRadius; // ��ȯ ����
    [SerializeField] private float dectectRange = 1f; // ���� ����
    [SerializeField] private float minSpawnRadius = 3f; // �÷��̾���� �Ÿ� �ּҰ�
    [SerializeField] private string tagName = "Building";

    [Header("=====> ��ȯ ���� <=====")]
    [SerializeField] private int maxSpawnCount = 100;
    [SerializeField] private float spawnRate = 1f; // ��ȯ �ð�?
    [SerializeField] private float stageTimer = 360f;

    [Header("=====> �ڿ����� ���� ��ư <=====")]
    [SerializeField] private GameObject stageEventObject;
    [SerializeField] private SelectEventButtonUI selectEventButtonUI_1;
    [SerializeField] private SelectEventButtonUI selectEventButtonUI_2;
    [SerializeField] private SelectEventButtonUI selectEventButtonUI_3;

    [Header("=====> �ڿ����� <=====")]
    [SerializeField] private List<EventDataSO> eventList = new List<EventDataSO>();

    private List<float> stageEventTimerList = new List<float>();
    private List<int> randomIndexList = new List<int>();
    private List<EventDataSO> selectEventList = new List<EventDataSO>();
    #endregion // ����

    #region ������Ƽ
    public PlayerMain PlayerMain { get; set; }
    public int StageLevel { get; set; } = 0;
    public int SpawnCount { get; set; } = 0;

    public Dictionary<eStageEventType, bool> stageEventDict = new Dictionary<eStageEventType, bool>();
    public List<EventDataSO> EventDataSOList { get; set; } = new List<EventDataSO>();
    #endregion // ������Ƽ

    #region �Լ�
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(playerPos.position, spawnRadius);

        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(playerPos.position, minSpawnRadius);
    }

    /** �ʱ�ȭ */
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

    /** �ʱ�ȭ */
    private void Start()
    {
        StartCoroutine(StageCO());
        StartCoroutine(StageEventCO());
    }

    /** ��ȯ�� ��ġ�� ã�´� */
    private Vector3 FindSpawnPosition()
    {
        // �õ� Ƚ�� ����
        int attempts = 5;

        Vector3 spawnPosition = Vector3.zero;
        Vector3 playerPosition = playerPos.position;
        
        while (attempts > 0)
        {
            // �÷��̾� �ֺ� ���� ��ġ ���, ��ȯ�� ���̴� ����
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * spawnRadius;
            randomDirection += playerPosition;
            randomDirection.y = playerPosition.y + correctPosY;

            // �÷��̾���� �Ÿ� ���
            float distance = Vector3.Distance(randomDirection, playerPosition);

            // �ǹ��� �ֺ��� �ִ��� Ȯ��
            if (distance >= minSpawnRadius && !BuildingCheck(randomDirection))
            {
                // ��ġ ��ȯ
                spawnPosition = randomDirection;
                break;
            }

            attempts--;
        }

        // Vector3.zero
        return spawnPosition;
    }

    /** ���� ��ȯ�Ѵ� */
    private Enemy SpawnEnemy(Vector3 spawnPos)
    {
        GameObject enemy = GameManager.Instance.PoolManager.GetEnemy(0, spawnPos);
        enemy.GetComponent<Enemy>().Player = playerPos.GetComponent<PlayerMain>();
        enemy.GetComponent<Enemy>().SpawnManager = this;
        return enemy.GetComponent<Enemy>();
    }

    /** �ֺ��� �ǹ��� �ִ��� Ȯ���Ѵ� */
    private bool BuildingCheck(Vector3 pos)
    {
        // �־��� ��ġ �ֺ��� �ǹ��� �ִ��� Ȯ��
        Collider[] colliders = Physics.OverlapSphere(pos, dectectRange);

        foreach (Collider collider in colliders)
        {
            // �ǹ��� �±׸� "Building"���� �����ϰ� ���
            if (collider.CompareTag(tagName))
            {
                // �ǹ��� �߰ߵ�
                return true; 
            }
        }

        // �ֺ��� �ǹ��� ����
        return false; 
    }

    /** �������� �̺�Ʈ 3���� ���� */
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

            // stageEventDict�� true�̸� �ش� �ε����� �����ϰ� �ٽ� �������� ����
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

    /** �ڿ����� ��ư �⺻������ �Ѵ� */
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

    /** �������� �̺�Ʈ�� �����Ѵ� */
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
                StageEventRain(eventDataSO);
                break;
            case eStageEventType.Poison:
                break;
            case eStageEventType.Fog:
                break;
        }

        stageEventDict[eventDataSO.stageEventType] = true;
    }

    /** �������� �̺�Ʈ ����â�� Ȱ��ȭ�Ѵ� */
    private void ShowStageEventSelect()
    {
        if(RandomSelectEvent() == false) { Time.timeScale = 1; return; }

        InitButton();

        stageEventObject.SetActive(true);
    }

    /** �Ͻ����� ���� */
    public void StageEventTimeScaleStart()
    {
        Time.timeScale = 1;
        stageEventObject.SetActive(false);
    }

    /** �ڿ����� "��"�� �����Ѵ� */
    private void StageEventRain(EventDataSO eventDataSO)
    {
        float x = UnityEngine.Random.Range(stageSO.minPos.x, stageSO.maxPos.x);
        float z = UnityEngine.Random.Range(stageSO.minPos.z, stageSO.maxPos.z);
        float y = 20f;

        Vector3 spawnPos = new Vector3(x, y, z);

        GameObject rainObject = Instantiate(eventDataSO.prefab);
        rainObject.transform.position = spawnPos;
    }
    #endregion // �Լ�

    #region �ڷ�ƾ
    /** �������� ��ȯ */
    private IEnumerator StageCO()
    {
        while (true)
        {
            yield return new WaitUntil(() => SpawnCount < maxSpawnCount);

            // �÷��̾� �ֺ����� ��ȯ ��ġ�� ã�� ��ȯ
            Vector3 spawnPosition = FindSpawnPosition();

            // Vector3.zero�� �ƴ� ���
            if (spawnPosition != Vector3.zero)
            {
                SpawnEnemy(spawnPosition);
                SpawnCount++;
                yield return new WaitForSeconds(spawnRate);
            }
        }
    }

    /** �������� �̺�Ʈ Ÿ�̸� */
    private IEnumerator StageEventCO()
    {
        while (true)
        {
            stageTimer -= Time.deltaTime;

            // �������� �̺�Ʈ ó��
            for (int i = 1; i <= 6; i++)
            {
                if (stageEventTimerList[i] >= stageTimer && StageLevel < i)
                {
                    StageLevel = i;
                    Time.timeScale = 0;

                    // ����â ����
                    ShowStageEventSelect();
                    break; // ���� â ���� �Ŀ��� ������ ��������
                }
            }

            if (StageLevel == 6)
            {
                // ������ȯ
                yield break;
            }

            yield return null; // ���� �����ӱ��� ���
        }
    }
    #endregion // �ڷ�ƾ
}

