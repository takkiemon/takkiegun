using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PatternManager patternManager;
    public string[] patternNames;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Set Pattern Names")]
    public void SetPatternNames()
    {
        patternNames = new string[] {
            "Pattern One",
            "Pattern Two",
            "Pattern Three",
            "Pattern Four"
        };
    }
    public void PatternOne()
    {
        StartCoroutine(patternManager.StartPattern(1));
    }

    public void PatternTwo()
    {
        StartCoroutine(patternManager.StartPattern(2));
    }

    public void PatternThree()
    {
        StartCoroutine(patternManager.StartPattern(3));
    }

    public void PatternFour()
    {
        StartCoroutine(patternManager.StartPattern(4));
    }
}
