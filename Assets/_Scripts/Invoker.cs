using System.Threading.Tasks;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Invoker : MonoBehaviour
{
    [SerializeField] InputAction undo, redo;

    static int counter = 0;
    public static List<IMyAction>
    master = new List<IMyAction>(),
    que = new List<IMyAction>();
    static bool _dirty = false;

    void Awake()
    {
        undo.started += ctx =>
        {
            if (counter <= 0) return;
            IMyAction act;
            (act = master[--counter]).Undo();
            CreatePopups.SendPopup("Undo: " + act.ToString());
        };
        redo.started += ctx =>
        {
            if (counter >= master.Count) return;
            IMyAction act;
            (act = master[counter++]).Invoke();
            CreatePopups.SendPopup("Redo: " + act.ToString());
        };
    }

    private void OnEnable()
    {
        undo.Enable();
        redo.Enable();
    }

    private void OnDisable()
    {
        undo.Disable();
        redo.Disable();
    }

    public static void addAction(IMyAction act)
    {
        que.Add(act);

        master.RemoveRange(counter, master.Count - counter);
        counter -= master.Count - counter;

        CreatePopups.SendPopup("Action: " + act.ToString());
        _dirty = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_dirty) return;
        _dirty = false;

        foreach (var act in que)
        {
            act.Invoke();
            master.Add(act);
            ++counter;
        }
        que.Clear();
    }
}
