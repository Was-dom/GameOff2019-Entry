using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyScript : MonoBehaviour
{
    public float Length;
    public bool Enemy4Attack = false;
    public bool BossAttack = false;
    public bool BossDeathP = false;
    public float UntilHitBox = 0.5f;

    void Update() {
      Length -= Time.deltaTime;

      if (Length <= 0.01f && BossDeathP == false) {
        Destroy(this.gameObject);
      } else if (Length <= 0.01f && BossDeathP == true) {
        SceneManager.LoadScene("menu");
        Destroy(this.gameObject);
      }

      if (Enemy4Attack == true) {

        if (UntilHitBox >= 0.01f) {
          UntilHitBox -= Time.deltaTime;
        } else if (UntilHitBox <= 0.01f) {
          GetComponent<CircleCollider2D>().enabled = true;
        }
      }

      if (BossAttack == true) {

        if (UntilHitBox >= 0.01f) {
          UntilHitBox -= Time.deltaTime;
        } else if (UntilHitBox <= 0.01f) {
          GetComponent<BoxCollider2D>().enabled = true;
        }
      }
    }
}
