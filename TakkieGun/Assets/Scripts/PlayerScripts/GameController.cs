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
        patternManager.StartPattern(1);
    }

    public void PatternTwo()
    {
        patternManager.StartPattern(2);
    }

    public void PatternThree()
    {
        patternManager.StartPattern(3);
    }

    public void PatternFour()
    {
        patternManager.StartPattern(4);
    }
}
