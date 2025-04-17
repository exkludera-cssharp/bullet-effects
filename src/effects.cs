using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;
using System.Drawing;

public class EffectHelper
{
    private readonly Plugin _plugin;

    public EffectHelper(Plugin plugin)
    {
        _plugin = plugin;
    }

    public void CreateTracer(Vector position, Vector endposition, string colorValues, float width, float lifetime, string sound = "")
    {
        var tracer = Utilities.CreateEntityByName<CBeam>("env_beam")!;

        Color color = Utils.ParseColor(colorValues);
        tracer.Render = color;

        tracer.Width = width;
        tracer.DispatchSpawn();

        tracer.Teleport(position);

        tracer.EndPos.X = endposition.X;
        tracer.EndPos.Y = endposition.Y;
        tracer.EndPos.Z = endposition.Z;

        Utilities.SetStateChanged(tracer, "CBeam", "m_vecEndPos");

        if (!string.IsNullOrEmpty(sound))
            tracer.EmitSound(sound);

        _plugin.AddTimer(lifetime, () =>
        {
            if (tracer != null && tracer.IsValid)
                tracer.Remove();
        });
    }

    public void CreateParticle(Vector position, string particleFile, float lifetime, string sound = "", CCSPlayerController? player = null)
    {
        var particle = Utilities.CreateEntityByName<CParticleSystem>("info_particle_system")!;

        particle.EffectName = particleFile;
        particle.StartActive = true;
        particle.Teleport(position);
        particle.DispatchSpawn();

        if (player != null)
            particle.AcceptInput("FollowEntity", player.PlayerPawn.Value, particle, "!activator");

        if (!string.IsNullOrEmpty(sound))
            particle.EmitSound(sound);

        _plugin.AddTimer(lifetime, () =>
        {
            if (particle != null && particle.IsValid)
                particle.Remove();
        });
    }
}