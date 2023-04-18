using UnityEngine;

namespace TVP
{
    [System.Serializable]
    class UziHitInfo
    {
        public Vector3 point;
        public Vector2 pixelData;
        public float dep;

        public Vector3 UP { get => point + new Vector3(0, 1, 0); }
        public Vector3 RIGHT { get => point + new Vector3(1, 0, 0); }
        public Vector3 FORWARD { get => point + new Vector3(0, 0, 1); }
        public bool IsFake { get; internal set; }
        public Vector3 DIRECTION { get; set; }
        public float CoeficientX { get; set; }
        public float CoeficientY { get; set; }

        public UziHitInfo(Vector3 point)
        {
            this.point = point;
        }
    }

}