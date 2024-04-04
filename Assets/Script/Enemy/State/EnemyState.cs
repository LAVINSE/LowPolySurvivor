using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    // 상태 종류
    public enum eEnemyState
    {
        Tracking,
        Attack,
    }

    #region 변수
    private Enemy enemy;
    #endregion // 변수

    #region 프로퍼티
    public BaseState[] stateArray { get; private set; } // 상태 배열
    public BaseState currentState { get; private set; } // 현재 상태
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        enemy = GetComponent<Enemy>();

        // 상태 크기 설정
        stateArray = new BaseState[3];

        // 상태
        stateArray[(int)eEnemyState.Tracking] = new EnemyTrackingState(this, enemy);
        stateArray[(int)eEnemyState.Attack] = new EnemyAttackState(this, enemy);
    }

    /** 초기화 */  
    private void Start()
    {
        // 시작 상태
        currentState = stateArray[(int)eEnemyState.Tracking];
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

    /** 상태를 변경한다 */
    public void ChangeState(eEnemyState changeType)
    {
        if (stateArray[(int)changeType] == null) { return; }

        // 현재 상태가 존재할 경우
        if (currentState != null)
        {
            // 상태 종료
            currentState.StateExit();
        }

        // 상태 변경
        currentState = stateArray[(int)changeType];
        currentState.StateEnter();
    }
    #endregion // 함수
}
