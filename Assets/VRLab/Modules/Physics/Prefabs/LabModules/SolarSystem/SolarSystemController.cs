using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsLab
{
    public class SolarSystemController : MonoBehaviour
    {
        [SerializeField] private float sizeScale = 100000;
        [SerializeField] private float distanceScale = 2;
        [SerializeField] private float speedScale = 100;
        [SerializeField] private ParticleSystem asteroids;
        [SerializeField] private Planet[] planets;

        private const int CHANGE_SPEED_MULTIPLIER = 200;
        private void Start()
        {
            asteroids.transform.localScale = new Vector3(distanceScale, distanceScale, distanceScale);
            asteroids.Clear();
            asteroids.Play();
            for (int i = 1; i < planets.Length; i++)
            {
                planets[i].SetObject(sizeScale, distanceScale);
            }
        }

        private void Update()
        {
            for (int i = 1; i < planets.Length; i++)
            {
                float angle = (speedScale * Time.deltaTime) / (planets[i].revolutionPeriod / 365.25f);
                planets[i].planetObject.transform.RotateAround(transform.position, transform.up, angle);
            }
        }

        public void ChangeSpeed(float newValue)
        {
            speedScale = newValue * CHANGE_SPEED_MULTIPLIER;
        }
    }

    [System.Serializable]
    public class Planet
    {
        public string planetName;
        [Tooltip("(km)")]
        public float diameter;
        [Tooltip("(au)")]
        public float distanceFromSun;
        public float rotationPeriod;
        public float revolutionPeriod;
        public GameObject planetObject;

        public void SetObject(float scale, float dScale)
        {
            if (planetObject == null) return;
            float size = diameter / scale;
            planetObject.transform.localScale = new Vector3(size, size, size);
            planetObject.transform.localPosition = new Vector3(distanceFromSun / dScale, 0, 0);
        }
    }
}