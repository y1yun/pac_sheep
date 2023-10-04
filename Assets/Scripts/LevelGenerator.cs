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
        GameObject topLeft = new GameObject("TopLeft_Generated");
        GameObject topRight = new GameObject("TopRight_Generated");
        // GameObject topLeft = new GameObject("TopLeft_Generated");


        //levelMap.GetLength(0) gives you the length (the number of rows)
        //levelMap.GetLength(1) gives you the length (the number of columns) 
        for (int col =0; col < levelMap.GetLength(1); col++)
        {
            for (int row=0; row < levelMap.GetLength(0); row++)
            {
                int tileType = levelMap[row, col];
                Vector3 tilePosition = new Vector3(col-8.5f, -row+4.5f, 0);

                GameObject tileAsset = ChooseType(tileType);

                if (tileAsset != null) //avoid case 0
                {
                Quaternion tileRotation = DecideRotation(tileType, col, row);
                GameObject generated = Instantiate(tileAsset, tilePosition, tileRotation);
                generated.transform.SetParent(topLeft.transform);
                }
                //Quaternion.identity indicates no initial rotation
            }

        }
        GameObject generatedTopRight = Instantiate(topLeft);
        generatedTopRight.transform.SetParent(topRight.transform);
        generatedTopRight.transform.localScale = new Vector3(-1, 1, 1);
        generatedTopRight.transform.position = new Vector3(10,0,0);
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

    Quaternion DecideRotation(int tileType, int col, int row)
    {
        Quaternion tileRotation = Quaternion.identity;
        int right = -1;
        int left = -1;
        int above = -1;
        int under = -1;
        //-1 means does not exist

        if (col < levelMap.GetLength(1)-1)
        right = levelMap[row, col+1];
//length -1 cuz col/row starts from 0
        if (col > 0)
        left = levelMap[row, col-1];

        if (row > 0)
        above = levelMap[row-1, col];

        if (row < levelMap.GetLength(0)-1)
        under = levelMap[row+1, col];

        //empty
        // if (tileType == 0){
        //     tileRotation = Quaternion.identity;
        // }
        //outsideCorner
        if (tileType == 1){
            if (above ==2 && right == 2)
            tileRotation = Quaternion.Euler(0,0,90);

            if (left == 2 && under ==2)
            tileRotation = Quaternion.Euler(0,0,270);

            if (above == 2 && left == 2)
            tileRotation = Quaternion.Euler(0,0,180);
        }
        //oursideWall
        if (tileType == 2){
            if (left == 1 || left == 2 || right == 2|| right == 1)
            tileRotation = Quaternion.Euler(0,0,90);
        }
        //insideCorner
        if (tileType == 3){
            if (above ==4 && right == 4 && left != 4)
            tileRotation = Quaternion.Euler(0,0,90);

            if (left == 4 && under ==4 && right != 4)
            tileRotation = Quaternion.Euler(0,0,270);

            if (above == 4 && left == 4 && right != 4)
            tileRotation = Quaternion.Euler(0,0,180);

            if (above == 3 && right == 4 && under != 4)
            tileRotation = Quaternion.Euler(0,0,90);

            if (above == 3 && left == 4 && right != 4)
            tileRotation = Quaternion.Euler(0,0,180);

            if (under == 3 && left == 4 && right != 4 )
            tileRotation = Quaternion.Euler(0,0,270);

            if (left == 3 && under == 4)
            tileRotation = Quaternion.Euler(0,0,270);

            if (above == 4 && right == 3)
            tileRotation = Quaternion.Euler(0,0,90);

            if (left == 3 && above == 4)
            tileRotation = Quaternion.Euler(0,0,180); 

            if (left == 0 && under == 0)   
            tileRotation = Quaternion.Euler(0,0,90); 
            
            if (left == 5 && under == 5 && right == -1 && above == 4)   
            tileRotation = Quaternion.Euler(0,0,90); 

            if (left == 4 && under == 4 && right == -1 )
            tileRotation = Quaternion.Euler(0,0,270); 

            if (left == 4 && right == 4 && above == 4 && under == 3)
            tileRotation = Quaternion.Euler(0,0,90);
            
            // if (left == 4 && right == 4 && above == 3 && under == 4)
            // tileRotation = Quaternion.Euler(0,0,0);
        
        }
        //insideWall
        if (tileType == 4){
            if (left == 3 && right == 3 )
            tileRotation = Quaternion.Euler(0,0,90);

            if (left == 3 && right == 4 && under == 4)
            tileRotation = Quaternion.Euler(0,0,90);

            if (left == 4 && right == 3 && under == 4)
            tileRotation = Quaternion.Euler(0,0,90);

            if (left == 3 && right == 4 && above == 4)
            tileRotation = Quaternion.Euler(0,0,90);

            if (left == 4 && right == 3 && above == 4)
            tileRotation = Quaternion.Euler(0,0,90);

            if (left == 3 && right == 4 && under == 0)
            tileRotation = Quaternion.Euler(0,0,90);

            if (left == 4 && right == 3 && under == 0)
            tileRotation = Quaternion.Euler(0,0,90);

            if (left == 3 && right == 4 && above == 0)
            tileRotation = Quaternion.Euler(0,0,90);

            if (left == 4 && right == 3 && above == 0)
            tileRotation = Quaternion.Euler(0,0,90);

            if (left == 4 && right == 4 && under == 0)
            tileRotation = Quaternion.Euler(0,0,90);

            if (left == 4 && right == 4 && above == 0)
            tileRotation = Quaternion.Euler(0,0,90);

            if (left == 4 && right == 4 && under == 4)
            tileRotation = Quaternion.Euler(0,0,90);

            if (above == 0 && right ==0)
            tileRotation = Quaternion.Euler(0,0,90);

            if (left == 4 && under == 3 && above == 5 && right == -1)
            tileRotation = Quaternion.Euler(0,0,90);
        }
        // //standardPellet
        // if (tileType == 5){
        //     tileRotation = Quaternion.identity;
        // }
        // //PowerPellet
        // if (tileType == 6){
        //     tileRotation = Quaternion.identity;
        // }
        // //TJunction
        // if (tileType == 7){
        //     tileRotation = Quaternion.identity;
        // }

        return tileRotation;

    }
}
