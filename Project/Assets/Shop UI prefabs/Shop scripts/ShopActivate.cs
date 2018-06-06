using UnityEngine;
using System.Collections;

public class ShopActivate : MonoBehaviour {
    public GameObject shopUI;

	[SerializeField]
	private Shop _Shop = null;

    bool enterShop = false;

    // Use this for initialization
    void Start() {
        shopUI.SetActive(false);
    }

    // Update is called once per frame

        private void OnTriggerEnter(Collider other)
    {
        enterShop = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            return;
        }
      if (enterShop == true && InputManager.Instance.inputType == InputManager.InputType.Controller
                && Input.GetAxis("Triggers" + other.GetComponent<MoveController>().currPlayer.ToString()) == -1.0F)
        {
            enterShop = false;
			_Shop.OpenShopUI ();
        }

    }

}
