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
    static List<IMyAction>
    master = new List<IMyAction>(),
    que = new List<IMyAction>();

    void Awake()
    {
        undo.started += ctx =>
           master[--counter].Undo();

        redo.started += ctx =>
           master[counter++].Invoke();
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
    }


    // Update is called once per frame
    void Update()
    {
        foreach (var act in que)
        {
            act.Invoke();
            master.Add(act);
            ++counter;
        }
        que.Clear();
    }
}
