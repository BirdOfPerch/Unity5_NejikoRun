using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{

    //スタートボタンが押されたときStageに移動
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Stage");
    }
}
