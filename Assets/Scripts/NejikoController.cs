
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NejikoController : MonoBehaviour
{

    const int MinLane = -2;
    const int MaxLane = 2;
    const float LaneWidth = 1.0f;
    const int DefaultLife = 3;
    const float StunDuration = 0.5f;


    CharacterController controller;
    Animator animator;

    Vector3 moveDirection = Vector3.zero;�@//�L�����N�^�[�̓����ׂ����W
    int targetLane;
    int life = DefaultLife;
    float recoverTime = 0.0f;

    public float gravity; //�d��
    public float speedZ; //�L�����N�^�[�̃X�s�[�h
    public float speedX; //�������̃X�s�[�h
    public float speedJump; //�L�����N�^�[�̃W�����v��
    public float accelerationZ; //�O�i�����x

    public int Life()
    {
        return life;
    }

    //�C�┻��
    bool IsStun()
    {
        return recoverTime > 0.0f || life <= 0;
    }

    void Start()
    {

        //�K�v�ȃR���|�[�l���g�������擾
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //�L�����N�^�[�R���|�[�l���g��isGrounded�v���p�e�B�ł͏�ɐڒn��������Ă���
        // if (controller.isGrounded)
        //{

        //�f�o�b�N�p
        if (Input.GetKeyDown("left")) MoveToLeft();
        if (Input.GetKeyDown("right")) MoveToRight();
        if (Input.GetKeyDown("space")) Jump();

        //�C�⎞�̏���
        if (IsStun())
        {
            moveDirection.x = 0.0f;
            moveDirection.z = 0.0f;
            recoverTime -= Time.deltaTime;
        }
        else
        {
            //��ɑO�����ɉ������đO�i����
            float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
            moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

            //�������Ɉړ��@���ݒn�ƖڕW�n�_�̋����̍��ő��x���ω�����
            float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
            moveDirection.x = ratioX * speedX;
        }



        //��L�[(W�L�[)�������ꂽ��

        //    if (Input.GetAxis("Vertical") > 0.0f)
        //    {
        //        moveDirection.z = Input.GetAxis("Vertical") * speedZ;
        //    }
        //    else
        //    {
        //        moveDirection.z = 0;
        //    }
        //    //}

        //    //���E�L�[�ɂăL�����N�^�[�𒼐ډ�]�����閽��
        //    transform.Rotate(0, Input.GetAxis("Horizontal") * 3, 0);

        //    //�������W�����v�{�^���������ꂽ��
        //    if (Input.GetButton("Jump"))
        //    {
        //        moveDirection.y = speedJump;
        //        //Jump�A�j���̔���
        //        animator.SetTrigger("jump");
        //    }
        //}

        //�d�͕��̗͂𖈃t���[���ǉ�
        moveDirection.y -= gravity * Time.deltaTime;

        //�ړ����s
        //�L�����N�^�[�̌������l�����ă��[�J��(�L�����N�^�[���_�̍��W�j����O���[�o���ȍ��W�ɍČv�Z
        Vector3 globalDirection = transform.TransformDirection(moveDirection);

        //Move���\�b�h�F�w�肵�������̍��W�Ƀv���C���[�𓮂���
        controller.Move(globalDirection * Time.deltaTime);

        //�ړ���ݒu���Ă���Y�����̑��x�̓��Z�b�g����
        if (controller.isGrounded) moveDirection.y = 0;

        //���x��0�ȏ�Ȃ瑖���Ă���A�j���t���O��true�ɂ���
        animator.SetBool("run", moveDirection.z > 0.0f);
    }

    //���̃��[���Ɉړ�
    public void MoveToLeft()
    {
        //�C�⎞�ɏ������~�߂�
        if (IsStun()) return;

        if (controller.isGrounded && targetLane > MinLane) targetLane--;
    }

    //�E�̃��[���Ɉړ�
    public void MoveToRight()
    {
        //�C�⎞�ɏ������~�߂�
        if (IsStun()) return;

        if (controller.isGrounded && targetLane < MaxLane) targetLane++;
    }

    public void Jump()
    {
        //�C�⎞�ɏ������~�߂�
        if (IsStun()) return;

        if (controller.isGrounded)
        {
            moveDirection.y = speedJump;

            //Jump�g���K�[�̐ݒ�
            animator.SetTrigger("jump");
        }
    }

    //CharacterController�ɏՓ˔��肪���������Ƃ�
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (IsStun()) return;

        if (hit.gameObject.tag == "Robo")
        {
            //���C�t�����炵�ċC�₷��
            life--;
            recoverTime = StunDuration;

            //damage�A�j���[�V�������Đ�
            animator.SetTrigger("damage");

            //�I�u�W�F�N�g���폜
            Destroy(hit.gameObject);
        }

    }
}
