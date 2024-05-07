using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum eUpgradeWeaponType
{
    Count,
    Ammo,
    Damage,
    Reload,
    Range,
    Rate,
    Max_Value
}

public enum eUpgradePlayerType
{
    Luck,
    MoveSpeed,
    MaxHp,
    itemPickRange,
    Max_Value,
}

public class SelectUpgradeButtonUI : MonoBehaviour
{
    #region 변수
    [SerializeField] private eUpgradeWeaponType upgradeWeaponType;
    [SerializeField] private eUpgradePlayerType upgradePlayerType;
    [SerializeField] private Image upgradeImg;
    [SerializeField] private TMP_Text upgradeNameText;
    [SerializeField] private TMP_Text upgradeDescText;
    [SerializeField] private TMP_Text upgradeInfoText;

    private int randomIndex = -1;

    private float weaponPercent = 0;
    private int weaponInt = 0;

    private float playerPercent = 0;
    private int playerInt = 0;
    #endregion // 변수

    #region 프로퍼티
    public Weapon Weapon { get; set; }
    public PlayerMain PlayerMain { get; set; }
    #endregion // 프로퍼티

    #region 함수
    public void ActiveUpgrade()
    {
        GameManager.Instance.ShowUpgradeUI(false);
    }

    /** 플레이어 이미지 기본 설정 */
    private void InitPlayerData()
    {
        upgradeImg.sprite = null;
        upgradeNameText.text = "캐릭터 스텟";
        upgradeDescText.text = "캐릭터 스텟을 업그레이드 합니다";
    }

    /** 무기 이미지 기본 설정 */
    private void InitWeaponData()
    {
        upgradeImg.sprite = Weapon.WeaponSprite;
        upgradeNameText.text = Weapon.WeaponName;
        upgradeDescText.text = Weapon.WeaponDesc;
    }

    /** 버튼 기본설정 */
    public void InitButton()
    {
        int randomWeaponType = Random.Range((int)eUpgradeWeaponType.Count, (int)eUpgradeWeaponType.Max_Value);
        int randomPlayerType = Random.Range((int)eUpgradePlayerType.Luck, (int)eUpgradePlayerType.Max_Value);

        randomIndex = Random.Range(0, 2);

        upgradeWeaponType = (eUpgradeWeaponType)randomWeaponType;
        upgradePlayerType = (eUpgradePlayerType)randomPlayerType;

        // 현재 무기 레벨이 최대 레벨일 경우
        if(Weapon.Level >= Weapon.MaxLevel)
        {
            // 캐릭 스텟 업그레이드
            randomIndex = 1;
        }

        // 현재 무기 개수가 최대 개수일 경우
        if(Weapon.WeaponCount >= Weapon.MaxWeaponCount)
        {
            int randomWeaponTypeRest = Random.Range((int)eUpgradeWeaponType.Ammo, (int)eUpgradeWeaponType.Max_Value);
            upgradeWeaponType = (eUpgradeWeaponType)randomWeaponTypeRest;
        }

        // 현재 행운수치가 최대 수치일 경우
        if(PlayerMain.luck >= PlayerMain.maxLuck)
        {
            int randomPlayerTypeRest = Random.Range((int)eUpgradePlayerType.MoveSpeed, (int)eUpgradePlayerType.Max_Value);
            upgradePlayerType = (eUpgradePlayerType)randomPlayerTypeRest;
        }

        // 무기 업그레이드
        if (randomIndex == 0)
        {
            InitWeaponData();
            weaponPercent = Weapon.LuckDicePercent();
            weaponInt = Weapon.LuckDiceInt();

            switch (upgradeWeaponType)
            {
                case eUpgradeWeaponType.Count:
                    upgradeInfoText.text = $"무기 투사체 수 {1} 증가";
                    break;
                case eUpgradeWeaponType.Ammo:
                    upgradeInfoText.text = $" 탄창 {weaponInt} 증가 ";
                    break; 
                case eUpgradeWeaponType.Damage:
                    upgradeInfoText.text = $"무기 데미지 {weaponPercent * 10} 증가";
                    break;
                case eUpgradeWeaponType.Reload:
                    upgradeInfoText.text = $"무기 재장전 {weaponPercent * 10} 증가";
                    break;
                case eUpgradeWeaponType.Range:
                    upgradeInfoText.text = $"무기 사거리 {weaponPercent * 10} 증가";
                    break;
                case eUpgradeWeaponType.Rate:
                    upgradeInfoText.text = $"무기 연사속도 {weaponPercent * 10} 증가";
                    break;
            }
        }
        // 캐릭 스텟 업그레이드
        else if (randomIndex == 1)
        {
            InitPlayerData();
            playerPercent = PlayerMain.LuckDicePercent();
            playerInt = PlayerMain.LuckDiceInt();

            switch (upgradePlayerType)
            {
                case eUpgradePlayerType.Luck:
                    upgradeInfoText.text = $"행운 {playerPercent * 10} 증가";
                    break;
                case eUpgradePlayerType.MoveSpeed:
                    upgradeInfoText.text = $"이동속도 {playerPercent * 10} 증가";
                    break;
                case eUpgradePlayerType.MaxHp:
                    upgradeInfoText.text = $"최대체력 {playerPercent * 10} 증가";
                    break;
                case eUpgradePlayerType.itemPickRange:
                    upgradeInfoText.text = $"아이템수집 범위 {playerPercent * 10} 증가";
                    break; 
            }
        }
        
    } 

    /** 버튼을 클릭했을때 발생하는 업그레이드 */
    public void OnClickUpgrade()
    {
        if(randomIndex == 0)
        {
            // 무기 업글
            switch (upgradeWeaponType)
            {
                case eUpgradeWeaponType.Ammo:
                    Weapon.AmmoUpgrade(weaponInt);
                    break;
                case eUpgradeWeaponType.Count:
                    Weapon.CountUpgrade(1);
                    break;
                case eUpgradeWeaponType.Damage:
                    Weapon.DamageUpgrade(weaponPercent);
                    break;
                case eUpgradeWeaponType.Reload:
                    Weapon.ReloadTimeUpgrade(weaponPercent);
                    break;
                case eUpgradeWeaponType.Range:
                    Weapon.RangeUpgrade(weaponPercent);
                    break;
                case eUpgradeWeaponType.Rate:
                    Weapon.RateUpgrade(weaponPercent);
                    break;
            }

            Weapon.LevelUpgrade();
        }
        else if(randomIndex == 1)
        {
            // 플레이어 스텟 업글
            switch (upgradePlayerType)
            {
                case eUpgradePlayerType.MoveSpeed:
                    PlayerMain.MoveSpeedUpgrade(playerPercent);
                    break;
                case eUpgradePlayerType.MaxHp:
                    PlayerMain.MaxHpUpgrade(playerPercent);
                    break;
                case eUpgradePlayerType.itemPickRange:
                    PlayerMain.ItemPickRange(playerPercent);
                    break;
                case eUpgradePlayerType.Luck:
                    PlayerMain.LuckUpgrade(playerInt);
                    break;
            }
        }  
    }
    #endregion // 함수
}
