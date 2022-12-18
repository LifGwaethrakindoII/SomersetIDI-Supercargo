using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FAPInteraction : MonoBehaviour
{
    private const int INDEX_SCREEN_FIRST = 0;
    private const int INDEX_SCREEN_DOORS = 1;
    private const int INDEX_SCREEN_DOORS_CLOSED = 2;
    private const int INDEX_SCREEN_LIGHTS = 3;

    public Texture[] FAPTexture;
    public Renderer FAPMaterial;
    public Material FAPLights;

    //public Light lightsFAP;
    public Light[] lights;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Entra Pantalla");
            FAPMaterial.material.mainTexture = FAPTexture[0];
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Entra Pantalla Puertas");
            FAPMaterial.material.mainTexture = FAPTexture[1];
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Entra Pantalla Puertas Cerradas");
            FAPMaterial.material.mainTexture = FAPTexture[2];
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("Entra Pantalla Luces");
            FAPMaterial.material.mainTexture = FAPTexture[3];
            FAPLights.SetColor("_EmissionColor", Color.white);
            foreach(Light i in lights)
            {
                i.enabled = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Entra Pantalla Luces");
            FAPMaterial.material.mainTexture = FAPTexture[3];
            FAPLights.SetColor("_EmissionColor", Color.black);
            foreach(Light light in lights)
            {
                light.enabled = false;
            }
        }
    }

    public void ChangeScreen(int _screenIndex)
    {
        FAPMaterial.material.mainTexture = FAPTexture[_screenIndex];

        switch(_screenIndex)
        {
            case INDEX_SCREEN_LIGHTS:
                bool enabled = lights[0].enabled;
                FAPLights.SetColor("_EmissionColor", enabled ? Color.white : Color.black);
                foreach (Light light in lights)
                {
                    light.enabled = !enabled;
                }
            break;
        }
    }
}
