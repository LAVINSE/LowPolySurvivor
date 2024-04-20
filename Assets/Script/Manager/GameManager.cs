using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region ����
    [SerializeField] private List<GameObject> playerPrefabsList = new List<GameObject>();
    [SerializeField] private ObjectPoolManager poolManager;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [SerializeField] private int playerId = -1;
    #endregion // ����

    #region ������Ƽ
    public static GameManager Instance { get; private set; }
    public ObjectPoolManager PoolManager => poolManager;
    public PlayerMain PlayerMain { get; private set; }

    public int EquipIndex_1 { get; private set; }
    public int EquipIndex_2 { get; private set; }
    public int EquipIndex_3 { get; private set; }
    #endregion // ������Ƽ

    #region ����
    /** �ʱ�ȭ */
    private void Awake()
    {
        Instance = this; 
    }

    private void Start()
    {
        // ĳ���� ���� ���� ���� ���
        if (PlayerPrefs.HasKey("CharacterIndex"))
        {
            // ���õ� ���� �����´�
            playerId = PlayerPrefs.GetInt("CharacterIndex");

            // ĳ���� ����
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

        // TODO : Ȯ�ο�
        if(playerId == -1)
        {
            Debug.Log(" ĳ���� ���ð��� �����ϴ� ");
        }
    }
    #endregion // ����
}
