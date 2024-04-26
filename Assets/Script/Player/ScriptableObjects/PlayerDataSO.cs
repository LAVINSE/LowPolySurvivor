using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "Scriptable Objects/PlayerDataSO/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    public Sprite characterImg; // 캐릭터 이미지
    public int maxHp; // 최대 체력
    public float moveSpeed; // 이동 속도
    public float itemPickRange; // 아이템 수집 범위
    public float expPercent; // 경험치 배율
    public int maxLevel; // 최대 레벨
    public int maxLuck; // 최대 행운
    [Tooltip(" 최대 레벨만큼 생성해서 사용 ")]public float[] expArray; // 경험치 통
}
