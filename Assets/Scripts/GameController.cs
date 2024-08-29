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
        //�X�R�A�̕\��
        int score = CalcScore();
        scoreText.text = "Score :" + score + "m";

        //�L�����N�^�[��Life��UI��Life��A�g������
        lifePanel.UpdateLife(nejiko.Life());

        //�L�����N�^�[��Life��0�ɂȂ�����^�C�g���ɖ߂����
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
        //LoadScene�Ń^�C�g����
        SceneManager.LoadScene("Title");
    }
}

