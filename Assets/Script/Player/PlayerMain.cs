using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMain : MonoBehaviour
{
    #region 변수
    [Header("=====> 장착 장비 <=====")]
    [SerializeField] private GameObject weaponObject;

    [Header("=====> 플레이어 데이터 <=====")]
    [SerializeField] private PlayerDataSO playerDataSO;

    [Header("=====> 플레이어 스텟 <=====")]
    [SerializeField] private int maxHp; // 최대 체력
    [SerializeField] private float moveSpeed; // 움직이는 속도
    [SerializeField] private float itemPickRange; // 아이템 수집 범위
    [SerializeField] private float[] expArray; // 경험치 통
    [SerializeField] private float expPercent; // 경험치 배율
    [SerializeField] private int maxLevel; // 최대 레벨
    [SerializeField] private int currentLevel; // 레벨
    [SerializeField] public int maxLuck; // 최대 행운
    [SerializeField] public int luck; // 행운

    [Header("=====> 플레이어 자석 <=====")]
    [SerializeField] private SphereCollider sphereCollider;

    private PlayerMovement playerMovement;

    private Dictionary<eEquipType, GameObject> weaponDict = new Dictionary<eEquipType, GameObject>();
    private Animator animator;
    private Rigidbody rigid;
    private CapsuleCollider capsuleCollider;

    private bool isDie = false;
    #endregion // 변수

    #region 프로퍼티
    public List<Weapon> WeaponList { get; set; } = new List<Weapon>(); // 장착 무기
    public int MaxHp => maxHp;
    public int CurrentHp { get; set; }
    public float[] ExpArray => expArray;
    public float CurrentExp { get; set; } = 0;
    public int CurrentLevel => currentLevel;
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
        maxLevel = playerDataSO.maxLevel;
        maxLuck = playerDataSO.maxLuck;
        playerDataSO.Stats();

        // TODO : 영구 업그레이드 적용 만들어야함
        currentLevel = 0;
        luck = 0;
        CurrentExp = 0;

        CurrentHp = maxHp;
        playerMovement.moveSpeed = moveSpeed;
        expArray = playerDataSO.expArray;

        sphereCollider.radius = itemPickRange;
    }

    /** 데미지를 받는다 */
    public void TakeDamage(float damage)
    {
        // 애니메이션
        animator.SetTrigger("hitTrigger");

        // TODO : 임시 변환
        CurrentHp -= Mathf.RoundToInt(damage);
        AudioManager.Inst.PlaySFX("PlayerHeatSFX");

        Debug.Log(CurrentHp);

        // 체력바 갱신
        GameManager.Instance.InGameUI.UpdateHpBar(maxHp, CurrentHp);

        if (CurrentHp <= 0)
        {
            // 죽음
            Die();
        }
    }

    /** 경험치를 얻는다 */
    public void GetExp(float getExp)
    {
        if (getExp <= 0) { return; }

        CurrentExp += getExp * (1f + expPercent);
        AudioManager.Inst.PlaySFX("ExpSFX");

        if (CurrentExp == ExpArray[currentLevel])
        {
            LevelUP();
        }

        // 경험치 바 업데이트
        GameManager.Instance.InGameUI.UpdateExpBar(expArray[currentLevel], CurrentExp);
    }

    /** 레벨업 */
    public void LevelUP()
    {
        // 레벨업
        AudioManager.Inst.PlaySFX("LevelUpSFX");
        currentLevel++;
        CurrentExp = 0;

        GameManager.Instance.InGameUI.UpdateLevelText(currentLevel);
        GameManager.Instance.InGameUI.UpdateExpBar(expArray[currentLevel], CurrentExp);
        GameManager.Instance.ShowUpgradeUI(true);
    }

    /** 플레이어 죽음 */
    private void Die()
    {
        if (isDie == true) { return; }

        isDie = true;
        animator.SetBool("isDie", true);

        AudioManager.Inst.PlaySFX("PlayerDieSFX");

        // TODO : 중력 X, 콜라이더 isTrigger 해제 설정해야됨 
        rigid.useGravity = false;
        capsuleCollider.enabled = false;

        Invoke("GameOverSound", 1f);
        Invoke("ChangeStarMenu", 3f);
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

    /** 플레이어 행운 수치에 따라 업그레이드 수치가 달라진다 */
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

    /** 이동속도 업그레이드 */
    public void MoveSpeedUpgrade(float increaseSpeed)
    {
        moveSpeed *= (1f + increaseSpeed);
        playerMovement.moveSpeed = moveSpeed;
    }

    /** 최대 체력 업그레이드 */
    public void MaxHpUpgrade(float increaseMaxHp)
    {
        float maxhp = maxHp;
        maxhp *= (1f + increaseMaxHp);

        int addHp = Mathf.RoundToInt(maxhp) - this.maxHp;

        this.maxHp = Mathf.RoundToInt(maxhp);
        CurrentHp += addHp;
    }

    /** 아이템 수집 범위 업그레이드 */
    public void ItemPickRange(float increaseRange)
    {
        itemPickRange *= (1f + increaseRange);
        sphereCollider.radius = itemPickRange;
    }

    /** 아이템 수집 범위 감소 */
    public void DeItemPickRange(float decreaseRange)
    {
        itemPickRange /= (1f + decreaseRange);
        sphereCollider.radius = itemPickRange;
    }

    /** 행운 업그레이드 */
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
    #endregion // 함수
}
