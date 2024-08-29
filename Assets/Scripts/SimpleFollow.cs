using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    Vector3 diff; //���߂�ꂽ������

    public GameObject target; //�v���C���[
    public float followSpeed; //�ǂ����X�s�[�h

    // Start is called before the first frame update
    void Start()
    {
        //���X�̃J�����ƃv���C���[�̋����������̂܂܍̗p
        diff = target.transform.position - transform.position;
    }

    // Update is called once per frame
    //LateUpdate��Update�̌�ɔ���
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            target.transform.position - diff,
            Time.deltaTime * followSpeed);
    }
}
