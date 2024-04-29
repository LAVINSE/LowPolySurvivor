using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    #region 변수
    [SerializeField] private int expValue = 1;

    private ItemMove itemMove;
    #endregion // 변수

    #region 프로퍼티
    public float moveSpeed { get; set; } = 3f;
    public Transform TargetTrasform { get; set; }
    public int ExpValue => expValue;
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        itemMove = GetComponent<ItemMove>();
    }

    /** 접촉했을 때 (트리거) */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Magnet"))
        {
            TargetTrasform = other.gameObject.transform;
            itemMove.enabled = true;
        }
    }

    /** 접촉이 끝났을 때 (트리거) */
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Magnet"))
        {
            itemMove.enabled = false;
            TargetTrasform = null;
        }
    }
    #endregion // 함수
}
