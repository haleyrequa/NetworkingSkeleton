using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zone
{
    public class InputController : MonoBehaviour
    {
        public Transform OrbbecCamera;
        public Transform Zone;
        public Text OrbbecText;
        public Text ZoneText;
        public GameObject Legend;
        public float UIDuration = 3f;

        private float delta = 0.1f;
        private Vector3 angle;

        // Start is called before the first frame update
        void Start()
        {
            angle = OrbbecCamera.eulerAngles;
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKey(KeyCode.Plus))
            {
                delta = 1f;
            }

            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    if (Input.GetKey(KeyCode.Space))
                        TransformZoneB(Vector3.down);
                    else TransformCamera(Vector3.up);
                }
                else if (Input.GetKey(KeyCode.Space))
                {
                    TransformZoneB(Vector3.back);
                }
                else TransformCamera(Vector3.forward);
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    if (Input.GetKey(KeyCode.Space))
                        TransformZoneB(Vector3.up);
                    else TransformCamera(Vector3.down);
                }
                else if (Input.GetKey(KeyCode.Space))
                {
                    TransformZoneB(Vector3.forward);
                }
                else TransformCamera(Vector3.back);
            }

            if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.Space))
                    TransformZoneB(Vector3.right);
                else TransformCamera(Vector3.left);
            }

            if (Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.Space))
                    TransformZoneB(Vector3.left);
                else TransformCamera(Vector3.right);
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                TransformCamera(Vector3.zero, Vector3.left);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                TransformCamera(Vector3.zero, Vector3.right);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                TransformCamera(Vector3.zero, Vector3.up);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                TransformCamera(Vector3.zero, Vector3.down);
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                OrbbecText.gameObject.SetActive(!OrbbecText.gameObject.activeSelf);
                ZoneText.gameObject.SetActive(!ZoneText.gameObject.activeSelf);
            }

            if (Input.GetKeyDown(KeyCode.F1))
            {
                Zone.GetComponent<ZoneController>().Toggle(true);
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                Zone.GetComponent<ZoneController>().Toggle(false);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                Legend.SetActive(!Legend.activeSelf);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                //Zone.
            }
            delta = 0.1f;
        }

        private void TransformCamera(Vector3 position = new Vector3(), Vector3 rotation = new Vector3())
        {

            angle += rotation;

            OrbbecCamera.transform.localPosition =
                new Vector3(
                    OrbbecCamera.transform.localPosition.x + (delta * position.x),
                    OrbbecCamera.transform.localPosition.y + (delta * position.y),
                    OrbbecCamera.transform.localPosition.z + (delta * position.z));

            OrbbecCamera.transform.eulerAngles = angle;
            OrbbecText.text =
                "Orbbec Camera" + '\n' +
                "Position : X " +
                OrbbecCamera.transform.localPosition.x.ToString("F2") + "m, Y " +
                OrbbecCamera.transform.localPosition.y.ToString("F2") + "m, Z " +
                OrbbecCamera.transform.localPosition.z.ToString("F2") + "m" +
                 '\n' +
                "Rotation : X " +
                Mathf.Round(OrbbecCamera.transform.localEulerAngles.x) + "°, Y " +
                Mathf.Round(OrbbecCamera.transform.localEulerAngles.y) + "°";
        }

        private void TransformZoneB(Vector3 position = new Vector3())
        {
            Zone.transform.position =
                new Vector3(
                    Zone.transform.position.x + (delta * position.x),
                    Zone.transform.position.y + (delta * position.y),
                    Zone.transform.position.z + (delta * position.z));

            ZoneText.text =
                "Zone" + '\n' +
                "World Space Position : " + '\n' + "X " +
                Zone.transform.position.x.ToString("F2") + "m, Y " +
                Zone.transform.position.y.ToString("F2") + "m, Z " +
                Zone.transform.position.z.ToString("F2") + "m";
        }

        public Vector3 CameraEularAngle()
        {
            return angle;
        }
    }
}