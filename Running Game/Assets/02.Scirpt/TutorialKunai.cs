using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialKunai : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x >= 35)
        {
            Vector3 velocity = this.GetComponent<Rigidbody>().velocity;
            velocity.x = -7;
            this.GetComponent<Rigidbody>().velocity = velocity;
        }
    }

}
