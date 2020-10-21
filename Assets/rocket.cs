using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



//delta time needed due to changing framerates
//frist letter CAP then type of var
//not cap then var itself


public class rocket : MonoBehaviour
{

    Rigidbody rigidbody; //defines var as a Rigidbody
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 200f;
    [SerializeField] float mainThrust = 50f;
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip win;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem winParticles;
    
    [SerializeField] ParticleSystem mainEngineParticles;
    
    enum State { Alive, Dying, Transcending }
    State state = State.Alive;


    bool collisionOff = false;



    // Start is called before the first frame update
    void Start(){
        rigidbody = GetComponent<Rigidbody>();   //get the body from the object
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()    {
        if (state == State.Alive) {
            ThrustInput();
            RotateInput();  
        }

        if (Debug.isDebugBuild){
            RespondToDebugKey();
        }

    }


    void RespondToDebugKey(){
        if (Input.GetKeyDown(KeyCode.L)) {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C)){

            //toggle collision
            collisionOff = !collisionOff;

        }


    }


    void OnCollisionEnter(Collision collision){


        if (state != State.Alive  || collisionOff) {
            return;  // do not continue executing this function
        }


        switch (collision.gameObject.tag) {

            case "friend": 
                //do nothing - its my best friend
                break;
            case "Finish":
                StartWin();
                break;

            case "Dead":
                StartDeath();
                break;

            default:
                break;
        }
            /*???????
            foreach (ContactPoint contact in collision.contacts)
            {
                Debug.DrawRay(contact.point, contact.normal, Color.white);
            }
            if (collision.relativeVelocity.magnitude > 2)
                audioSource.Play();
            */
    }

    private void StartWin(){
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(win);
        winParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay);

    }

    private void StartDeath(){
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        
        Invoke("LoadFirstLevel", levelLoadDelay);

    }


    private void LoadNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex += 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);

    }

    private void LoadFirstLevel() {
        SceneManager.LoadScene(0);
    }

    private void RotateInput(){
        
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
    private void ThrustInput() {
        if (Input.GetKey(KeyCode.Space)) {
            ApplyThrust();
        }
        else {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust(){
        rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        
        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(mainEngine);
        }
        
        mainEngineParticles.Play();
    }


}
