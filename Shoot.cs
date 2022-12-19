using System.Threading;
using System.Net.Mime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Shoot : MonoBehaviour
{
    public Transform Goflball;
    public Transform golfballroadcrossposition;
    public Transform GPS;

   public GameObject golfsummon;
   public GameObject shootmode;
   Vector3 oldGOlfBaLLPositionfrinde;
   Slider sliderz;
   GameObject old;
   Rigidbody Golfrigb;
   private void Start() {
       sliderz = GameObject.Find("Canvas").transform.Find("Slider").GetComponent<Slider>();
       Golfrigb = Goflball.GetComponent<Rigidbody>();

       
   }
    void Update() {
        
        float slidepower = sliderz.value;
        if((Goflball.position-GPS.position).magnitude<2f || (golfballroadcrossposition.position-GPS.position).magnitude<2f){
            if(slidepower ==0 && Golfrigb.velocity.magnitude<0.1f){
                golfsummon.SetActive(false);
                shootmode.SetActive(true);
            }
        }
        if((Goflball.position-GPS.position).magnitude>=2f && (golfballroadcrossposition.position-GPS.position).magnitude>=2f){
            if(slidepower ==0){
                golfsummon.SetActive(true);
                shootmode.SetActive(false);
            }
        }
        if(Golfrigb.velocity.magnitude>0.1f){
            if(slidepower ==0)
            {
            golfsummon.SetActive(true);
            shootmode.SetActive(false);
            }
        }




        oldGOlfBaLLPositionfrinde = Goflball.position;
    }
    
    
   
        
    
    
}






