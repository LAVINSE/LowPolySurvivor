using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    #region ����
    [SerializeField] private int expValue = 1;

    private ItemMove itemMove;
    #endregion // ����

    #region ������Ƽ
    public float moveSpeed { get; set; } = 3f;
    public Transform TargetTrasform { get; set; }
    public int ExpValue => expValue;
    #endregion // ������Ƽ

    #region �Լ�
    /** �ʱ�ȭ */
    private void Awake()
    {
        itemMove = GetComponent<ItemMove>();
    }

    /** �������� �� (Ʈ����) */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Magnet"))
        {
            TargetTrasform = other.gameObject.transform;
            itemMove.enabled = true;
        }
    }

    /** ������ ������ �� (Ʈ����) */
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Magnet"))
        {
            itemMove.enabled = false;
            TargetTrasform = null;
        }
    }
    #endregion // �Լ�
}
