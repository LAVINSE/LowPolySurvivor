using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotGunDataSO", menuName = "Scriptable Objects/EquipWeaponDataSO/ShotGunData")]
public class ShotGunDataSO : ScriptableObject
{
    [Header("=====> 샷건 무기 데이터 <=====")]
    [Tooltip(" 샷건 총알 각도 ")] public float shotAngle = 30f;
    [Tooltip(" 샷건 넉백 힘 ")] public float knockBackPower = 2f;
}
