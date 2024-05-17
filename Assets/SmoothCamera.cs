﻿using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
	public Transform target;
	public float distance = 10.0f;
	public float height = 5.0f;
	public float heightDamping = 2.0f;
	public float followDamping = 5.0f;
	public float rotationDamping = 3.0f; // Viteză de rotație a camerei
	public LayerMask obstacleMask; // Layer pentru obstacole

	private Vector3 lastPosition; // Poziția anterioară a jucătorului

	void LateUpdate()
	{
		if (!target)
			return;

		// Calculează înălțimea dorită a camerei
		float wantedHeight = target.position.y + height;

		// Calculează poziția dorită a camerei
		Vector3 wantedPosition = target.position - target.forward * distance;
		wantedPosition.y = wantedHeight;

		Vector3 currentPosition = Vector3.zero; // Initializare variabilă

		// Verifică dacă există obstacole între cameră și țintă
		RaycastHit hit;
		if (Physics.Linecast(target.position, wantedPosition, out hit, obstacleMask))
		{
			// Dacă există un obstacol, ajustează poziția camerei
			currentPosition = hit.point;
			currentPosition.y += height;
		}
		else
		{
			// Interpolare pentru înălțimea camerei
			float currentHeight = Mathf.Lerp(transform.position.y, wantedHeight, heightDamping * Time.deltaTime);

			// Interpolare pentru poziția camerei
			currentPosition = Vector3.Lerp(transform.position, wantedPosition, followDamping * Time.deltaTime);
			currentPosition.y = currentHeight;
		}

		// Aplică poziția camerei
		transform.position = currentPosition;

		// Rotație a camerei
		Quaternion currentRotation = Quaternion.LookRotation(target.position - transform.position);
		transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, rotationDamping * Time.deltaTime);

		// Actualizează poziția anterioară a jucătorului
		lastPosition = target.position;
	}
}