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
    #endregion // ������Ƽ

    #region �Լ�
    public virtual void Awake()
    {
        PlayerScanner = GetComponentInParent<PlayerScanner>();

        Init();
    }

    private void Start()
    {
        WeaponUse();
    }

    /** ������ �⺻ ���� */
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

            yield return new WaitForFixedUpdate();
        }


        Debug.Log(" ��Ÿ�� ��");
        Callback?.Invoke();
    }
    #endregion // �ڷ�ƾ
}
