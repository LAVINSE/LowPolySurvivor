using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScanner : MonoBehaviour
{
    #region ����
    [SerializeField] private float scanRange = 0f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Transform nearTarget;
    #endregion // ����

    #region ������Ƽ
    public bool IsTarget { get; set; } = false;
    public Collider[] ColliderArray { get; set; }
    #endregion // ������Ƽ

    #region �Լ�
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, scanRange);
    }

    /** �ʱ�ȭ => ���¸� �����Ѵ� */
    private void Update()
    {
        ColliderArray = Physics.OverlapSphere(transform.position, scanRange, targetLayer);

        IsTarget = ColliderArray.Length > 0;

        // ���� ����� ���� ã���ϴ�.
        nearTarget = GetNear();
    }

    /** �÷��̾�� ��ġ�� ���Ͽ� ���尡��� ���� ã�´� */
    private Transform GetNear()
    {
        if(ColliderArray.Length == 0) 
        {
            return null;
        }

        if (ColliderArray.Length > 0)
        {
            float distance1 = Vector3.Distance(this.transform.position, ColliderArray[0].transform.position);

            foreach(Collider collider in ColliderArray)
            {
                float distance2 = Vector3.Distance(this.transform.position, collider.transform.position);

                if(distance1 > distance2)
                {
                    distance1 = distance2;
                    return collider.transform;
                }
            }
        }

        return ColliderArray[0].transform;
    }
    #endregion // �Լ�
}
