using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateAudio : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource crateAS;
    public AudioClip metalTouch;
    void Start()
    {
        crateAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(){
            crateAS.volume = Random.Range(0.3f, 0.5f);
            crateAS.pitch = Random.Range(0.9f, 1.1f);
            crateAS.PlayOneShot(metalTouch);
    }
}
