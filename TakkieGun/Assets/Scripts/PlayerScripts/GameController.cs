using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public WallPatternManager wallPatternManager;
    public string[] patternNames = new string[5];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Set the stuff")]
    public void SetStuff()
    {
        Debug.Log("Perform operation");
    }
}
