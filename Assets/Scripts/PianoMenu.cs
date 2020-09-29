using UnityEngine;
using Hover.Core.Items.Types;
using System.Diagnostics;

public class PianoMenu : MonoBehaviour
{
    public CameraController Camera;
    private Process audioControl;
    private int previousVolumeValue = -1;

    void Start()
    {
        audioControl = new Process();
        audioControl.StartInfo.FileName = Application.dataPath + "/StreamingAssets/VolumeControl.exe";
    }

    void Update()
    {

    }

    public void SetDistanceToPianoOffset()
    {
        GameObject sliderDistance = GameObject.Find("Slider-Distancia");
        if(sliderDistance != null)
        {
            float offset = sliderDistance.GetComponent<HoverItemDataSlider>().RangeValue;
            this.Camera.SetPlayingCameraDistanceOffset(offset);
        }
    }

    public void SetHeightOffset()
    {
        GameObject sliderHeight = GameObject.Find("Slider-Altura");
        if(sliderHeight != null)
        {
            float offset = sliderHeight.GetComponent<HoverItemDataSlider>().RangeValue;
            this.Camera.SetPlayingCameraHeightOffset(offset);
        }
    }

    public void SetVolume()
    {
        int volume = Mathf.CeilToInt(GameObject.Find("Slider-Volumen").GetComponent<HoverItemDataSlider>().RangeValue);
		audioControl.StartInfo.Arguments = "set " + volume;
		audioControl.Start();
	}
}
