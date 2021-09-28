using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpForce = 10.0f;
    private Rigidbody playerRb;
    public bool isOnGround = true;
    private float xRange = 13;
    private float horizontalInput;
    private int moreBread = 0;
    private int playerPosition = 0;

    // Start is called before the first frame update
    void Start()
    {
        //allow you to add forces to player
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PowerChicken();
    }

    private void PlayerMovement()
    {
        bool leftInput;
        bool rightInput;
        leftInput = Input.GetKeyDown(KeyCode.A);
        rightInput = Input.GetKeyDown(KeyCode.D);

        //There are 5 lanes (-lane1, -lane2, lane3, lane2, lane1)
        float lane1 = 12.5f;
        float lane2 = 6.5f;
        float lane3 = 0f;

        if (isOnGround == true && leftInput == true && playerPosition > -2)
        {
            playerPosition--;
            Debug.Log("Player Position = " + playerPosition);
        }

        if (isOnGround == true && rightInput == true && playerPosition < 2)
        {
            playerPosition++;
            Debug.Log("Player Position = " + playerPosition);
        }

        if (playerPosition == 0)
        {
            transform.position = new Vector3(lane3, transform.position.y, transform.position.z);
        }

        if(playerPosition == 1)
        {
            transform.position = new Vector3(lane2, transform.position.y, transform.position.z);
        }

        if(playerPosition == 2)
        {
            transform.position = new Vector3(lane1, transform.position.y, transform.position.z);
        }

        if (playerPosition == -1)
        {
            transform.position = new Vector3(-lane2, transform.position.y, transform.position.z);
        }

        if (playerPosition == -2)
        {
            transform.position = new Vector3(-lane1, transform.position.y, transform.position.z);
        }

        //Controls the players jumping
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround == true)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        //Checks to see if player is on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            Debug.Log("isOnGround");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bread"))
        {
            Debug.Log("Picked up power up");
            Destroy(other.gameObject);

            moreBread ++;
        }
        if (other.CompareTag("Fox"))
        {
            Debug.Log("Game Over");
        }
        if (other.CompareTag("Fence"))
        {
            Debug.Log("Get Pitted");
        }
    }

    private void PowerChicken()
    {
        if (moreBread == 3)
        {
            Debug.Log("ITS POWER CHICKEN TIME!");
        }
    }
}

