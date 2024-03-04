using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // ���� ������ �� ����
    public Transform startPoint;
    public Transform endPoint;

    // �ʱ� ������� ũ��� ��� �ӵ�
    public float initialSize = 10f;
    public float shrinkSpeed = 1f;

    // ��ҵ� ũ��
    private float currentSize;

    // ���� ������
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        // �ʱ� ����� ����
        SetZoneSize(initialSize);
    }

    void Update()
    {
        // ����� ũ�� ���
        currentSize -= shrinkSpeed * Time.deltaTime;
        SetZoneSize(currentSize);
    }

    // ����� ũ�� ����
    void SetZoneSize(float size)
    {
        lineRenderer.SetPosition(0, startPoint.position);
        lineRenderer.SetPosition(1, endPoint.position);
        lineRenderer.startWidth = size;
        lineRenderer.endWidth = size;
    }
}
