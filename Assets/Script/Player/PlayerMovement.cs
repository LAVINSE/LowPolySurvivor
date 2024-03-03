using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    #region ����
    [SerializeField] private float moveSpeed; // �̵� �ӵ�
    [SerializeField] private float rotationSpeed; // ȸ�� �ӵ�

    private Vector3 moveDirection = Vector3.zero;

    private Rigidbody rigidBody;
    #endregion // ����

    #region �Լ�
    /** �ʱ�ȭ */
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    /** �ʱ�ȭ => ���¸� �����Ѵ� */
    private void Update()
    {
        // �÷��̾� �Է�ó��
        PlayerInput();
    }

    /** �ʱ�ȭ => ���¸� �����Ѵ� */
    private void FixedUpdate()
    {
        // �÷��̾� �̵�
        PlayerMove();
    }

    /** �÷��̾� �Է�ó�� */
    private void PlayerInput()
    {
        // ������
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.z = Input.GetAxisRaw("Vertical");
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
    #endregion // �Լ�
}
