using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : bridgeObject {
    [SerializeField] Vector3 topPosition;
    [SerializeField] Vector3 bottomPosition;
    [SerializeField] float speed;
    [SerializeField]
    private AudioClip sfxCoin;
    private AudioSource audioSource;

    int pointScore;


    // Use this for initialization
    void Start () {
        StartCoroutine(Move(bottomPosition));
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	protected override void Update () {
        if (GameManager.instance.PlayerActive)
        {
            base.Update();
        }
    }

    IEnumerator Move(Vector3 target)
    {
        while (Mathf.Abs((target - transform.localPosition).y) > 0.20f)
        {
            Vector3 direction = target.y == topPosition.y ? Vector3.up : Vector3.down;
            transform.localPosition += direction * Time.deltaTime * speed;

            transform.Rotate(new Vector3(0, Time.deltaTime * 125, 0), Space.Self);

            yield return null;
        }

        print("Reached the target");

        yield return new WaitForSeconds(0.5f);

        Vector3 newTarget = target.y == topPosition.y ? bottomPosition : topPosition;

        StartCoroutine(Move(newTarget));
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(sfxCoin);
            //GameManager.instance.PlayerCollided();

            //GameManager.instance.ExitGame();
        }
    }
}
