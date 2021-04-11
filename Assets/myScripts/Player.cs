using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	// accessing bool from cameraRotator script
	public Main bS;
	public bool enteredShip;
	public bool collectedWingR;
	public bool collectedWingL;
	public bool collectedBoosterR;
	public bool collectedBoosterL;

	private Animator anim;
	private CharacterController controller;

    public float speed;
	public float mainSpeed = 0.0f;
	public float increasedSpeed = 0.0f;
	public float turnSpeed;
	public float gravity;

	[SerializeField]
	private float jumpSpeed = 3.5f;
	private float directionY;

	public float RotateSpeed = 3.0f;

	public float currentX = 0.0f;
	public float currentY = 0.0f;
	public float currentZ = 0.0f;

	private const float Y_ANGLE_MIN = 25.0f;
	private const float Y_ANGLE_MAX = 50.0f;

	public float turnSmoothTime = 0.1f;
	float turnSmoothVelocity;

	public Transform mainCam;

	void Start () {
	    controller = GetComponent <CharacterController>();
	    anim = gameObject.GetComponentInChildren<Animator>(); // for the character animations
	}

	private void FixedUpdate()
	{
		if (bS.enteredShip == true)
		{
			Destroy(GetComponent<Player>()); // disable player movement
		}

		// setting the character animations based on movement
		if (Input.GetButton("Jump"))
		{
			anim.SetInteger("AnimationPar", 0);
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
			speed = increasedSpeed;

		}
		else
		{
			speed = mainSpeed;
		}

		// tutorial reference for making player movement adjust to camera angle: https://www.youtube.com/watch?v=ORD7gsuLivE
		// tutorial reference for making player rotate accordingly: https://www.youtube.com/watch?v=4HpC--2iowE
		Vector3 camF = mainCam.forward;
		Vector3 camR = mainCam.right;

		camF.y = 0;
		camR.y = 0;
		camF = camF.normalized;
		camR = camR.normalized;

		if (direction.magnitude >= 0.1f)
		{
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			transform.rotation = Quaternion.Euler(0f, angle, 0f);

			controller.Move((camF * verticalInput + camR * horizontalInput) * speed * Time.deltaTime);
		}

		directionY -= gravity * Time.deltaTime * 0.5f;
		direction.y = directionY;

		controller.Move(transform.up * directionY);

		if (controller.isGrounded)
		{
			if (Input.GetButton("Jump"))
			{
				directionY = jumpSpeed;
			}
		}
	}

}
