using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public NejikoController nejiko;
    public TextMeshProUGUI scoreText;
    public LifePanel lifePanel;

    public void Update()
    {
        //スコアの表示
        int score = CalcScore();
        scoreText.text = "Score :" + score + "m";

        //キャラクターのLifeとUIのLifeを連携させる
        lifePanel.UpdateLife(nejiko.Life());

        //キャラクターのLifeが0になったらタイトルに戻される
        if (nejiko.Life() <= 0)
        {
            enabled = false;

            Invoke("ReturnToTitle", 2.0f);
        }
    }

    int CalcScore()
    {
        return (int)nejiko.transform.position.z;
    }

    void ReturnToTitle()
    {
        //LoadSceneでタイトルへ
        SceneManager.LoadScene("Title");
    }
}

