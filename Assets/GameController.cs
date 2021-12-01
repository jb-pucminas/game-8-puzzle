using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<GameObject> boxes = new List<GameObject>();
    GameObject[,] matriz = new GameObject[3,3];

    bool winner = false;
    public GameObject winHUD;

    void Start()
    {
        winHUD.SetActive(false);
        SortBoxesPositions();
        MoveBoxes();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0) && winner == false)
        {
            HandleClick();
            winner = CheckVictory();

            if (winner) winHUD.SetActive(true);
        }
    }

    void SortBoxesPositions()
    {
        for (int l = 0; l < 3; l++)
        {
            for (int c = 0; c < 3; c++)
            {
                if (boxes.Count > 0)
                {
                    int boxIndex = Random.Range(0, boxes.Count);
                    GameObject box = boxes[boxIndex];

                    matriz[l, c] = box;
                    boxes.Remove(box);
                }
            }
        }
    }

    void MoveBoxes()
    {
        for (int l = 0; l < 3; l++)
        {
            for (int c = 0; c < 3; c++)
            {
                if (matriz[l, c] != null)
                {
                    GameObject box = matriz[l, c];
                    Movement(box, l, c);
                }
            }
        }
    }

    void HandleClick()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 clickPos = Camera.main.ScreenToWorldPoint(mousePos);

        RaycastHit hit;
        if (Physics.Raycast(clickPos, Vector3.forward, out hit))
        {
            GameObject box = hit.collider.gameObject;
            Vector2 boxPos = GetBoxPosition(box);
            Vector2 emptyPos = GetBoxPosition(null);

            bool shouldMovement = ShouldMovement(boxPos, emptyPos);
            if (shouldMovement)
            {
                matriz[(int) boxPos.y, (int) boxPos.x] = null;
                matriz[(int) emptyPos.y, (int) emptyPos.x] = box;
                Movement(hit.collider.gameObject, (int)emptyPos.y, (int)emptyPos.x);
            }
        }
    }

    Vector2 GetBoxPosition(GameObject box)
    {
        Vector2 position = new Vector2(0, 0);

        for (int l = 0; l < 3; l++)
        {
            for (int c = 0; c < 3; c++)
            {
                if (matriz[l, c] == box)
                {
                    position.x = c;
                    position.y = l;
                    break;
                }
            }
        }

        return position;
    }

    void Movement(GameObject box, int line, int column)
    {
        int x = 1 + column * 2;
        int y = (1 + line * 2) * -1;
        box.transform.localPosition = new Vector3(x, y, 0);
        box.transform.localPosition = new Vector3(x, y, 0);
    }

    bool ShouldMovement(Vector2 box, Vector2 empty)
    {
        bool should = false;
        if ((empty.x == box.x + 1 || empty.x == box.x - 1) && empty.y == box.y)
        {
            should = true;
        }
        else if ((empty.y == box.y + 1 || empty.y == box.y - 1) && empty.x == box.x)
        {
            should = true;
        }

        return should;
    }

    bool CheckVictory()
    {
        List<int> numbers = new List<int>();

        for (int l = 0; l < 3; l++)
        {
            for (int c = 0; c < 3; c++)
            {
                if (matriz[l, c] != null)
                {
                    BoxController box = matriz[l, c].GetComponent<BoxController>();
                    numbers.Add(box.value);
                }
            }
        }


        bool victory = true;
        int numRef = 1;
        foreach (var n in numbers)
        {
            if (numRef != n)
            {
                victory = false;
                break;
            }
            numRef++;
        }

        return victory;
    }
}
