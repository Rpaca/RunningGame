using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBox : MonoBehaviour
{
    public GameObject tutorailBox;

    public void OpenTutorialBox()
    {
        tutorailBox.SetActive(true);
    }

    public void CloseTutorialBox()
    {
        tutorailBox.SetActive(false);
    }
}
