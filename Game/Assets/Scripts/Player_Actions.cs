using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Actions : MonoBehaviour {

  public GameObject gameOverUI;
  public GameObject gameWinUI;
  public Rigidbody2D rigidBody2D;
  public Collider2D hurtBox;
  public Collider2D hitBox;
  public GameObject target;
  public LayerMask plantLayer;
  public LayerMask groundLayer;
  public Collider2D[] collisionResults;
  public Collider2D[] shieldResults;
  public int currentSeed;
  public GameObject[] seeds;
  public Transform[] plantParents;
  public SpriteRenderer switchSprite;
  public Sprite[] switchSprites;
  private string actionKey = "space";
  private string toggleKey = "e";

  void Update() {

    CheckForShield();

    if (Input.GetKeyDown(toggleKey)) {
      StartCoroutine(ChangeAction());
    }
    if (Input.GetKeyDown(actionKey)) {
      DoAction();
    }
  }

  IEnumerator ChangeAction() {
    switchSprite.gameObject.SetActive(true);
    if (currentSeed < seeds.Length - 1) {
      currentSeed += 1;
    }
    else {
      currentSeed = 0;
    }
    switchSprite.sprite = switchSprites[currentSeed];
    yield return new WaitForSecondsRealtime(1f);
    switchSprite.gameObject.SetActive(false);
  }

  void DoAction() {

    ChooseTarget();

    switch (target.name) {
      case "Dirt":
        Vector3 plantingVector3 = new Vector3(hitBox.transform.position.x, hitBox.transform.position.y, hitBox.transform.position.z);
        target = Instantiate(seeds[currentSeed], plantingVector3, Quaternion.identity, plantParents[currentSeed]);
        break;
      case "Plant(Clone)":
        target.SendMessage("UpdateLevel", 1);
        break;
      case "Bush(Clone)":
        target.SendMessage("UpdateLevel", 1);
        break;
      case "Path":
        Debug.Log("nothing - path");
        break;
      case "Wall":
        Debug.Log("nothing - wall");
        break;
      default:
        Debug.Log("nothing");
        break;
    }
  }

  GameObject ChooseTarget() {

    hitBox = gameObject.transform.GetChild(1).GetComponentInChildren<Collider2D>();

    ContactFilter2D interactFilter = new ContactFilter2D();
    interactFilter.SetLayerMask(plantLayer | groundLayer);

    if (hitBox.OverlapCollider(interactFilter, collisionResults) > 0) {
      foreach (Collider2D collisionResult in collisionResults) {
        if (collisionResult.gameObject.CompareTag("Growable")) {
          target = collisionResult.gameObject;
          return target;
        }
        else if (collisionResult.gameObject.CompareTag("Ground")) {
          target = collisionResult.gameObject;
          return target;
        }
      }
    }
    return null;
  }

  void CheckForShield() {

    ContactFilter2D interactFilter = new ContactFilter2D();
    interactFilter.SetLayerMask(plantLayer);

    if (hurtBox.OverlapCollider(interactFilter, shieldResults) > 0) {
      foreach (Collider2D shieldResult in shieldResults) {
        if (shieldResult.transform.childCount == 2 && shieldResult.gameObject.transform.GetChild(1).CompareTag("Shield")) {
          hurtBox.tag = "Untagged";
        }
        else {
          hurtBox.tag = "Player";
        }
      }
    }
    else {
      hurtBox.tag = "Player";
    }
  }

  void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.CompareTag("Dog")) {
      Debug.Log("dead");
      gameObject.SetActive(false);
      if (gameWinUI.activeInHierarchy == false) {
        gameOverUI.SetActive(true);
      }
    }
  }
}
