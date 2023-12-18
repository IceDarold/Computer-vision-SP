using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FaceMovement.Data;

[CreateAssetMenu(fileName = "BonesData", menuName = "BonesData")]
public class BonesData : ScriptableObject
{
    [SerializeField] private List<BoneData> _bones;
    public List<BoneData> bones { get { return _bones; } }
}

