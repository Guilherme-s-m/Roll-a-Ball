using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI; // Importante para trabalhar com UI
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    // Rigidbody of the player.
    private Rigidbody rb;

    // Variable to keep track of collected "PickUp" objects.
    private int count;

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    public float speed = 0;

    // UI text component to display count of "PickUp" objects collected.
    public TextMeshProUGUI countText;

    // UI object to display winning text.
    public GameObject winTextObject;

    // AudioSource component for playing sounds.
    private AudioSource audioSource;


    public int maxHealth = 100; // Vida máxima
    private int currentHealth; // Vida atual
    public Slider healthSlider; // Referência para o Slider da barra de vida

    // Adicionando um temporizador para controle de dano contínuo
    private float damageTimer = 0.0f;
    private bool isTakingContinuousDamage = false;

    // Start is called before the first frame update.
    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();

        // Get the AudioSource component attached to the GameObject.
        audioSource = GetComponent<AudioSource>();

        // Initialize count to zero.
        count = 0;

        // Update the count display.
        SetCountText();

       // Inicializa a vida atual com o valor máximo
        currentHealth = maxHealth;

        // Configura o valor máximo da barra de vida na UI
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth; // Configura o valor atual da barra
    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object the player collided with has the "PickUp" tag.
        if (other.gameObject.CompareTag("PickUp"))
        {
            // Play the pickup sound effect.
            audioSource.Play();

            // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);

            // Increment the count of "PickUp" objects collected.
            count += 1;

            // Update the count display.
            SetCountText();
        }
    }

    // Function to update the displayed count of "PickUp" objects collected.
    void SetCountText()
    {
        // Update the count text with the current count.
        countText.text = "Count: " + count.ToString();

        // Check if the count has reached or exceeded the win condition.
        if (count >= 5)
        {
            // Display the win text.
            SceneManager.LoadScene("VictoryScene");
        }
    }

        // Método para reduzir a vida do personagem
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth; // Atualiza a barra de vida na UI

        // Verifica se a vida chegou a 0
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("LoseScene");
            // Aqui você pode adicionar a lógica para o personagem morrer
        }
    }
    void Update()
    {
        // Código para dano contínuo
        if (isTakingContinuousDamage)
        {
            if (damageTimer <= 0)
            {
                TakeDamage(10); // Assume 10 de dano
                damageTimer = 1.0f; // Reset timer para 1 segundo
            }
            else
            {
                damageTimer -= Time.deltaTime;
            }
        }
    }

        void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isTakingContinuousDamage = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isTakingContinuousDamage = false;
            damageTimer = 0; // Reset timer
        }
    }

}

    