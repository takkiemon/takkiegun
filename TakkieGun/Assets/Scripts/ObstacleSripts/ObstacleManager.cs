using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public BoundaryWallBehavior boundaryWallsObject;
    public PatternManager wallPatternManager;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < boundaryWallsObject.walls.Length; i++)
        {
            for (int j = 0; j < wallPatternManager.movingWalls.Length; j++)
            {
                Physics.IgnoreCollision(
                    boundaryWallsObject.walls[i].GetComponent<Collider>(), 
                    wallPatternManager.movingWalls[j].wallPartitions[0].GetComponent<Collider>()
                    );
                Physics.IgnoreCollision(
                    boundaryWallsObject.walls[i].GetComponent<Collider>(), 
                    wallPatternManager.movingWalls[j].wallPartitions[1].GetComponent<Collider>()
                    );
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
