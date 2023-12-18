using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FaceMovement.Data;
using System;
namespace FaceMovement
{
    public class FaceMovements : MonoBehaviour
    {
        [SerializeField] private BonesData bonesData;
        private Dictionary<Bone, GameObject> bones;
        private 
        void Start ()
        {
            bones = new Dictionary<Bone, GameObject>();
            foreach (BoneData bone in bonesData.bones)
            {
                bone.bone_object = GameObject.Find(bone.name);
                if (bone.bone_object != null) 
                    Debug.LogAssertion($"There is no object with name \"{bone.name}\" on the scene");
            }
        }
        void Update ()
        {
            var data = FaceDataReceiver.GetData();
            if (data != null)
            {
                Debug.Log("Succes");
                var bonsesOffset = FaceDataReceiver.GetBonesOffset(FaceForms.FACEMESH_RIGHT_EYEBROW, data);
                foreach (BoneData bone in bonesData.bones)
                {
                    (double, double, double) offset = (0, 0, 0);
                    try
                    {
                        offset = bonsesOffset[bone.boneType];
                    }
                    catch 
                    {
                        Debug.LogAssertion($"There is no {bone.boneType}");
                    }
                    bone.bone_object.transform.Translate(new Vector3(Convert.ToSingle(offset.Item1), Convert.ToSingle(offset.Item2), Convert.ToSingle(offset.Item3)));

                }
            }
        }
     }
}
