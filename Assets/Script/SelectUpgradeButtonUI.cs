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
        Ammo,
        Count,
        Damage,
        Reload, 
        Range,
        Rate,
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
    private float rangePercent = 0;
    private float ratePercent = 0;
    private int Ammo = 3;
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

    /** ��ư �⺻���� */
    public void InitButton()
    {
        int randomType = Random.Range((int)eUpgradeType.None, (int)eUpgradeType.Max_Value);
        upgradeType = (eUpgradeType)randomType;

        if(weapon.Level >= weapon.MaxLevel)
        {
            upgradeType = eUpgradeType.None;
        }

        switch (upgradeType)
        {
            case eUpgradeType.None:
                // TODO : ���� �߰� ����
                upgradeInfoText.text = "�ƹ��͵� ����";
                break;
            case eUpgradeType.Ammo:
                InitData();
                upgradeInfoText.text = $" źâ {Ammo} ���� ";
                break;
            case eUpgradeType.Count:
                InitData();
                upgradeInfoText.text = $"���� ����ü �� {count} ����";
                break;
            case eUpgradeType.Damage:
                InitData();
                damagePercent = weapon.LuckDice();
                upgradeInfoText.text = $"���� ������ {damagePercent * 10} ����";
                break;
            case eUpgradeType.Reload:
                InitData();
                reloadPercent = weapon.LuckDice();
                upgradeInfoText.text = $"���� ������ {reloadPercent * 10} ����";
                break;
            case eUpgradeType.Range:
                InitData();
                rangePercent = weapon.LuckDice();
                upgradeInfoText.text = $"���� ��Ÿ� {rangePercent * 10} ����";
                break;
            case eUpgradeType.Rate:
                InitData();
                ratePercent = weapon.LuckDice();
                upgradeInfoText.text = $"���� ����ӵ� {ratePercent * 10} ����";
                break;
        }
    } 

    /** ��ư�� Ŭ�������� �߻��ϴ� ���׷��̵� */
    public void OnClickUpgrade()
    {
        switch (upgradeType)
        {
            case eUpgradeType.None:
                // TODO : ���� �߰� ����
                break;
            case eUpgradeType.Ammo:
                weapon.AmmoUpgrade(Ammo);
                break;
            case eUpgradeType.Count:
                weapon.CountUpgrade(count);
                break;
            case eUpgradeType.Damage:
                weapon.DamageUpgrade(damagePercent);
                break;
            case eUpgradeType.Reload:
                weapon.ReloadTimeUpgrade(reloadPercent);
                break;
            case eUpgradeType.Range:
                weapon.RangeUpgrade(rangePercent);
                break;
            case eUpgradeType.Rate:
                weapon.RateUpgrade(ratePercent);
                break;
        }

        weapon.LevelUpgrade();
    }
    #endregion // �Լ�
}
