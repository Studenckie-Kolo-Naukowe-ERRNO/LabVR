using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
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

        [SerializeField] private GameObject earthGameObject;
        [SerializeField] private GameObject moonGameObject;
        [SerializeField] private float orbitSpeed;
        [SerializeField] private float orbitRadius;

        private const int CHANGE_SPEED_MULTIPLIER = 200;
        private void Start()
        {
            CalculatePosOfTheMoon();
            asteroids.transform.localScale = new Vector3(distanceScale, distanceScale, distanceScale);
            asteroids.Clear();
            asteroids.Play();
            for (int i = 1; i < planets.Length; i++)
            {
                planets[i].SetObject(sizeScale, distanceScale);

                planets[i].planetNameText.text = planets[i].planetName;
                planets[i].planetNameText.transform.position = planets[i].planetObject.transform.position;
            }
        }

        private void Update()
        {
            for (int i = 1; i < planets.Length; i++)
            {
                float angle = (speedScale * Time.deltaTime) / (planets[i].revolutionPeriod / 365.25f);
                planets[i].planetObject.transform.RotateAround(transform.position, transform.up, angle);

            }

            MoveTheMoon();
        }


        public void ChangeSpeed(float newValue)
        {
            speedScale = newValue * CHANGE_SPEED_MULTIPLIER;
        }

        [ContextMenu("Stop All The Planets")]
        public void StopsAllThePlanets() {
            speedScale = 0;
        }

        [ContextMenu("Start All The Planets")]
        public void StartAllThePlanets() {
            speedScale = 100;
        }

        [ContextMenu("Planets Names On")]
        public void PlanetsNamesOn()
        {
            foreach (Planet planet in planets)
            {
                planet.planetNameText.gameObject.SetActive(true);
            }
        }

        [ContextMenu("Planets Names Off")]
        public void PlanetsNamesOff()
        {
            foreach (Planet planet in planets)
            {
                planet.planetNameText.gameObject.SetActive(false);
            }

        }

        private void CalculatePosOfTheMoon() {
            Vector3 earthPosition = earthGameObject.transform.position;
            Vector3 initialPosition = earthPosition + new Vector3(orbitRadius, 0f, 0f);

            moonGameObject.transform.position = initialPosition;
        }

        private void MoveTheMoon() {
            Vector3 earthPosition = earthGameObject.transform.position;
            float angle = orbitSpeed * Time.deltaTime * speedScale;

            moonGameObject.transform.RotateAround(earthPosition, Vector3.up, angle);
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
        public TextMesh planetNameText;

        public void SetObject(float scale, float dScale)
        {
            if (planetObject == null) return;
            float size = diameter / scale;
            planetObject.transform.localScale = new Vector3(size, size, size);
            planetObject.transform.localPosition = new Vector3(distanceFromSun / dScale, 0, 0);
        }
    }
}