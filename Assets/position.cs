using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class position : MonoBehaviour
{
    public GameObject spaceship;
    public float x = 0.0f;
    public float y = 0.0f;
    public float z = 0.0f;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        //Vector3 screenPoint = Camera.main.ViewportToScreenPoint(new Vector3(0, 0, 0));
        //spaceship.transform.position = screenPoint;

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        spaceship.transform.position = worldPoint;
    }


}
