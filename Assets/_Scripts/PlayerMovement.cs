using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Player Speed -with speedboost
    //for 3 seconds
  public float forwardSpeed = 10f;
  public float sideSpeed = 10f;
  public int speedTime = 3;

  public void StartSpeedBoost()
  {
    StartCoroutine(SpeedBoost());
  }


  public IEnumerator SpeedBoost()
  {
    forwardSpeed = 17;
    yield return new WaitForSeconds(speedTime);
    forwardSpeed = 12;
  }
    //no action by default
    public bool isMoving = false;
    public GameObject defeatMenu;
    public GameObject victoryMenu;
    public GameObject MainMenu;

    void Update()
  {
    if (!isMoving)
    {
            //start to move & keep moving -> if user start the game and not open the menu
            if (Input.GetMouseButtonDown(0) && MainMenu == null && defeatMenu.activeInHierarchy == false && victoryMenu.activeInHierarchy == false)
            { isMoving = true; }
      return;
    }

     
    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + forwardSpeed * Time.deltaTime);

    if (transform.position.x >= -2)
    {
      transform.position = new Vector3(transform.position.x - sideSpeed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    if (transform.position.x <= 2)
    {
      transform.position = new Vector3(transform.position.x + sideSpeed * Time.deltaTime, transform.position.y, transform.position.z);
    }

  }
}
