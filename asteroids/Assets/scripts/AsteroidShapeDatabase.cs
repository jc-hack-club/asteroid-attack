using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidShapeDatabase : MonoBehaviour
{


    public Sprite[] normalSprites;



    //https://gamedev.stackexchange.com/questions/116009/in-unity-how-do-i-correctly-implement-the-singleton-pattern
    private static AsteroidShapeDatabase _instance;


    //https://gamedev.stackexchange.com/questions/116009/in-unity-how-do-i-correctly-implement-the-singleton-pattern
    public static AsteroidShapeDatabase Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AsteroidShapeDatabase>();
            }

            return _instance;
        }
    }


}
