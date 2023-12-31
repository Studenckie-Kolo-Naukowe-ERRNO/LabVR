using System.Collections;
using UnityEngine;

// Environment
public class GenerateLabyrinth : MonoBehaviour {
    private int[,] labyrinth = new int[11, 11];
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject wayPrefab;
    [SerializeField] private GameObject endPrefab;
    [SerializeField] private GameObject startPrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject parent;
    private GameObject playerGfx;
    private Agent agent;
    private const int SCALE_MULTIPLIER = 1;
    [Range(0f, 1f)] public float speed;

    private void Start() {
        Generate();
        StartCoroutine(MoveTheCube());
    }

    [ContextMenu("Generate new labyrinth")]
    public void Generate() {
        DestroyTheLabyrinth();
        InitializeLabyrinth();
        GenerateMaze(1, 1);
        CreateEnd();
        DrawTheLabyrinth();

        agent = new Agent(labyrinth.GetLength(0), labyrinth.GetLength(1), 1, 1);
        GeneratePlayer();
    }

    public void GeneratePlayer() {
        playerGfx = Instantiate(playerPrefab);
        playerGfx.transform.SetParent(parent.transform, false);
        playerGfx.transform.localPosition = new Vector2(agent.posX * SCALE_MULTIPLIER, agent.posY * -SCALE_MULTIPLIER);
        playerGfx.transform.localScale = Vector3.one * SCALE_MULTIPLIER;
    }

    private void DestroyTheLabyrinth() {
        foreach (Transform child in parent.transform) {
            Destroy(child.gameObject);
        }
    }

    private void InitializeLabyrinth() {
        for (int rows = 0; rows < labyrinth.GetLength(0); rows++) {
            for (int columns = 0; columns < labyrinth.GetLength(1); columns++) {
                labyrinth[rows, columns] = 1; // Set all cells as walls initially
            }
        }
    }

    private void GenerateMaze(int startRows, int startColumns) {
        labyrinth[startRows, startColumns] = 2; // Mark the starting cell as a path
        RecursiveDFS(startRows, startColumns);
    }

    private void RecursiveDFS(int rows, int columns) {
        // Define random order for the directions (up, down, left, right)
        int[] directions = { 1, 2, 3, 4 };
        ShuffleArray(directions);

        foreach (int direction in directions) {
            int newRows;
            int newColumns;

            if (direction == 1) {
                newRows = rows - 2;
                newColumns = columns;
            } else if (direction == 2) {
                newRows = rows + 2;
                newColumns = columns;
            } else if (direction == 3) {
                newRows = rows;
                newColumns = columns - 2;
            } else if (direction == 4) {
                newRows = rows;
                newColumns = columns + 2;
            } else {
                // Handle unexpected direction value
                continue;
            }

            if (IsInBounds(newRows, newColumns) && labyrinth[newRows, newColumns] == 1) {
                labyrinth[newRows, newColumns] = 0; // Mark the cell as a path
                labyrinth[(rows + newRows) / 2, (columns + newColumns) / 2] = 0; // Mark the cell between current and next as a path
                RecursiveDFS(newRows, newColumns);
            }
        }
    }

    private void DrawTheLabyrinth() {
        for (int rows = 0; rows < labyrinth.GetLength(0); rows++) {
            for (int columns = 0; columns < labyrinth.GetLength(1); columns++) {
                if (labyrinth[rows, columns] == 1) {
                    GameObject obj = Instantiate(wallPrefab);
                    obj.transform.SetParent(parent.transform, false);
                    obj.transform.localScale = Vector3.one * SCALE_MULTIPLIER;
                    obj.transform.localPosition = new Vector2(rows * SCALE_MULTIPLIER, -columns * SCALE_MULTIPLIER);
                } else if (labyrinth[rows, columns] == 0) {
                    GameObject obj = Instantiate(wayPrefab);
                    obj.transform.SetParent(parent.transform, false);
                    obj.transform.localScale = new Vector3(1, 1, 0.01f);
                    obj.transform.localPosition = new Vector2(rows * SCALE_MULTIPLIER, -columns * SCALE_MULTIPLIER);
                } else if (labyrinth[rows, columns] == 3) {
                    GameObject obj = Instantiate(endPrefab);
                    obj.transform.SetParent(parent.transform, false);
                    obj.transform.localScale = Vector3.one * SCALE_MULTIPLIER;
                    obj.transform.localPosition = new Vector2(rows * SCALE_MULTIPLIER, -columns * SCALE_MULTIPLIER);
                } else {
                    GameObject obj = Instantiate(startPrefab);
                    obj.transform.SetParent(parent.transform, false);
                    obj.transform.localScale = Vector3.one * SCALE_MULTIPLIER;
                    obj.transform.localPosition = new Vector2(rows * SCALE_MULTIPLIER, -columns * SCALE_MULTIPLIER);
                }
            }
        }
    }

    private bool IsInBounds(int rows, int columns) {
        return rows >= 0 && rows < labyrinth.GetLength(0) - 1 && columns >= 0 && columns < labyrinth.GetLength(1) - 1;
    }

    private void ShuffleArray(int[] array) {
        for (int i = array.Length - 1; i > 0; i--) {
            int j = Random.Range(0, i + 1);
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    private void CreateEnd() {
        bool hasEnd = false;

        if (hasEnd) return;

        for (int rows = labyrinth.GetLength(0) - 1; rows >= 0; rows--) {
            for (int columns = labyrinth.GetLength(0) - 1; columns >= 0; columns--) {
                if (labyrinth[rows, columns] == 0) {
                    if ((labyrinth[rows - 1, columns] == 1 && labyrinth[rows, columns - 1] == 1 && labyrinth[rows, columns + 1] == 1) ||
                        (labyrinth[rows + 1, columns] == 1 && labyrinth[rows, columns - 1] == 1 && labyrinth[rows, columns + 1] == 1) ||
                        (labyrinth[rows + 1, columns] == 1 && labyrinth[rows - 1, columns] == 1 && labyrinth[rows, columns + 1] == 1) ||
                        (labyrinth[rows + 1, columns] == 1 && labyrinth[rows - 1, columns] == 1 && labyrinth[rows, columns - 1] == 1)) {
                        labyrinth[rows, columns] = 3;
                        hasEnd = true;
                        return;
                    }
                }
            }
        }
    }

    IEnumerator MoveTheCube() {
        yield return new WaitForSeconds(2);
        while (true) {
            int xPos = agent.posX;
            int yPos = agent.posY;
            int agentDecision = agent.MakeDecision();

            switch (agentDecision) {
                case 0:
                xPos--;
                break;
                case 1:
                yPos++;
                break;
                case 2:
                xPos++;
                break;
                case 3:
                yPos--;
                break;
                default:
                Debug.Log("YOU BROKE IT");
                break;
            }

            if (xPos < 0 || xPos > labyrinth.GetLength(0) || yPos < 0 || yPos > labyrinth.GetLength(1) || labyrinth[xPos, yPos] == 1) {
                agent.GiveReward(-1, agentDecision, agent.posX, agent.posY);
            } else if (labyrinth[xPos, yPos] == 3) {
                agent.GiveReward(100, agentDecision, 1, 1);
            } else {
                agent.GiveReward(0, agentDecision, xPos, yPos);
            }

            playerGfx.transform.localPosition = new Vector2(agent.posX * SCALE_MULTIPLIER, agent.posY * -SCALE_MULTIPLIER);

            yield return new WaitForSeconds(speed);
        }
    }
}
