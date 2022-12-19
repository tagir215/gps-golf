using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootLogic : MonoBehaviour
{
    float time = 5f;
    float alpha = 1;
    float alphaR = 0.2f;
    GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        child = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTime())
            Destroy(gameObject);   

        alpha -= alphaR*Time.deltaTime;
        child.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f,alpha);
        
    }

    bool isTime(){
        time -= Time.deltaTime;
        if(time<0)
            return true;
        else 
            return false;
    }
}
