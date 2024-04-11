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

    /** ���� ���� */
    public override void StateEnter()
    {
        enemy.Animator.SetBool("isIdle", true);
    }

    /** ���� ���� */
    public override void StateExit()
    {
        enemy.Animator.SetBool("isIdle", false);
    }

    /** �ʱ�ȭ => ���¸� �����Ѵ� */
    public override void StateUpdate()
    {
        enemy.TargetSetting();

        if (enemy.CheckAttackRange() == false && enemy.IsAttack == false)
        {
            enemyState.ChangeState(EnemyState.eEnemyState.Tracking);
        }
    }
}
