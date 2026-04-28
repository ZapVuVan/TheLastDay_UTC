using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    [SerializeField] Transform startPoint;


    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        _lineRenderer.SetPosition(0, startPoint.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, - transform.up, out hit))
        {
            if (hit.collider)
            {
                _lineRenderer.SetPosition(1, hit.point);
            }
            if(hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player hit!");
            }
        }
        else
        {
            _lineRenderer.SetPosition(1, - transform.up * 100f);
        }
    } 
}
