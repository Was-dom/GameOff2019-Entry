using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
  [Header("VIEW")]
  private Rigidbody2D rb;
  public GameObject HurtParticles;
  public GameObject DashExplosion;
  public float MSpeed;
  public float DSpeed;
  public float DTime;
  public float DCool;
  public Text scoreText;
  public GameObject smBar;
  public GameObject Vignette;
  [Header("IGNORE")]
  public bool Dashing = false;
  public bool onCooldown = false;
  private float DTimeS;
  public float DCoolS;
  private bool facingRight = true;
  private Animator anim;
  public int score = 0;
  private float smLength = 2f;

  void Start () {
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    DTimeS = DTime;
    DCoolS = DCool;
    scoreText.text = "";
  }

  void Update () {
    smBar.transform.localScale = new Vector3(smLength * 5, 1f, 1f);
    if (score >= 0) scoreText.text = Mathf.Abs(score) + " Sales";
    if (score <= 0) scoreText.text = Mathf.Abs(score) + " Debt";
    //FaceMouse();
    float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
    Vector3 tempVect = new Vector3(h, v, 0);
		tempVect = tempVect.normalized * MSpeed * Time.deltaTime;
		rb.MovePosition(rb.transform.position + tempVect);

    transform.position = new Vector3(Mathf.Clamp(transform.position.x, -11f, 11f),
    Mathf.Clamp(transform.position.y, -11f, 11f), transform.position.z);

    Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector2 direction = (Vector2)((worldMousePos - transform.position));
    direction.Normalize();

    if (Dashing == true) {
      if (DTime >= 0.01f && onCooldown == false) {
        transform.position = Vector2.MoveTowards(transform.position, worldMousePos, DSpeed*Time.deltaTime);
        DTimeS -= Time.deltaTime;
      }
    }

    if (onCooldown == true) {
      DCoolS -= Time.deltaTime;
    }

    if (DTimeS <= 0.01f) {
      DTimeS = DTime;
      Dashing = false;
      onCooldown = true;
      GetComponent<SpriteRenderer>().color = new Color(100f, 100f, 100f, 1f);
      Instantiate(DashExplosion, transform.position, transform.rotation);
    }

    if (DCoolS <= 0.01f) {
      DCoolS = DCool;
      onCooldown = false;
    }

    if (Input.GetButtonDown("Fire1") && DCoolS == DCool) {
      GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0.2f);
      Instantiate(DashExplosion, transform.position, transform.rotation);
      Dashing = true;
      GameObject.FindWithTag("MainCamera").GetComponent<ShakeBehavior>().DashShake();
    }

    if (h != 0 || v != 0) {
      anim.SetBool("Running", true);
    }

		if(h < 0 && facingRight) {
		    Flip();
		} else if (h > 0 && !facingRight) {
		    Flip();
		}

    if (h == 0 && v == 0) {
      anim.SetBool("Running", false);
    }

    if (Input.GetKeyDown(KeyCode.Space) && smLength >= 0.01f) {
      Time.timeScale = 0.3F;
      Time.fixedDeltaTime = 0.02F * Time.timeScale;
    } else if (Input.GetKeyUp(KeyCode.Space) || smLength <= 0.01f) {
      Time.timeScale = 1F;
      Time.fixedDeltaTime = 0.02F;
      Vignette.GetComponent<SpriteRenderer>().color = new Color (255f, 255f, 255f, 0.22f);
    }
    if (Time.fixedDeltaTime == 0.02F && smLength <= 2f) {
      smLength += 1.5f * Time.deltaTime;
    } else if (Input.GetKey(KeyCode.Space) && smLength >= 0.01f) {
      smLength -= 2.5f * Time.deltaTime;
      Vignette.GetComponent<SpriteRenderer>().color += new Color (255f, 255f, 255f, 0.005f);
    }

    if (score <= -10) {
      SceneManager.LoadScene("menu");
      Destroy(this.gameObject);
    }
  }

  /*void FaceMouse() {
    Vector3 mousePosition = Input.mousePosition;
    mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
    Vector2 direction = new Vector2(
      mousePosition.x - transform.position.x,
      mousePosition.y - transform.position.y
    );

    transform.up = direction;
  }*/

  void OnTriggerEnter2D(Collider2D col) {
    if (col.gameObject.tag.Equals("Enemy") && Dashing != true) {
      score -= 1;
      Instantiate(HurtParticles, transform.position, Quaternion.identity);
      GameObject.FindWithTag("MainCamera").GetComponent<ShakeBehavior>().HurtShake();
    } else if (col.gameObject.tag.Equals("Enemy") && Dashing == true) {
      Destroy(col.gameObject);
    } else if (col.gameObject.tag.Equals("Enemy1") && Dashing != true && col.gameObject.GetComponent<EnemyController>().Dashing == true) {
      score -= 1;
      Instantiate(HurtParticles, transform.position, Quaternion.identity);
      GameObject.FindWithTag("MainCamera").GetComponent<ShakeBehavior>().HurtShake();
    } else if (col.gameObject.tag.Equals("Enemy3") && Dashing != true) {
      score -= 1;
      Instantiate(HurtParticles, transform.position, Quaternion.identity);
      GameObject.FindWithTag("MainCamera").GetComponent<ShakeBehavior>().HurtShake();
    }
  }

  void OnTriggerStay2D(Collider2D col) {
    if (col.gameObject.tag.Equals("Enemy4")) {
      score -= 1;
      Instantiate(HurtParticles, transform.position, Quaternion.identity);
      GameObject.FindWithTag("MainCamera").GetComponent<ShakeBehavior>().HurtShake();
    }
  }

  void Flip() {
    facingRight = !facingRight;
    Vector2 scale = transform.localScale;
    scale.x *= -1;
    transform.localScale = scale;
	}
}
