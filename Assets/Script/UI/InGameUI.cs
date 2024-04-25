using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    #region ����
    [Header("=====> ü�¹� <=====")]
    [SerializeField] private Image hpBarImg = null;

    [Header("=====> ��� ���� <=====")]
    [SerializeField] private Button equipSlot_1 = null;
    [SerializeField] private Button equipSlot_2 = null;
    [SerializeField] private Button equipSlot_3 = null;
    #endregion // ����

    #region �Լ�
    /** ��� ������ ���� �̹����� �����Ѵ� */
    public void InitEquipSlot(List<Weapon> weaponList)
    {
        if(weaponList.Count != 3) { Debug.Log(" ��ϵ� ���Ⱑ 3���� �ƴ� "); return; }

        equipSlot_1.GetComponent<EquipSlotUI>().itemImg.sprite = weaponList[0].WeaponSprite;
        equipSlot_2.GetComponent<EquipSlotUI>().itemImg.sprite = weaponList[1].WeaponSprite;
        equipSlot_3.GetComponent<EquipSlotUI>().itemImg.sprite = weaponList[2].WeaponSprite;
    }

    /** �÷��̾� ü�¹� �̹����� �����Ѵ� */
    public void UpdateHpBar(int maxHp, int currentHp)
    {
        hpBarImg.fillAmount = currentHp / maxHp;
    }
    #endregion // �Լ�
}
