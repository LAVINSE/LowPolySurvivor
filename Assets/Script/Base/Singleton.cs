using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    #region 변수
    private static T oInst = null;
    #endregion // 변수

    #region 클래스 프로퍼티
    public static T Inst
    {
        get
        {
            // 인스턴스가 없을 경우
            if (Singleton<T>.oInst == null)
            {
                var Gameobj = new GameObject(typeof(T).Name);
                Singleton<T>.oInst = Gameobj.AddComponent<T>();
            }

            return Singleton<T>.oInst;
        }
    }
    #endregion // 클래스 프로퍼티

    #region 함수
    /** 초기화 */
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
    #endregion // 함수

    #region 클래스 함수
    /** 인스턴스를 생성한다 */
    public static T Create()
    {
        return Singleton<T>.Inst;
    }
    #endregion // 클래스 함수
}
