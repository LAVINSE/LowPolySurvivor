using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "Scriptable Objects/PlayerDataSO/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    public float maxHp; // 최대 체력
    public float moveSpeed; // 이동 속도
    public float itemPickRange; // 아이템 수집 범위
    public float expPercent; // 경험치 배율
    public int maxLevel; // 최대 레벨
    public int maxLuck; // 최대 행운
}
