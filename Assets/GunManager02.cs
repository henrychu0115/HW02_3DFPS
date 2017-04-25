﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GunManager02 : MonoBehaviour {

	public float MinimumShootPeriod;
	public float muzzleShowPeriod;

	private float shootCounter = 0;
	//private float muzzleCounter = 0;

	//public GameObject muzzleFlash;
	public GameObject bulletCandidate;
	private AudioSource gunShootSound;

	public void Start(){
		gunShootSound = this.GetComponent<AudioSource> ();

	}

	// Use this for initialization
	public void TryToTriggerGun () {
		if (shootCounter <= 0) {
			/*gunShootSound.Stop ();
			gunShootSound.pitch = Random.Range (0.8f,1);
			gunShootSound.Play ();*/

			this.transform.DOShakeRotation (MinimumShootPeriod*0.8f, 3f);

			//muzzleCounter = muzzleShowPeriod;
			//muzzleFlash.transform.localEulerAngles = new Vector3 (0, 0, Random.Range (0, 360));

			shootCounter = MinimumShootPeriod;

			GameObject newBullet = GameObject.Instantiate (bulletCandidate);//生成子彈

			BulletScript bullet = newBullet.GetComponent<BulletScript> ();//把子彈賦予子彈的屬性
			//bullet.transform.position = muzzleFlash.transform.position;
			//bullet.transform.rotation = muzzleFlash.transform.rotation;

			//bullet.InitAndShoot (muzzleFlash.transform.forward);

		}
	}

	// Update is called once per frame
	void Update () {
		if (shootCounter > 0) {

			shootCounter -= Time.deltaTime;

		}

		/*if (muzzleCounter > 0) {

			muzzleFlash.gameObject.SetActive (true);
			muzzleCounter -= Time.deltaTime;

		} else {
			muzzleFlash.gameObject.SetActive (false);
		}*/
	}
}
