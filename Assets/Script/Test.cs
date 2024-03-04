using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // 시작 지점과 끝 지점
    public Transform startPoint;
    public Transform endPoint;

    // 초기 블루존의 크기와 축소 속도
    public float initialSize = 10f;
    public float shrinkSpeed = 1f;

    // 축소된 크기
    private float currentSize;

    // 라인 랜더러
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        // 초기 블루존 설정
        SetZoneSize(initialSize);
    }

    void Update()
    {
        // 블루존 크기 축소
        currentSize -= shrinkSpeed * Time.deltaTime;
        SetZoneSize(currentSize);
    }

    // 블루존 크기 설정
    void SetZoneSize(float size)
    {
        lineRenderer.SetPosition(0, startPoint.position);
        lineRenderer.SetPosition(1, endPoint.position);
        lineRenderer.startWidth = size;
        lineRenderer.endWidth = size;
    }
}
