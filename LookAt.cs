using System;
using System.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;
    public Transform golfBall;
    private Vector3 previousposition;
    private Vector3 prevdirection;
    public Transform cubistus;
    public Transform cubistus2;
    bool rotate = false;
    Touch first;
    Touch second;
    GameObject GPS;
    
    
    float sdif = 0f;
    private float distancepress;

    void Start(){
        GPS = GameObject.Find("GPS");
        
    }
    void Update(){
        
        var sp = GPS.GetComponent<GPS>();
        
        //camera rotate
            
            if(rotate == false && Input.touchCount ==1){
                previousposition = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
                rotate = true;
            }
            if (Input.touchCount==1 && rotate == true){
                Vector3 direction = previousposition-Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
                if(Vector3.Dot(prevdirection, direction)>0)
                sp.rotatearoundthismuch = direction.x*150f;
                else if(direction.x == 0 && Input.touchCount==1)
                sp.rotatearoundthismuch = 0;
                
                
                
                previousposition = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
                prevdirection = direction;
            }
            if(Input.touchCount==2){
                 first=  Input.GetTouch(0);
                 second=  Input.GetTouch(1);
                 rotate = false;
            }
            
            float differen = (first.position - second.position).magnitude;

        //camera zoom
            if (Input.GetAxis("Mouse ScrollWheel")> 0f || differen-sdif >0f){  
                if(sp.cameralerp<0)
                {
                    sp.cameralerp = 0;
                } 
                else
                {
                sp.cameralerp -= Time.deltaTime*0.1f;
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel")<0f || differen-sdif <0f){
               if(sp.cameralerp>1)
                {
                    sp.cameralerp = 1;
                } 
                else
                {
                sp.cameralerp += Time.deltaTime*0.1f;

                }
            }

           sdif = (first.position-second.position).magnitude; 

        if(Input.touchCount==0){
        rotate = false;   
        }
        
        
    }
}
