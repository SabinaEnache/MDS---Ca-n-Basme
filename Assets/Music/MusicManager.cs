using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
public class MusicManager : MonoBehaviour
{
	public AudioSource tavernMusic;
	public AudioSource battleMusic;
	public AudioSource dragonMusic;
	public AudioSource draculaMusic;
	public AudioSource princessMusic;
	public AudioSource backgroundMusic;

	private AudioSource currentMusic;
	private float transitionDuration = 2.0f; // Durata de tranzitie dintre melodii

	void Start()
	{
		tavernMusic.volume = 1;
		battleMusic.volume = 1;
		dragonMusic.volume = 1;
		draculaMusic.volume = 1;
		princessMusic.volume = 1;
		backgroundMusic.volume = 1;

		PlayBackgroundMusic(); //Pentru ca incepem din taverna, punem direct muzica pentru aceasta in loc de backgroundMusic
	}

	public void PlayTavernMusic()
	{
		//UnityEngine.Debug.Log("Entering PlayTavernMusic");
		if (currentMusic != null && currentMusic != tavernMusic)
		{
			//UnityEngine.Debug.Log("Stopping current music in PlayTavernMusic");
			StartCoroutine(Crossfade(currentMusic, tavernMusic));
		}
		//else
		//{
		//	UnityEngine.Debug.Log("currentMusic is null in PlayTavernMusic");
		//}
		tavernMusic.Play();
		currentMusic = tavernMusic;
		//UnityEngine.Debug.Log("Tavern music started, currentMusic set to tavernMusic");
	}

	public void PlayBattleMusic()
	{
		//UnityEngine.Debug.Log("Entering PlayBattleMusic");
		if (currentMusic != null)
		{
			//UnityEngine.Debug.Log("Stopping current music in PlayBattleMusic");
			StartCoroutine(Crossfade(currentMusic, battleMusic));
		}
		else
		{
			//UnityEngine.Debug.Log("currentMusic is null in PlayBattleMusic");
			battleMusic.Play();
			currentMusic = battleMusic;
		}
		battleMusic.Play();
		currentMusic = battleMusic;
		//UnityEngine.Debug.Log("Battle music started, currentMusic set to battleMusic");
	}

	public void PlayDragonMusic()
	{
		if (currentMusic != null && currentMusic != dragonMusic)
		{
			StartCoroutine(Crossfade(currentMusic, dragonMusic));
		}
		else
		{
			dragonMusic.Play();
			currentMusic = dragonMusic;
		}
	}
	public void PlayDraculaMusic()
	{
		if (currentMusic != null && currentMusic != draculaMusic)
		{
			StartCoroutine(Crossfade(currentMusic, draculaMusic));
		}
		else
		{
			draculaMusic.Play();
			currentMusic = draculaMusic;
		}
	}
	public void PlayBackgroundMusic()
	{
		if (currentMusic != null && currentMusic != backgroundMusic)
		{
			StartCoroutine(Crossfade(currentMusic, backgroundMusic));
		}
		else
		{
			backgroundMusic.Play();
			currentMusic = backgroundMusic;
		}
	}
	public void PlayPrincessMusic()
	{
		if (currentMusic != null && currentMusic != princessMusic)
		{
			StartCoroutine(Crossfade(currentMusic, princessMusic));
		}
		else
		{
			princessMusic.Play();
			currentMusic = princessMusic;
		}
	}
	private IEnumerator Crossfade(AudioSource from, AudioSource to)
    {
        float time = 0;
        to.volume = 0;
        to.Play();

        while (time < transitionDuration)
        {
            time += Time.deltaTime;
            from.volume = Mathf.Lerp(1, 0, time / transitionDuration);
            to.volume = Mathf.Lerp(0, 1, time / transitionDuration);
            yield return null;
        }

        from.Stop();
        from.volume = 1; // Reset volume for the next use
        currentMusic = to;
    }
}