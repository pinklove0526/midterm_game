using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

    public float depth = 1;
    Player player;
    private void Awake() {
        
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        float screenVelocity = player.velocity.x / depth;
        Vector2 pos = transform.position;

        pos.x -= screenVelocity * Time.fixedDeltaTime;

        if(pos.x < -124)
        {
            pos.x = 45;
        }
        transform.position = pos;
    }
}
