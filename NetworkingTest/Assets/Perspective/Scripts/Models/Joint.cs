using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zone
{
    public class Joint
    {
        public JointType Type;
        public bool Status;
        public Vector3 Position;
        public Vector3 ScreenPosition;

        private float Scale = 0.001f;

        public Joint(Astra.Joint astraJoint)
        {
            Type = (JointType)astraJoint.Type;
            Status = astraJoint.Status == Astra.JointStatus.Tracked;
            Position = Scale * new Vector3 (astraJoint.WorldPosition.X, astraJoint.WorldPosition.Y, astraJoint.WorldPosition.Z);
            ScreenPosition = new Vector3(Position.x, Position.y, Position.z);
        }
    }
}
