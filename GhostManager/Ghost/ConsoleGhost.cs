using MonoGameLibrary.Util;

namespace GhostManager.Ghost;

public class ConsoleGhost : Ghost
{
    readonly GameConsole console;
    public ConsoleGhost()
    {
        console = null;
    }

    public ConsoleGhost(GameConsole console)
    {
        this.console = console;
    }

    protected override void Log(string s)
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