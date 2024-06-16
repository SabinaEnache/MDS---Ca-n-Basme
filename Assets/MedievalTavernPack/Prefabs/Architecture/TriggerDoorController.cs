using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
	[SerializeField] private Animator myDoor = null;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			StartCoroutine(controleazaUsa());
		}
	}

	private IEnumerator controleazaUsa()
	{
		myDoor.Play("OpenDoor", 0, 0.0f);
		yield return new WaitForSeconds(1.0f);
		myDoor.Play("CloseDoor", 0, 0.0f);
	}
}