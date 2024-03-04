using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObjectCamera : MonoBehaviour
{
    #region ����
    public GameObject Player;
    #endregion // ����

    /** �ʱ�ȭ => ���¸� �����Ѵ� */
    private void LateUpdate()
    {
        RaycastObject();
    }

    private void RaycastObject()
    {
        // ����ĳ��Ʈ ����
        Vector3 direction = (Player.transform.position - this.transform.position).normalized;

        // ���Ѵ�� �߻�Ǵ� ����ĳ��Ʈ EnvironmentObject�� ����
        RaycastHit[] hits = Physics.RaycastAll(this.transform.position, direction, Mathf.Infinity,
            LayerMask.GetMask("EnvironmentObject"));

        // �浹�� ��ü���� �ݺ�
        for (int i = 0; i < hits.Length; i++)
        {
            // �浿�� ��ü �ȿ� �ִ� ������Ʈ ��������
            TransparentObject[] obj = hits[i].transform.GetComponentsInChildren<TransparentObject>();

            for (int j = 0; j < obj.Length; j++)
            {
                // ������Ʈ�� �����ϰ� �����
                obj[j]?.BecomeTransparent();
            }
        }
    }
}
