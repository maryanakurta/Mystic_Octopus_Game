using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public float moveSpeed;

    void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0, 0);

        if(transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }
}
