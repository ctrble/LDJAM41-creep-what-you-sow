using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

  public int currentSpeed;
  public int minSpeed;
  public int maxSpeed;
  private string runKey = "left shift";
  private string upKey = "w";
  private string downKey = "s";
  private string leftKey = "a";
  private string rightKey = "d";
  private Vector2 upV2;
  private Vector2 downV2;
  private Vector2 leftV2;
  private Vector2 rightV2;
  public GameObject up;
  public GameObject down;
  public GameObject left;
  public GameObject right;
  public LayerMask wallLayer;
  void OnEnable() {

    UpdateSpeed(minSpeed);
  }

  void Update() {

    // IsBlocked();

    if (Input.GetKeyDown(runKey)) {
      UpdateSpeed(maxSpeed);
    }
    if (Input.GetKeyUp(runKey)) {
      UpdateSpeed(minSpeed);
    }
    if (Input.GetKeyDown(upKey)) {
      if (IsBlocked(Vector2.up)) {
        return;
      }
      transform.Translate(upV2);
      UpdateDirection("up");
    }
    if (Input.GetKeyDown(downKey)) {
      if (IsBlocked(Vector2.down)) {
        return;
      }
      transform.Translate(downV2);
      UpdateDirection("down");
    }
    if (Input.GetKeyDown(leftKey)) {
      if (IsBlocked(Vector2.left)) {
        return;
      }
      transform.Translate(leftV2);
      UpdateDirection("left");
    }
    if (Input.GetKeyDown(rightKey)) {
      if (IsBlocked(Vector2.right)) {
        return;
      }
      transform.Translate(rightV2);
      UpdateDirection("right");
    }
  }

  bool IsBlocked(Vector2 direction) {
    Vector2 position = transform.position;
    float distance = 1.0f;
    RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, wallLayer);
    if (hit.collider != null) {
      return true;
    }
    return false;
  }

  void UpdateSpeed(int speed) {
    currentSpeed = speed;
    upV2 = new Vector2(0, currentSpeed);
    downV2 = new Vector2(0, -currentSpeed);
    leftV2 = new Vector2(-currentSpeed, 0);
    rightV2 = new Vector2(currentSpeed, 0);
  }

  void UpdateDirection(string direction) {
    if (direction == "up") {
      up.SetActive(true);
      down.SetActive(false);
      left.SetActive(false);
      right.SetActive(false);
    }
    if (direction == "down") {
      up.SetActive(false);
      down.SetActive(true);
      left.SetActive(false);
      right.SetActive(false);
    }
    if (direction == "left") {
      up.SetActive(false);
      down.SetActive(false);
      left.SetActive(true);
      right.SetActive(false);
    }
    if (direction == "right") {
      up.SetActive(false);
      down.SetActive(false);
      left.SetActive(false);
      right.SetActive(true);
    }
  }
}
