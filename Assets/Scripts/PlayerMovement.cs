using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;

	public float runSpeed = 40f;
    public string jumpButton = "Jump_P1";
    public string crouchButton = "Crouch_P1";
    public string horizontalCtrl = "Horizontal_P1";
    public float distance;
    public LayerMask whatIsLadder;
    private bool climbing;
    private float inputVertical;
    private float inputHorizontal;
    private Rigidbody2D rb;
    

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
	
	// Update is called once per frame
	void Update () {

		horizontalMove = Input.GetAxisRaw(horizontalCtrl) * runSpeed;

		if (Input.GetButtonDown(jumpButton))
		{
			jump = true;
		}

		if (Input.GetButtonDown(crouchButton))
		{
			crouch = true;
		} else if (Input.GetButtonUp(crouchButton))
		{
			crouch = false;
		}

	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;

        // Climb Ladders
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputHorizontal * runSpeed, rb.velocity.y);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);

        if(hitInfo.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                climbing = true;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.upArrow)) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    climbing = false;
                }
            }

            if(climbing == true && hitInfo.collider !=null)
            {
                inputVertical = Input.GetAxisRaw("Vertical");
                rb.velocity = new Vector2(rb.position.x, inputVertical * runSpeed);
                rb.gravityScale = 0;
            }
            else
            {
                rb.gravityScale = 5;
            }
        }
	}
}
