using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_Grower : MonoBehaviour {

  public Transform grownPlants;
  public int currentLevel;
  public int currentWater = 0;
  public Sprite[] plantStages;
  public Sprite[] plantCanGrows;
  public SpriteRenderer spriteRenderer;
  public float growTime = 1f;
  public float maxGrowTime = 1f;
  public bool canGrow = true;
  public bool doneGrowing = false;

  void OnEnable() {
    currentLevel = 0;
    spriteRenderer.sprite = plantStages[currentLevel];
    // currentWater = 0;
  }

  void Update() {
    if (!canGrow && !doneGrowing) {
      growTime -= Time.deltaTime;
      if (growTime < 0) {

        growTime = maxGrowTime;
        canGrow = true;
        currentWater = currentLevel - 1;
        spriteRenderer.sprite = plantCanGrows[currentWater];
      }
    }
  }

  void UpdateLevel(int growthLevel) {
    if (canGrow && !doneGrowing) {
      if (currentLevel < plantStages.Length - 1) {
        currentLevel += growthLevel;
      }

      spriteRenderer.sprite = plantStages[currentLevel];

      if (gameObject.name == "Plant(Clone)") {
        CheckForPoint();
      }
      else if (gameObject.name == "Bush(Clone)" && currentLevel == 3) {
        CreateShield();
      }
      if (currentLevel == 3) {
        doneGrowing = true;
      }
      canGrow = false;
    }
  }

  void CreateShield() {
    transform.GetChild(1).gameObject.SetActive(true);
  }

  void CheckForPoint() {
    if (currentLevel == 3) {
      // canGrow = false;
      // doneGrowing = true;
      SendMessageUpwards("CalculatePoints", 1);
    }
  }
}
