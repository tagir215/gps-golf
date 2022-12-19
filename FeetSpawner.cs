


using System.ComponentModel;
using System.Collections.Specialized;
using System;
using System.Threading;
using System.Runtime;
using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject followTarget;
    [SerializeField] private float speed=10f;
    [SerializeField] private GameObject leftFoot;
    [SerializeField] private GameObject rightFoot;
    Vector3 lookAtPos = new Vector3(0,0,0);
    float time = 0.5f;
    float speedOfTimeItself =1f;
    bool left;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 targetPos = followTarget.transform.position;
        float distance = Vector3.Distance(pos,targetPos); 
        if(distance<200){
            if(distance>0.2f){    
                transform.position = Vector3.MoveTowards(pos,targetPos,speed*Time.deltaTime * distance);
            }
        }
        else
            transform.position = targetPos;

        transform.rotation = followTarget.transform.rotation;
        Quaternion q = Quaternion.FromToRotation(transform.up,(targetPos-pos));
        transform.rotation = q * transform.rotation;
        if(distance<=0.2f){
            speedOfTimeItself = 0.3f;
        }
        else{
            speedOfTimeItself = 1f;
        }

        if(itsTime()){
            Vector3 fdirection = new Vector3(0,0,0)- pos;  
            GameObject o = null;
            if(left){
                o = GameObject.Instantiate(leftFoot,pos,transform.rotation);
                left = false;
            }
            else{
                o = GameObject.Instantiate(rightFoot,pos,transform.rotation);
                left = true;
            }
            o.AddComponent<FootLogic>();

        }
    }

    bool itsTime(){
        time -= 1*Time.deltaTime *speedOfTimeItself;
        if(time<0){
            time = 0.3f;
            return true;
        }    
        else return false;
    }
}
