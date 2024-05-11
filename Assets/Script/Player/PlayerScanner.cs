using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScanner : MonoBehaviour
{
    #region ����
    [Header("=====> ��ġ <=====")]
    [SerializeField] private float scanRange = 0f; // ����
    [SerializeField] private float sacnRangeForward = 0f; // ����
    [SerializeField] private float detectAngle = 45f; // ���� ����

    [Header("=====> ���̾� ���� <=====")]
    [SerializeField] private LayerMask targetLayer; // ���̾�

    [Header("=====> �ν����� Ȯ�� <=====")]
    [SerializeField] private Transform nearTarget; // ���� ����� Ÿ��
    [SerializeField] private Transform[] nearTargetArray; // ����� Ÿ�� �迭
    [SerializeField] private Transform[] forwardNearTargetArray; // ���� Ÿ�� �迭
    [SerializeField] private Transform forwardNearTarget; // ���� ����� ���� Ÿ��
    #endregion // ����

    #region ������Ƽ
    public bool IsTarget { get; set; } = false;
    public bool IsTargetForward { get; set; } = false;
    public Collider[] ColliderArray { get; set; }

    // ��ü
    public Transform NearTarget => nearTarget;
    public Transform[] NearTargetArray => nearTargetArray;

    // ����
    public Transform ForwardNearTarget => forwardNearTarget;
    public Transform[] ForwardNearTargetArray => forwardNearTargetArray;
    #endregion // ������Ƽ

    #region �Լ�
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, scanRange);

        Gizmos.color = Color.green;
        Vector3 direction = transform.forward;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-detectAngle / 2, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(detectAngle / 2, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * direction;
        Vector3 rightRayDirection = rightRayRotation * direction;

        Gizmos.DrawLine(transform.position, transform.position + leftRayDirection * sacnRangeForward);
        Gizmos.DrawLine(transform.position, transform.position+ rightRayDirection * sacnRangeForward);
        Gizmos.DrawLine(transform.position+ leftRayDirection * sacnRangeForward,
            transform.position+ rightRayDirection * sacnRangeForward);
    }

    /** �ʱ�ȭ => ���¸� �����Ѵ� */
    private void Update()
    {
        ColliderArray = Physics.OverlapSphere(transform.position, scanRange, targetLayer);

        // ���� ����� �� ���� (��ü)
        SetNearTarget();
        // ���� ����� �� ���� (����)
        SetNearTargetForward();

        IsTarget = ColliderArray.Length > 0;
        IsTargetForward = forwardNearTargetArray.Length > 0;
    }

    /** ���� ����� �� ���� (��ü) */
    private void SetNearTarget()
    {
        nearTargetArray = SortEnemyList();

        if (nearTargetArray.Length > 0)
        {
            nearTarget = nearTargetArray[0];
        }
        else
        {
            nearTarget = null;
        }
    }

    /** ���� ����� �� ���� (����) */
    private void SetNearTargetForward()
    {
        forwardNearTargetArray = ForwardNearTargetList();

        if (forwardNearTargetArray.Length > 0)
        {
            forwardNearTarget = NearTargetArray[0];
        }
        else
        {
            forwardNearTarget = null;
        }
    }

    /** �÷��̾�� ��ġ�� ���Ͽ� ���尡��� ������� �����Ѵ� (����) */
    private Transform[] ForwardNearTargetList()
    {
        List<Transform> sortList = new List<Transform>();

        foreach (Collider collider in ColliderArray)
        {
            Transform enemyTransform = collider.transform;

            // ���� ��ä�� ���� �ȿ� ���� �ִ��� Ȯ��
            if (IsEnemyInDetectionCone(enemyTransform))
            {
                sortList.Add(enemyTransform);
            }
        }

        // �Ÿ��� �������� ����, ��������
        sortList.Sort((aPos, bPos) =>
        {
            float distanceA = Vector3.Distance(this.transform.position, aPos.position);
            float distanceB = Vector3.Distance(this.transform.position, bPos.position);

            return distanceA.CompareTo(distanceB);
        });

        return sortList.ToArray(); 
    }

    /** �÷��̾�� ��ġ�� ���Ͽ� ���尡��� ������� �����Ѵ� (��ü) */
    private Transform[] SortEnemyList()
    {
        List<Transform> sortList = new List<Transform>();

        // ������ �ִ� ������ ���� ����Ʈ�� �߰��Ѵ�
        foreach (Collider collider in ColliderArray)
        {
            sortList.Add(collider.GetComponent<Enemy>().transform);
        }

        // �Ÿ��� �������� ����, ��������
        sortList.Sort((aPos, bPos) =>
        {
            float distanceA = Vector3.Distance(this.transform.position, aPos.position);
            float distanceB = Vector3.Distance(this.transform.position, bPos.position);

            return distanceA.CompareTo(distanceB);
        });

        // ���ĵ� ������ �迭�� ��ȯ�Ͽ� ��ȯ�մϴ�.
        return sortList.ToArray();
    }

    /** ���� ��ä�� ���� �ȿ� ���� �ִ��� Ȯ���Ѵ� */
    private bool IsEnemyInDetectionCone(Transform enemyTransform)
    {
        Vector3 direction = (enemyTransform.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, direction);

        // ���� ������ ������ ���� �ȿ� ���� ���
        if (angle < detectAngle / 2)
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, sacnRangeForward, targetLayer);
            foreach(Collider collider in colliders)
            {
                if(collider.transform == enemyTransform)
                {
                    return true;
                }
            }
        }

        // ���� ���� ���� ���� ���ų� �������� ����
        return false; 
    }
    #endregion // �Լ�
}
