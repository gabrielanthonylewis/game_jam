using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Transform))]
public class MoveController : MonoBehaviour 
{
	[SerializeField]
	private int _currPlayer = 0;
	public int currPlayer { set { _currPlayer = value; } get { return _currPlayer; }}

	[SerializeField] private float _horizSpeedMulti = 2f;
	[SerializeField] private float _vertSpeedMulti = 2f;
	[SerializeField] private float _runningMulti = 2f;
	[SerializeField] private float _crouchMulti = 0.5f;
	[SerializeField] private float _proneMulti = 0.25f;
	[SerializeField] private float _jumpUpMulti = 200f;

	[Header("WOULD USE ANIMATIONS IDEALY")]
	[SerializeField] private Transform _obj = null;
    [HideInInspector] private Animator player_anim;


	// Component References
	private Rigidbody _Rigidbody = null;
	private Transform _Transform = null;



	// Use this for initialization
	void Start () 
	{
        player_anim = GetComponent<Animator>();
		_Transform = this.transform;
		_Rigidbody = this.GetComponent<Rigidbody> ();
	}




	void FixedUpdate () 
	{

		if ( Time.timeScale == 0)
			return;

        if(Camera.main == null)
        {
            return;
        }
		// Input
		float vertical = Input.GetAxisRaw ("Vertical" + _currPlayer) * Time.fixedDeltaTime * _vertSpeedMulti;
		float horizontal = Input.GetAxisRaw ("Horizontal" + _currPlayer) * Time.fixedDeltaTime *  _horizSpeedMulti;

        if(vertical == 0 && horizontal == 0)
        {
            player_anim.SetTrigger("hasStopped");
            return;
        }
        else
        {
            Move(vertical, horizontal);
        }

    }

	private float GetJumpMoveMulti(bool state)
	{
		if (state == true)
			return 0.2f;
		else
			return 1f;
	}

	private float GetFloat(bool val)
	{
		if (val == true)
			return 1f;
		else
			return 0f;
	}


	private void Move(float vertical, float horizontal)
	{
        player_anim.SetTrigger("isWalking");
		Vector3 viewPos = Camera.main.WorldToViewportPoint (_Transform.position + _Transform.forward * vertical + _Transform.right * horizontal);

		if (viewPos.x > 1 || viewPos.x < 0
			|| viewPos.y > 1 || viewPos.y < 0)
		{

			return;
		}

		_Rigidbody.MovePosition (_Transform.position 
			+ _Transform.forward * vertical
			+ _Transform.right * horizontal);
	}







}
