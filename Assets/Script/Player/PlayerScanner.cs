using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScanner : MonoBehaviour
{
    #region 변수
    [Header("=====> 수치 <=====")]
    [SerializeField] private float scanRange = 0f; // 통합
    [SerializeField] private float sacnRangeForward = 0f; // 정면
    [SerializeField] private float detectAngle = 45f; // 감지 범위

    [Header("=====> 레이어 설정 <=====")]
    [SerializeField] private LayerMask targetLayer; // 레이어

    [Header("=====> 인스펙터 확인 <=====")]
    [SerializeField] private Transform nearTarget; // 가장 가까운 타겟
    [SerializeField] private Transform[] nearTargetArray; // 가까운 타겟 배열
    [SerializeField] private Transform[] forwardNearTargetArray; // 정면 타겟 배열
    [SerializeField] private Transform forwardNearTarget; // 가장 가까운 정면 타겟
    #endregion // 변수

    #region 프로퍼티
    public bool IsTarget { get; set; } = false;
    public bool IsTargetForward { get; set; } = false;
    public Collider[] ColliderArray { get; set; }

    // 전체
    public Transform NearTarget => nearTarget;
    public Transform[] NearTargetArray => nearTargetArray;

    // 정면
    public Transform ForwardNearTarget => forwardNearTarget;
    public Transform[] ForwardNearTargetArray => forwardNearTargetArray;
    #endregion // 프로퍼티

    #region 함수
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, scanRange);

        Gizmos.color = Color.green;
        Vector3 direction = transform.forward;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-detectAngle / 2, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(detectAngle / 2, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * direction;
        Vector3 rightRayDirection = rightRayRotation * direction;

        Gizmos.DrawLine(transform.position, transform.position + leftRayDirection * sacnRangeForward);
        Gizmos.DrawLine(transform.position, transform.position+ rightRayDirection * sacnRangeForward);
        Gizmos.DrawLine(transform.position+ leftRayDirection * sacnRangeForward,
            transform.position+ rightRayDirection * sacnRangeForward);
    }

    /** 초기화 => 상태를 갱신한다 */
    private void Update()
    {
        ColliderArray = Physics.OverlapSphere(transform.position, scanRange, targetLayer);

        // 가장 가까운 적 설정 (전체)
        SetNearTarget();
        // 가장 가까운 적 설정 (정면)
        SetNearTargetForward();

        IsTarget = ColliderArray.Length > 0;
        IsTargetForward = forwardNearTargetArray.Length > 0;
    }

    /** 가장 가까운 적 설정 (전체) */
    private void SetNearTarget()
    {
        nearTargetArray = SortEnemyList();

        if (nearTargetArray.Length > 0)
        {
            nearTarget = nearTargetArray[0];
        }
        else
        {
            nearTarget = null;
        }
    }

    /** 가장 가까운 적 설정 (정면) */
    private void SetNearTargetForward()
    {
        forwardNearTargetArray = ForwardNearTargetList();

        if (forwardNearTargetArray.Length > 0)
        {
            forwardNearTarget = NearTargetArray[0];
        }
        else
        {
            forwardNearTarget = null;
        }
    }

    /** 플레이어와 위치를 비교하여 가장가까운 순서대로 정렬한다 (정면) */
    private Transform[] ForwardNearTargetList()
    {
        List<Transform> sortList = new List<Transform>();

        foreach (Collider collider in ColliderArray)
        {
            Transform enemyTransform = collider.transform;

            // 정면 부채꼴 범위 안에 적이 있는지 확인
            if (IsEnemyInDetectionCone(enemyTransform))
            {
                sortList.Add(enemyTransform);
            }
        }

        // 거리를 기준으로 정렬, 오름차순
        sortList.Sort((aPos, bPos) =>
        {
            float distanceA = Vector3.Distance(this.transform.position, aPos.position);
            float distanceB = Vector3.Distance(this.transform.position, bPos.position);

            return distanceA.CompareTo(distanceB);
        });

        return sortList.ToArray(); 
    }

    /** 플레이어와 위치를 비교하여 가장가까운 순서대로 정렬한다 (전체) */
    private Transform[] SortEnemyList()
    {
        List<Transform> sortList = new List<Transform>();

        // 범위에 있는 적들을 정렬 리스트에 추가한다
        foreach (Collider collider in ColliderArray)
        {
            sortList.Add(collider.GetComponent<Enemy>().transform);
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

    /** 정면 부채꼴 범위 안에 적이 있는지 확인한다 */
    private bool IsEnemyInDetectionCone(Transform enemyTransform)
    {
        Vector3 direction = (enemyTransform.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, direction);

        // 계산된 각도가 정해진 각도 안에 있을 경우
        if (angle < detectAngle / 2)
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, sacnRangeForward, targetLayer);
            foreach(Collider collider in colliders)
            {
                if(collider.transform == enemyTransform)
                {
                    return true;
                }
            }
        }

        // 적이 감지 범위 내에 없거나 가림막이 있음
        return false; 
    }
    #endregion // 함수
}
