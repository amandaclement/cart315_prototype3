using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatOff : MonoBehaviour
{

    public GameObject moon;
    public float minDistance;

    public Transform target;
    private float speed = 8f;

    public GameObject player;
    public Light playerHalo;


    // when in range, player floats to moon
    void floatToMoon()
    {
        float dist = Vector3.Distance(moon.transform.position, this.transform.position);

        Debug.Log(dist);

        if (dist < minDistance)
        {
            playerHalo.enabled = true; // make player glow
            Destroy(GetComponent<Player>()); // destroy player script to disable controls (caused shaking issue)

            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        target = moon.transform;
        playerHalo.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        floatToMoon();
    }
}
