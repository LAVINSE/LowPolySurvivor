using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    #region ����
    [SerializeField] private List<GameObject> modelList = new List<GameObject>();
    [SerializeField] private List<EquipSelect> equipSelectList = new List<EquipSelect>();

    [Header("=====> PlayerPrefs �̸� Ȯ�� <=====")]
    [SerializeField] private string equipType_1 = "EquipType_1";
    [SerializeField] private string equipType_2 = "EquipType_2";
    [SerializeField] private string equipType_3 = "EquipType_3";

    private List<eEquipType> equipTypeList = new List<eEquipType>();
    private int selectionIndex = 0; // ĳ���� ����â, �⺻ �� 0 
    private int changeIndex = 0; // ��ư���� �� ����
    #endregion // ����

    #region ������Ƽ
    public static CharacterSelect Instance { get; private set; }
    #endregion // ������Ƽ

    /** �ʱ�ȭ */
    private void Awake()
    {
        Instance = this;
    }

    /** �ʱ�ȭ */
    private void Start()
    {
        foreach (GameObject models in modelList)
        {
            // �� ��Ȱ��ȭ
            models.gameObject.SetActive(false);
        }

        // ���õ� �� Ȱ��ȭ
        modelList[selectionIndex].SetActive(true);
    }

    /** ���� �����Ѵ� */
    private void SelectModel(int index)
    {
        // ��ġ�� ���� ���, ����
        if(index == selectionIndex) { return; }
        // ���� 0 �Ʒ� �̰ų�, �� �� ���� ���� ���, ����
        if(index < 0 || index >= modelList.Count) { return; }

        // ���� �� ��Ȱ��ȭ
        modelList[selectionIndex].SetActive(false);

        // ���� �� Ȱ��ȭ
        selectionIndex = index;
        modelList[selectionIndex].SetActive(true);
    }

    /** ���� ������ �����Ѵ� */
    public void ChangeScene()
    {
        if(EquipDataCheck() == false) { return; }

        // ���� �� ��ȣ ����
        PlayerPrefs.DeleteKey("CharacterIndex");
        // ���� �� ��ȣ ����
        PlayerPrefs.SetInt("CharacterIndex", selectionIndex);

        // TODO : ���������� �߰��Ǹ� ���� ����
        // �� ����
        SceneManager.LoadScene("MainScene");
    }

    /** ���� �����͸� �����ϰ� Ȯ���Ѵ� */
    private bool EquipDataCheck()
    {
        // ���� Ÿ�� �����͸� �����´�
        if(equiptype() == false) { return false; }

        if (equipTypeList.Count != 3) { Debug.Log(equipTypeList.Count); return false; }
        if (equipTypeList.Contains(eEquipType.None)) { return false; }

        // ���� ��� ������ ����
        PlayerPrefs.DeleteKey(equipType_1);
        PlayerPrefs.DeleteKey(equipType_2);
        PlayerPrefs.DeleteKey(equipType_3);

        PlayerPrefs.SetInt(equipType_1, (int)equipTypeList[0]);
        PlayerPrefs.SetInt(equipType_2, (int)equipTypeList[1]);
        PlayerPrefs.SetInt(equipType_3, (int)equipTypeList[2]);

        return true;
    }

    /** ���� ��ư */
    public void NextButton()
    {
        if (++changeIndex > modelList.Count - 1)
        {
            changeIndex = 0;
        }

        SelectModel(changeIndex);
    }

    /** ���� ��ư */
    public void PrevButton()
    {
        if (--changeIndex < 0)
        {
            changeIndex = modelList.Count - 1;
        }

        SelectModel(changeIndex);
    }

    /** ���� Ÿ�� �����͸� �����´� */
    private bool equiptype()
    {
        if(equipTypeList.Count > 0 && equipTypeList.Contains(eEquipType.None))
        {
            equipTypeList.Clear();
            return false;
        }

        for (int i = 0; i < equipSelectList.Count; ++i)
        {
            equipTypeList.Add(equipSelectList[i].equipType);
        }

        return true;
    }
}
