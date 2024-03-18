using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region ����
    [Header("=====> Enemy ���� <=====")]
    [SerializeField] private float moveSpeed = 0;
    [SerializeField] private float maxHp = 0;
    #endregion // ����

    #region ������Ƽ
    public bool IsTracking { get; set; } = false;
    public bool IsAttack { get; set; } = false;
    public bool IsDie { get; set; } = false;

    public PlayerMain Player { get; set; }
    #endregion // ������Ƽ

    #region �Լ�
    /** �ʱ�ȭ */
    private void Awake()
    {
        
    }

    /** �������� �ش� */
    protected void TakeDamage()
    {
        // Do something
    }

    /** �÷��̾�� �Ÿ��� üũ�ϰ� �����Ѵ� */
    public virtual void TargetSetting()
    {
        // Do something
    }
    #endregion // �Լ�
}
