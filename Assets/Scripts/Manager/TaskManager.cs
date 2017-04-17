using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour {

    public static TaskManager Instance;

    public List<Agent> AgentList;

    private bool sortTaskList = false;
    public List<Task> TaskList { get; private set; }

    private void Awake() {
        Instance = this;
        this.TaskList = new List<Task>();
        this.AgentList = new List<Agent>();
    }

    private void SortListByPriority() {
        this.TaskList.Sort(delegate (Task c1, Task c2) { return c1.Priority.CompareTo(c2.Priority); });
        this.sortTaskList = false;
    }

    private void Update() {

        //Clear TaskList invalid and finished tasks
        for (int i = 0 ; i < this.TaskList.Count ; i++) {
            ClearListElement(this.TaskList[i]);
        }

        //If the TaskList isn't empty and has more than one task sort the list.
        if (this.sortTaskList && TaskList.Count > 1) {
            SortListByPriority();
        }
        //If the TaskList isn't empty, Process the list.
        foreach (Task t in this.TaskList) {
            AssignListElement(t);
        }
    }

    private void AssignListElement(Task t) {
        if (t.Dooable && t.AssignedAgent == null) {
            foreach (Agent a in this.AgentList) {
                if (a.TaskAssigned == null && (t.AssignementLevel == 0 || a.AssignationLevel == t.AssignementLevel)) {
                    t.AssignedAgent = a;
                    a.AssignTask(t);
                    break;
                }
            }
        }
    }

    private void ClearListElement(Task task) {
        if (!task.Valid) {
            //IF this Task decides it is invalid, then delete it.
            Debug.LogWarning("TaskManager - Invalid Task " + task.TaskID + " detected, removing!");
            this.TaskList.Remove(task);
        } else if (task.Finished()) {
            //If this Task is Finished
            Debug.Log("TaskManager - Task " + task.TaskID + " finished, removing!");
            this.TaskList.Remove(task);
        }
    }

    public void AddTask(Task t) {
        this.TaskList.Add(t);
        this.sortTaskList = true;
    }
}
