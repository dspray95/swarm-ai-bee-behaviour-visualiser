using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

    List<Coordinate> locations;
    List<bool> alive;
    bool dead;
    int i = 0;
    SpriteRenderer renderer;
    AgentType type;
    // Use this for initialization
    void Start()
    {
        renderer = (SpriteRenderer)transform.GetComponent("SpriteRenderer");
    }

	// Update is called once per frame
	void FixedUpdate () {

        if (i < locations.Count)
        {
            if (alive[i])
            {
                Coordinate currentPos = locations[i];
                transform.position = new Vector2(currentPos.x, currentPos.y);
            }
            else if (!dead)
            {
                renderer.color = Color.blue;
                dead = true;
            }
            i++;
        }
        else
        {
            if (type.Equals(AgentType.APID))
            {
                renderer.color = Color.white;
            }
            else
            {
                renderer.color = Color.red;
            }
            
            i = 0;
            dead = false;
        }
	}

    public void SetLocations(List<Coordinate> l)
    {
        this.locations = l;
    }

    public void SetAliveList(List<bool> alive)
    {
        this.alive = alive;
    }

    public void SetAgentType(AgentType type)
    {
        this.type = type; 
    }
}
