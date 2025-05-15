using UnityEngine;

public class CharacterSwap : MonoBehaviour
{
    private GameObject[] characterList;
    private int index = 0;

    private void Start()
    {
        characterList = new GameObject[2];

        // Fill the array with our models
        for (int i = 0; i < 2; i++)
            characterList[i] = transform.GetChild(i).gameObject;

        // Toggle off all characters
        foreach (GameObject character in characterList)
            character.SetActive(false);
        
        // We toggle on the first character
        if (characterList[0])
            characterList[0].SetActive(true);
    }

    private void Update()
    {
        // Check if the Q key was pressed
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwapCharacter();
        }
    }

    public void SwapCharacter()
    {
        Rigidbody2D currentRb = characterList[index].GetComponent<Rigidbody2D>();

        Vector3 currentPosition = characterList[index].transform.position;
        Vector2 currentVelocity = currentRb != null ? currentRb.linearVelocity : Vector2.zero;

        characterList[index].SetActive(false);

        if (index == 0)
        {
            index = 1;
        }
        else
        {
            index = 0;
        }

        // Mover nuevo personaje a la posición anterior
        characterList[index].transform.position = currentPosition;

        // Activar nuevo personaje
        characterList[index].SetActive(true);

        // Asignar la misma velocidad si tiene Rigidbody2D
        Rigidbody2D newRb = characterList[index].GetComponent<Rigidbody2D>();
        if (newRb != null)
            newRb.linearVelocity = currentVelocity;
    }


}
