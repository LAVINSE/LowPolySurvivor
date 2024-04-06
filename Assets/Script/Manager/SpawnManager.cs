using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    #region 변수
    [SerializeField] private Transform playerPos;
    [SerializeField] private float spawTime; // 소환 시간?
    [SerializeField] private float spawnRadius; // 소환 범위
    [SerializeField] private float dectectRange = 1f; // 감지 범위
    [SerializeField] private string tagName = "Building";
    [SerializeField] private float correctPosY = 0.2f; // 보정값
    [SerializeField] private int spawnCount = 0;
    [SerializeField] private float minSpawnRadius = 3f; // 플레이어와의 거리 최소값
    #endregion // 변수

    #region 함수
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

    /** 테스트 코드 */
    private IEnumerator sdf()
    {
        while (true)
        {
            // 플레이어 주변에서 소환 위치를 찾아 소환
            Vector3 spawnPosition = FindSpawnPosition();

            // Vector3.zero가 아닐 경우
            if (spawnPosition != Vector3.zero)
            {
                SpawnEnemy(spawnPosition);
                yield return new WaitForSeconds(1f);
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
    private void SpawnEnemy(Vector3 spawnPos)
    {
        GameObject enemy = GameManager.Instance.PoolManager.GetEnemy(0, spawnPos);
        enemy.GetComponent<Enemy>().Player = playerPos.GetComponent<PlayerMain>();
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
    #endregion // 함수
}

