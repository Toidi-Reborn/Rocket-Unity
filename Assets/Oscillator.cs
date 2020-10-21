using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]  //Tells the script only one allowed
public class Oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f; // set full cycle to 2 seconds

    [Range(0,1)]
    [SerializeField]
    float moveFactor;  //0 no move, 1 full move
    Vector3 startPos;
    void Start(){
        startPos = transform.position;  //taking from transfor on object

    }

    // Update is called once per frame
    void Update(){
        if (period <= Mathf.Epsilon) {return;}
        float cycles = Time.time / period; //grows from 0
        const float tau = Mathf.PI * 2f;
        float rawSinWave = Mathf.Sin(cycles * tau);
        

        moveFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = moveFactor * movementVector;
        transform.position = startPos + offset;




    }
}
