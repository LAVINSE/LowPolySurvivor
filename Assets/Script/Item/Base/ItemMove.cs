using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    #region 변수
    private Item item;
    #endregion // 변수

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        item = GetComponent<Item>();
    }

    /** 초기화 => 상태를 갱신한다 */
    private void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, item.TargetTrasform.position,
            item.moveSpeed * Time.deltaTime);
    }

    /** 접촉했을 때 (트리거) */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerMain>().GetExp(item.ExpValue);
            this.gameObject.SetActive(false);
        }
    }
    #endregion // 함수
}
