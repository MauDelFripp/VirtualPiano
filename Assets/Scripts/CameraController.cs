using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject Camera;
	private static Vector3 PlayingCameraPosition;
	private Vector3 CameraPositionOffset;
    public bool isJustCalibrated = false;

	void Start () {
		this.CameraPositionOffset = Vector3.zero;
		PlayingCameraPosition = new Vector3(-34.6326f, 12.25f, 6.485892f);
	}
	
	void Update () {
	}

	public void UpdateCameraPosition()
	{
		this.Camera.transform.position = PlayingCameraPosition;
	}

	public void SetPlayingCameraPosition(Vector3 calibratedPosition) {	
		this.isJustCalibrated = true;
		this.CameraPositionOffset = Vector3.zero;
		PlayingCameraPosition = calibratedPosition;
	}

	public void SetPlayingCameraDistanceOffset(float offset){
		this.CameraPositionOffset.x = -offset/200;
		this.Camera.transform.position = this.CameraPositionOffset + PlayingCameraPosition;
	}

	public void SetPlayingCameraHeightOffset(float offset){
		this.CameraPositionOffset.y = offset/400;
		this.Camera.transform.position = this.CameraPositionOffset + PlayingCameraPosition;
	}
}