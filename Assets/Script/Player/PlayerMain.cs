using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    #region 변수
    [Header("=====> 장착 장비 <=====")]
    [SerializeField] private GameObject weaponObject;

    [Header("=====> 플레이어 정보 <=====")]
    [SerializeField] private PlayerDataSO playerDataSO;
    [SerializeField] private float maxHp;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float itemPickRange;
    [SerializeField] private float expPercent;
    [SerializeField] public int maxLuck;
    [SerializeField] public int luck;

    private PlayerMovement playerMovement;

    private Dictionary<eEquipType, GameObject> weaponDict = new Dictionary<eEquipType, GameObject>();
    private Animator animator;
    private Rigidbody rigid;
    private CapsuleCollider capsuleCollider;

    private bool isDie = false;
    #endregion // 변수

    #region 프로퍼티
    public List<Weapon> WeaponList { get; set; } = new List<Weapon>();
    public float CurrentHp;
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        Init();

        InitWeapon();
    }

    /** 플레이어 데이터 세팅 */
    private void Init()
    {
        maxHp = playerDataSO.maxHp;
        moveSpeed = playerDataSO.moveSpeed;
        itemPickRange = playerDataSO.itemPickRange;
        expPercent = playerDataSO.expPercent;
        luck = playerDataSO.luck;
        maxLuck = playerDataSO.maxLuck;

        CurrentHp = maxHp;
        playerMovement.moveSpeed = moveSpeed;
    }

    /** 데미지를 받는다 */
    public void TakeDamage(float damage)
    {
        animator.SetTrigger("hitTrigger");

        CurrentHp -= damage;

        if (CurrentHp <= 0)
        {
            // 죽음
            Die();
        }
    }

    /** 플레이어 죽음 */
    private void Die()
    {
        // TODO : 테스트용 코드
        if (isDie == true) { return; }

        isDie = true;
        animator.SetBool("isDie", true);

        // TODO : 중력 X, 콜라이더 isTrigger 해제 설정해야됨 
        rigid.useGravity = false;
        capsuleCollider.enabled = false;
    }

    /** 무기 데이터 설정 및 딕셔너리에 추가한다 */
    private void InitWeapon()
    {
        foreach(Transform weapon in weaponObject.transform)
        {
            var playerweapon = weapon.GetComponent<Weapon>();
            playerweapon.Init();
            weaponDict.Add(playerweapon.EquipType, weapon.gameObject);
        }
    }

    /** 활성화 할 무기를 정한다 */
    public void ActiveAddWeapon(eEquipType type)
    {
        if (weaponDict.ContainsKey(type))
        {
            GameObject weapon = weaponDict[type];
            weapon.SetActive(true);
            WeaponList.Add(weapon.GetComponent<Weapon>());
        }
    }

    /** 플레이어 행운 수치에 따라 업그레이드 수치가 달라진다 */
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
    #endregion // 함수
}
