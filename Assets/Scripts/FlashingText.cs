using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingText : MonoBehaviour
{
    [SerializeField] float timer;

    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.5)
        {
            text.enabled = true;
        }

        if (timer >= 2)
        {
            text.enabled = false;
            timer = 0;
        }
    }
}
