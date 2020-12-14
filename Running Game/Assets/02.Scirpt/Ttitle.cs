using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Ttitle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.anyKeyDown)
        //{
        //    SceneManager.LoadScene("Main");
        //}
    }

    public void gameStart()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1;
        return;
    }

    public void goTitle()
    {
        SceneManager.LoadScene("Title");
        Time.timeScale = 1;
        return;
    }
}
