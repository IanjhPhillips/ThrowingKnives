using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{

	public float angularSpeed;

    void Update()
    {
        transform.Rotate(0f,0f,angularSpeed*Time.deltaTime);
    }

    public void IncreaseSpeed (float inc) {
        angularSpeed += inc;
    }
}
