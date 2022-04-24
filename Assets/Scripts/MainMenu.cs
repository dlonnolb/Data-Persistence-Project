using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    
    public AudioClip sonidoBoton;
    public AudioClip sonidoBotonClick;
    private AudioSource sonidos;
    public Animator animacionPlayer;

    public GameObject inputField;
    public GameObject mensaje;    
    private string playerActual;
    public Text winName;    
    public Text bestScore;

    void Start()
    {
        mensaje.SetActive(false);
        sonidos = GetComponent<AudioSource>();        
    }
    
    void Update()
    {
        if (playerActual != "")
            mensaje.SetActive(false);
        playerActual = inputField.GetComponent<InputField>().text;
        MenuManager.Instance.actualName = playerActual;

        winName.text = MenuManager.Instance.bestName;
        bestScore.text = MenuManager.Instance.bestScore.ToString();
    }
    public void SonidoBoton1() => sonidos.PlayOneShot(sonidoBoton);
    public void SonidoBotonClick() => sonidos.PlayOneShot(sonidoBotonClick);
    public void CargaJuego() 
    {
        if (playerActual != "")
        {
            animacionPlayer.Play("TransicionAescena");
            StartCoroutine(IniciarEscena());
        }
        else { mensaje.SetActive(true); }
    }
    public void ExitGame()
    {
        animacionPlayer.Play("TransicionAescena");
        MenuManager.Instance.SaveDatos();
        StartCoroutine(SalirMenu());
    }
    private IEnumerator IniciarEscena()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }
    private IEnumerator SalirMenu()
    {
        yield return new WaitForSeconds(1.5f);
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    } 
}

