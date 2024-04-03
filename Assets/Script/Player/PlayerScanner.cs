using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScanner : MonoBehaviour
{
    #region ����
    [SerializeField] private float scanRange = 0f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Transform nearTarget;
    [SerializeField] private Transform[] nearTargetArray;
    #endregion // ����

    #region ������Ƽ
    public bool IsTarget { get; set; } = false;
    public Collider[] ColliderArray { get; set; }
    public Transform NearTarget => nearTarget;
    public Transform[] NearTargetArray => nearTargetArray;
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

        nearTargetArray = SortEnemyList();

        if(nearTargetArray.Length > 0)
        {
            nearTarget = nearTargetArray[0];
        }
    }

    /** �÷��̾�� ��ġ�� ���Ͽ� ���尡��� ���� ã�´� */
    private Transform GetNear()
    {
        if (ColliderArray.Length == 0)
        {
            return null;
        }

        Transform nearEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Collider collider in ColliderArray)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearEnemy = collider.GetComponent<Enemy>().MeshTransform;
            }
        }

        return nearEnemy;
    }

    /** �÷��̾�� ��ġ�� ���Ͽ� ���尡��� ������� �����Ѵ� */
    private Transform[] SortEnemyList()
    {
        List<Transform> sortList = new List<Transform>();

        // ������ �ִ� ������ ���� ����Ʈ�� �߰��Ѵ�
        foreach (Collider collider in ColliderArray)
        {
            sortList.Add(collider.GetComponent<Enemy>().MeshTransform);
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
    #endregion // �Լ�
}
