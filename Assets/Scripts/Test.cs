using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
    public int number1 = 2;
    public int number2 = 9;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Return))
            AddNumber();
	}

    void AddNumber()
    {
        Debug.Log(number1 + number2);
        number1++;
    }
}
