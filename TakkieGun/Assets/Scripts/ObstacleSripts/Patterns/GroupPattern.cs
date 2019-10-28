using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupPattern : WallPattern
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCurrentPattern)
        {
            timePassed += Time.deltaTime;
            if (timePassed > waveDuration)
            {
                WaveIsDone();
            }
        }
    }
}
