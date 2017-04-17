using System.Collections;
using UnityEngine;

public class WaitTask : Task {

    public float WaitTimeSeconds;
    private bool hasWaited = false;
    private bool processing = false;

    public override bool Valid {
        get {
            return true;
        }
    }

    public override bool Dooable {
        get {
            return true;
        }
    }

    public override IEnumerator Execute() {
        if (!this.processing) {
            this.processing = true;
            Debug.Log("Task " + TaskID + " is waiting");
            yield return new WaitForSeconds(WaitTimeSeconds);
            Debug.Log("Task " + TaskID + " has waited");
            this.hasWaited = true;
        }
    }

    public override bool Finished() {
        return this.hasWaited;
    }

    public override void Initialise() {
        this.Initialised = true;
    }
}
