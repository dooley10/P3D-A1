using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] bombSpawns;
    public GameObject bomb;
    private GameObject bombInstance;
    public float bombTimer;
    public Text timerText;
    public Text failureText;
    public Text successText;
    private bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        failureText.enabled = false;
        successText.enabled = false;
        //Sets the timer to 180 seconds
        bombTimer = 180f;
        bombInstance = Instantiate(bomb, bombSpawns[Random.Range (0, 6)].transform.position, Quaternion.identity);
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        //If the game isn't over
        if(gameOver == false){
            //Calculates time left
            bombTimer -= Time.deltaTime;
            //Outputs time in seconds
            timerText.text = Mathf.Round(bombTimer).ToString();
            //If the bomb blew up do this
            if(bombInstance.GetComponent<BombAudio>().exploded == true && bombInstance.GetComponent<BombAudio>().disposed == true){
                gameOver = true;
                successText.enabled = true;
            } else if(bombInstance.GetComponent<BombAudio>().exploded == true && bombInstance.GetComponent<BombAudio>().disposed == false){
                gameOver = true;
                failureText.enabled = true;
            }
            //If the bomb 
            if(bombTimer <= 0){
                gameOver = true;
                bombInstance.GetComponent<BombAudio>().Explode();
                failureText.enabled = true;
            }
            //If the bomb has exploded
            if(gameOver == true){
                bombTimer = 5f;
            }
        } else if (gameOver == true){
            bombTimer -= Time.deltaTime;
            if(bombTimer <= 0){
                SceneManager.LoadScene(0);
            }
        }
    }
}
