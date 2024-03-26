using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region 변수
    [SerializeField] private List<GameObject> playerPrefabsList = new List<GameObject>();
    #endregion // 변수

    #region 프로퍼티
    public static GameManager Instance { get; private set; }
    public ObjectPoolManager PoolManager { get; private set; }
    public int playerId;
    #endregion // 프로퍼티

    #region 변수
    /** 초기화 */
    private void Awake()
    {
        Instance = this;

        PoolManager = Factory.CreateObject<ObjectPoolManager>("ObjectPoolManager", this.gameObject,
            Vector3.zero, Vector3.one, Vector3.zero);
    }

    private void Start()
    {
        // 캐릭터 선택 값이 있을 경우
        if (PlayerPrefs.HasKey("CharacterIndex"))
        {
            // 선택된 값을 가져온다
            playerId = PlayerPrefs.GetInt("CharacterIndex");

            // 캐릭터 생성
            Instantiate(playerPrefabsList[playerId]);
        }
    }
    #endregion // 변수
}
