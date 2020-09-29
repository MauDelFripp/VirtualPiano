using NAudio.CoreAudioApi;
using UnityEngine;
using System.Runtime.InteropServices;

public class VolumeControl
{
    public static void setVolume(float newVolume)
    {
        MMDeviceEnumerator devEnum = new MMDeviceEnumerator();
        MMDevice device = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
        device.AudioEndpointVolume.MasterVolumeLevelScalar = newVolume/100f;
    }
}