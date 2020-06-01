using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DNA : MonoBehaviour
{
    //gene for colour
    public float r;
    public float g;
    public float b;
    public float s;
    public float timetodie = 0;
    bool dead = false;
    SpriteRenderer srenderer;
    Collider2D scollider;
    
    // Start is called before the first frame update
    void Start()
    {
        srenderer = GetComponent<SpriteRenderer>();
        scollider = GetComponent<Collider2D>();
        srenderer.color = new Color(r, b, g);
        this.transform.localScale = new Vector3(s, s, s);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        dead = true;
        timetodie = PopulationManager.elapsed;
        Debug.Log("Dead At : " + timetodie);
        srenderer.enabled = false;
        scollider.enabled = false;
    }
}
