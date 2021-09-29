using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

	public int isAlive;

	private Material mat;

	void Start() {
		
	}

	public void changeAliveState(int state) {
		isAlive = state;

		if (isAlive == 0) {
			gameObject.SetActive(false);
			gameObject.GetComponent<BoxCollider>().enabled = false;
		} 
		else if (isAlive == 1) {
			gameObject.SetActive(true);
			gameObject.GetComponent<BoxCollider>().enabled = true;
		}
	}

}
