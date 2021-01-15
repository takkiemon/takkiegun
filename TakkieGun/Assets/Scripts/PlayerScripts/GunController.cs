using System.Collections;
using System.Collections.Generic;
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
    public Color invincibleColor;
    public Color[] normalColor;

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

    }

    public IEnumerator StartInvincibility()
    {
        isInvincible = true;
        invincibleTimer = invincibleDuration;

        while (invincibleTimer >= 0)
        {
            Debug.Log("invincible: " + invincibleTimer);
            for (int i = 0; i < gameMeshes.Length; i++)
            {
                gameMeshes[i].material.color = invincibleColor;
            }
            yield return new WaitForSeconds(flashDelay);
            for (int i = 0; i < gameMeshes.Length; i++)
            {
                gameMeshes[i].material.color = normalColor[i];
            }
            yield return new WaitForSeconds(flashDelay);

            invincibleTimer -= Time.deltaTime + flashDelay * 2;
        }
        isInvincible = false;
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
                currentLives = maxLives;
                patternManager.StartPattern(patternManager.levelNumber);
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
