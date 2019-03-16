using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
	private bool canShoot;

	private bool hit;

	public float throwSpeed;

	private Rigidbody2D rb;

	private GameObject knife;

	private TrailRenderer trail;
	

    // Start is called before the first frame update
    void Start()
    {
    	trail = gameObject.GetComponent<TrailRenderer>();
    	rb = gameObject.GetComponent<Rigidbody2D>();

        canShoot = true;
        hit = false;	//keeps track of whether or not the knife has made contact with either another knife or the log
        
        knife = (GameObject) Resources.Load("Prefabs/knife");
    }

    void OnTriggerEnter2D (Collider2D other) {

    	if (other.transform.tag == "log" && !hit)
    		LogHit(other.transform);

    	else if (other.transform.tag == "knifeButt" && !hit) {
    		KnifeHit();
    	}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canShoot && Input.GetKeyDown("space") && !Manager.manager.isGameOver()) {
        	Shoot();
        }
    }

    private void Shoot() {

    	rb.velocity = new Vector3 (-1f, 0f, 0f) * throwSpeed;
    	canShoot = false;
    }

    /* 
    Method is called when a log is hit
    sets log as parent
    increases log speed & score
	creates new knife & cleans up
	TODO: plays a sound from sound manager
    */
    private void LogHit (Transform parent) {    

    	trail.enabled = false;
    	parent.transform.parent.GetComponent<Log>().IncreaseSpeed(10f);

    	rb.velocity = new Vector3 (0f,0f,0f);
    	transform.parent = parent;

    	hit = true;
    	Manager.manager.IncreaseScore(1);

    	NewKnife();
    	this.enabled = false;
    }

    /*
    Method is called when another knife is hit
    destroys current knife and creates a new one
    TODO: plays a sound from sound manager
    */
    private void KnifeHit () {
    	hit = true;
    	NewKnife();
   		Manager.manager.LoseLife();
   		Destroy(this.gameObject);
    }

    private void NewKnife () {
    	Instantiate (knife);
    }
}
