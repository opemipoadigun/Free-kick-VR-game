using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalkeeper : MonoBehaviour
{
    public int[] Pos;

    public int Move;

    public int index;

    Animator gk;

    // Start is called before the first frame update
    void Start()
    {
        gk = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Move: " + Move + ", index: " + index);

        if ( Move == 0)
        {
            Reset();
        }

        if ( Move == 1) 
        {
            SaveR();
        }

        if ( Move == 2) 
        {
            SaveL();
        }
    }

    public void GoalMove()
    {
        index = Random.Range(0, Pos.Length);
        Move = Pos[index];
    }

    public void SaveR()
    {
        gk.SetFloat("Save", 0.5f);
    }

    public void SaveL()
    {
        gk.SetFloat("Save", 1.0f);
    }

    public void Reset()
    {
        gk.SetFloat("Save", 0f);
    }
}
