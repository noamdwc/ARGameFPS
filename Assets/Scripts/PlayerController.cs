using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float lookSensitivity = 3f;

    private PlayerMotor motor;
    // Start is called before the first frame update


    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    //Debug: show/hide mouse cursor using 'i'
    private bool ShowMouse = false;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("i"))
        {
            ShowMouse = !ShowMouse;
        }
        if (ShowMouse == true)
        {
            Cursor.visible = true;
        }
        if (ShowMouse == false)
        {
            Cursor.visible = false;
        }
#if UNITY_EDITOR
    //Run this code only when using the editor.
        //Calculate rotation as a 3D vector (turning around)
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        //Apply rotation
        // motor.Rotate(_rotation);

        //Calculate camera rotation as a 3D vector (turning around)
        float _xRot = Input.GetAxisRaw("Mouse Y");

        float _cameraRotationX = _xRot * lookSensitivity;

        //Apply camera rotation
        motor.RotateCamera(_cameraRotationX);
#else
#endif
    }
}
