using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

		private Animator anim;
		private CharacterController controller;

		public float speed = 600.0f;
		public float turnSpeed = 400.0f;
		private Vector3 moveDirection = Vector3.zero;
		public float gravity = 20.0f;

	    [SerializeField]
	    private float jumpSpeed = 3.5f;
	    private float directionY;

	void Start () {
	    controller = GetComponent <CharacterController>();
	    anim = gameObject.GetComponentInChildren<Animator>();
		
	}

		void Update (){
			if (Input.GetKey ("w")) {
				anim.SetInteger ("AnimationPar", 1);
			}  else {
				anim.SetInteger ("AnimationPar", 0);
			}

			if(controller.isGrounded){
				moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
			}

			float turn = Input.GetAxis("Horizontal");
			transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
			controller.Move(moveDirection * Time.deltaTime);
			moveDirection.y -= gravity * Time.deltaTime;
		}

    private void FixedUpdate()
    {
		if (Input.GetButton("Jump"))
		{
			anim.SetInteger("AnimationPar", 1);
		}

		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");

		Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

		directionY -= gravity * Time.deltaTime;
		direction.y = directionY;
		controller.Move(direction * speed * Time.deltaTime);
		if (controller.isGrounded)
		{
			if (Input.GetButton("Jump"))
			{
				directionY = jumpSpeed;
			}
		}
	}
}
