using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour {

    // speed of movement
    public float speed = 6.0f;

    //gravity
    public float gravity = -9.8f;

    // initialize character controller
    private CharacterController _charController;

	// Use this for initialization
	void Start () {

        _charController = GetComponent<CharacterController>();

	}
	
	// Update is called once per frame
	void Update () {

        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;
        movement *= Time.deltaTime;

        // convert the move from local to global space
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);

	}
}
