using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    #region 변수
    [SerializeField] private List<GameObject> modelList = new List<GameObject>();
    [SerializeField] private List<EquipSelect> equipSelectList = new List<EquipSelect>();

    [Header("=====> PlayerPrefs 이름 확인 <=====")]
    [SerializeField] private string equipType_1 = "EquipType_1";
    [SerializeField] private string equipType_2 = "EquipType_2";
    [SerializeField] private string equipType_3 = "EquipType_3";

    private List<eEquipType> equipTypeList = new List<eEquipType>();
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
}
