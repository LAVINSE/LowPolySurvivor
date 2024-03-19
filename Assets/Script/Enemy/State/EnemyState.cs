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

    #region 변수
    private Enemy enemy;
    #endregion // 변수

    #region 프로퍼티
    public BaseState[] stateArray { get; private set; }
    public BaseState currentState { get; private set; }
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        enemy = GetComponent<Enemy>();

        // 상태 크기 설정
        stateArray = new BaseState[3];

        stateArray[(int)EEnemyState.Tracking] = new EnemyTrackingState(this, enemy);
        stateArray[(int)EEnemyState.Attack] = new EnemyAttackState(this, enemy);
        stateArray[(int)EEnemyState.Die] = new EnemyDieState(this, enemy);
    }

    /** 초기화 */  
    private void Start()
    {
        currentState = stateArray[(int)EEnemyState.Attack];
        currentState.StateEnter();
    }

    /** 초기화 => 상태를 갱신한다 */
    private void Update()
    {
        // 상태가 존재할 경우
        if (currentState != null)
        {
            currentState.StateUpdate();
        }
        else
        {
            Debug.Log(" 상태가 없습니다 ");
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
    #endregion // 함수
}
