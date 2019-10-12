using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Point_Counter : MonoBehaviour {

  public Text pointsUI;
  public Text gameOverPointsUI;
  public GameObject gameWinUI;
  public GameObject doggo;
  public int points;
  public int maxPoints;
  public int spawnDoggo;
  public Vector2[] spawnLocations;
  void CalculatePoints(int point) {
    points += point;
    pointsUI.text = points.ToString() + " Plant Babies";
    gameOverPointsUI.text = "You grew " + points.ToString() + " Plant Babies";

    if (points % spawnDoggo == 0) {
      int randomNumber = Mathf.RoundToInt(Random.Range(0, spawnLocations.Length - 1));
      Instantiate(doggo, spawnLocations[randomNumber], Quaternion.identity);
    }

    if (points == maxPoints) {
      gameWinUI.SetActive(true);
    }
  }
}
