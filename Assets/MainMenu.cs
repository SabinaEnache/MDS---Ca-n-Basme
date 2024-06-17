using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class MainMenu : MonoBehaviour
{
	public GameObject mainMenuCanvas;
	public GameObject healthBarCanvas; // Adaugă referința către canvas-ul barei de viață
	public Button playButton;
	public Button optionsButton;
	public Button quitButton;

	private bool isMainMenuActive = true;

	void Start()
	{
		// Asigură că meniul principal și bara de viață sunt active la începutul jocului
		mainMenuCanvas.SetActive(true);
		healthBarCanvas.SetActive(false); // Activează bara de viață

		// Atașează funcțiile butoanelor
		playButton.onClick.AddListener(PlayButtonClicked);
		optionsButton.onClick.AddListener(OptionsButtonClicked);
		quitButton.onClick.AddListener(QuitButtonClicked);
	}

	public void PlayButtonClicked()
	{
		// Ascunde meniul principal
		mainMenuCanvas.SetActive(false);
		// Dezactivează bara de viață când se afișează meniul principal
		healthBarCanvas.SetActive(true);
		// Aici poți adăuga logica pentru a începe jocul
		isMainMenuActive = false; // Actualizăm starea meniului principal
		UnityEngine.Debug.Log("Play button clicked. Main menu hidden.");
	}

	void OptionsButtonClicked()
	{
		// Aici poți adăuga logica pentru setările jocului
		UnityEngine.Debug.Log("Options button clicked.");
	}

	void QuitButtonClicked()
	{
		// Închide aplicația
		UnityEngine.Debug.Log("Quit button clicked. Application quitting.");
		UnityEngine.Application.Quit();
	}

	void Update()
	{
		// Verificăm dacă s-a apăsat tasta Escape
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			// Inversăm starea meniului principal
			isMainMenuActive = !isMainMenuActive;

			if (isMainMenuActive)
			{
				// Dacă meniul principal trebuie să fie activat, îl activăm și dezactivăm bara de viață
				mainMenuCanvas.SetActive(true);
				healthBarCanvas.SetActive(false); // Dezactivează bara de viață
				UnityEngine.Debug.Log("Escape pressed. Main menu shown.");
			}
			else
			{
				// Dacă meniul principal trebuie să fie dezactivat, îl dezactivăm și activăm bara de viață
				mainMenuCanvas.SetActive(false);
				healthBarCanvas.SetActive(true); // Activează bara de viață
				UnityEngine.Debug.Log("Escape pressed. Main menu hidden.");
			}
		}
	}
}