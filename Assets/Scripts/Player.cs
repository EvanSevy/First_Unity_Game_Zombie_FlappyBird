using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour {

    private Animator anim;
    private Rigidbody rigidBody;

    [SerializeField]
    private float jumpForce = 100f;
    [SerializeField]
    private AudioClip sfxJump;
    [SerializeField]
    private AudioClip sfxDeath;
    [SerializeField]
    private AudioClip sfxCoin;


    private bool jump = false;
    private AudioSource audioSource;
    private int coinScore;

    private void Awake()
    {
        Assert.IsNotNull(sfxJump);
        Assert.IsNotNull(sfxDeath);
    }

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {

        if (!GameManager.instance.GameOver && GameManager.instance.GameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.instance.PlayerStartedGame();
                anim.Play("Jump");
                audioSource.PlayOneShot(sfxJump);
                rigidBody.useGravity = true;
                jump = true;
            }
        }

	}

    private void FixedUpdate()
    {
        if (jump == true)
        {
            jump = false;

            rigidBody.velocity = new Vector2(0, 0);
            rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            rigidBody.AddForce(new Vector2(-50, 20), ForceMode.Impulse);
            rigidBody.detectCollisions = false;
            audioSource.PlayOneShot(sfxDeath);
            GameManager.instance.PlayerCollided();

            GameManager.instance.ExitGame();
        } else if (collision.gameObject.tag == "coin")
        {
            audioSource.PlayOneShot(sfxCoin);
            Vector3 newPos = new Vector3(-100.5f, transform.position.y, transform.position.z);
            collision.gameObject.transform.position = newPos;
            coinScore += 1;
            //collision.gameObject.SetActive(false);
            //GameObject coinObject = collision.gameObject.GetComponent<GameObject>();
            
            //transform.position = newPos;
            //coinObject.transform.position = newPos;
            


        }
        //if (collision.gameObject.tag == "floor")
        //{
        //    rigidBody.AddForce(new Vector2(-50, 20), ForceMode.Impulse);
        //    rigidBody.detectCollisions = false;
        //    audioSource.PlayOneShot(sfxDeath);
        //    GameManager.instance.PlayerCollided();

        //    GameManager.instance.ExitGame();
        //}
    }
}
