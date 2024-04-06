using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrackingState : BaseState
{
    #region 변수
    private EnemyState enemyState;
    private Enemy enemy;
    #endregion // 변수

    #region 생성자
    public EnemyTrackingState(EnemyState enemyState, Enemy enemy)
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
        
    }

    /** 초기화 => 상태를 갱신한다 */
    public override void StateUpdate()
    {
        enemy.NavMeshSetDestination();

        Debug.Log("추적");
        if (enemy.CheckattackChangeRange())
        {
            enemyState.ChangeState(EnemyState.eEnemyState.Attack);
        }
    }
}
