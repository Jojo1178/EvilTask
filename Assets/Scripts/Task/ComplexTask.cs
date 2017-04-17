using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexTask : Task {

    //List of Tasks to complete.
    public List<Task> ComplexTaskList;

    //Constructor
    public ComplexTask() {
        this.ComplexTaskList = new List<Task>();
    }

    private bool internalValid = true;

    IEnumerator ProcessComplexTask() {
        //If this task is not initialised, initialise it.
        Task t = this.ComplexTaskList[0];
        if (t.Valid) {
            //If its not initialised, intialise it.
            if (t.Initialised) {
                //If the task isn't finished, execute it.
                if (!t.Finished()) {
                    yield return t.Execute();
                } else {
                    Debug.Log("ComplexTask " + this.TaskID + " - Task " + t.TaskID + " finished, removing!");
                    this.ComplexTaskList.RemoveAt(0);
                }
            } else {
                t.Initialise();
            }
        } else {
            Debug.LogWarning("ComplexTask " + this.TaskID + " - Invalid Task " + t.TaskID + " detected, removing!");
            this.internalValid = false;
        }
    }


    private bool SetupCheck() {
        if (this.Priority == 0 || this.TaskID == 0) {
            Debug.LogWarning("ComplexTask - Task " + this.TaskID + " was not setup correctly!");
            return false;
        } else {
            return true;
        }
    }

    public override bool Valid {
        get {
            if (!SetupCheck() && !internalValid) {
                return false;
            } else {
                return true;
            }
        }
    }

    public override bool Dooable {
        get {
            foreach(Task t in this.ComplexTaskList) {
                if (!t.Dooable)
                    return false;
            }
            return true;
        }
    }

    public override void Initialise() {
        Initialised = true;
    }

    public override bool Finished() {
        if (ComplexTaskList.Count <= 0) {
            return true;
        } else {
            return false;
        }
    }

    public override IEnumerator Execute() {
        yield return ProcessComplexTask();
    }
}