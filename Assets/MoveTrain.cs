using UnityEngine;
using System.Collections;
public class MoveTrain : MonoBehaviour {
    public Rigidbody train;
    public float speed;
    // Use this for initialization
	void Start () {
        Rigidbody train = GetComponent<Rigidbody>();
         
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("up")){
            train.AddForce(new Vector3(0, 0, (1 * speed)));
        }
        if (Input.GetKey("down"))
        {
            train.AddForce(new Vector3(0, 0, (-1 * speed)));
        }
    }
}
