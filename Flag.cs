using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Flag : MonoBehaviour
{
    public GameObject wayme;
    private GameObject mywaypoint;
    public Transform waypoint;
    Vector3 direction;
    GameObject child;
    // Start is called before the first frame update
    void Awake(){
        direction =(transform.position - new Vector3(0,0,0).normalized);
        child = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        
    }
    private void OnTriggerEnter(Collider other) {
        var sp =GameObject.Find("Golf Ball").GetComponent<GolfBallMoving>();
        if(sp.rigb.velocity.magnitude <0.5f)
        {
        sp.holes++;
        gameObject.SetActive(false);
        
        }
    }
    private void setAlpha(float distance){
        float alpha = 100f / distance -0.1f;
        child.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 0, 0,alpha);
    }

    private void Update() {
        float distance = Vector3.Distance(wayme.transform.position,transform.position);
        setAlpha(distance);
        Vector3 campos = Camera.main.transform.position;
        waypoint.LookAt(campos,direction);   

    }
}
