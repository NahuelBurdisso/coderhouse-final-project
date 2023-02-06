using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private SphereCollider _planetCollider;

    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;

    private float _maxHeight;
    private float _minHeight;
    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _planetCollider = GetComponent<SphereCollider>();
        _meshFilter = GetComponent<MeshFilter>();       
        _maxHeight = _planetCollider.radius;
        _minHeight = _planetCollider.radius * 0.5f;

    }
z
    // Update is called once per frame
    void Update()
    {
      // rotate the planet around the y axis
      transform.Rotate(0, 0.1f, 0);
        
    }
}
