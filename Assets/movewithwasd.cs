//using UnityEngine;

//public class WASDToMove : MonoBehaviour
//{
//	public float speed;
//	public CharacterController controller;
//	private Vector3 position;

//	public AnimationClip runForward;
//	public AnimationClip runBackwards;
//	public AnimationClip runLeft;
//	public AnimationClip runRight;
//	public AnimationClip runBackwardsLeft;
//	public AnimationClip runBackwardsRight;
//	public AnimationClip idle;

//	private enum MoveDirection
//	{
//		None,
//		Forward,
//		Backwards,
//		Left,
//		Right,
//		BackwardsLeft,
//		BackwardsRight
//	}

//	private MoveDirection currentDirection = MoveDirection.None;

//	private Animation anim;

//	void Start()
//	{
//		position = transform.position;
//		anim = GetComponent<Animation>(); // Obține componenta de animație la început
//	}

//	void Update()
//	{
//		move();
//	}

//	void move()
//	{
//		float horizontalInput = Input.GetAxis("Horizontal");
//		float verticalInput = Input.GetAxis("Vertical");

//		Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
//		moveDirection = transform.TransformDirection(moveDirection);
//		moveDirection *= speed;

//		if (moveDirection.magnitude > 0)
//		{
//			if (horizontalInput > 0)
//			{
//				if (verticalInput > 0)
//					PlayAnimation(MoveDirection.Right);
//				else if (verticalInput < 0)
//					PlayAnimation(MoveDirection.BackwardsRight);
//				else
//					PlayAnimation(MoveDirection.Right);
//			}
//			else if (horizontalInput < 0)
//			{
//				if (verticalInput > 0)
//					PlayAnimation(MoveDirection.Left);
//				else if (verticalInput < 0)
//					PlayAnimation(MoveDirection.BackwardsLeft);
//				else
//					PlayAnimation(MoveDirection.Left);
//			}
//			else if (verticalInput > 0)
//			{
//				PlayAnimation(MoveDirection.Forward);
//			}
//			else if (verticalInput < 0)
//			{
//				PlayAnimation(MoveDirection.Backwards);
//			}
//			controller.SimpleMove(moveDirection);
//		}
//		else
//		{
//			PlayAnimation(MoveDirection.None);
//		}
//	}

//	void PlayAnimation(MoveDirection direction)
//	{
//		if (currentDirection == direction)
//			return;

//		currentDirection = direction;

//		switch (direction)
//		{
//			case MoveDirection.Forward:
//				anim.CrossFade(runForward.name);
//				break;
//			case MoveDirection.Backwards:
//				anim.CrossFade(runBackwards.name);
//				break;
//			case MoveDirection.Left:
//				anim.CrossFade(runLeft.name);
//				break;
//			case MoveDirection.Right:
//				anim.CrossFade(runRight.name);
//				break;
//			case MoveDirection.BackwardsLeft:
//				anim.CrossFade(runBackwardsLeft.name);
//				break;
//			case MoveDirection.BackwardsRight:
//				anim.CrossFade(runBackwardsRight.name);
//				break;
//			default:
//				anim.CrossFade(idle.name);
//				break;
//		}
//	}
//}
