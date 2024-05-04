using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    #region ����
    [SerializeField] protected EquipWeaponDataSO equipWeaponDataSO;
    #endregion // ����

    #region ������Ƽ
    public eEquipType EquipType { get; set; } = eEquipType.None;
    public string WeaponName { get; set; } = string.Empty;
    public string WeaponDesc { get; set; } = string.Empty;
    public int MaxLevel { get; set; } = 0;
    public int Level { get; set; } = 0;
    public int MaxAmmo { get; set; } = 0;
    public int Ammo { get; set; } = 0;
    public int MaxWeaponCount { get; set; } = 0;
    public int WeaponCount { get; set; } = 0;
    public int Penetrate { get; set;} = 0; // ����
    public float Damage { get; set; } = 0f;
    public float ReloadTime { get; set; } = 0f;
    public float Range { get; set; } = 0f; // ��Ÿ�
    public float Rate { get; set; } = 0f; // ����ӵ�
    public float bulletVelocity { get; set; } = 0f; // ź��
    public GameObject prefab { get; set; } = null;
    public Sprite WeaponSprite { get; set; } = null;
    public Image skillImage { get; set; } = null;

    public PlayerScanner PlayerScanner { get; set; }
    public PlayerMain PlayerMain { get; set; }
    #endregion // ������Ƽ

    #region �Լ�
    /** �ʱ�ȭ */
    public virtual void Awake()
    {
        PlayerScanner = GetComponentInParent<PlayerScanner>();
        PlayerMain = GetComponentInParent<PlayerMain>();
    }

    /** �ʱ�ȭ => Ȱ��ȭ ������ */
    private void OnEnable()
    {
        WeaponUse();
    }

    /** ������ �⺻ ���� */
    public virtual void Init()
    {
        EquipType = equipWeaponDataSO.equipType;
        WeaponName = equipWeaponDataSO.weaponName;
        WeaponDesc = equipWeaponDataSO.weaponDesc;
        Level = equipWeaponDataSO.baseLevel;
        MaxLevel = equipWeaponDataSO.maxLevel;
        MaxAmmo = equipWeaponDataSO.baseMaxAmmo;
        MaxWeaponCount = equipWeaponDataSO.MaxWeaponCount;
        WeaponCount = equipWeaponDataSO.baseWeaponCount;
        Penetrate = equipWeaponDataSO.basePenetrate;
        Damage = equipWeaponDataSO.baseDamage;
        ReloadTime = equipWeaponDataSO.baseReloadTime;
        Range = equipWeaponDataSO.baseRange;
        Rate = equipWeaponDataSO.baseRate;
        bulletVelocity = equipWeaponDataSO.baseBulletVelocity;
        prefab = equipWeaponDataSO.prefab;
        WeaponSprite = equipWeaponDataSO.weaponSprite;
    }

    /** ���⸦ ����Ѵ� */
    public virtual void WeaponUse()
    {

    }

    /** �÷��̾� ��� ��ġ�� ���� ���׷��̵� ��ġ�� �޶����� */
    public float LuckDicePercent()
    {
        if (UnityEngine.Random.Range(0, 101) < PlayerMain.luck)
        {
            int dice = UnityEngine.Random.Range(0, 101);

            if (dice <= 5) // 5 %
            {
                return 0.5f;
            }
            else if (dice <= 30) // 25 %
            {
                return 0.4f;
            }
            else if (dice <= 60) // 30 %
            {
                return 0.3f;
            }
            else if (dice <= 100) // 40 %
            {
                return 0.2f;
            }
        }

        return 0.1f;
    }

    /** �÷��̾� ��� ��ġ�� ���� ���׷��̵� ��ġ�� �޶����� */
    public int LuckDiceInt()
    {
        if (UnityEngine.Random.Range(0, 101) < PlayerMain.luck)
        {
            int dice = UnityEngine.Random.Range(0, 101);

            if (dice <= 5) // 5 %
            {
                return 5;
            }
            else if (dice <= 30) // 25 %
            {
                return 4;
            }
            else if (dice <= 60) // 30 %
            {
                return 3;
            }
            else if (dice <= 100) // 40 %
            {
                return 2;
            }
        }

        return 1;
    }

    /** ���� ���׷��̵� */
    public void LevelUpgrade()
    {
        Level++;
    }

    /** źâ ���׷��̵� */
    public void AmmoUpgrade(int increaseAmmo)
    {
        MaxAmmo += increaseAmmo;
    }

    /** ���� ���� ���׷��̵� */
    public void CountUpgrade(int increaseCount)
    {
        WeaponCount += increaseCount;
    }

    /** ������ ���׷��̵� */
    public void DamageUpgrade(float increaseDamage)
    {
        Damage *= (1f + increaseDamage);
    }
    
    /** ������ �ð� ���׷��̵� */
    public void ReloadTimeUpgrade(float decreaseReloadTime)
    {
        ReloadTime *= (1f - decreaseReloadTime);
    }

    /** ��Ÿ� ���׷��̵� */
    public void RangeUpgrade(float increaseRange)
    {
        Range *= (1f + increaseRange);
    }

    /** ����ӵ� ���׷��̵� */
    public void RateUpgrade(float decreaseRate)
    {
        Rate *= (1f - decreaseRate);
    }
    #endregion // �Լ�

    #region �ڷ�ƾ
    /** ��Ÿ�� ���� */
    protected IEnumerator CoolDownCO(float CoolTime, Action Callback, Image SkillImg = null)
    {
        float CurrentTime = 0.0f;
        CurrentTime = CoolTime;

        while (CurrentTime > 0.0f)
        {
            CurrentTime -= Time.deltaTime;

            // ��ų�̹����� �������
            if (SkillImg != null)
            {
                SkillImg.fillAmount = (CurrentTime / CoolTime);
            }

            yield return null;
        }

        Callback?.Invoke();
    }
    #endregion // �ڷ�ƾ
}
