
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

    Vector3 moveDirection = Vector3.zero;　//キャラクターの動くべき座標
    int targetLane;
    int life = DefaultLife;
    float recoverTime = 0.0f;

    public float gravity; //重力
    public float speedZ; //キャラクターのスピード
    public float speedX; //横方向のスピード
    public float speedJump; //キャラクターのジャンプ力
    public float accelerationZ; //前進加速度

    public int Life()
    {
        return life;
    }

    //気絶判定
    bool IsStun()
    {
        return recoverTime > 0.0f || life <= 0;
    }

    void Start()
    {

        //必要なコンポーネントを自動取得
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //キャラクターコンポーネントのisGroundedプロパティでは常に接地判定をしている
        // if (controller.isGrounded)
        //{

        //デバック用
        if (Input.GetKeyDown("left")) MoveToLeft();
        if (Input.GetKeyDown("right")) MoveToRight();
        if (Input.GetKeyDown("space")) Jump();

        //気絶時の処理
        if (IsStun())
        {
            moveDirection.x = 0.0f;
            moveDirection.z = 0.0f;
            recoverTime -= Time.deltaTime;
        }
        else
        {
            //常に前方向に加速して前進する
            float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
            moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

            //横方向に移動　現在地と目標地点の距離の差で速度が変化する
            float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
            moveDirection.x = ratioX * speedX;
        }



        //上キー(Wキー)がおされたら

        //    if (Input.GetAxis("Vertical") > 0.0f)
        //    {
        //        moveDirection.z = Input.GetAxis("Vertical") * speedZ;
        //    }
        //    else
        //    {
        //        moveDirection.z = 0;
        //    }
        //    //}

        //    //左右キーにてキャラクターを直接回転させる命令
        //    transform.Rotate(0, Input.GetAxis("Horizontal") * 3, 0);

        //    //もしもジャンプボタンがおされたら
        //    if (Input.GetButton("Jump"))
        //    {
        //        moveDirection.y = speedJump;
        //        //Jumpアニメの発動
        //        animator.SetTrigger("jump");
        //    }
        //}

        //重力分の力を毎フレーム追加
        moveDirection.y -= gravity * Time.deltaTime;

        //移動実行
        //キャラクターの向きを考慮してローカル(キャラクター視点の座標）からグローバルな座標に再計算
        Vector3 globalDirection = transform.TransformDirection(moveDirection);

        //Moveメソッド：指定した引数の座標にプレイヤーを動かす
        controller.Move(globalDirection * Time.deltaTime);

        //移動後設置してたらY方向の速度はリセットする
        if (controller.isGrounded) moveDirection.y = 0;

        //速度が0以上なら走っているアニメフラグをtrueにする
        animator.SetBool("run", moveDirection.z > 0.0f);
    }

    //左のレーンに移動
    public void MoveToLeft()
    {
        //気絶時に処理を止める
        if (IsStun()) return;

        if (controller.isGrounded && targetLane > MinLane) targetLane--;
    }

    //右のレーンに移動
    public void MoveToRight()
    {
        //気絶時に処理を止める
        if (IsStun()) return;

        if (controller.isGrounded && targetLane < MaxLane) targetLane++;
    }

    public void Jump()
    {
        //気絶時に処理を止める
        if (IsStun()) return;

        if (controller.isGrounded)
        {
            moveDirection.y = speedJump;

            //Jumpトリガーの設定
            animator.SetTrigger("jump");
        }
    }

    //CharacterControllerに衝突判定が発生したとき
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (IsStun()) return;

        if (hit.gameObject.tag == "Robo")
        {
            //ライフを減らして気絶する
            life--;
            recoverTime = StunDuration;

            //damageアニメーションを再生
            animator.SetTrigger("damage");

            //オブジェクトを削除
            Destroy(hit.gameObject);
        }

    }
}
