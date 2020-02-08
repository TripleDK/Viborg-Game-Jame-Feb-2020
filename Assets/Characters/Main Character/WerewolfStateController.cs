using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets;
using UnityStandardAssets._2D;
using UnityEngine.UI;
using UnityEditor.Animations;
using UnityEngine.SceneManagement;

public class WerewolfStateController : MonoBehaviour
{
    public bool wolfForm = false;
    [SerializeField]
    float health = 100;

    [Header("Human stats")]
    [SerializeField]
    private float humanSpeed;
    [SerializeField]
    private float humanJump;
    [SerializeField]
    private float humanMass;
    [SerializeField]
    private AnimatorController humanAnimator;

    [Header("Wolf stats")]
    [SerializeField]
    private float wolfSpeed;
    [SerializeField]
    private float wolfJump;
    [SerializeField]
    private float wolfMass;
    [SerializeField]
    private AnimatorController wolfAnimator;

    private PlatformerCharacter2D playerController;
    private Animator anim;
    private bool touchingShadow = false;
    private bool touchingLight = false;
    private bool moonlight = false;

    void Start()
    {
        playerController = GetComponent<PlatformerCharacter2D>();
        anim = transform.Find("Graphics").Find("Wolf").GetComponent<Animator>();


        if (wolfForm) TransformToWolf();
        else TransformToHuman();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Light")
        {
            touchingLight = true;
            TransformToWolf();
        }
        else if (collider.gameObject.tag == "Shadow")
        {
            touchingShadow = true;
            if (!touchingLight)
                TransformToHuman();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Light")
        {
            touchingLight = false;
            CheckTransformation();
        }
        else if (collider.gameObject.tag == "Shadow")
        {
            touchingShadow = false;
            CheckTransformation();
        }
    }

    void CheckTransformation()
    {
        if (touchingShadow && !touchingLight)
            TransformToHuman();
        else
        {
            if (!touchingShadow && moonlight)
                TransformToWolf();
        }
    }
    public void ChangeMoonLight(bool moonLightOn)
    {
        moonlight = moonLightOn;
        CheckTransformation();
    }

    void TransformToWolf()
    {
        wolfForm = true;
        playerController.m_MaxSpeed = wolfSpeed;
        playerController.m_JumpForce = wolfJump;
        anim.runtimeAnimatorController = wolfAnimator;
    }


    void TransformToHuman()
    {
        wolfForm = false;
        playerController.m_MaxSpeed = humanSpeed;
        playerController.m_JumpForce = humanJump;
        anim.runtimeAnimatorController = humanAnimator;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
