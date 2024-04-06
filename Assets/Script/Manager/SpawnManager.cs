using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    #region ����
    [SerializeField] private Transform playerPos;
    [SerializeField] private float spawTime; // ��ȯ �ð�?
    [SerializeField] private float spawnRadius; // ��ȯ ����
    [SerializeField] private float dectectRange = 1f; // ���� ����
    [SerializeField] private string tagName = "Building";
    [SerializeField] private float correctPosY = 0.2f; // ������
    [SerializeField] private int spawnCount = 0;
    [SerializeField] private float minSpawnRadius = 3f; // �÷��̾���� �Ÿ� �ּҰ�
    #endregion // ����

    #region �Լ�
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(playerPos.position, spawnRadius);

        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(playerPos.position, minSpawnRadius);
    }

    private void Start()
    {
        StartCoroutine(sdf());
    }

    /** �׽�Ʈ �ڵ� */
    private IEnumerator sdf()
    {
        while (true)
        {
            // �÷��̾� �ֺ����� ��ȯ ��ġ�� ã�� ��ȯ
            Vector3 spawnPosition = FindSpawnPosition();

            // Vector3.zero�� �ƴ� ���
            if (spawnPosition != Vector3.zero)
            {
                SpawnEnemy(spawnPosition);
                yield return new WaitForSeconds(1f);
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
    private void SpawnEnemy(Vector3 spawnPos)
    {
        GameObject enemy = GameManager.Instance.PoolManager.GetEnemy(0, spawnPos);
        enemy.GetComponent<Enemy>().Player = playerPos.GetComponent<PlayerMain>();
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
    #endregion // �Լ�
}

