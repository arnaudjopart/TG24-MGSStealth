using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfCoverDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var ray = new Ray(transform.position, transform.forward*-1f);
        _hasCover = Physics.Raycast(ray, _length, _layerMask);
        
    }
    [SerializeField] private float _length =.5f;
    [SerializeField] private LayerMask _layerMask;
    private bool _hasCover;

    public bool HasCover
    {
        get
        {
            return _hasCover;
        }
    }

}
