using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    
    public Transform targ;
    public Transform pointer;
    
   
    
    // Update is called once per frame
    void Update()
    {
        
        transform.position = new Vector3(targ.position.x, targ.position.y, targ.position.z);
        
        
       transform.LookAt(pointer);

        
       
        
    }
}
