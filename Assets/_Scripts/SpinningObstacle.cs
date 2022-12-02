using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObstacle : MonoBehaviour
{
  public float speed = 100f;

  void Start()
  {
    transform.Rotate(0, Random.Range(0, 70), 0);
  }

  void Update()
  {
    transform.Rotate(0, speed * Time.deltaTime, 0);
  }
}
