using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(LookController))]
[CanEditMultipleObjects]

public class LookControllerEditor : Editor
{

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector ();

		LookController _myTarget = (LookController)target;

		//serializedObject.Update ();
		Undo.RecordObject (_myTarget, "LookControllerEditor");

		{
			GUILayout.BeginVertical ("Main", EditorStyles.helpBox);
			GUILayout.Space (10f);

			_myTarget.SetAllowHorizontal (GUILayout.Toggle (_myTarget.allowHorizontal, "Allow Horizontal"));


			if (_myTarget.allowHorizontal) {
				GUILayout.BeginVertical (EditorStyles.helpBox);
				_myTarget.SetSensitivityX (EditorGUILayout.FloatField ("Sensitivity", _myTarget.sensitivityX));
				GUILayout.EndVertical ();
			}


			_myTarget.SetAllowVertical (GUILayout.Toggle (_myTarget.allowVertical, "Allow Vertical"));

			if (_myTarget.allowVertical) {
				GUILayout.BeginVertical (EditorStyles.helpBox);
				_myTarget.SetSensitivityY (EditorGUILayout.FloatField ("Sensitivity", _myTarget.sensitivityY));


				GUILayout.BeginVertical ("Angle Limits", 
					EditorStyles.helpBox);
				//GUILayout.Label ("Angle Limits", EditorStyles.boldLabel);
				GUILayout.Space (10f);

				_myTarget.SetUpperLimit (EditorGUILayout.FloatField ("Upper Limit", _myTarget.upperLimit));
				_myTarget.SetLowerLimit (EditorGUILayout.FloatField ("Lower Limit", _myTarget.lowerLimit));
				GUILayout.EndVertical ();
				GUILayout.EndVertical ();
			}

			GUILayout.EndVertical ();
		}

		{
			GUILayout.BeginVertical ("Other", EditorStyles.helpBox);
			GUILayout.Space (10f);
			_myTarget.SetUseRigidbodyIfAvailable (GUILayout.Toggle (_myTarget.useRigidbodyIfAvailable, "Use Rigidbody if available"));
			GUILayout.EndVertical ();
		}

	

		//serializedObject.ApplyModifiedProperties ();
	}


}
