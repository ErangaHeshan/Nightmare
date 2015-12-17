using UnityEngine;
using System.Collections;
using TANK;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;

public class Game : MonoBehaviour {
    private GameClient game_client;
    string serverString;
    public Text test;
    public GameObject[] brickWall = new GameObject[25];
    public GameObject[] stoneWall = new GameObject[25];
    public GameObject[] water = new GameObject[25];

    // Use this for initialization
    void Start () {

        try
        {
            //brickWall[1] = Instantiate(Resources.Load("BrickWall"), new Vector3(-22.5f + 5 * 1, 2.5f, -22.5f + 5 * 1), Quaternion.identity) as GameObject;
            //brickWall[2] = Instantiate(Resources.Load("BrickWall"), new Vector3(-22.5f + 5 * 1, 2.5f, -22.5f + 5 * 2), Quaternion.identity) as GameObject;
            //stoneWall[1] = Instantiate(Resources.Load("StoneWall"), new Vector3(-22.5f + 5 * 2, 2.5f, -22.5f + 5 * 2), Quaternion.identity) as GameObject;
            game_client = new GameClient("127.0.0.1");
            game_client.SendToServer("JOIN#");
            //brickWall[0] = (GameObject)Instantiate( Resources.Load("BrickWall"), new Vector3(-22.5f + 5 * 1, 2.5f, -22.5f + 5 * 1), Quaternion.identity);
            //brickWall[1] = clone.GetComponent("MeshRenderer").renderer.enabled = false;
            //brickWall[1] = (GameObject)Instantiate(Resources.Load("BrickWall"), new Vector3(-22.5f + 5 * 2, 2.5f, -22.5f + 5 * 2), Quaternion.identity);
            //brickWall[2] = (GameObject)Instantiate(Resources.Load("BrickWall"), new Vector3(-22.5f + 5 * 3, 2.5f, -22.5f + 5 * 3), Quaternion.identity);
            //test.text = "";
        }
        catch (System.Exception e)
        {
            //Send null pointer error 
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        KeyboardInput();
        Decode();
    }

    void KeyboardInput()
    {
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
    }

    void Decode()
    {
        if(serverString != "" && serverString != game_client.data)
        {
            try
            {
                serverString = game_client.data;
                string[] lines = Regex.Split(serverString.Substring(0, serverString.Length - 1), ":");
                if (lines[0].StartsWith("S"))
                {
                    //Console.WriteLine(lines[1]); //Name
                    //Console.WriteLine(lines[2]); //Cordinate
                    string[] cord = Regex.Split(lines[2], ",");
                    //tank.transform.position = new Vector3(-17, 0, -15);
                    //tank.transform.position = new Vector3(-17 + 5 * Int32.Parse(cord[0]), 0, -15 + 5 * Int32.Parse(cord[1]));
                    test.text = "Tank Created";
                    //Console.WriteLine(lines[3]); //Direction
                    //Tank t = new Tank(lines[1], Int32.Parse(cord[0]), Int32.Parse(cord[1]));
                    //Arena.AddGameObject(t.x_cordinate, t.y_cordinate, t);
                }
                else if (lines[0].StartsWith("I"))
                {
                    //Initialize
                    string player_name = lines[1];
                    //lines[2] - Brick
                    string[] bricks = Regex.Split(lines[2], ";");
                    for (int i = 0; i < bricks.Length; i++)
                    {
                        string[] cords = Regex.Split(bricks[i], ",");
                        brickWall[i] = Instantiate(Resources.Load("BrickWall"), new Vector3(-22.5f + 5 * Int32.Parse(cords[0]), 2.5f, -22.5f + 5 * Int32.Parse(cords[1])), Quaternion.identity) as GameObject;
                    }
                    //lines[3] - Stone
                    string[] stones = Regex.Split(lines[3], ";");
                    for (int i = 0; i < stones.Length; i++)
                    {
                        string[] cords = Regex.Split(stones[i], ",");
                        stoneWall[i] = Instantiate(Resources.Load("StoneWall"), new Vector3(-22.5f + 5 * Int32.Parse(cords[0]), 2.5f, -22.5f + 5 * Int32.Parse(cords[1])), Quaternion.identity) as GameObject;
                    }
                    //lines[4] - Water
                    string[] waters = Regex.Split(lines[4], ";");
                    for (int i = 0; i < waters.Length; i++)
                    {
                        string[] cords = Regex.Split(waters[i], ",");
                        water[i] = Instantiate(Resources.Load("Water"), new Vector3(-22.5f + 5 * Int32.Parse(cords[0]), 0.01f, -22.5f + 5 * Int32.Parse(cords[1])), Quaternion.identity) as GameObject;
                    }
                }
                else if (lines[0].StartsWith("G"))
                {
                    for (int x = 0; x < 10; x++)
                    {
                        for (int y = 0; y < 10; y++)
                        {
                            //if (Arena.obj_map[x, y].type == Model.Type.TANK || Arena.obj_map[x, y].type == Model.Type.BRICK)
                            //{
                            //    Arena.obj_map[x, y] = new Floor(x, y);
                            //}
                        }
                    }

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].StartsWith("G"))
                        {

                        }
                        else if (lines[i].StartsWith("P"))
                        {
                            string[] player = Regex.Split(lines[i], ";");
                            //Tank t = new Tank(player[0], Int32.Parse(player[2]));
                            //string[] cord = Regex.Split(player[1], ",");
                            //t.x_cordinate = Int32.Parse(cord[0]);
                            //t.y_cordinate = Int32.Parse(cord[1]);
                            //t.shot = Int32.Parse(player[3]);
                            //t.health = Int32.Parse(player[4]);
                            //t.coins = Int32.Parse(player[5]);
                            //t.points = Int32.Parse(player[6]);
                            //Arena.AddGameObject(t.x_cordinate, t.y_cordinate, t);
                        }
                        else
                        {
                            string[] walls = Regex.Split(lines[i], ";");
                            for (int j = 0; j < walls.Length; j++)
                            {
                                string[] wall = Regex.Split(walls[j], ",");
                                if (Int32.Parse(wall[2]) != 4)
                                {
                                    //BrickWall b = new BrickWall(Int32.Parse(wall[0]), Int32.Parse(wall[1]), Int32.Parse(wall[2]));
                                    //Arena.AddGameObject(b.x_cordinate, b.y_cordinate, b);
                                }
                            }
                        }
                    }
                }
                //Arena.UpdateArena();
            }
            catch (System.Exception e)
            {

            }
        }        
    }

}
