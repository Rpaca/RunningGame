using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSpawner : MonoBehaviour
{
    private GameObject player = null;
    private Vector3 position_offset = Vector3.zero;
    public GameObject item = null;

    private float time;
    private int waitingTime;
    private bool debug = false;
    private float minDelay;
    private float maxDelay;

    void Start()
    {
        time = 0.0f;
        waitingTime = 40;
        // 멤버 변수 player에 Player 오브젝트를 할당.
        this.player = GameObject.FindGameObjectWithTag("Player");
        // 카메라 위치(this.transform.position)와 플레이어 위치(this.player.transform.position)의 차이.
        this.position_offset = this.transform.position - this.player.transform.position;
        minDelay = 12;
        maxDelay = 18;
    }

    void Update()
    {
        time += Time.deltaTime;
        float delay = Random.Range(minDelay, maxDelay);

        if (time > waitingTime && debug == false) // 첫 수리검
        {
            Instantiate(item, this.transform.position, this.transform.rotation);
            time = 0;
            debug = true;
        }

        if (time > delay && debug == true) // 이후 수리검
        {
                Instantiate(item, this.transform.position, this.transform.rotation);
            time = 0;
        }

        // 카메라 현재 위치를 new_position에 할당.
        Vector3 new_position = this.transform.position;
        // 플레이어의 X좌표에 차이 값을 더해서 new_po sition의 X에 대입.
        new_position.x = this.player.transform.position.x + this.position_offset.x;
        // 카메라 위치를 새로운 위치(new_position)로 갱신.
        this.transform.position = new_position;
    }
}