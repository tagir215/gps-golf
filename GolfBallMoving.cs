using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolfBallMoving : MonoBehaviour
{
    Vector3 point1;
    Vector3 point0;
    public Transform cast2;
    public GameObject golfballcrossobjectmaster;
    public UnityEngine.UI.Text holesman;
    public int holes = 0;
    float sphereradius = 0;
    int oldholes =0;

    bool foundhitpoint =  false;

    public float speed= 0;

    Vector3 oldposition;
    public Vector3 direction;
    Vector3 actualdirection;
    Vector3 down;
    Vector3 cross;
    Vector3 hitpoint;
    RaycastHit raycastHitsphere;
    RaycastHit raycastHit0;
    RaycastHit raycastHit1;

    public Transform left;
    public Transform right;
    public Transform center;
    public Rigidbody rigb;

    private void Start() {
        rigb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        
        direction = transform.position-oldposition;
        down = transform.position - new Vector3(0,0,0);
        speed = direction.magnitude/Time.deltaTime;

        
        if(Vector3.Distance(direction,new Vector3(0,0,0))>0.05f){
            transform.LookAt(direction.normalized,down);
            actualdirection = direction.normalized;
        }
       
        
        
        if(holes > oldholes){
            holesman.text ="Score: "+ holes.ToString();
            oldholes = holes;
        }

        if(rigb.velocity.magnitude<=0.2f && foundhitpoint == false)
            sphereCast();
        
        if(rigb.velocity.magnitude > 0.2){
            foundhitpoint = false;
            golfballcrossobjectmaster.SetActive(false);
            sphereradius = 0;
        }
        
        oldposition = transform.position;
        
    }

    void sphereCast()
    {
        if(Physics.SphereCast(transform.position+down.normalized*10f,sphereradius,-down,out raycastHitsphere,20F,1<<0)){
            hitpoint = raycastHitsphere.point;
            golfballcrossobjectmaster.transform.position= hitpoint;
            golfballcrossobjectmaster.transform.rotation = Quaternion.LookRotation(down);
            golfballcrossobjectmaster.SetActive(true);
            sphereradius = 0;
            foundhitpoint = true;
        }
        else
            sphereradius += 0.1f;
        

    }


}
