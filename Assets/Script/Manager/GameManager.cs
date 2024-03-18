using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region ������Ƽ
    public static GameManager Instance { get; private set; }
    public ObjectPoolManager PoolManager { get; private set; }
    #endregion // ������Ƽ

    #region ����
    /** �ʱ�ȭ */
    private void Awake()
    {
        PoolManager = Factory.CreateObject<ObjectPoolManager>("ObjectPoolManager", this.gameObject,
            Vector3.zero, Vector3.one, Vector3.zero);
    }
    #endregion // ����
}
