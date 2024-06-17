using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
	public enum MusicType { Tavern, Battle, Dragon, Dracula, Background, Princess }
	public MusicType musicType;

	private MusicManager musicManager;

	void Start()
	{
		musicManager = FindObjectOfType<MusicManager>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			UnityEngine.Debug.Log("Player entered trigger zone: " + musicType);
			if (musicType == MusicType.Tavern)
			{
				musicManager.PlayTavernMusic();
			}
			else if (musicType == MusicType.Battle)
			{
				musicManager.PlayBattleMusic();
			}
			else if (musicType == MusicType.Dragon)
			{
				musicManager.PlayDragonMusic();
			}
			else if (musicType == MusicType.Dracula)
			{
				musicManager.PlayDraculaMusic();
			}
			else if (musicType == MusicType.Princess)
			{
				musicManager.PlayPrincessMusic();
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			UnityEngine.Debug.Log("Player exited trigger zone: " + musicType);
			musicManager.PlayBackgroundMusic();
		}
	}
}
