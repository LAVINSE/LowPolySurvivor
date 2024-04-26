using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    #region ����
    [SerializeField] private bool x;
    [SerializeField] private bool y;
    [SerializeField] private bool z;
    #endregion // ����

    #region ������Ƽ
    public Transform Target { get; set; }
    #endregion // ������Ƽ

    #region �Լ�
    private void Update()
    {
        if(Target == null) { return; }

        this.transform.position = new Vector3(
            (x ? Target.position.x : this.transform.position.x),
            (y ? Target.position.y : this.transform.position.y),
            (z ? Target.position.z : this.transform.position.z));
    }
    #endregion // �Լ�
}
