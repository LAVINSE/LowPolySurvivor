using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventDataSO", menuName = "Scriptable Objects/EventDataSO/EventData")]
public class EventDataSO : ScriptableObject
{
    public eStageEventType stageEventType;
    public GameObject prefab;
    public string eventName;
    public string eventDesc;
    public Sprite eventImg;
}
