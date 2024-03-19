using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : BaseState
{
    #region ����
    private EnemyState enemyState;
    private Enemy enemy;
    #endregion // ����

    #region ������
    public EnemyAttackState(EnemyState enemyState, Enemy enemy)
    {
        this.enemyState = enemyState;
        this.enemy = enemy;
    }
    #endregion // ������

    public override void StateEnter()
    {
        
    }

    public override void StateExit()
    {
        throw new System.NotImplementedException();
    }

    public override void StateUpdate()
    {
        enemy.TargetSetting();
    }
}
