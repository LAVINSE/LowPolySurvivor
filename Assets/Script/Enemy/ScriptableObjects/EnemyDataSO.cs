using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "Scriptable Objects/EnemyDataSO/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    [System.Serializable]
    public struct EnemyDataStruct
    {
        public int maxHp;
        public float moveSpeed;
        public float attackDelay;
    }

    public EnemyDataStruct[] enemyDataStruct;
}
