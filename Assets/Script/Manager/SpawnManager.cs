using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region ����
    #endregion // ����

    #region �Լ�
    #endregion // �Լ�
}

// �׽�Ʈ 1
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // ��ȯ�� ���� ������
    public Transform playerTransform; // �÷��̾��� ��ġ

    public float spawnRadius = 10f; // ��ȯ �ݰ�
    public LayerMask obstacleLayer; // �ǹ� �Ǵ� ��ֹ��� ��Ÿ���� ���̾�

    void Update()
    {
        // �÷��̾�� �ǹ� ���̿� ��ֹ��� ������ ���� ��ȯ
        if (!Physics.CheckSphere(playerTransform.position, spawnRadius, obstacleLayer))
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // ��ȯ�� ��ġ ���
        Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius;
        spawnPosition += playerTransform.position;
        spawnPosition.y = 0f; // ���̸� 0���� �����Ͽ� ���� ���� ��ȯ�ǵ��� ��

        // �� ����
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}

