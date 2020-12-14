using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    private float nextTime = 6.0f;
    private float time = 0;
    void Update()
    {
        time += Time.deltaTime;
        Vector3 velocity = this.GetComponent<Rigidbody>().velocity; 
        velocity.x = -3;
        this.GetComponent<Rigidbody>().velocity = velocity;

        if (time >= nextTime)
        {
            Destroy(this.gameObject);
        }

    }
}
