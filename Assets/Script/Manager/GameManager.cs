using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region 프로퍼티
    public static GameManager Instance { get; private set; }
    public ObjectPoolManager PoolManager { get; private set; }
    #endregion // 프로퍼티

    #region 변수
    /** 초기화 */
    private void Awake()
    {
        PoolManager = Factory.CreateObject<ObjectPoolManager>("ObjectPoolManager", this.gameObject,
            Vector3.zero, Vector3.one, Vector3.zero);
    }
    #endregion // 변수
}
