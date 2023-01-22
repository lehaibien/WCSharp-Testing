function InitGlobals()
end

function CreateAllItems()
    local itemID
    CreateItem(FourCC("I000"), -226.0, -157.8)
    CreateItem(FourCC("I001"), -282.2, -332.7)
    CreateItem(FourCC("tgxp"), 123.2, -360.2)
    CreateItem(FourCC("tgxp"), 91.5, -325.6)
end

function CreateUnitsForPlayer0()
    local p = Player(0)
    local u
    local unitID
    local t
    local life
    u = CreateUnit(p, FourCC("H000"), -168.2, -8.5, 135.037)
    u = CreateUnit(p, FourCC("H001"), -36.4, -237.2, 37.245)
    u = CreateUnit(p, FourCC("hfoo"), -121.0, -365.2, 86.355)
end

function CreateNeutralHostile()
    local p = Player(PLAYER_NEUTRAL_AGGRESSIVE)
    local u
    local unitID
    local t
    local life
    u = CreateUnit(p, FourCC("nftb"), -484.0, 402.1, 66.535)
    u = CreateUnit(p, FourCC("nftb"), -536.4, 291.7, 132.763)
    u = CreateUnit(p, FourCC("nftb"), -419.5, 510.3, 335.566)
    u = CreateUnit(p, FourCC("nftb"), -549.6, 483.6, 135.026)
    u = CreateUnit(p, FourCC("nftb"), -662.3, 422.9, 114.437)
    u = CreateUnit(p, FourCC("nftb"), -717.0, 336.7, 114.899)
    u = CreateUnit(p, FourCC("nftb"), -713.6, 262.4, 19.919)
    u = CreateUnit(p, FourCC("nftb"), -652.5, 192.9, 176.600)
    u = CreateUnit(p, FourCC("nftb"), -567.9, 166.5, 286.323)
    u = CreateUnit(p, FourCC("nftb"), -493.1, 194.4, 203.792)
    u = CreateUnit(p, FourCC("nftb"), -457.0, 295.2, 136.487)
    u = CreateUnit(p, FourCC("nftb"), -491.1, 510.3, 156.208)
    u = CreateUnit(p, FourCC("nftb"), -595.1, 568.6, 13.569)
    u = CreateUnit(p, FourCC("nftb"), -720.2, 574.1, 101.451)
    u = CreateUnit(p, FourCC("nftb"), -772.1, 497.8, 311.142)
    u = CreateUnit(p, FourCC("nftb"), -762.4, 389.2, 95.551)
end

function CreatePlayerBuildings()
end

function CreatePlayerUnits()
    CreateUnitsForPlayer0()
end

function CreateAllUnits()
    CreatePlayerBuildings()
    CreateNeutralHostile()
    CreatePlayerUnits()
end

function InitCustomPlayerSlots()
    SetPlayerStartLocation(Player(0), 0)
    SetPlayerColor(Player(0), ConvertPlayerColor(0))
    SetPlayerRacePreference(Player(0), RACE_PREF_HUMAN)
    SetPlayerRaceSelectable(Player(0), true)
    SetPlayerController(Player(0), MAP_CONTROL_USER)
end

function InitCustomTeams()
    SetPlayerTeam(Player(0), 0)
end

function main()
    SetCameraBounds(-7424.0 + GetCameraMargin(CAMERA_MARGIN_LEFT), -7680.0 + GetCameraMargin(CAMERA_MARGIN_BOTTOM), 7424.0 - GetCameraMargin(CAMERA_MARGIN_RIGHT), 7168.0 - GetCameraMargin(CAMERA_MARGIN_TOP), -7424.0 + GetCameraMargin(CAMERA_MARGIN_LEFT), 7168.0 - GetCameraMargin(CAMERA_MARGIN_TOP), 7424.0 - GetCameraMargin(CAMERA_MARGIN_RIGHT), -7680.0 + GetCameraMargin(CAMERA_MARGIN_BOTTOM))
    SetDayNightModels("Environment\\DNC\\DNCLordaeron\\DNCLordaeronTerrain\\DNCLordaeronTerrain.mdl", "Environment\\DNC\\DNCLordaeron\\DNCLordaeronUnit\\DNCLordaeronUnit.mdl")
    NewSoundEnvironment("Default")
    SetAmbientDaySound("LordaeronSummerDay")
    SetAmbientNightSound("LordaeronSummerNight")
    SetMapMusic("Music", true, 0)
    CreateAllItems()
    CreateAllUnits()
    InitBlizzard()
    InitGlobals()
end

function config()
    SetMapName("TRIGSTR_006")
    SetMapDescription("TRIGSTR_008")
    SetPlayers(1)
    SetTeams(1)
    SetGamePlacement(MAP_PLACEMENT_USE_MAP_SETTINGS)
    DefineStartLocation(0, 128.0, 0.0)
    InitCustomPlayerSlots()
    SetPlayerSlotAvailable(Player(0), MAP_CONTROL_USER)
    InitGenericPlayerSlots()
end

