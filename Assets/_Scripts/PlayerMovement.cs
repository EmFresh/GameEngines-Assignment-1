using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using static Unity.Mathematics.math;
using static MainControls;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour, IPlayModeActions
{
    MainControls controls;
    MainControls.PlayModeActions play;

    bool move, rotate;
    float moveSpd = 150, moveMax = 15, rotSpd = 50, jumpForce = 25;

    private void FixedUpdate()
    {
        if (move)
        {
            Vector3 forward = transform.GetChild(0).forward * new float3(1, 0, 1);
            Vector3 right = transform.GetChild(0).right * new float3(1, 0, 1);
            forward.Normalize();
            right.Normalize();

            //Force Movement
            GetComponent<Rigidbody>().
            AddForce(Vector3.Normalize(forward * pos.z + right * pos.x) * moveSpd, ForceMode.Force);
            //   GetComponent<Rigidbody>().;

            //velocity cap
            GetComponent<Rigidbody>().velocity =
            math.length(GetComponent<Rigidbody>().velocity * new float3(1, 0, 1)) > moveMax ?
            (Vector3)(GetComponent<Rigidbody>().velocity.normalized * new float3(moveMax, 0, moveMax) + new float3(0, GetComponent<Rigidbody>().velocity.y, 0)) :
            GetComponent<Rigidbody>().velocity;

            ////Positional movement
            //      transform.position += Vector3.Normalize(forward * pos.z + right * pos.x) * Time.deltaTime * moveSpd;

        }
        else
        {
            GetComponent<Rigidbody>().velocity -= (Vector3)(GetComponent<Rigidbody>().velocity * new float3(1, 0, 1)) * 0.1f;

            if (math.length(GetComponent<Rigidbody>().velocity * new float3(1, 0, 1)) < .3f)
                GetComponent<Rigidbody>().velocity =
                new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);

        }
    }

    // Update is called once per frame
    void Update()
    {

        if (rotate)
        {
            rot += rotVec * Time.deltaTime * rotSpd;
            transform.GetChild(0).rotation = Quaternion.Euler(rot);
            transform.rotation = Quaternion.Euler(rot * new float3(0, 1, 0));
        }
    }

    Vector3 pos;
    public void OnMovement(InputAction.CallbackContext ctx)
    {
        move = !ctx.canceled;

        if (!move) return;
        pos = new Vector3(ctx.ReadValue<Vector2>().x, 0, ctx.ReadValue<Vector2>().y);
    }

    Vector3 rot, rotVec;
    public void OnRotation(InputAction.CallbackContext ctx)
    {
        rotate = !ctx.canceled;
        if (!rotate) return;

        rotVec = new Vector3(-ctx.ReadValue<Vector2>().y, ctx.ReadValue<Vector2>().x, 0);
        // print(ctx.ReadValue<Vector2>());

    }
    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (!ctx.started) return;
        var body = GetComponent<Rigidbody>();
        var onfloor = Physics.Raycast(transform.position, Vector3.down, 1.01f);

        if (onfloor)
            body.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }

    private void OnEnable()
    {
        if (controls == null)
        {
            Physics.gravity = new Vector3(0, -33.141596f, 0);


            rot = transform.GetChild(0).rotation.eulerAngles;
            controls = new MainControls();
            play = controls.PlayMode;
            play.SetCallbacks(this);
        }
        play.Enable();
    }
    void OnDisable() =>
        play.Disable();
}
