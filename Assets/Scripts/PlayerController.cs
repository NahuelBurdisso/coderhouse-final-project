using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float throttleForce = 2f;
    public ParticleSystem Explosion;
    public ParticleSystem Trail;

    private Rigidbody _rigidBody;

    void Start () {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void Update() {

      if (Input.GetKeyDown(KeyCode.Space))
      {
        Trail.Play();
        _rigidBody.AddForce(transform.forward * throttleForce, ForceMode.Impulse);
      }

      float horizontalInput = Input.GetAxis("Horizontal");

      if (horizontalInput != 0) {
        _rigidBody.AddForce(transform.right * horizontalInput, ForceMode.Acceleration);
        _rigidBody.AddTorque(transform.up * horizontalInput, ForceMode.Acceleration);
      }
      
    }

    // When the user presses the spacebar apply a force to the spaceship to move it forward
    void FixedUpdate()
    {
        // Rotate the spaceship to always face the direction of its velocity
        if (_rigidBody.velocity.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(_rigidBody.velocity);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.CompareTag("Planet")) {
        // Destroy the spaceship
        Destroy(gameObject);
        // Instantiate an Explosion
        Instantiate(Explosion, transform.position, transform.rotation);
      }
    }

    // Draw a gizmo of all the physics forces affecting the spaceship
    void OnDrawGizmos()
    {
      if (_rigidBody != null && transform != null) {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _rigidBody.velocity);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, _rigidBody.velocity * _rigidBody.mass);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, _rigidBody.velocity * _rigidBody.mass * _rigidBody.drag);
      }
    }

}
