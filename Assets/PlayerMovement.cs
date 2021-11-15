using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    public static int sound = 1;
    public static int mute = 0;
    public AudioSource audioGame;
    public AudioSource audioCoinsAndSpheres;
    public AudioSource audioObstacle;
    public AudioSource invincibleAudio;
    public AudioSource menus;
    public float speed;
    public float speedMovement;
    private string lane = "mid";
    private Rigidbody rb;
    public GameObject tile;
    public GameObject parent;
    public GameObject sphere;
    public GameObject coin;
    public GameObject ironBall;
    public GameObject bomb;
    public GameObject pauseMenu;
    public Camera a ;
    public Camera b;
    public float score = 0;
    public TMP_Text scoreDisplayed;
    public TMP_Text coinsDisplayed;
    public TMP_Text spheresDisplayed;
    public TMP_Text invincibleTxt;
    public float coins = 0;
    public float spheres = 0;
    public Boolean invincible = false;
    public Boolean speedDouble = false;
    public Boolean gameOver = false;
    private Boolean timerStarted = false;
    private Boolean paused = false;
    private Boolean cameraChange = false;
    private float jumpSpeed = 0.5f; 
    private float _timer = 0f;
    public float timeIWantInSeconds ;
    string[] collectables = { "sphere", "coin", "ironBall", "bomb" };
    private float tilePos = -245;
    private Vector2 fp; // first finger position
    private Vector2 lp; // last finger position
    public bool swipeLeftt = false;
    public bool swipeRightt = false;
    public float speedTemp;
    public float speedMovTemp;
    public float jumpSpeedTemp;
    //public static PlayerMovement X;



    void Start()
    {
        cameraChange = false;
        a.enabled = true;
        b.enabled = false;
        paused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        cameraChange = false;
        pauseMenu.SetActive(false);
        if (mute == 0)
        {
            audioGame.Play();
        }
        float pos = tilePos;
        rb = GetComponent<Rigidbody>();
        for (int i = 0; i < 6; i++)
        {
           
            pos = pos + 25;
            makingCollectables(pos);
        }
    }

    void makingCollectables(float pos)
    {
       
       
        int lane1 = UnityEngine.Random.Range(0,4);
        if (lane1 == 0)
            {
            GameObject x =  Instantiate(sphere, new Vector3(-3.5f, 1.0f, pos), Quaternion.identity);
            x.transform.parent = parent.transform;
            }
            if (lane1 == 1)
            {
            GameObject x = Instantiate(coin, new Vector3(-3.5f, 1.0f, pos), Quaternion.identity);
            x.transform.parent = parent.transform;
        }
            if (lane1 == 2)
            {
            GameObject x = Instantiate(ironBall, new Vector3(-3.5f, 1.0f, pos), Quaternion.identity);
            x.transform.parent = parent.transform;
        }
            if (lane1 == 3)
            {
            GameObject x = Instantiate(bomb, new Vector3(- 3.5f, 1.0f, pos), Quaternion.identity);
            x.transform.parent = parent.transform;
        }

            
            int lane2 = UnityEngine.Random.Range(0, 4);
        if (lane2 == 0)
            {
            GameObject x = Instantiate(sphere, new Vector3(0.0f, 1.0f, pos), Quaternion.identity);
            x.transform.parent = parent.transform;
        }
            if (lane2 == 1)
            {
            GameObject x = Instantiate(coin, new Vector3(0.0f, 1.0f, pos), Quaternion.identity);
            x.transform.parent = parent.transform;
        }

            if (lane2 == 2)
            {
            GameObject x = Instantiate(ironBall, new Vector3(0.0f, 1.0f, pos), Quaternion.identity);
            x.transform.parent = parent.transform;
        }
            if (lane2 == 3)
            {
            GameObject x = Instantiate(bomb, new Vector3(0.0f, 1.0f, pos), Quaternion.identity);
            x.transform.parent = parent.transform;
        }

       
            int lane3 = UnityEngine.Random.Range(0, 4);
        if (lane3 == 0)
            {
            GameObject x = Instantiate(sphere, new Vector3(3.5f, 1.0f, pos), Quaternion.identity);
            x.transform.parent = parent.transform;
        }
            if (lane3 == 1)
            {
            GameObject x = Instantiate(coin, new Vector3(3.5f, 1.0f, pos), Quaternion.identity);
            x.transform.parent = parent.transform;
        }
            if (lane3 == 2)
            {
            GameObject x = Instantiate(ironBall, new Vector3(3.5f, 1.0f, pos), Quaternion.identity);
            x.transform.parent = parent.transform;
        }
            if (lane3 == 3)
            {
            GameObject x = Instantiate(bomb, new Vector3(3.5f, 1.0f, pos), Quaternion.identity);
            x.transform.parent = parent.transform;
        }

        

    }
    // Update is called once per frame
    public void Update() 
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == UnityEngine.TouchPhase.Began)
            {
                fp = touch.position;
                lp = touch.position;
            }
            if (touch.phase == UnityEngine.TouchPhase.Moved)
            {
                lp = touch.position;
            }
            if (touch.phase == UnityEngine.TouchPhase.Ended)
            {
                if ((fp.x - lp.x) > 80) // left swipe
                {
                    swipeLeftt = true;
                    swipeRightt = false;
                    swipeLeft();
                }
                else if ((fp.x - lp.x) < -80) // right swipe
                {
                    swipeLeftt = false;
                    swipeRightt = true;
                    swipeRight();
                }
            }
        }
    
        if(score <0)
        {
           
            SceneManager.LoadScene(sceneName: "audiogamemeOver");
        }
        scoreDisplayed.text = "Score: " + score.ToString();
        coinsDisplayed.text = "Coins: " + coins.ToString();
        spheresDisplayed.text = "Spheres: " + spheres.ToString();
        rb.transform.Translate(new Vector3(0.0f, 0.0f, 2.0f) * speedMovement*Time.timeScale);
        a.transform.Translate(new Vector3(0.0f, 0.0f, 2.0f) * speedMovement * Time.timeScale);
        if (timerStarted)
        {
            _timer += Time.deltaTime;

            if (_timer >= timeIWantInSeconds)
            {
                _timer = 0f;
                if (invincible == true)
                {
                    invincible = false;
                    if (mute == 0)
                    {
                        audioGame.Play();
                        invincibleAudio.Pause();
                    }
                    invincibleTxt.text = "invincibility: " + "OFF";
                }
                if(speedDouble == true)
                {
                    speedMovement = speedMovement / 2;
                    speedDouble = false;
                }
            }
        }
        }
    public void Jump (InputAction.CallbackContext context)

    {
        if (context.performed)
        {
            if (rb.position.y == 0.4)
            {
                rb.transform.Translate(new Vector3(0.0f, 10.0f, 0.0f) * jumpSpeed * Time.timeScale);
            }

            else
            {
                rb.transform.Translate(new Vector3(0.0f, 10.0f - rb.position.y, 0.0f) * jumpSpeed * Time.timeScale);
            }
        }
    }

    public void jumpButton ()

    {

        if (rb.position.y == 0.4)
        {
            rb.transform.Translate(new Vector3(0.0f, 10.0f, 0.0f) * jumpSpeed * Time.timeScale);
        }

        else
        {
            rb.transform.Translate(new Vector3(0.0f, 10.0f - rb.position.y, 0.0f) * jumpSpeed * Time.timeScale);
        }

    }

    public void Left (InputAction.CallbackContext context)

    {
        if (context.performed)
        {
            if (lane == "mid")
            {
                lane = "left";
               
                
                    transform.Translate(new Vector3(-60.0f, 0.0f, 0.0f) * 0.05f * Time.timeScale);
                
            }
            if (lane == "right")
            {
                lane = "mid";
               
                    transform.Translate(new Vector3(-60.0f, 0.0f, 0.0f) * 0.05f * Time.timeScale);
                
            }
        }
    }
    public void swipeLeft()

    {
      
            if (lane == "mid")
            {
                lane = "left";
                if (speedMovement == 0.5)
               
                    transform.Translate(new Vector3(-60.0f, 0.0f, 0.0f) * 0.05f * Time.timeScale);
                
            }
            if (lane == "right")
            {
                lane = "mid";
                
                    transform.Translate(new Vector3(-60.0f, 0.0f, 0.0f) * 0.05f * Time.timeScale);
                
            }
        
    }

    public void pause(InputAction.CallbackContext context)

    {
        if (paused == false)
        {
            paused = true;
            pauseMenu.SetActive(true);
            if ( mute == 0)
            {
                audioGame.Pause();
                menus.Play();
            }
            Time.timeScale = 0;
          
        }
        else
        {
            if (mute == 0)
            {
                menus.Pause();
                audioGame.Play();
                
            }
            Time.timeScale = 1;
            paused = false;
            pauseMenu.SetActive(false);
           
            

        }
    }
    public void pauseButton()

    {
        if (paused == false)
        {
            paused = true;
            pauseMenu.SetActive(true);
            if (mute == 0)
            {
                audioGame.Pause();
                menus.Play();
            }
            Time.timeScale = 0;

        }
        else
        {
            if (mute == 0)
            {
                menus.Pause();
                audioGame.Play();

            }
            Time.timeScale = 1;
            paused = false;
            pauseMenu.SetActive(false);



        }
    }
    public void resume ( )
    {
        paused = false;
        menus.Pause();
        if(mute == 0)
        {
            audioGame.Play();
        }
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void restart()
    {
        cameraChange = false;
        a.enabled = true;
        b.enabled = false;
        paused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName: "Scene1");
    }
    public void cameraView(InputAction.CallbackContext context)

    {
        if (cameraChange == false)
        {
            cameraChange = true;
            a.enabled = false;
            b.enabled = true;
        }
        else
        {
            cameraChange = false;
            a.enabled = true;
            b.enabled = false;
        }
    }

    public void cameraButton()

    {
        if (cameraChange == false)
        {
            cameraChange = true;
            a.enabled = false;
            b.enabled = true;
        }
        else
        {
            cameraChange = false;
            a.enabled = true;
            b.enabled = false;
        }
    }

        public void Right(InputAction.CallbackContext context)

    {
        if (context.performed)
        {
            if (lane == "mid")
            {

                lane = "right";
                if (speedMovement == 0.5)
                {
                    print("in");

                    transform.Translate(new Vector3(60.0f, 0.0f, 0.0f) * 0.05f * Time.timeScale);
                }
                else
                {
                    transform.Translate(new Vector3(60.0f, 0.0f, 0.0f) * 0.05f * Time.timeScale);
                }
                
            }
            if (lane == "left")
            {

                lane = "mid";
                if (speedMovement == 0.5)
                {
                    print("in");
                    transform.Translate(new Vector3(60.0f, 0.0f, 0.0f) * 0.05f * Time.timeScale);
                }
                else
                {
                    transform.Translate(new Vector3(60.0f, 0.0f, 0.0f) * 0.05f * Time.timeScale);
                }
            }
        }
    }

    public void swipeRight()

    {
        
            if (lane == "mid")
            {

                lane = "right";
                if (speedMovement == 0.5)
                {
                    print("in");

                    transform.Translate(new Vector3(60.0f, 0.0f, 0.0f) * 0.05f * Time.timeScale);
                }
                else
                {
                    transform.Translate(new Vector3(60.0f, 0.0f, 0.0f) * 0.05f * Time.timeScale);
                }

            }
            if (lane == "left")
            {

                lane = "mid";
                if (speedMovement == 0.5)
                {
                    print("in");
                    transform.Translate(new Vector3(60.0f, 0.0f, 0.0f) * 0.05f * Time.timeScale);
                }
                else
                {
                    transform.Translate(new Vector3(60.0f, 0.0f, 0.0f) * 0.05f * Time.timeScale);
                }
            }
        
    }


    public void OnTriggerEnter(Collider other)

    {
        if (other.CompareTag("tile"))
        {
            
            tilePos = tilePos + 150;
            parent = Instantiate(tile, new Vector3(0.0f, 0.0f, tilePos), Quaternion.identity);
            float pos = tilePos + 50;
            for (int i = 0; i < 6; i++)
            {

                pos = pos + 25;
                makingCollectables(pos);
            }

        }
        if (other.CompareTag("sphere"))
        {
            if ( mute == 0)
            {
                audioCoinsAndSpheres.PlayOneShot(audioCoinsAndSpheres.clip);
            }
            Destroy(other.gameObject);
            score = score + 10;
            spheres = spheres + 1;
            if (spheres % 3 == 0)
            {
                if (speedDouble == false)
                {
                    speedMovement = speedMovement * 2;
                }
                timerStarted = true;
                speedDouble = true;
                timeIWantInSeconds = 7;

            }

        }
        if (other.CompareTag("Coin"))
        {
            if ( mute == 0)
            {
                
                audioCoinsAndSpheres.PlayOneShot(audioCoinsAndSpheres.clip);
               
            }
            Destroy(other.gameObject);
            score = score + 15;
            coins = coins + 1;
            if (coins % 3 == 0)
            {

                timerStarted = true;
                invincible = true;
                if (mute == 0)
                {
                    audioGame.Pause();
                    invincibleAudio.Play();
                }
                invincibleTxt.text = "invincibility: " + "ON";
                timeIWantInSeconds = 5;
            }
        }
        if (other.CompareTag("IronBall"))
        {
            if (  mute == 0)
            {
            
                audioObstacle.PlayOneShot(audioObstacle.clip);
                
            }
            if (invincible == false && score > 0)
            {
                score = score - 10;
            }
            
            if(invincible == false && score == 0)
            {
                gameOver = true;
                if (mute == 1)
                {

                    menus.Pause();
                }
                else
                {
                    menus.Play();
                }
                SceneManager.LoadScene(sceneName: "gameOver");
            }
            Destroy(other.gameObject);
        }
        if (other.CompareTag("bomb"))
        {
            if (mute == 0)
            {
                
                audioObstacle.PlayOneShot(audioObstacle.clip);
                
            }
            if (invincible == false)
            {
                print("in");
                print(invincible);
                gameOver = true;
                if (mute == 1)
                {
                   
                    menus.Pause();
                }
                else
                {
                    menus.Play();
                }
                SceneManager.LoadScene(sceneName: "gameOver");
                print("bomb");
                

            }
            Destroy(other.gameObject);



        }
    }
    public void OnTriggerExit(Collider other)

    {
        if (other.CompareTag("tile"))
        {

            Destroy(other.gameObject);

        }

        if (other.CompareTag("sphere"))
        {
            

        }
        if (other.CompareTag("Coin"))
        {
           
        }
        if (other.CompareTag("IronBall"))
        {
            
        }
        if (other.CompareTag("bomb"))
        {
           
            



        }
        if (other.CompareTag("tile"))
        {
            


        }

    }

    public void start ()

    {
            speed = 0.025f;
            speedMovement = 0.05f;
            jumpSpeed = 0.5f;
            paused = false;
            

    }

}