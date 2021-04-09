using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingR : MonoBehaviour
{
    public GameObject player;
    public GameObject WingHide;
    public bool collectedWingR = false;

    public Light wingRLightTop; // inventory light
    public Light wingRLightBottom; // inventory light

    public Light wingRLight; // glow effect

    private Animator anim;
    private CharacterController controller;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject == player)
        {
            WingHide.SetActive(false); // hide part once found
            collectedWingR = true;
            anim.SetTrigger("Flip"); // make player flip once

            wingRLight.GetComponent<Light>().intensity = 0; // once collected, remove light

            // light up that specific part in inventory (fade in)
            if (wingRLightTop.intensity < 1)
            {
                wingRLightTop.intensity += 0.8f * Time.deltaTime;
            }
            if (wingRLightBottom.intensity < 1)
            {
                wingRLightBottom.intensity += 0.8f * Time.deltaTime;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = player.GetComponentInChildren<Animator>(); // for the character animations
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!collectedWingR)
        {
            wingRLight.GetComponent<Light>().intensity = Mathf.PingPong(Time.time * 0.1f, 0.13f); // fade in/out light until collected
        }
    }
}
