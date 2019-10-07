using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHook : MonoBehaviour
{

    [Header("Public References")]
    public IdentifyHookableObjects identifyHookableObjects;
    public GameObject hook;

    [Space]
    public float hookSpeed;
    Vector3 attachPoint;
    [HideInInspector]public bool fired;

    Animator anim;

    private void Start() {
        anim = transform.root.gameObject.GetComponent<Animator>();
    }
    private void Update() {
        
        FireHook();
        ShootHookTowardsEdge();
    }

    
    public void FireHook(){
        if(Input.GetButtonDown("Fire1") && !fired){
            if(identifyHookableObjects.ledgeFound){
                attachPoint = identifyHookableObjects.attachPoint;
                //fired = true;
                
                anim.SetTrigger("Fire Grapple");
                anim.SetTrigger("Pull Grapple");
            }    
        }
    }
    public void ShootHookTowardsEdge(){
        if(fired){
            hook.transform.parent = null;
            float step = hookSpeed * Time.deltaTime;
            hook.transform.position = Vector3.MoveTowards(hook.transform.position, attachPoint, step);
        }
    }

    public void RotateToAttach(){
        Vector3 rotateToAttach = attachPoint - transform.root.transform.position;
        rotateToAttach.y = 0;
        transform.root.transform.rotation = Quaternion.Slerp(transform.root.transform.rotation, Quaternion.LookRotation(rotateToAttach), 1f);
       
    }

    public void ResetHookToGun(){
        if(hook.GetComponent<HookBehavior>().hooked){
            hook.transform.parent = transform;
            hook.transform.localPosition = Vector3.zero;
            hook.transform.localRotation = Quaternion.identity;  //test out
            hook.GetComponent<HookBehavior>().hooked = false;
            fired = false;
            
            hook.GetComponent<TrailRenderer>().emitting = false;;
        }
    }
}
