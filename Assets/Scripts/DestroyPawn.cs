using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPawn : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject playerExplosion;
    [SerializeField] int scoreValue = 150;

    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy")) {
            return;
        }
        if (other.CompareTag("Player")) {
            GameObject instPlayerExplosion = Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            Destroy(instPlayerExplosion, 2f);
            gameController.GameOver();
        }
        gameController.AddScore(scoreValue);
        if (explosion != null)
        {
            GameObject instExplosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(instExplosion, 2f);
        }
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}
