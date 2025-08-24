using UnityEngine;

public class Continent : MonoBehaviour
{
    
    void Start()
    {
        int n = Random.Range(1, 6); // 1..5 inclusive
        for (int i = 0; i<transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false); //turns off all the countries
        
        transform.GetChild(n - 1).gameObject.SetActive(true); // turns on a country
        GameManager.instance.SetConNum(n-1); // updates the gameManager country that was choosen 
    }
   
}
