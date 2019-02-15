using System;
using UnityEngine;
using UnityEngine.XR.iOS;

namespace XLand.Nodes.ARKit
{
    public enum BlendShapeLocation : int
    {
        BrowDownLeft,
        BrowDownRight,
        BrowInnerUp,
        BrowOuterUpLeft,
        BrowOuterUpRight,
        CheekPuff,
        CheekSquintLeft,
        CheekSquintRight,
        EyeBlinkLeft,
        EyeBlinkRight,
        EyeLookDownLeft,
        EyeLookDownRight,
        EyeLookInLeft,
        EyeLookInRight,
        EyeLookOutLeft,
        EyeLookOutRight,
        EyeLookUpLeft,
        EyeLookUpRight,
        EyeSquintLeft,
        EyeSquintRight,
        EyeWideLeft,
        EyeWideRight,
        JawForward,
        JawLeft,
        JawOpen,
        JawRight,
        MouthClose,
        MouthDimpleLeft,
        MouthDimpleRight,
        MouthFrownLeft,
        MouthFrownRight,
        MouthFunnel,
        MouthLeft,
        MouthLowerDownLeft,
        MouthLowerDownRight,
        MouthPressLeft,
        MouthPressRight,
        MouthPucker,
        MouthRight,
        MouthRollLower,
        MouthRollUpper,
        MouthShrugLower,
        MouthShrugUpper,
        MouthSmileLeft,
        MouthSmileRight,
        MouthStretchLeft,
        MouthStretchRight,
        MouthUpperUpLeft,
        MouthUpperUpRight,
        NoseSneerLeft,
        NoseSneerRight,
        TongueOut,
    }

    internal static class BlendShapeEnumStringMapping
    {
        private static readonly string[] m_LocationStrings =
        {
            ARBlendShapeLocation.BrowDownLeft,
            ARBlendShapeLocation.BrowDownRight,
            ARBlendShapeLocation.BrowInnerUp,
            ARBlendShapeLocation.BrowOuterUpLeft,
            ARBlendShapeLocation.BrowOuterUpRight,
            ARBlendShapeLocation.CheekPuff,
            ARBlendShapeLocation.CheekSquintLeft,
            ARBlendShapeLocation.CheekSquintRight,
            ARBlendShapeLocation.EyeBlinkLeft,
            ARBlendShapeLocation.EyeBlinkRight,
            ARBlendShapeLocation.EyeLookDownLeft,
            ARBlendShapeLocation.EyeLookDownRight,
            ARBlendShapeLocation.EyeLookInLeft,
            ARBlendShapeLocation.EyeLookInRight,
            ARBlendShapeLocation.EyeLookOutLeft,
            ARBlendShapeLocation.EyeLookOutRight,
            ARBlendShapeLocation.EyeLookUpLeft,
            ARBlendShapeLocation.EyeLookUpRight,
            ARBlendShapeLocation.EyeSquintLeft,
            ARBlendShapeLocation.EyeSquintRight,
            ARBlendShapeLocation.EyeWideLeft,
            ARBlendShapeLocation.EyeWideRight,
            ARBlendShapeLocation.JawForward,
            ARBlendShapeLocation.JawLeft,
            ARBlendShapeLocation.JawOpen,
            ARBlendShapeLocation.JawRight,
            ARBlendShapeLocation.MouthClose,
            ARBlendShapeLocation.MouthDimpleLeft,
            ARBlendShapeLocation.MouthDimpleRight,
            ARBlendShapeLocation.MouthFrownLeft,
            ARBlendShapeLocation.MouthFrownRight,
            ARBlendShapeLocation.MouthFunnel,
            ARBlendShapeLocation.MouthLeft,
            ARBlendShapeLocation.MouthLowerDownLeft,
            ARBlendShapeLocation.MouthLowerDownRight,
            ARBlendShapeLocation.MouthPressLeft,
            ARBlendShapeLocation.MouthPressRight,
            ARBlendShapeLocation.MouthPucker,
            ARBlendShapeLocation.MouthRight,
            ARBlendShapeLocation.MouthRollLower,
            ARBlendShapeLocation.MouthRollUpper,
            ARBlendShapeLocation.MouthShrugLower,
            ARBlendShapeLocation.MouthShrugUpper,
            ARBlendShapeLocation.MouthSmileLeft,
            ARBlendShapeLocation.MouthSmileRight,
            ARBlendShapeLocation.MouthStretchLeft,
            ARBlendShapeLocation.MouthStretchRight,
            ARBlendShapeLocation.MouthUpperUpLeft,
            ARBlendShapeLocation.MouthUpperUpRight,
            ARBlendShapeLocation.NoseSneerLeft,
            ARBlendShapeLocation.NoseSneerRight,
            ARBlendShapeLocation.TongueOut,
        };

        public static string GetLocationString(this BlendShapeLocation location)
        {
            var loc = (int) location;
            if (loc < 0 || loc >= m_LocationStrings.Length)
            {
                Debug.LogFormat("Invalid Position {0}, value {1}", location, loc);
                return string.Empty;
            }

            return m_LocationStrings[loc];
        }
    }

    [MarkAsNode(DisplayName = "Input/ARKit/Blend Shapes")]
    public class BlendShapes : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Attribute)]
        public BlendShapeLocation location = BlendShapeLocation.BrowDownLeft;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float value;

        public override void Evaluate()
        {
            var blendShapes = ARKitFaceAnchor.BlendShapes;
            if (blendShapes == null) return;
            float v;
            if (blendShapes.TryGetValue(location.GetLocationString(), out v))
            {
                value = v;
            }
        }
    }
}