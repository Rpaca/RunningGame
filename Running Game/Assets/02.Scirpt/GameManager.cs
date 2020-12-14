using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text scoreText;
    public Text comboText;
    public Text comboLogoText;
    public Slider progressBar;
    public GameObject Result;
    public GameObject Clear;
    public GameObject Life;
    public Text lastScore;
    public Text lastCombo;

    private float score = 0;
    private float combo = 0;
    private float distance = 0;
    private float goal = 100;
    private float MaxCombo = 0;

    private float time = 0;
    private float nextTime = 1.0f;

    private void Start()
    {
        comboLogoText.enabled = false;
        comboText.enabled = false;
        Clear.SetActive(false);
        Result.SetActive(false);
        progressBar.maxValue = goal;
    }


    private void Update()
    {
        time += Time.deltaTime;
        if (time > nextTime)
        {
            distance++;
            time = 0;
        }
        progressBar.value = distance;

        if (distance >= goal)
        {
            Time.timeScale = 0;
            Clear.SetActive(true);
            lastCombo.text = MaxCombo.ToString("N0");
            lastScore.text = score.ToString("N0");
        }
    } 

    void Awake()
    {
        if (!instance) //정적으로 자신을 체크합니다.
            instance = this; //정적으로 자신을 저장합니다.
    }

    public void AddScore(int n) //점수를 추가해주는 함수를 만들어 줍니다.
    {
        score += n; //점수를 더해줍니다.
        score += (combo * 1.3f);
        scoreText.text = score.ToString("N0"); //텍스트에 반영합니다.
    }

    public void AddCombo(int num) //점수를 추가해주는 함수를 만들어 줍니다.
    {
        if (comboLogoText.enabled == false)
        {
            comboLogoText.enabled = true;
            comboText.enabled = true;
        }
        combo += num; //점수를 더해줍니다.
        comboText.text = combo.ToString("N0"); //텍스트에 반영합니다.
        if (MaxCombo <= combo)
            MaxCombo = combo;
    }

    public void ResetCombo()
    {
        comboLogoText.enabled = false;
        comboText.enabled = false;
        combo = 0; //점수를 더해줍니다.
        comboText.text = combo.ToString("N0"); //텍스트에 반영합니다.
    }

    public void ResultOn()
    {
        if(!Result.activeSelf)
           Time.timeScale = 0;
        Result.SetActive(true);
    }

    public void OnDamaged(int life)
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject heart = Life.transform.GetChild(i).gameObject;
            heart.SetActive(false);
        }


        for (int i = 0; i < life; i++)
        {
            GameObject heart = Life.transform.GetChild(i).gameObject;
            heart.SetActive(true);
        }
    }
}
