using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScanner : MonoBehaviour
{
    #region 변수
    [SerializeField] private float scanRange = 0f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Transform nearTarget;
    [SerializeField] private Transform[] nearTargetArray;
    #endregion // 변수

    #region 프로퍼티
    public bool IsTarget { get; set; } = false;
    public Collider[] ColliderArray { get; set; }
    public Transform NearTarget => nearTarget;
    public Transform[] NearTargetArray => nearTargetArray;
    #endregion // 프로퍼티

    #region 함수
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, scanRange);
    }

    /** 초기화 => 상태를 갱신한다 */
    private void Update()
    {
        ColliderArray = Physics.OverlapSphere(transform.position, scanRange, targetLayer);

        IsTarget = ColliderArray.Length > 0;

        nearTargetArray = SortEnemyList();

        if(nearTargetArray.Length > 0)
        {
            nearTarget = nearTargetArray[0];
        }
    }

    /** 플레이어와 위치를 비교하여 가장가까운 적을 찾는다 */
    private Transform GetNear()
    {
        if (ColliderArray.Length == 0)
        {
            return null;
        }

        Transform nearEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Collider collider in ColliderArray)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearEnemy = collider.GetComponent<Enemy>().MeshTransform;
            }
        }

        return nearEnemy;
    }

    /** 플레이어와 위치를 비교하여 가장가까운 순서대로 정렬한다 */
    private Transform[] SortEnemyList()
    {
        List<Transform> sortList = new List<Transform>();

        // 범위에 있는 적들을 정렬 리스트에 추가한다
        foreach (Collider collider in ColliderArray)
        {
            sortList.Add(collider.GetComponent<Enemy>().MeshTransform);
        }

        // 거리를 기준으로 정렬, 오름차순
        sortList.Sort((aPos, bPos) =>
        {
            float distanceA = Vector3.Distance(this.transform.position, aPos.position);
            float distanceB = Vector3.Distance(this.transform.position, bPos.position);

            return distanceA.CompareTo(distanceB);
        });

        // 정렬된 적들을 배열로 변환하여 반환합니다.
        return sortList.ToArray();
    }
    #endregion // 함수
}
