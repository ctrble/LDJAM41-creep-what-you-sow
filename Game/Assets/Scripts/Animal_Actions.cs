using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal_Actions : MonoBehaviour {

  public Rigidbody2D rigidBody2D;
  public GameObject target;
  public GameObject alertSprite;
  public Sprite[] stateSprites;
  public SpriteRenderer spriteRenderer;
  public Transform lookSprite;
  public float thinkTime = 1f;
  public float maxThinkTime = 1f;

  public Vector2 newDirection;
  public AudioSource audioSource;
  public AudioClip audioClip;

  public LayerMask playerLayer;
  public LayerMask wallLayer;

  void OnEnable() {
    spriteRenderer.sprite = stateSprites[0];
    audioSource = gameObject.GetComponent<AudioSource>();
  }

  void Update() {
    thinkTime -= Time.deltaTime;

    if (target == null) {
      target = FindTarget(newDirection);
      // Move(newDirection);
    }
    else {
      Debug.Log("has target");
      StartCoroutine(AlertIcon());
    }

    if (thinkTime < 0) {

      newDirection = GetDirection();

      if (target == null) {
        // target = FindTarget(newDirection);
        Move(newDirection);
      }
      else {
        // Debug.Log("kill");
        // StartCoroutine(AlertIcon());
        Vector2 targetDirection = target.transform.position - transform.position;
        Move(targetDirection.normalized);
      }

      thinkTime = maxThinkTime;
    }
  }

  IEnumerator AlertIcon() {
    alertSprite.gameObject.SetActive(true);
    spriteRenderer.sprite = stateSprites[1];
    if (!audioSource.isPlaying) {
      audioSource.PlayOneShot(audioClip, 1);
    }
    yield return new WaitForEndOfFrame();
    alertSprite.gameObject.SetActive(false);
    spriteRenderer.sprite = stateSprites[0];
  }

  bool IsBlocked(Vector2 direction) {
    Vector2 position = transform.position;
    float distance = 1.45f;
    RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, wallLayer);
    if (hit.collider != null) {
      return true;
    }
    return false;
  }

  Vector2 GetDirection() {
    int x = Mathf.RoundToInt(Random.Range(-1.45f, 1.45f));
    int y = Mathf.RoundToInt(Random.Range(-1.45f, 1.45f));
    Vector2 direction = new Vector2(x, y);
    return direction;
  }

  void Move(Vector2 direction) {
    Debug.DrawRay(transform.position, direction, Color.red);

    if (IsBlocked(direction)) {
      target = null;
      return;
    }

    lookSprite.transform.localPosition = direction;
    transform.Translate(direction);
  }

  GameObject FindTarget(Vector2 direction) {
    Vector2 position = transform.position;
    float distance = 10f;

    Debug.DrawRay(position, direction, Color.green);
    RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, playerLayer | wallLayer);
    if (hit.collider != null && hit.collider.CompareTag("Player")) {
      return hit.collider.gameObject;
    }
    return null;
  }
}
