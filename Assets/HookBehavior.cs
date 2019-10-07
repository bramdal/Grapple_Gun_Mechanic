using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookBehavior : MonoBehaviour
{
    public bool hooked;
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Hookable"){
            hooked = true;
        }
    }
}
