using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public WallPatternManager wallPatternManager;
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
        wallPatternManager.StartPattern(1);
    }

    public void PatternTwo()
    {

    }

    public void PatternThree()
    {

    }

    public void PatternFour()
    {

    }
}
