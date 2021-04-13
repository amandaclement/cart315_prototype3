using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// LEFT WING SCRIPT

public class WingL : MonoBehaviour
{
    public GameObject player;
    public bool collectedComponent = false;

    public Light wingLLightTop; // inventory light
    public Light wingLLightBottom; // inventory light

    public Light wingLLight; // component glow effect
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
            wingLLight.intensity = 0; // once collected, remove light
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
        wingLLightTop.intensity = 0;
        wingLLightBottom.intensity = 0;
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
            if (wingLLightTop.intensity < 1)
            {
                wingLLightTop.intensity += 0.8f * Time.deltaTime;
            }
            if (wingLLightBottom.intensity < 1)
            {
                wingLLightBottom.intensity += 0.8f * Time.deltaTime;
            }

            if (playerLight.intensity < 0.1f)
            {
                playerLight.intensity += 0.1f * Time.deltaTime;
                Invoke("playerLightFade", 0.8f);
            }
            wingLLight.intensity = 0;
        }
        else
        {
            wingLLight.intensity = Mathf.PingPong(Time.time * 0.1f, 0.2f); // fade in/out light until collected
        }
    }
}
