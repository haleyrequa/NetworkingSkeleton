using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zone
{
    public class DepthCameraController : MonoBehaviour
    {
        public GameObject BodyPrefab;

        private Dictionary<int, BodyController> _bodies = new Dictionary<int, BodyController>();
        private AstraController _astraController;
        private NuitrackManager _nuitrackManager;
        private Astra.Body[] _astraBodies = new Astra.Body[Astra.BodyFrame.MaxBodies];

        // Start is called before the first frame update
        private void Awake()
        {
            _astraController = FindObjectOfType<AstraController>();
            _nuitrackManager = FindObjectOfType<NuitrackManager>();

            if (_astraController != null)
                _astraController.NewBodyFrameEvent.AddListener(OnAstraNewFrame);

        }

        private void Update()
        {
            OnNuitrackUpdate();
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

        public void OnNuitrackUpdate()
        {
            if (_nuitrackManager == null || CurrentUserTracker.CurrentUser == 0) return;

            Body body = new Body(CurrentUserTracker.CurrentSkeleton);

            if (body.Status && !_bodies.ContainsKey(body.Id))
                InstantiateBody(body);
            else if (body.Status)
                _bodies[body.Id].OnNewFrame(body);
            else if (_bodies.ContainsKey(body.Id))
                _bodies[body.Id].gameObject.SetActive(false);
        }

        private void InstantiateBody(Body body)
        {
            _bodies[body.Id] = Instantiate(BodyPrefab, transform).GetComponent<BodyController>();
            _bodies[body.Id].Initialize(body);
        }
    }
}
