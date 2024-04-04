using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    #region ����
    [Header("=====> ���� ��� <=====")]
    [SerializeField] private GameObject weaponObject;

    [Header("=====> �÷��̾� ���� <=====")]
    [SerializeField] private float maxHp;
    [SerializeField] public int luck;

    private Dictionary<eEquipType, GameObject> weaponDict = new Dictionary<eEquipType, GameObject>();
    private Animator animator;
    #endregion // ����

    #region ������Ƽ
    public List<Weapon> WeaponList { get; set; } = new List<Weapon>();
    public float CurrentHp;
    #endregion // ������Ƽ

    #region �Լ�
    /** �ʱ�ȭ */
    private void Awake()
    {
        animator = GetComponent<Animator>();

        // ���� ü�� ����
        CurrentHp = maxHp;

        InitWeapon();
        //ActiveAddWeapon(eEquipType.SubmachineGun);
    }

    /** �������� �޴´� */
    public void TakeDamage(float damage)
    {
        animator.SetTrigger("hitTrigger");

        CurrentHp -= damage;

        if (CurrentHp <= 0)
        {
            // ����
            Die();
        }
    }

    /** �÷��̾� ���� */
    private void Die()
    {

    }

    /** ���� ������ ���� �� ��ųʸ��� �߰��Ѵ� */
    private void InitWeapon()
    {
        foreach(Transform weapon in weaponObject.transform)
        {
            var playerweapon = weapon.GetComponent<Weapon>();
            playerweapon.Init();
            weaponDict.Add(playerweapon.EquipType, weapon.gameObject);
        }
    }

    /** Ȱ��ȭ �� ���⸦ ���Ѵ� */
    public void ActiveAddWeapon(eEquipType type)
    {
        if (weaponDict.ContainsKey(type))
        {
            GameObject weapon = weaponDict[type];
            weapon.SetActive(true);
            WeaponList.Add(weapon.GetComponent<Weapon>());
        }
    }
    #endregion // �Լ�
}
