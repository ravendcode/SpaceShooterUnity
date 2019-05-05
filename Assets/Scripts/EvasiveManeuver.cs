using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour
{
    [SerializeField] float tilt;
    [SerializeField] float dodge;
    [SerializeField] float smoothing;
    [SerializeField] Vector2 startWait;
    [SerializeField] Vector2 maneuverTime;
    [SerializeField] Vector2 maneuverWait;

    private float currentSpeed;
    private float targetManeuver;
    private Rigidbody rb;
    private PlayerController player;

    float xMin = -6f;
    float xMax = 6f;
    float zMin = -20f;
    float zMax = 20f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerController>();
        currentSpeed = rb.velocity.z;
        StartCoroutine(Evade());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
        while (true)
        {
            if (player != null)
            {
                targetManeuver = player.transform.position.x;
            }
            else
            {
                targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            }

            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

    void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, smoothing * Time.deltaTime);
        rb.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, xMin, xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, zMin, zMax)
        );

        rb.rotation = Quaternion.Euler(0, 0, rb.velocity.x * -tilt);
    }
}
