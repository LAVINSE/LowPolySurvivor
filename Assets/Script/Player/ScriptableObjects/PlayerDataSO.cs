using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "Scriptable Objects/PlayerDataSO/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    public float maxHp; // �ִ� ü��
    public float moveSpeed; // �̵� �ӵ�
    public float itemPickRange; // ������ ���� ����
    public float expPercent; // ����ġ ����
    public int maxLevel; // �ִ� ����
    public int maxLuck; // �ִ� ���
}
