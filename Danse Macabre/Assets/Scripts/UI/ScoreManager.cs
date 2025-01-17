using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    #region parameters
    public float perfectTimingValue = 10;
    private float greatTimingValue = 5;
    private float goodTimingValue = 1;

    private bool _gameStart = false;
    #endregion 

    #region references
    [SerializeField] private TextMeshProUGUI _textPuntos;
    [SerializeField] private TextMeshPro _textPuntosAñadidos;
    [SerializeField] private TextMeshProUGUI _textoArriba;
    private ComboManager _combo;
    #endregion

    #region properties
    //para el slider del combo
    public double _totalPoint = 0f;
    public double _basicPoint = 0f;
    private float _coinPoint = 0;
    private float _enemyPoint = 0;
    private float _destroyObjectPoint = 0;

    private float _addPoints = 0; //los puntos que se van sumando
    private float _sudPoints= 0;
    private float _lastPickupTime; 
    [SerializeField] private float _resetTime = 0.3f;

    //guardar el timing que hizo el jugador
    private int _nPerfect;
    private int _nGreat;
    private int _nGood;
    private int _nMiss;
    private int _nWrong;
    #endregion

    #region methods
    public void GameStart(bool start)
    {
        _gameStart = start;
    }
    public void AddPoints(float points, int type)
    {
        //Añade puntos segun su tipo y lo escribe en pantalla
        //tipo de punto, 0=monedas, 1=enemigo, 2=objeto
        if (type==0) _coinPoint += (points * _combo.multiplier);
        else if (type==1) _enemyPoint += (points * _combo.multiplier);
        else if(type==2) _destroyObjectPoint += (points * _combo.multiplier);

        if (points < 0) // si es negativo
        {
            _addPoints = 0;
            _sudPoints += points;
            _textPuntosAñadidos.text = _sudPoints.ToString();
            _combo.resetCombo();
        }
        else
        {
            _addPoints += (points * _combo.multiplier);
            _textPuntosAñadidos.text = "+" + _addPoints.ToString(); //poner +numero
            _combo.addCombo(points);
        }
        _lastPickupTime = Time.time;
    }
    private void ResetText() //texto qie al lado del player
    {
        _textPuntosAñadidos.text = " "; //quitar el texto
        AddToTotalPoint(); //sumar la puntuacion al score
    }
    private void ResetUpText()
    {
        _textoArriba.text = "   ";
    }
    void AddToTotalPoint()
    {
        //sumar los puntos obtenidos a la puntuacion total
        if (_addPoints > 0) //puntos positivos
        {       
            _totalPoint += _addPoints;
            _textoArriba.text ="+"+ _addPoints.ToString();
        }
        else //puntos negativos
        {
            _totalPoint += _sudPoints;
            _textoArriba.text = _sudPoints.ToString();
        }
        Invoke("ResetUpText", _resetTime);
    }
    public void AddTimingPoints(string timing)
    {
        //suma puntos segun el timing
        if (timing == "PERFECT") {

            _nPerfect++;
            _totalPoint += (perfectTimingValue * _combo.multiplier);
            _combo.addCombo(perfectTimingValue);
        }
        else if (timing == "GREAT") {

            _nGreat++;
            _totalPoint += (greatTimingValue * _combo.multiplier);
            _combo.addCombo(greatTimingValue);
        }
        else if (timing == "GOOD") {
            _nGood++;
            _totalPoint += (goodTimingValue * _combo.multiplier);
            _combo.addCombo(goodTimingValue);
        }
        else if (timing == "WRONG")
        {
            _nWrong++;
            _combo.resetCombo();
        }
        else if (timing == "MISSED")
        {
            _nMiss++;
            _combo.resetCombo();
        }
    }

    public void SaveCheckpointScore()
    {
        PlayerPrefs.SetFloat("CheckpointScore", (float)_totalPoint);
        PlayerPrefs.SetFloat("CheckpointCoinScore", (float)_coinPoint);
        PlayerPrefs.SetFloat("CheckpointEnemyScore", (float)_enemyPoint);
        PlayerPrefs.SetFloat("CheckpointObjectScore", (float)_destroyObjectPoint);
        PlayerPrefs.SetFloat("CheckpointBasicScore", (float)_basicPoint);

        //el timing
        PlayerPrefs.SetInt("PerfectNumber", (int)_nPerfect);
        PlayerPrefs.SetInt("GreatNumber", (int)_nGreat);
        PlayerPrefs.SetInt("GoodNumber", (int)_nGood);
        PlayerPrefs.SetInt("MissNumber", (int)_nMiss);
        PlayerPrefs.SetInt("WrongNumber", (int)_nWrong);
    }

    public void LoadCheckpointScore()
    {
        _totalPoint = PlayerPrefs.GetFloat("CheckpointScore");
        _coinPoint = PlayerPrefs.GetFloat("CheckpointCoinScore");
        _enemyPoint = PlayerPrefs.GetFloat("CheckpointEnemyScore");
        _destroyObjectPoint = PlayerPrefs.GetFloat("CheckpointObjectScore");
        _basicPoint = PlayerPrefs.GetFloat("CheckpointBasicScore");

        //timing
        _nPerfect = PlayerPrefs.GetInt("PerfectNumber");
        _nGreat = PlayerPrefs.GetInt("GreatNumber");
        _nGood = PlayerPrefs.GetInt("GoodNumber");
        _nMiss = PlayerPrefs.GetInt("MissNumber");
        _nWrong = PlayerPrefs.GetInt("WrongNumber");
    }

    public void SaveFinalScore() 
    {
        // Guarda la puntuaci�n en PlayerPrefs antes de cambiar de escena
        PlayerPrefs.SetFloat("FinalScore", (float)_totalPoint);
        PlayerPrefs.SetFloat("CoinScore", (float)_coinPoint);
        PlayerPrefs.SetFloat("EnemyScore", (float)_enemyPoint);
        PlayerPrefs.SetFloat("ObjectScore", (float)_destroyObjectPoint);

        //Timing
        PlayerPrefs.SetInt("PerfectNumber", (int)_nPerfect);
        PlayerPrefs.SetInt("GreatNumber", (int)_nGreat);
        PlayerPrefs.SetInt("GoodNumber", (int)_nGood);
        PlayerPrefs.SetInt("MissNumber", (int)_nMiss);
        PlayerPrefs.SetInt("WrongNumber", (int)_nWrong);
    }
    #endregion
    void Start()
    {
        _lastPickupTime = Time.time;
        _textoArriba.enabled = false;
        _combo = FindObjectOfType<ComboManager>();
    }
    void Update()
    {
        if (_gameStart) //empieza a contar los puntos cuando inicie la musica
        {
            _basicPoint += Time.deltaTime;
            _totalPoint += Time.deltaTime;

        }

        //Debug.Log("Puntos" + _totalPoint);
        _textPuntos.text = _totalPoint.ToString("0");

        // cuando lleva un tiempo sin coger objeto se resetea
        if (Time.time - _lastPickupTime >= _resetTime) 
        {
            _textoArriba.enabled = true;
            if(_addPoints>0||_sudPoints<0)ResetText();
            _addPoints = 0;
            _sudPoints = 0;
        }
    }
}
