using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Skor : MonoBehaviour
{
    public int skor = 0;
    public TextMeshPro scoreText;
    public TextMeshProUGUI scoreUI;
    public int timer = 30;
    public int hedefSayi = 1;
    public TextMeshProUGUI timerText, winFailText;
    public GameObject endPanel;
    public Transform pota;
    public AudioSource audiosource;

    void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        if(timer > 0)
        {
            timer--;
            StartCoroutine(Timer());
            timerText.text = timer.ToString();
        }
        else if(timer == 0)
        {
            //skoru kontrol et ve oyunu bitir
            if(skor >= hedefSayi)
            {
                //kazandın
                Debug.Log("Show win text");
                winFailText.text = "Kazandin!!";
                winFailText.color = Color.green;
                Debug.Log("Attempting to load scene 1");
                SceneManager.LoadScene(1);
                Debug.Log("Scene 1 loaded");

            }
            else if(skor < hedefSayi)
            {
                //kaybettin
                winFailText.text = "Kaybettin!!";
                audiosource = GetComponent<AudioSource>();
                audiosource.Play();
                winFailText.color = Color.red;
                endPanel.SetActive(true);
                SceneManager.LoadScene(1);
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Basketball")
        {            
            if(other.GetComponent<BasketCheck>().alt == true &&
                other.GetComponent<BasketCheck>().ust == true)
            {
                skor++;
                Debug.Log(skor);
                scoreText.text = skor.ToString();
                scoreUI.text = "Amazing!!";
                scoreText.color = Color.green;
                Invoke(nameof(BakctoBlack), 1);
                pota.position = new Vector3(Random.Range(-6.0f,-4.0f),0,Random.Range(-8.0f,-7.0f));
            }
            
        }
    }

    void BakctoBlack()
    {
        scoreText.color = Color.black;
        scoreUI.text = "";
    }
}
