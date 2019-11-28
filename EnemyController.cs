using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("DETERMINE TYPE")]
    public bool Enemy1;
    public bool Enemy2;
    public bool Enemy3;
    public bool Enemy4;
    public bool Boss1;

    [Header("CODE")]
    public GameObject BDParticles;
    public float attackTimer1 = 2f;
    public float attackTimer2 = 4f;
    public float attackTimer3 = 7f;
    public GameObject Attack2SpawnObj;
    public GameObject Attack2Obj;
    public GameObject Attack3Obj;
    private GameObject Player;
    private Transform target;
    private GameObject dashTarget;
    private Animator anim;
    private float SpawnTime = 0.5f;
    private bool Spawned = false;
    public GameObject DeathParticles;
    public GameObject DeathText;
    public GameObject Attack;
    public bool Dashing = false;
    public bool onCooldown = false;
    public float DTime;
    public float DCool;
    private float DTimeS;
    private float DCoolS;
    public float DSpeed = 40f;
    public float MSpeed = 8f;
    private float shootSpeed = 0.1f;
    public float bulletVelocity = 5f;
    public float AttackDelay = 5f;
    private bool ifAttacked = false;
    private bool BossAttack1 = false;
    private bool BossAttack2 = false;
    private bool BossAttack3 = false;
    private bool BossAttack2Spawn = false;
    private bool BA3a = false;
    private bool BA3b = false;
    private bool BA3c = false;
    private bool iA1D = false;
    private bool iA2D = false;
    private Vector3 pastPlayerPos;
  	//public float travelSpeed = 8f;
  	public GameObject Projectile;
  	public GameObject Projectile1;
    private int HP = 15;

    void Awake() {
      Player = GameObject.Find("Player");
      dashTarget = GameObject.Find("DashTarget");
      target = Player.transform;
      anim = GetComponent<Animator>();

      DTimeS = DTime;
      DCoolS = DCool;
    }

    void Update() {
      // Boss1
      if (Player != null && Boss1 == true) {
        if (HP <= 0) {
          Instantiate(BDParticles, transform.position, transform.rotation);
          Destroy(this.gameObject);
        }

        if (attackTimer1 <= 1.65f) {
          MSpeed = 4f;
        }

        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, MSpeed*Time.deltaTime);

        if(target.position.x > transform.position.x) {
          transform.localScale = new Vector3(1,1,1);
        } else if (target.position.x < transform.position.x) {
          transform.localScale = new Vector3(-1,1,1);
        }

        attackTimer1 -= Time.deltaTime;
        if (attackTimer1 <= 0.01f && BossAttack2 == false && BossAttack3 == false) {
          anim.SetBool("Attack1", true);
          attackTimer1 = 2f;
          BossAttack1 = true;
          GameObject.FindWithTag("MainCamera").GetComponent<ShakeBehavior>().JumpShake();
        } else if (attackTimer1 <= 1.25f && BossAttack2 == false && BossAttack3 == false) {
          anim.SetBool("Attack1", false);
          BossAttack1 = false;
          if (iA1D == false) {
            iA1D = true;
          }
        } else if (attackTimer1 <= 1.9f && BossAttack2 == false && BossAttack3 == false) {
          MSpeed = 30f;
        }

        if (iA1D == true) {
        attackTimer2 -= Time.deltaTime;
        if (attackTimer2 <= 0.01f && BossAttack1 == false && BossAttack3 == false) {
          anim.SetBool("Attack2", true);
          attackTimer2 = 4f;
          BossAttack2 = true;
        } else if (attackTimer2 <= 2.58f && BossAttack1 == false && BossAttack3 == false) {
          anim.SetBool("Attack2", false);
          BossAttack2 = false;
          BossAttack2Spawn = false;
        } else if (attackTimer2 <= 3f && BossAttack2Spawn == false && BossAttack1 == false && BossAttack3 == false) {
          GameObject.FindWithTag("MainCamera").GetComponent<ShakeBehavior>().LightningShake();
          Instantiate(Attack2Obj, Attack2SpawnObj.transform.position, Attack2SpawnObj.transform.rotation);
          BossAttack2Spawn = true;
          if (iA2D == false) {
            iA2D = true;
          }
        }
        }

        if (iA2D == true) {
        attackTimer3 -= Time.deltaTime;
        if (attackTimer3 <= 0.01f && BossAttack1 == false && BossAttack2 == false) {
          BossAttack3 = true;
          attackTimer3 = 5f;
          BA3a = false;
          BA3b = false;
          BA3c = false;
          Instantiate(Attack3Obj, Player.transform.position, Quaternion.identity);
          GameObject.FindWithTag("MainCamera").GetComponent<ShakeBehavior>().LightningShake();
        } else if (attackTimer3 <= 4.5f && BossAttack1 == false && BossAttack2 == false && BA3a == false) {
          Instantiate(Attack3Obj, Player.transform.position, Quaternion.identity);
          GameObject.FindWithTag("MainCamera").GetComponent<ShakeBehavior>().LightningShake();
          BA3a = true;
        } else if (attackTimer3 <= 4f && BossAttack1 == false && BossAttack2 == false && BA3b == false) {
          Instantiate(Attack3Obj, Player.transform.position, Quaternion.identity);
          GameObject.FindWithTag("MainCamera").GetComponent<ShakeBehavior>().LightningShake();
          BA3b = true;
        } else if (attackTimer3 <= 3.5f && BossAttack1 == false && BossAttack2 == false && BA3c == false) {
          Instantiate(Attack3Obj, Player.transform.position, Quaternion.identity);
          GameObject.FindWithTag("MainCamera").GetComponent<ShakeBehavior>().LightningShake();
          BA3c = true;
        } else if (attackTimer3 <= 3f && BossAttack1 == false && BossAttack2 == false) {
          BossAttack3 = false;
        }
        }
      }
      // Enemies
      if (SpawnTime >= 0.01f) {
        SpawnTime -= Time.deltaTime;
        if (Enemy3 == true && Player != null) {
          Vector3 offset = target.position - transform.position;
          transform.rotation = Quaternion.LookRotation(Vector3.forward, offset);
        }
      }

      if (Enemy2 == true && Spawned == true && Player != null) {
        if (shootSpeed >= 0.01f) {
          shootSpeed -= Time.deltaTime;
        } if (shootSpeed <= 0.01f) {
          Vector3 playerPos = target.transform.position;
  				Vector2 direction = (Vector2)((playerPos - transform.position));
  				direction.Normalize();
  				GameObject Projectile = (GameObject)Instantiate(Projectile1, transform.position + (Vector3)(direction * 0.5f), Quaternion.identity);
  				Projectile.GetComponent<Rigidbody2D>().velocity = direction * bulletVelocity;
  				shootSpeed = 1f;
        }
      }


      if (Player != null) {
        if (Enemy4 == true || Enemy2 == true) {
          if(target.position.x > transform.position.x) {
            transform.localScale = new Vector3(1,1,1);
          } else if (target.position.x < transform.position.x) {
            transform.localScale = new Vector3(-1,1,1);
          }

          if (Enemy4 == true) {
            AttackDelay -= Time.deltaTime;
            if (AttackDelay <= 0.01f) {
              AttackDelay = 1.5f;
              ifAttacked = false;
              anim.SetBool("isStopped", false);
            } else if (AttackDelay <= 1f) {
              anim.SetBool("isStopped", true);
            }
            if (AttackDelay <= 0.7f && ifAttacked == false) {
              Instantiate(Attack, target.position, transform.rotation);
              ifAttacked = true;
            }
          }
        }
        /*Vector3 offset = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, offset);*/
        if (SpawnTime <= 0.01f) {
          Spawned = true;
          if (Enemy3 == false && AttackDelay >= 1f && Boss1 == false) {
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, MSpeed*Time.deltaTime);
            anim.SetBool("isDone", true);
          }
        }

      if (Enemy3 == true && Spawned == true) {
        transform.position += transform.up * MSpeed * Time.deltaTime;
      }

      if (Enemy1 == true) {
        if (Dashing == true) {
          if (DTime >= 0.01f && onCooldown == false) {
            transform.position = Vector2.MoveTowards(transform.position, dashTarget.transform.position, DSpeed*Time.deltaTime);
            DTimeS -= Time.deltaTime;
          }
        }

        if (onCooldown == true) {
          DCoolS -= Time.deltaTime;
          Dashing = false;
        }

        if (DTimeS <= 0.01f) {
          DTimeS = DTime;
          Dashing = false;
          onCooldown = true;
          //Instantiate(DashParticles, transform.position, transform.rotation);
        }

        if (DCoolS <= 0.01f) {
          DCoolS = DCool;
          onCooldown = false;
        }

        if (DCoolS == DCool && SpawnTime <= 0.01f) {
          //Instantiate(DashParticles, transform.position, transform.rotation);
          //Instantiate(DashParticles2, transform.position, transform.rotation);
          Dashing = true;
          GameObject.FindWithTag("MainCamera").GetComponent<ShakeBehavior>().DashShake();
        }
      }
      }
    }

    void OnTriggerStay2D(Collider2D col) {
      if (col.gameObject.tag.Equals("Player") && Spawned == true) {
        if (col.gameObject.GetComponent<PlayerController>().Dashing == true) {
          if (Boss1 == false) {
            Instantiate(DeathParticles, transform.position, Quaternion.identity);
            Instantiate(DeathText, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 0f));
            GameObject.FindWithTag("MainCamera").GetComponent<ShakeBehavior>().EDeathShake();
            Destroy(this.gameObject); // Note to self : Destroy needs to go last when wanting to do something on death.
          }
        }
      }
    }

    void OnTriggerEnter2D(Collider2D col) {
      if (col.gameObject.tag.Equals("Player") && Spawned == true) {
        if (col.gameObject.GetComponent<PlayerController>().Dashing == true) {
          if (Boss1 == true) {
           Instantiate(DeathParticles, transform.position, Quaternion.identity);
           Instantiate(DeathText, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 0f));
           GameObject.FindWithTag("MainCamera").GetComponent<ShakeBehavior>().EDeathShake();
           HP -= 1; // Note to self : Destroy needs to go last when wanting to do something on death.
          }
        }
      }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    /*void OnTriggerExit2D(Collider2D col) {
      if (col.gameObject.tag.Equals("Player") && SpawnTime <= 0.01f) {
        if (col.gameObject.GetComponent<PlayerController>().Dashing == true) {
          //if (DeathParticles != null || DeathText != null) {
            Instantiate(DeathParticles, transform.position, Quaternion.identity);
            Instantiate(DeathText, transform.position, Quaternion.identity);
            Destroy(this.gameObject); // Note to self : Destroy needs to go last when wanting to do something on death.
          //}
        }
      }
    }*/
}
