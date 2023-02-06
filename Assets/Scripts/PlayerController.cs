using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float throttleForce = 5f;
    public float timeToThrottle = 0.5f;
    public ParticleSystem Explosion;
    public ParticleSystem Trail;

    private Rigidbody _rigidBody;

    private MeshCollider _meshCollider;

    private bool _canThrottle = true;



    void Start () {
        _rigidBody = GetComponent<Rigidbody>();

        _meshCollider = GetComponent<MeshCollider>();

        // position the trail particle system directly where the spaceship mesh collider ends (in the back)
        Trail.transform.position = _meshCollider.bounds.center + (_meshCollider.bounds.extents.z - 2) * transform.forward;
    }

    void Update () {
        // If the player presses the space bar, throttle the spaceship
        if (Input.GetKeyDown(KeyCode.Space)) {
            Throttle();
        }

        // get horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0) {
            _rigidBody.AddTorque(transform.up * horizontalInput * 0.5f, ForceMode.Acceleration);
        }

    }

    void Throttle () {
        // If the spaceship is not throttling, throttle it
        if (_canThrottle) {
            // Apply a force to the spaceship
            _rigidBody.AddForce(transform.forward * throttleForce, ForceMode.Impulse);            
            // Disable the throttle for a while
            _canThrottle = false;
            Invoke("EnableThrottle", timeToThrottle);

            Trail.Play();
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

    void EnableThrottle()
    {
      _canThrottle = true;
    }

}
