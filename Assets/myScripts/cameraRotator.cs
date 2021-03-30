using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// reference for camera rotation effect: https://www.youtube.com/watch?v=iuygipAigew

public class cameraRotator : MonoBehaviour
{
    public float transitionSpeed;
    // public float rotationSpeed;
    // float rotationFull = 355; // offset

    public float translationSpeed;
    float translationFull = 750;

    // camera zoom in effect coordinates
    public float x;
    public float y;
    public float z;

    public GameObject player;
    public float trackingSpeed;

    public float speed;

    public Text controls;
    public Text controls2;
    private float alphaAmt = 0f;

    public Transform lookAt;
    public Transform camTransform;
    public Camera mainCam;
    public float distance = 5.0f;

    public float currentX = 0.0f;
    public float currentY = 0.0f;
    public float currentZ = 0.0f;

    private const float Y_ANGLE_MIN = 25.0f;
    private const float Y_ANGLE_MAX = 50.0f;


    public float speedH = 2.0f;
    public float speedV = 2.0f;

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

    public GameObject collectableBody;
    bool collectedBody = false;
    private float minDistance = 6;

    bool enableCameraControls = false;

    void cameraEffect()
    {
        // float rotation = rotationSpeed * Time.deltaTime;

        float translation = translationSpeed * Time.deltaTime;

        //if (rotationFull > rotation)
        //{
        //    rotationFull -= rotation;
        //}

        if (translationFull > translation)
        {
            translationFull -= translation;
        }

        else
        {
            translation = translationFull;
            translationFull = 0;
            transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, z), Time.deltaTime * transitionSpeed);

            Invoke("cameraSwitch", 1f);
        }
        transform.Translate(0, 0, -translation);
    }

    void cameraSwitch()
    {
        Invoke("fadeInText", 0); // control instructions appear
        Invoke("cameraControls", 0); 

        enableCameraControls = true;
    }

    // function to control fading in/out control instructions
    void fadeInText()
    {
        alphaAmt += 0.8f * Time.deltaTime; 

        if (Input.GetKey(KeyCode.E)) {
            Destroy(controls);
        }

        var tempColor = controls.color;
        tempColor.a = alphaAmt;
        controls.color = tempColor;
    }

    void inventory()
    {
        // positioning parts based on viewport
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

        // for now, keep light intensities at 0 since the player has not collected anything
        //bodyLightTop.intensity = 0;
        //bodyLightBottom.intensity = 0;
        wingLLightTop.intensity = 0;
        wingLLightBottom.intensity = 0;
        wingRLightTop.intensity = 0;
        wingRLightBottom.intensity = 0;
        boosterLLightTop.intensity = 0;
        boosterLLightBottom.intensity = 0;
        boosterRLightTop.intensity = 0;
        boosterRLightBottom.intensity = 0;
        engineLightTop.intensity = 0;
        engineLightBottom.intensity = 0;

        // distance between player and collectable
        float dist = Vector3.Distance(player.transform.position, collectableBody.transform.position);
        if (dist < minDistance)
        {
            // if within range, player has found the part so we can destroy it and light it up
           // Destroy(collectableBody);
            collectableBody.SetActive(false); // hide part once found
            collectedBody = true;
        } 

        if (collectedBody)
        {
            if (bodyLightTop.intensity < 1)
            {
                bodyLightTop.intensity += 0.8f * Time.deltaTime;
            }
            // bodyLightTop.intensity = 1;
            bodyLightBottom.intensity = 1;
        } else
        {
            bodyLightTop.intensity = 0;
            bodyLightBottom.intensity = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        var tempColor = controls.color;
        tempColor.a = alphaAmt;
        controls.color = tempColor;

        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        cameraEffect();

        currentX += Input.GetAxis("Mouse X");
        currentY -= Input.GetAxis("Mouse Y");

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
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
