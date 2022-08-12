using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public float acceleration;
    public float turnSpeed;

    public Transform carModel;
    private Vector3 startModelOffset;

    public float groundCheckRate;
    private float lastGroundCheckTime;

    private float curYRot;

    private bool accelarateInput;
    private float turnInput;

    public TrackZone curTrackZone;
    public int zonePassed;
    public int racePosition;
    public int curLap;

    public Rigidbody rig;

    private void Start()
    {
        startModelOffset = carModel.transform.localPosition;
        GameManager.instance.cars.Add(this);
    }

    private void Update()
    {
        float turnRate = Vector3.Dot(rig.velocity.normalized, carModel.forward);
        turnRate = Mathf.Abs(turnRate);

        curYRot += turnInput * turnSpeed * turnRate * Time.deltaTime;
        
        carModel.position = transform.position + startModelOffset;
        //carModel.eulerAngles = new Vector3(0, curYRot, 0);

        CheckGround();
    }

    private void FixedUpdate()
    {
        if (accelarateInput == true)
        {
            rig.AddForce(carModel.forward * acceleration, ForceMode.Acceleration);
        }
    }

    void CheckGround()
    {
        Ray ray = new Ray(transform.position + new Vector3(0f, -0.75f, 0f), Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1f))
        {
            carModel.up = hit.normal;
        }
        else
        {
            carModel.up = Vector3.up;
        }

        carModel.Rotate(new Vector3(0f, curYRot, 0f), Space.Self);
    }

    public void OnAcclerateInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
            accelarateInput = true;
        else
            accelarateInput = false;
    }

    public void OnTurnInput(InputAction.CallbackContext context)
    {
        turnInput = context.ReadValue<float>();
    }
}
