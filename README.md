<div align="center">
  <img width="50" height="50" alt="cssharp" src="https://github.com/user-attachments/assets/3393573f-29be-46e1-bc30-fafaec573456" />
	<h3><strong>Bullet Effects</strong></h3>
	<h4>a plugin that creates effects on shooting, hitting & killing players</h4>
	<h2>
		<img src="https://img.shields.io/github/downloads/exkludera-cssharp/bullet-effects/total" alt="Downloads">
		<img src="https://img.shields.io/github/stars/exkludera-cssharp/bullet-effects?style=flat&logo=github" alt="Stars">
		<img src="https://img.shields.io/github/forks/exkludera-cssharp/bullet-effects?style=flat&logo=github" alt="Forks">
		<img src="https://img.shields.io/github/license/exkludera-cssharp/bullet-effects" alt="License">
	</h2>
	<!--<a href="https://discord.gg" target="_blank"><img src="https://img.shields.io/badge/Discord%20Server-7289da?style=for-the-badge&logo=discord&logoColor=white" /></a> <br>-->
	<a href="https://ko-fi.com/exkludera" target="_blank"><img src="https://img.shields.io/badge/KoFi-af00bf?style=for-the-badge&logo=kofi&logoColor=white" alt="Buy Me a Coffee at ko-fi.com" /></a>
	<a href="https://paypal.com/donate/?hosted_button_id=6AWPNVF5TLUC8" target="_blank"><img src="https://img.shields.io/badge/PayPal-0095ff?style=for-the-badge&logo=paypal&logoColor=white" alt="PayPal"  /></a>
	<a href="https://github.com/sponsors/exkludera" target="_blank"><img src="https://img.shields.io/badge/Sponsor-696969?style=for-the-badge&logo=github&logoColor=white" alt="GitHub Sponsor" /></a>
</div>

### Requirements
- [MetaMod](https://github.com/alliedmodders/metamod-source)
- [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp)

## Showcase
<details>
	<summary>content</summary>
	<img src="https://github.com/user-attachments/assets/5f9d1be2-a02c-4b7a-b133-37bca2c1fb4b" width="400px"> <br>
	<img src="https://github.com/user-attachments/assets/6348e94b-3da2-49b9-bacf-c78a1d38c257" width="300px"> <br>
	<img src="https://github.com/user-attachments/assets/a168fa3a-410b-49ce-a36a-82ef43c1aead" width="300px"> <br>
	<img src="https://github.com/user-attachments/assets/ccc52017-fcea-4742-935b-64696e523e3c" width="300px"> <br>
</details>

## Config

<details>
<summary>BulletEffects.json</summary>
**Enable** - Default: `false` (option to disable/enable per effect) <br>
**Permission** - Default: `[""]` (empty for no check, flags or groups can be used) <br>
**Team** - Default: `""` (T for Terrorist, CT for CounterTerrorist or empty for both) <br>
**Particle**: - Default: `""` (particle file to use on the effect) <br>
**Lifetime** - Default: `3` (how many seconds the the effect should last) <br>
**Sound** - Default: `""` (sound event) <br>

**tracer only:** <br>
**Color** - Default: `"random"` (value is RGB (255 255 255)) <br>
**Width** - Default: `1` (set how wide the beam should be) <br>

```json
{
  "Tracer": {
    "Enable": true,
    "Color": "random",
    "Width": 1,
    "Lifetime": 3
  },
  "Impact": {
    "Enable": true,
    "Particle": "particles/ambient_fx/aircraft_navred.vpcf"
  },
  "HitEffect": {
    "Enable": true,
    "Particle": "particles/weapons/cs_weapon_fx/weapon_taser_glow.vpcf"
  },
  "KillEffect": {
    "Enable": true,
    "Particle": "particles/explosions_fx/explosion_basic.vpcf"
  },
  "KillerEffect": {
    "Enable": true,
    "Permission": ["@css/reservation"],
    "Team": "T",
    "Particle": "particles/explosions_fx/explosion_basic.vpcf",
    "Lifetime": 3,
    "Sound": "UI.XP.Milestone_03"
  }
}
```
</details>
