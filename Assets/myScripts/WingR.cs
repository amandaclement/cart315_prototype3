using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RIGHT WING SCRIPT

public class WingR : MonoBehaviour
{
    public GameObject player;
    public bool collectedComponent = false;

    public Light wingRLightTop; // inventory light
    public Light wingRLightBottom; // inventory light

    public Light wingRLight; // component glow effect
    public Light playerLight; // player glow effect

    private Animator anim;
    private CharacterController controller;

    public AudioSource SFX;

    public bool colliding = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject == player)
        {
            collectedComponent = true;
            wingRLight.intensity = 0; // once collected, remove light
            anim.SetTrigger("Flip"); // make player flip once
            Invoke("sound", 0.15f); // slightly delay the sound effect

            this.tag = "Untagged"; // remove component tag so that it isn't taken into account when player needs hint

            // hide object by disabling render
            MeshRenderer render = gameObject.GetComponentInChildren<MeshRenderer>();
            render.enabled = false;

            colliding = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject == player)
        {
            colliding = false;
            // once collided, destroy the component's rigidbody to disable collider activity
            Destroy(GetComponent<Rigidbody>());
        }
    }

    void sound()
    {
        SFX.Play(); // sound effect
    }

    // START
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = player.GetComponentInChildren<Animator>(); // for the character animations

        playerLight.intensity = 0;
        wingRLightTop.intensity = 0;
        wingRLightBottom.intensity = 0;
    }

    void playerLightFade()
    {
        playerLight.intensity -= 0.2f * Time.deltaTime;
    }

    // UPDATE
    private void FixedUpdate()
    {
        if (collectedComponent)
        {
            if (wingRLightTop.intensity < 1)
            {
                wingRLightTop.intensity += 0.8f * Time.deltaTime;
            }
            if (wingRLightBottom.intensity < 1)
            {
                wingRLightBottom.intensity += 0.8f * Time.deltaTime;
            }

            if (playerLight.intensity < 0.1f)
            {
                playerLight.intensity += 0.1f * Time.deltaTime;
                Invoke("playerLightFade", 0.8f);
            }
            wingRLight.intensity = 0;
        } else
        {
            wingRLight.intensity = Mathf.PingPong(Time.time * 0.1f, 0.2f); // fade in/out light until collected
        }
    }
}
