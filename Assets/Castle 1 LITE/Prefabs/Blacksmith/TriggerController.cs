using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
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
		myDoor.Play("Open2", 0, 0.0f);
		yield return new WaitForSeconds(2.0f);
		myDoor.Play("Close2", 0, 0.0f);
	}
}

