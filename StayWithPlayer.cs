using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayWithPlayer : MonoBehaviour
{

    private GameObject Player;
    public float Thingy = 1f;
    public bool LookAtPlayer;
    public float fSpeed;
    public bool ifDash;
    private float stayWithDash = 0.1f;

    void Awake() {
      Player = GameObject.Find("Player");
    }

    void Update() {
    if (Player != null) {
      if (ifDash == true && stayWithDash >= 0.01f) {
        stayWithDash -= Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, fSpeed*Time.deltaTime);
      }

      if (ifDash == false) {
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, fSpeed*Time.deltaTime);
      }
      if (Thingy >= 0.0001f) {
        Thingy -= Time.deltaTime;
      }
      if (LookAtPlayer == true) {
        Vector3 offset = Player.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, offset);
      }
    }
    }

    void OnTriggerStay2D(Collider2D col) {
      if (col.gameObject.tag.Equals("Player") && Thingy <= 0.1f) {
            Player.GetComponent<PlayerController>().score += 1;
            Destroy(this.gameObject); // Note to self : Destroy needs to go last when wanting to do something on death.
      }
    }
}
