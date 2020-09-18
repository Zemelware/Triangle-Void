using UnityEngine;
using UnityEngine.UI;

public class SkinsManager : MonoBehaviour
{
    public GameObject skinsScreen;
    public static GameObject menuScreen;
    public GameObject itemsParent;
    public GameObject selectSkinContent;
    
    public static GameObject selectedSkin;
    public static int selectedSkinID;

    public static BuySkin[] skins;

    public static bool gotToMenu = false;
    
    void Awake()
    {
        menuScreen = GameObject.Find("Menu Screen");
        
        skinsScreen.SetActive(true);

        selectSkinContent = GameObject.Find("Content");
        itemsParent = GameObject.Find("Items Parent");
        skins = itemsParent.GetComponentsInChildren<BuySkin>();
        
        //Make sure the skin ID is correct in items parent, then in content (select skin UI on menu page)
        foreach (BuySkin buySkin in itemsParent.GetComponentsInChildren<BuySkin>())
        {
            buySkin.skin.id = buySkin.transform.GetSiblingIndex() + 1;
            
            // Also duplicate the skins into content
            var contentSkins = Instantiate(buySkin.gameObject, Vector3.zero, Quaternion.identity);
            contentSkins.transform.parent = selectSkinContent.transform;
        }
        foreach (BuySkin buySkin in selectSkinContent.GetComponentsInChildren<BuySkin>())
        {
            buySkin.skin.id = buySkin.transform.GetSiblingIndex() + 1;
        }

    }

    void Start()
    {
        skinsScreen.SetActive(false);
    }

    public static void SelectASkin(GameObject newSkin)
    {
        // Show that the skin is selected visually
        if (selectedSkin != null)
        {
            selectedSkin.GetComponentInChildren<Image>().color = Color.white;
            
            if (!gotToMenu)
                PlayerPrefs.SetInt("MenuOldSkinID", selectedSkin.GetComponent<BuySkin>().skin.id);
            
            if (menuScreen.activeInHierarchy)
            {
                foreach (var obj in FindObjectsOfType<BuySkin>())
                {
                    if (obj.skin.id == PlayerPrefs.GetInt("MenuOldSkinID", 1))
                        obj.GetComponentInChildren<Image>().color = Color.white;
                    gotToMenu = true;
                }
            }
        }

        selectedSkinID = newSkin.GetComponent<BuySkin>().skin.id;
        selectedSkin = newSkin;

        foreach (var obj in FindObjectsOfType<BuySkin>())
        {
            if (obj.skin.id == selectedSkinID)
            {
                obj.GetComponentInChildren<Image>().color = Color.cyan;
            }
        }
    }
}
