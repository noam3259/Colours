using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    
    public CharacterController controller;
    public float magnitude = 6f;
    public float jumpSpeed;
    public float originalStepOffset;
    public float bumpSpeed;
    public float frictionValue;
    public GameObject Noam;
    public GameObject Diamond;


    public Transform cam;
    private Vector3 direction;
    private float ySpeed;
    //private float xSpeed;
    //private float zSpeed;
    private bool touchesRed;
    private bool touchesBlue;


    void Start()
    {
        originalStepOffset = controller.stepOffset;
        touchesRed = false;
        touchesBlue = false;
        direction = new Vector3(0, 0, 0);
    }



    // Update is called once per frame
    void Update()
    {
        Diamond.transform.Rotate(Vector3.up,0.5f);
        //float horizontal = Input.GetAxisRaw("Horizontal");
        //float vertical = Input.GetAxisRaw("Vertical");
        //Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        float sum = Mathf.Abs(direction.x) + Mathf.Abs(direction.z);
        float angle = gameObject.transform.localEulerAngles.x;

        if (touchesRed==false && sum<2)//if the player got bumped (or touches red) don't reset the direction vector
        {

            direction = new Vector3(0, 0, 0);

            if (Input.GetKey(KeyCode.W)) 
            {
                if (direction != new Vector3(0, 0, 0)) //if another button is held and there is a vector in some direction, calc new vector from both buttons
                {
                    Vector3 horizontal = new Vector3(cam.forward.x, 0, cam.forward.z).normalized;//take the horizontal vector, then normalize for constant length
                    direction = (direction + horizontal) / 2;
                }
                else //if only one button is held the direction is in it's direction
                {
                    direction.x = cam.forward.x;//take the horizontal vector, then normalize for constant length
                    direction.z = cam.forward.z;
                    direction.Normalize();
                }
                //Debug.Log("Direction: " + direction);
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (direction != new Vector3(0, 0, 0)) //if another button is held and there is a vector in some direction, calc new vector from both buttons
                {
                    Vector3 horizontal = new Vector3(cam.forward.x, 0, cam.forward.z).normalized;//take the horizontal vector, then normalize for constant length
                    direction = (direction - horizontal) / 2;
                }
                else //if only one button is held the direction is in it's direction
                {
                    direction.x = -cam.forward.x;//take the horizontal vector, then normalize for constant length
                    direction.z = -cam.forward.z;
                    direction.Normalize();

                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                Vector3 dir = cam.right;
                if ((angle > 78.223 && angle < 180) || (angle < 271.777 && angle > 181))//we reverse the vector in this angle but we want the right/left axis to stay same so reverse again
                {
                    dir = -dir;
                }
                if (direction != new Vector3(0,0,0)) //if another button is held and there is a vector in some direction, calc new vector from both buttons
                {
                    direction = (direction + dir) / 2;
                }
                else //if only one button is held the direction is in it's direction
                {
                    direction = dir;
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                Vector3 dir = - cam.right;
                if ((angle > 78.223 && angle < 180) || (angle < 271.777 && angle > 181))//we reverse the vector in this angle but we want the right/left axis to stay same so reverse again
                {
                    dir = -dir;
                }
                if (direction != new Vector3(0, 0, 0)) //if another button is held and there is a vector in some direction, calc new vector from both buttons
                {
                    direction = (direction + dir) / 2;
                }
                else //if only one button is held the direction is in it's direction
                {
                    direction = dir;
                }
            }

        }
        //Debug.Log(gameObject.transform.localEulerAngles.x);
        if (((angle > 78.223 && angle < 180) || (angle < 271.777 && angle >181)) && touchesRed == false && sum < 2)
        {// the camera goes over the player and the vector goes to the opposite side
             direction = -direction;
            
        }

        ySpeed += Physics.gravity.y * Time.deltaTime;
        if (controller.isGrounded && touchesRed==false)//if the player touches red the yspeed is set to 10 so we avoid resetting the speed to -0.5fwd
        {
            controller.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            if (Input.GetKeyDown(KeyCode.Space))
            {

                ySpeed = jumpSpeed;
                //Noam.GetComponent<Animator>().Play("Idle");
                //Noam.GetComponent<Animator>().Play("Animation",1-1,0f);
                
                //Noam.GetComponent<Animator>().StartPlayback();
            }
        }
        else
        {
            controller.stepOffset = 0;
            //touchesRed = false;
        }
        if (sum > 2)//if the player got bumped decrease his velocity because of the friction
        {
            if (direction.x > 0)//if the velocity on the X axis is positive, reduce it, and if it's negative, increase it
            {
                if ((direction.x - frictionValue * Time.deltaTime) < 0)//friction applies until the velocity is 0
                {
                    direction.x = 0;
                }
                else
                {
                    direction.x -= frictionValue * Time.deltaTime;
                }
            }
            else
            {
                if ((direction.x + frictionValue * Time.deltaTime) > 0)
                {
                    direction.x = 0;
                }
                else
                {
                    direction.x += frictionValue * Time.deltaTime;
                }
            }

            if (direction.z > 0)
            {
                if ((direction.z - frictionValue * Time.deltaTime) < 0)//friction applies until the velocity is 0
                {
                    direction.z = 0;
                }
                else
                {
                    direction.z -= frictionValue * Time.deltaTime;
                }
            }
            else
            {
                if ((direction.z + frictionValue * Time.deltaTime) > 0)
                {
                    direction.z = 0;
                }
                else
                {
                    direction.z += frictionValue * Time.deltaTime;
                }
            }
        }

        if (touchesRed==false)
        {
            Vector3 velocity = direction * magnitude;
            velocity.y = ySpeed;
            controller.Move(velocity * Time.deltaTime);
        }
        else
        {
            controller.Move(direction * Time.deltaTime);
            touchesRed = false;
        }
        
    }

    private void LateUpdate()
    {
        if (SceneManager.GetActiveScene().name!="openingScene")
        {
            Noam.transform.LookAt(2 * Noam.transform.position - transform.position);//Noam looks at the player
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //What will happen to the player based on the hit color
        Color HitColor = hit.transform.GetComponent<Renderer>().sharedMaterial.color;


        if (HitColor == Color.red)//bump the player
        {
            FindObjectOfType<AudioManager>().Play("jumpSound");
            //direction = transform.position - hit.gameObject.transform.position;
            //direction.Normalize();
            //direction = direction * bumpSpeed;
            //ySpeed = bumpSpeed;
            touchesRed = true;
            direction = transform.position - hit.point;
            direction = direction.normalized * bumpSpeed;
            //Debug.Log("direction: " + direction);
            ySpeed = direction.y;
            //xSpeed = direction.x;
            //zSpeed = direction.z;
            
        }

        if (HitColor == Color.blue && magnitude == 6f)//speed the player
        {
            magnitude = magnitude * 3;
            touchesBlue = true;
        }
        if (HitColor!=Color.blue)
        {
            touchesBlue = false;
        }

        if (controller.isGrounded && touchesBlue==false && magnitude != 6f)//When the player touches something else returns to normal speed
        {
            magnitude = magnitude / 3;
        }


    }
}



