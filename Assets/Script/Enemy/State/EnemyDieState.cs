using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieState : BaseState
{
    #region 변수
    private EnemyState enemyState;
    private Enemy enemy;
    #endregion // 변수

    #region 생성자
    public EnemyDieState(EnemyState enemyState, Enemy enemy)
    {
        this.enemyState = enemyState;
        this.enemy = enemy;
    }
    #endregion // 생성자

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
