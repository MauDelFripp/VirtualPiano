using UnityEngine;
using System.Collections;
using Leap.Unity;

public class RigidHandEditorPersistence : RigidHand {
	public override bool SupportsEditorPersistence()
	{
		return true;
	}
}
