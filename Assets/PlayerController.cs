using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player movement variables")]
    public float moveSpeed = 5f;
    public float gravity = 9.8f;
    public float rotationSpeed;
    Vector3 movementDirection = Vector3.zero;
    float inputX = 0f;
    float inputZ = 0f;
    [HideInInspector]public bool moving;

    CharacterController characterController;
    Animator anim;

    private void Start() {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }
    private void Update() {
        GetMovementInput();
        SetAnimations();
    }

    void GetMovementInput(){
        movementDirection = Vector3.zero;
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        var forwardDirection = Camera.main.transform.forward;
        var rightDirection = Camera.main.transform.right;

        forwardDirection.y = rightDirection.y = 0f;

        forwardDirection.Normalize();
        rightDirection.Normalize();

        movementDirection = inputZ * forwardDirection + inputX * rightDirection;
        if(movementDirection.magnitude>0f){
            moving = true;
        }
        else{
            moving = false;
        }
        movementDirection *= moveSpeed;
        
        if(movementDirection != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection), rotationSpeed);

        //verticals
        movementDirection.y -= gravity;

        characterController.Move(movementDirection * Time.deltaTime);    

    }

    void SetAnimations(){
        if(moving){
            anim.SetBool("Moving", true);
            anim.SetFloat("Velocity Z", movementDirection.magnitude);
        }
        else
        {
            anim.SetBool("Moving", false);
            anim.SetFloat("Velocity Z", movementDirection.magnitude);
        }
    }
    

}
