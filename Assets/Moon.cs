using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Moon : MonoBehaviour
{
    WerewolfStateController controller;
    
    //public Light2D moonLightSource;
    public Light2D moonLightZone;

    public Color hiddenColor;
    public Color brightColor;

    public float fadeTime = 0.5f;

    bool fadingOut = false;
    bool fadingIn = false;

    float timer = 0f;

    int cloudCounter = 0;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<WerewolfStateController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("fade out");
        if (other.tag == "Cloud")
        {
            cloudCounter++;
            if (cloudCounter >= 0)
            {
                FadeOutMoonShine();

                // SET NEW WOLF STATE
                controller.ChangeMoonLight(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        Debug.Log("fade in");
        if (other.tag == "Cloud")
        {
            cloudCounter--;
            if (cloudCounter <= 0)
            {
                FadeInMoonShine();

                // SET NEW WOLF STATE
                controller.ChangeMoonLight(true);
            }
        }

    }

    private void FadeOutMoonShine()
    {
        
        fadingIn = false;
        fadingOut = true;
        timer = 0f;
    }

    private void FadeInMoonShine()
    {
        
        fadingOut = false;
        fadingIn = true;
        timer = 0f;
    }

    private void Update()
    {
       

        if (fadingOut)
        {
            
            //moonLightSource.color = Color.Lerp(moonLightSource.color, hiddenColor, timer);
            moonLightZone.intensity = Mathf.Lerp(1, 0, timer);
            if (timer < 1)
            {
                timer += Time.deltaTime / fadeTime;
            }

        }
        if (fadingIn)
        {
            
            //moonLightSource.color = Color.Lerp(moonLightSource.color, brightColor, timer);
            moonLightZone.intensity = Mathf.Lerp(0, 1, timer);

            if (timer < 1)
            {
                timer += Time.deltaTime / fadeTime;
            }

        }
    }
}
