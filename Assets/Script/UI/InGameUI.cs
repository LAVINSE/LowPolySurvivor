using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    #region 변수
    [Header("=====> 캐릭터 이미지 <=====")]
    [SerializeField] private Image characterImg = null;

    [Header("=====> 체력바 <=====")]
    [SerializeField] private Image hpBarImg = null;

    [Header("=====> 보스 체력바 <=====")]
    [SerializeField] private GameObject bossHpbarObject = null;
    [SerializeField] private Image bosshpbarImg = null;

    [Header("=====> 경험치 바 <=====")]
    [SerializeField] private Image expBarImg = null;

    [Header("=====> 레벨 <=====")]
    [SerializeField] private TMP_Text levelText = null;

    [Header("=====> 장비 슬롯 <=====")]
    [SerializeField] private Button equipSlot_1 = null;
    [SerializeField] private Button equipSlot_2 = null;
    [SerializeField] private Button equipSlot_3 = null;

    [Header("=====> 타이머 <=====")]
    [SerializeField] private TMP_Text timerText = null;
    #endregion // 변수

    #region 함수
    /** 장비 아이템 슬롯 이미지를 설정한다 */
    public void InitEquipSlot(List<Weapon> weaponList)
    {
        if(weaponList.Count != 3) { Debug.Log(" 등록된 무기가 3개가 아님 "); return; }

        equipSlot_1.GetComponent<EquipSlotUI>().itemImg.sprite = weaponList[0].WeaponSprite;
        equipSlot_2.GetComponent<EquipSlotUI>().itemImg.sprite = weaponList[1].WeaponSprite;
        equipSlot_3.GetComponent<EquipSlotUI>().itemImg.sprite = weaponList[2].WeaponSprite;
    }

    /** 플레이어 체력바 이미지를 갱신한다 */
    public void UpdateHpBar(int maxHp, int currentHp)
    {
        hpBarImg.fillAmount = currentHp / maxHp;
    }

    /** 플레이어 경험치바 이미지를 갱신한다 */
    public void UpdateExpBar(float maxExp, float currentExp)
    {
        expBarImg.fillAmount = currentExp / maxExp;
    }

    /** 플레이어 레벨 텍스트를 갱신한다 */
    public void UpdateLevelText(int currentLevel)
    {
        levelText.text = $"{currentLevel} LV";
    }

    /** 플레이어 이미지를 설정한다 */
    public void InitCharacterImg(Image characterImg)
    {
        this.characterImg = characterImg;
    }

    /** 타이머를 갱신한다 */
    public void UpdateTimerText(float timer)
    {
        int min = Mathf.FloorToInt((timer % 3600) / 60);
        int second = Mathf.FloorToInt(timer % 60);

        if(min <= 0 && second <= 0)
        {
            timerText.gameObject.SetActive(false);
        }

        timerText.text = $"{min:00}:{second:00}";
    }

    /** 보스 체력바를 활성화/비활성화 한다 */
    public void ActiveBossHpbar(bool isActive)
    {
        bossHpbarObject.SetActive(isActive);
    }

    /** 보스 체력바를 갱신한다 */
    public void BossHpBarUpdate(float maxExp, float currentExp)
    {
        expBarImg.fillAmount = currentExp / maxExp;
    }
    #endregion // 함수
}
