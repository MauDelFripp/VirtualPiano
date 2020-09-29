using UnityEngine;
using System.Collections;

public class Navigation : MonoBehaviour {

	public CameraController Camera;
	private string CurrentPosition;

	void Start () {
		this.CurrentPosition = Constants.HelpPosition;
	}
	
	void Update () {
	}

	public void NavigateTo(string destination) {
		iTween.MoveTo(this.Camera.gameObject, iTween.Hash("path", iTweenPath.GetPath(this.CurrentPosition + "-" + destination), "time", 5, "easeType", iTween.EaseType.easeInOutSine, 
			"orienttopath", true, "oncomplete", "ActivateArea", "oncompletetarget", this.gameObject, "oncompleteparams", destination));
		this.CurrentPosition = destination;
	}

	public void NavigateToWithoutCallback(string destination) {
		iTween.MoveTo(this.Camera.gameObject, iTween.Hash("path", iTweenPath.GetPath(this.CurrentPosition + "-" + destination), "time", 5, "easeType", iTween.EaseType.easeInOutSine, 
			"orienttopath", true));
		this.CurrentPosition = destination;
	}
}
