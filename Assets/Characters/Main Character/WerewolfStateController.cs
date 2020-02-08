using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets;
using UnityStandardAssets._2D;
using UnityEngine.UI;
using UnityEditor.Animations;

public class WerewolfStateController : MonoBehaviour
{
    public bool wolfForm = false;

    [Header("Human stats")]
    [SerializeField]
    private float humanSpeed;
    [SerializeField]
    private float humanJump;
    [SerializeField]
    private float humanStrength;
    [SerializeField]
    private AnimatorController humanAnimator;

    [Header("Wolf stats")]
    [SerializeField]
    private float wolfSpeed;
    [SerializeField]
    private float wolfJump;
    [SerializeField]
    private float wolfStrength;
    [SerializeField]
    private AnimatorController wolfAnimator;

    private PlatformerCharacter2D playerController;
    private Animator anim;
    private bool touchingShadow = false;
    private bool touchingLight = false;

    void Start()
    {
        playerController = GetComponent<PlatformerCharacter2D>();
        anim = transform.Find("Graphics").GetComponent<Animator>();


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
            if (touchingShadow)
                TransformToHuman();
        }
        else if (collider.gameObject.tag == "Shadow")
        {
            touchingShadow = false;
            TransformToWolf();
        }
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
}
