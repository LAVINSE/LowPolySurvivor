using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    #region ����
    [Header("=====> ���� ��� <=====")]
    [SerializeField] private List<EquipDataSO> equipDataSOList = new List<EquipDataSO>();

    [Header("=====> �÷��̾� ���� <=====")]
    [SerializeField] private float maxHp;

    private Dictionary<EquipDataSO, bool> useEquipDataDict = new Dictionary<EquipDataSO, bool>();
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
