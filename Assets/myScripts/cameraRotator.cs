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

    public float posX;
    public float posY;
    public float posZ;

    public float posCX;
    public float posCY;
    public float posCZ;

    public GameObject player;
    public float trackingSpeed;
    //public Camera rotationCamera;
    public Camera rotationCamera;

    public float speed;

    public Text controls;
    private float alphaAmt = 0f;

    public Transform lookAt;
    public Transform camTransform;
    public Camera mainCam;
    public float distance = 5.0f;

    public float currentX = 0.0f;
    public float currentY = 0.0f;
    public float currentZ = 0.0f;
    private float sensitivityX = 20.0f;
    private float sensitivityY = 20.0f;

    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 50.0f;


    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

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
        //mainCam.transform.parent = player.transform; // camera now child of player
        //Invoke("cameraControls", 0); // player can now control camera angle
        //Invoke("cameraControls", 0); // player can now control camera angle
        Invoke("fadeInText", 0); // control instructions appear
        Invoke("cameraControls", 0); 

        enableCameraControls = true;
    }

    // arrow keys to control camera angle
    void cameraControls()
    {
        //yaw += speedH * Input.GetAxis("Mouse X");
        //pitch -= speedV * Input.GetAxis("Mouse Y");

        //mainCam.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    rotationCamera.transform.Rotate(Vector3.up, 20f * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    rotationCamera.transform.Rotate(Vector3.down, 20f * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    rotationCamera.transform.Rotate(Vector3.right, 20f * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    rotationCamera.transform.Rotate(Vector3.left, 20f * Time.deltaTime);
        //}
    }

    // function to control fading in/out control instructions
    void fadeInText()
    {
        if (alphaAmt < 1f)
        {
            alphaAmt += 0.8f * Time.deltaTime; ;
        }

        if (Input.GetKey(KeyCode.E)) // E to close control instructions
        {
            Destroy(controls);
        }
        var tempColor = controls.color;
        tempColor.a = alphaAmt;
        controls.color = tempColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        var tempColor = controls.color;
        tempColor.a = alphaAmt;
        controls.color = tempColor;

        // camTransform = transform;
        //rotationCamera = transform;

        //enableCameraControls = true;

        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        cameraEffect();

        currentX += Input.GetAxis("Mouse X");
        currentY -= Input.GetAxis("Mouse Y");

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        //if (enableCameraControls)
        //{

        //    yaw += speedH * Input.GetAxis("Mouse X");
        //    pitch -= speedV * Input.GetAxis("Mouse Y");

        //    mainCam.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        //}


    }

    private void LateUpdate()
    {
        if (enableCameraControls)
        {
            //yaw += speedH * Input.GetAxis("Mouse X");
            //pitch -= speedV * Input.GetAxis("Mouse Y");

            //transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

            {
               // transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, z), Time.deltaTime * transitionSpeed);

                Vector3 dir = new Vector3(posX, posY, -20 - distance);
                Quaternion rotation = Quaternion.Euler(-20 + currentY, currentX, currentZ);
                
                camTransform.position = lookAt.position + rotation * dir;
                camTransform.LookAt(lookAt.position);

                //mainCam.transform.position = new Vector3(posCX, posCY, posCZ);
            }
        }
    }
}
