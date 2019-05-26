using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] private int points = 0;
    public int Points {
        get { return points; }
    }

}
