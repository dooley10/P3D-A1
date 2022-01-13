using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private Animator myAnimationController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            myAnimationController.SetBool("buttonClicked", true);
        }
    }

    
    private void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            myAnimationController.SetBool("buttonClicked", false);
        }
    }
}
