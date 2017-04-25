using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionListScript : MonoBehaviour {
	public List<Collider> CollisionObjects; 
	// Use this for initialization
	public void OnTriggerEnter (Collider other) {
		CollisionObjects.Add (other);
	}
	
	// Update is called once per frame
	public void OnTriggerExit (Collider other) {
		CollisionObjects.Remove (other);
	}
}
