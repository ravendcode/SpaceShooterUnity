using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float tilt;

	[Header("Fire")]
    [SerializeField] GameObject shot;
    [SerializeField] Transform shotSpawn;
	[SerializeField] float fireRate = 0.5f;

    float xMin = -6f;
    float xMax = 6f;
    float zMin = -4f;
    float zMax = 8f;

    Rigidbody rb;
    AudioSource shotSfx;

	float nextFire = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        shotSfx = GetComponent<AudioSource>();
    }

	void Update()
	{
		if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
		{
			nextFire = fireRate + Time.time;
			GameObject cloneShot = Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject;
            shotSfx.Play();
		}
	}

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed;
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, xMin, xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, zMin, zMax)
        );
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
