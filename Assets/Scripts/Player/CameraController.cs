using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * The CameraController class handles all input from the 
 * player's mouse and aligns the main camera accordingly 
 * with the movement.
 **/
public class CameraController : MonoBehaviour {

    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes axes = RotationAxes.MouseXAndY;

    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float _rotationX = 0f;

	/**
     * Locks the cursor to the game upon initialization, makes the camera movement interactable,
     * and lets the user unlock the cursor using ESC key. Ob
     **/
	void Start () {

        Cursor.lockState = CursorLockMode.Locked;

        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    /**
     * Updates the rotation of the camera to correspond with the mouse movement and 
     * makes sure that the position of the camera is aligned directly between the player's
     * eyes.
     **/
    void Update()
    {
        switch (axes)
        {
            case RotationAxes.MouseX:
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
                break;
            case RotationAxes.MouseY:
                _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
                _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

                transform.localEulerAngles = new Vector3(_rotationX, transform.localEulerAngles.y, 0);
                break;
            default:
                float rotationY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityHor;

                _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
                _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

                transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
                break;
        }
    }
}

