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
    public string WeaponName { get; set; } = string.Empty;
    public string WeaponDesc { get; set; } = string.Empty;
    public int MaxLevel { get; set; } = 0;
    public int Level { get; set; } = 0;
    public int Ammo { get; set; } = 0;
    public int Count { get; set; } = 0;
    public float Damage { get; set; } = 0f;
    public float ReloadTime { get; set; } = 0f;
    public float Range { get; set; } = 0f;
    public float Rate { get; set; } = 0f;
    public GameObject prefab;

    public PlayerScanner PlayerScanner { get; set; }
    #endregion // 프로퍼티

    #region 함수
    public virtual void Awake()
    {
        PlayerScanner = GetComponentInParent<PlayerScanner>();

        Init();
    }

    private void Start()
    {
        WeaponUse();
    }

    /** 데이터 기본 설정 */
    private void Init()
    {
        WeaponName = equipWeaponDataSO.weaponName;
        WeaponDesc = equipWeaponDataSO.weaponDesc;
        Level = equipWeaponDataSO.baseLevel;
        MaxLevel = equipWeaponDataSO.maxLevel;
        Ammo = equipWeaponDataSO.baseAmmo;
        Count = equipWeaponDataSO.baseAmmo;
        Damage = equipWeaponDataSO.baseDamage;
        ReloadTime = equipWeaponDataSO.baseReloadTime;
        Range = equipWeaponDataSO.baseRange;
        Rate = equipWeaponDataSO.baseRate;
        prefab = equipWeaponDataSO.prefab;
    }

    public virtual void WeaponUse()
    {

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

            yield return new WaitForFixedUpdate();
        }


        Debug.Log(" 쿨타미 끝");
        Callback?.Invoke();
    }
    #endregion // 코루틴
}
