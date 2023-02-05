using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereController : MonoBehaviour
{
 
    public GameObject planet;
    private SphereCollider _atmosfericCollider;
    private GameObject _spaceShip;
    private Rigidbody _spaceShipRb;

    // Start is called before the first frame update
    void Start()
    {
        _atmosfericCollider = GetComponent<SphereCollider>();
        _spaceShip = GameObject.FindGameObjectWithTag("Player");
        _spaceShipRb = _spaceShip.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update() {
    }
    
    void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player")) {            
            // Calculate a force that keeps the ship rotating around the planet following the planet's rotation
            Vector3 force = Vector3.Cross(_spaceShipRb.velocity, -planet.transform.up);
            _spaceShipRb.AddForce(force, ForceMode.Acceleration);
            
        }

    }
}
