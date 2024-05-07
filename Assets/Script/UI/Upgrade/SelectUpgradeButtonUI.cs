using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum eUpgradeWeaponType
{
    Count,
    Ammo,
    Damage,
    Reload,
    Range,
    Rate,
    Max_Value
}

public enum eUpgradePlayerType
{
    Luck,
    MoveSpeed,
    MaxHp,
    itemPickRange,
    Max_Value,
}

public class SelectUpgradeButtonUI : MonoBehaviour
{
    #region ����
    [SerializeField] private eUpgradeWeaponType upgradeWeaponType;
    [SerializeField] private eUpgradePlayerType upgradePlayerType;
    [SerializeField] private Image upgradeImg;
    [SerializeField] private TMP_Text upgradeNameText;
    [SerializeField] private TMP_Text upgradeDescText;
    [SerializeField] private TMP_Text upgradeInfoText;

    private int randomIndex = -1;

    private float weaponPercent = 0;
    private int weaponInt = 0;

    private float playerPercent = 0;
    private int playerInt = 0;
    #endregion // ����

    #region ������Ƽ
    public Weapon Weapon { get; set; }
    public PlayerMain PlayerMain { get; set; }
    #endregion // ������Ƽ

    #region �Լ�
    public void ActiveUpgrade()
    {
        GameManager.Instance.ShowUpgradeUI(false);
    }

    /** �÷��̾� �̹��� �⺻ ���� */
    private void InitPlayerData()
    {
        upgradeImg.sprite = null;
        upgradeNameText.text = "ĳ���� ����";
        upgradeDescText.text = "ĳ���� ������ ���׷��̵� �մϴ�";
    }

    /** ���� �̹��� �⺻ ���� */
    private void InitWeaponData()
    {
        upgradeImg.sprite = Weapon.WeaponSprite;
        upgradeNameText.text = Weapon.WeaponName;
        upgradeDescText.text = Weapon.WeaponDesc;
    }

    /** ��ư �⺻���� */
    public void InitButton()
    {
        int randomWeaponType = Random.Range((int)eUpgradeWeaponType.Count, (int)eUpgradeWeaponType.Max_Value);
        int randomPlayerType = Random.Range((int)eUpgradePlayerType.Luck, (int)eUpgradePlayerType.Max_Value);

        randomIndex = Random.Range(0, 2);

        upgradeWeaponType = (eUpgradeWeaponType)randomWeaponType;
        upgradePlayerType = (eUpgradePlayerType)randomPlayerType;

        // ���� ���� ������ �ִ� ������ ���
        if(Weapon.Level >= Weapon.MaxLevel)
        {
            // ĳ�� ���� ���׷��̵�
            randomIndex = 1;
        }

        // ���� ���� ������ �ִ� ������ ���
        if(Weapon.WeaponCount >= Weapon.MaxWeaponCount)
        {
            int randomWeaponTypeRest = Random.Range((int)eUpgradeWeaponType.Ammo, (int)eUpgradeWeaponType.Max_Value);
            upgradeWeaponType = (eUpgradeWeaponType)randomWeaponTypeRest;
        }

        // ���� ����ġ�� �ִ� ��ġ�� ���
        if(PlayerMain.luck >= PlayerMain.maxLuck)
        {
            int randomPlayerTypeRest = Random.Range((int)eUpgradePlayerType.MoveSpeed, (int)eUpgradePlayerType.Max_Value);
            upgradePlayerType = (eUpgradePlayerType)randomPlayerTypeRest;
        }

        // ���� ���׷��̵�
        if (randomIndex == 0)
        {
            InitWeaponData();
            weaponPercent = Weapon.LuckDicePercent();
            weaponInt = Weapon.LuckDiceInt();

            switch (upgradeWeaponType)
            {
                case eUpgradeWeaponType.Count:
                    upgradeInfoText.text = $"���� ����ü �� {1} ����";
                    break;
                case eUpgradeWeaponType.Ammo:
                    upgradeInfoText.text = $" źâ {weaponInt} ���� ";
                    break; 
                case eUpgradeWeaponType.Damage:
                    upgradeInfoText.text = $"���� ������ {weaponPercent * 10} ����";
                    break;
                case eUpgradeWeaponType.Reload:
                    upgradeInfoText.text = $"���� ������ {weaponPercent * 10} ����";
                    break;
                case eUpgradeWeaponType.Range:
                    upgradeInfoText.text = $"���� ��Ÿ� {weaponPercent * 10} ����";
                    break;
                case eUpgradeWeaponType.Rate:
                    upgradeInfoText.text = $"���� ����ӵ� {weaponPercent * 10} ����";
                    break;
            }
        }
        // ĳ�� ���� ���׷��̵�
        else if (randomIndex == 1)
        {
            InitPlayerData();
            playerPercent = PlayerMain.LuckDicePercent();
            playerInt = PlayerMain.LuckDiceInt();

            switch (upgradePlayerType)
            {
                case eUpgradePlayerType.Luck:
                    upgradeInfoText.text = $"��� {playerPercent * 10} ����";
                    break;
                case eUpgradePlayerType.MoveSpeed:
                    upgradeInfoText.text = $"�̵��ӵ� {playerPercent * 10} ����";
                    break;
                case eUpgradePlayerType.MaxHp:
                    upgradeInfoText.text = $"�ִ�ü�� {playerPercent * 10} ����";
                    break;
                case eUpgradePlayerType.itemPickRange:
                    upgradeInfoText.text = $"�����ۼ��� ���� {playerPercent * 10} ����";
                    break; 
            }
        }
        
    } 

    /** ��ư�� Ŭ�������� �߻��ϴ� ���׷��̵� */
    public void OnClickUpgrade()
    {
        if(randomIndex == 0)
        {
            // ���� ����
            switch (upgradeWeaponType)
            {
                case eUpgradeWeaponType.Ammo:
                    Weapon.AmmoUpgrade(weaponInt);
                    break;
                case eUpgradeWeaponType.Count:
                    Weapon.CountUpgrade(1);
                    break;
                case eUpgradeWeaponType.Damage:
                    Weapon.DamageUpgrade(weaponPercent);
                    break;
                case eUpgradeWeaponType.Reload:
                    Weapon.ReloadTimeUpgrade(weaponPercent);
                    break;
                case eUpgradeWeaponType.Range:
                    Weapon.RangeUpgrade(weaponPercent);
                    break;
                case eUpgradeWeaponType.Rate:
                    Weapon.RateUpgrade(weaponPercent);
                    break;
            }

            Weapon.LevelUpgrade();
        }
        else if(randomIndex == 1)
        {
            // �÷��̾� ���� ����
            switch (upgradePlayerType)
            {
                case eUpgradePlayerType.MoveSpeed:
                    PlayerMain.MoveSpeedUpgrade(playerPercent);
                    break;
                case eUpgradePlayerType.MaxHp:
                    PlayerMain.MaxHpUpgrade(playerPercent);
                    break;
                case eUpgradePlayerType.itemPickRange:
                    PlayerMain.ItemPickRange(playerPercent);
                    break;
                case eUpgradePlayerType.Luck:
                    PlayerMain.LuckUpgrade(playerInt);
                    break;
            }
        }  
    }
    #endregion // �Լ�
}
