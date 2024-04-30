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
    [SerializeField] private int maxSpawnCount = 0;
    [SerializeField] private float spawnRate = 1f; // ��ȯ �ð�?
    [SerializeField] private float stageTimer = 360f;

    [Header("=====> �ڿ����� ���� ��ư <=====")]
    [SerializeField] private GameObject stageEventObject;
    [SerializeField] private SelectEventButtonUI selectEventButtonUI_1;
    [SerializeField] private SelectEventButtonUI selectEventButtonUI_2;
    [SerializeField] private SelectEventButtonUI selectEventButtonUI_3;

    [Header("=====> �ڿ����� <=====")]
    [SerializeField] private GameObject rainPrefab;

    private List<float> stageEventTimerList = new List<float>();

    private eStageEventType stageEventType = eStageEventType.None;

    private List<int> randomIndexList = new List<int>();
    #endregion // ����

    #region ������Ƽ
    public PlayerMain PlayerMain { get; set; }
    public int StageLevel { get; set; } = 0;
    public int SpawnCount { get; set; } = 0;

    public Dictionary<eStageEventType, bool> stageEventDict = new Dictionary<eStageEventType, bool>();
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
            stageEventTimerList.Add(stageTimer - (60 * i));
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
    }

    /** �׽�Ʈ �ڵ� */
    private IEnumerator StageCO()
    {
        while (true)
        {
            yield return new WaitUntil(() => SpawnCount < maxSpawnCount);

            // �÷��̾� �ֺ����� ��ȯ ��ġ�� ã�� ��ȯ
            Vector3 spawnPosition = FindSpawnPosition();

            stageTimer -= Time.deltaTime;

            // Vector3.zero�� �ƴ� ���
            if (spawnPosition != Vector3.zero)
            {
                SpawnEnemy(spawnPosition);
                SpawnCount++;
                yield return new WaitForSeconds(spawnRate);

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

                if(StageLevel == 6)
                {
                    // ������ȯ
                }
            }
        }
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
            Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
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

    /** �������� �̺�Ʈ ����â�� Ȱ��ȭ/��Ȱ��ȭ �Ѵ� */
    private void ShowStageEventSelect()
    {
        stageEventObject.SetActive(!stageEventObject.activeSelf);

    }

    /** �������� �̺�Ʈ�� �����Ѵ� */
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

    /** �������� �̺�Ʈ 3���� ���� */
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

    /** �ڿ����� "��"�� �����Ѵ� */
    private void StageEventRain()
    {
        float x = Random.Range(stageSO.minPos.x, stageSO.maxPos.x);
        float z = Random.Range(stageSO.minPos.z, stageSO.maxPos.z);
        float y = 20f;

        Vector3 spawnPos = new Vector3(x, y, z);

        GameObject rainObject = Instantiate(rainPrefab);
        rainObject.transform.position = spawnPos;
    }

    /** �Ͻ����� ���� */
    public void TimeScaleStart()
    {
        Time.timeScale = 1;
    }
    #endregion // �Լ�
}

