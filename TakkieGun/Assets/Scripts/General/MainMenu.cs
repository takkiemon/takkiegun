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
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
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
            TutorialPanel002.SetActive(false);
        }
    }
}
