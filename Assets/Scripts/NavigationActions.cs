using UnityEngine;
using System.Collections.Generic;
public class NavigationActions : MonoBehaviour {

	public List<SystemArea> areasList = new List<SystemArea>();

	void Start () {
	}
	
	void Update () {
	}

	public void RegisterArea(SystemArea area)
	{
		this.areasList.Add(area);
	}

	public void ActivateArea(string type) {
		foreach(SystemArea area in areasList){
			if(area.IsSameType(type)){
				area.ActivateArea();
				return;
			}
		}
	}
}
