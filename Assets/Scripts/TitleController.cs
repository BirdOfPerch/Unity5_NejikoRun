using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{

    //�X�^�[�g�{�^���������ꂽ�Ƃ�Stage�Ɉړ�
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Stage");
    }
}
