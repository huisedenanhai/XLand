using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

namespace XLand
{
    public class ARKitFaceAnchor : MonoBehaviour
    {
        private UnityARSessionNativeInterface m_Session;

        // Use this for initialization
        private void Start()
        {
            m_Session = UnityARSessionNativeInterface.GetARSessionNativeInterface();

            Application.targetFrameRate = 60;
            var config = new ARKitFaceTrackingConfiguration
            {
                alignment = UnityARAlignment.UnityARAlignmentGravity, enableLightEstimation = false
            };

            if (config.IsSupported != null)
            {
                m_Session.RunWithConfig(config);

                UnityARSessionNativeInterface.ARFaceAnchorAddedEvent += FaceAdded;
                UnityARSessionNativeInterface.ARFaceAnchorUpdatedEvent += FaceUpdated;
                UnityARSessionNativeInterface.ARFaceAnchorRemovedEvent += FaceRemoved;
            }

            ForceUpdateValue();
        }


        private static void FaceAdded(ARFaceAnchor anchorData)
        {
            FaceAnchor = anchorData;
            ForceUpdateValue();
        }

        private static void FaceUpdated(ARFaceAnchor anchorData)
        {
            FaceAnchor = anchorData;
            ForceUpdateValue();
        }

        private static void FaceRemoved(ARFaceAnchor anchorData)
        {
            FaceAnchor = null;
            ForceUpdateValue();
        }

        private static void ForceUpdateValue()
        {
            CalculateFacePosition();
            CalculateFaceEuler();
            CalculateBlendShapes();
            CalculateLeftEyePosition();
            CalculateLeftEyeEuler();
            CalculateRightEyePosition();
            CalculateRightEyeEuler();
            CalculateLookAtPoint();
            CalculateFaceTransform();
        }

        private static ARFaceAnchor FaceAnchor { set; get; }

        public static bool IsTracked
        {
            get { return FaceAnchor != null && FaceAnchor.isTracked; }
        }

        private static Vector3 m_LookAtPoint = Vector3.zero;

        public static Vector3 LookAtPoint
        {
            get { return m_LookAtPoint; }
        }

        private static void CalculateLookAtPoint()
        {
            var anchor = FaceAnchor;
            if (anchor == null || !anchor.isTracked) return;
            m_LookAtPoint = anchor.lookAtPoint;
        }

        private static Vector3 m_RightEyeEuler = Vector3.zero;

        public static Vector3 RightEyeEuler
        {
            get { return m_RightEyeEuler; }
        }

        private static void CalculateRightEyeEuler()
        {
            var anchor = FaceAnchor;
            if (anchor == null || !anchor.isTracked) return;
            m_RightEyeEuler = anchor.rightEyePose.rotation.eulerAngles;
        }

        private static Vector3 m_RightEyePosition = Vector3.zero;

        public static Vector3 RightEyePosition
        {
            get { return m_RightEyePosition; }
        }

        private static void CalculateRightEyePosition()
        {
            var anchor = FaceAnchor;
            if (anchor == null || !anchor.isTracked) return;
            m_RightEyePosition = anchor.rightEyePose.position;
        }

        private static Vector3 m_LeftEyeEuler = Vector3.zero;

        public static Vector3 LeftEyeEuler
        {
            get { return m_LeftEyeEuler; }
        }

        private static void CalculateLeftEyeEuler()
        {
            var anchor = FaceAnchor;
            if (anchor == null || !anchor.isTracked) return;
            m_LeftEyeEuler = anchor.leftEyePose.rotation.eulerAngles;
        }

        private static Vector3 m_LeftEyePosition = Vector3.zero;

        public static Vector3 LeftEyePosition
        {
            get { return m_LeftEyePosition; }
        }

        private static void CalculateLeftEyePosition()
        {
            var anchor = FaceAnchor;
            if (anchor == null || !anchor.isTracked) return;
            m_LeftEyePosition = anchor.leftEyePose.position;
        }

        private static Dictionary<string, float> m_BlendShapes = new Dictionary<string, float>();

        public static Dictionary<string, float> BlendShapes
        {
            get { return m_BlendShapes; }
        }

        private static void CalculateBlendShapes()
        {
            var anchor = FaceAnchor;
            if (anchor == null || !anchor.isTracked) return;
            m_BlendShapes = anchor.blendShapes;
        }

        private static Vector3 m_FacePosition = Vector3.zero;

        public static Vector3 FacePosition
        {
            get { return m_FacePosition; }
        }

        private static void CalculateFacePosition()
        {
            var anchor = FaceAnchor;
            if (anchor == null || !anchor.isTracked) return;
            m_FacePosition = UnityARMatrixOps.GetPosition(anchor.transform);
        }

        private static Vector3 m_FaceEuler = Vector3.zero;

        public static Vector3 FaceEuler
        {
            get { return m_FaceEuler; }
        }

        private static void CalculateFaceEuler()
        {
            var anchor = FaceAnchor;
            if (anchor == null || !anchor.isTracked) return;
            var rotation = UnityARMatrixOps.GetRotation(anchor.transform);
            m_FaceEuler = rotation.eulerAngles;
        }

        public static Matrix4x4 FaceTransform = Matrix4x4.identity;
        public static Matrix4x4 FaceInverseTransform = Matrix4x4.identity;

        private static void CalculateFaceTransform()
        {
            var anchor = FaceAnchor;
            if (anchor == null || !anchor.isTracked) return;
            FaceTransform = anchor.transform;
            FaceInverseTransform = Matrix4x4.Inverse(FaceTransform);
        }
    }
}