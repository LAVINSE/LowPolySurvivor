using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectUpgradeButtonUI : MonoBehaviour
{
    public enum eUpgradeType
    { 
        None,
        Damage,
        Reload,
        Count,
        Max_Value
    }

    #region ����
    [SerializeField] private eUpgradeType upgradeType = eUpgradeType.None;
    [SerializeField] private Image upgradeImg;
    [SerializeField] private TMP_Text upgradeNameText;
    [SerializeField] private TMP_Text upgradeDescText;
    [SerializeField] private TMP_Text upgradeInfoText;

    [SerializeField] private Weapon weapon;
    [SerializeField] private PlayerMain playerMain;

    private float damagePercent = 0;
    private float reloadPercent = 0;
    private int count = 1;
    #endregion // ����

    #region �Լ�
    /** �̹��� �⺻ ���� */
    private void InitData()
    {
        // TODO : ���� �̹��� �߰��ؾߵ�
        upgradeNameText.text = weapon.WeaponName;
        upgradeDescText.text = weapon.WeaponDesc;
    }

    /** �÷��̾� ��� ��ġ�� ���� ���׷��̵� ��ġ�� �޶����� */
    private int luckDice()
    {
        if(Random.Range(0, 101) < playerMain.luck)
        {
            int dice = Random.Range(0, 101);

            if(dice <= 5) // 5 %
            {
                return 5;
            }
            else if(dice <= 30) // 25 %
            {
                return 4;
            }
            else if(dice <= 60) // 30 %
            {
                return 3;
            }
            else if(dice <= 100) // 40 %
            {
                return 2;
            }
        }

        return 1;
    }

    /** ��ư �⺻���� */
    public void InitButton()
    {
        int randomType = Random.Range((int)eUpgradeType.None, (int)eUpgradeType.Max_Value);
        upgradeType = (eUpgradeType)randomType;

        if(weapon.Level > weapon.MaxLevel)
        {
            upgradeType = eUpgradeType.None;
        }

        switch (upgradeType)
        {
            case eUpgradeType.None:
                upgradeInfoText.text = "�ƹ��͵� ����";
                break;
            case eUpgradeType.Damage:
                InitData();
                damagePercent = luckDice() * 10;
                upgradeInfoText.text = $"���� ������ {damagePercent} ����";
                break;
            case eUpgradeType.Reload:
                InitData();
                reloadPercent = luckDice() * 10;
                upgradeInfoText.text = $"���� ������ {reloadPercent} ����";
                break;
            case eUpgradeType.Count:
                InitData();
                upgradeInfoText.text = $"���� ����ü �� {count} ����";
                break;
        }
    } 

    /** ��ư�� Ŭ�������� �߻��ϴ� ���׷��̵� */
    private void OnClickUpgrade()
    {
        switch (upgradeType)
        {
            case eUpgradeType.None:
                upgradeInfoText.text = "�ƹ��͵� ����";
                break;
            case eUpgradeType.Damage:
                weapon.Damage *= damagePercent;
                break;
            case eUpgradeType.Reload:
                weapon.ReloadTime *= reloadPercent;
                break;
            case eUpgradeType.Count:
                weapon.Count += count;
                break;
        }

        weapon.Level++;
    }
    #endregion // �Լ�
}