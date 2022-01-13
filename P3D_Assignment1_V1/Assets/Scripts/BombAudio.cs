using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAudio : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody bombRig;
    public GameObject explosion;
    private bool explodable;
    public bool exploded;
    public bool disposed;
    private AudioSource bombAS;
    public AudioClip metalTouch;
    public AudioClip explosionSound;
    public AudioClip tick;

    void Start()
    {
        bombAS = GetComponent<AudioSource>();
        bombRig = GetComponent<Rigidbody>();
        explodable = false;
        exploded = false;
        disposed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(bombRig.velocity.magnitude > 4 && explodable == false){
            explodable = true;
        }

        if(bombAS.isPlaying == false){
            bombAS.volume = .85f;
            bombAS.pitch = 0.5f;
            bombAS.PlayOneShot(tick);
        }
    }

    void OnCollisionEnter(){
        if(explodable == true){
            bombAS.volume = 1f;
            bombAS.pitch = 1f;
            if(exploded == false){ 
                Explode();
            }
        } else {
            bombAS.volume = Random.Range(0.3f, 0.5f);
            bombAS.pitch = Random.Range(0.9f, 1.1f);
            bombAS.PlayOneShot(metalTouch);
        }
    }

    void OnTriggerEnter(){
        disposed = true;
    }

    public void Explode(){
        bombAS.rolloffMode = AudioRolloffMode.Linear;
        bombAS.PlayOneShot(explosionSound);
        Instantiate(explosion, transform.position, Quaternion.identity);
        GetComponent<MeshRenderer>().enabled = false;
        exploded = true;
        Destroy(gameObject, explosionSound.length);
    }
}
