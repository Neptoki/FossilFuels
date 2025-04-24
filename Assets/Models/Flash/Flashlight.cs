using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light light;
    public bool isOn = false;

    void update()
    {
        GetComponent<Light>().enabled = isOn;
    }

    public void ToggleLight()
    {
        isOn = !isOn;
    }

}
