using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScanner : MonoBehaviour
{
    #region ����
    [SerializeField] private float scanRange = 0f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private RaycastHit[] targets;
    [SerializeField] private Transform nearTarget;
    #endregion // ����

    #region �Լ�
    /** �ʱ�ȭ => ���¸� �����Ѵ� */
    private void FixedUpdate()
    {
        targets = Physics.SphereCastAll(this.transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearTarget = GetNear();
    }

    /** �÷��̾�� ��ġ�� ���Ͽ� ���尡��� ���� ã�´� */
    private Transform GetNear()
    {
        Transform result = null;
        float diff = 100;

        foreach (RaycastHit target in targets)
        {
            Vector3 myPos = this.transform.position;
            Vector3 targetPos = target.transform.position;
            float currentDiff = Vector3.Distance(myPos, targetPos);

            if (currentDiff < diff)
            {
                diff = currentDiff;
                result = target.transform;
            }
        }

        return result;
    }
    #endregion // �Լ�
}
