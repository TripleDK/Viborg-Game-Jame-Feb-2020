using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public float tTimeWerewolf;
    public float tTimeHuman;

    public static bool musicIsPlaying;

    public WerewolfStateController mainCharacter;
    private bool wolfForm;
    private bool wolfFormLastFrame;
    private float timeForIntroduction = 0f;

    public AudioMixerSnapshot werewolf;
    public AudioMixerSnapshot human;
    public AudioMixerSnapshot SfxStarting;
    public AudioMixerSnapshot SfxRunning;

    public AudioClip transitionToHuman;
    public AudioClip transitionToWolf;
    private AudioSource audioSource;

    private void OnSceneLoaded(Scene scene, LoadSceneMode load)
    {
        mainCharacter = GameObject.FindWithTag("Player").GetComponent<WerewolfStateController>();
    }

    private void Awake()
    {
        mainCharacter = GameObject.FindWithTag("Player").GetComponent<WerewolfStateController>();

        SceneManager.sceneLoaded += OnSceneLoaded;
        if (musicIsPlaying == true)
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();

        if (musicIsPlaying == false)
        {
            GameObject.Find("Layer1").GetComponent<AudioSource>().Play(); // plays always
            GameObject.Find("Layer2").GetComponent<AudioSource>().Play(); // plays when wolf
            GameObject.Find("Layer3").GetComponent<AudioSource>().Play(); // plays when human
            DontDestroyOnLoad(transform.gameObject);
            musicIsPlaying = true;
        }
    }

    private void Start()
    {
        SfxRunning.TransitionTo(1f);
    }

    private void Update()
    {
        wolfForm = mainCharacter.wolfForm;

        if (Time.time > timeForIntroduction)
        {
            if (wolfForm != wolfFormLastFrame)
            {
                switch (wolfForm)
                {
                    case false:
                        TransitionToHuman();
                        break;
                    case true:
                        TransitionToWerewolf();
                        break;
                }
                wolfFormLastFrame = wolfForm;
            }
        }
    }

    private void TransitionToHuman()
    {
        audioSource.clip = transitionToHuman;
        audioSource.Play();
        human.TransitionTo(tTimeHuman);
    }

    private void TransitionToWerewolf()
    {
        audioSource.clip = transitionToWolf;
        audioSource.Play();
        werewolf.TransitionTo(tTimeWerewolf);
    }
}
