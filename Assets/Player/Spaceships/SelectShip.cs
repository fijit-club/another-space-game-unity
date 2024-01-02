using System.Collections.Generic;
using SpaceEscape;
using TMPro;
using UnityEngine;

public class SelectShip : MonoBehaviour
{
    public Spaceship[] spaceships;
    public Spaceship currentSpaceship;
    
    [SerializeField] private SpriteRenderer spaceShipImage;
    [SerializeField] private SpriteRenderer spaceShipVisual;
    [SerializeField] private GameObject playImage;
    [SerializeField] private GameObject abilityButton;
    [SerializeField] private TMP_Text shipText;
    [SerializeField] private GameObject buyArea;
    [SerializeField] private TMP_Text buyText;
    //[SerializeField] private Animator visual;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text shipCostCoinsText;
    
    private int _currentIndex;

    public int GetIndex()
    {
        return _currentIndex;
    }
    
    public void GoRight()
    {
        if (_currentIndex < spaceships.Length - 1)
            _currentIndex++;
        else
            _currentIndex = 0;
        
        //visual.Play("MoveRight", -1, 0f);
        
        UpdateProperties();
        ChangeSprite();
    }

    public void GoLeft()
    {
        if (_currentIndex > 0)
            _currentIndex--;
        else
            _currentIndex = spaceships.Length - 1;
        
        //visual.Play("MoveLeft", -1, 0f);
        UpdateProperties();
        ChangeSprite();
    }

    public void PurchaseCar()
    {
        Bridge.GetInstance().BuySpaceship(currentSpaceship.id);

        currentSpaceship.purchased = true;
        coinsText.text = Bridge.GetInstance().thisPlayerInfo.coins.ToString();
        
        UpdateProperties();
    }

    public void CheckShips()
    {
        List<SpaceEscape.Assets> dataInBridge = Bridge.GetInstance().thisPlayerInfo.data.assets;
        
        for (int i = 0; i < spaceships.Length; i++)
        {
            for (int j = 0; j < dataInBridge.Count; j++)
            {
                string id = dataInBridge[j].id;
                if (spaceships[i].id == id)
                    spaceships[i].purchased = true;
            }
        }

        coinsText.text = Bridge.GetInstance().thisPlayerInfo.coins.ToString();
    }

    public void ChangeSprite()
    {
        spaceShipVisual.sprite = currentSpaceship.sprite;
    }
    
    public void UpdateProperties()
    {
        currentSpaceship = spaceships[_currentIndex];
        
        spaceShipImage.sprite = currentSpaceship.sprite;

        List<SpaceEscape.Assets> spaceshipsData = Bridge.GetInstance().thisPlayerInfo.data.assets;

        shipText.text = currentSpaceship.name;

        if (_currentIndex > 0)
        {
            abilityButton.SetActive(true);
        }
        else
            abilityButton.SetActive(false);

        if (spaceships[_currentIndex].purchased)
        {
            playImage.SetActive(true);
            buyArea.SetActive(false);
            buyText.gameObject.SetActive(false);
        }
        else
        {
            playImage.SetActive(false);

            buyText.gameObject.SetActive(true);
            
            if (Bridge.GetInstance().thisPlayerInfo.coins >= currentSpaceship.coinsRequired)
            {
                buyText.text = "Tap to Purchase";
                buyArea.SetActive(true);
            }
            else
            {
                buyText.text = "Not enough coins";
                buyArea.SetActive(false);
            }
            shipCostCoinsText.text = currentSpaceship.coinsRequired.ToString();

        }
    }
}