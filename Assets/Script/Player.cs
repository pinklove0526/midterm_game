using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public float gravity;
    public Vector2 velocity;
    public float distance = 0;
    public  float acceleration = 10;
    public float maxRunSpeed = 100;
    public float maxAccelearation = 10;
    public float jumpSpeed = 40;
    public float groundPos;
    public bool ground = false;
    public bool holdJump = false;
    public float maxHoldJumpTime = 0.3f;
    
    public float HoldJumpTimer = 0.0f;

    public float jumpThreshold = 1;
    
    public bool gameOver = false;

    public int playerLife;
    public LayerMask groundLayermask;
    public LayerMask obstacleLayermask;

    // Start is called before the first frame update
    void Start()
    {
        if(MenuController.Difficulty == 1)
        {
            playerLife = 3;
        }
        else if(MenuController.Difficulty == 3)
        {
            playerLife = 1;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        float aboveGround = Mathf.Abs(pos.y -  groundPos);
        
        if(ground|| aboveGround <= jumpThreshold)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                ground = false;
                velocity.y = jumpSpeed;
                holdJump = true;
                HoldJumpTimer = 0;
            }
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            holdJump = false;
        }
    }
    private void FixedUpdate() 
    {
        Vector2 pos = transform.position;


      
        if(!ground)
        {
            pos.y += velocity.y * Time.fixedDeltaTime;
            if(!holdJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }
         

            Vector2 rayOrigin =  new Vector2(pos.x + 0.5f, pos.y);
            Vector2 rayDirection =  Vector2.up; //negative gravity so up mean down on screen
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D  hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, groundLayermask);
            if(hit2D.collider != null)
            {
                Ground platform = hit2D.collider.GetComponent<Ground>();
                if(platform != null)
                {   
                    if(pos.y >= platform.groundPos)
                    {    
                        groundPos = platform.groundPos;
                        pos.y = groundPos;
                        velocity.y = 0;
                        ground = true;
                    }
                
                }
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);

            Vector2 wallOrgingin = new Vector2(pos.x, pos.y);
            RaycastHit2D wallHit = Physics2D.Raycast(wallOrgingin, Vector2.right, velocity.x * Time.fixedDeltaTime, groundLayermask);
            if(wallHit.collider != null)
            {
                Ground sidePlatform = wallHit.collider.GetComponent<Ground>();
                if(sidePlatform != null)
                {
                    if(pos.y < sidePlatform.groundPos)
                    {
                        velocity.x  =  0;
                    }
                }


            }



            if(holdJump)
            {
                HoldJumpTimer += Time.fixedDeltaTime;
                if(HoldJumpTimer>= maxHoldJumpTime)
                {
                    holdJump = false;
                }
            }

        }   
        distance += velocity.x * Time.fixedDeltaTime;
        if(ground)
        {
            float accelerationRate = velocity.x / maxRunSpeed;
            acceleration = maxAccelearation * (1 - accelerationRate);
            velocity.x += acceleration * Time.fixedDeltaTime;


            if(velocity.x >= maxRunSpeed)
            {
                velocity.x = maxRunSpeed;
            }
            Vector2 rayOrigin =  new Vector2(pos.x - 1f, pos.y);
                Vector2 rayDirection =  Vector2.up; //negative gravity so up mean down on screen
                float rayDistance = velocity.y * Time.fixedDeltaTime;
                RaycastHit2D  hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
                if(hit2D.collider == null)
                {
                ground = false;
                }
                Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);
                

        }
        
        Vector2 obstacleOrigin = new Vector2(pos.x, pos.y);
        RaycastHit2D obstacleHitX = Physics2D.Raycast(obstacleOrigin, Vector2.right, velocity .x* Time.fixedDeltaTime, obstacleLayermask); 
        if(obstacleHitX.collider != null)
        {
            Obstacle obstacle = obstacleHitX.collider.GetComponent<Obstacle>();
            if(obstacle != null)
            {
                hitObstacle(obstacle);
            }
                
        }
        RaycastHit2D obstacleHitY = Physics2D.Raycast(obstacleOrigin, Vector2.up, velocity .y* Time.fixedDeltaTime, obstacleLayermask); 
        if(obstacleHitY.collider != null)
        {
            Obstacle obstacle = obstacleHitY.collider.GetComponent<Obstacle>();
            if(obstacle != null)
            {
                hitObstacle(obstacle);
            }
        }
          if(pos.y < -50 || playerLife == 0)
        {
            gameOver = true;

        }
        if(gameOver)
        {
            velocity.x = 0;
            return;
        }        

        transform.position = pos;
    }
    void hitObstacle(Obstacle obstacle)
    {
            Destroy(obstacle.gameObject);
            velocity.x *= 0.7f;
            playerLife -= 1;
            // gameOver = true;
    }
    
}
