#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(NavigationActions))]
public class NavigationActionsInspector : Editor {
    public override void OnInspectorGUI () {
		serializedObject.Update();
		EditorGUILayout.PropertyField(serializedObject.FindProperty("areasList"), true);
		serializedObject.ApplyModifiedProperties();
	}
}
#endif