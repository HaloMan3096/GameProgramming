using MonoGameLibrary.Util;

namespace GhostManager.PacMan;

public class ConsolePac : PacMan
{
    readonly GameConsole console;
    public ConsolePac()
    {
        console = null;
    }

    public ConsolePac(GameConsole console)
    {
        this.console = console;
    }
    
    public override void Log(string s)
    {
        if (console != null)
        {
            console.GameConsoleWrite(s);
        }
        else
        {
            base.Log(s);
        }
    }
}