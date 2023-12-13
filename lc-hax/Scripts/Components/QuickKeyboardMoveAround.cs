using UnityEngine;
using UnityEngine.InputSystem;

namespace Hax;

public class QuickKeyboardMoveAround : MonoBehaviour {
    public float SpeedMultiplier { get; private set; } = 20;
    public bool OnlyXZPlaneMovement { get; private set; } = false;
    float SprintMultiplier { get; set; } = 1;

    void Update() {
        if (!Keyboard.current.IsNotNull(out Keyboard keyboard)) return;

        Vector3 direction = Vector3.zero;
        direction.z += keyboard.wKey.ReadValue();
        direction.z -= keyboard.sKey.ReadValue();
        direction.x += keyboard.dKey.ReadValue();
        direction.x -= keyboard.aKey.ReadValue();
        direction.y += keyboard.spaceKey.ReadValue();
        direction.y -= keyboard.ctrlKey.ReadValue();

        this.SprintMultiplier = keyboard.shiftKey.IsPressed() ? this.SprintMultiplier + (5 * Time.deltaTime) : 1;

        if (this.OnlyXZPlaneMovement) {
            Vector3 forward = this.transform.forward;
            Vector3 right = this.transform.right;
            Vector3 up = this.transform.up;

            forward.y = 0;
            right.y = 0;

            forward = forward.normalized;
            right = right.normalized;
            up = up.normalized;

            this.transform.position +=
                (forward * direction.z * Time.deltaTime * this.SpeedMultiplier * this.SprintMultiplier) +
                (right * direction.x * Time.deltaTime * this.SpeedMultiplier * this.SprintMultiplier) +
                (up * direction.y * Time.deltaTime * this.SpeedMultiplier * this.SprintMultiplier);
        }

        else {
            this.transform.position +=
                (this.transform.forward * direction.z * Time.deltaTime * this.SpeedMultiplier * this.SprintMultiplier) +
                (this.transform.right * direction.x * Time.deltaTime * this.SpeedMultiplier * this.SprintMultiplier) +
                (this.transform.up * direction.y * Time.deltaTime * this.SpeedMultiplier * this.SprintMultiplier);
        }
    }
}
