using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SelectUpgradeButtonUI;
using static UnityEditor.Progress;

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

    private float percent = 0f;
    private int count = 0;
    #endregion // 변수

    #region 함수
    private void InitData()
    {
        // TODO : 무기 이미지 추가해야됨
        upgradeNameText.text = weapon.WeaponName;
        upgradeDescText.text = weapon.WeaponDesc;
    }

    private void luckTest()
    {
        // 랜덤 숫자가 아이템의 드랍 확률보다 작거나 같을경우
        if (Random.Range(0, 21) <= playerMain.luck)
        {
            // 해당 아이템을 추가한다
            // 퍼센트 증가
        }
    }

    public void test()
    {
        int randomType = Random.Range((int)eUpgradeType.None, (int)eUpgradeType.Max_Value);
        upgradeType = (eUpgradeType)randomType;

        switch (upgradeType)
        {
            case eUpgradeType.None:
                upgradeInfoText.text = "아무것도 없다";
                break;
            case eUpgradeType.Damage:
                upgradeInfoText.text = "무기 데미지";
                break;
            case eUpgradeType.Reload:
                upgradeInfoText.text = "무기 재장전";
                break;
            case eUpgradeType.Count:
                upgradeInfoText.text = "무기 투사체 수";
                break;
        }
    }

    private void OnClickUpgrade()
    {
        switch (upgradeType)
        {
            case eUpgradeType.None:
                upgradeInfoText.text = "아무것도 없다";
                break;
            case eUpgradeType.Damage:
                break;
            case eUpgradeType.Reload:
                upgradeInfoText.text = "무기 재장전";
                break;
            case eUpgradeType.Count:
                upgradeInfoText.text = "무기 투사체 수";
                break;
        }

        weapon.Level++;
    }
    #endregion // 함수
}
