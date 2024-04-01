using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScanner : MonoBehaviour
{
    #region 변수
    [SerializeField] private float scanRange = 0f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Transform nearTarget;
    #endregion // 변수

    #region 프로퍼티
    public bool IsTarget { get; set; } = false;
    public Collider[] ColliderArray { get; set; }
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

        // 가장 가까운 적을 찾습니다.
        nearTarget = GetNear();
    }

    /** 플레이어와 위치를 비교하여 가장가까운 적을 찾는다 */
    private Transform GetNear()
    {
        if(ColliderArray.Length == 0) 
        {
            return null;
        }

        if (ColliderArray.Length > 0)
        {
            float distance1 = Vector3.Distance(this.transform.position, ColliderArray[0].transform.position);

            foreach(Collider collider in ColliderArray)
            {
                float distance2 = Vector3.Distance(this.transform.position, collider.transform.position);

                if(distance1 > distance2)
                {
                    distance1 = distance2;
                    return collider.transform;
                }
            }
        }

        return ColliderArray[0].transform;
    }
    #endregion // 함수
}
