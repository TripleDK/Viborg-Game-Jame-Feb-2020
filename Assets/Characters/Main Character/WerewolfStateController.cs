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
    private GameObject humanGraphics;

    [Header("Wolf stats")]
    [SerializeField]
    private float wolfSpeed;
    [SerializeField]
    private float wolfJump;
    [SerializeField]
    private float wolfMass;
    [SerializeField]
    private GameObject wolfGraphics;

    private PlatformerCharacter2D playerController;
    private bool touchingShadow = false;
    private bool touchingLight = false;
    private bool moonlight = false;

    private void Awake()
    {

        playerController = GetComponent<PlatformerCharacter2D>();
    }
    void Start()
    {
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
        Debug.Log("exited " + collider.gameObject.name);
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
        Debug.Log("Touching shadow?" + touchingShadow + " touching light? " + touchingLight + " moonlight? " + moonlight);
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
        Debug.Log("I am wolf");
        wolfForm = true;
        playerController.m_MaxSpeed = wolfSpeed;
        playerController.m_JumpForce = wolfJump;
        wolfGraphics.SetActive(true);
        humanGraphics.SetActive(false);
    }


    void TransformToHuman()
    {
        Debug.Log("I am man");
        wolfForm = false;
        playerController.m_MaxSpeed = humanSpeed;
        playerController.m_JumpForce = humanJump;
        wolfGraphics.SetActive(false);
        humanGraphics.SetActive(true);

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
