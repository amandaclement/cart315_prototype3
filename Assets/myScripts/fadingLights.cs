using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadingLights : MonoBehaviour
{

    //// accessing bool from cameraRotator script
    //public Main bS;
    //public bool collectedBody;
    //public bool collectedWingL;
    //public bool collectedWingR;
    //public bool collectedBoosterL;
    //public bool collectedBoosterR;
    //public bool collectedEngine;

    //public Light bodyLight;
    //public Light wingLLight;
    //public Light wingRLight;
    //public Light boosterLLight;
    //public Light boosterRLight;
    //public Light engineLight;

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    // BODY LIGHT
    //    if (bS.collectedBody == true) // once collected, set the light intensity to 0
    //    {
    //        bodyLight.GetComponent<Light>().intensity = 0;
    //    }
    //    else
    //    {
    //        bodyLight.GetComponent<Light>().intensity = Mathf.PingPong(Time.time * 0.1f, 0.13f); // fade in/out light until collected
    //    }

    //    // LEFT WING LIGHT
    //    if (bS.collectedWingL == true) // once collected, set the light intensity to 0
    //    {
    //        wingLLight.GetComponent<Light>().intensity = 0;
    //    }
    //    else
    //    {
    //        wingLLight.GetComponent<Light>().intensity = Mathf.PingPong(Time.time * 0.1f, 0.13f); // fade in/out light until collected
    //    }

    //    // RIGHT WING LIGHT
    //    //if (bS.collectedWingR == true) // once collected, set the light intensity to 0
    //    //{
    //    //    wingRLight.GetComponent<Light>().intensity = 0;
    //    //} else
    //    //{
    //    //    wingRLight.GetComponent<Light>().intensity = Mathf.PingPong(Time.time * 0.1f, 0.13f); // fade in/out light until collected
    //    //}

    //    // LEFT BOOSTER LIGHT
    //    if (bS.collectedBoosterL == true) // once collected, set the light intensity to 0
    //    {
    //        boosterLLight.GetComponent<Light>().intensity = 0;
    //    }
    //    else
    //    {
    //        boosterLLight.GetComponent<Light>().intensity = Mathf.PingPong(Time.time * 0.1f, 0.13f); // fade in/out light until collected
    //    }

    //    // RIGHT BOOSTER LIGHT
    //    if (bS.collectedBoosterR == true) // once collected, set the light intensity to 0
    //    {
    //        boosterRLight.GetComponent<Light>().intensity = 0;
    //    }
    //    else
    //    {
    //        boosterRLight.GetComponent<Light>().intensity = Mathf.PingPong(Time.time * 0.1f, 0.13f); // fade in/out light until collected
    //    }

    //    // ENGINE LIGHT
    //    if (bS.collectedEngine == true) // once collected, set the light intensity to 0
    //    {
    //        engineLight.GetComponent<Light>().intensity = 0;
    //    }
    //    else
    //    {
    //        engineLight.GetComponent<Light>().intensity = Mathf.PingPong(Time.time * 0.1f, 0.13f); // fade in/out light until collected
    //    }
    //}
}
