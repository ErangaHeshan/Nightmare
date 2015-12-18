using UnityEngine;
using System.Collections;

public class CreateObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject createTank(int x,int y,int dir,string name)
    {
        GameObject newtank = Instantiate(Resources.Load("Prefabs/pre_tank"), new Vector3(10 * x, 0, 10 * y), Quaternion.identity) as GameObject;

        newtank.gameObject.GetComponent<Player>().x = x;
        newtank.gameObject.GetComponent<Player>().y = y;
        newtank.gameObject.GetComponent<Player>().direction = dir;
        newtank.gameObject.GetComponent<Player>().name = name;

        return newtank;
    }

    public GameObject createCoinPack(int x, int y,int value,int time)
    {
        GameObject coinpack = Instantiate(Resources.Load("Prefabs/pre_coin"), new Vector3(10 * x, 0, 10 * y), Quaternion.identity) as GameObject;

        coinpack.gameObject.GetComponent<CoinPack>().x = x;
        coinpack.gameObject.GetComponent<CoinPack>().y = y;
        coinpack.gameObject.GetComponent<CoinPack>().value = value;
        coinpack.gameObject.GetComponent<CoinPack>().time = time;

        return coinpack;
    }

    public GameObject createLifePack(int x, int y, int time)
    {
        GameObject lifepack = Instantiate(Resources.Load("Prefabs/pre_lifepack"), new Vector3(10 * x, 0, 10 * y), Quaternion.identity) as GameObject;

        lifepack.gameObject.GetComponent<LifePack>().x = x;
        lifepack.gameObject.GetComponent<LifePack>().y = y;
        lifepack.gameObject.GetComponent<LifePack>().time = time;

        return lifepack;
    }

    public GameObject createWater(int x, int y)
    {
        GameObject water = Instantiate(Resources.Load("Prefabs/pre_water"), new Vector3(10 * x, 0, 10 * y), Quaternion.identity) as GameObject;

        water.gameObject.GetComponent<Water>().x = x;
        water.gameObject.GetComponent<Water>().y = y;

        return water;
    }

    public GameObject createStone(int x, int y)
    {
        GameObject stone = Instantiate(Resources.Load("Prefabs/pre_stone"), new Vector3(10 * x, 0, 10 * y), Quaternion.identity) as GameObject;

        stone.gameObject.GetComponent<Stone>().x = x;
        stone.gameObject.GetComponent<Stone>().y = y;

        return stone;
    }

    public GameObject createBrick(int x, int y)
    {
        GameObject brick = Instantiate(Resources.Load("Prefabs/pre_brick"), new Vector3(10 * x, 0, 10 * y), Quaternion.identity) as GameObject;

        brick.gameObject.GetComponent<Brick>().x = x;
        brick.gameObject.GetComponent<Brick>().y = y;

        return brick;
    }

    public int getTankX(GameObject tank)
    {
        return (tank.gameObject.GetComponent<Player>().x);
    }
    public int getTankY(GameObject tank)
    {
        return (tank.gameObject.GetComponent<Player>().y);
    }

    public string getTankName(GameObject tank)
    {
        return (tank.gameObject.GetComponent<Player>().name);
    }

    public int getCoinPackTime(GameObject coinpack)
    {
        return (coinpack.gameObject.GetComponent<CoinPack>().time);
    }

    public int getCoinPackX(GameObject coinpack)
    {
        return (coinpack.gameObject.GetComponent<CoinPack>().x);
    }

    public int getCoinPackY(GameObject coinpack)
    {
        return (coinpack.gameObject.GetComponent<CoinPack>().y);
    }
    public int getLifePackTime(GameObject lifepack)
    {
        return (lifepack.gameObject.GetComponent<LifePack>().time);
    }

    public int getLifePackX(GameObject lifepack)
    {
        return (lifepack.gameObject.GetComponent<LifePack>().x);
    }

    public int getLifePackY(GameObject lifepack)
    {
        return (lifepack.gameObject.GetComponent<LifePack>().y);
    }

    public int getBrickX(GameObject brick)
    {
        return (brick.gameObject.GetComponent<Brick>().x);
    }
    public int getBrickY(GameObject brick)
    {
        return (brick.gameObject.GetComponent<Brick>().y);
    }

}
