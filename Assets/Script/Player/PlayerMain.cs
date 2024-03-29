using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    #region ����
    [Header("=====> ���� ��� <=====")]
    [SerializeField] private List<EquipWeaponDataSO> equipDataSOList = new List<EquipWeaponDataSO>();

    [Header("=====> �÷��̾� ���� <=====")]
    [SerializeField] private float maxHp;
    [SerializeField] public int luck;

    private Dictionary<EquipWeaponDataSO, bool> useEquipDataDict = new Dictionary<EquipWeaponDataSO, bool>();
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
