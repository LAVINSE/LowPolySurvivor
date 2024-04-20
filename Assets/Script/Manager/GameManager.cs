using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region 변수
    [SerializeField] private List<GameObject> playerPrefabsList = new List<GameObject>();
    [SerializeField] private ObjectPoolManager poolManager;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [SerializeField] private int playerId = -1;
    #endregion // 변수

    #region 프로퍼티
    public static GameManager Instance { get; private set; }
    public ObjectPoolManager PoolManager => poolManager;
    public PlayerMain PlayerMain { get; private set; }

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

            EquipIndex_1 = PlayerPrefs.GetInt("EquipType_1");
            EquipIndex_2 = PlayerPrefs.GetInt("EquipType_2");
            EquipIndex_3 = PlayerPrefs.GetInt("EquipType_3");

            PlayerMain.ActiveAddWeapon((eEquipType)EquipIndex_1);
            PlayerMain.ActiveAddWeapon((eEquipType)EquipIndex_2);
            PlayerMain.ActiveAddWeapon((eEquipType)EquipIndex_3);

            virtualCamera.Follow = playerObject.transform;
            virtualCamera.GetComponent<TransparentObjectCamera>().Player = playerObject;
        }

        // TODO : 확인용
        if(playerId == -1)
        {
            Debug.Log(" 캐릭터 선택값이 없습니다 ");
        }
    }
    #endregion // 변수
}
