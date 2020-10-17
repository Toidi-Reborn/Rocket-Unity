using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//delta time needed due to changing framerates
//frist letter CAP then type of var
//not cap then var itself


public class rocket : MonoBehaviour
{

    Rigidbody rigidbody; //defines var as a Rigidbody
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 200f;
    [SerializeField] float mainThrust = 50f;

    // Start is called before the first frame update
    void Start(){
        rigidbody = GetComponent<Rigidbody>();   //get the body from the object
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()    {
        Thrust();
        Rotate();  
    }


void OnCollisionEnter(Collision collision){

    switch (collision.gameObject.tag) {

        case "friend": 
            //do nothing - its my best friend
            break;


        default:
            break;
            


    }




        /*
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        if (collision.relativeVelocity.magnitude > 2)
            audioSource.Play();
        */
    
    }



    private void Rotate(){
        
        rigidbody.freezeRotation = true;  // take manual control of rotation
        
        float rotationSpeed = rcsThrust * Time.deltaTime;


        if (Input.GetKey(KeyCode.A)) {   //if key is pressed
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D)) {
            
            transform.Rotate(-Vector3.forward * rotationSpeed);

        }

        rigidbody.freezeRotation = false; // resume physics control of rotation



    }

    //private can only be called from our own code
    private void Thrust() {


        if (Input.GetKey(KeyCode.Space)) {
            print("thrust");

            rigidbody.AddRelativeForce(Vector3.up * mainThrust);
            if (!audioSource.isPlaying) {
            audioSource.Play();
            }
        }
        else {
            audioSource.Stop();
        }

    }





}
