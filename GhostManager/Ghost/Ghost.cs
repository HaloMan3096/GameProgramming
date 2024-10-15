using System;

namespace GhostManager.Ghost;

public enum GhostState { Chasing, Evading, Roving, Dead }
public class Ghost
{
    private GhostState _state;

    public GhostState State
    {
        get => _state;
        set
        {
            if (_state != value)
            {
                this.Log(string.Format("{0} was: {1} now {2}", this.ToString(), _state, value));
                _state = value;
            }
        }
    }

    public void Chase()
    {
        this.State = GhostState.Chasing;
    }

    public void Evade()
    {
        this.State = GhostState.Evading;
    }

    public void Rove()
    {
        this.State = GhostState.Roving;
    }

    public void Dead()
    {
        this.State = GhostState.Dead;
    }

    protected virtual void Log(string s)
    {
        Console.WriteLine(s);
    }
}