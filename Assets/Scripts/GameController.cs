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
        int score = CalcScore();
        scoreText.text = "Score :" + score + "m";

        lifePanel.UpdateLife(nejiko.Life());

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
        SceneManager.LoadScene("Title");
    }
}

