using System;

namespace GhostManager.PacMan;

public enum PacState
{
    Spawning,
    Chomping,
    Still,
    SuperPac,
    Dying,
    Dead
}
public class PacMan
{
    private PacState _state;

    public PacState State
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

    public PacMan()
    {
        _state = PacState.Spawning;
    }

    public void Spawn()
    {
        State = PacState.Spawning;
    }
    
    public void Chomp()
    {
        State = PacState.Chomping;
    }

    public void PowerUp()
    {
        State = PacState.SuperPac;
    }

    public void Hit()
    {
        State = PacState.Dying;
    }

    public void Dead()
    {
        State = PacState.Dead;
    }
    
    public virtual void Log(string s)
    {
        Console.WriteLine(s);
    }
}