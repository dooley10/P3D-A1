using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{

    CharacterController charCont;
    public AudioClip walking;
    public AudioClip running;
    public AudioClip jump;
    private AudioSource playerMovement;
    private bool jumped;

    // Start is called before the first frame update
    void Start()
    {
        charCont = GetComponent<CharacterController>();
        playerMovement = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(charCont.isGrounded == true && charCont.velocity.magnitude > 2f && charCont.velocity.magnitude < 11f && playerMovement.isPlaying == false){
            playerMovement.volume = Random.Range (0.8f, 1f);
            playerMovement.pitch = Random.Range (0.9f, 1.1f);
            playerMovement.PlayOneShot(walking);
            jumped = false;
        } else if(charCont.isGrounded == true && charCont.velocity.magnitude >= 11f && playerMovement.isPlaying == false){
            playerMovement.volume = Random.Range (0.8f, 1);
            playerMovement.pitch = Random.Range (0.9f, 1.1f);
            playerMovement.PlayOneShot(running);
            jumped = false;
        } else if(charCont.isGrounded == false && jumped == false){
            playerMovement.volume = Random.Range (0.8f, 1);
            playerMovement.pitch = Random.Range (0.9f, 1.1f);
            playerMovement.PlayOneShot(jump);
            jumped = true;
        }
    }
}
