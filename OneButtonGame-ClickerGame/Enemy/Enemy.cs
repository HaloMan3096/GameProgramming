using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;

namespace MonoGame;

public enum EnemyState
{
    Idle,
    Blocking,
    Attacking,
    Healing
}

public class Enemy
{
    private EnemyState _state;
    public EnemyState State
    {
        get { return _state; }
        set
        {
            if (_state != value)
            {
                Console.WriteLine(string.Format("{0}'s state was: {1} now is: {2}", this.ToString(), State, value));
                _state = value;
            }
        } 
    }

    public Enemy()
    {
        State = EnemyState.Idle;
    }

    public void Idle()
    {
        State = EnemyState.Idle;
    }

    public void Blocking()
    {
        State = EnemyState.Blocking;
    }

    public void Attacking()
    {
        State = EnemyState.Attacking;
    }

    public void Healing()
    {
        State = EnemyState.Healing;
    }
    
    public virtual void Log(string s)
    {
        Console.WriteLine(s);
    }
}