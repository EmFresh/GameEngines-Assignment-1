using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using static MainControls;
using static Unity.Mathematics.math;

public class EditMovement : MonoBehaviour, IEditModeActions
{
    public static MainControls controls;
    bool move, upnDown, rotate;
    float moveSpd = 15, rotSpd = 25;
    MainControls.EditModeActions edit;

    private void Update()
    {
        if (move)
        {
            transform.position += (Vector3)(normalize(transform.forward * pos.z + transform.right * pos.x) * Time.deltaTime * moveSpd);
        }
        if (upnDown)
            transform.position += (Vector3)(normalize(transform.up * pos.y) * Time.deltaTime * moveSpd);
        if (rotate)
        {
            rot += rotVec * Time.deltaTime * rotSpd;
            transform.rotation = Quaternion.Euler(rot);
        }
    }

    Vector3 pos;
    public void OnMovement(InputAction.CallbackContext ctx)
    {
        move = !ctx.canceled;

        print(ctx.control.name);

        if (ctx.control.name == "q" || ctx.control.name == "e")
        {
            print(ctx.ReadValue<float>());
            pos = new Vector3(0, ctx.ReadValue<float>(), 0);
        }
        else
        {
            print(ctx.ReadValue<Vector2>());
            pos = new Vector3(ctx.ReadValue<Vector2>().x, 0, ctx.ReadValue<Vector2>().y);
        }

    }
    public void OnUpnDown(InputAction.CallbackContext ctx)
    {
        upnDown = !ctx.canceled;
        var tmp = !!move;
        // if (upnDown)
        OnMovement(ctx);
        move = tmp;
    }

    Vector3 rot, rotVec;
    public void OnRotation(InputAction.CallbackContext ctx)
    {
        rotate = !ctx.canceled;

        print(ctx.ReadValue<Vector2>());
        rotVec = new Vector3(-ctx.ReadValue<Vector2>().y, ctx.ReadValue<Vector2>().x, 0);

    }

    private void OnEnable()
    {
        if (controls == null)
        {
            pos = transform.position;
            rot = transform.rotation.eulerAngles;

            controls = new MainControls();
            edit = controls.EditMode;
            edit.SetCallbacks(this);
        }
        edit.Enable();
    }
    private void OnDisable() =>
        edit.Disable();

}
