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
    #endregion // ����

    #region ������Ƽ
    public Weapon[] WeaponArray { get; set; }
    public float CurrentHp { get; set; }
    #endregion // ������Ƽ

    #region �Լ�
    /** �ʱ�ȭ */
    private void Awake()
    {
        // ���� ü�� ����
        CurrentHp = maxHp;

        test();

        // TODO : �׽�Ʈ
        if (weaponDict.ContainsKey(eEquipType.SubmachineGun))
        {
            GameObject asdf = weaponDict[eEquipType.SubmachineGun];
            //asdf.SetActive(true);
        }
    }

    private void test()
    {
        foreach(Transform weapon in weaponObject.transform)
        {
            var playerweapon = weapon.GetComponent<Weapon>();
            playerweapon.Init();
            weaponDict.Add(playerweapon.EquipType, weapon.gameObject);
        }
    }

    public void AddWeapon(eEquipType type)
    {
        
    }
    #endregion // �Լ�
}
