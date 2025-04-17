using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;

public partial class Plugin : BasePlugin, IPluginConfig<Config>
{
    public override string ModuleName => "Bullet Effects";
    public override string ModuleVersion => "1.0.3";
    public override string ModuleAuthor => "exkludera";

    private EffectHelper? _effectHelper;

    public override void Load(bool hotReload)
    {
        _effectHelper = new EffectHelper(this);

        RegisterListener<Listeners.OnServerPrecacheResources>(OnServerPrecacheResources);

        RegisterEventHandler<EventBulletImpact>(EventBulletImpact);
        RegisterEventHandler<EventPlayerDeath>(EventPlayerDeath);
        RegisterEventHandler<EventPlayerHurt>(EventPlayerHurt);
    }
    public override void Unload(bool hotReload)
    {
        RemoveListener<Listeners.OnServerPrecacheResources>(OnServerPrecacheResources);

        DeregisterEventHandler<EventBulletImpact>(EventBulletImpact);
        DeregisterEventHandler<EventPlayerDeath>(EventPlayerDeath);
        DeregisterEventHandler<EventPlayerHurt>(EventPlayerHurt);
    }

    public Config Config { get; set; } = new Config();
    public void OnConfigParsed(Config config) => Config = config;

    public void OnServerPrecacheResources(ResourceManifest manifest)
    {
        List<string> resources =
        [
            Config.Impact.Particle,
            Config.HitEffect.Particle,
            Config.KillEffect.Particle,
        ];

        foreach (string resource in resources)
        {
            if (!string.IsNullOrEmpty(resource))
                manifest.AddResource(resource);
        }
    }

    HookResult EventBulletImpact(EventBulletImpact @event, GameEventInfo info)
    {
        var player = @event.Userid;

        if (player == null)
            return HookResult.Continue;

        Vector StartPos = Utils.GetEyePosition(player);
        Vector EndPos = new Vector(@event.X, @event.Y, @event.Z);

        if (Config.Tracer.Enable)
        {
            List<string> permission = Config.Tracer.Permission;
            string team = Config.Tracer.Team;
            string color = Config.Tracer.Color;
            float width = Config.Tracer.Width;
            float lifetime = Config.Tracer.Lifetime;
            string sound = Config.Tracer.Sound;

            if (Utils.HasPermission(player, permission, team))
                _effectHelper?.CreateTracer(StartPos, EndPos, color, width, lifetime, sound);
        }

        if (Config.Impact.Enable)
        {
            List<string> permission = Config.Impact.Permission;
            string team = Config.Impact.Team;
            string particle = Config.Impact.Particle;
            float lifetime = Config.Impact.Lifetime;
            string sound = Config.Impact.Sound;

            if (Utils.HasPermission(player, permission, team))
                _effectHelper?.CreateParticle(EndPos, particle, lifetime, sound);
        }

        return HookResult.Continue;
    }

    HookResult EventPlayerHurt(EventPlayerHurt @event, GameEventInfo info)
    {
        if (!Config.HitEffect.Enable)
            return HookResult.Continue;

        CCSPlayerController? victim = @event.Userid;
        if (victim == null) return HookResult.Continue;

        Vector? VictimPos = victim.PlayerPawn.Value?.AbsOrigin;
        if (VictimPos == null) return HookResult.Continue;

        CCSPlayerController? attacker = @event.Attacker;
        if (attacker == null) return HookResult.Continue;

        List<string> permission = Config.HitEffect.Permission;
        string team = Config.HitEffect.Team;
        string particle = Config.HitEffect.Particle;
        float lifetime = Config.HitEffect.Lifetime;
        string sound = Config.HitEffect.Sound;

        if (Utils.HasPermission(attacker, permission, team))
            _effectHelper?.CreateParticle(VictimPos, particle, lifetime, sound, victim);

        return HookResult.Continue;
    }

    HookResult EventPlayerDeath(EventPlayerDeath @event, GameEventInfo info)
    {
        CCSPlayerController? victim = @event.Userid;
        if (victim == null) return HookResult.Continue;

        Vector? VictimPos = victim.PlayerPawn.Value?.AbsOrigin;
        if (VictimPos == null) return HookResult.Continue;

        CCSPlayerController? attacker = @event.Attacker;
        if (attacker == null) return HookResult.Continue;

        Vector? AttackerPos = attacker.PlayerPawn.Value?.AbsOrigin;
        if (AttackerPos == null) return HookResult.Continue;

        if (Config.KillEffect.Enable)
        {
            List<string> permission = Config.KillEffect.Permission;
            string team = Config.KillEffect.Team;
            string particle = Config.KillEffect.Particle;
            float lifetime = Config.KillEffect.Lifetime;
            string sound = Config.KillEffect.Sound;

            if (Utils.HasPermission(attacker, permission, team))
                _effectHelper?.CreateParticle(VictimPos, particle, lifetime, sound, victim);
        }

        if (Config.KillerEffect.Enable)
        {
            List<string> permission = Config.KillerEffect.Permission;
            string team = Config.KillerEffect.Team;
            string particle = Config.KillerEffect.Particle;
            float lifetime = Config.KillerEffect.Lifetime;
            string sound = Config.KillerEffect.Sound;

            if (Utils.HasPermission(attacker, permission, team))
                _effectHelper?.CreateParticle(AttackerPos, particle, lifetime, sound, attacker);
        }

        return HookResult.Continue;
    }
}