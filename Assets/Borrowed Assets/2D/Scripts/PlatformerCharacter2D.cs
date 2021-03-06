using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        private AudioSource audioSource;

        public float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        public float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] public float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        public bool m_AirControl = false;// Whether or not a player can steer while jumping;
        public float m_AttackRange = 0.5f; //Range of attack
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_WolfAnim;            // Reference to the player's animator component.
        private Animator m_HumanAnim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        private WerewolfStateController wolfStateController;
        private List<Interactable> nearbyInteractables = new List<Interactable>();
        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_WolfAnim = transform.Find("Graphics").Find("Wolf").GetComponent<Animator>();
            m_HumanAnim = transform.Find("Graphics").Find("Human").GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            wolfStateController = GetComponent<WerewolfStateController>();
            audioSource = GetComponent<AudioSource>();
        }


        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            m_WolfAnim.SetBool("Ground", m_Grounded);
            m_HumanAnim.SetBool("Ground", m_Grounded);
            // Set the vertical animation
            m_WolfAnim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
            m_HumanAnim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }


        public void Move(float move, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch && m_WolfAnim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_WolfAnim.SetBool("Crouch", crouch);
            m_HumanAnim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move * m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_WolfAnim.SetFloat("Speed", Mathf.Abs(move));
                m_HumanAnim.SetFloat("Speed", Mathf.Abs(move));
                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump && m_WolfAnim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_WolfAnim.SetBool("Ground", false);
                m_HumanAnim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        public void Attack()
        {
            if (wolfStateController.wolfForm)
            {
                audioSource.Play();
                m_WolfAnim.SetTrigger("Attack");
               Collider2D[] hitTargets = Physics2D.OverlapCircleAll(transform.position, m_AttackRange);
                foreach (Collider2D target in hitTargets)
                {
                    Debug.Log("Got you? " + target.gameObject.name);
                    //Remove targets behind you
                    if (m_FacingRight)
                    {
                        if (target.transform.position.x < transform.position.x)
                            continue;
                    }
                    else
                    {
                        if (target.transform.position.x < transform.position.x)
                            continue;
                    }
                    //Kill targets
                    Debug.Log("Front of me " + target.gameObject.name);
                    if (target.gameObject.tag == "Enemy")
                    {
                        target.GetComponent<EnemyStateController>().Die();

                    }
                }
            }
        }

        public void Interact()
        {
            if (wolfStateController.wolfForm)
                Attack();
            else
            {
                foreach (Interactable interactable in nearbyInteractables)
                {
                    interactable.Interact();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Interactable interactable = collision.GetComponent<Interactable>();
            if (interactable != null)
            {
                nearbyInteractables.Add(interactable);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Interactable interactable = collision.GetComponent<Interactable>();
            if (interactable != null)
            {
                nearbyInteractables.Remove(interactable);
            }
        }

    }
}
