using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private Transform _laser = null;

    [SerializeField]
    private LayerMask _layerMask;

    private Vector3 _initialPosition;

    private void Start()
    {
        _initialPosition = _laser.localPosition;
    }

    // Update is called once per frame
    void Update ()
    {
        if (_laser == null)
            return;

        RaycastHit hit;

        if(Physics.Raycast(this.transform.position, this.transform.forward, out hit, 100, _layerMask))
        {
            // Calculate new distance
            Vector3 newScale = _laser.localScale;
            newScale.z = hit.distance;
            _laser.localScale = newScale;

            // Move accordingly so that the laser is at the firepoint
            Vector3 newPos = _initialPosition;
            newPos.z = (newScale.z / 2.0F);
            _laser.localPosition = newPos;
        }
        else
        {
            // Calculate new distance
            Vector3 newScale = _laser.localScale;
            newScale.z = 100;
            _laser.localScale = newScale;

            // Move accordingly so that the laser is at the firepoint
            Vector3 newPos = _initialPosition;
            newPos.z = 50;
            _laser.localPosition = newPos;
        }
	
	}
}
