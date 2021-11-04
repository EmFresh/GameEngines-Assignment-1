using UnityEngine;

public interface IMyAction
{
    /// <summary>
    /// the object the action is associated with
    /// </summary>
    public GameObject lnk { get; set; }
    public string name { get; set; }
    public void Invoke();
    public void Undo();
}
