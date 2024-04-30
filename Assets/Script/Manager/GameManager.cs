using Cinemachine;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region 변수
    [Header("=====> 소환 매니저 <=====")]
    [SerializeField] private SpawnManager spawnManager;

    [Header("=====> 플레이어 리스트 <=====")]
    [SerializeField] private List<GameObject> playerPrefabsList = new List<GameObject>();

    [Header("=====> 오브젝트 풀 <=====")]
    [SerializeField] private ObjectPoolManager poolManager;

    [Header("=====> 카메라 <=====")]
    [Tooltip(" 플레이어 추적 카메라 ")][SerializeField] private CinemachineVirtualCamera virtualCamera;
    [Tooltip(" 미니맵 카메라 ")][SerializeField] private MiniMapCamera miniMapCamera;

    [Header("=====> UI <=====")]
    [SerializeField] private InGameUI inGameUI;
    [SerializeField] private MiniMapUI miniMapUI;
    [SerializeField] private GameObject upgradeObject;
    [SerializeField] private SelectUpgradeButtonUI selectUpgradeButtonUI_1;
    [SerializeField] private SelectUpgradeButtonUI selectUpgradeButtonUI_2;
    [SerializeField] private SelectUpgradeButtonUI selectUpgradeButtonUI_3;

    [SerializeField] private int playerId = -1;
    #endregion // 변수

    #region 프로퍼티
    public static GameManager Instance { get; private set; }
    public ObjectPoolManager PoolManager => poolManager;
    public PlayerMain PlayerMain { get; private set; }
    public InGameUI InGameUI => inGameUI;

    public int EquipIndex_1 { get; private set; }
    public int EquipIndex_2 { get; private set; }
    public int EquipIndex_3 { get; private set; }
    #endregion // 프로퍼티

    #region 변수
    /** 초기화 */
    private void Awake()
    {
        Instance = this; 
    }

    private void Start()
    {
        // 캐릭터 선택 값이 있을 경우
        if (PlayerPrefs.HasKey("CharacterIndex"))
        {
            // 선택된 값을 가져온다
            playerId = PlayerPrefs.GetInt("CharacterIndex");

            // 캐릭터 생성
            GameObject playerObject = Instantiate(playerPrefabsList[playerId]);
            PlayerMain = playerObject.GetComponent<PlayerMain>();

            // 장비 슬롯 설정
            EquipIndex_1 = PlayerPrefs.GetInt("EquipType_1");
            EquipIndex_2 = PlayerPrefs.GetInt("EquipType_2");
            EquipIndex_3 = PlayerPrefs.GetInt("EquipType_3");

            // 장비 추가
            PlayerMain.ActiveAddWeapon((eEquipType)EquipIndex_1);
            PlayerMain.ActiveAddWeapon((eEquipType)EquipIndex_2);
            PlayerMain.ActiveAddWeapon((eEquipType)EquipIndex_3);

            // 카메라 설정
            virtualCamera.Follow = playerObject.transform;
            virtualCamera.GetComponent<TransparentObjectCamera>().Player = playerObject;

            // UI 설정
            inGameUI.InitEquipSlot(PlayerMain.WeaponList);
            inGameUI.UpdateHpBar(PlayerMain.MaxHp, PlayerMain.CurrentHp) ;
            inGameUI.UpdateExpBar(PlayerMain.ExpArray[PlayerMain.CurrentLevel], PlayerMain.CurrentExp);
            inGameUI.UpdateLevelText(PlayerMain.CurrentLevel);

            // TODO : 스테이지 이름
            miniMapCamera.Target = PlayerMain.transform;
            miniMapUI.MiniMapCamera = miniMapCamera.GetComponent<Camera>();
            miniMapUI.Init("맵 이름");

            // 업그레이드 UI 설정
            selectUpgradeButtonUI_1.PlayerMain = PlayerMain;
            selectUpgradeButtonUI_2.PlayerMain = PlayerMain;
            selectUpgradeButtonUI_3.PlayerMain = PlayerMain;

            selectUpgradeButtonUI_1.Weapon = PlayerMain.WeaponList[0];
            selectUpgradeButtonUI_1.Weapon = PlayerMain.WeaponList[1];
            selectUpgradeButtonUI_1.Weapon = PlayerMain.WeaponList[2];
        }

        // TODO : 확인용
        if(playerId == -1)
        {
            Debug.Log(" 캐릭터 선택값이 없습니다 ");
        }
    }

    /** 업그레이드 UI를 활성화/비활성화 한다 */
    public void ShowUpgradeUI(bool isShow)
    {
        Time.timeScale = isShow == true ? 0 : 1;
        upgradeObject.SetActive(isShow);
    }
    #endregion // 변수
}
