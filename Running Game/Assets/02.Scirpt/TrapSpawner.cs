using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    private GameObject player = null;
    private Vector3 position_offset = Vector3.zero;
    public GameObject kunai = null;

    private float minDelay;
    private float maxDelay;
    private float time;
    private float poaisionDelay;
    private int waitingTime;
    private float reaTime;
    private int level;
    bool debug = false;
    bool isExist = false;

    void Start()
    {
        level = 1;
        time = 0.0f;
        waitingTime = 16;
        minDelay = 4;
        maxDelay = 6;
        poaisionDelay = 40;
        // 멤버 변수 player에 Player 오브젝트를 할당.
        this.player = GameObject.FindGameObjectWithTag("Player");
        // 카메라 위치(this.transform.position)와 플레이어 위치(this.player.transform.position)의 차이.
        this.position_offset = this.transform.position - this.player.transform.position;
    }

    void Update()
    {
        time += Time.deltaTime;
        reaTime += Time.deltaTime; //0.24. 최소 연속 횟수
        if (reaTime > 16.0f) { minDelay = 1.5f; maxDelay = 6.0f; }
        if (reaTime > 30.0f) { minDelay = 3.0f; maxDelay = 6.0f; }
        if (reaTime > 65.0f) { minDelay = 2.0f; maxDelay = 4.5f; }
        if (reaTime > 16.0f) { level = 2; }
        if (reaTime > 60.0f) { level = 3; }

        float delay = Random.Range(minDelay, maxDelay);

        if (time > waitingTime && !debug ) // 첫 수리검
        {
            Instantiate(kunai, this.transform.position, this.transform.rotation);
            time = 0;
            debug = true;
        }

        if (time > delay && debug && !isExist) // 이후 수리검
        {
            float rand = Random.Range(0, level);
            int int_rand = (int)rand;
            Debug.Log(int_rand);
            if (int_rand == 0)
                Instantiate(kunai, this.transform.position, this.transform.rotation);
            if (int_rand == 1)
                StartCoroutine(CreatDoubleTrap());
            if (int_rand == 2)
                StartCoroutine(CreatTripleTrap());
            time = 0;
        }

        if (reaTime > poaisionDelay && debug)
        {
            float rand = Random.Range(0, 2);
            int int_rand = (int)rand;
            if (int_rand == 0)
                this.gameObject.transform.position = new Vector3(this.transform.position.x, 1, this.transform.position.z);
            else
                this.gameObject.transform.position = new Vector3(this.transform.position.x, 2.5f, this.transform.position.z);
        }

        // 카메라 현재 위치를 new_position에 할당.
        Vector3 new_position = this.transform.position;
        // 플레이어의 X좌표에 차이 값을 더해서 new_po sition의 X에 대입.
        new_position.x = this.player.transform.position.x + this.position_offset.x;
        // 카메라 위치를 새로운 위치(new_position)로 갱신.
        this.transform.position = new_position;
    }

    IEnumerator CreatDoubleTrap()
    {
        isExist = true;
        Instantiate(kunai, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(0.3f);
        Instantiate(kunai, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(1.0f);
        isExist = false;
    }

    IEnumerator CreatTripleTrap()
    {
        isExist = true;
        Instantiate(kunai, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(0.3f);
        Instantiate(kunai, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(0.3f);
        Instantiate(kunai, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(1.0f);
        isExist = false;
    }
}
