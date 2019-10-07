using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdentifyHookableObjects : MonoBehaviour
{
    GameObject player;
    
    RaycastHit spherecastInfo;
    [HideInInspector]public bool ledgeFound;

    public float maxDistance=20f;

    [HideInInspector]public Vector3 attachPoint;

    [Space]
    [Header("Public References")]
    public Image attachPointCursor;
    public RectTransform canvasRect;
   

    private void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        player = GameObject.FindWithTag("Player");
        attachPointCursor.gameObject.SetActive(false);
    }

    private void Update() {
        Vector3 spherecastOrigin = new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z); 
        bool hitSomething = Physics.SphereCast(spherecastOrigin, 2f, transform.forward, out spherecastInfo, maxDistance);  //hard coded sphere radius
        if(hitSomething && spherecastInfo.collider.tag == "Hookable" && Vector3.Distance(player.transform.position, spherecastInfo.point)>5f){
                ledgeFound = true;
                attachPointCursor.gameObject.SetActive(true);
                print(spherecastInfo.point);

                attachPoint = spherecastInfo.point;

                attachPointCursor.transform.position = Camera.main.WorldToScreenPoint(attachPoint);    
        } 
        else{
                ledgeFound = false;
                attachPointCursor.gameObject.SetActive(false);
        }  
         
    }

}
