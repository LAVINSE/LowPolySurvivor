using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    #region ����
    [Header("=====> ĳ���� �̹��� <=====")]
    [SerializeField] private Image characterImg = null;

    [Header("=====> ü�¹� <=====")]
    [SerializeField] private Image hpBarImg = null;

    [Header("=====> ���� ü�¹� <=====")]
    [SerializeField] private GameObject bossHpbarObject = null;
    [SerializeField] private Image bosshpbarImg = null;

    [Header("=====> ����ġ �� <=====")]
    [SerializeField] private Image expBarImg = null;

    [Header("=====> ���� <=====")]
    [SerializeField] private TMP_Text levelText = null;

    [Header("=====> ��� ���� <=====")]
    [SerializeField] private Button equipSlot_1 = null;
    [SerializeField] private Button equipSlot_2 = null;
    [SerializeField] private Button equipSlot_3 = null;

    [Header("=====> Ÿ�̸� <=====")]
    [SerializeField] private TMP_Text timerText = null;
    #endregion // ����

    #region �Լ�
    /** ��� ������ ���� �̹����� �����Ѵ� */
    public void InitEquipSlot(List<Weapon> weaponList)
    {
        if(weaponList.Count != 3) { Debug.Log(" ��ϵ� ���Ⱑ 3���� �ƴ� "); return; }

        equipSlot_1.GetComponent<EquipSlotUI>().itemImg.sprite = weaponList[0].WeaponSprite;
        equipSlot_2.GetComponent<EquipSlotUI>().itemImg.sprite = weaponList[1].WeaponSprite;
        equipSlot_3.GetComponent<EquipSlotUI>().itemImg.sprite = weaponList[2].WeaponSprite;
    }

    /** �÷��̾� ü�¹� �̹����� �����Ѵ� */
    public void UpdateHpBar(int maxHp, int currentHp)
    {
        hpBarImg.fillAmount = currentHp / maxHp;
    }

    /** �÷��̾� ����ġ�� �̹����� �����Ѵ� */
    public void UpdateExpBar(float maxExp, float currentExp)
    {
        expBarImg.fillAmount = currentExp / maxExp;
    }

    /** �÷��̾� ���� �ؽ�Ʈ�� �����Ѵ� */
    public void UpdateLevelText(int currentLevel)
    {
        levelText.text = $"{currentLevel} LV";
    }

    /** �÷��̾� �̹����� �����Ѵ� */
    public void InitCharacterImg(Image characterImg)
    {
        this.characterImg = characterImg;
    }

    /** Ÿ�̸Ӹ� �����Ѵ� */
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

    /** ���� ü�¹ٸ� Ȱ��ȭ/��Ȱ��ȭ �Ѵ� */
    public void ActiveBossHpbar(bool isActive)
    {
        bossHpbarObject.SetActive(isActive);
    }

    /** ���� ü�¹ٸ� �����Ѵ� */
    public void BossHpBarUpdate(float maxExp, float currentExp)
    {
        expBarImg.fillAmount = currentExp / maxExp;
    }
    #endregion // �Լ�
}
