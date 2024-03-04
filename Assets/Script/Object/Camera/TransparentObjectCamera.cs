using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObjectCamera : MonoBehaviour
{
    #region 변수
    public GameObject Player;
    #endregion // 변수

    /** 초기화 => 상태를 갱신한다 */
    private void LateUpdate()
    {
        RaycastObject();
    }

    private void RaycastObject()
    {
        // 레이캐스트 방향
        Vector3 direction = (Player.transform.position - this.transform.position).normalized;

        // 무한대로 발사되는 레이캐스트 EnvironmentObject만 반응
        RaycastHit[] hits = Physics.RaycastAll(this.transform.position, direction, Mathf.Infinity,
            LayerMask.GetMask("EnvironmentObject"));

        // 충돌한 객체들을 반복
        for (int i = 0; i < hits.Length; i++)
        {
            // 충동한 객체 안에 있는 컴포넌트 가져오기
            TransparentObject[] obj = hits[i].transform.GetComponentsInChildren<TransparentObject>();

            for (int j = 0; j < obj.Length; j++)
            {
                // 오브젝트를 투명하게 만든다
                obj[j]?.BecomeTransparent();
            }
        }
    }
}
