using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScanner : MonoBehaviour
{
    #region 변수
    [SerializeField] private float scanRange = 0f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private RaycastHit[] targets;
    [SerializeField] private Transform nearTarget;
    #endregion // 변수

    #region 함수
    /** 초기화 => 상태를 갱신한다 */
    private void FixedUpdate()
    {
        targets = Physics.SphereCastAll(this.transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearTarget = GetNear();
    }

    /** 플레이어와 위치를 비교하여 가장가까운 적을 찾는다 */
    private Transform GetNear()
    {
        Transform result = null;
        float diff = 100;

        foreach (RaycastHit target in targets)
        {
            Vector3 myPos = this.transform.position;
            Vector3 targetPos = target.transform.position;
            float currentDiff = Vector3.Distance(myPos, targetPos);

            if (currentDiff < diff)
            {
                diff = currentDiff;
                result = target.transform;
            }
        }

        return result;
    }
    #endregion // 함수
}
