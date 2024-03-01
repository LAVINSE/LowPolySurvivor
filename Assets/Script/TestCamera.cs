using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    public GameObject Player;

    void LateUpdate()
    {
        // Player�� �̱����̱⿡ ���������� ������ �� �ֽ��ϴ�.
        Vector3 direction = (Player.transform.position - transform.position).normalized;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, Mathf.Infinity,
                            1 << LayerMask.NameToLayer("EnvironmentObject"));

        for (int i = 0; i < hits.Length; i++)
        {
            Test[] obj = hits[i].transform.GetComponentsInChildren<Test>();

            for (int j = 0; j < obj.Length; j++)
            {
                obj[j]?.BecomeTransparent();
            }
        }
    }
}
