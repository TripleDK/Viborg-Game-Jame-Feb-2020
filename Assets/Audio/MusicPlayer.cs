using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    public float tTimeWerewolf;
    public float tTimeHuman;

    public static bool musicIsPlaying;

    public WerewolfStateController mainCharacter;
    private bool wolfForm;
    private bool fakeWolfForm;
    private bool fakeWolfFormHasChanged;

    public AudioMixerSnapshot werewolf;
    public AudioMixerSnapshot human;

    public AudioClip transitionToHuman;
    public AudioClip transitionToWolf;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (musicIsPlaying == false)
        {
            GameObject.Find("Layer1").GetComponent<AudioSource>().Play(); // plays always
            GameObject.Find("Layer2").GetComponent<AudioSource>().Play(); // plays when wolf
            GameObject.Find("Layer3").GetComponent<AudioSource>().Play(); // plays when human
            DontDestroyOnLoad(transform.gameObject);
            musicIsPlaying = true;
        }

        wolfForm = mainCharacter.wolfForm;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            fakeWolfForm = false;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            fakeWolfForm = true;
        }

        //Debug.Log(wolfForm);
        //switch(wolfForm)
        //{
        //    case false:
        //        human.TransitionTo(1f);
        //        break;
        //    case true:
        //        werewolf.TransitionTo(1f);
        //        break;
        //}

        switch (fakeWolfForm)
        {
            case false:
                if (fakeWolfFormHasChanged == false)
                {
                    audioSource.clip = transitionToHuman;
                    audioSource.Play();
                    human.TransitionTo(tTimeHuman);
                    fakeWolfFormHasChanged = true;
                }
                break;
            case true:
                if (fakeWolfFormHasChanged == true)
                {
                    audioSource.clip = transitionToWolf;
                    audioSource.Play();
                    werewolf.TransitionTo(tTimeWerewolf);
                    fakeWolfFormHasChanged = false;
                }
                break;
        }
    }
}
