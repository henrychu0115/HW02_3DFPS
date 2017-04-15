using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	public float FlyingSpeed;
	public float LifeTime;
	public GameObject explosion;

	// Use this for initialization
	public void InitAndShoot (Vector3 Direction)
	{
		Rigidbody rigidbody = this.GetComponent<Rigidbody> ();

		rigidbody.velocity = Direction * FlyingSpeed;

		Invoke ("KillYourself", LifeTime);
	}

	public void KillYourself()
	{
		GameObject.Destroy (this.gameObject);
	}

	public float damageValue = 15;
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) 
	{
		other.gameObject.SendMessage ("Hit", damageValue);

		explosion.transform.parent = null;
		explosion.SetActive (true);

		KillYourself ();
	}
}
