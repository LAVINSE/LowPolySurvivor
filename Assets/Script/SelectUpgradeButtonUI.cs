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

    #region ����
    [SerializeField] private eUpgradeType upgradeType = eUpgradeType.None;
    [SerializeField] private Image upgradeImg;
    [SerializeField] private TMP_Text upgradeNameText;
    [SerializeField] private TMP_Text upgradeDescText;
    [SerializeField] private TMP_Text upgradeInfoText;

    [SerializeField] private Weapon weapon;
    [SerializeField] private PlayerMain playerMain;

    private float percent = 0f;
    private int count = 0;
    #endregion // ����

    #region �Լ�
    private void InitData()
    {
        // TODO : ���� �̹��� �߰��ؾߵ�
        upgradeNameText.text = weapon.WeaponName;
        upgradeDescText.text = weapon.WeaponDesc;
    }

    private void luckTest()
    {
        // ���� ���ڰ� �������� ��� Ȯ������ �۰ų� �������
        if (Random.Range(0, 21) <= playerMain.luck)
        {
            // �ش� �������� �߰��Ѵ�
            // �ۼ�Ʈ ����
        }
    }

    public void test()
    {
        int randomType = Random.Range((int)eUpgradeType.None, (int)eUpgradeType.Max_Value);
        upgradeType = (eUpgradeType)randomType;

        switch (upgradeType)
        {
            case eUpgradeType.None:
                upgradeInfoText.text = "�ƹ��͵� ����";
                break;
            case eUpgradeType.Damage:
                upgradeInfoText.text = "���� ������";
                break;
            case eUpgradeType.Reload:
                upgradeInfoText.text = "���� ������";
                break;
            case eUpgradeType.Count:
                upgradeInfoText.text = "���� ����ü ��";
                break;
        }
    }

    private void OnClickUpgrade()
    {
        switch (upgradeType)
        {
            case eUpgradeType.None:
                upgradeInfoText.text = "�ƹ��͵� ����";
                break;
            case eUpgradeType.Damage:
                break;
            case eUpgradeType.Reload:
                upgradeInfoText.text = "���� ������";
                break;
            case eUpgradeType.Count:
                upgradeInfoText.text = "���� ����ü ��";
                break;
        }

        weapon.Level++;
    }
    #endregion // �Լ�
}
