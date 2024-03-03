using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    #region 변수
    [SerializeField] private float moveSpeed; // 이동 속도
    [SerializeField] private float rotationSpeed; // 회전 속도

    private Vector3 moveDirection = Vector3.zero;

    private Rigidbody rigidBody;
    #endregion // 변수

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    /** 초기화 => 상태를 갱신한다 */
    private void Update()
    {
        // 플레이어 입력처리
        PlayerInput();
    }

    /** 초기화 => 상태를 갱신한다 */
    private void FixedUpdate()
    {
        // 플레이어 이동
        PlayerMove();
    }

    /** 플레이어 입력처리 */
    private void PlayerInput()
    {
        // 움직임
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.z = Input.GetAxisRaw("Vertical");
        moveDirection.Normalize();
    }

    /** 플레이어 이동 */
    private void PlayerMove()
    {
        // 이동방향이 0이 아닐 경우
        if(moveDirection != Vector3.zero)
        {
            // 지금 바라보고 있는 방향의 부호와 나아갈 방향의 부호가 다를 경우
            if(Mathf.Sign(this.transform.forward.x) != Mathf.Sign(moveDirection.x) ||
                Mathf.Sign(this.transform.forward.z) != Mathf.Sign(moveDirection.z))
            {
                // 조금 회전 시킨다
                this.transform.Rotate(0, 1, 0);
            }

            // 오브젝트가 목표 방향을 바라보게 설정
            this.transform.forward = Vector3.Lerp(this.transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
        }

        // 방향에 따라 이동속도 만큼 이동한다
        rigidBody.MovePosition(this.transform.position + moveDirection * moveSpeed * Time.deltaTime);
    }
    #endregion // 함수
}
