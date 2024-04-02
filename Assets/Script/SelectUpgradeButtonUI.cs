using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectUpgradeButtonUI : MonoBehaviour
{
    public enum eUpgradeType
    { 
        None,
        Ammo,
        Count,
        Damage,
        Reload, 
        Range,
        Rate,
        Max_Value
    }

    #region 변수
    [SerializeField] private eUpgradeType upgradeType = eUpgradeType.None;
    [SerializeField] private Image upgradeImg;
    [SerializeField] private TMP_Text upgradeNameText;
    [SerializeField] private TMP_Text upgradeDescText;
    [SerializeField] private TMP_Text upgradeInfoText;

    [SerializeField] private Weapon weapon;
    [SerializeField] private PlayerMain playerMain;

    private float damagePercent = 0;
    private float reloadPercent = 0;
    private float rangePercent = 0;
    private float ratePercent = 0;
    private int Ammo = 3;
    private int count = 1;
    #endregion // 변수

    #region 함수
    /** 이미지 기본 설정 */
    private void InitData()
    {
        // TODO : 무기 이미지 추가해야됨
        upgradeNameText.text = weapon.WeaponName;
        upgradeDescText.text = weapon.WeaponDesc;
    }

    /** 버튼 기본설정 */
    public void InitButton()
    {
        int randomType = Random.Range((int)eUpgradeType.None, (int)eUpgradeType.Max_Value);
        upgradeType = (eUpgradeType)randomType;

        if(weapon.Level >= weapon.MaxLevel)
        {
            upgradeType = eUpgradeType.None;
        }

        switch (upgradeType)
        {
            case eUpgradeType.None:
                // TODO : 로직 추가 예정
                upgradeInfoText.text = "아무것도 없다";
                break;
            case eUpgradeType.Ammo:
                InitData();
                upgradeInfoText.text = $" 탄창 {Ammo} 증가 ";
                break;
            case eUpgradeType.Count:
                InitData();
                upgradeInfoText.text = $"무기 투사체 수 {count} 증가";
                break;
            case eUpgradeType.Damage:
                InitData();
                damagePercent = weapon.LuckDice();
                upgradeInfoText.text = $"무기 데미지 {damagePercent * 10} 증가";
                break;
            case eUpgradeType.Reload:
                InitData();
                reloadPercent = weapon.LuckDice();
                upgradeInfoText.text = $"무기 재장전 {reloadPercent * 10} 증가";
                break;
            case eUpgradeType.Range:
                InitData();
                rangePercent = weapon.LuckDice();
                upgradeInfoText.text = $"무기 사거리 {rangePercent * 10} 증가";
                break;
            case eUpgradeType.Rate:
                InitData();
                ratePercent = weapon.LuckDice();
                upgradeInfoText.text = $"무기 연사속도 {ratePercent * 10} 증가";
                break;
        }
    } 

    /** 버튼을 클릭했을때 발생하는 업그레이드 */
    public void OnClickUpgrade()
    {
        switch (upgradeType)
        {
            case eUpgradeType.None:
                // TODO : 로직 추가 예정
                break;
            case eUpgradeType.Ammo:
                weapon.AmmoUpgrade(Ammo);
                break;
            case eUpgradeType.Count:
                weapon.CountUpgrade(count);
                break;
            case eUpgradeType.Damage:
                weapon.DamageUpgrade(damagePercent);
                break;
            case eUpgradeType.Reload:
                weapon.ReloadTimeUpgrade(reloadPercent);
                break;
            case eUpgradeType.Range:
                weapon.RangeUpgrade(rangePercent);
                break;
            case eUpgradeType.Rate:
                weapon.RateUpgrade(ratePercent);
                break;
        }

        weapon.LevelUpgrade();
    }
    #endregion // 함수
}
