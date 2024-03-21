using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "StageSO", menuName = "Scriptable Objects/StageSO/Stage")]
public class StageSO : ScriptableObject
{
    [System.Serializable]
    public struct StageStruct 
    {
        public int stageLevel; // 스테이지 안에서 레벨
        public GameObject[] enemyPrefabs; // 단계별로 소환될 몬스터 종류
    }

    public StageStruct[] asdf;
}
