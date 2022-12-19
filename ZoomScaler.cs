using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ZoomScaler : MonoBehaviour
{
    public float scale;
    public float minDistance;

    // Update is called once per frame
    void Update(){
        Vector3 campos = Camera.main.transform.position;
        Vector3 pos = transform.position;
        float distance = Vector3.Distance(campos,pos) /10;
        if(distance<minDistance)
            distance = minDistance;
        transform.localScale = new Vector3(scale*distance,scale*distance,scale*distance);
    }
}
