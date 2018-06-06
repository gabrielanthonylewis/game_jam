using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class DeathCamera : MonoBehaviour {


	//[SerializeField]
	//private GameObject _deathCamera = null;

	[SerializeField]
	private GameObject[] _children;

	private Animator _Animator = null;

	// Use this for initialization
	void Start() 
	{
		_Animator = this.GetComponent<Animator> ();

		_Animator.Stop ();
		_Animator.enabled = false;
	}
	
	public void StartSequence()
	{
		this.transform.position = Camera.main.transform.position;
	
		Camera.main.gameObject.SetActive (false);

		_Animator.enabled = true;
		_Animator.Play("UIDeath");

		foreach (GameObject child in _children)
			child.SetActive (true);
	}
}
