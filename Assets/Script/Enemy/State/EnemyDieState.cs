using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieState : BaseState
{
    #region ����
    private EnemyState enemyState;
    private Enemy enemy;
    #endregion // ����

    #region ������
    public EnemyDieState(EnemyState enemyState, Enemy enemy)
    {
        this.enemyState = enemyState;
        this.enemy = enemy;
    }
    #endregion // ������

    public override void StateEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void StateExit()
    {
        throw new System.NotImplementedException();
    }

    public override void StateUpdate()
    {
        throw new System.NotImplementedException();
    }
}
