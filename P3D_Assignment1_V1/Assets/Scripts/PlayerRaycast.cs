using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    public float maxDistance = 3;
    RaycastHit objectHit;
    public GameObject player;
    public GameObject corridorDoor;
    public GameObject controlText;
    public GameObject mainCamera;
    private bool doorOpen;
    private bool objectHeld;
    public Transform holdPosition;
    private AudioSource interactAudio;
    public AudioClip buttonAudio;
    public AudioClip throwAudio;
    


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        corridorDoor = GameObject.FindGameObjectWithTag("corridorDoor");

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        doorOpen = false;
        objectHeld = false;

        interactAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug ray
        Debug.DrawRay(this.transform.position, this.transform.forward * maxDistance, Color.magenta);
        //If player looks at object set objectHit to that gameObject
        if(Physics.Raycast(this.transform.position, this.transform.forward, out objectHit, maxDistance)){
            //If the object touched is a button
            if(objectHit.collider.gameObject.tag == "corridorButton"){
                if(Input.GetMouseButtonDown(0)){
                    //playing button click sound
                    interactAudio.volume = 0.4f;
                    interactAudio.pitch = 0.25f;
                    interactAudio.PlayOneShot(buttonAudio);
                    objectHit.collider.gameObject.GetComponent<Animator>().SetTrigger("Clicked");
                    //Destroying 3D text
                    if(controlText != null){
                        Destroy(controlText);
                    }
                    //Opens the door
                    if (doorOpen == false){
                        corridorDoor.GetComponent<Animator>().SetBool("doorOpen", true);
                        corridorDoor.GetComponent<AudioSource>().Play();
                        doorOpen = true;
                    //Closes the door
                    } else {
                        corridorDoor.GetComponent<Animator>().SetBool("doorOpen", false);
                        doorOpen = false;
                        corridorDoor.GetComponent<AudioSource>().Play();
                    }
                }
            //If the object touched it the bomb
            } else if (objectHit.collider.gameObject.tag == "bomb"){
                if(Input.GetMouseButtonDown(0)){
                    if (objectHeld == false){
                        //Picks up the bomb
                        pickUpBomb();

                    } else if (objectHeld == true){
                        //Drops the bomb
                        dropBomb();
                    }
                } else if (Input.GetMouseButtonDown(1)){
                    //Throws the bomb
                    throwBomb();
                }
            } 
        }
    }
    
    void pickUpBomb(){
        objectHit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        objectHit.collider.gameObject.GetComponent<Rigidbody>().useGravity = false;
        objectHit.collider.gameObject.transform.position = holdPosition.position;
        objectHit.collider.gameObject.transform.rotation = holdPosition.rotation;
        objectHit.collider.gameObject.transform.parent = holdPosition;
        objectHeld = true;
    }

    void dropBomb(){
        objectHit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        objectHit.collider.gameObject.GetComponent<Rigidbody>().useGravity = true;
        objectHit.collider.gameObject.transform.parent = null;
        objectHeld = false;
    }

    void throwBomb(){
        objectHit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        objectHit.collider.gameObject.GetComponent<Rigidbody>().useGravity = true;
        objectHit.collider.gameObject.transform.parent = null;
        objectHit.collider.gameObject.GetComponent<Rigidbody>().AddForce(objectHit.collider.gameObject.transform.forward * (Time.deltaTime + 500));
        objectHeld = false;
        interactAudio.volume = 1f;
        interactAudio.pitch = 1f;
        interactAudio.PlayOneShot(throwAudio);
        
    }
}

    
