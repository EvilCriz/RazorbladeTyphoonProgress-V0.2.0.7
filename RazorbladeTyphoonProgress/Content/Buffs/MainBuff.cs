using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using System.Collections.Generic;
using Terraria;
using Microsoft.Xna.Framework;

namespace RazorbladeTyphoonProgress.Buffs;

//[RU]: Бафф для основного предмета мода.
//[RU]: Выдается игроку при использовании основного предмета.
//[RU]: Исключение: если уровень в разделе "дополнительные эффекты" меньше 1
//[RU]: В данном классе реализована активация эффетов для персонажа из категории "дополнительные эффекты"
//[RU]: Подробнее см. ниже и в классе CustomClasses.EffectsSystem
//------------------------------------------
//[EN]: Buff for the main item of the mod.
//[EN]: Given to the player when using the main item.
//[EN]: Exception: if the level in the "additional effects" section is less than 1
//[EN]: This class implements the activation of effects for the character from the "additional effects" category
//[EN]: See below and in the CustomClasses.EffectsSystem class for more details
public class MainBuff : ModBuff
{
	//[RU]: Переопределение расположения текстуры данного предмета в проекте
	//------------------------------------------
	//[EN]: Override the location of the texture for this item in the project.
	public override string Texture => "RazorbladeTyphoonProgress/Content/Buffs/" + nameof(MainBuff);

	//[RU]: Данная строка указывает, что локализованный текст по ключу "Description" не должен создаваться в файлах локализации
	//------------------------------------------
	//[EN]: This line indicates that the localized text with the key "Description" should not be created in the localization files.
	public override LocalizedText Description => LocalizedText.Empty;
	
	//[RU]: Вспомогательный класс "EffectsSystem" для категории "Дополнительные эффекты"
	//[RU]: Метод GetOrRegister обеспечивает гарантию, что будет создан всего 1 класс EffectsSystem за все время
	//[RU]: См. CustomClasses/EffectsSystem.cs
	//------------------------------------------
	//[EN]: Auxiliary class "EffectsSystem" for the "Additional Effects" category.
	//[EN]: The GetOrRegister method ensures that only 1 EffectsSystem class will be created at all times
	//[EN]: See CustomClasses/EffectsSystem.cs
	public CustCl.EffectsSystem EffectsSystem = CustCl.EffectsSystem.GetOrRegister();
	
	//[RU]: Метод Update обновляется каждый кадр для каждого персонажа, на котором находится данный баф
	//------------------------------------------
	public override void Update(Player player, ref int buffIndex)
	{
		//[RU]: Получаем экземпляр класса PlayerSave игрока, который является владельцем данного снаряда.
		//[RU]: Класс PlayerSave содержит все данные о текущем уровне, нанесенном уроне и т.д. для каждой категории
		//[RU]: Исключение: максимальный уровень категории "Дополнительные эффекты" хранится в классе "EffectsSystem"
		//[RU]: См. Common/Player/PlayerSave.cs
		//------------------------------------------
		//[EN]: Obtain an instance of the PlayerSave class for the player who owns this projectile.
		//[EN]: The PlayerSave class contains all data about the current level, damage dealt, etc. for each category.
		//[EN]: Exception: The maximum level of the "Additional Effects" category is stored in the "EffectsSystem" class.
		//[EN]: See Common/Player/PlayerSave.cs
		var playerSave = player.GetModPlayer<Common.PlayerSave>();
		
		//[RU]: Получаем максимальный уровень игрока из категории "дополнительные эффекты"
		//[RU]: Проверяем, если уровень больше максимального - приравниваем текущий уровень к максимальному.
		//[RU]: Сделано для избежания ошибок, в случае изменения уровня данной категории.
		//------------------------------------------
		//[EN]: Get the maximum level of the player from the "additional effects" category
		//[EN]: Check if the level is greater than the maximum - equate the current level to the maximum.
		//[EN]: Done to avoid errors in case of changing the level of this category.
		var level = playerSave.PlayerLevelEffect;
		level = level > EffectsSystem.MaxLevel ? EffectsSystem.MaxLevel : level;
		
		//[RU]: Перебираем массив объектов класса LevelInfo.
		//[RU]: LevelInfo - класс, содержащий информацию о каждом из уровней категории "дополнительные эффекты"
		//[RU]: См. файл CustomClasses/EffectsSystem.cs
		//------------------------------------------
		//[EN]: Iterate over the array of LevelInfo class objects.
		//[EN]: LevelInfo is a class containing information about each level of the "additional effects" category
		//[EN]: See the CustomClasses/EffectsSystem.cs file
		for(int i = 0; i < level; i++)
		{
			//[RU]: Получаем объект LevelInfo из текущей итерации массива объектов LevelInfo
			//[RU]: См. файл CustomClasses/EffectsSystem.cs
			//------------------------------------------
			//[EN]: Get the LevelInfo object from the current iteration of the LevelInfo object array
			//[EN]: See the CustomClasses/EffectsSystem.cs file
			var lvlInfo = EffectsSystem.LevelsInfo[i];
			
			//[RU]: Проверяем, присвоено ли значение делегату ActionPlayer - null
			//[RU]: Данный делегат отвечает за эффекты, которые будут воздействовать на персонажа во время действия основного (данного) баффа.
			//[RU]: Если ActionPlayer равен null - это означает, что данный уровень из категории "дополнительные эффекты"..
			//[RU]: ..предназначен не для игрока или требует отдельной реализации в другом месте.
			//[RU]: Пример отдельной релазиации смотрите в методе OnHitNPC в файле: 
			//[RU]: Content/Projectiles/ProjectileRazorbladeTyphoonProgress.cs
			//[RU]: См. файл CustomClasses/EffectsSystem.cs
			//------------------------------------------
			//[EN]: Check if the ActionPlayer delegate is assigned a value of null
			//[EN]: This delegate is responsible for the effects that will affect the character during the action of the main (given) buff.
			//[EN]: If ActionPlayer is null - this means that this level from the "additional effects"..
			//[EN]: ..category is not intended for the player or requires a separate implementation elsewhere.
			//[EN]: For an example of a separate release, see the OnHitNPC method in the file: 
			//[EN: Content/Projectiles/ProjectileRazorbladeTyphoonProgress.cs
			//[EN]: See the CustomClasses/EffectsSystem.cs file
			if(lvlInfo.ActionPlayer is null) continue;
			
			//[RU]: Перебираем массив объектов типа int - BuffIDArray.
			//[RU]: Данный массив содержит информацию о том, какие баффы добавляются (имитируются) игроку.
			//[RU]: Затем проверяем, какой из данных бафов уже имеются на игроке.
			//[RU]: Если баффы, которые имеются на игроке - снимаются.
			//[RU]: Это сделано для избежания дублирования баффов.
			//[RU]: См. файл CustomClasses/EffectsSystem.cs
			//------------------------------------------
			//[EN]: Iterate over an array of int objects - BuffIDArray.
			//[EN]: This array contains information about which buffs are added (simulated) to the player.
			//[EN]: Then we check which of these buffs the player already has.
			//[EN]: If the buffs that the player has are removed.
			//[EN]: This is done to avoid duplicating buffs.
			//[EN]: See the CustomClasses/EffectsSystem.cs file
			foreach(var buffType in lvlInfo.BuffIDArray)
				if(player.HasBuff(buffType)) player.ClearBuff(buffType);
				
			//[RU]: Вызываем делегат, активируя на игроке эффект..
			//[RU]: ..который реализован на данном уровне категори "дополнительные эффекты"
			//[RU]: См. файл CustomClasses/EffectsSystem.cs
			//------------------------------------------
			//[EU]: Call the delegate, activating the effect on the player.. 
			//[EN]: ..which is implemented at this level of the category "additional effects"
			//[EN]: See the CustomClasses/EffectsSystem.cs file
			lvlInfo.ActionPlayer(player);
		}
	}
}