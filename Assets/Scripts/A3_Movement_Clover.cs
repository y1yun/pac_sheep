using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using DG.Tweening;

public class A3_Movement_Clover : MonoBehaviour
{
    public Transform[] waypoints;
    //4 waypoints made in unity
    public float speed = 5f;
    public int currentWaypoint = 0;
    public Animator ani;

    private Vector3 startPos;
    private Vector3 endPos;
    private float distance;
    private float startTime;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        Move();
    }

    private void Move(){
        if (currentWaypoint < waypoints.Length)
        {
            startPos = transform.position;
            endPos = waypoints[currentWaypoint].position;
            distance = Vector3.Distance(startPos, endPos);
            startTime = Time.time;
            isMoving = true;

            if(currentWaypoint ==0)
            ani.SetTrigger("MoveRight");

            if(currentWaypoint ==1)
            ani.SetTrigger("MoveDown");

            if(currentWaypoint ==2)
            ani.SetTrigger("MoveLeft");

            if(currentWaypoint ==3)
            ani.SetTrigger("MoveUp");
        }
    }

    private void Update(){
            

        if (isMoving)
        {
            float distanceTraveled = (Time.time - startTime)* speed;
            float journeyFraction = distanceTraveled/distance;
            transform.position = Vector3.Lerp(startPos, endPos, journeyFraction);

            if (journeyFraction >= 1.0f)
            {
                isMoving = false;
                currentWaypoint++;

                if (currentWaypoint >= waypoints.Length)
                {
                    currentWaypoint = 0;
                }

                Move();
            }
        }
    }
}
//     private void Move(){
//         currentWaypoint = 0;

//         if (currentWaypoint < waypoints.Length)
//         {
//             Vector3 endPos = waypoints[currentWaypoint].position;
            
//             float distanceToEndPos = Vector3.Distance(transform.position, endPos);
//             float duration = distanceToEndPos/ speed;
        

//         tranform.DOMove(endPos, duration).OnComplete(() => {
//             currentWaypoint++;
//             if (currentWaypoint >= waypoints.Length){
//             currentWaypoint = 0;
//             }
//             Move();

//         });
//     }
// }


