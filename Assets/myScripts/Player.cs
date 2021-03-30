using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

		private Animator anim;
		private CharacterController controller;

		public float speed;
		public float turnSpeed;
		public float gravity;

	    [SerializeField]
	    private float jumpSpeed = 3.5f;
	    private float directionY;

	    public float RotateSpeed = 3.0f;

	void Start () {
	    controller = GetComponent <CharacterController>();
	    anim = gameObject.GetComponentInChildren<Animator>(); // for the character animations
	}

	private void FixedUpdate()
	{
		// for smooth character rotation when player changes directions
		float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(movement), Time.time * 1);
		//

		// setting the character animations based on movement
		if (Input.GetButton("Jump"))
		{
			anim.SetInteger("AnimationPar", 1);
		}

		if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d") || Input.GetKey("left") || Input.GetKey("right") || Input.GetKey("up") || Input.GetKey("down"))
        {
            anim.SetInteger("AnimationPar", 1);
        }
        else
        {
            anim.SetInteger("AnimationPar", 0);
        }
		//

		// handling movement
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");

		Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

		// shift for speed increase
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{ 
			speed = 25f;
		}
		else
		{
			speed = 13f;
		}

		directionY -= gravity * Time.deltaTime * 0.5f; // 0.5f to slow down jump speed
		direction.y = directionY;
		controller.Move(direction * speed * Time.deltaTime);
		if (controller.isGrounded)
		{
			if (Input.GetButton("Jump"))
			{
				directionY = jumpSpeed;
			}
		}
		//
	}
}
