using UnityEngine;
using System;
using Hover.Core.Items.Types;

public class StageArea : SystemArea {

	public static string filePathToLoad;
	public GameObject PianoMenu;
	public GameObject Keyboard;
	public HoverItemDataSlider SliderDistance;
	public HoverItemDataSlider SliderHeight;
	public CameraController Camera;
	public IPianoSound PianoSound;

	void Start () {
	}
	
	void Update () {
	}

	public void BackToMainMenu() {
		this.ChangeKeyboardActivationState(false);		
		this.PianoSound.StopAllNotes();
		this.PianoSound.Cleanup();
		this.Navigation.NavigateTo(Constants.MainPosition);
		this.PianoMenu.SetActive(false);
		this.enabled = false;
	}

	private void ChangeKeyboardActivationState(bool newState) {
		for(int i=0; i < this.Keyboard.transform.childCount; i++) {
			this.Keyboard.transform.GetChild(i).GetComponent<KeyPlayManager>().enabled = newState;
			this.Keyboard.transform.GetChild(i).GetComponent<KeyPhysics>().enabled = newState;
			this.Keyboard.transform.GetChild(i).GetComponent<BoxCollider>().enabled = newState;
		}
	}

	public override void ActivateArea() {
		this.enabled = true;
		this.PianoMenu.SetActive(true);
		if(this.Camera.isJustCalibrated) {
			this.SliderDistance.Value = 0.5f;
			this.SliderHeight.Value = 0.5f;
			this.Camera.isJustCalibrated = false;
			this.Camera.UpdateCameraPosition();
		}
		
		this.PianoSound = new PianoSoundManager();
		this.PianoSound.Initialize();
		this.PianoSound.LoadSoundResource(filePathToLoad);
		this.ChangeKeyboardActivationState(true);
	}

	public override bool IsSameType(string type){
		if(type.Equals(Constants.PlayingPosition)){
			return true;
		}
		return false;
	}
}
