using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsLab
{
    public class KinematicBall : MonoBehaviour
    {
        [SerializeField] private GameObject pointEmmiter;
        [SerializeField] private GameObject lineEmmiter;

        public enum KinematicBallMode { None, Points, Trail }
        [SerializeField] private KinematicBallMode mode;
        [SerializeField] private Color[] modesColor;
        private Material thisMaterial;

        private void Start()
        {
            thisMaterial = gameObject.GetComponent<Renderer>().material;
            SetMode(0);
        }

        public void SetMode(KinematicBallMode newMode)
        {
            mode = newMode;

            pointEmmiter.SetActive(mode == KinematicBallMode.Points);
            lineEmmiter.SetActive(mode == KinematicBallMode.Trail);

            thisMaterial.color = modesColor[(int)mode];
        }


        [ContextMenu("ChangeMode")]
        public void ChangeModes()
        {
            mode++;
            if (mode > KinematicBallMode.Trail) mode = 0;
            SetMode(mode);
        }
    }
}