using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAttack : MonoBehaviour
{
    #region 변수
    [SerializeField] private float moveSpeed = 2f;
    

    protected bool isAttack = false;
    #endregion // 변수

    #region 프로퍼티
    public PlayerMain PlayerMain { get; set; }
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 => 상태를 갱신한다 */
    private void Update()
    {
        if (PlayerMain != null)
        {
            Vector3 targetPosition = new Vector3(PlayerMain.transform.position.x,
                transform.position.y, PlayerMain.transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    /** 접촉했을때 (트리거) */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isAttack = true;
            Attack(PlayerMain);
        }
    }

    /** 접촉이끝났을때 (트리거) */
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isAttack = false;
        }
    }

    protected virtual void Attack(PlayerMain playerMain) { }
    #endregion // 함수
}
