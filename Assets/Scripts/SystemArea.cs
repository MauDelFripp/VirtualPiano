using System;
using UnityEngine;

[System.Serializable]
public abstract class SystemArea : MonoBehaviour
{
	public Navigation Navigation;

	public abstract void ActivateArea();
	public abstract bool IsSameType(string type);
}
