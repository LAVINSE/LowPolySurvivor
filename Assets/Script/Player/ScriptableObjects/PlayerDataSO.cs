using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "Scriptable Objects/PlayerDataSO/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    public Sprite characterImg; // ĳ���� �̹���
    public int maxHp; // �ִ� ü��
    public float moveSpeed; // �̵� �ӵ�
    public float itemPickRange; // ������ ���� ����
    public float expPercent; // ����ġ ����
    public int maxLevel; // �ִ� ����
    public int maxLuck; // �ִ� ���
    [Tooltip(" �ִ� ������ŭ �����ؼ� ��� ")]public float[] expArray; // ����ġ ��
}
