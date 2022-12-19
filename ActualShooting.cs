using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActualShooting : MonoBehaviour, IPointerDownHandler
{
    public Transform shotguide;
    public Transform golfBall;
    public GameObject slider;
    public GameObject arrow1;
    public GameObject arrow2;
    private int arrowminus = 1;
    private bool arrowcenterer = true;
    public Transform pointerholder;
    public int ShootingNow = 0;
    Vector3 direction;
    Vector3 arrow2direction = new Vector3(0,0,0);
    float slidepower = 0f;
    // Start is called before the first frame update
    public void OnPointerDown(PointerEventData eventData)
    {
        ShootingNow++;
        if(ShootingNow == 2){
            slider.SetActive(true);
            GetComponent<RawImage>().enabled = false;
            
        }
    }

    // Update is called once per frame
    void Update()
    {

        direction = shotguide.position-GameObject.Find("GPS").transform.position;
        if(ShootingNow == 2){
            var slid =  slider.GetComponent<Slider>();
            arrow1.SetActive(false);
            arrow2.SetActive(true);
            float arrowspeed = slidepower * 0.05f;
            float maxangle = slidepower * 0.01f * 60f;
            Vector3 arrow1direction = pointerholder.position-arrow1.transform.position;
            arrow2direction = pointerholder.position-arrow2.transform.position;
            float arrowAngle = Vector3.Angle(arrow1direction,arrow2direction);
            
            pointerholder.Rotate(new Vector3(0,0,arrowspeed*arrowminus),Space.Self);
            
            if(arrowAngle<maxangle)
                arrowcenterer = true;

            if(arrowAngle>maxangle && arrowcenterer == true){
                arrowminus = arrowminus * -1;
                arrowcenterer = false;
            }
            
            slidepower = slid.value;

        }
        else if(ShootingNow == 1){
            var slid =  slider.GetComponent<Slider>();
            arrow1.SetActive(true);
            arrow2.SetActive(false);
        }

        if(Input.touchCount==0 && Input.GetMouseButton(0)==false && slidepower != 0 && ShootingNow >= 2){
            var rigb = golfBall.GetComponent<Rigidbody>();
            rigb.AddForce(-arrow2direction.normalized*slidepower*5); 
            ShootingNow = 0;
            arrow1.SetActive(false);
            arrow2.SetActive(false);
        }

        if(ShootingNow == 0 && slidepower>0){
            
            var slid =  slider.GetComponent<Slider>();
            slid.value -= 200f*Time.deltaTime;
            if(slid.value <= 0){
                slidepower = 0;
                slider.SetActive(false);
                GetComponent<RawImage>().enabled = true;

            }
        }

    }
}
