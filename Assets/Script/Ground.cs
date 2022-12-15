using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Player player;
    public float groundPos;
    public float groundRight;
    public float screenRight;
    bool disGroundGenerator = false;
    public Obstacle obstacleTemplate;

    public float offsetObstacleY = 2.7f;
    

    BoxCollider2D collider;
    private void Awake() {
        player = GameObject.Find("Player").GetComponent<Player>();
        
        collider = GetComponent<BoxCollider2D>();
        groundPos = transform.position.y + (collider.size.y/2);
        screenRight = Camera.main.transform.position.x * 2f;
    }

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void FixedUpdate() {
        Vector2 pos = transform.position;
        pos.x -= player.velocity.x * Time.fixedDeltaTime;

        if(groundRight < -56f)
        {
            Destroy(gameObject);
            return;
        }
        
        groundRight = transform.position.x + (collider.size.x/2);
        if(!disGroundGenerator)
        {
            if(groundRight < screenRight)
            {   
                disGroundGenerator = true;
                groundGenerator();
            }
        }
        transform.position = pos;
    }

    void groundGenerator()
    {
        GameObject go =  Instantiate(gameObject);
        BoxCollider2D goCollider =  go.GetComponent<BoxCollider2D>();
        Vector2 pos;

        float h1 = player.jumpSpeed * player.maxHoldJumpTime; //max jump height without gravity
        float t =  player.jumpSpeed / -player.gravity; //Ratio of player velocity and gravity
        float h2 = player.jumpSpeed * t + (0.5f * (-player.gravity* (t * t))); // jump height with gravity
        float maxJumpHeight = h1 * h2; // acutal max jump height
        float maxPosY =  maxJumpHeight - 50f;  // plus float is for ignore tiny human errors
        maxPosY += groundPos;
        float minPosY= 1;
        float realPlatformPosY = Random.Range(minPosY, maxPosY-5f)  ;
        
        pos.y =  realPlatformPosY - goCollider.size.y /2;
        if(pos.y > -20f)
        {
            pos.y = -35f;
        }
        float t1= t + player.maxHoldJumpTime; 
        float t2 = Mathf.Sqrt((2.0f * (maxPosY - realPlatformPosY)) / -player.gravity);
        float totalTime = t1 + t2;
        float maxPosX = totalTime * player.velocity.x;
        maxPosX *= 0.7f; //plus float is for ignore tiny human errors
        maxPosX += groundRight;
        float minPosX = screenRight + 10f;
        float realPlatformPosX = Random.Range(minPosX, maxPosX - 10f);


        pos.x = realPlatformPosX + goCollider.size.x /2;
        go.transform.position = pos;

        Ground goGround = go.GetComponent<Ground>();
        goGround.groundPos = go.transform.position.y + (goCollider.size.y/2);



        int obstacleNumber =  Random.Range(0,2); // obstacles generator
        for (int i=0; i<obstacleNumber; i++)
        {
            GameObject newObstacle = Instantiate(obstacleTemplate.gameObject);
            float obstacleY = Random.Range(goGround.groundPos, goGround.groundPos + 7f);
            float halfHorizontal = goCollider.size.x /2 - 10f ;
            float left = go.transform.position.x - halfHorizontal;
            float right = go.transform.position.x + halfHorizontal;
            float obstacleX = Random.Range(left,right);
            Vector2 obstaclePos = new Vector2(obstacleX, obstacleY + offsetObstacleY);
            newObstacle.transform.position = obstaclePos;
        } 


    }
}
