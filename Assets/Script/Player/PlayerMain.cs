using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    #region ����
    [Header("=====> ���� ��� <=====")]
    [SerializeField] private GameObject weaponObject;

    [Header("=====> �÷��̾� ���� <=====")]
    [SerializeField] private PlayerDataSO playerDataSO;
    [SerializeField] private float maxHp; // �ִ� ü��
    [SerializeField] private float moveSpeed; // �����̴� �ӵ�
    [SerializeField] private float itemPickRange; // ������ ���� ����
    [SerializeField] private float expPercent; // ����ġ ����
    [SerializeField] private float exp; // ����ġ ��
    [SerializeField] private int maxLevel; // �ִ� ����
    [SerializeField] private int level; // ����
    [SerializeField] public int maxLuck; // �ִ� ���
    [SerializeField] public int luck; // ���

    private PlayerMovement playerMovement;

    private Dictionary<eEquipType, GameObject> weaponDict = new Dictionary<eEquipType, GameObject>();
    private Animator animator;
    private Rigidbody rigid;
    private CapsuleCollider capsuleCollider;

    private bool isDie = false;
    #endregion // ����

    #region ������Ƽ
    public List<Weapon> WeaponList { get; set; } = new List<Weapon>();
    public float CurrentHp;
    #endregion // ������Ƽ

    #region �Լ�
    /** �ʱ�ȭ */
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        Init();

        InitWeapon();
    }

    /** �÷��̾� ������ ���� */
    private void Init()
    {
        maxHp = playerDataSO.maxHp;
        moveSpeed = playerDataSO.moveSpeed;
        itemPickRange = playerDataSO.itemPickRange;
        expPercent = playerDataSO.expPercent;
        maxLevel = playerDataSO.maxLevel;
        maxLuck = playerDataSO.maxLuck;

        // TODO : ���� ���׷��̵� ���� ��������
        level = 0;
        luck = 0;
        exp = 0;

        CurrentHp = maxHp;
        playerMovement.moveSpeed = moveSpeed;
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
        // TODO : �׽�Ʈ�� �ڵ�
        if (isDie == true) { return; }

        isDie = true;
        animator.SetBool("isDie", true);

        // TODO : �߷� X, �ݶ��̴� isTrigger ���� �����ؾߵ� 
        rigid.useGravity = false;
        capsuleCollider.enabled = false;
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

    /** �÷��̾� ��� ��ġ�� ���� ���׷��̵� ��ġ�� �޶����� */
    public float LuckDicePercent()
    {
        if (UnityEngine.Random.Range(0, 101) < luck)
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
        if (UnityEngine.Random.Range(0, 101) < luck)
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

    public void MoveSpeedUpgrade(float increaseSpeed)
    {

    }

    public void MaxHpUpgrade(float increaseMaxHp)
    {

    }

    public void ItemPickRange(float increaseRange)
    {

    }

    public void LuckUpgrade(float increaseLuck)
    {

    }
    #endregion // �Լ�
}
