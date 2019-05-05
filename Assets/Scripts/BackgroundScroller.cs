using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float scrollSpeed = -0.2f;
    [SerializeField] float tileSizeZ = 30f;

    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newPosition = Mathf.Repeat(scrollSpeed * Time.time, tileSizeZ);
        transform.position = startPosition + Vector3.forward * newPosition;
    }
}
