using UnityEngine;
using System.Collections;
using TANK;

public class Game : MonoBehaviour {
    private GameClient game_client; 
	// Use this for initialization
	void Start () {
        try
        {
            GameObject monster = (GameObject)Instantiate(Resources.Load("Tank2"));
            monster.transform.position = new Vector3(-17, 0, -15);
            /*
            game_client = new GameClient("127.0.0.1");
            //Send join command to the server
            game_client.SendToServer("JOIN#");
            GameObject p = Game*/
        }
        catch (System.Exception e)
        {
            //Send null pointer error 
        }
        
	}
	
	// Update is called once per frame
	void Update () {/*
        if (Input.GetKeyUp(KeyCode.UpArrow))
            game_client.SendToServer("UP#");     //Tank movement up
        if (Input.GetKeyUp(KeyCode.DownArrow))
            game_client.SendToServer("DOWN#");   //Tank movement down
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            game_client.SendToServer("LEFT#");   //Tank movement left
        if (Input.GetKeyUp(KeyCode.RightArrow))
            game_client.SendToServer("RIGHT#");  //Tank movement right
        if (Input.GetKeyUp(KeyCode.Space))
            game_client.SendToServer("SHOOT#");  //Tank shoot
            */
        
    }
}
