using UnityEngine;
using System.Collections;
using System.Collections.Generic; // for Dictionary

public class ObjectPool : MonoBehaviour
{
	// Singleton pattern
	public static ObjectPool _instance = null;
	public static ObjectPool Instance { get { return _instance; } }

	// Object and quantity to create in object pool
	[System.Serializable]
	private struct ToStore
	{
		public GameObject obj;
		public int quantity;
	};
	[SerializeField]
	private ToStore[] _toStore;

	// Object and use status 
	private struct StoredObject
	{
		public GameObject obj;
		public bool beingUsed;
	};
	// Access stored objects using name
	private Dictionary<string, StoredObject[]> _storage = new Dictionary<string, StoredObject[]>();


	private void Awake()
	{	
		// Singleton pattern
		if (_instance != null && _instance != this)
			Destroy (this.GetComponent<ObjectPool>());
		else
			_instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		InitialiseStorage ();
	}


	private void InitialiseStorage()
	{
		// make hierachy 
		GameObject objectPoolHier = new GameObject ();
		objectPoolHier.name = "objectpool";

		foreach (ToStore itemQua in _toStore) 
		{
			
	
			StoredObject[] newObjects = new StoredObject[itemQua.quantity];

			// make hierachy 
			GameObject newObjectHeir = new GameObject();
			newObjectHeir.name = itemQua.obj.name + "s";
			newObjectHeir.transform.SetParent (objectPoolHier.transform);
		

			for (int i = 0; i < newObjects.Length; i++) 
			{
				newObjects[i].obj = Instantiate (itemQua.obj, new Vector3 (-1000.0F, -1000.0F, -1000.0F), Quaternion.Euler (Vector3.zero)) as GameObject; 
				newObjects [i].obj.name = itemQua.obj.name;
				newObjects [i].obj.transform.SetParent (newObjectHeir.transform);

				Rigidbody rb = newObjects [i].obj.GetComponent<Rigidbody> ();
				if (rb != null) {
					rb.Sleep ();
					rb.velocity = Vector3.zero;
					rb.angularVelocity = Vector3.zero;
					rb.isKinematic = true;
					rb.useGravity = false;
				}


				newObjects [i].obj.SetActive (false);



				newObjects [i].beingUsed = false;
			}


			_storage.Add (itemQua.obj.name, newObjects);
		}

	}

	public GameObject GetFirstFreeObject(string objectName)
	{
		StoredObject[] objects;
		if (!_storage.TryGetValue (objectName, out objects))
			return null;

		for (int i = 0; i < objects.Length; i++)
		{

			if (objects[i].beingUsed == true)
				continue;


			if (objects [i].obj == null)
				continue;

			Rigidbody rb = objects [i].obj.GetComponent<Rigidbody> ();
			if (rb != null) {
				rb.isKinematic = false;
				rb.useGravity = true;
			}


			objects[i].beingUsed = true;

			return objects[i].obj;
		}
			



		return null;
	}


	public bool Kill (GameObject obj)
	{
		StoredObject[] objects;
		if (!_storage.TryGetValue (obj.name, out objects)) {

			return false;
		}
		for (int i = 0; i < objects.Length; i++)
		{

			if (objects[i].obj.GetInstanceID() != obj.GetInstanceID())
				continue;

			Rigidbody rb = objects [i].obj.GetComponent<Rigidbody> ();
			if (rb != null) {
				rb.Sleep ();
				rb.velocity = Vector3.zero;
				rb.angularVelocity = Vector3.zero;
				rb.isKinematic = true;
				rb.useGravity = false;
			}

			objects [i].beingUsed = false;
			objects [i].obj.SetActive (false);

			if (objects [i].obj.GetComponent<NavMeshAgent> ()) {
				objects [i].obj.GetComponent<NavMeshAgent> ().enabled = false;
			}

			// Want to do but need to disable navmesh?
			objects [i].obj.transform.position = new Vector3 (-1000.0F, -1000.0F, -1000.0F);

			return true;
		}

		return false;
	}
}
