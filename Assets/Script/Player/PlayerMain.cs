using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMain : MonoBehaviour
{
    #region ����
    [Header("=====> ���� ��� <=====")]
    [SerializeField] private GameObject weaponObject;

    [Header("=====> �÷��̾� ������ <=====")]
    [SerializeField] private PlayerDataSO playerDataSO;

    [Header("=====> �÷��̾� ���� <=====")]
    [SerializeField] private int maxHp; // �ִ� ü��
    [SerializeField] private float moveSpeed; // �����̴� �ӵ�
    [SerializeField] private float itemPickRange; // ������ ���� ����
    [SerializeField] private float[] expArray; // ����ġ ��
    [SerializeField] private float expPercent; // ����ġ ����
    [SerializeField] private int maxLevel; // �ִ� ����
    [SerializeField] private int currentLevel; // ����
    [SerializeField] public int maxLuck; // �ִ� ���
    [SerializeField] public int luck; // ���

    [Header("=====> �÷��̾� �ڼ� <=====")]
    [SerializeField] private SphereCollider sphereCollider;

    private PlayerMovement playerMovement;

    private Dictionary<eEquipType, GameObject> weaponDict = new Dictionary<eEquipType, GameObject>();
    private Animator animator;
    private Rigidbody rigid;
    private CapsuleCollider capsuleCollider;

    private bool isDie = false;
    #endregion // ����

    #region ������Ƽ
    public List<Weapon> WeaponList { get; set; } = new List<Weapon>(); // ���� ����
    public int MaxHp => maxHp;
    public int CurrentHp { get; set; }
    public float[] ExpArray => expArray;
    public float CurrentExp { get; set; } = 0;
    public int CurrentLevel => currentLevel;
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
        playerDataSO.Stats();

        // TODO : ���� ���׷��̵� ���� ��������
        currentLevel = 0;
        luck = 0;
        CurrentExp = 0;

        CurrentHp = maxHp;
        playerMovement.moveSpeed = moveSpeed;
        expArray = playerDataSO.expArray;

        sphereCollider.radius = itemPickRange;
    }

    /** �������� �޴´� */
    public void TakeDamage(float damage)
    {
        // �ִϸ��̼�
        animator.SetTrigger("hitTrigger");

        // TODO : �ӽ� ��ȯ
        CurrentHp -= Mathf.RoundToInt(damage);
        AudioManager.Inst.PlaySFX("PlayerHeatSFX");

        Debug.Log(CurrentHp);

        // ü�¹� ����
        GameManager.Instance.InGameUI.UpdateHpBar(maxHp, CurrentHp);

        if (CurrentHp <= 0)
        {
            // ����
            Die();
        }
    }

    /** ����ġ�� ��´� */
    public void GetExp(float getExp)
    {
        if (getExp <= 0) { return; }

        CurrentExp += getExp * (1f + expPercent);
        AudioManager.Inst.PlaySFX("ExpSFX");

        if (CurrentExp == ExpArray[currentLevel])
        {
            LevelUP();
        }

        // ����ġ �� ������Ʈ
        GameManager.Instance.InGameUI.UpdateExpBar(expArray[currentLevel], CurrentExp);
    }

    /** ������ */
    public void LevelUP()
    {
        // ������
        AudioManager.Inst.PlaySFX("LevelUpSFX");
        currentLevel++;
        CurrentExp = 0;

        GameManager.Instance.InGameUI.UpdateLevelText(currentLevel);
        GameManager.Instance.InGameUI.UpdateExpBar(expArray[currentLevel], CurrentExp);
        GameManager.Instance.ShowUpgradeUI(true);
    }

    /** �÷��̾� ���� */
    private void Die()
    {
        if (isDie == true) { return; }

        isDie = true;
        animator.SetBool("isDie", true);

        AudioManager.Inst.PlaySFX("PlayerDieSFX");

        // TODO : �߷� X, �ݶ��̴� isTrigger ���� �����ؾߵ� 
        rigid.useGravity = false;
        capsuleCollider.enabled = false;

        Invoke("GameOverSound", 1f);
        Invoke("ChangeStarMenu", 3f);
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

    /** �̵��ӵ� ���׷��̵� */
    public void MoveSpeedUpgrade(float increaseSpeed)
    {
        moveSpeed *= (1f + increaseSpeed);
        playerMovement.moveSpeed = moveSpeed;
    }

    /** �ִ� ü�� ���׷��̵� */
    public void MaxHpUpgrade(float increaseMaxHp)
    {
        float maxhp = maxHp;
        maxhp *= (1f + increaseMaxHp);

        int addHp = Mathf.RoundToInt(maxhp) - this.maxHp;

        this.maxHp = Mathf.RoundToInt(maxhp);
        CurrentHp += addHp;
    }

    /** ������ ���� ���� ���׷��̵� */
    public void ItemPickRange(float increaseRange)
    {
        itemPickRange *= (1f + increaseRange);
        sphereCollider.radius = itemPickRange;
    }

    /** ������ ���� ���� ���� */
    public void DeItemPickRange(float decreaseRange)
    {
        itemPickRange /= (1f + decreaseRange);
        sphereCollider.radius = itemPickRange;
    }

    /** ��� ���׷��̵� */
    public void LuckUpgrade(float increaseLuck)
    {
        float luck = this.luck;
        luck *= (1f + increaseLuck);

        this.luck = Mathf.RoundToInt(luck);
    }

    private void GameOverSound()
    {
        AudioManager.Inst.PlaySFX("GameOverSFX");
    }

    private void ChangeStarMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
    #endregion // �Լ�
}
