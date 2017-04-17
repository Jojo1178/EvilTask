using UnityEngine;

public class Testies : MonoBehaviour {

    TaskManager taskManager;

    WaitTask mt;
    ComplexTask ct;

    // Use this for initialization

    void Awake() {
        taskManager = GetComponent<TaskManager>();

    }
    void Start() {
        Invoke("NewTask", 0.1f);
        Invoke("NewComplexTask", 1f);

    }
    //Just for testing, constructs a new task after half a second and adds it to the list.
    void NewTask() {
        Debug.Log("Add new task");
        mt = new WaitTask() {
            Priority = 1,
            TaskID = 001,
            AssignementLevel = 0,
            WaitTimeSeconds = 2
        };
        taskManager.TaskList.Add(mt);
        mt = new WaitTask() {
            Priority = -10,
            TaskID = 002,
            AssignementLevel = 1,
            WaitTimeSeconds = 5
        };
        taskManager.TaskList.Add(mt);
    }

    void NewComplexTask() {
        Debug.Log("Add new complex task");
        //Create new ComplexTask.
        ct = new ComplexTask() {
            Priority = -10,
            TaskID = 003,
            AssignementLevel = 0

        };
        //Create new MoveTask.
        mt = new WaitTask() {
            Priority = 1,
            TaskID = 004,
            AssignementLevel = 0,
            WaitTimeSeconds = 1

        };
        //Add it to the complex task.
        ct.ComplexTaskList.Add(mt);
        //Make another.
        mt = new WaitTask() {
            Priority = 2,
            TaskID = 005,
            AssignementLevel = 0,
            WaitTimeSeconds = 3
        };
        //Add that one aswell.
        ct.ComplexTaskList.Add(mt);
        //Finally add the complex task we have just built to the tasklist.
        taskManager.TaskList.Add(ct);
    }
}
