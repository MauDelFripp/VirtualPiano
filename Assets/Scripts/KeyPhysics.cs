using UnityEngine;
using System.Collections;

public class KeyPhysics : MonoBehaviour {

	Quaternion keyTopAngle = Quaternion.Euler(new Vector3(0, 0, 0)); 
	Quaternion keyBedAngle = Quaternion.Euler(new Vector3(5, 0, 0));
	public Vector3 keyConstantTorque = new Vector3(0, 0, 0);
	private Vector3 keyInitialPosition;
	private bool stateKeyBed = false;
	private bool isInCollision = false;

	void Start () {
		this.transform.GetComponent<Rigidbody>().maxAngularVelocity = Mathf.Infinity;
		this.keyInitialPosition = this.transform.localPosition;
	}
	
	void Update () {
		if(!stateKeyBed){
			this.resetPosition();
			if(this.transform.localRotation.x < this.keyTopAngle.x) {
				this.KeyTop();
			}
			else if(this.transform.localRotation.x == this.keyTopAngle.x) {
				this.Idle();
			}
			else if(this.transform.localRotation.x > this.keyTopAngle.x && this.transform.localRotation.x <= this.keyBedAngle.x) {
				this.Moving();
			}
			else if(this.transform.localRotation.x > this.keyBedAngle.x) {
				this.KeyBed();
			}
		}else{
			if(isInCollision){
				KeyBed();
			}
			else
			{
				this.isInCollision = false;
				this.stateKeyBed = false;
				this.AddKeyConstantForce();
			}
		}
	}

	private void resetPosition()
	{
		this.transform.localPosition = this.keyInitialPosition;
	}

	private void KeyTop() {
		this.RemoveInertia();		
		this.transform.localRotation = this.keyTopAngle;
	} 

	private void Idle() { } 

	private void Moving() {
		this.AddKeyConstantForce();
	} 

	private void KeyBed() {
		this.RemoveInertia();
		this.transform.localRotation = this.keyBedAngle;
		this.stateKeyBed = true;
	}

	void OnCollisionExit(Collision collision) {
		this.isInCollision = false;
		this.stateKeyBed = false;
        this.AddKeyConstantForce();
    }

	void OnCollisionEnter(Collision collision) {
		this.isInCollision = true;
    }

	private void RemoveInertia() {
		this.transform.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);			
		this.transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
		this.transform.localPosition = this.keyInitialPosition;
	}

	private void AddKeyConstantForce() {
		this.transform.GetComponent<Rigidbody>().AddTorque(this.keyConstantTorque, ForceMode.Impulse);	
	}
}
