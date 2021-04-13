using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hint : MonoBehaviour
{
    // ACCESSING COMPONENT SCRIPTS (to access booleans that track whether component has been collected)
    public Body bodyScript;
    public Engine engineScript;
    public WingL wingLScript;
    public WingR wingRScript;
    public BoosterL boosterLScript;
    public BoosterR boosterRScript;

    // FOR HINT INSTRUCTIONS/CONTROLS TEXT
    bool getHint = false;
    bool showHintInstructions = false;
    public Text hintControls;
    public Text hintInstructions;
    private float alphaAmt = 0f;
    private float alphaAmt2 = 0f;
    bool hintAvailable = false;
    bool fadeInInstructions = false;
    bool fadeInControls = false;

    // HINT LIGHTS
    public Light bodyHintLight;
    public Light wingLHintLight;
    public Light wingRHintLight;
    public Light boosterLHintLight;
    public Light boosterRHintLight;
    public Light engineHintLight;

    // TIMER
    private float timeRemaining = 100;

    // reference: https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html?_ga=2.125981838.2057613814.1612463807-1308570294.1603245419
    // detecting which non-collected component is closest to player (collected components not accounted for since they are untagged)
    public GameObject FindClosestComponent()
    {
        GameObject[] components;
        components = GameObject.FindGameObjectsWithTag("Component");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject component in components)
        {
            Vector3 diff = component.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = component;
                distance = curDistance;
            }
        }
        return closest;
    }

    // START
    void Start()
    {
        // for fading
        var tempColor = hintInstructions.color;
        tempColor.a = alphaAmt;
        hintInstructions.color = tempColor;

        var tempColor2 = hintControls.color;
        tempColor2.a = alphaAmt2;
        hintControls.color = tempColor2;
    }

    void triggerHintInstructions()
    {
        showHintInstructions = true;
        alphaAmt += 0.3f * Time.deltaTime;
        hintControls.enabled = false; // hide hint controls       
    }

    void hideHintInstructions()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            hintInstructions.enabled = false; // hide controls
            getHint = true;
        }
    }

    // UPDATE
    void Update()
    {
        var target = FindClosestComponent();
        var body = GameObject.Find("body");
        var wingL = GameObject.Find("wingL");
        var wingR = GameObject.Find("wingR");
        var boosterL = GameObject.Find("boosterL");
        var boosterR = GameObject.Find("boosterR");
        var engine = GameObject.Find("engine");

        if (hintAvailable)
        {
            fadeInControls = true;
        }

        if (Input.GetKey(KeyCode.Q) && hintAvailable)
        {
            if (!showHintInstructions)
            {
                triggerHintInstructions();
                fadeInInstructions = true;
                hintControls.enabled = false;
            }
        }

        if (fadeInInstructions)
        {
            alphaAmt += 0.2f * Time.deltaTime; // fade in hint instructions
            Invoke("hideHintInstructions", 0.5f);

        }
            var tempColor = hintInstructions.color;
            tempColor.a = alphaAmt;
            hintInstructions.color = tempColor;

        if (fadeInControls)
        {
            alphaAmt2 += 0.2f * Time.deltaTime; // fade in hint instructions
        }

        var tempColor2 = hintControls.color;
        tempColor2.a = alphaAmt2;
        hintControls.color = tempColor2;

        // WHEN PLAYER REQUESTS HINT
        if (getHint)
        {
        // BODY
        if (target == body)
        {
            bodyHintLight.intensity = Mathf.PingPong(Time.time * 0.15f, 0.4f);
        }
        else
        {
            bodyHintLight.intensity = 0;
        }

        // WING R
        if (target == wingR)
        {   
            wingRHintLight.intensity = Mathf.PingPong(Time.time * 0.15f, 0.4f);
        }
        else
        {
            wingRHintLight.intensity = 0;
        }

        // WING L
        if (target == wingL)
        {
            wingLHintLight.intensity = Mathf.PingPong(Time.time * 0.15f, 0.4f);
        }
        else
        {
            wingLHintLight.intensity = 0;
        }

        // BOOSTER R
        if (target == boosterR)
        {
            boosterRHintLight.intensity = Mathf.PingPong(Time.time * 0.15f, 0.4f);
        }
        else
        {
            boosterRHintLight.intensity = 0;
        }

        // BOOSTER L
        if (target == boosterL)
        {
            boosterLHintLight.intensity = Mathf.PingPong(Time.time * 0.15f, 0.4f);
        }
        else
        {
            boosterLHintLight.intensity = 0;
        }

        // ENGINE
        if (target == engine)
        {
            engineHintLight.intensity = Mathf.PingPong(Time.time * 0.15f, 0.4f);
        }
        else
        {
            engineHintLight.intensity = 0;
        }
        } else
        {
            bodyHintLight.intensity = 0;
            wingLHintLight.intensity = 0;
            wingRHintLight.intensity = 0;
            boosterLHintLight.intensity = 0;
            boosterRHintLight.intensity = 0;
            engineHintLight.intensity = 0;
        }

        // FOR TIMER
        if (bodyScript.colliding || engineScript.colliding || wingRScript.colliding || wingLScript.colliding || boosterRScript.colliding || boosterLScript.colliding)
        {
            timeRemaining = 100; // when a component is collected, reset the timer (occurs during collision)
        }
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            // if no components are found within 100 seconds, give player option of triggering hint
            hintAvailable = true;
        }
        Debug.Log(timeRemaining);
    }
}
