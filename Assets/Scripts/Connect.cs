using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

public class Connect : MonoBehaviour {
    //This object is used to create GameObjects
    CreateObject creator = new CreateObject();

    //To keep the list of tanks,coins,LifePacks,bricks,water & stones
    private static ArrayList coinPacks = new ArrayList();
    private static ArrayList lifePacks = new ArrayList();
    private static ArrayList tankList = new ArrayList();
    private static ArrayList brickList = new ArrayList();
    private static ArrayList waterList = new ArrayList();
    private static ArrayList stoneList = new ArrayList();

    public static ArrayList tankObjects = new ArrayList();
    public static ArrayList coinpackObjects = new ArrayList();
    public static ArrayList lifepackObjects = new ArrayList();
    public static ArrayList brickObjects = new ArrayList();
    public static ArrayList waterObjects = new ArrayList();
    public static ArrayList stoneObjects = new ArrayList();

    public static string[,] map = new string[10, 10];

    //this contains the number of tanks in the game
    int numberOfTanks = 0;
    int numberOfBricks = 0;

    public GameClient game_client;
    public string curMessage = "";
    public string prevMessage = "";
	void Start () {
        try
        {
            game_client = new GameClient("127.0.0.1");
            //Send join command to the server
            game_client.SendToServer("JOIN#");
            
        }
        catch (Exception e)
        {

        }
	}
	
	// Update is called once per frame
    void Update()
    {
        curMessage = game_client.currentMessage;
        if (!curMessage.Equals(prevMessage))
        {
            string[] lines = Regex.Split(curMessage, ":");
            if (curMessage.Substring(0, 1).Equals("S"))
            {
                decodeInitialPlayerLocations(curMessage);
            }
            else if (curMessage.Substring(0, 1).Equals("I"))
            {
                decodeObstacleLocations(curMessage);
            }
            else if (curMessage.Substring(0, 1).Equals("C"))
            {
                decodeCoinLocations(curMessage);
            }
            else if (curMessage.Substring(0, 1).Equals("G"))
            {
                decodeCurrentState(curMessage);
            }
            else if (curMessage.Substring(0, 1).Equals("L"))
            {
                decodeLifePacks(curMessage);
            }
            else
            {
                if (curMessage.Equals("OBSTACLE#"))
                {
                    Console.WriteLine("OBSTACLE :( ");
                }
                else if (curMessage.Equals("CELL_OCCUPIED#"))
                {
                    Console.WriteLine("CELL_OCCUPIED#");
                }
                else if (curMessage.Equals("DEAD#"))
                {
                    Console.WriteLine("DEAD#");
                }
                else if (curMessage.Equals("TOO_QUICK#"))
                {
                    Console.WriteLine("TOO_QUICK#");
                }
                else if (curMessage.Equals("INVALID_CELL#"))
                {
                    Console.WriteLine("INVALID_CELL#");
                }
                else if (curMessage.Equals("GAME_HAS_FINISHED#"))
                {
                    Console.WriteLine("GAME_HAS_FINISHED#");
                }
                else if (curMessage.Equals("GAME_NOT_STARTED_YET#"))
                {
                    Console.WriteLine("GAME_NOT_STARTED_YET#");
                }
                else if (curMessage.Equals("NOT_A_VALID_CONTESTANT#"))
                {
                    Console.WriteLine("NOT_A_VALID_CONTESTANT#");
                }

            }
        }
        prevMessage = curMessage;
    }

    void KeyBoard()
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



    //This section contains methods for instantiation of objects
    public static void update()
    {
        while (true)
        {
            updateCoinsAndLifepacks();
            Thread.Sleep(800);
        }
    }

    //this function would print out the current map on the Console
    public void printMap()
    {

        //foreach (Player tank in tankList)
        //{
        //    int x = tank.x;
        //    int y = tank.y;
        //    map[y, x] = "T";
        //}

        //foreach (GameObject tank in tankObjects)
        //{
        //    int x = creator.getTankX(tank);
        //    int y = creator.getTankY(tank);
        //    map[y, x] = "T";
        //}



        //Console.WriteLine("Printing map");
        //for (int x = 0; x < 10; x++)
        //{
        //    for (int y = 0; y < 10; y++)
        //    {
        //        Console.Write(map[x, y] + " ");
        //    }
        //    Console.WriteLine();
        //}
    }

    //this method would update the map with coins and life packs
    public void updateMap()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (map[x, y].Equals("C") || map[x, y].Equals("L"))
                {
                    map[x, y] = "_";
                }
            }
        }
        try
        {
            foreach (CoinPack item in coinPacks)
            {
                if (item.time > 0)
                {

                    map[item.y, item.x] = "C";
                }
                else
                {
                    coinPacks.Remove(item);
                }
            }

            foreach (GameObject coinpack in coinpackObjects)
            {
                if (coinpack != null)
                {
                    if (creator.getCoinPackTime(coinpack) > 0)
                    {
                        map[creator.getCoinPackY(coinpack), creator.getCoinPackX(coinpack)] = "C";
                    }
                    else
                    {
                        coinpackObjects.Remove(coinpack);
                        Destroy(coinpack);
                    }
                }
            }

        }
        catch (Exception e)
        {

        }

        try
        {
            foreach (LifePack item in lifePacks)
            {
                if (item.time > 0)
                {
                    map[item.y, item.x] = "L";
                }
                else
                {
                    lifePacks.Remove(item);
                }
            }

            foreach (GameObject lifepack in lifepackObjects)
            {
                if (lifepack != null)
                {
                    if (creator.getLifePackTime(lifepack) > 0)
                    {
                        map[creator.getLifePackY(lifepack), creator.getLifePackX(lifepack)] = "L";
                    }
                    else
                    {
                        lifepackObjects.Remove(lifepack);
                        Destroy(lifepack);
                    }
                }
                
            }
        }
        catch (Exception e)
        {

        }
    }

    //this would update the locations of the tanks
    public static void updateTanks()
    {
        foreach (Player tank in tankList)
        {
            int xCor = tank.x;
            int yCor = tank.y;
            map[yCor, xCor] = "_";

        }


    }

    //this method would decrement the time of life packs and update them
    public static void updateCoinsAndLifepacks()
    {
        foreach (CoinPack item in coinPacks)
        {
            item.time -= 1;
        }

        foreach (GameObject coinpack in coinpackObjects)
        {
            if (coinpack != null)
            {
                coinpack.gameObject.GetComponent<CoinPack>().time -= 1;
            }
            
        }

        foreach (LifePack item in lifePacks)
        {
            item.time -= 1;
        }

        foreach (GameObject lifepack in lifepackObjects)
        {
            if (lifepack != null)
            {
                lifepack.gameObject.GetComponent<LifePack>().time -= 1;
            }
        }
    }



    //this method would decode the information about the life packs recieved from the server
    private void decodeLifePacks(string data)
    {
        //L:<x>,<y>:<LT>#

        char[] delimeters = { ':', '#' };
        string[] lines = data.Split(delimeters);
        Console.WriteLine("Decoding lifepack locations");

        //L   <x>,<y>  <LT>

        char[] delimeters2 = { ',' };
        string[] location = lines[1].Split(delimeters2);

        //this would add the newly created life packs to the life packs list
        lifePacks.Add(new LifePack(Int32.Parse(location[0]), Int32.Parse(location[1]), 1 + (Int32.Parse(lines[2]) / 1000)));
        lifepackObjects.Add(creator.createLifePack(Math.Abs(9-Int32.Parse(location[0])), Int32.Parse(location[1]), 1 + (Int32.Parse(lines[2]) / 1000)));

        //this adds the life packs to the life pack array 
        map[Int32.Parse(location[1]), Int32.Parse(location[0])] = "L";
        updateMap();
        printMap();

    }

    //this would decode the message stating the current status of the server
    private void decodeCurrentState(string data)
    {
        //G  P1;<x>,<y>;<Dir>;<shotes?>;<health>;<coins>;<points>   ....   P5;<x>,<y>;<Dir>;<shotes?>;<health>;<coins>;<points>         <x>,<y>,<damage>;   .....   <x>,<y>,<damage># 

        char[] delimeters = { ':', '#' };
        char[] delimeters2 = { ';' };
        char[] delimeters3 = { ',' };
        string[] lines = data.Split(delimeters);

        Console.WriteLine("Decoding current state");

        //this for loop would traverse throught the list of string which has details about the tanks
        //and would decode the details and update relevent tanks in the tank list
        for (int x = 1; x < numberOfTanks; x++)
        {
            string[] playerDetails = lines[x].Split(delimeters2);
            Console.WriteLine("Decoding player details");

            string name = playerDetails[0];
            string[] cordinates = playerDetails[1].Split(delimeters3);
            int xCor = Int32.Parse(cordinates[0]);
            int yCor = Int32.Parse(cordinates[1]);
            int dir = Int32.Parse(playerDetails[2]);
            int shots = Int32.Parse(playerDetails[3]);
            string health = playerDetails[4];
            int coins = Int32.Parse(playerDetails[5]);
            int points = Int32.Parse(playerDetails[6]);

            updateTanks();
            foreach (Player tank in tankList)
            {
                if (tank.name.Equals(name))
                {
                    tank.x = xCor;
                    tank.y = yCor;
                    tank.direction = dir;
                    tank.health = health;
                    tank.coins = coins;
                    tank.points = points;

                    if (map[tank.y, tank.x].Equals("C"))
                    {
                        foreach (GameObject coinpack in coinpackObjects)
                        {
                            if (coinpack!=null && creator.getCoinPackX(coinpack) == tank.x && creator.getCoinPackY(coinpack) == tank.y)
                            {
                                coinpackObjects.Remove(coinpack);
                                Destroy(coinpack);
                            }
                        }

                        foreach (CoinPack item in coinPacks)
                        {
                            if (item.x == tank.x && item.y == tank.y)
                            {
                                item.time = -1;
                            }
                        }


                    }
                    else if (map[tank.y, tank.x].Equals("L"))
                    {
                        foreach (GameObject lifepack in lifepackObjects)
                        {
                            if (lifepack!=null && creator.getLifePackX(lifepack) == tank.x && creator.getLifePackY(lifepack) == tank.y)
                            {
                                coinpackObjects.Remove(lifepack);
                                Destroy(lifepack);
                            }
                        }

                        foreach (LifePack item in lifePacks)
                        {
                            if (item.x == tank.x && item.y == tank.y)
                            {
                                item.time = -1;
                            }
                        }

                    }

                    map[tank.y, tank.x] = "T";

                }
            }

            foreach (GameObject tank in tankObjects)
            {
                if (tank!=null && creator.getTankName(tank).Equals(name))
                {
                    tank.transform.position = new Vector3(10*xCor, 0, 10*yCor);
                    tank.gameObject.GetComponent<Player>().health = health;
                    tank.gameObject.GetComponent<Player>().direction = dir;
                    tank.gameObject.GetComponent<Player>().coins = coins;
                    tank.gameObject.GetComponent<Player>().points = points;
                }
            }
        }

        //This section would decode the details about damage levels of bridge
        string damage = lines[numberOfTanks];
        string[] damageBricks = damage.Split(delimeters2);
        try
        {
            for (int y = 0; y < damageBricks.Length; y++)
            {
                string[] dmg = damageBricks[y].Split(delimeters3);


                int dmgX = Int32.Parse(dmg[0]);
                int dmgY = Int32.Parse(dmg[1]);
                int dmgInt = Int32.Parse(dmg[2]);
                foreach (GameObject brick in brickObjects)
                {
                    if (brick != null)
                    {
                        if (creator.getBrickX(brick) == dmgX && creator.getBrickY(brick) == dmgY)
                        {
                            if (dmgInt == 0)
                            {
                                brick.gameObject.GetComponent<Brick>().damage = 0;
                            }
                            else if (dmgInt == 1)
                            {
                                brick.gameObject.GetComponent<Brick>().damage = 25;
                            }
                            else if (dmgInt == 2)
                            {
                                brick.gameObject.GetComponent<Brick>().damage = 50;
                            }
                            else if (dmgInt == 3)
                            {
                                brick.gameObject.GetComponent<Brick>().damage = 75;
                            }
                            else if (dmgInt == 4)
                            {
                                brick.gameObject.GetComponent<Brick>().damage = 100;
                                brickObjects.Remove(brick);
                                Destroy(brick);
                            }

                        }
                    }
                }


                foreach (Brick brick in brickList)
                {
                    if (brick.x == dmgX && brick.y == dmgY)
                    {
                        if (dmgInt == 0)
                        {
                            brick.damage = 0;
                        }
                        else if (dmgInt == 1)
                        {
                            brick.damage = 25;
                        }
                        else if (dmgInt == 2)
                        {
                            brick.damage = 50;
                        }
                        else if (dmgInt == 3)
                        {
                            brick.damage = 75;
                        }
                        else if (dmgInt == 4)
                        {
                            brick.damage = 100;
                            removeBrick();
                            map[brick.y, brick.x] = "_";
                        }

                    }
                }
            }
        }
        catch (Exception e)
        {

        }

        updateMap();
        printMap();
    }

    //Needs to be implemented
    //This method should remove a brick from the current list
    private void removeBrick()
    {
        throw new NotImplementedException();
    }

    //this method would decode the locations of the coins and their values.
    private void decodeCoinLocations(string data)
    {
        //C:<x>,<y>:<LT>:<Val>#
        char[] delimeters = { ':', '#' };
        string[] lines = data.Split(delimeters);

        Console.WriteLine("Decoding coin locations");

        char[] delimeters2 = { ',' };
        string[] location = lines[1].Split(delimeters2);

        coinPacks.Add(new CoinPack(Int32.Parse(location[0]), Int32.Parse(location[1]), 1 + (Int32.Parse(lines[2]) / 1000), Int32.Parse(lines[3])));
        coinpackObjects.Add(creator.createCoinPack(Math.Abs(9-Int32.Parse(location[0])), Int32.Parse(location[1]), Int32.Parse(lines[3]), 1 + (Int32.Parse(lines[2]) / 1000)));

        map[Int32.Parse(location[1]), Int32.Parse(location[0])] = "C";

        updateMap();
        printMap();
    }

    //this method would decode data about obstavcles such as bricks, stones & water
    private void decodeObstacleLocations(string data)
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                map[y, x] = "_";
            }
        }
        //I P1   <x>,<y>;<x>,<y>;<x>,<y>…..< x>,<y>    <x>,<y>;<x>,<y>;<x>,<y>…..<x>,<y>    <x>,<y>;<x>,<y>;<x>,<y>…..<x>,<y>#

        char[] delimeters = { ':', '#' };
        char[] delimeters2 = { ';' };
        char[] delimeters3 = { ',' };
        char[] delimeters4 = { ';', ',' };
        string[] lines = data.Split(delimeters);


        Console.WriteLine("Decoding obstacle locations");

        string bricksStr = lines[2];
        string stoneStr = lines[3];
        string waterStr = lines[4];

        string[] brickStrCordinates = bricksStr.Split(delimeters4);
        string[] stoneStrCordinates = stoneStr.Split(delimeters4);
        string[] waterStrCordinates = waterStr.Split(delimeters4);

        int xLoc = 0, yLoc = 0;
        numberOfBricks = brickStrCordinates.Length;
        for (int x = 0; x < brickStrCordinates.Length; x++)
        {

            if (x % 2 == 0)
            {
                xLoc = Int32.Parse(brickStrCordinates[x]);
            }
            else
            {
                yLoc = Int32.Parse(brickStrCordinates[x]);
                brickList.Add(new Brick(yLoc, xLoc));
                brickObjects.Add(creator.createBrick(Math.Abs(9-xLoc), yLoc));

                numberOfBricks += 1;
                map[yLoc, xLoc] = "B";
            }

        }

        for (int x = 0; x < stoneStrCordinates.Length; x++)
        {

            if (x % 2 == 0)
            {
                xLoc = Int32.Parse(stoneStrCordinates[x]);
            }
            else
            {

                yLoc = Int32.Parse(stoneStrCordinates[x]);

                stoneList.Add(new Stone(yLoc, xLoc));
                stoneObjects.Add(creator.createStone(Math.Abs(9 - xLoc), yLoc));

                map[yLoc, xLoc] = "S";
            }

        }

        for (int x = 0; x < waterStrCordinates.Length; x++)
        {

            if (x % 2 == 0)
            {
                xLoc = Int32.Parse(waterStrCordinates[x]);
            }
            else
            {
                yLoc = Int32.Parse(waterStrCordinates[x]);
                waterList.Add(new Water(yLoc, xLoc));
                waterObjects.Add(creator.createWater(Math.Abs(9 - xLoc), yLoc));
                map[yLoc, xLoc] = "W";
            }

        }

        updateMap();
        printMap();
    }

    //this method decodes data about 
    private void decodeInitialPlayerLocations(string data)
    {
        // S:P0;0,0;0:P1;0,9;0#
        char[] delimeters = { ':', '#' };
        char[] delimeters2 = { ';' };
        char[] delimeters3 = { ',' };
        char[] delimeters4 = { ';', ',' };

        string[] lines = data.Split(delimeters);

        int numberOfPlayers = lines.Length - 1;
        numberOfTanks = numberOfPlayers;
        for (int x = 1; x < numberOfPlayers; x++)
        {
            // S  P0;0,0;0   P1;0,9;0
            string[] lines2 = lines[x].Split(delimeters2);
            string[] my_tankXY = lines2[1].Split(delimeters3);

            string name = lines2[0];
            int xLoc = Int32.Parse(my_tankXY[0]);
            int yLoc = Int32.Parse(my_tankXY[1]);
            int direction = Int32.Parse(lines2[2]);
            tankList.Add(new Player(name, xLoc, yLoc, direction));

            tankObjects.Add(creator.createTank(Math.Abs(90-xLoc*10), yLoc*10, direction, name));

            Debug.Log("Tank added");
            map[yLoc, xLoc] = "T";

        }
        //Update map
        updateMap();

        //Print map
        printMap();

    }

    //return the list of coin packs
    public ArrayList getCoinPackArray()
    {
        return coinPacks;
    }

    //return the list of tanks
    public ArrayList getTankArray()
    {
        return tankList;
    }

    //return the list of life packs
    //this method would return the LifePackArray
    public ArrayList getLifePackArray()
    {
        return lifePacks;
    }

    //this method would return the current brick pack array
    public ArrayList getBrickArray()
    {
        return brickList;
    }

    //this method would return the current water location array
    public ArrayList getWaterArray()
    {
        return waterList;
    }

    //this method would return the stone array
    public ArrayList getStoneArray()
    {
        return stoneList;
    }
    

}
