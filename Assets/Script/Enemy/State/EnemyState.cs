using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    // ���� ����
    public enum eEnemyState
    {
        Tracking,
        Attack,
    }

    #region ����
    private Enemy enemy;
    #endregion // ����

    #region ������Ƽ
    public BaseState[] stateArray { get; private set; } // ���� �迭
    public BaseState currentState { get; private set; } // ���� ����
    #endregion // ������Ƽ

    #region �Լ�
    /** �ʱ�ȭ */
    private void Awake()
    {
        enemy = GetComponent<Enemy>();

        // ���� ũ�� ����
        stateArray = new BaseState[3];

        // ����
        stateArray[(int)eEnemyState.Tracking] = new EnemyTrackingState(this, enemy);
        stateArray[(int)eEnemyState.Attack] = new EnemyAttackState(this, enemy);
    }

    /** �ʱ�ȭ */  
    private void Start()
    {
        // ���� ����
        currentState = stateArray[(int)eEnemyState.Tracking];
        currentState.StateEnter();
    }

    /** �ʱ�ȭ => ���¸� �����Ѵ� */
    private void Update()
    {
        // ���°� ������ ���
        if (currentState != null)
        {
            currentState.StateUpdate();
        }
        else
        {
            Debug.Log(" ���°� �����ϴ� ");
        }
    }

    /** ���¸� �����Ѵ� */
    public void ChangeState(eEnemyState changeType)
    {
        if (stateArray[(int)changeType] == null) { return; }

        // ���� ���°� ������ ���
        if (currentState != null)
        {
            // ���� ����
            currentState.StateExit();
        }

        // ���� ����
        currentState = stateArray[(int)changeType];
        currentState.StateEnter();
    }
    #endregion // �Լ�
}
