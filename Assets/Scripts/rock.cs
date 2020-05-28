using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock : bridgeObject {

    [SerializeField]
    Vector3 topPosition;
    [SerializeField]
    Vector3 bottomPosition;
    [SerializeField] float speed;

	// Use this for initialization
	void Start () {
        StartCoroutine(Move(bottomPosition));
    }
	
	// Update is called once per frame
	protected override void Update () {
        if (GameManager.instance.PlayerActive)
        {
            base.Update();
        }

        



        //transform.Rotate(new Vector3(Time.deltaTime * 50, 0, 0));
        //transform.localRotation = new Quaternion(0,1,0,0);
        //transform.Rotate(new Vector3(0, 1, 0), Space.Self);
        //Vector2 rotate = new Vector2(1, 1);
        //transform.Rotate(rotate);

    }

    private void FixedUpdate()
    {
        //transform.Rotate(new Vector3(0, Time.deltaTime * 50, 0));
    }


    IEnumerator Move(Vector3 target)
    {
        
        //StartCoroutine(Spin());
        while (Mathf.Abs((target - transform.localPosition).y) > 0.20f) {
            Vector3 direction = target.y == topPosition.y ? Vector3.up : Vector3.down;
            transform.localPosition += direction * Time.deltaTime * speed;

            transform.Rotate(new Vector3(0, Time.deltaTime * 125, 0), Space.Self);
           
            
            //transform.RotateAround(new Vector3(0, 1, 0), 1);
            //transform.Rotate(Vector3.up * Time.deltaTime, Space.World);
            //transform.Rotate(Vector3.right * Time.deltaTime);
            yield return null;
        }

        print("Reached the target");

        //yield return new WaitForSeconds(0.0f);
        yield return new WaitForSeconds(0.5f);

        Vector3 newTarget = target.y == topPosition.y ? bottomPosition : topPosition;

        //float newX = transform.localPosition.x;
        //newX += 30  ;
        //transform.Rotate(new Vector3(0, newX, 0));

        StartCoroutine(Move(newTarget));
    }

    //IEnumerator Spin()
    //{
    //    for (int i = 0; i < 360; i++)
    //    {
    //        //Vector3 directionZ2 = new Vector3(0,0,i);
    //        //Vector3 directionZ = (spin.z += 1.0f);
    //        //transform.localRotation += 1.0f;

            

    //    }
    //    //transform.localPosition.y


        
    //    yield return new WaitForSeconds(0.0f);
    //    //StartCoroutine(Spin());
    //}
}
