using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    #region ����
    [Header("=====> �÷��̾� ���� <=====")]
    [SerializeField] private float maxHp;
    #endregion // ����

    #region ������Ƽ
    public float CurrentHp { get; set; }
    #endregion // ������Ƽ

    #region �Լ�
    /** �ʱ�ȭ */
    private void Awake()
    {
        // ���� ü�� ����
        CurrentHp = maxHp;
    }
    #endregion // �Լ�
}
