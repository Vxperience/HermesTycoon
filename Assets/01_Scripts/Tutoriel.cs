using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tutoriel : MonoBehaviour
{
    public GameObject[] niveau;
    public Sprite[] sprite;
    public bool reset;
    private GameObject select;
    private bool validStep;
    private bool finished;
    private bool step1;
    private bool step2;
    private bool step3;
    private bool step4;
    private bool step5;
    private bool step6;

    void Start()
    {
        // initialised the scene
        reset = false;
        validStep = false;
        finished = PlayerPrefs.GetInt("tuto") != 0 ? true : false;
        if (!finished) {
            step1 = false;
            step2 = false;
            step3 = false;
            step4 = false;
            step5 = false;
            step6 = false;
        } else {
            step1 = true;
            step2 = true;
            step3 = true;
            step4 = true;
            step5 = true;
            step6 = true;
        }
    }
    
    void Update()
    {
        if (niveau[0].activeSelf || niveau[1].activeSelf || niveau[2].activeSelf) {
            // initialised the scene and restart it if it has to be done
            select = GameObject.Find("select");
            if (reset && !finished)
                ResetTuto();

            // valid the player selection step
            if (!step1 && !validStep) {
                Time.timeScale = 0;
                if (select.GetComponent<Text>().text.Contains("Personnage") && !validStep) {
                    Time.timeScale = 1;
                    step1 = true;
                    validStep = true;
                    StartCoroutine(NextStep(1));
                }
            }

            // valid the player move correct step
            if (step1 && !step2 && !validStep) {
                Time.timeScale = 0;
                if (Input.GetMouseButtonDown(0) && select.GetComponent<Text>().text.Contains("Personnage")) {
                    RaycastHit hit;
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100) && hit.transform.gameObject.name == "Plane" && !validStep) {
                        Time.timeScale = 1;
                        validStep = true;
                        step2 = true;
                        StartCoroutine(NextStep(1));
                    }
                }
            }

            // valid the player move finished step
            if (step1 && step2 && !step3 && !validStep) {
                if (select.GetComponent<Text>().text.Contains("Personnage")) {
                    if (!GameObject.Find(select.GetComponent<Text>().text).GetComponent<Personnage>().isInAction) {
                        validStep = true;
                        step3 = true;
                        StartCoroutine(NextStep(1));
                    }
                }
            }

            // valid the pick element step
            if (step1 && step2 && step3 && !step4 && !validStep) {
                if (select.GetComponent<Text>().text.Contains("Personnage")) {
                    if (GameObject.Find(select.GetComponent<Text>().text).GetComponent<Personnage>().item != "") {
                        validStep = true;
                        step4 = true;
                        StartCoroutine(NextStep(1));
                    }
                }
            }

            // valid the drop of item step
            if (step1 && step2 && step3 && step4 && !step5 && !validStep) {
                if (select.GetComponent<Text>().text.Contains("Personnage")) {
                    if (GameObject.Find(select.GetComponent<Text>().text).GetComponent<Personnage>().item == "") {
                        validStep = true;
                        step5 = true;
                        StartCoroutine(NextStep(1));
                    }
                }
            }

            // valid the box explication step
            if (step1 && step2 && step3 && step4 && step5 && !step6 && !validStep) {
                validStep = true;
                step6 = true;
                StartCoroutine(NextStep(10));
            }

            // manage if the tutorial is finished
            if (step1 && step2 && step3 && step4 && step5 && step6 && !validStep && !finished) {
                PlayerPrefs.SetInt("tuto", 1);
                finished = true;
            }
        }
    }

    IEnumerator NextStep(float time)
    {
        yield return new WaitForSeconds(time);
        validStep = false;
    }

    private void ResetTuto()
    {
        reset = false;
        validStep = false;
        finished = false;
        step1 = false;
        step2 = false;
        step3 = false;
        step4 = false;
        step5 = false;
        step6 = false;
    }

    // manage the different UI element for the tutorial
    private void OnGUI()
    {
        GUI.skin.box.fontSize = 18;

        if (niveau[0].activeSelf || niveau[1].activeSelf || niveau[2].activeSelf) {
            // select a player
            if (!step1 && Time.timeScale == 0)
                GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height / 2 + 140, 300, 120), "Pour commencer le tutoriel veuillez\nselectionner un personnage.\n\nTips: pour selectionner un \npersonnage cliquer dessus");

            // move a player
            if (step1 && !step2 && Time.timeScale == 0)
                GUI.Box(new Rect(Screen.width / 2 - 175, Screen.height / 2 + 120, 350, 160), "Parfait ! Une fois selectionné vous pouvez\n donné divers ordre à vos personnages.\nEssayez de le faire se déplacer.\n\nTips: Une fois selectionné appuyer sur\n un espace libre pour qu'il se deplace\n jusqu'a cette nouvelle destination");

            // pick item
            if (step1 && step2 && step3 && !step4 && !validStep) {
                GUI.Box(new Rect(Screen.width / 2 - 170, Screen.height / 2 + 120, 340, 160), "Bien, maintenant voyons comment\nrécupérer des éléments\n\nTips: avec un personnage selectionner\nclicker sur un des objets suivant");
                for (int i = 0; i < 5; i++)
                    GUI.DrawTexture(new Rect(Screen.width / 2 - (5 / 2 * 35) + i * 35 - 15, Screen.height / 2 + 235, 30, 30), sprite[i].texture);
            }

            // drop item in trash
            if (step1 && step2 && step3 && step4 && !step5 && !validStep) {
                GUI.Box(new Rect(Screen.width / 2 - 210, Screen.height / 2 + 60, 410, 240), "Gardé en tête qu'un personnage ne peut\ntransporter qu'un seul élement. Vous\npouvez déposer des élement dans deux endroits\ndifférent: les cartons et les poubelles.\nVoyons d'abord les poubelles, déposez votre\nélément dans la poubelle\n\nTips: selectionner un personnage possédant un\nitem et cliquer sur la poubelle");
                GUI.DrawTexture(new Rect(Screen.width / 2 - 15, Screen.height / 2 + 260, 30, 30), sprite[5].texture);
            }

            // box explanation
            if (step1 && step2 && step3 && step4 && step5 && step6 && validStep) {
                GUI.Box(new Rect(Screen.width / 2 - 210, Screen.height / 2 + 40, 420, 260), "Passons maintenant au cartons, Des cartons se\ncréeront avec un chargement aléatoire en fonction\ndes éléments disponible dans le niveau. Pour\nintéragir avec eux la méthode est la même que\npour la poubelle. Il y a 4 aspect de carton différent.\n\n\n\n\n\n\nexcepté          contenu");
                for (int i = 0; i < 4; i++)
                    GUI.DrawTexture(new Rect(Screen.width / 2 - (4 / 2 * 35) + i * 35, Screen.height / 2 + 180, 30, 30), sprite[i + 6].texture);
                GUI.color = new Color(1f, 1f, 1f, 0.5f);
                GUI.DrawTexture(new Rect(Screen.width / 2 - 75, Screen.height / 2 + 230, 30, 30), sprite[0].texture);
                GUI.color = new Color(1f, 1f, 1f, 1f);
                GUI.DrawTexture(new Rect(Screen.width / 2 + 40, Screen.height / 2 + 230, 30, 30), sprite[0].texture);
            }
        }
    }
}
