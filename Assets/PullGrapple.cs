using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullGrapple : MonoBehaviour
{
    [Header("Public References")]
    public GameObject hook;
    public ShootHook grappleGun;

    [Space]
    public float flyTowardsHookSpeed = 5f;

    public float launchForce = 10f;
    bool pulling;
    bool reachedHook;
    bool gliding;
    Rigidbody rb;
    private void Start() {
        rb = GetComponent<Rigidbody>();    
    }

    private void Update() {
        if(pulling){
            PullTowardsHook();
            StopAtHook();
            
            if(Input.GetButtonDown("Fire2")){
                gliding = true;
                Glide();
            }    
        }

        if(gliding){
            LandFromGlide();
        }
    }
    
    void PullTowardsHook(){
            if(hook.GetComponent<HookBehavior>().hooked){
                pulling = true;
                rb.isKinematic = false;
                Vector3 destinationPoint = (new Vector3(hook.transform.position.x, hook.transform.position.y + 1f, hook.transform.position.z));
                rb.velocity = (-transform.position + destinationPoint) * flyTowardsHookSpeed;
            }
    }

    void StopAtHook(){
        if(Vector3.Distance(transform.position, new Vector3(hook.transform.position.x, hook.transform.position.y + 1f , hook.transform.position.z))<0.1f){
            GetComponent<CharacterController>().enabled = true;
            GetComponent<PlayerController>().enabled = true;
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.Sleep();
            pulling = false;
            GetComponent<Animator>().SetTrigger("Idle");

            grappleGun.ResetHookToGun();
        }
    }

    void Glide(){
        if(pulling){
           
                gliding = true;
                pulling = false;
                grappleGun.fired=false;

                grappleGun.ResetHookToGun();
                rb.useGravity = true;

                rb.AddForce((transform.forward + transform.up)* launchForce, ForceMode.Impulse);
        }
    }

    void LandFromGlide(){
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.2f);
            if(isGrounded){
                gliding = false;
                GetComponent<CharacterController>().enabled = true;
                GetComponent<PlayerController>().enabled = true;
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.Sleep();
                GetComponent<Animator>().SetTrigger("Idle");
            }
    }
    void ShootHook(){
        grappleGun.fired = true;
        grappleGun.hook.GetComponent<TrailRenderer>().emitting = true;
    }

    void RotateToAttach(){
        GetComponent<PlayerController>().enabled = false;
        GetComponent<CharacterController>().enabled = false;
        grappleGun.RotateToAttach();
    }

    void PullHook(){
        pulling = true;
    }
}
