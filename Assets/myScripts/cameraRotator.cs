using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// reference for camera rotation effect: https://www.youtube.com/watch?v=iuygipAigew

public class cameraRotator : MonoBehaviour
{
    public float transitionSpeed;
    public float rotationSpeed;
    float rotationFull = 355; // offset

    public float translationSpeed;
    float translationFull = 750;

    // camera zoom in effect coordinates
    public float x;
    public float y;
    public float z;

    public GameObject player;
    public float trackingSpeed;
    public Camera rotationCamera;

    public float speed;

    public Text controls;
    private float alphaAmt = 0f;

    void cameraEffect()
    {
        float rotation = rotationSpeed * Time.deltaTime;

        float translation = translationSpeed * Time.deltaTime;

        if (rotationFull > rotation)
        {
            rotationFull -= rotation;
        }

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
        rotationCamera.transform.parent = player.transform; // camera now child of player
        Invoke("cameraControls", 0); // player can now control camera angle
        Invoke("fadeInText", 0); // control instructions appear
    }

    // arrow keys to control camera angle
    void cameraControls()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotationCamera.transform.Rotate(Vector3.up, 20f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotationCamera.transform.Rotate(Vector3.down, 20f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rotationCamera.transform.Rotate(Vector3.right, 20f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rotationCamera.transform.Rotate(Vector3.left, 20f * Time.deltaTime);
        }
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
    }

    // Update is called once per frame
    void Update()
    {
        cameraEffect();
    }
}
