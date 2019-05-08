using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Zone
{
    [System.Serializable]
    public class BodyFrameEvent : UnityEvent<List<Body>> { }

    public class ZoneController : MonoBehaviour
    {
        public BodyFrameEvent BodyFrameEvent = new BodyFrameEvent();
        public DepthCameraController DepthCamera;
        public GameObject DirectionalLight;
        public GameObject Floor;
        public GameObject OrbbecMesh;
        public GameObject Screen;

        public void Toggle(bool on)
        {
            //DepthCamera.gameObject.SetActive(on);
            DirectionalLight.SetActive(on);
            Floor.SetActive(on);
            OrbbecMesh.SetActive(on);
            Screen.SetActive(on);
        }
        

    }
}
