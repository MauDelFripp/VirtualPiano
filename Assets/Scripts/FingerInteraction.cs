using UnityEngine;
using System.Collections;

public class FingerInteraction : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
	}

	void OnCollisionExit(Collision collision) {
		this.gameObject.layer = LayerMask.NameToLayer("Default");		
    }

	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "WhiteKey")
		{
			this.gameObject.layer = LayerMask.NameToLayer("FingerToWhite");
		}
		else if(collision.gameObject.tag == "BlackKey")
		{
			this.gameObject.layer = LayerMask.NameToLayer("FingerToBlack");
		}
    }
}
