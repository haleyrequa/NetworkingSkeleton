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
            Type = JointType(astraJoint.Type);
            Status = astraJoint.Status == Astra.JointStatus.Tracked;
            Position = Scale * new Vector3 (astraJoint.WorldPosition.X, astraJoint.WorldPosition.Y, astraJoint.WorldPosition.Z);
            ScreenPosition = new Vector3(Position.x, Position.y, Position.z);
        }

        public Joint(nuitrack.Joint nuitrackJoint)
        {
            Type = (JointType)nuitrackJoint.Type;
            Status = nuitrackJoint.Confidence >= 0.5f;
            Position = Scale * nuitrackJoint.ToVector3();
            ScreenPosition = new Vector3(Position.x, Position.y, Position.z);
        }

        private JointType JointType(Astra.JointType aType) {

            switch (aType)
            {
                case Astra.JointType.Unknown:
                    return Zone.JointType.None;
                case Astra.JointType.Head:
                    return Zone.JointType.Head;
                case Astra.JointType.ShoulderSpine:
                    return Zone.JointType.Neck;
                case Astra.JointType.LeftShoulder:
                    return Zone.JointType.LeftShoulder;
                case Astra.JointType.LeftElbow:
                    return Zone.JointType.LeftElbow;
                case Astra.JointType.LeftHand:
                    return Zone.JointType.LeftHand;
                case Astra.JointType.RightShoulder:
                    return Zone.JointType.RightShoulder;
                case Astra.JointType.RightElbow:
                    return Zone.JointType.RightElbow;
                case Astra.JointType.RightHand:
                    return Zone.JointType.RightHand;
                case Astra.JointType.MidSpine:
                    return Zone.JointType.Torso;
                case Astra.JointType.BaseSpine:
                    return Zone.JointType.Waist;
                case Astra.JointType.LeftHip:
                    return Zone.JointType.LeftHip;
                case Astra.JointType.LeftKnee:
                    return Zone.JointType.LeftKnee;
                case Astra.JointType.LeftFoot:
                    return Zone.JointType.LeftFoot;
                case Astra.JointType.RightHip:
                    return Zone.JointType.RightHip;
                case Astra.JointType.RightKnee:
                    return Zone.JointType.RightKnee;
                case Astra.JointType.RightFoot:
                    return Zone.JointType.RightFoot;
                case Astra.JointType.LeftWrist:
                    return Zone.JointType.LeftWrist;
                case Astra.JointType.RightWrist:
                    return Zone.JointType.RightWrist;
                case Astra.JointType.Neck:
                    return Zone.JointType.Neck;
                default:
                    return Zone.JointType.None;
            };
        }
    }
}
