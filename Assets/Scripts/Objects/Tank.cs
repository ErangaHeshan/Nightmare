using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour {

    public enum Direction { North, East, South, West };    //Store tank's direction
    public int health { get; set; }
    public int points { get; set; }
    public Direction direction { get; set; }
    public float positionX { get; set; }
    public float positionY { get; set; }
    public int id { get; set; }

    // Use this for initialization
    void Start () {
        health = 100;
        points = 0;
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
