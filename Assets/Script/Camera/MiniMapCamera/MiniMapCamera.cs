using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    #region 변수
    [SerializeField] private bool x;
    [SerializeField] private bool y;
    [SerializeField] private bool z;
    #endregion // 변수

    #region 프로퍼티
    public Transform Target { get; set; }
    #endregion // 프로퍼티

    #region 함수
    private void Update()
    {
        if(Target == null) { return; }

        this.transform.position = new Vector3(
            (x ? Target.position.x : this.transform.position.x),
            (y ? Target.position.y : this.transform.position.y),
            (z ? Target.position.z : this.transform.position.z));
    }
    #endregion // 함수
}
