﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Zone
{
    public class Body
    {
        public int Id;
        public List<Joint> Joints;
        public Vector2 ScreenPosition;
        public Vector3 CenterOfMass;
        public bool Status;

        private float Scale = 0.001f;

        public Body(Astra.Body astraBody) {
            Id = astraBody.Id;
            Status = astraBody.Status == Astra.BodyStatus.Tracking;
            CenterOfMass = Scale * new Vector3(astraBody.CenterOfMass.X, astraBody.CenterOfMass.Y, astraBody.CenterOfMass.Z);
            if (astraBody.Joints != null)
                Joints = astraBody.Joints.Select(j => new Joint(j)).ToList();
        }
    }
}
