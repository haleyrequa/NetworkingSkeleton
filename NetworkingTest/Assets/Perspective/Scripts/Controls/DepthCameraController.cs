using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zone
{
    public class DepthCameraController : MonoBehaviour
    {
        public GameObject BodyPrefab;

        private Dictionary<int, BodyController> _bodies = new Dictionary<int, BodyController>();
        private BodyController _bodyController;
        private AstraController _astraController;
        private Astra.Body[] _astraBodies = new Astra.Body[Astra.BodyFrame.MaxBodies];

        // Start is called before the first frame update
        private void Awake()
        {
            _bodyController = FindObjectOfType<BodyController>();
            _astraController = FindObjectOfType<AstraController>();
        }

        void Start()
        {
            InitializedAstraController();
        }

        private void InitializedAstraController()
        {
            if (_astraController == null) return;

            _astraController.NewBodyFrameEvent.AddListener(OnAstraNewFrame);
        }

        public void OnAstraNewFrame(Astra.BodyStream bodyStream, Astra.BodyFrame frame)
        {
            frame.CopyBodyData(ref _astraBodies);
            foreach (Astra.Body astraBody in _astraBodies)
            {
                Body body = new Body(astraBody);
                if (body.Status && !_bodies.ContainsKey(body.Id))
                    InstantiateBody(body);
                else if (body.Status)
                    _bodies[body.Id].OnNewFrame(body);
                else if (_bodies.ContainsKey(body.Id))
                    _bodies[body.Id].gameObject.SetActive(false);
            }
        }

        private void InstantiateBody(Body body)
        {
            _bodies[body.Id] = Instantiate(BodyPrefab, transform).GetComponent<BodyController>();
            _bodies[body.Id].Initialize(body);
        }
    }
}
