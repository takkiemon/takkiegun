using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float thrustingForce;
    public Vector3 gravityVector;
    public float maxLives;
    public float currentLives;

    public Jet[] thrusters;

    public Text lifeText;

    public List<Rigidbody> collidingBodies;
    public PatternManager patternManager;
    public ParticleSystem explosions;
    public int particleCount;
    public AudioSource damageSFX;
    public CameraController cameraObject;
    public float shakeDuration, shakeMagnitude;

    private bool isInvincible;
    private float invincibleTimer;
    public float invincibleDuration;
    public float flashDelay;
    public Renderer[] gameMeshes;
    public SpriteRenderer hatSprite;
    public Image fadeToBlackScreen;
    public Color invincibleColor;
    public Color[] normalColor;

    private bool gameIsPaused;
    public GameObject pauseScreen;
    public GameObject pauseAreYouSureScreen;
    private bool gameIsFading;

    // Start is called before the first frame update
    void Start()
    {
        ApplySettings();
        thrustersSetup();
        UpdateLifeText();

        collidingBodies = new List<Rigidbody>();
        gravityVector = new Vector3(0, 0, 0);
        invincibleTimer = invincibleDuration;
        normalColor = new Color[gameMeshes.Length];
        for (int i = 0; i < gameMeshes.Length; i++)
        {
            normalColor[i] = gameMeshes[i].material.color;
        }
        isInvincible = false;
        gameIsPaused = false;
        gameIsFading = false;
    }

    public void thrustersSetup()
    {
        thrusters[0].inputName = "Move Down"; // red
        thrusters[1].inputName = "Move Up"; // green
        thrusters[2].inputName = "Move Left"; // blue
        thrusters[3].inputName = "Move Right"; // yellow

        SetAxisThrusters();
    }

    [ContextMenu("Apply new values")]
    public void ApplySettings()
    {
        SetAxisThrusters();
        Physics.gravity = gravityVector;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (gameIsPaused)
            {
                PauseGame(false);
            }
            else
            {
                PauseGame(true);
            }
        }
    }

    public void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
    }

    public void PauseGame(bool gameIsSetToPaused)
    {
        gameIsPaused = gameIsSetToPaused;
        foreach (Jet jet in thrusters)
        {
            jet.gameIsPaused = gameIsSetToPaused;
        }
        if (gameIsPaused)
        {
            SetTimeScale(0f);
        }
        else
        {
            SetTimeScale(1f);
        }
        pauseScreen.SetActive(gameIsSetToPaused);
    }

    public void LeaveWarning()
    {
        pauseAreYouSureScreen.SetActive(true);
    }

    public void CancelLeaving()
    {
        pauseAreYouSureScreen.SetActive(false);
    }

    public void GoToMainMenu()
    {
        pauseAreYouSureScreen.SetActive(false);
        pauseScreen.SetActive(false);
        PauseGame(false);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public IEnumerator StartInvincibility()
    {
        isInvincible = true;
        invincibleTimer = invincibleDuration;
        float tempAlpha = hatSprite.color.a;
        while (invincibleTimer >= 0)
        {
            for (int i = 0; i < gameMeshes.Length; i++)
            {
                gameMeshes[i].material.color = invincibleColor;
            }
            hatSprite.color = new Color(hatSprite.color.r, hatSprite.color.g, hatSprite.color.b, 0f);
            yield return new WaitForSeconds(flashDelay);
            for (int i = 0; i < gameMeshes.Length; i++)
            {
                gameMeshes[i].material.color = normalColor[i];
            }
            hatSprite.color = new Color(hatSprite.color.r, hatSprite.color.g, hatSprite.color.b, tempAlpha);
            yield return new WaitForSeconds(flashDelay);

            invincibleTimer -= Time.deltaTime + flashDelay * 2;
        }
        isInvincible = false;
    }

    public IEnumerator DyingAnimation()
    {
        patternManager.StopAllWaves();
        isInvincible = true;
        int explosionAmount = 5;
        for (int i = 0; i < explosionAmount; i++)
        {
            /*
            if (i >= explosionAmount - 1 && !gameIsFading)
            {
                StartCoroutine(FadeScreen(1.2f, true));
            }
            */
            Time.timeScale = (float)(explosionAmount - i) / (float)explosionAmount;
            explosions.Emit(particleCount);
            damageSFX.Play();
            StartCoroutine(cameraObject.Shake(shakeDuration, shakeMagnitude));
            explosions.Emit(particleCount);
            yield return new WaitForSeconds(.4f);
        }
        Time.timeScale = 1f;
        isInvincible = false;
        currentLives = maxLives;
        // insert better transition
        patternManager.StopAllWaves();
        UpdateLifeText();
        //patternManager.StartPattern(patternManager.levelNumber);
        fadeToBlackScreen.color = new Color(0f, 0f, 0f, 0f);
    }

    public IEnumerator FadeScreen(float secondsToFade, bool fadeToBlack)
    {
        gameIsFading = true;
        float fadeTimer = 0f;
        float fadeDirection; // this determines whether the alpha value goes up or down, depending on whether it fades into or out of black
        if (fadeToBlack)
        {
            fadeDirection = 1f;
            fadeToBlackScreen.color = new Color(0f, 0f, 0f, 0f);
        }
        else // if it fades from black into the screen
        {
            fadeDirection = -1f;
            fadeToBlackScreen.color = new Color(0f, 0f, 0f, 255f);
        }

        while (fadeTimer < secondsToFade)
        {
            Debug.Log("fadeTimer: \t(" + fadeTimer + "/" + secondsToFade + ").");
            fadeTimer += secondsToFade / 10f;
            fadeToBlackScreen.color = new Color(0f, 0f, 0f, fadeTimer / secondsToFade );
            yield return new WaitForSeconds(secondsToFade / 10f);
        }
        gameIsFading = false;
    }

    public void UpdateLifeText()
    {
        lifeText.text = "Lives: " + currentLives + "/" + maxLives;
        if (currentLives >= maxLives)
        {
            lifeText.color = Color.green;
        }
        if (currentLives < maxLives)
        {
            lifeText.color = Color.yellow;
        }
        if (currentLives * 3 <= maxLives)
        {
            lifeText.color = Color.red;
        }
    }

    public void SetAxisThrusters()
    {
        for (int i = 0; i < thrusters.Length; i++)
        {
            Jet tempThruster = thrusters[i];
            if (tempThruster != null)
            {
                //PlaceAxisThrusters(tempThruster, i);
                tempThruster.thrustForce = thrustingForce;
            }
        }
    }

    public void SetDiagonalThrusters()
    {
        for (int i = 0; i < thrusters.Length; i++)
        {
            Jet tempThruster = thrusters[i];
            if (tempThruster != null)
            {
                tempThruster.thrustForce = thrustingForce;
            }
        }
    }

    public void OnHit(Collider killerObject)
    {
        if (!isInvincible && killerObject.gameObject.GetComponentInParent<MovingWallBehavior>() && collidingBodies.Count >= 2)
        {
            currentLives--;
            explosions.Emit(particleCount);
            damageSFX.Play();
            StartCoroutine(cameraObject.Shake(shakeDuration, shakeMagnitude));
            StartCoroutine(StartInvincibility());
            if (currentLives <= 0)
            {
                StartCoroutine(DyingAnimation());
            }
            // maybe add a foreach() where I can make it debug something along the lines of "you were hit by object A and object B and object C." etc. where all the colliders that are stored in the list will be spelled out.
            UpdateLifeText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collidingBodies.Add(collision.rigidbody);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collidingBodies.Contains(collision.rigidbody))
        {
            collidingBodies.Remove(collision.rigidbody);
        }
    }
}
