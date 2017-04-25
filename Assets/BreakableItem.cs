﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BreakableItem : MonoBehaviour {
	[System.Serializable]
	// Use this for initialization
	public class BreakingEntry
	{
		public GameObject breakNode;
		public float breakingHP;
	}
	public float CurrentHP;
	public List<BreakingEntry> BreakingSetting;
	public GameObject DestroyEffect;

	public void Hit(float hitValue){

		if (CurrentHP > 0) {
			CurrentHP -= hitValue;
			if (CurrentHP <= 0) {
				DestroyEffect.gameObject.SetActive (true);
				this.transform.DOScale (new Vector3 (0.0f, 0.0f, 0.0f), 0.01f).SetDelay (0.1f).OnComplete (() => {
					Invoke ("DisableParticleSystem", 10);

				});
			} else {
				foreach (BreakingEntry entry in BreakingSetting) {
					if (CurrentHP < entry.breakingHP) {
						entry.breakNode.gameObject.SetActive (true);
					}
				}
			}
		}
		
	}
	public void DisableParticleSystem()
	{
		ParticleSystem[] particles = this.GetComponentsInChildren<ParticleSystem> ();

		foreach(ParticleSystem p in particles)
		{
			p.Stop ();
		}
		Invoke ("KillYourself",5);
	
	}


	public void KillYourself()
	{
		GameObject.Destroy (this.gameObject);
	}
}
