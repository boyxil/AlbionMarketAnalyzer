using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Linq;
using AlbionMarket.Extensions;

namespace AlbionMarket.Model
{
	[XmlRoot("items")]
	public class ItemsRawXml : IXmlSerializable
	{
		public IEnumerable<ItemRawXml> Items = new List<ItemRawXml>();

		public XmlSchema GetSchema()
		{
			throw new NotImplementedException();

		}

		public void ReadXml(XmlReader reader)
		{
			reader.Read();
			List<ItemRawXml> result = new List<ItemRawXml>();

			while (!reader.EOF)
			{
				string name = reader.Name;
				string xmlNode = reader.ReadOuterXml();
				if (name == "farmableitem")
					result.Add(xmlNode.SerializeXmlToObject<FarmableItemXml>());
				else if (name == "simpleitem")
					result.Add(xmlNode.SerializeXmlToObject<SimpleItemXml>());
				else if (name == "consumableitem")
					result.Add(xmlNode.SerializeXmlToObject<ConsumableItemXml>());
				else if (name == "equipmentitem")
					result.Add(xmlNode.SerializeXmlToObject<EquipmentItemXml>());
				else if (name == "weapon")
					result.Add(xmlNode.SerializeXmlToObject<WeaponXml>());
				else if (name == "mount")
					result.Add(xmlNode.SerializeXmlToObject<MountXml>());
				else if (name == "furnitureitem")
					result.Add(xmlNode.SerializeXmlToObject<FurnitureItemXml>());
				else if (name == "journalitem")
					result.Add(xmlNode.SerializeXmlToObject<JournalItemXml>());
				else if (name == "consumablefrominventoryitem")
					result.Add(xmlNode.SerializeXmlToObject<ConsumableFromInventoryItemXml>());
			}
			Items = result;
		}

		public void WriteXml(XmlWriter writer)
		{
			throw new NotImplementedException();
		}
	}

	public class ItemRawXml
	{
		[XmlAttribute("uniquename")]
		public string UniqueName { get; set; }
		[XmlAttribute("shopcategory")]
		public string ShopCategory { get; set; }
		[XmlAttribute("shopsubcategory1")]
		public string ShopSubCategory { get; set; }
		[XmlAttribute("kind")]
		public string Kind { get; set; }
		[XmlAttribute("weight")]
		public string Weight { get; set; }
		[XmlAttribute("maxstacksize")]
		public int MaxStackSize { get; set; }
		[XmlAttribute("tier")]
		public int Tier { get; set; }
		[XmlAttribute("unlockedtocraft")]
		public bool UnlockedToCraft { get; set; }
	}


	//<farmableitem uniquename="T1_FARM_CARROT_SEED" tier="1" craftfamegainfactor="0.0" placecost="0" placefame="0" pickupable="false" destroyable="true" unlockedtoplace="false" maxstacksize="999" shopcategory="farmables" shopsubcategory1="seed" kind="plant" weight="0.1" unlockedtocraft="true" animationid="planting" activefarmfocuscost="1000" activefarmmaxcycles="1" activefarmactiondurationseconds="1" activefarmcyclelengthseconds="79200" activefarmbonus="2.0">
	[XmlRoot("farmableitem")]
	public class FarmableItemXml : ItemRawXml { }

	//<simpleitem uniquename="T6_MOUNTUPGRADE_GIANTSTAG_XMAS" tier="6" weight="2" maxstacksize="999" uisprite="SPECIAL_STAG_XMAS" shopcategory="products" shopsubcategory1="animals" unlockedtocraft="true" />
	[XmlRoot("simpleitem")]
	public class SimpleItemXml : ItemRawXml
	{
		//[XmlAttribute("uisprite")]
		//public bool UiSprite { get; set; }
	}

	//<consumableitem uniquename="T1_FISH_FRESHWATER_ALL_COMMON" fishingfame="10" fishingminigamesetting="T1" descriptionlocatag="@ITEMS_FISH_FRESHWATER_ALL_COMMON_DESC" uisprite="T1_FISH_FRESHWATER_ALL_COMMON" nutrition="1" abilitypower="100" slottype="food" consumespell="FOOD_FISH_FRESHWATER_1_RAW" shopcategory="consumables" shopsubcategory1="fish" resourcetype="FISH" tier="1" weight="0.05" dummyitempower="300" maxstacksize="999" unlockedtocraft="true" unlockedtoequip="true" uicraftsoundstart="Play_ui_action_craft_potion_start" uicraftsoundfinish="Play_ui_action_craft_potion_finish">
	[XmlRoot("consumableitem")]
	public class ConsumableItemXml : ItemRawXml { }

	//<equipmentitem uniquename = "T1_OFF_SHIELD" uisprite="OFF_SHIELDLARGE" maxqualitylevel="5" abilitypower="100" slottype="offhand" itempowerprogressiontype="shield" shopcategory="offhand" shopsubcategory1="shield" uicraftsoundstart="Play_ui_action_craft_metal_start" uicraftsoundfinish="Play_ui_action_craft_metal_finish" skincount="2" tier="1" weight="0.5" activespellslots="0" passivespellslots="0" physicalarmor="0" magicresistance="0" durability="1000" durabilityloss_attack="1" durabilityloss_spelluse="1" durabilityloss_receivedattack="1" durabilityloss_receivedspell="1" offhandanimationtype="shield" unlockedtocraft="true" unlockedtoequip="true" hitpointsmax="0" hitpointsregenerationbonus="0" energymax="0" energyregenerationbonus="0" crowdcontrolresistance="0" itempower="100" physicalattackdamagebonus="0" magicattackdamagebonus="0" physicalspelldamagebonus="0" magicspelldamagebonus="0" healbonus="0" bonusccdurationvsplayers="0" bonusccdurationvsmobs="0" threatbonus="0.04" magiccooldownreduction="0" bonusdefensevsplayers="0.0096" bonusdefensevsmobs="0.0096" magiccasttimereduction="0" attackspeedbonus="0" movespeedbonus="0" healmodifier="0" canbeovercharged="false" showinmarketplace="true">
	[XmlRoot("equipmentitem")]
	public class EquipmentItemXml : ItemRawXml { }

	//<weapon uniquename="T1_2H_TOOL_PICK" mesh="T1_2H_PICK" uisprite="2H_PICK" maxqualitylevel="1" abilitypower="100" slottype="mainhand" shopcategory="tools" shopsubcategory1="pickaxe" attacktype="melee" attackdamage="20" attackspeed="1" attackrange="3" twohanded="true" tier="1" weight="0.5" activespellslots="0" passivespellslots="0" durability="400" durabilityloss_attack="1" durabilityloss_spelluse="1" durabilityloss_receivedattack="1" durabilityloss_receivedspell="1" mainhandanimationtype="t_pick" unlockedtocraft="true" unlockedtoequip="true" itempower="100" unequipincombat="true" uicraftsoundstart="Play_ui_action_craft_tool_start" uicraftsoundfinish="Play_ui_action_craft_tool_finish" canbeovercharged="false">
	[XmlRoot("weapon")]
	public class WeaponXml : ItemRawXml { }

	//<mount uniquename="T2_MOUNT_MULE" maxqualitylevel="5" itempower="300" abilitypower="100" slottype="mount" shopcategory="mounts" shopsubcategory1="rare_mount" mountedbuff="T2_MOUNT_MULE_MOUNTED" halfmountedbuff="T2_MOUNT_MULE_HALFMOUNTED" tier="2" weight="45" activespellslots="0" passivespellslots="0" durability="8000" durabilityloss_attack="0" durabilityloss_spelluse="0" durabilityloss_receivedattack="0" durabilityloss_receivedspell="0" durabilityloss_receivedattack_mounted="1" durabilityloss_receivedspell_mounted="5" durabilityloss_mounting="0" unlockedtocraft="true" unlockedtoequip="true" mounttime="3" dismounttime="0" mounthitpointsmax="238" mounthitpointsregeneration="2.38" prefabname="MOUNT_BEGINNERMULE_01" prefabscaling="1.1" despawneffect="FX/ClientPrefabs/demount_fx_medium" despawneffectscaling="1" remountdistance="2" halfmountrange="12" forceddismountcooldown="30" forceddismountspellcooldown="1" fulldismountcooldown="30" remounttime="0.5" uicraftsoundstart="Play_ui_action_craft_mount_start" uicraftsoundfinish="Play_mount_mounted" dismountbuff="DISMOUNTED">
	[XmlRoot("mount")]
	public class MountXml : ItemRawXml { }

	//<furnitureitem uniquename="T4_FURNITUREITEM_REPAIRKIT" shopcategory="furniture" shopsubcategory1="repairkit" tier="4" durability="1" durabilitylossperdayfactor="40" weight="60" unlockedtocraft="true" placeableindoors="true" placeableoutdoors="true" placeableindungeons="true" accessrightspreset="public" uicraftsoundstart="Play_ui_action_craft_refine_wood_start" uicraftsoundfinish="Play_ui_action_craft_refine_wood_finish">
	[XmlRoot("furnitureitem")]
	public class FurnitureItemXml : ItemRawXml { }

	//<journalitem salvageable = "false" uniquename="T5_JOURNAL_STONE" tier="5" maxfame="3600" baselootamount="32" shopcategory="products" shopsubcategory1="journal" weight="0.51" unlockedtocraft="true" craftfamegainfactor="0" fasttravelfactor="411">
	[XmlRoot("journalitem")]
	public class JournalItemXml : ItemRawXml { }

	//<consumablefrominventoryitem uniquename="UNIQUE_AVATARRING_ADC_MAR2019" uisprite="UNIQUE_AVATARRING_UNLOCK" tradable="false" consumespell="UNIQUE_AVATARRING_ADC_MAR2019_SPELL" abilitypower="100" shopcategory="consumables" shopsubcategory1="other" tier="6" enchantmentlevel="0" weight="1" unlockedtocraft="true" itemvalue="0" />
	[XmlRoot("consumablefrominventoryitem")]
	public class ConsumableFromInventoryItemXml : ItemRawXml { }
}
