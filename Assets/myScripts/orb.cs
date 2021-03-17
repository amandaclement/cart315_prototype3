using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orb : MonoBehaviour
{
    public GameObject player;
    public Light playerHalo;
    private float minDistance = 2;
    private float currentIntensity = 0.0f;

    void collectOrb()
    {
        float dist = Vector3.Distance(player.transform.position, this.transform.position);

        if (dist<minDistance)
        {
            Destroy(gameObject); // destroy the orb
            playerHalo.intensity = currentIntensity + 0.06f; // increase player's brightness
        }
        currentIntensity = playerHalo.intensity;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        collectOrb();
        transform.Rotate(50 * Time.deltaTime, 0, 0); // to constantly rotate orb on x-axis
    }
}
