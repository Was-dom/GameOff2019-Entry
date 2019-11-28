using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehavior : MonoBehaviour
{
  // Transform of the GameObject you want to shake
    private Transform transform;
  // Desired duration of the shake effect
    private float shakeDuration = 0f;
  // A measure of magnitude for the shake. Tweak based on your preference
    private float shakeMagnitude = 0.1f;
  // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 6f;
  // The initial position of the GameObject
    Vector3 initialPosition;

    void Awake() {
      if (transform == null) {
        transform = GetComponent(typeof(Transform)) as Transform;
      }
    }

    void OnEnable() {
      initialPosition = transform.localPosition;
    }

    void Update() {
      if (shakeDuration > 0) {
        transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
        shakeDuration -= Time.deltaTime * dampingSpeed;
      }
      else
      {
        shakeDuration = 0f;
        transform.localPosition = initialPosition;
      }
    }

    public void DashShake() {
      shakeDuration = 0.8f;
      shakeMagnitude = 0.15f;
    }

    public void HurtShake() {
      shakeDuration = 0.9f;
      shakeMagnitude = 0.2f;
    }

    public void EDeathShake() {
      shakeDuration = 1.0f;
      shakeMagnitude = 0.5f;
    }

    public void LightningShake() {
      shakeDuration = 0.8f;
      shakeMagnitude = 0.7f;
    }

    public void JumpShake() {
      shakeDuration = 0.6f;
      shakeMagnitude = 0.5f;
    }
}
