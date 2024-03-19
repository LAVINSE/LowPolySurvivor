using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public enum EEnemyState
    {
        Tracking,
        Attack,
        Die,
    }

    #region ����
    private Enemy enemy;
    #endregion // ����

    #region ������Ƽ
    public BaseState[] stateArray { get; private set; }
    public BaseState currentState { get; private set; }
    #endregion // ������Ƽ

    #region �Լ�
    /** �ʱ�ȭ */
    private void Awake()
    {
        enemy = GetComponent<Enemy>();

        // ���� ũ�� ����
        stateArray = new BaseState[3];

        stateArray[(int)EEnemyState.Tracking] = new EnemyTrackingState(this, enemy);
        stateArray[(int)EEnemyState.Attack] = new EnemyAttackState(this, enemy);
        stateArray[(int)EEnemyState.Die] = new EnemyDieState(this, enemy);
    }

    /** �ʱ�ȭ */  
    private void Start()
    {
        currentState = stateArray[(int)EEnemyState.Attack];
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

    public void ChangeState(EEnemyState changeType)
    {
        if (stateArray[(int)changeType] == null) { return; }

        if (currentState != null)
        {
            currentState.StateExit();
        }

        currentState = stateArray[(int)changeType];
        currentState.StateEnter();
    }
    #endregion // �Լ�
}
