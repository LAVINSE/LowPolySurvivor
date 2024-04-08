using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    #region ����
    [SerializeField] private List<GameObject> modelList = new List<GameObject>();

    [SerializeField] private List<EquipSelect> equipSelectList = new List<EquipSelect>();

    public List<eEquipType> equipTypeList = new List<eEquipType>();

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
        // ���� �� ��ȣ ����
        PlayerPrefs.DeleteKey("CharacterIndex");
        // ���� �� ��ȣ ����
        PlayerPrefs.SetInt("CharacterIndex", selectionIndex);

        // �� ����
        SceneManager.LoadScene("MainScene");
    }

    /** ���� ��ư */
    public void NextButton()
    {
        if (++changeIndex > modelList.Count - 1)
        {
            changeIndex = 0;
        }

        SelectModel(changeIndex);
        Debug.Log(changeIndex);
    }

    /** ���� ��ư */
    public void PrevButton()
    {
        if (--changeIndex < 0)
        {
            changeIndex = modelList.Count - 1;
        }

        SelectModel(changeIndex);
        Debug.Log(changeIndex);
    }

    public void equiptype()
    {
        for (int i = 0; i < equipSelectList.Count; ++i)
        {
            equipTypeList.Add(equipSelectList[i].equipType);
        }
    }
}
