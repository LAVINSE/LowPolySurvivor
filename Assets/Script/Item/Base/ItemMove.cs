using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    #region ����
    private Item item;
    #endregion // ����

    #region �Լ�
    /** �ʱ�ȭ */
    private void Awake()
    {
        item = GetComponent<Item>();
    }

    /** �ʱ�ȭ => ���¸� �����Ѵ� */
    private void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, item.TargetTrasform.position,
            item.moveSpeed * Time.deltaTime);
    }

    /** �������� �� (Ʈ����) */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerMain>().GetExp(item.ExpValue);
            this.gameObject.SetActive(false);
        }
    }
    #endregion // �Լ�
}
