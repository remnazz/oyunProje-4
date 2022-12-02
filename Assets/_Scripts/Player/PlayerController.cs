using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{
  private AudioSource audioSource;

  public AudioClip collectCube;
  public AudioClip collectGem;
  public AudioClip speedBoost;
  public AudioClip victory;
  public AudioClip defeat;

  public GameObject character;
  public GameObject defeatMenu;
  public GameObject victoryMenu;
  public TMP_Text scoreText;


  void Start()
  {
    audioSource = GetComponent<AudioSource>();
  }


    //Action stops and game objects are closed
    public void GameOver()
    {
        FindObjectOfType<PlayerMovement>().isMoving = false;
        FindObjectOfType<GameManager>().gemCounter.SetActive(false);
        FindObjectOfType<GameManager>().progressBar.SetActive(false);
        FindObjectOfType<GameManager>().gameButtons.SetActive(false);
    }





    //cube collecting
    private void AddCube(Collider other)
    {

        for (int i = 0; i < other.transform.childCount; i++)
        {

            //when cube is detect, new cube is cloned
            other.transform.GetChild(i).GetComponent<BoxCollider>().enabled = true;
            other.transform.GetChild(i).GetComponent<Rigidbody>().useGravity = true;


            if (transform.tag == "Player")
            {
                var childCube = Instantiate(other.transform.GetChild(i).gameObject, transform.position, Quaternion.identity, transform);
                transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                childCube.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
                var animator = childCube.transform.Find("+1").GetComponent<Animator>();
                if (animator != null) { animator.SetBool("collected", true); }
            }
            else
            {
                var childCube = Instantiate(other.transform.GetChild(i).gameObject, transform.position, Quaternion.identity, transform.parent);
                transform.parent.position = new Vector3(transform.parent.position.x, transform.parent.position.y + 1, transform.parent.position.z);
                childCube.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
                var animator = childCube.transform.Find("+1").GetComponent<Animator>();
                if (animator != null) { animator.SetBool("collected", true); }
            }
        }

        Destroy(other.gameObject);

    }









    private void OnTriggerEnter(Collider other)
  {

    if (other.tag == "Cubes")
    {
      audioSource.clip = collectCube;
      audioSource.Play();
      other.enabled = false;
      AddCube(other);
    }

    if (other.tag == "Gem")
    {
      audioSource.clip = collectGem;
      audioSource.Play();
      FindObjectOfType<GameManager>().gemCount++;
      Destroy(other.gameObject);
      
    }

    if (other.tag == "SpeedBoost")
    {
      other.enabled = false;
      audioSource.clip = speedBoost;
      audioSource.Play();
      FindObjectOfType<PlayerMovement>().StartSpeedBoost();

    }

        //There are several multiplier platforms, if cubes are destroyed after first finish line, cube should be detect and game should be over with victory
        //but if player still have some cubes, player goes to second finish line
        //and if cubes are destroyed on one of the multiplayer platforms, game should end with victory.


        //VICTORY ON FINISH LINE
        if (other.tag == "FinishLine")
    {
      GameOver();
            
      if (gameObject.tag == "Player")
      {
        character.GetComponent<Animation>().CrossFade("Victory");
        victoryMenu.SetActive(true);
             //setting score according to final multiplier platforms
        scoreText.text = ("SCORE: " + ((other.GetComponent<MultiplierHandler>().multiplierValue - 1) * FindObjectOfType<GameManager>().gemCount).ToString());
      }

      if (gameObject.tag == "Cube")
      {
        character.GetComponent<Animation>().CrossFade("Victory");
        transform.parent.GetComponent<PlayerController>().victoryMenu.SetActive(true);
        transform.parent.GetComponent<PlayerController>().scoreText.text = ("SCORE: " + ((other.GetComponent<MultiplierHandler>().multiplierValue - 1) * FindObjectOfType<GameManager>().gemCount).ToString());
      }
      audioSource.clip = victory;
      audioSource.Play();

    }

        //If game over before finish line, because of an obstacle, game should be end with defeat.

    if (gameObject.tag == "Player")
    {
      if (other.tag == "Obstacle" || other.tag == "SpinningObstacle" || other.tag == "Lava")
      {
        character.GetComponent<Animation>().CrossFade("Defeat");
        other.enabled = false;
        transform.GetComponent<BoxCollider>().enabled = false;
        transform.GetComponent<Rigidbody>().useGravity = false;
        GameOver();
        defeatMenu.SetActive(true);
        audioSource.clip = defeat;
        audioSource.Play();

      }



      if (other.tag == "Multiplier")
      {
        GameOver();
        character.GetComponent<Animation>().CrossFade("Victory");
        victoryMenu.SetActive(true);
        audioSource.clip = victory;
        audioSource.Play();

        if (other.GetComponent<MultiplierHandler>().multiplierValue - 1 == 0)
        { scoreText.text = "SCORE: " + FindObjectOfType<GameManager>().gemCount.ToString(); }
        else
        { scoreText.text = ("SCORE: " + ((other.GetComponent<MultiplierHandler>().multiplierValue - 1) * FindObjectOfType<GameManager>().gemCount).ToString()); }
      }

    }
  }


 
}


