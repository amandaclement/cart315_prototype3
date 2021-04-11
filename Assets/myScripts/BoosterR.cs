using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterR : MonoBehaviour
{
    public GameObject player;
    public bool collectedComponent = false;

    public Light boosterRLightTop; // inventory light
    public Light boosterRLightBottom; // inventory light

    public Light boosterRLight; // component glow effect
    public Light playerLight; // player glow effect

    private Animator anim;
    private CharacterController controller;

    public AudioSource SFX;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject == player)
        {
            collectedComponent = true;
            boosterRLight.intensity = 0; // once collected, remove light
            anim.SetTrigger("Flip"); // make player flip once
            Invoke("sound", 0.15f); // slightly delay the sound effect

            // once collided, destroy the component's rigidbody to disable collider activity
            Destroy(GetComponent<Rigidbody>());
            // hide object by disabling render
            MeshRenderer render = gameObject.GetComponentInChildren<MeshRenderer>();
            render.enabled = false;
        }
    }

    void sound()
    {
        SFX.Play(); // sound effect
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = player.GetComponentInChildren<Animator>(); // for the character animations

        playerLight.intensity = 0;
        boosterRLightTop.intensity = 0;
        boosterRLightBottom.intensity = 0;
    }

    void playerLightFade()
    {
        playerLight.intensity -= 0.2f * Time.deltaTime;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (collectedComponent)
        {
            if (boosterRLightTop.intensity < 1)
            {
                boosterRLightTop.intensity += 0.8f * Time.deltaTime;
            }
            if (boosterRLightBottom.intensity < 1)
            {
                boosterRLightBottom.intensity += 0.8f * Time.deltaTime;
            }

            if (playerLight.intensity < 0.08f)
            {
                playerLight.intensity += 0.08f * Time.deltaTime;
                Invoke("playerLightFade", 0.8f);
            }
            boosterRLight.intensity = 0;
        }
        else
        {
            boosterRLight.intensity = Mathf.PingPong(Time.time * 0.1f, 0.13f); // fade in/out light until collected
        }
    }
}
