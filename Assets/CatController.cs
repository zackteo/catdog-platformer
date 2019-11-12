using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;
    [SerializeField] private float runSpeed = 40f;
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching
    private bool hasDoubleJumped = false;
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
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

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ground"))
        {
            hasDoubleJumped = false;
        }

    }


    public void Update()
    {
        //only control the player if grounded or airControl is turned on
        if (m_Grounded && Input.GetKeyDown("left"))
        {

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2((-runSpeed * Time.fixedDeltaTime) * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);


            m_FacingRight = false;
        }
        if (m_Grounded && Input.GetKeyDown("right"))
        {

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2((runSpeed * Time.fixedDeltaTime) * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...

        }
        if (m_Grounded && Input.GetKey("right"))
        {
            if (!m_FacingRight)
            {
                Flip();
            }

            m_FacingRight = true;
        }
        if (m_Grounded && Input.GetKey("left"))
        {
            if (m_FacingRight)
            {
                Flip();
            }
        }

        if (m_Grounded == true && Input.GetKey("up"))
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            hasDoubleJumped = false;
        }

        if (m_Grounded == false && hasDoubleJumped == false && Input.GetKey("up"))
        {
            // Add a vertical force to the player.
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            hasDoubleJumped = true;
        }
        if (m_Grounded == false && Input.GetKeyDown("left"))
        {

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2((-runSpeed * Time.fixedDeltaTime) * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
        }
        if (m_Grounded == false && Input.GetKeyDown("right"))
        {

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2((runSpeed * Time.fixedDeltaTime) * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
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
        m_Rigidbody2D.velocity = new Vector2(0, 0);
    }
}