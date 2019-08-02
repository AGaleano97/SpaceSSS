using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}
public class PlayerController : MonoBehaviour
{
     private Rigidbody rb;

     

     public Boundary boundary;

     public float tilt;

     public GameObject shot;
     public Transform shotSpawn;
     public float fireRate;

     private GameController gameController;

     public int scoreValue;
     
     private float speed;

     private float nextFire;

     private AudioSource audioSource;

     void Start()
     {
          rb = GetComponent<Rigidbody>();
          audioSource = GetComponent<AudioSource>();
          GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
          if (gameControllerObject != null)
          {
            gameController = gameControllerObject.GetComponent <GameController>();
          }
          if (gameController == null)
          {
            Debug.Log ("Cannot find 'GameController' script");
          }
          speed = 10;
     }

     void Update ()
     {
         if (Input.GetButton("Fire1") && Time.time > nextFire)
         {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.Play ();
         }
     }

     void FixedUpdate()
     {
          float moveHorizontal = Input.GetAxis("Horizontal");
          float moveVertical = Input.GetAxis("Vertical");

          Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
          rb.velocity = movement * speed;

          rb.position = new Vector3
          (
              Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
              0.0f,
              Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
          );

          rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);

          if (Input.GetKeyDown(KeyCode.Escape))
          {
              Application.Quit();
          }
     }

     void OnTriggerEnter (Collider other)
     {
         if (other.gameObject.CompareTag("Pick Up"))
         {
             other.gameObject.SetActive (false);
             gameController.Addscore (scoreValue + 10);
         }
         if (other.gameObject.CompareTag("Boost"))
         {
             other.gameObject.SetActive (false);
             speed = 18;
         }
     }
}
