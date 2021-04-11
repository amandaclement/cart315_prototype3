using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterL : MonoBehaviour
{
    public GameObject player;
    public bool collectedComponent = false;

    public Light boosterLLightTop; // inventory light
    public Light boosterLLightBottom; // inventory light

    public Light boosterLLight; // component glow effect
    public Light playerLight; // player glow effect

    private Animator anim;
    private CharacterController controller;

    public AudioSource SFX;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject == player)
        {
            collectedComponent = true;
            boosterLLight.intensity = 0; // once collected, remove light
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
        boosterLLightTop.intensity = 0;
        boosterLLightBottom.intensity = 0;
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
            if (boosterLLightTop.intensity < 1)
            {
                boosterLLightTop.intensity += 0.8f * Time.deltaTime;
            }
            if (boosterLLightBottom.intensity < 1)
            {
                boosterLLightBottom.intensity += 0.8f * Time.deltaTime;
            }

            if (playerLight.intensity < 0.08f)
            {
                playerLight.intensity += 0.08f * Time.deltaTime;
                Invoke("playerLightFade", 0.8f);
            }
            boosterLLight.intensity = 0;
        }
        else
        {
            boosterLLight.intensity = Mathf.PingPong(Time.time * 0.1f, 0.13f); // fade in/out light until collected
        }

    }
}
