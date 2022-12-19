using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameStart : MonoBehaviour, IPointerDownHandler
{
    public Transform GPS;
    public GameObject golfBall;
    // Start is called before the first frame update
    
    public void OnPointerDown(PointerEventData eventData)
    {
        //GameObject.Instantiate(golfBall,GPS.position,new Quaternion(0,0,0,0));
        golfBall.transform.position = GPS.position;
    }
    
}
