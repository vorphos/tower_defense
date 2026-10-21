using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("Économie")]
    [SerializeField] public int money = 150;
    
    [Header("Vies")]
    [SerializeField] public int lives = 20;
    
    private bool _isGameOver = false;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    public int GetMoney()
    {
        return money;
    }

    public int GetLives()
    {
        return lives;
    }

    public bool IsGameOver()
    {
        return _isGameOver;
    }

    public bool Spend(int cost)
    {
        if (money < cost)
        {
            return false;
        }
        money -= cost;
        return true;
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public void LoseLife(int amount)
    {
        if (_isGameOver) return;

        lives -= amount;
        if (lives <= 0)
        {
            lives = 0;
            EndGame(false);
        }
    }

    public void EndGame(bool victory)
    {
        if (_isGameOver) return;
        
        _isGameOver = true;
        Time.timeScale = 0;
    }

    public void ToggleSpeed()
    {
        if (_isGameOver) return;
        
        if (Time.timeScale == 1f)
        { 
            Time.timeScale = 2f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
