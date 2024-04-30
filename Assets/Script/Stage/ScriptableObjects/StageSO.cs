using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "StageSO", menuName = "Scriptable Objects/StageSO/Stage")]
public class StageSO : ScriptableObject
{
    public Vector3 minPos;
    public Vector3 maxPos;
}
