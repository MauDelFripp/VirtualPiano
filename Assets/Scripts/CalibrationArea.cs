using UnityEngine;
using System.Collections.Generic;
using Leap.Unity;
using Leap;

public class CalibrationArea : SystemArea {
	public Transform LeftHand;
	public GameObject CalibrationPiano;
	LeapProvider Provider;
	private static Vector3 DefaultPlayingCameraPosition;
	private static Vector3 DefaultCalibrationPianoPosition;
	public CameraController Camera;
	public Vector3 lastPosition = Vector3.zero;
	private bool isCalibrationEnable = true;
	public GameObject defaultCalibrationButton;
	public GameObject calibrationButton;
	public GameObject startCalibrationButton;

	void Start () {
		DefaultPlayingCameraPosition = new Vector3(-34.6326f, 12.25f, 6.485892f);
		DefaultCalibrationPianoPosition = new Vector3(42.87f, 4.72f, -53.8f);
		this.Provider = FindObjectOfType<LeapProvider>() as LeapProvider;
	}
	
	void Update () {
		if(this.isCalibrationEnable) {
			this.SetCalibrationPianoPosition();
		}
	}
	
	public void SetPlayingCameraPosition(){
		Vector3 offset = this.CalibrationPiano.transform.TransformDirection(this.CalibrationPiano.transform.position - DefaultCalibrationPianoPosition);
		offset.y *= -1;
		offset.z += 0.25f;
		//Vector3 pianoKeySize = this.CalibrationPiano.transform.GetChild(1).GetChild(0).GetComponent<Renderer>().bounds.size;
		// offset.y -= pianoKeySize.y;
		Vector3 resultPosition = DefaultPlayingCameraPosition + offset;
		this.Camera.SetPlayingCameraPosition(resultPosition);
		this.ResetCalibrationPianoPosition();
	}

	public void SetDefaultPlayingCameraPosition(){
		this.Camera.SetPlayingCameraPosition(DefaultPlayingCameraPosition);
		this.ResetCalibrationPianoPosition();
	}

	private void ResetCalibrationPianoPosition() {
		this.isCalibrationEnable = false;
		this.calibrationButton.SetActive(false);
		this.defaultCalibrationButton.SetActive(false);
		this.startCalibrationButton.SetActive(true);		
		this.CalibrationPiano.transform.position = DefaultCalibrationPianoPosition;
	}

	private void SetCalibrationPianoPosition() {
		Frame frame = this.Provider.CurrentFrame;
      	List<Hand> hands = frame.Hands;
		if(hands.Exists(h => h.IsLeft)) {
			Vector3 pianoFirstKey = this.CalibrationPiano.transform.GetChild(1).GetChild(0).transform.position;
            Vector3 pianoKeySize = this.CalibrationPiano.transform.GetChild(1).GetChild(0).GetComponent<Renderer>().bounds.size;
            Vector3 pianoLastKey = this.CalibrationPiano.transform.GetChild(1).GetChild(this.CalibrationPiano.transform.GetChild(1).childCount-1).transform.position;

			this.CalibrationPiano.transform.position = LeftHand.position + (this.CalibrationPiano.transform.position - pianoFirstKey);
			Vector3 keyboardSize = pianoLastKey - pianoFirstKey;
			this.CalibrationPiano.transform.position -= new Vector3(keyboardSize.x/2, 0, keyboardSize.z/2);
			this.CalibrationPiano.transform.localPosition -= new Vector3(pianoKeySize.z/2 * Mathf.Cos(40), 0, pianoKeySize.z/2 * Mathf.Sin(40));
		} 
	}

	public void StartCalibration() {
		this.isCalibrationEnable = true;
		this.calibrationButton.SetActive(true);
		this.defaultCalibrationButton.SetActive(true);
		this.startCalibrationButton.SetActive(false);
	}

	public void BackToMainMenu() {
		this.Navigation.NavigateTo(Constants.MainPosition);
		this.enabled = false;
	}

	public override void ActivateArea() {
		this.enabled = true;
	}

	public override bool IsSameType(string type){
		if(type.Equals(Constants.CalibrationPosition)){
			return true;
		}
		return false;
	}
}
