using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region 변수
    #endregion // 변수

    #region 함수
    #endregion // 함수
}

// 테스트 1
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 소환할 적의 프리팹
    public Transform playerTransform; // 플레이어의 위치

    public float spawnRadius = 10f; // 소환 반경
    public LayerMask obstacleLayer; // 건물 또는 장애물을 나타내는 레이어

    void Update()
    {
        // 플레이어와 건물 사이에 장애물이 없으면 적을 소환
        if (!Physics.CheckSphere(playerTransform.position, spawnRadius, obstacleLayer))
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // 소환할 위치 계산
        Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius;
        spawnPosition += playerTransform.position;
        spawnPosition.y = 0f; // 높이를 0으로 설정하여 적이 지상에 소환되도록 함

        // 적 생성
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}

