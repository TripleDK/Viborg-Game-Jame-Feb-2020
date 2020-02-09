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
        [Range(0f, 1f)]
        public float volume;
    }

    public Sound[] italianGuy;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        stateController = transform.parent.GetComponent<EnemyStateController>();
        detectCollider = GetComponent<Collider2D>();
        enemyCollider = transform.parent.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(detectCollider, enemyCollider, true);
    }

    public void ItalianSpeaks()
    {
        selectedFile = Random.Range(0, italianGuy.Length);
        audioSource.clip = italianGuy[selectedFile].soundFile;
        audioSource.pitch = 1.2f;
        audioSource.volume = italianGuy[selectedFile].volume;
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
            RaycastHit2D[] detectChecks = Physics2D.RaycastAll(enemyCollider.transform.position, (Vector2)collider.transform.position - (Vector2)enemyCollider.transform.position, detectRange);

            bool canSeePlayer = false;

            foreach (RaycastHit2D check in detectChecks)
            {
                if (check.collider != null && check.collider.gameObject.tag == "Player")
                {
                    canSeePlayer = true;
                }


            }
            if (canSeePlayer)
            {
                WerewolfStateController wolfController = collider.gameObject.GetComponent<WerewolfStateController>();
                if (wolfController.wolfForm)
                {

                    stateController.detectedPlayer = collider.gameObject;

                    stateController.GoToAttackState();
                }
                if (seeingEnemy == false)
                    SpottedEnemy();
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            seeingEnemy = false;
        }
    }
}
