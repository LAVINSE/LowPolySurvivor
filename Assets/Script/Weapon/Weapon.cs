using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    #region 변수
    [SerializeField] protected EquipWeaponDataSO equipWeaponDataSO;
    #endregion // 변수

    #region 프로퍼티
    public eEquipType EquipType { get; set; } = eEquipType.None;
    public string WeaponName { get; set; } = string.Empty;
    public string WeaponDesc { get; set; } = string.Empty;
    public int MaxLevel { get; set; } = 0;
    public int Level { get; set; } = 0;
    public int MaxAmmo { get; set; } = 0;
    public int Ammo { get; set; } = 0;
    public int MaxWeaponCount { get; set; } = 0;
    public int WeaponCount { get; set; } = 0;
    public int Penetrate { get; set;} = 0; // 관통
    public float Damage { get; set; } = 0f;
    public float ReloadTime { get; set; } = 0f;
    public float Range { get; set; } = 0f; // 사거리
    public float Rate { get; set; } = 0f; // 연사속도
    public float bulletVelocity { get; set; } = 0f; // 탄속
    public GameObject prefab { get; set; } = null;
    public Sprite WeaponSprite { get; set; } = null;
    public Image skillImage { get; set; } = null;

    public PlayerScanner PlayerScanner { get; set; }
    public PlayerMain PlayerMain { get; set; }
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    public virtual void Awake()
    {
        PlayerScanner = GetComponentInParent<PlayerScanner>();
        PlayerMain = GetComponentInParent<PlayerMain>();
    }

    /** 초기화 => 활성화 됐을때 */
    private void OnEnable()
    {
        WeaponUse();
    }

    /** 데이터 기본 설정 */
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

    /** 무기를 사용한다 */
    public virtual void WeaponUse()
    {

    }

    /** 플레이어 행운 수치에 따라 업그레이드 수치가 달라진다 */
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

    /** 플레이어 행운 수치에 따라 업그레이드 수치가 달라진다 */
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

    /** 레벨 업그레이드 */
    public void LevelUpgrade()
    {
        Level++;
    }

    /** 탄창 업그레이드 */
    public void AmmoUpgrade(int increaseAmmo)
    {
        MaxAmmo += increaseAmmo;
    }

    /** 무기 개수 업그레이드 */
    public void CountUpgrade(int increaseCount)
    {
        WeaponCount += increaseCount;
    }

    /** 데미지 업그레이드 */
    public void DamageUpgrade(float increaseDamage)
    {
        Damage *= (1f + increaseDamage);
    }
    
    /** 재장전 시간 업그레이드 */
    public void ReloadTimeUpgrade(float decreaseReloadTime)
    {
        ReloadTime *= (1f - decreaseReloadTime);
    }

    /** 사거리 업그레이드 */
    public void RangeUpgrade(float increaseRange)
    {
        Range *= (1f + increaseRange);
    }

    /** 연사속도 업그레이드 */
    public void RateUpgrade(float decreaseRate)
    {
        Rate *= (1f - decreaseRate);
    }
    #endregion // 함수

    #region 코루틴
    /** 쿨타임 적용 */
    protected IEnumerator CoolDownCO(float CoolTime, Action Callback, Image SkillImg = null)
    {
        float CurrentTime = 0.0f;
        CurrentTime = CoolTime;

        while (CurrentTime > 0.0f)
        {
            CurrentTime -= Time.deltaTime;

            // 스킬이미지가 있을경우
            if (SkillImg != null)
            {
                SkillImg.fillAmount = (CurrentTime / CoolTime);
            }

            yield return null;
        }

        Callback?.Invoke();
    }
    #endregion // 코루틴
}
