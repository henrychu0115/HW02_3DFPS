using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour {

	public Transform rotateYTransform;
	public Transform rotateXTransform;
	public float rotateSpeed ;
	public float currentRotateX = 0;
	private Animator animatorController;
	public float MoveSpeed;
	float currentSpeed = 0;
	public Rigidbody rigidBody;

	public JumpSensor JumpSensor;
	public float JumpSpeed;

	public GunManager gunManager;
	public GunManager02 gunManager02;

	public GameObject gun01;
	public GameObject gun02;

	public GameUIManager uiManager;
	public int hp = 100;

	private AudioSource walksound;

	// Use this for initialization
	void Start () {
		walksound = this.GetComponent<AudioSource> ();
		animatorController = this.GetComponent<Animator> ();

	}

	public void Hit(int value)
	{
		if (hp <= 0) {
			return;
		}
		hp -= value;
		uiManager.SetHP (hp);
		if (hp > 0) {
			uiManager.PlayHitAnimation ();
		} else {
			uiManager.PlayerDiedAnimation ();

			rigidBody.gameObject.GetComponent<Collider> ().enabled = false;
			rigidBody.useGravity = false;
			rigidBody.velocity = Vector3.zero;
			this.enabled = false;
			rotateXTransform.transform.DOLocalRotate (new Vector3 (-60, 0, 0),0.5f);
			rotateYTransform.transform.DOLocalMoveY (-1.5f,0.5f).SetRelative (true);
		}

	}



	// Update is called once per frame
	void Update () {

		Cursor.visible = false;
		if (Input.GetMouseButton (0)) {
		
			gunManager.TryToTriggerGun ();

		
		}
		if (Input.GetMouseButton (1)) {


			gunManager02.TryToTriggerGun ();

		}


		Vector3 movDirection = Vector3.zero;

		if (Input.GetKeyDown (KeyCode.Z)) {
			gun01.gameObject.SetActive(false);
			gun02.gameObject.SetActive (true);
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			gun01.gameObject.SetActive(true);
			gun02.gameObject.SetActive (false);
		}

		if (Input.GetKey (KeyCode.W)) {
			movDirection.z += 1;
		}
		if (Input.GetKeyDown (KeyCode.W)) {
			walksound.Play();
		}
		if (Input.GetKeyUp (KeyCode.W)) {
			walksound.Stop ();
		}

		if (Input.GetKey (KeyCode.S)) {
			movDirection.z -= 1;
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			walksound.Play();
		}
		if (Input.GetKeyUp (KeyCode.S)) {
			walksound.Stop ();
		}

		if (Input.GetKey (KeyCode.A)) {
			movDirection.x -= 1;
		}
		if (Input.GetKeyDown (KeyCode.A)) {
			walksound.Play();
		}
		if (Input.GetKeyUp (KeyCode.A)) {
			walksound.Stop ();
		}

		if (Input.GetKey (KeyCode.D)) {
			movDirection.x += 1;
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			walksound.Play();
		}
		if (Input.GetKeyUp (KeyCode.D)) {
			walksound.Stop ();
		}

		movDirection = movDirection.normalized;

		if (movDirection.magnitude == 0) {
			currentSpeed = 0;
		}
		else
		{
			if (movDirection.z < 0) {
				currentSpeed = -MoveSpeed;
			} else {
				currentSpeed = MoveSpeed;
			}
		}
		animatorController.SetFloat ("Speed", currentSpeed);

		Vector3 worldSpaceDirection = movDirection.z * rotateYTransform.transform.forward + movDirection.x * rotateYTransform.transform.right;

		Vector3 velocity = rigidBody.velocity;
		velocity.x = worldSpaceDirection.x * MoveSpeed;
		velocity.z = worldSpaceDirection.z * MoveSpeed;

		if (Input.GetKey (KeyCode.Space) && JumpSensor.IsCanJump ()) 
		{
			velocity.y = JumpSpeed;
		}


		rigidBody.velocity = velocity;

		rotateYTransform.transform.localEulerAngles += new Vector3 (0, Input.GetAxis ("Horizontal"), 0) * rotateSpeed;
		currentRotateX += Input.GetAxis ("Vertical") * rotateSpeed;

		if (currentRotateX > 90) {
			currentRotateX = 90;
		}else if(currentRotateX < -90){
			currentRotateX = -90;
		}

		rotateXTransform.transform.localEulerAngles = new Vector3 (-currentRotateX,0,0) * rotateSpeed;

				
	}
}
