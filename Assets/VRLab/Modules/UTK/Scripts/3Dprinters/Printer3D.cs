using System.Collections;
using System.Collections.Generic;
using System.Globalization; // WA¯NE: Do poprawnego parsowania liczb z kropk¹
using UnityEngine;

namespace UTKLab.printers3d
{
    public class Printer3D : MonoBehaviour
    {
        [Header("Code")]
        [SerializeField] private TextAsset gcode;

        [Header("Axis Movers (Drag GameObjects here)")]
        [SerializeField] private Transform xAxisMover;
        [SerializeField] private Transform yAxisMover;
        [SerializeField] private Transform zAxisMover;
        [Space]
        [SerializeField] private bool invertXaxis;
        [SerializeField] private bool invertYaxis;
        [SerializeField] private bool invertZaxis;
        [Space]
        [SerializeField] private Renderer modelRenderer;

        // Przechowujemy pozycje startowe (lokalne) dla ka¿dej osi
        private Vector3 xOrigin;
        private Vector3 yOrigin;
        private Vector3 zOrigin;

        [Header("Parameters")]
        [SerializeField] private float speed = 100.0f; // Prêdkoœæ w mm/s
        [SerializeField] private float gcodeScale = 0.001f; // Skala (np. 0.001 dla mm -> m)

        private Material modelMaterial;
        private Coroutine printingLoopCorountine;

        private float modelMinY_ObjectSpace = 0f;
        private float modelMaxY_ObjectSpace = 0f;

        [Header("Debug")]
        [SerializeField] private Vector3 debugMove;

        private void Start()
        {
            // Zapisujemy pozycje startowe (lokalne)
            if (xAxisMover) xOrigin = xAxisMover.localPosition;
            if (yAxisMover) yOrigin = yAxisMover.localPosition;
            if (zAxisMover) zOrigin = zAxisMover.localPosition;

            // --- SEKCJA START() SHADERA (bez zmian) ---
            if (modelRenderer != null)
            {
                Mesh modelMesh = null;
                MeshFilter mf = modelRenderer.GetComponent<MeshFilter>();
                if (mf != null)
                {
                    modelMesh = mf.sharedMesh;
                }
                else
                {
                    SkinnedMeshRenderer smr = modelRenderer.GetComponent<SkinnedMeshRenderer>();
                    if (smr != null)
                        modelMesh = smr.sharedMesh;
                }

                if (modelMesh != null)
                {
                    modelMinY_ObjectSpace = modelMesh.bounds.min.y;
                    modelMaxY_ObjectSpace = modelMesh.bounds.max.y;
                    Debug.Log($"Wykryto granice modelu: Y od {modelMinY_ObjectSpace} do {modelMaxY_ObjectSpace}");
                }
                else
                {
                    Debug.LogError("Nie mo¿na znaleŸæ siatki (Mesh) na 'modelRenderer'!");
                    modelMinY_ObjectSpace = -0.5f;
                    modelMaxY_ObjectSpace = 0.5f;
                }

                modelMaterial = modelRenderer.material;
                modelMaterial.SetFloat("_ClipHeight", modelMinY_ObjectSpace);
            }
            // --- KONIEC SEKCJI SHADERA ---
        }

        [ContextMenu("StartPrint")]
        public void StartPrint()
        {
            if (gcode == null) return;
            if (modelRenderer == null) return;

            if (xAxisMover == null || yAxisMover == null || zAxisMover == null)
            {
                Debug.LogError("Brakuje referencji do jednej lub wiêcej osi (Axis Movers)! Przypisz je w Inspektorze.");
                return;
            }

            if (printingLoopCorountine != null) StopCoroutine(printingLoopCorountine);
            printingLoopCorountine = StartCoroutine(PrintingLoop(gcode));
        }

        [ContextMenu("StopPrint")]
        public void StopPrint()
        {
            if (printingLoopCorountine != null) StopCoroutine(printingLoopCorountine);
        }

        [ContextMenu("Level")]
        public void Level()
        {
            if (xAxisMover == null || yAxisMover == null || zAxisMover == null)
            {
                Debug.LogError("Brakuje referencji do jednej lub wiêcej osi (Axis Movers)! Przypisz je w Inspektorze.");
                return;
            }

            if (printingLoopCorountine != null) StopCoroutine(printingLoopCorountine);
            StartCoroutine(MoveAxesToTarget(Vector3.zero));
        }

        [ContextMenu("Debug move")]
        public void MoveDebug()
        {
            if (xAxisMover == null || yAxisMover == null || zAxisMover == null)
            {
                Debug.LogError("Brakuje referencji do jednej lub wiêcej osi (Axis Movers)! Przypisz je w Inspektorze.");
                return;
            }

            if (printingLoopCorountine != null) StopCoroutine(printingLoopCorountine);
            StartCoroutine(MoveAxesToTarget(debugMove));
        }

        private void OnDestroy()
        {
            if (modelMaterial != null)
            {
                Destroy(modelMaterial);
            }
        }

        private IEnumerator PrintingLoop(TextAsset code)
        {
            // Resetujemy shader na start
            if (modelMaterial) modelMaterial.SetFloat("_ClipHeight", modelMinY_ObjectSpace);

            // ZAKTUALIZOWANE: Resetujemy pozycje nowych osi do ich punktów startowych
            if (xAxisMover) xAxisMover.localPosition = xOrigin;
            if (yAxisMover) yAxisMover.localPosition = yOrigin;
            if (zAxisMover) zAxisMover.localPosition = zOrigin;

            string[] lines = code.text.Split('\n');
            CultureInfo ci = CultureInfo.InvariantCulture;

            // KROK 1: ZnajdŸ max Z (bez zmian)
            float maxGcodeZ = 7.0f; // Domyœlna
            foreach (string rawLine in lines)
            {
                string line = rawLine.Trim();
                if (line.StartsWith(";MAXZ:"))
                {
                    maxGcodeZ = float.Parse(line.Substring(7), ci);
                    Debug.Log($"Symulacja dla MAXZ: {maxGcodeZ}mm");
                    break;
                }
            }

            Vector3 currentGcodePos = Vector3.zero;
            float currentE = 0;

            // 3. G³ówna pêtla czytaj¹ca G-code
            foreach (string rawLine in lines)
            {
                string line = rawLine;
                int commentIndex = line.IndexOf(';');
                if (commentIndex != -1) line = line.Substring(0, commentIndex);
                line = line.Trim();

                if (string.IsNullOrEmpty(line) || line.StartsWith("M")) continue;

                string[] parts = line.Split(' ');
                string command = parts[0];

                if (command == "G0" || command == "G1")
                {
                    Vector3 newGcodePos = currentGcodePos;
                    float newE = currentE;

                    for (int i = 1; i < parts.Length; i++)
                    {
                        string part = parts[i];
                        if (string.IsNullOrEmpty(part) || part.Length <= 1) continue;

                        switch (part[0])
                        {
                            case 'X': newGcodePos.x = (invertXaxis ? -1 : 1) * float.Parse(part.Substring(1), ci); break;
                            case 'Y': newGcodePos.y = (invertYaxis ? -1 : 1) * float.Parse(part.Substring(1), ci); break;
                            case 'Z': newGcodePos.z = (invertZaxis ? -1 : 1) * float.Parse(part.Substring(1), ci); break;
                            case 'E': newE = float.Parse(part.Substring(1), ci); break;
                        }
                    }

                    // Logika shadera (bez zmian)
                    if (newGcodePos.z != currentGcodePos.z && modelMaterial != null)
                    {
                        float normalizedHeight = newGcodePos.z / maxGcodeZ;
                        float newClipHeight = Mathf.Lerp(modelMinY_ObjectSpace, modelMaxY_ObjectSpace, normalizedHeight);
                        modelMaterial.SetFloat("_ClipHeight", newClipHeight);
                    }

                    // ZAKTUALIZOWANE: Wywo³ujemy now¹ korutynê ruchu
                    // Przekazujemy tylko docelowe wspó³rzêdne G-code (w mm)
                    yield return StartCoroutine(MoveAxesToTarget(newGcodePos));

                    currentGcodePos = newGcodePos;
                    currentE = newE;
                }
                else if (command == "G28") // Homing
                {
                    currentGcodePos = Vector3.zero;
                    currentE = 0;

                    // ZAKTUALIZOWANE: Przesuñ do G-code (0,0,0)
                    yield return StartCoroutine(MoveAxesToTarget(Vector3.zero));

                    // Resetuj shader
                    if (modelMaterial) modelMaterial.SetFloat("_ClipHeight", modelMinY_ObjectSpace);
                }
                else if (command == "G92")
                {
                    if (line.Contains("E0"))
                    {
                        currentE = 0;
                    }
                }
            }

            Debug.Log("Symulacja zakoñczona!");
            if (modelMaterial) modelMaterial.SetFloat("_ClipHeight", modelMaxY_ObjectSpace);
            printingLoopCorountine = null;
        }

        private IEnumerator MoveAxesToTarget(Vector3 gcodeTarget)
        {
            // 1. Oblicz LOKALNE pozycje docelowe dla ka¿dego "movera"
            Vector3 xTargetLocal = xOrigin + new Vector3(gcodeTarget.x * gcodeScale, 0, 0);
            Vector3 yTargetLocal = yOrigin + new Vector3(0, gcodeTarget.z * gcodeScale, 0);
            Vector3 zTargetLocal = zOrigin + new Vector3(0, 0, gcodeTarget.y * gcodeScale);


            // Oblicz prêdkoœæ kroku
            float unitySpeed = speed * gcodeScale;

            // 2. Pêtla interpoluj¹ca (bez zmian)
            while (
                Vector3.Distance(xAxisMover.localPosition, xTargetLocal) > 0.0001f ||
                Vector3.Distance(yAxisMover.localPosition, yTargetLocal) > 0.0001f ||
                Vector3.Distance(zAxisMover.localPosition, zTargetLocal) > 0.0001f)
            {
                float step = unitySpeed * Time.deltaTime;

                if (xAxisMover)
                    xAxisMover.localPosition = Vector3.MoveTowards(
                        xAxisMover.localPosition, xTargetLocal, step);

                if (yAxisMover)
                    yAxisMover.localPosition = Vector3.MoveTowards(
                        yAxisMover.localPosition, yTargetLocal, step);

                if (zAxisMover)
                    zAxisMover.localPosition = Vector3.MoveTowards(
                        zAxisMover.localPosition, zTargetLocal, step);

                yield return null;
            }

            // 3. Ustawienie koñcowe dla 100% precyzji (bez zmian)
            if (xAxisMover) xAxisMover.localPosition = xTargetLocal;
            if (yAxisMover) yAxisMover.localPosition = yTargetLocal;
            if (zAxisMover) zAxisMover.localPosition = zTargetLocal;
        }
      
    }
}