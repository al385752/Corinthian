using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCharacterSheet
{
    private int intimidacion = 10;
    private int persuasion = 10;
    private int perspicacia = 10;
    private int mentira = 10;

    public playerCharacterSheet(int intimidacion, int persuasion, int perspicacia, int mentira)
    {
        _intimidacion = (intimidacion - 10) / 2;
        _persuasion = (persuasion - 10) / 2;
        _perspicacia = (perspicacia - 10) / 2;
        _mentira = (mentira - 10) / 2;
    }

    public int _intimidacion {get{return intimidacion;} set {intimidacion = value;}}
    public int _persuasion {get{return persuasion;} set {persuasion = value;}}
    public int _perspicacia {get{return perspicacia;} set {perspicacia = value;}}
    public int _mentira {get{return mentira;} set {mentira = value;}}

    public int intimidacionCheck()
    {
        int dado = Random.Range(1, 21);
        return dado + _intimidacion;
    }

    public int persuasionCheck()
    {
        int dado = Random.Range(1, 21);
        return dado + _persuasion;
    }

    public int perspicaciaCheck()
    {
        int dado = Random.Range(1, 21);
        return dado + _perspicacia;
    }

    public int mentiraCheck()
    {
        int dado = Random.Range(1, 21);
        return dado + _mentira;
    }
}