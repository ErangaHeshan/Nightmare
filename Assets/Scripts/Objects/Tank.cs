using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour {

    public enum Direction { North, East, South, West };    //Store tank's direction
    public int health;
    public Direction direction;
    // Use this for initialization
    void Start () {
        health = 100;
	}
	
	// Update is called once per frame
	void Update () {
	  
	}
}
