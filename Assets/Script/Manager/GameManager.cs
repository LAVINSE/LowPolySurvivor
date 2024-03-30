using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region ����
    [SerializeField] private List<GameObject> playerPrefabsList = new List<GameObject>();
    [SerializeField] private ObjectPoolManager poolManager;
    #endregion // ����

    #region ������Ƽ
    public static GameManager Instance { get; private set; }
    public ObjectPoolManager PoolManager => poolManager;
    public int playerId;
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
            Instantiate(playerPrefabsList[playerId]);
        }
    }
    #endregion // ����
}
