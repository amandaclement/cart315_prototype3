using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class floatOff : MonoBehaviour
{

    public GameObject moon;
    public float minDistance;

    public Transform target;
    private float speed = 8f;

    private float alphaAmt = 0f;

    public Image image;

    // when in range, player floats to moon
    void floatToMoon()
    {
        float dist = Vector3.Distance(moon.transform.position, this.transform.position);

        if (dist < minDistance)
        {
            Destroy(GetComponent<Player>()); // destroy player script to disable controls (caused shaking issue)

            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            Invoke("fadeOut", 5f);
        }
    }

    void fadeOut()
    {
        if (alphaAmt < 1f)
        {
            alphaAmt += 1f * Time.deltaTime; ;
        }
        var tempColor = image.color;
        tempColor.a = alphaAmt;
        image.color = tempColor;

    }

    // Start is called before the first frame update
    void Start()
    {
        target = moon.transform;
        var tempColor = image.color;
        tempColor.a = alphaAmt;
        image.color = tempColor;
    }

    // Update is called once per frame
    void Update()
    {
        floatToMoon();
    }
}
