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
        Damage,
        Reload,
        Count,
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

    [SerializeField] private int minNumber = 2;
    [SerializeField] private int MaxNumber = 6;

    private float damagePercent = 0;
    private float reloadPercent = 0;
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

    /** 플레이어 행운 수치에 따라 업그레이드 수치가 달라진다 */
    private int luckDice()
    {
        if(Random.Range(0, 101) < playerMain.luck)
        {
            int dice = Random.Range(minNumber, MaxNumber);

            switch (dice)
            {
                case 2:
                    return 2;
                case 3:
                    return 3;
                case 4:
                    return 4;
                case 5:
                    return 5;
            }
        }

        return 1;
    }

    /** 버튼 기본설정 */
    public void InitButton()
    {
        int randomType = Random.Range((int)eUpgradeType.None, (int)eUpgradeType.Max_Value);
        upgradeType = (eUpgradeType)randomType;

        if(weapon.Level > weapon.MaxLevel)
        {
            upgradeType = eUpgradeType.None;
        }

        switch (upgradeType)
        {
            case eUpgradeType.None:
                upgradeInfoText.text = "아무것도 없다";
                break;
            case eUpgradeType.Damage:
                InitData();
                damagePercent = luckDice() * 10;
                upgradeInfoText.text = $"무기 데미지 {damagePercent} 증가";
                break;
            case eUpgradeType.Reload:
                InitData();
                reloadPercent = luckDice() * 10;
                upgradeInfoText.text = $"무기 재장전 {reloadPercent} 증가";
                break;
            case eUpgradeType.Count:
                InitData();
                upgradeInfoText.text = $"무기 투사체 수 {count} 증가";
                break;
        }
    } 

    /** 버튼을 클릭했을때 발생하는 업그레이드 */
    private void OnClickUpgrade()
    {
        switch (upgradeType)
        {
            case eUpgradeType.None:
                upgradeInfoText.text = "아무것도 없다";
                break;
            case eUpgradeType.Damage:
                weapon.Damage *= damagePercent;
                break;
            case eUpgradeType.Reload:
                weapon.ReloadTime *= reloadPercent;
                break;
            case eUpgradeType.Count:
                weapon.Count += count;
                break;
        }

        weapon.Level++;
    }
    #endregion // 함수
}
