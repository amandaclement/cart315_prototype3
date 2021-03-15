using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatOff : MonoBehaviour
{
    private float movementSpeed = 0.5f;


    public GameObject moon;
    private float minDistance;

    public Transform target;
    private float speed = 8f;


    // when in range, player floats to moon
    void floatToMoon()
    {
        float dist = Vector3.Distance(moon.transform.position, this.transform.position);
        minDistance = 120;

        if (dist < minDistance)
        {
            Destroy(GetComponent<Player>()); // destroy player script to disable controls (caused shaking issue)

            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        target = moon.transform;
    }

    // Update is called once per frame
    void Update()
    {
        floatToMoon();
    }
}
