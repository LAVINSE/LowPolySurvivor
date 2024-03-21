using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "StageSO", menuName = "Scriptable Objects/StageSO/Stage")]
public class StageSO : ScriptableObject
{
    [System.Serializable]
    public struct StageStruct 
    {
        public int stageLevel; // �������� �ȿ��� ����
        public GameObject[] enemyPrefabs; // �ܰ躰�� ��ȯ�� ���� ����
    }

    public StageStruct[] asdf;
}
