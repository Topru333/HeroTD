using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public static PlayerControll instance;

    [SerializeField] private int startTokensAmount = 0;
    [SerializeField] private int startHpAmount = 0;
    public Resource Tokens { get; private set; }
    public Resource HP { get; private set; }


    public void Start()
    {
        instance = this;
        Tokens = new Resource(startTokensAmount);
        HP = new Resource(startHpAmount);
    }

    public class Resource
    {
        public Resource(int value)
        {
            Value = value;
        }

        public int Value { get; set; }

    }

}


