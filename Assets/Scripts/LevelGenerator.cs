using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject tileMap;
    public GameObject outsideCorner;
    public GameObject outsideWall;
    public GameObject insideCornor;
    public GameObject insideWall;
    public GameObject standardPellet;
    public GameObject powerPellet;
    public GameObject tJunction;


    int[,] levelMap =
 {
 {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
 {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
 {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
 {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
 {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
 {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
 {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
 {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
 {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
 {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
 {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
 {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
 {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
 {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
 {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
 };

    
    // Start is called before the first frame update
    void Start()
    {
        tileMap.SetActive(false);

        Generate();
    }

    void Generate()
    {
        //levelMap.GetLength(0) gives you the length (the number of rows)
        //levelMap.GetLength(1) gives you the length (the number of columns) 
        for (int col =0; col < levelMap.GetLength(1); col++)
        {
            for (int row=0; row < levelMap.GetLength(0); row++)
            {
                int tileType = levelMap[row, col];
                Vector3 tilePosition = new Vector3(col, -row, 0);

                GameObject tileAsset = ChooseType(tileType);

                if (tileAsset != null) //avoid case 0
                Instantiate(tileAsset, tilePosition, Quaternion.identity);
                //Quaternion.identity indicates no initial rotation
            }
        }
    }

    GameObject ChooseType(int tileType)
    {
// 0 – Empty (do not put anything here, no sprite needed)
// 1 - Outside corner (double lined corner piece in original game)
// 2 - Outside wall (double line in original game)
// 3 - Inside corner (single lined corner piece in original game)
// 4 - Inside wall (single line in original game)
// 5 – An empty space with a Standard pellet (see Visual Assets above)
// 6 – An empty space with a Power pellet (see Visual Assets above)
// 7 - A t junction piece for connecting with adjoining regions
        switch(tileType)
        {
            case 0:
            return null;

            case 1:
            return outsideCorner;

            case 2:
            return outsideWall;

            case 3:
            return insideCornor;

            case 4:
            return insideWall;

            case 5:
            return standardPellet;

            case 6:
            return powerPellet;

            case 7: 
            return tJunction;

            default:
            return null; //avoid null exceptions

        }
    }
}
