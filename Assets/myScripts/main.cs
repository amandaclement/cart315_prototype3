using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// MAIN SCRIPT

public class main : MonoBehaviour
{
    private Animator anim;

    // FOR INTRO CAMERA PANNING
    public GameObject camRotator;
    public float transitionSpeed;
    public float translationSpeed;
    float translationFull = 750;
    // main cam target coordinates
    public float x;
    public float y;
    public float z;
    bool enableCameraControls = false;

    public GameObject spaceshipIntro;

    // FOR SPACESHIP CAMERA PANNING
    // secondary cam target coordinates
    public float camSX;
    public float camSY;
    public float camSZ;
    // transition speed
    public float camSSpeed;

    // FOR GENERAL PLAYER MOVEMENT
    public GameObject player;
    public float trackingSpeed;
    public float speed;

    // FOR INSTRUCTIONS/CONTROLS
    public Text instructions;
    public Text controls;
    private float alphaAmt = 0f;
    private float alphaAmt1 = 0f;
    bool fadeInControls = false;

    // FOR FADE TO BLACK (BETWEEN SCENES/CAMERA SWITCH)
    public Image blackout;
    private float alphaAmt2 = 0f;

    // FOR CAMERA CONTROLS / MOVEMENT
    public Transform lookAt;
    public Transform camTransform;
    public Camera mainCam;
    public Camera secondaryCam;
    public float distance = 5.0f;

    public float currentX = 0.0f;
    public float currentY = 0.0f;
    public float currentZ = 0.0f;

    private const float Y_ANGLE_MIN = 25.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    // FOR INVENTORY
    public GameObject spaceship;
    public GameObject body;
    public GameObject wingR;
    public GameObject wingL;
    public GameObject boosterR;
    public GameObject boosterL;
    public GameObject engine;
    public float rotationSpeed = 2.0f; // rotation speed for inventory parts' rotation
    // inventory lights
    public Light bodyLightTop;
    public Light bodyLightBottom;
    public Light wingRLightTop;
    public Light wingRLightBottom;
    public Light wingLLightTop;
    public Light wingLLightBottom;
    public Light boosterRLightTop;
    public Light boosterRLightBottom;
    public Light boosterLLightTop;
    public Light boosterLLightBottom;
    public Light engineLightTop;
    public Light engineLightBottom;

    // FOR COLLECTABLES // public bools since they are accessed in fadingLights scripts
    public GameObject collectableBody;
    public bool collectedBody = false;
    public GameObject collectableWingR;
    public bool collectedWingR = false;
    public GameObject collectableWingL;
    public bool collectedWingL = false;
    public GameObject collectableBoosterR;
    public bool collectedBoosterR = false;
    public GameObject collectableBoosterL;
    public bool collectedBoosterL = false;
    public GameObject collectableEngine;
    public bool collectedEngine = false;
    // min necessary range between player and spaceship part in order for them to collect it
    private float minDistance = 6;

    public GameObject spaceshipReady;
    public float offsetX = 0.0f;
    public float offsetY = 0.0f;
    public float offsetZ = 0.0f;
    public GameObject moon;

    bool fadeInBlackout = true;

    public bool enteredShip = false;

    bool toMoon = false;

    public float floatSpeed = 0.0f;
    public Image whiteout; // end screen
    private float alphaAmt3 = 0f; // end screen alpha

    bool flip = false;
    public Light playerLight;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.gameObject == collectableWingR)
    //    {
    //        Debug.Log("wingR collision detected");
    //    }
    //}

    void cameraEffect()
    {
        float translation = translationSpeed * Time.deltaTime;

        if (translationFull > translation)
        {
            translationFull -= translation;
        }

        else
        {
            translation = translationFull;
            translationFull = 0;
            camRotator.transform.position = Vector3.Lerp(camRotator.transform.position, new Vector3(x, y, z), Time.deltaTime * transitionSpeed);

            Invoke("fadeInText", 1f);
        }
        camRotator.transform.Translate(0, 0, -translation);
    }

    // HANDLING FADING IN/OUT CONTROL INSTRUCTIONS
    void fadeInText()
    {

        enableCameraControls = true; // enable camera controls
        alphaAmt1 += 0.2f * Time.deltaTime; // fade in instructions

        if (Input.GetKey(KeyCode.E))
        {

            instructions.enabled = false; // hide instructions
            fadeInControls = true;
        }

        if (fadeInControls)
        {
            alphaAmt += 0.2f * Time.deltaTime; // fade in controls
            Invoke("hideControls", 0.5f);
        }

        var tempColor = controls.color;
        tempColor.a = alphaAmt;
        controls.color = tempColor;

        var tempColor1 = instructions.color;
        tempColor1.a = alphaAmt1;
        instructions.color = tempColor1;
    }

    void hideControls()
    {
        if (Input.GetKey(KeyCode.E))
        {
            controls.enabled = false; // hide controls
        }
    }

    // HANDLING FADING IN/OUT BETWEEN SCENES (once all spaceship parts have been collected)
    void fadeBlack()
    {
        if (fadeInBlackout)
        {
            alphaAmt2 += 0.9f * Time.deltaTime;
            Invoke("fadeBackIn", 1.6f);
        }

        var tempColor2 = blackout.color;
        tempColor2.a = alphaAmt2;
        blackout.color = tempColor2;
    }

    void fadeBackIn()
    {
        fadeInBlackout = false; // preventing it blackout from fading back in
        alphaAmt2 -= 0.9f * Time.deltaTime;

        var tempColor2 = blackout.color;
        tempColor2.a = alphaAmt2;
        blackout.color = tempColor2;

        spaceshipReady.SetActive(true); // reveal finalized spaceship
        spaceship.SetActive(false); // hide spaceship inventory

        secondaryCam.transform.LookAt(player.transform);

        secondaryCam.enabled = true; // switch to secondary camera view

        // place spaceship right near player (** NEED MODIFICATION/POLISHING **)
        spaceshipReady.transform.position = new Vector3(player.transform.position.x + offsetX, player.transform.position.y + offsetY, player.transform.position.z + offsetZ);

        // controlling the actual flying
        float distShip = Vector3.Distance(player.transform.position, spaceshipReady.transform.position);
        if (distShip < 13)
        {
            enteredShip = true;
            // position player in spaceship
            spaceshipReady.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z);
            Invoke("prepForFlight", 1.5f);
        }
    }

    void prepForFlight()
    {
        toMoon = true;
    }

    void fadeWhite()
    {
        if (alphaAmt3 < 1f)
        {
            alphaAmt3 += 1f * Time.deltaTime;
        }
        var tempColor3 = whiteout.color;
        tempColor3.a = alphaAmt3;
        whiteout.color = tempColor3;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.gameObject == collectableWingR)
    //    {
    //        Debug.Log("wingR collision detected");
    //        collectableWingR.SetActive(false); // hide part once found
    //        collectedWingR = true;
    //        anim.SetTrigger("Flip"); // make player flip once

    //        // light up that specific part in inventory (fade in)
    //        if (wingRLightTop.intensity < 1)
    //        {
    //            wingRLightTop.intensity += 0.8f * Time.deltaTime;
    //        }
    //        if (wingRLightBottom.intensity < 1)
    //        {
    //            wingRLightBottom.intensity += 0.8f * Time.deltaTime;
    //        }
    //    }
    //}

    // HANDLING COLLECTABLES INVENTORY
    void inventory()
    {
        // positioning inventory based on viewport
        spaceship.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 90, Screen.height - 30, Camera.main.nearClipPlane + 1));

        // constant rotation
        spaceship.transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));

        // aligning directional lights for each inventory part (part's light up when player finds them within the scene)
        bodyLightTop.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 90, Screen.height + 30, Camera.main.nearClipPlane + 1));
        bodyLightBottom.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 90, Screen.height - 90, Camera.main.nearClipPlane + 1));

        wingLLightTop.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 90, Screen.height + 30, Camera.main.nearClipPlane + 1));
        wingLLightBottom.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 90, Screen.height - 90, Camera.main.nearClipPlane + 1));

        wingRLightTop.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 90, Screen.height + 30, Camera.main.nearClipPlane + 1));
        wingRLightBottom.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 90, Screen.height - 90, Camera.main.nearClipPlane + 1));

        boosterLLightTop.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 90, Screen.height + 30, Camera.main.nearClipPlane + 1));
        boosterLLightBottom.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 90, Screen.height - 90, Camera.main.nearClipPlane + 1));

        boosterRLightTop.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 90, Screen.height + 30, Camera.main.nearClipPlane + 1));
        boosterRLightBottom.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 90, Screen.height - 90, Camera.main.nearClipPlane + 1));

        engineLightTop.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 90, Screen.height + 30, Camera.main.nearClipPlane + 1));
        engineLightBottom.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 90, Screen.height - 90, Camera.main.nearClipPlane + 1));

        // HANDLING THE FINDING OF COLLECTABLES
        // BODY
        float distBody = Vector3.Distance(player.transform.position, collectableBody.transform.position);
        if (distBody < minDistance)
        {
            collectableBody.SetActive(false); // hide part once found
            collectedBody = true;
        }
        if (collectedBody)
        {
            // light up that specific part in inventory (fade in)
            if (bodyLightTop.intensity < 1)
            {
                bodyLightTop.intensity += 0.8f * Time.deltaTime;
            }
            if (bodyLightBottom.intensity < 1)
            {
                bodyLightBottom.intensity += 0.8f * Time.deltaTime;
            }
        }
        else
        {
            // until collected, keep light off
            bodyLightTop.intensity = 0;
            bodyLightBottom.intensity = 0;
        }
        //
        // LEFT WING
        float distWingL = Vector3.Distance(player.transform.position, collectableWingL.transform.position);
        if (distWingL < minDistance)
        {
            collectableWingL.SetActive(false); // hide part once found
            collectedWingL = true;
        }
        if (collectedWingL)
        {
            // light up that specific part in inventory (fade in)
            if (wingLLightTop.intensity < 1)
            {
                wingLLightTop.intensity += 0.8f * Time.deltaTime;
            }
            if (wingLLightBottom.intensity < 1)
            {
                wingLLightBottom.intensity += 0.8f * Time.deltaTime;
            }
        }
        else
        {
            // until collected, keep light off
            wingLLightTop.intensity = 0;
            wingLLightBottom.intensity = 0;
        }
        //
        // RIGHT WING
        //float distWingR = Vector3.Distance(player.transform.position, collectableWingR.transform.position);
        //if (distWingR < minDistance)
        //{
        //    collectableWingR.SetActive(false); // hide part once found
        //    collectedWingR = true;
        //}
        //if (collectedWingR)
        //{

        //    // light up that specific part in inventory (fade in)
        //    if (wingRLightTop.intensity < 1)
        //    {
        //        wingRLightTop.intensity += 0.8f * Time.deltaTime;
        //    }
        //    if (wingRLightBottom.intensity < 1)
        //    {
        //        wingRLightBottom.intensity += 0.8f * Time.deltaTime;
        //    }
        //}
        //else
        //{
        //    // until collected, keep light off
        //    wingRLightTop.intensity = 0;
        //    wingRLightBottom.intensity = 0;
        //}
        //
        // LEFT BOOSTER
        float distBoosterL = Vector3.Distance(player.transform.position, collectableBoosterL.transform.position);
        if (distBoosterL < minDistance)
        {
            collectableBoosterL.SetActive(false); // hide part once found
            collectedBoosterL = true;
        }
        if (collectedBoosterL)
        {
            // light up that specific part in inventory (fade in)
            if (boosterLLightTop.intensity < 1)
            {
                boosterLLightTop.intensity += 0.8f * Time.deltaTime;
            }
            if (boosterLLightBottom.intensity < 1)
            {
                boosterLLightBottom.intensity += 0.8f * Time.deltaTime;
            }
        }
        else
        {
            // until collected, keep light off
            boosterLLightTop.intensity = 0;
            boosterLLightBottom.intensity = 0;
        }
        //
        // RIGHT BOOSTER
        float distBoosterR = Vector3.Distance(player.transform.position, collectableBoosterR.transform.position);
        if (distBoosterR < minDistance)
        {
            collectableBoosterR.SetActive(false); // hide part once found
            collectedBoosterR = true;
        }
        if (collectedBoosterR)
        {
            // light up that specific part in inventory (fade in)
            if (boosterRLightTop.intensity < 1)
            {
                boosterRLightTop.intensity += 0.8f * Time.deltaTime;
            }
            if (boosterRLightBottom.intensity < 1)
            {
                boosterRLightBottom.intensity += 0.8f * Time.deltaTime;
            }
        }
        else
        {
            // until collected, keep light off
            boosterRLightTop.intensity = 0;
            boosterRLightBottom.intensity = 0;
        }
        //
        // ENGINE
        float distEngine = Vector3.Distance(player.transform.position, collectableEngine.transform.position);
        if (distEngine < minDistance)
        {
            collectableEngine.SetActive(false); // hide part once found
            collectedEngine = true;
        }
        if (collectedEngine)
        {
            // light up that specific part in inventory (fade in)
            if (engineLightTop.intensity < 1)
            {
                engineLightTop.intensity += 0.8f * Time.deltaTime;
            }
            if (engineLightBottom.intensity < 1)
            {
                engineLightBottom.intensity += 0.8f * Time.deltaTime;
            }
        }
        else
        {
            // until collected, keep light off
            engineLightTop.intensity = 0;
            engineLightBottom.intensity = 0;
        }
        //

        // CHECKING IF SPACESHIP IS READY (if all parts are collected)
        if (collectedBody && collectedWingL && collectedWingR && collectedBoosterL && collectedBoosterR && collectedEngine)
        {
            Invoke("fadeBlack", 0); // fade screen to black before transitioning scenes
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>(); // for the character animations

        var tempColor = controls.color;
        tempColor.a = alphaAmt;
        controls.color = tempColor;

        var tempColor1 = instructions.color;
        tempColor1.a = alphaAmt1;
        instructions.color = tempColor1;

        var tempColor2 = blackout.color;
        tempColor2.a = alphaAmt2;
        blackout.color = tempColor2;

        var tempColor3 = whiteout.color;
        tempColor3.a = alphaAmt3;
        whiteout.color = tempColor3;

        spaceshipReady.SetActive(false); // hide spaceship (until all parts are found)

        mainCam = Camera.main;
        mainCam.enabled = true;
        secondaryCam.enabled = false; // secondary cam is disabled until all spaceship parts are collected

        playerLight.GetComponent<Light>().intensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        cameraEffect();

        currentX += Input.GetAxis("Mouse X");
        currentY -= Input.GetAxis("Mouse Y");

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        if (toMoon)
        {
            // making spaceship and player fly to moon
            float step = floatSpeed * Time.deltaTime; // calculate distance to move
            spaceshipReady.transform.position = Vector3.MoveTowards(spaceshipReady.transform.position, moon.transform.position, step);
            player.transform.position = Vector3.MoveTowards(player.transform.position, moon.transform.position, step);

            // slight rotation during flight
            player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, Quaternion.Euler(0, 0, 20), 2 * Time.deltaTime);
            spaceshipReady.transform.rotation = Quaternion.RotateTowards(spaceshipReady.transform.rotation, Quaternion.Euler(0, 0, 20), 2 * Time.deltaTime);

            // tracking distance between spaceship and moon to control when to start fading to white
            float dist = Vector3.Distance(spaceshipReady.transform.position, moon.transform.position);
            if (dist < 200)
            {
                fadeWhite();
            }
        }
    }

    private void LateUpdate()
    {
        if (enableCameraControls)
        {
            {
                Vector3 dir = new Vector3(0, 0, -20 - distance);
                Quaternion rotation = Quaternion.Euler(-20 + currentY, currentX, currentZ);

                camTransform.position = lookAt.position + rotation * dir;
                camTransform.LookAt(lookAt.position);

                inventory();
            }
        }
    }
}
