using UnityEngine;
using System.Collections;

public class MainMenuArea : SystemArea {

	void Start () {
	
	}
	
	void Update () {
	
	}

	public void GoToCalibration() {
		this.Navigation.NavigateTo(Constants.CalibrationPosition);
		this.enabled = false;
	}

	public void GoToSoundChooser() {
		this.Navigation.NavigateTo(Constants.SoundChooserPosition);
		this.enabled = false;
	}

	public void GoToPlay() {
		this.Navigation.NavigateTo(Constants.PlayingPosition);
		this.enabled = false;
	}

	public void GoToHelp() {
		this.Navigation.NavigateToWithoutCallback(Constants.HelpPosition);
	}

	public void GoToMainMenu() {
		this.Navigation.NavigateToWithoutCallback(Constants.MainPosition);
	}

	public override void ActivateArea() {
		this.enabled = true;
	}

	public override bool IsSameType(string type){
		if(type.Equals(Constants.MainPosition)){
			return true;
		}
		return false;
	}
}
