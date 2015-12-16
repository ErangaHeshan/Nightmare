using UnityEngine;
using System.Collections;

public class t1_Behaviour : MonoBehaviour {

    public float speed;
    private Rigidbody rb;
    private int count;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        count = 0;
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(moveVertical, 0.0f, -moveHorizontal);
        rb.AddForce(movement * speed);
    }
}
