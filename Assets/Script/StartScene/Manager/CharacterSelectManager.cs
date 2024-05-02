using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour
{
    #region 변수
    [Header("=====> 캐릭터 <=====")]
    [SerializeField] private List<GameObject> modelList = new List<GameObject>();

    [Header("=====> 장비 선택 <=====")]
    [SerializeField] private List<EquipSelect> equipSelectList = new List<EquipSelect>();

    [Header("=====> PlayerPrefs 이름 확인 <=====")]
    [SerializeField] private string equipType_1 = "EquipType_1";
    [SerializeField] private string equipType_2 = "EquipType_2";
    [SerializeField] private string equipType_3 = "EquipType_3";

    [Header("=====> 스테이지 이름 <=====")]
    [SerializeField] private string stageName = string.Empty;

    [Header("=====> 메인 메뉴 UI <=====")]
    [SerializeField] private GameObject mainMenuObject;
    [SerializeField] private Image mainMenuBackground;
    [SerializeField] private GameObject selectStageGroup;
    [SerializeField] private GameObject buttonGroup;
    [SerializeField] private GameObject leaveButton;

    [Header("=====> 케릭터 선택 UI, Object <=====")]
    [SerializeField] private GameObject characterSelectUI;
    [SerializeField] private GameObject characterSelectObject;
    [SerializeField] private GameObject characterSelectStaticObject;

    private List<eEquipType> equipTypeList = new List<eEquipType>();
    private int selectionIndex = 0; // 캐릭터 선택창, 기본 값 0 
    private int changeIndex = 0; // 버튼으로 값 변경
    private bool isLock = false;
    #endregion // 변수

    #region 프로퍼티
    #endregion // 프로퍼티

    /** 초기화 */
    private void Awake()
    {
        
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
        if(EquipDataCheck() == false) { return; }

        // 이전 모델 번호 삭제
        PlayerPrefs.DeleteKey("CharacterIndex");
        // 현재 모델 번호 저장
        PlayerPrefs.SetInt("CharacterIndex", selectionIndex);

        // TODO : 스테이지가 추가되면 변경 예정
        // 씬 변경
        SceneManager.LoadScene("MainScene");
    }

    /** 무기 데이터를 저장하고 확인한다 */
    private bool EquipDataCheck()
    {
        // 무기 타입 데이터를 가져온다
        if(equiptype() == false) { return false; }

        if (equipTypeList.Count != 3) { Debug.Log(equipTypeList.Count); return false; }
        if (equipTypeList.Contains(eEquipType.None)) { return false; }

        // 이전 장비 데이터 삭제
        PlayerPrefs.DeleteKey(equipType_1);
        PlayerPrefs.DeleteKey(equipType_2);
        PlayerPrefs.DeleteKey(equipType_3);

        PlayerPrefs.SetInt(equipType_1, (int)equipTypeList[0]);
        PlayerPrefs.SetInt(equipType_2, (int)equipTypeList[1]);
        PlayerPrefs.SetInt(equipType_3, (int)equipTypeList[2]);

        return true;
    }

    /** 다음 버튼 */
    public void NextButton()
    {
        if (++changeIndex > modelList.Count - 1)
        {
            changeIndex = 0;
        }

        SelectModel(changeIndex);
    }

    /** 이전 버튼 */
    public void PrevButton()
    {
        if (--changeIndex < 0)
        {
            changeIndex = modelList.Count - 1;
        }

        SelectModel(changeIndex);
    }

    /** 무기 타입 데이터를 가져온다 */
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

    /** 버튼그룹과 스테이지 선택 메뉴를 활성화/비활성화한다 */
    private void ButtonSelectStageActive()
    {
        buttonGroup.SetActive(!buttonGroup.activeSelf);
        selectStageGroup.SetActive(!selectStageGroup.activeSelf);
    }

    /** 배경화면 투명도를 조절한다 */
    private void BackgroundTransparent(float percent)
    {
        Color color = mainMenuBackground.color;
        color.a = percent;
        mainMenuBackground.color = color;
    }

    /** 스테이지/나가기 버튼을 누른다 */
    public void SelectStageButton(float percent)
    {
        ButtonSelectStageActive();
        leaveButton.SetActive(!leaveButton.activeSelf);
        BackgroundTransparent(percent);
    }

    /** 스테이지를 선택한다 */
    public void SelectStage(string stageName)
    {
        if (isLock) { return; }
        this.stageName = stageName;
        selectStageGroup.SetActive(false);
        mainMenuObject.SetActive(false);
        characterSelectUI.SetActive(true);
        characterSelectStaticObject.SetActive(true);
        characterSelectObject.SetActive(true);
    }

    /** 스테이지가 잠금되어있는지 확인한다 */
    public void LockCheck(bool isLock)
    {
        this.isLock = isLock;
    }

    /** 게임 종료 */
    public void QuitUnityGame()
    {
        Application.Quit();
    }
}
