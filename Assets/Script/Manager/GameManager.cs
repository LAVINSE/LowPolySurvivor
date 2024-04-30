using Cinemachine;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region ����
    [Header("=====> ��ȯ �Ŵ��� <=====")]
    [SerializeField] private SpawnManager spawnManager;

    [Header("=====> �÷��̾� ����Ʈ <=====")]
    [SerializeField] private List<GameObject> playerPrefabsList = new List<GameObject>();

    [Header("=====> ������Ʈ Ǯ <=====")]
    [SerializeField] private ObjectPoolManager poolManager;

    [Header("=====> ī�޶� <=====")]
    [Tooltip(" �÷��̾� ���� ī�޶� ")][SerializeField] private CinemachineVirtualCamera virtualCamera;
    [Tooltip(" �̴ϸ� ī�޶� ")][SerializeField] private MiniMapCamera miniMapCamera;

    [Header("=====> UI <=====")]
    [SerializeField] private InGameUI inGameUI;
    [SerializeField] private MiniMapUI miniMapUI;
    [SerializeField] private GameObject upgradeObject;
    [SerializeField] private SelectUpgradeButtonUI selectUpgradeButtonUI_1;
    [SerializeField] private SelectUpgradeButtonUI selectUpgradeButtonUI_2;
    [SerializeField] private SelectUpgradeButtonUI selectUpgradeButtonUI_3;

    [SerializeField] private int playerId = -1;
    #endregion // ����

    #region ������Ƽ
    public static GameManager Instance { get; private set; }
    public ObjectPoolManager PoolManager => poolManager;
    public PlayerMain PlayerMain { get; private set; }
    public InGameUI InGameUI => inGameUI;

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

            // ��� ���� ����
            EquipIndex_1 = PlayerPrefs.GetInt("EquipType_1");
            EquipIndex_2 = PlayerPrefs.GetInt("EquipType_2");
            EquipIndex_3 = PlayerPrefs.GetInt("EquipType_3");

            // ��� �߰�
            PlayerMain.ActiveAddWeapon((eEquipType)EquipIndex_1);
            PlayerMain.ActiveAddWeapon((eEquipType)EquipIndex_2);
            PlayerMain.ActiveAddWeapon((eEquipType)EquipIndex_3);

            // ī�޶� ����
            virtualCamera.Follow = playerObject.transform;
            virtualCamera.GetComponent<TransparentObjectCamera>().Player = playerObject;

            // UI ����
            inGameUI.InitEquipSlot(PlayerMain.WeaponList);
            inGameUI.UpdateHpBar(PlayerMain.MaxHp, PlayerMain.CurrentHp) ;
            inGameUI.UpdateExpBar(PlayerMain.ExpArray[PlayerMain.CurrentLevel], PlayerMain.CurrentExp);
            inGameUI.UpdateLevelText(PlayerMain.CurrentLevel);

            // TODO : �������� �̸�
            miniMapCamera.Target = PlayerMain.transform;
            miniMapUI.MiniMapCamera = miniMapCamera.GetComponent<Camera>();
            miniMapUI.Init("�� �̸�");

            // ���׷��̵� UI ����
            selectUpgradeButtonUI_1.PlayerMain = PlayerMain;
            selectUpgradeButtonUI_2.PlayerMain = PlayerMain;
            selectUpgradeButtonUI_3.PlayerMain = PlayerMain;

            selectUpgradeButtonUI_1.Weapon = PlayerMain.WeaponList[0];
            selectUpgradeButtonUI_1.Weapon = PlayerMain.WeaponList[1];
            selectUpgradeButtonUI_1.Weapon = PlayerMain.WeaponList[2];
        }

        // TODO : Ȯ�ο�
        if(playerId == -1)
        {
            Debug.Log(" ĳ���� ���ð��� �����ϴ� ");
        }
    }

    /** ���׷��̵� UI�� Ȱ��ȭ/��Ȱ��ȭ �Ѵ� */
    public void ShowUpgradeUI(bool isShow)
    {
        Time.timeScale = isShow == true ? 0 : 1;
        upgradeObject.SetActive(isShow);
    }
    #endregion // ����
}
