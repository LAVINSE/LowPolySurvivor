using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    #region ����
    [SerializeField] private List<GameObject> modelList = new List<GameObject>();
    #endregion // ����

    #region ������Ƽ
    public static CharacterSelect Instance { get; private set; }
    public int selectionIndex { get; set; } = 0; // ĳ���� ����â, �⺻ �� 0 
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

        // TODO : �׽�Ʈ �� ����
        SelectModel(1);
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
}
