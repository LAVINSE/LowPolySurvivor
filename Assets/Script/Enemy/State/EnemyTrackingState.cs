using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrackingState : BaseState
{
    #region ����
    private EnemyState enemyState;
    private Enemy enemy;
    #endregion // ����

    #region ������
    public EnemyTrackingState(EnemyState enemyState, Enemy enemy)
    {
        this.enemyState = enemyState;
        this.enemy = enemy;
    }
    #endregion // ������

    /** ���� ���� */
    public override void StateEnter()
    {
        throw new System.NotImplementedException();
    }

    /** ���� ���� */
    public override void StateExit()
    {
        throw new System.NotImplementedException();
    }

    /** �ʱ�ȭ => ���¸� �����Ѵ� */
    public override void StateUpdate()
    {
        throw new System.NotImplementedException();
    }
}
