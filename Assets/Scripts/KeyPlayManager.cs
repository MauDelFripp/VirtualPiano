using UnityEngine;

public class KeyPlayManager : MonoBehaviour {
	private Quaternion hammerHitAngle = Quaternion.Euler(new Vector3(2, 0, 0));
	private bool isPlaying = false;
	private float maxVelocity = 0f;
	private float[] normalRange = new float[] {0.2f, 4};
	private float[] loudRange = new float[] {4, 9};
	private Rigidbody keyPhysic;
	private IPianoSound pianoSound;

	void Start () {
		this.pianoSound = new PianoSoundManager(); 
		this.keyPhysic = this.transform.GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
		float keyAngularVelocity = this.keyPhysic.angularVelocity.z*-1;
		if (this.HasStoppedPlayingNote(keyAngularVelocity)) {
			this.isPlaying = false;
			this.pianoSound.StopNote(this.GetKeyNumber());
		}
		else if (this.HasToPlayNote(keyAngularVelocity)) {
			float velocityNormalized = this.NormalizeAngularVelocity(keyAngularVelocity);
			int midiVelocity = Mathf.CeilToInt(velocityNormalized*127);
			this.isPlaying = true;
			this.pianoSound.PlayNote(this.GetKeyNumber(), midiVelocity);
		}
	}

	private int GetKeyNumber() {
		return int.Parse(this.transform.name.Remove(0, 3));
	}

	private bool HasStoppedPlayingNote(float keyAngularVelocity) {
		return this.hammerHitAngle.x > this.keyPhysic.rotation.x && this.isPlaying;
	} 

	private bool HasToPlayNote(float keyAngularVelocity) {
		return keyAngularVelocity >= normalRange[0] && this.hammerHitAngle.x <= this.keyPhysic.rotation.x && !this.isPlaying;
	} 

	private float NormalizeAngularVelocity(float keyAngularVelocity) {
		if(keyAngularVelocity >= normalRange[0] && keyAngularVelocity <= normalRange[1]) {
			return (keyAngularVelocity * 0.9f) / normalRange[1];
		}
		else if(keyAngularVelocity > loudRange[0] && keyAngularVelocity <= loudRange[1]) {
			return ((keyAngularVelocity * 0.1f) / loudRange[1]) + 0.9f;
		}
		else if (keyAngularVelocity > loudRange[1]) {
			return 1;
		}
		else {
			return 0;
		}
	}
}
