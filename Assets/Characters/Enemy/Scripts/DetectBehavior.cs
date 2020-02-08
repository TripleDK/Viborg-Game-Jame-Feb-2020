using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectBehavior : MonoBehaviour
{
    [SerializeField]
    float detectRange;

    private Collider2D detectCollider;
    private Collider2D enemyCollider;
    private EnemyStateController stateController;
    private bool seeingEnemy = false;

    AudioSource audioSource;
    private int selectedFile;

    [System.Serializable]
    public class Sound
    {
        public AudioClip soundFile;
        public float volume;
    }
    public Sound[] italianGuy;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        stateController = transform.parent.GetComponent<EnemyStateController>();
        detectCollider = GetComponent<Collider2D>();
        enemyCollider = transform.parent.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(detectCollider, enemyCollider);
    }

    public void ItalianSpeaks()
    {
        TriggerSoundEvent(italianGuy);
        Debug.Log("hey tony");
    }

    private void TriggerSoundEvent(Sound[] sound)
    {
        selectedFile = Random.Range(0, sound.Length);
        audioSource.clip = sound[selectedFile].soundFile;
        audioSource.Play();
    }

    void SpottedEnemy()
    {
        if (seeingEnemy == false)
        {
            seeingEnemy = true;
            ItalianSpeaks();
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            RaycastHit2D detectCheck = Physics2D.Raycast(transform.position, collider.transform.position - transform.position, detectRange);
            Debug.Log("Detection hits " + collider.gameObject.name);
            if (collider.gameObject.tag == "Player")
            {
                
                WerewolfStateController wolfController = collider.gameObject.GetComponent<WerewolfStateController>();
                if (wolfController.wolfForm)
                {

                    stateController.detectedPlayer = collider.gameObject;

                    stateController.GoToAttackState();

                    SpottedEnemy();
                }
                else
                {
                    SpottedEnemy();
                }
            }
            else
            {
                seeingEnemy = false;
            }
        }
        else
        {
            seeingEnemy = false;
        }

    }

}
