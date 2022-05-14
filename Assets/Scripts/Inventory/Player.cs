using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    //private Dictionary<Item.ItemType, int> inventory;
    [SerializeField] Inventory inventory;
    [Header("UI")]
    [SerializeField] GameObject mainUICanvas;
    [SerializeField] TextMeshProUGUI canCounter;
    [SerializeField] TextMeshProUGUI glassBottleCounter;
    [SerializeField] TextMeshProUGUI plasticBottleCounter;
    [SerializeField] TextMeshProUGUI goldCounter;
    [SerializeField] Text sellText;
    [SerializeField] Text priceText;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] TextMeshProUGUI moneyCollectedText;
    [SerializeField] TextMeshProUGUI soldText;
    [SerializeField] Animator soldAnim;

    [Header("Audio")]
    [SerializeField] public List<AudioClip> woodMoves;
    [SerializeField] public List<AudioClip> stoneMoves;
    [SerializeField] public List<AudioClip> plasticMoves;
    [SerializeField] public AudioClip collectCan;
    [SerializeField] public List<AudioClip> softHits;
    [SerializeField] public AudioClip sellingClip;

    [SerializeField] public List<AudioClip> plate;
    [SerializeField] AudioClip sellSound;

    AudioSource audioPlayer;


    [Header("Trash Bag")]
    [SerializeField] GameObject trashBagOnTable;
    [SerializeField] float growthFactor;


    [Header("Timing")]
    [SerializeField] float startTime;
    [SerializeField] TextMeshProUGUI timeLeftText;
    private float balance = 0f;
    private float timeLeft;
    private bool gameOver;

    private float sellingCooldown = 2f;
    private bool canSell = true;
    private void Awake()
    {
        inventory = new Inventory();
    }

    void Start()
    {
        mainUICanvas.SetActive(true);
        gameOverCanvas.SetActive(false);

        timeLeft = startTime;
        gameOver = false;
        audioPlayer = GetComponent<AudioSource>();
        soldAnim = GameObject.Find("MainUI").GetComponent<Animator>();
    }

    void Update()
    {
        if (gameOver) return;

        if (timeLeft <= 0)
        {
            gameOver = true;
            Time.timeScale = 0;
            moneyCollectedText.text = balance.ToString("0.00") + "$";
            mainUICanvas.SetActive(false);
            gameOverCanvas.SetActive(true);
        }
        timeLeft -= Time.deltaTime;
        timeLeftText.text = timeLeft.ToString("0.0");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (gameOver) return;
        switch (other.gameObject.tag)
        {
            case "Item":
                ItemPrefab item = other.gameObject.GetComponent<ItemPrefab>();
                Destroy(other.gameObject);
                inventory.AddItem(item.item);
                UpdateUI(item.item);
                audioPlayer.PlayOneShot(collectCan);
                break;

            case "SellingMachine":
                SellItems();
                break;

            case "wood":
                audioPlayer.PlayOneShot(woodMoves[Random.Range(0, woodMoves.Count)]);
                break;

            case "plastic":
                audioPlayer.PlayOneShot(plasticMoves[Random.Range(0, plasticMoves.Count)]);
                break;
            case "softSurface":
                audioPlayer.PlayOneShot(softHits[Random.Range(0, softHits.Count)]);
                break;
            case "plate":
                audioPlayer.PlayOneShot(plate[Random.Range(0, plate.Count)], 0.05f);
                break;
            case "heavy":
                audioPlayer.PlayOneShot(stoneMoves[Random.Range(0, stoneMoves.Count)], 0.2f);
                break;


        }
    }

    private void UpdateUI(Item item)
    {
        int count = inventory.GetCount(item.itemType);
        switch (item.itemType)
        {
            case Item.ItemType.can:
                canCounter.text = "" + count.ToString();
                break;
            case Item.ItemType.glassBottle:
                glassBottleCounter.text = "" + count.ToString();
                break;
            case Item.ItemType.plasticBottle:
                plasticBottleCounter.text = "" + count.ToString();
                break;
        }
    }

    public void FinalGolds()
    {

        int counter1 = inventory.GetCount(Item.ItemType.can); //counter for cans
        int counter2 = inventory.GetCount(Item.ItemType.glassBottle);//counter for glassBottle
        int counter3 = inventory.GetCount(Item.ItemType.plasticBottle);//counter for plasticBottle
        balance += (float)(counter1 * 0.25) + (float)(counter2 * 0.4) + (float)(counter3 * 0.3);
        goldCounter.text = "" + balance.ToString();


    }

    public void GetGold()
    {


        int counter1 = inventory.GetCount(Item.ItemType.can); //counter for cans
        int counter2 = inventory.GetCount(Item.ItemType.glassBottle);//counter for glassBottle
        int counter3 = inventory.GetCount(Item.ItemType.plasticBottle);//counter for plasticBottle
        float finalGoldCounter = (float)(counter1 * 0.25) + (float)(counter2 * 0.4) + (float)(counter3 * 0.3);

        sellText.text = "You have: \n\n" + counter1 + " cans,\n" + counter2 + " plastic bottles and\n" + counter3 + " glass bottles!\n\n" + "That is: " + finalGoldCounter + "$";

    }

    void SellItems()
    {
        audioPlayer.PlayOneShot(sellingClip);
        if (!canSell) return;
        canSell = false;
        int counter1 = inventory.GetCount(Item.ItemType.can); //counter for cans
        int counter2 = inventory.GetCount(Item.ItemType.glassBottle);//counter for glassBottle
        int counter3 = inventory.GetCount(Item.ItemType.plasticBottle);//counter for plasticBottle
        var value = (float)(counter1 * 0.25) + (float)(counter2 * 0.4) + (float)(counter3 * 0.3);
        balance += value;

        soldAnim.SetTrigger("Sell");
        StartCoroutine("SellingCooldown");
        //soldText.text = "Dupsko";
        soldText.text = "+" + value.ToString("0.00") + "€";
        audioPlayer.PlayOneShot(sellSound);
        canCounter.text = "0";
        glassBottleCounter.text = "0";
        plasticBottleCounter.text = "0";
        goldCounter.text = balance.ToString("0.00");
        inventory.Empty();
    }

    IEnumerator SellingCooldown()
    {
        yield return new WaitForSeconds(sellingCooldown);
        canSell = true;
    }

    /*public void SoldMessage()
    {
        sellText.text = "Thank you! You can go and pick up items again!";
        priceText.text = "";
    }
    */
}
