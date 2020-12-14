﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    public float step_timer = 0.0f; // 경과 시간을 유지한다.
    private PlayerControl player = null;
    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }

    void Update()
    {
        this.step_timer += Time.deltaTime; // 경과 시간을 더해 간다.

        if (this.player.isPlayEnd())
        {
            GameManager.instance.ResultOn();
            //SceneManager.LoadScene("Title");
        }
    }
    public float getPlayTime()
    {
        float time;
        time = this.step_timer;
        return (time); // 호출한 곳에 경과 시간을 알려준다.
    }
}
