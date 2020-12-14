using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{


    public AudioSource m_Audio;
    public ParticleSystem m_ClashEffect;



    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        if (collision.gameObject.tag == "Trap")
        {
            Debug.Log("hit");
            collision.gameObject.transform.Rotate(new Vector3(0, 0, -45));
            collision.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            m_Audio.Play();
            m_ClashEffect.Play();
        }
    }
}
