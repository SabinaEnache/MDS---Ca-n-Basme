using UnityEngine;

public class WASDToMove : MonoBehaviour
{
	public float speed;
	public CharacterController controller;
	private Vector3 position;

	public AnimationClip walk;
	public AnimationClip idle;

	void Start()
	{
		position = transform.position;
	}

	void Update()
	{
		MoveWithWASD();
	}

	void MoveWithWASD()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");

		Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;

		if (moveDirection.magnitude > 0)
		{
			Quaternion newRotation = Quaternion.LookRotation(moveDirection);
			newRotation.x = 0f;
			newRotation.z = 0f;
			transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10);
			controller.SimpleMove(moveDirection);
			GetComponent<Animation>().CrossFade(walk.name);
		}
		else
		{
			GetComponent<Animation>().CrossFade(idle.name);
		}
	}
}
