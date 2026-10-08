using System;
using UnityEngine;

public class FlappyPlayer : MonoBehaviour
{
    
        

    //Columns
    //spawn column with random set gap / height
    //move across screen
    //when orb passes gain point
    //game over if touch

    public GameObject player;
    public float hopForce = 1000f;

    public float jumpInterval = 0.8f;
    float currentJumpInterval;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    
    
    void Start()
    {
        currentJumpInterval = jumpInterval;
        //Bird
        //make orange
        TurnObjectCyan(player);

    }

    // Update is called once per frame
    void Update()
    {
        if (currentJumpInterval < 0)
        {
            PlayerHop();
            currentJumpInterval = jumpInterval;
        }

        currentJumpInterval -= Time.deltaTime;
    }

    private void PlayerHop()
    {
        player.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0) * hopForce);
    }

    public void TurnObjectCyan(GameObject go)
    {
        go.GetComponent<Renderer>().material.color = Color.cyan;
    }
}
