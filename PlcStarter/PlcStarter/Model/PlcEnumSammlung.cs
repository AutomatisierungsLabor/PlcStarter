namespace PlcStarter.Model;

public enum OrdnerBezeichnungen
{
    DigitalTwin = 0,
    FactoryIo = 1,
    Logo = 2,
    TiaPortal = 3,
    TwinCat = 4
}
public enum PlcSoftwareVersion
{
    // ReSharper disable UnusedMember.Global
    None = 0,
    Logo8 = 1,
    TiaPortalV14Sp1 = 2,
    TiaPortalV17Sp1 = 3,
    TwinCatV3 = 4
    // ReSharper restore UnusedMember.Global
}
public enum Steuerungen
{
    Logo = 0,
    TiaPortal = 1,
    TwinCat = 2
}
public enum PlcKategorie
{
    // ReSharper disable UnusedMember.Global
    AdsRemote = 0,
    Bug = 1,
    DigitalTwin = 2,
    FactoryIo = 3,
    Hmi = 4,
    Nc = 5,
    Plc = 6,
    AutoTests = 7,
    Visu = 8
    // ReSharper restore UnusedMember.Global
}
public enum PlcSprachen
{
    As = 0,
    Awl = 1,
    Cfc = 2,
    Cpp = 3,
    Fup = 4,
    Kop = 5,
    Scl = 6,
    St = 7
}
public enum PlcJobs
{
    None = 0,
    ProjektKopieren = 2,
    ProjektStarten = 3,
    DigitalTwinKopieren = 4,
    DigitalTwinStarten = 5,
    FactoryIoKopieren = 6,
    FactoryIoStarten = 7,
    TemplateOrdnerKopieren = 8,
    DeltaOrdnerKopieren = 9
}