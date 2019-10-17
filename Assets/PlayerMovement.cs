using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;

	public float runSpeed = 40f;
    public string jumpButton = "Jump_P1";
    public string crouchButton = "Crouch_P1";
    public string horizontalCtrl = "Horizontal_P1";

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
	}
}
