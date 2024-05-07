using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAttack : MonoBehaviour
{
    #region ����
    [SerializeField] private float moveSpeed = 2f;
    

    protected bool isAttack = false;
    #endregion // ����

    #region ������Ƽ
    public PlayerMain PlayerMain { get; set; }
    #endregion // ������Ƽ

    #region �Լ�
    /** �ʱ�ȭ => ���¸� �����Ѵ� */
    private void Update()
    {
        if (PlayerMain != null)
        {
            Vector3 targetPosition = new Vector3(PlayerMain.transform.position.x,
                transform.position.y, PlayerMain.transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    /** ���������� (Ʈ����) */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isAttack = true;
            Attack(PlayerMain);
        }
    }

    /** �����̳������� (Ʈ����) */
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isAttack = false;
        }
    }

    protected virtual void Attack(PlayerMain playerMain) { }
    #endregion // �Լ�
}
