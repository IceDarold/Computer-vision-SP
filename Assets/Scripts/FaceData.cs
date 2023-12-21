using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FaceMovement.Data
{
    public enum Bone
    {
        Eyebrow_4_R,
        Eyebrow_3_R,
        Eyebrow_2_R,
        Eyebrow_1_R,
        Eyebrow_0_R,
        Eyebrow_4_L,
        Eyebrow_3_L,
        Eyebrow_2_L,
        Eyebrow_1_L,
        Eyebrow_0_L
    }
    [System.Serializable]
    public class BoneData
    {
        /// <summary>
        /// The bone name of this model
        /// </summary>
        [SerializeField] private string _name;
        public string name { get { return _name; } }
        /// <summary>
        /// Bone name
        /// </summary>
        [SerializeField] private Bone _boneType;
        public Bone boneType { get { return _boneType; } }
        /// <summary>
        /// Bone's link
        /// </summary>
        public GameObject bone_object;
    }
    /// <summary>
    /// Stores bones name and their coordinates on tesselation mesh
    /// </summary>
    public class FaceForms
    {
        static public Dictionary<(int, int), Bone> FACEMESH_RIGHT_EYEBROW = new Dictionary<(int, int), Bone>()
        {
            { (46, 53), Bone.Eyebrow_4_R },
            { (53, 52), Bone.Eyebrow_3_R },
            { (52, 65), Bone.Eyebrow_2_R },
            { (65, 55), Bone.Eyebrow_1_R },
            { (65, 55), Bone.Eyebrow_0_R }
};

        static public Dictionary<(int, int), Bone> FACEMESH_LEFT_EYEBROW_DICT = new Dictionary<(int, int), Bone>()
        {
            { (276, 283), Bone.Eyebrow_4_L },
            { (283, 282), Bone.Eyebrow_3_L },
            { (282, 295), Bone.Eyebrow_2_L },
            { (295, 285), Bone.Eyebrow_1_L }
        };
    }
}