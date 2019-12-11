using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SimpleFileBrowser;
public class LevelSelectScreen : MonoBehaviour
{
   public Text titleText;
   public Button startButton;
   public Button optionsButton;
   public Button quitButton;
	public Button selectMusicButton;
   public Toggle bcMode;
   public Button backButton;
   public Text startText, optionText, quitText, backText;

   int currentMenu = 0;

   void Start()
   {
      quitButton.onClick.AddListener(QuitButton);
      optionsButton.onClick.AddListener(OptionMenu);
      startButton.onClick.AddListener(StartGame);
      bcMode.onValueChanged.AddListener((value)=>BCMode(value));
		selectMusicButton.onClick.AddListener(SelectMusic);
      startButton.gameObject.SetActive(false);
      quitButton.gameObject.SetActive(false);
      optionsButton.gameObject.SetActive(false);
      backButton.onClick.AddListener(BackToMainMenu);
		LoadFileFilters();
      if (PlayerPrefs.GetInt("isFirstTime") != 1)
      {
         PlayerPrefs.SetString("Music", "soundtrack");
         PlayerPrefs.SetInt("isFirstTime", 1);
      }
   }
	void LoadFileFilters()
	{
		FileBrowser.SetFilters(true, new FileBrowser.Filter("Files", ".mp3"));
		FileBrowser.SetDefaultFilter(".mp3");
	}
	// Update is called once per frame
	void Update()
   {
      if(Input.anyKey && currentMenu == 0)
      {
         StartCoroutine(fadeOutText());
         currentMenu = 1;
      }
   }

   IEnumerator fadeOutText()
   {
      titleText.CrossFadeAlpha(0f, 0.5f, false);
      yield return new WaitForSeconds(0.7f);
      startButton.gameObject.SetActive(true);
      quitButton.gameObject.SetActive(true);
      optionsButton.gameObject.SetActive(true);

      startButton.image.CrossFadeAlpha(1f, 0.5f, false);
      optionsButton.image.CrossFadeAlpha(1f, 0.5f, false);
      quitButton.image.CrossFadeAlpha(1f, 0.5f, false);

      startText.CrossFadeAlpha(1f, 0.7f, false);
      optionText.CrossFadeAlpha(1f, 0.7f, false);
      quitText.CrossFadeAlpha(1f, 0.7f, false);
   }

   void StartGame()
   {
      SceneManager.LoadScene("SampleScene");
   }

   void OptionMenu()
   {
      startButton.image.CrossFadeAlpha(0f, 0.7f, false);
      optionsButton.image.CrossFadeAlpha(0f, 0.7f, false);
      quitButton.image.CrossFadeAlpha(0f, 0.7f, false);

      startText.CrossFadeAlpha(0f, 0.5f, false);
      optionText.CrossFadeAlpha(0f, 0.5f, false);
      quitText.CrossFadeAlpha(0f, 0.5f, false);

      startButton.gameObject.SetActive(false);
      quitButton.gameObject.SetActive(false);
      optionsButton.gameObject.SetActive(false);

      bcMode.gameObject.SetActive(true);
      backButton.gameObject.SetActive(true);
		selectMusicButton.gameObject.SetActive(true);
	}

	void BCMode(bool value)
   {
      // Set bc mode here
   }

   void QuitButton()
   {
      Application.Quit();
   }

   void BackToMainMenu()
   {
      bcMode.gameObject.SetActive(false);
      backButton.gameObject.SetActive(false);
		selectMusicButton.gameObject.SetActive(false);

      startButton.gameObject.SetActive(true);
      quitButton.gameObject.SetActive(true);
      optionsButton.gameObject.SetActive(true);

      startButton.image.CrossFadeAlpha(1f, 0.5f, false);
      optionsButton.image.CrossFadeAlpha(1f, 0.5f, false);
      quitButton.image.CrossFadeAlpha(1f, 0.5f, false);

      startText.CrossFadeAlpha(1f, 0.7f, false);
      optionText.CrossFadeAlpha(1f, 0.7f, false);
      quitText.CrossFadeAlpha(1f, 0.7f, false);
   }
	void SelectMusic()
	{
		string defaultPath = Application.dataPath + "/Resources";
		string a;
		FileBrowser.ShowLoadDialog( (path) => { a = FileBrowserHelpers.GetFilename(path); a = a.Substring(0, a.Length - 4);  Debug.Log(a); PlayerPrefs.SetString("Music", a); }, 
		                                () => { Debug.Log( "Canceled" ); }, 
		                                false, defaultPath, "Select Music", "Select" );
	}
}
