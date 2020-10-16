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


    // Start is called before the first frame update
    void Start(){
        rigidbody = GetComponent<Rigidbody>();   //get the body from the object
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()    {
    
        processUpdate();  
    }


    //private can only be called from our own code
    private void processUpdate() {
        if (Input.GetKey(KeyCode.Space)) {
            print("thrust");

            rigidbody.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying) {
            audioSource.Play();
            }
        }
        else {
            audioSource.Stop();
        }


        
        // thrust and rotate can happen same time so seperate if statement
        if (Input.GetKey(KeyCode.A)) {   //if key is pressed
            print("Rotate Left");
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D)) {
            print("Rotate Right");
            transform.Rotate(-Vector3.forward);

        }
    }


}
