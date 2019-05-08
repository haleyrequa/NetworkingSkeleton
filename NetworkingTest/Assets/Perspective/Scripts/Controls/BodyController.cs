using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Zone
{
    public class BodyController : MonoBehaviour
    {
        public GameObject JointPrefab;

        private Body _body;
        private Dictionary<JointType, GameObject> _joints = new Dictionary<JointType, GameObject>();

        public void OnNewFrame(Body body) {
            gameObject.SetActive(true);
            _body = body;
            UpdatePosition();
        }

        public void Initialize(Body body)
        {
            _body = body;
            gameObject.name = "Body_" + _body.Id.ToString();
            InstatiateJoints();
            UpdatePosition();
        }

        private void InstatiateJoints()
        {
            foreach (Joint joint in _body.Joints)
            {
                if (joint.Type == JointType.Unknown) continue;

                _joints[joint.Type] =
                  Instantiate(
                  JointPrefab,
                  joint.Position,
                  new Quaternion(),
                  transform);

                if(NetworkServer.active)
                    NetworkServer.Spawn(_joints[joint.Type]);

                if (joint.Type == JointType.LeftHand)
                {
                    _joints[joint.Type].GetComponentInChildren<MeshRenderer>().material.color = Color.yellow;
                }
                if (joint.Type == JointType.RightHand)
                {
                    _joints[joint.Type].GetComponentInChildren<MeshRenderer>().material.color = Color.red;
                }
            }
            _joints[JointType.Unknown] =
              Instantiate(
              JointPrefab,
              _body.CenterOfMass,
              new Quaternion(),
              transform);
            _joints[JointType.Unknown].GetComponentInChildren<MeshRenderer>().material.color = Color.green;
        }

        private void UpdatePosition() {
            foreach (Joint joint in _body.Joints)
            {
                if (joint.Type == JointType.Unknown) continue;
                _joints[joint.Type].transform.localPosition = joint.Position;
                Debug.DrawLine(_joints[joint.Type].transform.position, new Vector3(_joints[joint.Type].transform.position.x, _joints[joint.Type].transform.position.y));
            }
            _joints[JointType.Unknown].transform.localPosition = _body.CenterOfMass;
        }
        
    }
}
