using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePanel : MonoBehaviour
{
    public GameObject[] icons;
    public void UpdateLife(int life)
    {
        //���C�t�ɉ����ă��C�t�A�C�R����\���E��\���ɂ���
        for (int i = 0; i < icons.Length; i++)
        {
            if (i < life) icons[i].SetActive(true);
            else icons[i].SetActive(false);
        }
    }
}
