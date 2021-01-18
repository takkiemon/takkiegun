using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject IntroPanel;
    public GameObject TutorialPanel001;
    public GameObject TutorialPanel002;
    public GameObject CreditsPanel;

    public GameObject startButton;
    public GameObject quitButton;
    public GameObject creditsButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        OpenIntroduction(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenIntroduction(bool opening)
    {
        if (opening)
        {
            IntroPanel.SetActive(true);
        }
        else
        {
            OpenTutorial001(true);
            IntroPanel.SetActive(false);
        }
    }

    public void OpenTutorial001(bool opening)
    {
        if (opening)
        {
            TutorialPanel001.SetActive(true);
        }
        else
        {
            TutorialPanel001.SetActive(false);
        }
    }

    public void OpenTutorial002(bool opening)
    {
        if (opening)
        {
            TutorialPanel002.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
            quitButton.SetActive(false);
            creditsButton.SetActive(false);
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
            TutorialPanel002.SetActive(false);
        }
    }

    public void OpenCredits(bool opening)
    {
        if (opening)
        {
            CreditsPanel.SetActive(true);
        }
        else
        {
            CreditsPanel.SetActive(false);
        }
    }
}
