using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UIElements;

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
        [SerializeField] private float moonRevolutionPeriod;
        [SerializeField] private float moonDiameter;
        [SerializeField] private float moonDistanceFromEarth;

        private bool planetsCanRotate;

        private const int CHANGE_SPEED_MULTIPLIER = 200;
        private void Start()
        {
            planetsCanRotate = true;

            asteroids.transform.localScale = new Vector3(distanceScale, distanceScale, distanceScale);
            asteroids.Clear();
            asteroids.Play();
            for (int i = 1; i < planets.Length; i++)
            {
                planets[i].SetObject(sizeScale, distanceScale);

                planets[i].planetNameText.text = planets[i].planetName;
                planets[i].planetNameText.transform.position = planets[i].planetObject.transform.position;
            }
            CalculatePosOfTheMoon();
        }

        private void Update()
        {

            MoveTheMoon();

            for (int i = 1; i < planets.Length; i++)
            {
                float angle = (speedScale * Time.deltaTime) / (planets[i].revolutionPeriod / 365.25f);
                planets[i].planetObject.transform.RotateAround(transform.position, transform.up, angle);

                if (planetsCanRotate)
                {
                    planets[i].planetObject.transform.Rotate(Vector3.up * 360.0f / planets[i].rotationPeriod * Time.deltaTime);
                }

            }
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

        [ContextMenu("Planets Rotates Off")]
        public void PlanetsRotationOff()
        {
            planetsCanRotate = false;

        }

        [ContextMenu("Planets Rotates On")]
        public void PlanetsRotationOn()
        {
            planetsCanRotate = true;

        }

        private void CalculatePosOfTheMoon() {
            Vector3 earthPosition = earthGameObject.transform.localPosition;
            float outsideEarth = earthPosition.x + earthGameObject.transform.localScale.x;

            float size = moonDiameter / sizeScale;
            moonGameObject.transform.localScale = new Vector3(size, size, size);
            moonGameObject.transform.localPosition = new Vector3(moonDistanceFromEarth / distanceScale + outsideEarth, 0, 0);
        }

        private void MoveTheMoon() {
            Vector3 earthPosition = earthGameObject.transform.position;

            float angle = (speedScale * Time.deltaTime) / (moonRevolutionPeriod / 365.25f);
            moonGameObject.transform.RotateAround(earthPosition, transform.up, angle);
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