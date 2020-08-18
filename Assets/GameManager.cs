using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Animator scorelabelanimator;
    public Image timebar;
    public float TotalTime = 10;

    public TMP_Text scoreLabel;
    public TMP_Text questionLabel;

    public TMP_Text[] optionLabel;
    public QuestionData[] questions;

    private int CurrentQuestionIndex;
    private int Currentscore;
    private bool IsGameActive;
    private float Timer;
    // Start is called before the first frame update
    void Start()
    {
        IsGameActive = true;

        RestartTimer();
        scoreLabel.text = "0";

        questionLabel.text = questions[0].question;

        optionLabel[0].text = questions[0].options[0].option;

        for (int i = 0; i < questions[0].options.Length; i++)
        {

            optionLabel[i].text = questions[0].options[i].option;

        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!IsGameActive)
            return;
        Timer -= Time.deltaTime;
        timebar.fillAmount = Timer / TotalTime;

        if(Timer < 0)
        {
            NextQuestion();
        }
    }
    private void NextQuestion()
    {

        RestartTimer();

      CurrentQuestionIndex++;

        if (CurrentQuestionIndex < questions.Length)
        {
                questionLabel.text = questions[CurrentQuestionIndex].question;

                for (int i = 0; i < questions[CurrentQuestionIndex].options.Length; i++)
                {
                    optionLabel[i].text = questions[CurrentQuestionIndex].options[i].option;
                }
        }
        else
        {
            Debug.Log("Game Over");
            IsGameActive = false;
        }

    }
    public void OptionSelected(int index)
    {
        if (!IsGameActive)
            return;

        if (questions[CurrentQuestionIndex].options[index].isCorrect)
        {
            Debug.Log("correcta");
            Currentscore += 15;
            scoreLabel.text = Currentscore.ToString();
            scorelabelanimator.SetTrigger("animacion puntaje");
        }
        else
        {
            Debug.Log("incorrecta");
            Currentscore -= 5;
            scoreLabel.text = Currentscore.ToString();
        }
            NextQuestion();
    }
    private void RestartTimer() 
    {
        Timer = TotalTime;
        timebar.fillAmount = 1.0f;
    }
}

[System.Serializable]
public struct QuestionData
{
    public string question;
    public Option[] options;
}
[System.Serializable]
public struct Option
{
    public string option;
    public bool isCorrect;
}
