using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    private Rigidbody _rigidBody;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update() {
    }

    void FixedUpdate () {
        // Rotate the planet around its y axis
        Quaternion rotation = Quaternion.AngleAxis(1, Vector3.up);

        if (_rigidBody != null) {
          _rigidBody.MoveRotation(rotation * _rigidBody.rotation);

        }
    }
    
}
