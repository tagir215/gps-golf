using System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPS : MonoBehaviour
{
    public float speed = 7;
    public bool connected = false;
    
    private float latitude;
    private float longitude;
    private Vector3 prevpos;
    float prevlat;
    float prevlon;
    public int spaghettifinger = 0;
    public float rotatearoundthismuch;
    public float cameralerp = 0;
    public UnityEngine.UI.Text cord;
    public Transform camGuide;
    public Transform camGuide1;
    public Transform camGuide2;
    public Transform globe;
    public Transform cameraposition;
    private Camera cam;
    public GameObject golfBall;
    GameObject actualshot;
    Vector2 OLDPOSITIONFINGER;
    float cameralerpersmootherexpereiencer;


    private float dx;
    public GameObject spawnWay;
    private Rigidbody rigb;
    void Start()
    {
        cam = Camera.main;

        float maxwait = 10f;
        Input.location.Start();
        while(Input.location.status == LocationServiceStatus.Initializing && maxwait > 0){
            maxwait -= Time.deltaTime;
        }
        if(maxwait<=0){
            connected = false;
        }
        if(Input.location.status == LocationServiceStatus.Failed){
            connected = false;
        }
        else{
            Debug.Log("GPS connected");
            connected = true;
        }

        rigb =  golfBall.GetComponent<Rigidbody>();
        actualshot = GameObject.Find("Canvas").transform.Find("shoot").gameObject;
    }

 

    // Update is called once per frame
    void Update()
    {
        var sp = spawnWay.GetComponent<GenerateMap>();
        cord.text=sp.latff.ToString()+" "+sp.lonff.ToString();
        Vector3 gps = new Vector3(longitude,0,latitude);
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0);
        sp.latff += move.x*Time.deltaTime*0.003f;
        sp.lonff += move.y*Time.deltaTime*0.003f;       
      
       
        if(connected == true && Input.location.lastData.latitude != 0){
            sp.latff = Input.location.lastData.latitude;
            sp.lonff = Input.location.lastData.longitude; 

        }    
        
        Vector3 startpoint =globe.TransformPoint( new Vector3(sp.alt*Mathf.Sin(sp.latff)*Mathf.Cos(sp.lonff),sp.alt*Mathf.Sin(sp.latff)*Mathf.Sin(sp.lonff),sp.alt*Mathf.Cos(sp.latff)));
        Vector3 up = (globe.position - startpoint).normalized;
        transform.position = startpoint*-1;
        transform.LookAt(globe.position);

        var shootingnow = actualshot.GetComponent<ActualShooting>().ShootingNow;
        if(shootingnow < 2)
            camGuide.transform.RotateAround(transform.position,up,rotatearoundthismuch);
        else 
            rotatearoundthismuch = 0;

        
        if(cameralerpersmootherexpereiencer < cameralerp || cameralerpersmootherexpereiencer > cameralerp)
            cameralerpersmootherexpereiencer += (cameralerp-cameralerpersmootherexpereiencer)/3f;
        else 
            cameralerpersmootherexpereiencer=cameralerp;


        cam.transform.position = Vector3.Lerp(camGuide1.position,camGuide2.position,cameralerpersmootherexpereiencer);
        cam.transform.rotation = Quaternion.Slerp(camGuide1.rotation,camGuide2.rotation,cameralerpersmootherexpereiencer);
        

        if(Math.Abs(rotatearoundthismuch) >= 0.001f && Input.touchCount==spaghettifinger)
            rotatearoundthismuch=rotatearoundthismuch/1.3f;
        else if (Input.touchCount==0 && rotatearoundthismuch !=0)
            rotatearoundthismuch = 0;


        
        prevlat = sp.latff;
        prevlon = sp.lonff;

        //cameratogolfball
        if(shootingnow > 0)
            cameraposition.position = golfBall.transform.position;
        else if(shootingnow == 0 && rigb.velocity.magnitude<=0.2f)
            cameraposition.position = transform.position;

        //golfballdistance
        golfBall.transform.position = (golfBall.transform.position-new Vector3(0,0,0)).normalized * (sp.alt+0.1f);
    }
    
}
