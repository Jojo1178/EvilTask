using System.Collections;
using UnityEngine;

public class Agent : MonoBehaviour {

    public int ID;
    public string Name;

    public int AssignationLevel;
    public Task TaskAssigned;

    private bool IsWorking = false;

    private void Start() {
        TaskManager.Instance.AgentList.Add(this);
    }

    private void Update() {
        if (!this.IsWorking && this.TaskAssigned != null) {
            this.IsWorking = true;
            StartCoroutine(DoTask());
        }
    }

    private IEnumerator DoTask() {
        while (!this.TaskAssigned.Finished()) {
            yield return this.TaskAssigned.Execute();
            yield return 0;
        }
        this.IsWorking = false;
        this.TaskAssigned = null;
    }

    public void AssignTask(Task t) {
        Debug.Log("Task " + t.TaskID + " has been assigned to " + this.Name);
        this.TaskAssigned = t;
        t.Initialise();
    }
}
