using System.Collections.Generic;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System;

namespace RazorbladeTyphoonProgress.CustCl;

//[RU]: Вспомогательный класс "EffectsSystem" для категории "Дополнительные эффекты" основного предмета мода
//------------------------------------------
//[EN]: Auxiliary class "EffectsSystem" for the "Additional Effects" category of the main item in the mod
public class EffectsSystem
{
	//[RU]: Вспомогательный объект класса "EffectsSystem"
	//[RU]: Объект EffectsSystem обеспечивает гарантию, что будет создан всего 1 класс EffectsSystem за все время
	//[RU]: См. метод GetOrRegister ниже
	//------------------------------------------
	//[EN]: Helper object of class "EffectsSystem"
	//[EN]: The EffectsSystem object ensures that only 1 EffectsSystem class will be created at all times
	//[EN]: See the GetOrRegister method below
	private static EffectsSystem SingleObject = null;
	public const int BuffTime = 60 * 60;
	
	//[RU]: Максимальный уровень категории "Дополнительные эффекты"
	//------------------------------------------
	//[EN]: Maximum level of the "Additional Effects" category
    public int MaxLevel = 0;
	
	
	//[RU]: LevelInfo - класс, содержащий информацию о каждом из уровней категории "дополнительные эффекты"
	//[RU]: См. конец данного файла
	//------------------------------------------
	//[EN]: LevelInfo - class containing information about each level of the "additional effects" category
	//[EN]: See the end of this file
	public LevelInfo[] LevelsInfo = null;
	
	//[RU]: Запрещаем вызов конструктора (т.е. создание экземпляра данного класса) вне данного класса
	//------------------------------------------
	//[EN]: Prohibiting the invocation of the constructor (i.e., creating an instance of this class) outside of this class
    private EffectsSystem(){}

	//[RU]: Вспомогательный класс "EffectsSystem" для категории "Дополнительные эффекты"
	//[RU]: Метод GetOrRegister обеспечивает гарантию, что будет создан всего 1 класс EffectsSystem за все время
	//------------------------------------------
	//[EN]: Auxiliary class "EffectsSystem" for the "Additional Effects" category.
	//[EN]: The GetOrRegister method ensures that only 1 EffectsSystem class will be created at all times
    public static EffectsSystem GetOrRegister()
    {
		//[RU]: Проверяем, если SingleObject НЕ равно null - значит, объект данного класса уже создавался до этого.
		//[RU]: В этом случае мы возвращаем SingleObject
		//------------------------------------------
		//[EN]: Check if SingleObject is NOT null - this means that an object of this class has already been created before.
		//[EN]: In this case, we return SingleObject
		if(SingleObject is not null) return SingleObject;
		
		//[RU]: Если SingleObject равен null - значит, объект данного класса никогда ранее не создавался.
		//[RU]: В этом случае мы выделяем пать и инициилизируем объект данного класса.
		//------------------------------------------
		//[EN]: If SingleObject is null, then the object of this class has never been created before.
		//[EN]: In this case, we allocate a group and initialize the object of this class.
		SingleObject = new();
		
		//[RU]: Инициилизируем массив объектов LevelInfo переменной LevelsInfo объекта SingleObject
		//[RU]: В данном месте созданы все эффекты категории "дополнительные эффекты"
		//[RU]: Более подробное описание смотрите в конце данного файла в классе LevelInfo
		//------------------------------------------
		//[EN]: Initialize the array of LevelInfo objects of the LevelsInfo variable of the SingleObject object
		//[EN]: In this place, all effects of the "additional effects" category are created
		//[EN]: For a more detailed description, see the LevelInfo class at the end of this file
        SingleObject.LevelsInfo = new LevelInfo[] {
			new LevelInfo() { 
				BuffIDArray = new[] { BuffID.NightOwl },
				ActionPlayer = player => player.nightVision = true
			},
			new LevelInfo() { 
				BuffIDArray = new[] { BuffID.Shine },
				ActionPlayer = player => Lighting.AddLight((int)((double)player.position.X + (double)(player.width / 2)) / 16, (int)((double)player.position.Y + (double)(player.height / 2)) / 16, 0.8f, 0.95f, 1f)
			},
			new LevelInfo() { 
				BuffIDArray = new[] { BuffID.Spelunker },
				ActionPlayer = (Player player) => player.findTreasure = true
			},
			new LevelInfo() { 
				BuffIDArray = new[] { BuffID.Swiftness },
				ActionPlayer = player => player.moveSpeed += 0.25f
			},
			new LevelInfo() { 
				BuffIDArray = new[] { BuffID.Regeneration },
				ActionPlayer = (Player player) => player.lifeRegen += 4
			},
			new LevelInfo() { 
				BuffIDArray = new[] { BuffID.Ironskin },
				ActionPlayer = (Player player) => { 
					player.statDefense += 8;
					if (ModLoader.TryGetMod("CalamityMod", out Mod Calamity))
					{
						if ((bool)Calamity.Call("Downed", "dog")) player.statDefense += 12;
						else if (NPC.downedMoonlord) player.statDefense += 8;
						else if (Main.hardMode) player.statDefense += 4;
					}
				}
			},
			new LevelInfo() { 
				BuffIDArray = new[] { BuffID.Ichor },
				ActionNPC = target => target.AddBuff(BuffID.Ichor, BuffTime)
			},
			new LevelInfo() { 
				BuffIDArray = new[] { BuffID.ObsidianSkin },
				ActionPlayer = (Player player) => { 
					player.lavaImmune = true; 
					player.fireWalk = true;
				}
			},
			new LevelInfo() { 
				BuffIDArray = new[] { BuffID.Gills },
				ActionPlayer = player => player.gills = true
			},
			new LevelInfo() { 
				BuffIDArray = new[] { BuffID.MagicPower },
				ActionPlayer = player => player.GetDamage(DamageClass.Magic) += 0.2f
			},
			new LevelInfo() { 
				BuffIDArray = new[] { BuffID.ManaRegeneration },
				ActionPlayer = (Player player) => player.manaRegenBuff = true
			},
			new LevelInfo() { 
				IsNoCollide = true
			},
			new LevelInfo() { 
				BuffIDArray = new[] { BuffID.Rage },
				ActionPlayer = player => player.GetCritChance(DamageClass.Generic) += 10
			},
			new LevelInfo() {
				CountHealPercent = 0.25f
			},
			new LevelInfo() {
				CountHealPercent = 0.25f
			},
			new LevelInfo() {
				CountHealPercent = 0.5f
			},
			new LevelInfo() {
				CountHealPercent = 0.5f
			},
			new LevelInfo() {
				CountHealPercent = 1f
			}
		};
		
        SingleObject.MaxLevel = SingleObject.LevelsInfo.Length;
		
		for(int i = 0; i < SingleObject.MaxLevel; i++)
			SingleObject.LevelsInfo[i].Level = i + 1;
		
        return SingleObject;
    }

	//[RU]: Возвращает на каком уровне категори "Дополнительные эффекты" открывается доступ к усиление "Снаряды проходят сквозь блоки"
	//------------------------------------------
	//[EN]: Returns at which level the "Additional Effects" category unlocks access to the "Projectiles pass through blocks" enhancement.
    public int GetNoCollideLevel()
    {
		foreach(var lvlInfo in LevelsInfo)
			if(lvlInfo.IsNoCollide)
				return lvlInfo.Level;
        return -1;
    }
}

public class LevelInfo()
{
	//[RU]: Указывает, на каком уровне категории "дополнительные эффекты" открывается эффект текущего экземпляра класса
	//------------------------------------------
	//[EN]: Specifies at what level of the "additional effects" category the effect of the current class instance is unlocked
	public int Level = -1;
	
	//[RU]: Указывает на то, какой тип баффа имитирует эффект текущего экземпляра класса
	//[RU]: Используется для предотвращения дублирования баффов (см. Content.Buffs.MainBuff.cs)
	//[RU]: Используется для корректной локализации текста
	//[RU]: См. Content/Items/RazorbladeTyphoonProgress.cs (метод ModifyTooltips)
	//------------------------------------------
	//[EN]: Specifies what type of buff the current class instance's effect mimics
	//[EN]: Used to prevent buff duplication (see Content.Buffs.MainBuff.cs)
	//[EN]: Used to localize text correctly
	//[EN]: See Content/Items/RazorbladeTyphoonProgress.cs (ModifyTooltips method)
	public int[] BuffIDArray = Array.Empty<int>();
	
	//[RU]: Данный делегат отвечает за эффекты, которые будут воздействовать на персонажа во время действия основного баффа.
	//[RU]: Если ActionPlayer равен null - это означает, что данный уровень из категории "дополнительные эффекты"..
	//[RU]: ..предназначен не для игрока или требует отдельной реализации в другом месте.
	//[RU]: Пример отдельной релазиации смотрите в методе OnHitNPC в файле: 
	//[RU]: Content/Projectiles/ProjectileRazorbladeTyphoonProgress.cs
	//------------------------------------------
	//[EN]: This delegate is responsible for the effects that will affect the character during the action of the main buff.
	//[EN]: If ActionPlayer is null - this means that this level from the "additional effects"..
	//[EN]: ..category is not intended for the player or requires a separate implementation elsewhere.
	//[EN]: For an example of a separate release, see the OnHitNPC method in the file: 
	//[EN: Content/Projectiles/ProjectileRazorbladeTyphoonProgress.cs
	public Action<Player> ActionPlayer = null;
	
	//[RU]: Данный делегат отвечает за эффекты, которые будут воздействовать на NPC после попадания по нему данным снарядом.
	//[RU]: Если ActionNPC равен null - это означает, что данный уровень из категории "дополнительные эффекты"..
	//[RU]: ..предназначен не для NPC.
	//------------------------------------------
	//[EN]: This delegate is responsible for the effects that will affect the NPC after being hit by this projectile.
	//[EN]: If ActionNPC is null, this means that this level is from the "additional effects" category..
	//[EN]: ..is not intended for NPCs.
	public Action<NPC> ActionNPC = null;
	
	//[RU]: Указывает является ли усиление подкатегорией "Снаряды данного оружия могут проходить сквозь блоки"
	//------------------------------------------
	//[EN]: Indicates whether the buff is a subcategory of "This weapon's projectiles can pass through blocks"
	public bool IsNoCollide = false;
	
	//[RU]: Указывает, активируется ли эффект текущего экземпляра класса на горячую клавишу
	//------------------------------------------
	//[EN]: Specifies whether the effect of the current class instance is activated by a hotkey
	public bool IsUseKeybind = false;
	
	//[RU]: CountHealPercent отвечает за подкатегорию категории "дополнильные эффекты":
	//[RU]: "Процент исцеления здоровья от нанесеного урона"
	//[RU]: Суммируется все значения CountHealPercent открытых у игрока на данном уровне
	//------------------------------------------
	//[EN]: CountHealPercent is responsible for the subcategory of the "additional effects" category:
	//[EN]: "Percentage of health healed from damage dealt"
	//[EN]: All CountHealPercent values ​​open to the player at this level are summed up
	public float CountHealPercent = 0;
}