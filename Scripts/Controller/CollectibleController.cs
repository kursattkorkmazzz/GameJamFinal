using UnityEngine;
using TMPro;
public class CollectibleController : MonoBehaviour
{

    #region Singleton
    public static CollectibleController Instance
    {
        get
        {
            if (Instance == null)
            {
                Instance = new CollectibleController();
            }
            return Instance;
        }
        private set { }


    }

    private CollectibleController() { }

    #endregion


    public delegate void onCollected(GameObject collected);
    public event onCollected OnCollected;

    public TextMeshProUGUI TextUIObject;
    private Vector2 position;
    private Vector2 size;

    private bool isPickupPressing;



    private void Start()
    {
        TextUIObject.gameObject.SetActive(false);
        isPickupPressing = false;
    }

    private void Update()
    {
        CheckCollectibles();

        isPickupPressing = Input.GetKey(KeyCode.P);
       
    }

    public void CheckCollectibles()
    {

        position = new(transform.position.x, transform.position.y - 0.01f);
        size = new(1.8f * transform.localScale.x, 1);

        Collider2D[] cols = Physics2D.OverlapBoxAll(position, size, 0);
        bool isThereAnyCollectible = false;

        foreach (Collider2D col in cols)
        {
            if (col.TryGetComponent<ICollectible>(out ICollectible collectible))
            {
            
                Show(collectible.GetObject().name);
                isThereAnyCollectible = true;
                if (isPickupPressing) PickUp(col.gameObject);
            }
        }


        if (!isThereAnyCollectible) Hide();
    }






    public void PickUp(GameObject item)
    {


        if (item.TryGetComponent<BaseWeapon>(out BaseWeapon weapon))
        {
            
            if (gameObject.TryGetComponent<WeaponManager>(out WeaponManager wm))
            {
           
                wm.TakeWeapon(weapon.WeaponData);
                OnCollected?.Invoke(item);
                Destroy(item);
            }

        }else if (item.TryGetComponent<BaseBoost<InstantHealthBoostSO>>(out BaseBoost<InstantHealthBoostSO> baseBoost))
        {
            if (gameObject.TryGetComponent<BoostManager>(out BoostManager bm))
            {
                
                bm.AddBoost(baseBoost.BoostData);
                OnCollected?.Invoke(item);
                Destroy(item);
            }

        }


         // TODO Pickup operations.

    }


 
    
    private void OnDrawGizmos()
    {

        Gizmos.DrawCube(position, size); 
    }

    #region UI Methods
    private void Show(string ItemName)
    {
        TextUIObject.text = "Press P to pick up \"" + ItemName + "\"";

        TextUIObject.gameObject.SetActive(true);
    }


    private void Hide()
    {
        TextUIObject.gameObject.SetActive(false);
    }

 
    #endregion





}
