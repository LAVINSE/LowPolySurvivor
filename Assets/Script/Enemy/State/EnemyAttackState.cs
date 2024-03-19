using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : BaseState
{
    #region 변수
    private EnemyState enemyState;
    private Enemy enemy;
    #endregion // 변수

    #region 생성자
    public EnemyAttackState(EnemyState enemyState, Enemy enemy)
    {
        this.enemyState = enemyState;
        this.enemy = enemy;
    }
    #endregion // 생성자

    /** 상태 시작 */
    public override void StateEnter()
    {
        
    }

    /** 상태 종료 */
    public override void StateExit()
    {
        throw new System.NotImplementedException();
    }

    /** 초기화 => 상태를 갱신한다 */
    public override void StateUpdate()
    {
        enemy.TargetSetting();
    }
}
