using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    #region ����
    private static T oInst = null;
    #endregion // ����

    #region Ŭ���� ������Ƽ
    public static T Inst
    {
        get
        {
            // �ν��Ͻ��� ���� ���
            if (Singleton<T>.oInst == null)
            {
                var Gameobj = new GameObject(typeof(T).Name);
                Singleton<T>.oInst = Gameobj.AddComponent<T>();
            }

            return Singleton<T>.oInst;
        }
    }
    #endregion // Ŭ���� ������Ƽ

    #region �Լ�
    /** �ʱ�ȭ */
    public virtual void Awake()
    {
        if (Singleton<T>.oInst != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Debug.Assert(Singleton<T>.oInst == null);

        if (oInst != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Singleton<T>.oInst = this as T;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion // �Լ�

    #region Ŭ���� �Լ�
    /** �ν��Ͻ��� �����Ѵ� */
    public static T Create()
    {
        return Singleton<T>.Inst;
    }
    #endregion // Ŭ���� �Լ�
}
