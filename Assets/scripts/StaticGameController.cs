using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticGameController {

    public static List<GameObject> listaTiroAviao,
        listaInimigo,
        ListaTiroInimigo;

    public static int UltimoTiroAviao = 0,
        UltimoTiroInimigo = 0;


    public static void criarTiro(GameObject aviao,GameObject prefabTiroAviao)
    {
        bool podeCriarSemRepeticao = true;
        foreach (GameObject tiro in listaTiroAviao)
        {
            if(tiro.transform.position == aviao.transform.position)
            {
                podeCriarSemRepeticao = false;
                break;
            }
        }

        if (podeCriarSemRepeticao)
        {
            UltimoTiroAviao++;
            GameObject tiroDoAviao = GameObject.Instantiate(prefabTiroAviao) as GameObject;

            tiroDoAviao.name = "tiro" + StaticGameController.UltimoTiroAviao;

            tiroDoAviao.transform.position = new Vector3(aviao.transform.position.x + 0.5f,
                aviao.transform.position.y,
                aviao.transform.position.z);

            tiroDoAviao.SetActive(true);
            ListaTiroInimigo.Add(tiroDoAviao);

        }

    }

    public static void moverTiros (float velocidade)
    {
        foreach (GameObject tiro in listaTiroAviao)
            tiro.transform.position = new Vector3(tiro.transform.position.x,
               tiro.transform.position.y + velocidade * Time.deltaTime,
               tiro.transform.position.z);

        for (int i = listaTiroAviao.Count - 1; i>=0; i--)
        {
            GameObject tiro = listaTiroAviao[i];
            if (tiro.transform.position.y > 8.5f)
            {
                listaTiroAviao[i].SetActive(false);
                tiro.SetActive(false);
                tiro.GetComponent<Renderer>().enabled = false;
                listaTiroAviao.RemoveAt(i);            }
        }
    }

    public static void removerTiros(GameObject qualTiro)
    {
        for (int i= listaTiroAviao.Count-1; i>=0; i--)
        {
            if (listaTiroAviao[i].name == qualTiro.name)
            {
                listaTiroAviao[i].SetActive(false);
                qualTiro.SetActive(false);
                qualTiro.GetComponent<Renderer>().enabled = false;
                listaTiroAviao.RemoveAt(i);
                break;
            }
        }
    }

    public static int MaxInimigos = 10;

    public static void criarTiroInimigo(GameObject inimigo,
        GameObject prefabTiroInimigo)
    {
        if(prefabTiroInimigo != null)
        {
            UltimoTiroInimigo++;
            GameObject tiroDoInimigo = GameObject.Instantiate(prefabTiroInimigo)
                as GameObject;
            tiroDoInimigo.name = "tiroInimigo" +
                StaticGameController.UltimoTiroInimigo;
            tiroDoInimigo.transform.position = new Vector3(inimigo.transform.position.x,
                inimigo.transform.position.y - 2,
                inimigo.transform.position.z
                );

            tiroDoInimigo.SetActive(true);
            ListaTiroInimigo.Add(tiroDoInimigo);
        }
    }

    public static void moverTiroInimigos(float velocidade)
    {
        foreach (GameObject tiro in ListaTiroInimigo)
        {
            tiro.transform.position = new Vector3(tiro.transform.position.x,
                tiro.transform.position.y + velocidade * Time.deltaTime,
                tiro.transform.position.z);
        }

        for (int i = ListaTiroInimigo.Count - 1; i >= 0; i--)
        {
            GameObject qualTiro = ListaTiroInimigo[i];
            if(qualTiro.transform.position.y < -4.5f)
            {
                ListaTiroInimigo[i].SetActive(false);
                qualTiro.SetActive(qualTiro);
                qualTiro.GetComponent<Renderer>().enabled = false;
                ListaTiroInimigo.RemoveAt(i);
            }
        }
    }

    public static void removerTiroInimigo(GameObject qualTiro)
    {
        for (int i = ListaTiroInimigo.Count -1; i>=0; i--)
        
            if (ListaTiroInimigo[i].name == qualTiro.name)
            {
                Debug.Log("Removendo tiro inimigo" + qualTiro.name);
                ListaTiroInimigo[i].SetActive(false);
                qualTiro.SetActive(qualTiro);
                qualTiro.GetComponent<Renderer>().enabled = false;
                ListaTiroInimigo.RemoveAt(i);
            }
        
    }
    public static void InimigoSaiDeCena(GameObject outro)
    {
        for (int i = 0; i < MaxInimigos; i++)

            if (listaInimigo[i].name == outro.name)
            {
                Debug.Log("Removendo tiro inimigo" + outro.name);
                ListaTiroInimigo[i].SetActive(false);
                outro.SetActive(outro);
                outro.GetComponent<Renderer>().enabled = false;
                listaInimigo.RemoveAt(i);
                break;
            }

    }

    public static void SpawnInimigos(float xMinimo, float xMaximo)
    {
        float posicaoX = 0;
        GameObject inimigo = null;
        for (int i= 0; i < MaxInimigos; i++)
            if(listaInimigo[i].activeSelf == false)
            {
                inimigo = StaticGameController.listaInimigo[i];
                posicaoX = Random.Range(xMinimo, xMaximo);
                inimigo.transform.position = new Vector3(posicaoX, 10f, 1f);
                inimigo.SetActive(true);
                inimigo.GetComponent<Renderer>().enabled = true;
                StaticGameController.listaInimigo[i] = inimigo;
                break;
            }
    }

    public static void criarListaInimigos(GameObject prefabInimigo, int maxInimigos)
    {
        MaxInimigos = maxInimigos;
        listaInimigo = new List<GameObject>();
        for (int i =0; i < maxInimigos; i++)
        {
            GameObject inimigo = GameObject.Instantiate(prefabInimigo) as GameObject;
            inimigo.name = "inimigo" + i;
            inimigo.SetActive(false);
            inimigo.GetComponent<Renderer>().enabled = true;
            listaInimigo.Add(inimigo);
        }
    }

    public static void desativarInimigo (GameObject outro)
    {
        for (int i=0; i < MaxInimigos; i++)
        {
            if(listaInimigo[i].name == outro.name)
            {
                Debug.Log("inimigo desativado por colisão" + outro.name);
                listaInimigo[i].SetActive(false);
                outro.gameObject.SetActive(false);
                listaInimigo[i].GetComponent<Renderer>().enabled = false;
                break;
            }
        }
    }


}