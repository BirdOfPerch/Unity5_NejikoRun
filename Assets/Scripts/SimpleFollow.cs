using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    Vector3 diff; //決められた距離感

    public GameObject target; //プレイヤー
    public float followSpeed; //追いつくスピード

    // Start is called before the first frame update
    void Start()
    {
        //元々のカメラとプレイヤーの距離感をそのまま採用
        diff = target.transform.position - transform.position;
    }

    // Update is called once per frame
    //LateUpdateはUpdateの後に発動
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            target.transform.position - diff,
            Time.deltaTime * followSpeed);
    }
}
