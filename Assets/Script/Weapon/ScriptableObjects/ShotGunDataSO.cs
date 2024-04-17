using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotGunDataSO", menuName = "Scriptable Objects/EquipWeaponDataSO/ShotGunData")]
public class ShotGunDataSO : ScriptableObject
{
    [Header("=====> ���� ���� ������ <=====")]
    [Tooltip(" ���� �Ѿ� ���� ")] public float shotAngle = 30f;
    [Tooltip(" ���� �˹� �� ")] public float knockBackPower = 2f;
}
