using UnityEngine;
using UnityEngine.UI;

public class SkinsScreen : MonoBehaviour
{
    private void OnEnable()
    {
        MenuScreen.justStarted = false;
        
        SkinsManager.gotToMenu = false;

        foreach (var obj in FindObjectsOfType<BuySkin>())
        {
            if (obj.skin.id == PlayerPrefs.GetInt("MenuOldSkinID", 1))
                obj.GetComponentInChildren<Image>().color = Color.white;

            if (PlayerPrefs.GetInt("SelectedSkinID", 1) == obj.skin.id)
            {
                SkinsManager.SelectASkin(obj.gameObject);
                obj.GetComponentInChildren<Image>().color = Color.cyan;
            }
            
            if (obj.skin.id == PlayerPrefs.GetInt("SelectedSkinID", 1))
            {
                obj.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                obj.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);    
            }

            if (PlayerPrefs.GetInt("isBought" + obj.skin.id, 0) == 0)
            {
                obj.GetComponentInChildren<Image>().color = Color.white;
            }
            else
            {
                obj.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                obj.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);   
            }

            if (PlayerPrefs.GetInt("lastBoughtID", 0) == obj.skin.id)
            {
                obj.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                obj.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);   
            }
            
            if (PlayerPrefs.GetInt("lastBoughtID", 0) != obj.skin.id && obj.skin.id != PlayerPrefs.GetInt("SelectedSkinID", 1))
            {
                obj.GetComponentInChildren<Image>().color = Color.white;
            }
        }
    }
}
