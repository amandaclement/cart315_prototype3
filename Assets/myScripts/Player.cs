using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	// accessing bool from cameraRotator script
	public main bS;
	public bool enteredShip;
	public bool collectedWingR;
	public bool collectedWingL;
	public bool collectedBoosterR;
	public bool collectedBoosterL;

	private Animator anim;
		private CharacterController controller;

		public float speed;
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

	bool flip = false;

	//public Light playerLight;

	//private void OnCollisionEnter(Collision collision)
	//{
	//	if (collision.collider.gameObject == player)
	//	{
	//		Debug.Log("collision detected");
	//	}
	//}

	void Start () {
	    controller = GetComponent <CharacterController>();
	    anim = gameObject.GetComponentInChildren<Animator>(); // for the character animations

		//playerLight.GetComponent<Light>().intensity = 0;
	}

	//void stopFlip()
 //   {
	//	anim.SetBool("Flip", false);
	//}

    //void fadeOut()
    //{
    //    playerLight.GetComponent<Light>().intensity -= 0.5f * Time.deltaTime;
    //}
	//void stopFlip()
	//{
	//	flip = false;
	//	anim.SetBool("Flip", false);
	//}

	// TRIGGER FLIP AND PLAYER LIGHT (GLOW)
	void collected()
    {
		//anim.SetBool("Flip", true);
		//Invoke("stopFlip", 0.5f);

		//float currentIntensity = playerLight.GetComponent<Light>().intensity;
		//float targetIntensity = 0.2f;

		//// fade in light
		//if (currentIntensity < targetIntensity)
		//{
		//	currentIntensity += 0.2f * Time.deltaTime;
		//	Invoke("fadeOut", 1);
		//}
		//playerLight.GetComponent<Light>().intensity = currentIntensity;


		// transform.Translate(Vector3.up * Time.deltaTime * 10);

	}

	//void switchFlipState()
	//{
	//	flip = false;
	//}

	private void Update()
	{
		//if (Input.GetKeyDown(KeyCode.Space))
		//{
		//	collectAudio.Play();
		//}

		//if (bS.collectedWingR == true || bS.collectedWingL == true || bS.collectedBoosterR == true || bS.collectedBoosterL == true) // once collected, set the light intensity to 0
		//{
		//	collectAudio.Play();

		//}
	}

	void stopFlip()
    {
		// flip = false;
		anim.SetBool("Flip", false);
	}

	private bool isCoroutineExecuting = false;

	//IEnumerator ExecuteAfterTime(float time)
	//{
	//	yield return new WaitForSeconds(time);
	//	anim.SetBool("Flip", true);

	//	// Code to execute after the delay
	//}

	IEnumerator ExecuteAfterTime(float time)
	{
		//if (isCoroutineExecuting)
		//	yield break;

		//isCoroutineExecuting = true;
		////anim.SetBool("Flip", true);

		////yield return new WaitForSeconds(time);

		//// Code to execute after the delay

		//isCoroutineExecuting = false;
		////anim.SetBool("Flip", false);

		float timePassed = 0;
		if (timePassed < 1)
		{
			//anim.SetBool("Flip", true);
			timePassed += Time.deltaTime;

			yield return null;
		}
		else
		{
			anim.SetBool("Flip",false);
		}
	}

	private void FixedUpdate()
	{
		//anim.Play("Flip");

		//if (flip == true)
		//      {
		//	//anim.SetTrigger("Flip");
		//	flip = false;
		//	anim.Play("Jump");
		//} else
		//      {
		//	anim.SetInteger("AnimationPar", 0);
		//}

		//if (bS.collectedWingR == true || bS.collectedWingL == true || bS.collectedBoosterR == true || bS.collectedBoosterL == true) // once collected, set the light intensity to 0
		//{
		//	collectAudio.Play();

		//}

		if (bS.enteredShip == true)
		{
			Destroy(GetComponent<Player>()); // disable player movement
		}

		if (bS.collectedWingR == true)
		{
			//anim.SetTrigger("Flip");

			//Invoke("stopFlip", 1);
			//StartCoroutine(ExecuteAfterTime(1));


		}

		if (bS.collectedWingL == true)
		{
			//StartCoroutine(ExecuteAfterTime(10));
			//anim.SetBool("Flip", true);
			//Invoke("stopFlip", 1);
		}
		if (bS.collectedBoosterR == true)
		{

			//Invoke("stopFlip", 1);
		}
		if (bS.collectedBoosterL == true)
		{

			//Invoke("stopFlip", 1);
		}
		if (bS.collectedBody == true)
		{
	
			//Invoke("stopFlip", 1);
		}
		if (bS.collectedEngine == true)
		{
		
			//Invoke("stopFlip", 1);
		}

		// setting the character animations based on movement
		//if (Input.GetButton("Jump"))
		//{
		//	flip = true;

		//	//anim.SetTrigger("Flip");
		//}

		//if (flip == true)
		//      {
		//	anim.SetBool("Flip", true);
		//	Invoke("stopFlip", 1);
		//} else
		//      {
		//	anim.SetBool("Flip", false);
		//}

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
			speed = 28f;

		}
		else
		{
			speed = 16f;
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
