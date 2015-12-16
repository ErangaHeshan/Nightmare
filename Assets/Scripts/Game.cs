using UnityEngine;
using System.Collections;
using TANK;

public class Game : MonoBehaviour {
    private GameClient game_client; 
	// Use this for initialization
	void Start () {
        try{
            game_client = new GameClient("127.0.0.1");
            game_client.SendToServer("JOIN#");
        }catch(System.Exception e)
        {

        }
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.UpArrow))
            game_client.SendToServer("UP#");
        if (Input.GetKeyUp(KeyCode.DownArrow))
            game_client.SendToServer("DOWN#");
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            game_client.SendToServer("LEFT#");
        if (Input.GetKeyUp(KeyCode.RightArrow))
            game_client.SendToServer("RIGHT#");
        if (Input.GetKeyUp(KeyCode.Space))
            game_client.SendToServer("SHOOT#");
    }
}
