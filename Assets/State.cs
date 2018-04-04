using SFB;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State : MonoBehaviour {
    //button refs
    public Button btnLoadFile;
    public Button btnPlayPause;
    public Button btnUnload;
    public Button btnSlow;
    public Button btnNormal;
    public Button btnFast;
    public Button btnReturnCamera;
    //transform references
    public Transform apidTransform;
    public Transform vespidTransform;
    //file variables
    private FileLoader fl;
    private string filepath;
    //simulation control variables
    private List<Transform> agents; //stores all current active agents
    private bool simulationLoaded;
    private bool simulationPaused;
    private int currentSpeed;
    private Color32 colorActiveButton;
    private Vector3 initCameraPos;
    public float initCameraSize;

	void Start () {
        btnLoadFile.onClick.AddListener(LaunchExplorer);
        btnUnload.onClick.AddListener(UnloadSimulation);
        btnPlayPause.onClick.AddListener(PlayPause);
        btnSlow.onClick.AddListener(delegate { SetSpeed(1);});
        btnNormal.onClick.AddListener(delegate { SetSpeed(2);});
        btnFast.onClick.AddListener(delegate { SetSpeed(3); });
        btnReturnCamera.onClick.AddListener(ReturnCamera);
        fl = new FileLoader();
        agents = new List<Transform>();
        simulationLoaded = false;
        currentSpeed = 2;
        colorActiveButton = new Color32(0xC6, 0xC1, 0xEE, 0xFF);
        initCameraPos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        initCameraSize = Camera.main.orthographicSize;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LaunchExplorer()
    {
        Cursor.visible = true;
        Debug.Log("opening explorer...");
        String[] filePanelOutput = StandaloneFileBrowser.OpenFilePanel("Select a .SWARM file", "", "SWARM", false);

        if(filePanelOutput.Length > 0)
        {
            filepath = filePanelOutput[0];
        }
        else
        {
            return;
        }

        if (this.simulationLoaded)
        {
            UnloadSimulation();
        }

        Debug.Log("file: " + filepath);
        fl.loadPositions(filepath);

        List<List<Coordinate>> coords = fl.getPositions();
        List <Coordinate> vespidCoords = fl.getVespidPositions();
        List<List<bool>> aliveList = fl.getAliveList();

        Debug.Log("loaded " + coords.Count + " coordinate sequences");
        CreateAgents(coords, vespidCoords, aliveList);
    }

    public void CreateAgents(List<List<Coordinate>> coordinates, List<Coordinate> vespidCoords, List<List<bool>> aliveList)
    {
        for(int i = 0; i < coordinates.Count; i++)
        {
            List<Coordinate> coordinateList = coordinates[i];
            List<bool> alive = aliveList[i];
            Transform apidInstance = Instantiate(apidTransform);
            Agent apidScript = (Agent)apidInstance.GetComponent("Agent");
            agents.Add(apidInstance);
            apidScript.SetLocations(coordinateList);
            apidScript.SetAliveList(alive);
            apidScript.SetAgentType(AgentType.APID);
        }


        Transform vespidInstance = Instantiate(vespidTransform);
        Agent vespidScript = (Agent)vespidInstance.GetComponent("Agent");
        agents.Add(vespidInstance);
        vespidScript.SetLocations(vespidCoords);
        vespidScript.SetAliveList(aliveList[aliveList.Count - 1]);
        vespidScript.SetAgentType(AgentType.VESPID);
        this.simulationLoaded = true;
    }

    public void UnloadSimulation()
    {
        if(agents.Count > 0)
        {
            foreach(Transform agent in agents)
            {
                Destroy(agent.gameObject);
            }
            agents = new List<Transform>();
            this.simulationLoaded = false;
        }
    }

    public void PlayPause()
    {
        if (this.simulationPaused)
        {
            this.simulationPaused = false;
            SetSpeed(currentSpeed);
            btnPlayPause.GetComponentInChildren<Text>().text = "PAUSE";
            btnPlayPause.GetComponent<Image>().color = Color.white;
        }
        else
        {
            SetSpeed(0);
            btnPlayPause.GetComponentInChildren<Text>().text = "RESUME";
            btnPlayPause.GetComponent<Image>().color = colorActiveButton;
            this.simulationPaused = true;

        }
    }

    public void SetSpeed(int speed)
    {
        switch (speed)
        {
            case 0:
                Time.timeScale = 0f;
                break;
            case 1:
                Time.timeScale = 0.25f;
                currentSpeed = 1;
                break;
            case 2:
                Time.timeScale = 1f;
                currentSpeed = 2;
                break;
            case 3:
                Time.timeScale = 1.75f;
                currentSpeed = 3;
                break;
        }

        if(speed > 0)
        {
            simulationPaused = false;
            btnPlayPause.GetComponentInChildren<Text>().text = "PAUSE";
            btnPlayPause.GetComponent<Image>().color = Color.white;
        }
    }

    public void ReturnCamera()
    {
        Camera.main.transform.position = initCameraPos;
        Camera.main.orthographicSize = initCameraSize;
    }
}
