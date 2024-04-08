using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    #region 변수
    [SerializeField] private List<GameObject> modelList = new List<GameObject>();

    [SerializeField] private List<EquipSelect> equipSelectList = new List<EquipSelect>();

    public List<eEquipType> equipTypeList = new List<eEquipType>();

    private int selectionIndex = 0; // 캐릭터 선택창, 기본 값 0 
    private int changeIndex = 0; // 버튼으로 값 변경
    #endregion // 변수

    #region 프로퍼티
    public static CharacterSelect Instance { get; private set; }
    #endregion // 프로퍼티

    /** 초기화 */
    private void Awake()
    {
        Instance = this;
    }

    /** 초기화 */
    private void Start()
    {
        foreach (GameObject models in modelList)
        {
            // 모델 비활성화
            models.gameObject.SetActive(false);
        }

        // 선택된 모델 활성화
        modelList[selectionIndex].SetActive(true);
    }

    /** 모델을 선택한다 */
    private void SelectModel(int index)
    {
        // 위치가 값을 경우, 종료
        if(index == selectionIndex) { return; }
        // 값이 0 아래 이거나, 모델 수 보다 많을 경우, 종료
        if(index < 0 || index >= modelList.Count) { return; }

        // 이전 모델 비활성화
        modelList[selectionIndex].SetActive(false);

        // 현재 모델 활성화
        selectionIndex = index;
        modelList[selectionIndex].SetActive(true);
    }

    /** 게임 씬으로 변경한다 */
    public void ChangeScene()
    {
        // 이전 모델 번호 삭제
        PlayerPrefs.DeleteKey("CharacterIndex");
        // 현재 모델 번호 저장
        PlayerPrefs.SetInt("CharacterIndex", selectionIndex);

        // 씬 변경
        SceneManager.LoadScene("MainScene");
    }

    /** 다음 버튼 */
    public void NextButton()
    {
        if (++changeIndex > modelList.Count - 1)
        {
            changeIndex = 0;
        }

        SelectModel(changeIndex);
        Debug.Log(changeIndex);
    }

    /** 이전 버튼 */
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
