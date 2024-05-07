using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    #region ����
    [Header("=====> �ӵ� ���� <=====")]
    [SerializeField] private float rotationSpeed = 360f; // ȸ�� �ӵ�

    private Vector3 moveDirection = Vector3.zero;

    private Rigidbody rigidBody;
    private Animator animator;
    #endregion // ����

    #region ������Ƽ
    public VariableJoystick Joystick { get; set; }
    public float moveSpeed { get; set; } // �̵� �ӵ�
    #endregion // ������Ƽ

    #region �Լ�
    /** �ʱ�ȭ */
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    /** �ʱ�ȭ => ���¸� �����Ѵ� */
    private void Update()
    {
        if(Joystick != null)
        {
            // �÷��̾� �Է�ó��
            PlayerInput();
        }
    }

    /** �ʱ�ȭ => ���¸� �����Ѵ� */
    private void FixedUpdate()
    {
        if (Joystick != null)
        {
            // �÷��̾� �̵�
            PlayerMove();
            // �÷��̾� �̵� �ִϸ��̼�
            PlayerMoveAnimation();
        }
    }

    /** �÷��̾� �Է�ó�� */
    private void PlayerInput()
    {
        // ������
        moveDirection.x = Joystick.Horizontal;
        moveDirection.z = Joystick.Vertical;
        moveDirection.Normalize();
    }

    /** �÷��̾� �̵� */
    private void PlayerMove()
    {
        // �̵������� 0�� �ƴ� ���
        if(moveDirection != Vector3.zero)
        {
            // ���� �ٶ󺸰� �ִ� ������ ��ȣ�� ���ư� ������ ��ȣ�� �ٸ� ���
            if(Mathf.Sign(this.transform.forward.x) != Mathf.Sign(moveDirection.x) ||
                Mathf.Sign(this.transform.forward.z) != Mathf.Sign(moveDirection.z))
            {
                // ���� ȸ�� ��Ų��
                this.transform.Rotate(0, 1, 0);
            }

            // ������Ʈ�� ��ǥ ������ �ٶ󺸰� ����
            this.transform.forward = Vector3.Lerp(this.transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
        }

        // ���⿡ ���� �̵��ӵ� ��ŭ �̵��Ѵ�
        rigidBody.MovePosition(this.transform.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    /** �÷��̾� �̵� �ִϸ��̼� */
    private void PlayerMoveAnimation()
    {
        if(moveDirection != Vector3.zero)
        {
            animator.SetBool("isMove", true);
        }
        else
        {
            animator.SetBool("isMove", false);
        }
    }
    #endregion // �Լ�
}
