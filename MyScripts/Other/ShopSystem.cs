using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    //public GameObject PressE;
    public AudioClip ItemBoughtSound;
    public AudioClip NotEnoughMoneySound;

    public Transform ItemBoughtSpawnPoint_Car;
    public Transform ItemBoughtSpawnPoint_Truck;
    public Transform[] ItemBoughtSpawnPoint_Potions;

    public GameObject car1;
    public GameObject car2;
    public GameObject car3;
    public GameObject car4;

    public GameObject potionH;
    public GameObject potionS;
    public GameObject potionE;

    private int priceOfItem;
    private int currentCoins;
    private int currentMoney;

    private AudioSource audioSource;

    public Text moneyAmount_txt;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //Get data
        currentCoins = CoinManager.numOfCoins;
        currentMoney = WaveSpawner.scorePoints;

        moneyAmount_txt.text = " " + currentMoney.ToString();

    }

    public void BuyCar1()
    {
        priceOfItem = 100;

        if(priceOfItem > currentMoney)
        {
            //Play animation of DENIAL
            Debug.Log("You have no money!");
            audioSource.PlayOneShot(NotEnoughMoneySound);
        }
        else
        {
            //Item Bought
            WaveSpawner.scorePoints -= priceOfItem;
            audioSource.PlayOneShot(ItemBoughtSound);
            //TO:DO --> Instansiate rather then activating component
            //car1.SetActive(true);
            //PressE.SetActive(true);

            if(car1 != null && ItemBoughtSpawnPoint_Car != null)
            {
                Instantiate(car1, ItemBoughtSpawnPoint_Car.position, ItemBoughtSpawnPoint_Car.rotation);
            }
            else { Debug.Log("You dont have any objects or spawn points attached to this script"); }
            
        }
    }

    public void BuyCar2()
    {
        priceOfItem = 1000;

        if (priceOfItem > currentMoney)
        {
            //Play animation of DENIAL
            Debug.Log("You have no money!");
            audioSource.PlayOneShot(NotEnoughMoneySound);
        }
        else
        {
            WaveSpawner.scorePoints -= priceOfItem;
            audioSource.PlayOneShot(ItemBoughtSound);

            //PressE.SetActive(true);

            if (car2 != null && ItemBoughtSpawnPoint_Car != null) {
                Instantiate(car2, ItemBoughtSpawnPoint_Car.position, ItemBoughtSpawnPoint_Car.rotation);
            } else { Debug.Log("You dont have any objects or spawn points attached to this script"); }
        }
    }

    public void BuyCar3()
    {
        priceOfItem = 1500;

        if (priceOfItem > currentMoney)
        {
            //Play animation of DENIAL
            Debug.Log("You have no money!");
            audioSource.PlayOneShot(NotEnoughMoneySound);
        }
        else
        {
            WaveSpawner.scorePoints -= priceOfItem;
            audioSource.PlayOneShot(ItemBoughtSound);

            //PressE.SetActive(true);

            if (car3 != null && ItemBoughtSpawnPoint_Truck != null) {
                Instantiate(car3, ItemBoughtSpawnPoint_Truck.position, ItemBoughtSpawnPoint_Truck.rotation);
            } else { Debug.Log("You dont have any objects or spawn points attached to this script"); }
        }
    }

    public void BuyCar4()
    {
        priceOfItem = 2000;

        if (priceOfItem > currentMoney)
        {
            //Play animation of DENIAL
            Debug.Log("You have no money!");
            audioSource.PlayOneShot(NotEnoughMoneySound);
        }
        else
        {
            WaveSpawner.scorePoints -= priceOfItem;
            audioSource.PlayOneShot(ItemBoughtSound);

            //PressE.SetActive(true);

            if (car4 != null && ItemBoughtSpawnPoint_Car != null) {
                Instantiate(car4, ItemBoughtSpawnPoint_Car.position, ItemBoughtSpawnPoint_Car.rotation);
            } else { Debug.Log("You dont have any objects or spawn points attached to this script"); }
        }
    }

    public void BuyGun_Uzi()
    {
        priceOfItem = 99999;

        if (priceOfItem > currentMoney)
        {
            //Play animation of DENIAL
            Debug.Log("You have no money!");
            audioSource.PlayOneShot(NotEnoughMoneySound);
        }
        else
        {
            WaveSpawner.scorePoints -= priceOfItem;
            audioSource.PlayOneShot(ItemBoughtSound);
        }
    }

    public void BuyGun_ScopedAR()
    {
        priceOfItem = 99999;

        if (priceOfItem > currentMoney)
        {
            //Play animation of DENIAL
            Debug.Log("You have no money!");
            audioSource.PlayOneShot(NotEnoughMoneySound);
        }
        else
        {
            WaveSpawner.scorePoints -= priceOfItem;
            audioSource.PlayOneShot(ItemBoughtSound);
        }
    }

    public void BuyGun_RayGun()
    {
        priceOfItem = 99999;

        if (priceOfItem > currentMoney)
        {
            //Play animation of DENIAL
            Debug.Log("You have no money!");
            audioSource.PlayOneShot(NotEnoughMoneySound);
        }
        else
        {
            WaveSpawner.scorePoints -= priceOfItem;
            audioSource.PlayOneShot(ItemBoughtSound);
        }
    }

    public void BuyPotion_Health()
    {
        priceOfItem = 5;

        if (priceOfItem > currentCoins)
        {
            //Play animation of DENIAL
            Debug.Log("You have no coins!");
            audioSource.PlayOneShot(NotEnoughMoneySound);
        }
        else
        {
            audioSource.PlayOneShot(ItemBoughtSound);
            CoinManager.numOfCoins -= priceOfItem;
            //BackPackScript.HealthPotionAmount++;
            GeneratePotion(potionH);
        }
    }

    public void BuyPotion_Shield()
    {
        priceOfItem = 5;

        if (priceOfItem > currentCoins)
        {
            //Play animation of DENIAL
            Debug.Log("You have no coins!");
            audioSource.PlayOneShot(NotEnoughMoneySound);
        }
        else
        {
            CoinManager.numOfCoins -= priceOfItem;
            //BackPackScript.ShieldPotionAmount++;
            audioSource.PlayOneShot(ItemBoughtSound);

            GeneratePotion(potionS);
        }
    }

    public void BuyPotion_Endurance()
    {
        priceOfItem = 5;

        if (priceOfItem > currentCoins)
        {
            //Play animation of DENIAL
            Debug.Log("You have no coins!");
            audioSource.PlayOneShot(NotEnoughMoneySound);

        }
        else
        {
            CoinManager.numOfCoins -= priceOfItem;
            //BackPackScript.EndurancePotionAmount++;
            audioSource.PlayOneShot(ItemBoughtSound);

            GeneratePotion(potionE);
        }
    }

    public void BuyAmmo()
    {
        priceOfItem = 50;

        if(priceOfItem > currentMoney)
        {
            //Play animation of DENIAL
            Debug.Log("You have no coins!");
            audioSource.PlayOneShot(NotEnoughMoneySound);
        }
        else
        {
            AmmoManager.CurrentAmmoCount += 30;
            audioSource.PlayOneShot(ItemBoughtSound);
        }

    }

    public void BuyExplosiveAmmo()
    {
        priceOfItem = 99;

        if (priceOfItem > currentMoney)
        {
            //Play animation of DENIAL
            Debug.Log("You have no coins!");
            audioSource.PlayOneShot(NotEnoughMoneySound);
        }
        else
        {
            //set up value for explosion rounds
            AmmoManager.CurrentExplosiveAmmoCount += 30;
            audioSource.PlayOneShot(ItemBoughtSound);
        }
    }

    void GeneratePotion(GameObject potion)
    {
        Transform spawnPoint = ItemBoughtSpawnPoint_Potions[Random.Range(0, ItemBoughtSpawnPoint_Potions.Length)];
        Instantiate(potion, spawnPoint.position, transform.rotation);
    }




}
