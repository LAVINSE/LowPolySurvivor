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
        
    }

    /** ���� ���� */
    public override void StateExit()
    {
        
    }

    /** �ʱ�ȭ => ���¸� �����Ѵ� */
    public override void StateUpdate()
    {
        enemy.TargetSetting();

        Debug.Log("����");
        if (!enemy.CheckattackChangeRange())
        {
            enemyState.ChangeState(EnemyState.eEnemyState.Tracking);
        }
    }
}
