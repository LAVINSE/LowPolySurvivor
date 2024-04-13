using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        enemy.Animator.SetBool("isWalk", true);
    }

    /** ���� ���� */
    public override void StateExit()
    {
        
    }

    /** �ʱ�ȭ => ���¸� �����Ѵ� */
    public override void StateUpdate()
    {
        enemy.NavMeshSetDestination();

        if (enemy.CheckAttackRange() == true)
        {
            enemyState.ChangeState(EnemyState.eEnemyState.Attack);
        }
    }
}
