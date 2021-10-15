namespace PlcStarter.Model
{
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
        None = 0,
        Logo8 = 1,
        TiaPortalV14Sp1 = 2,
        TwinCatV3 = 3
    }
    public enum Steuerungen
    {
        Logo = 0,
        TiaPortal = 1,
        TwinCat = 2
    }
    public enum PlcKategorie
    {
        AdsRemote = 0,
        Bug = 1,
        DigitalTwin = 2,
        FactoryIo = 3,
        Hmi = 4,
        Nc = 5,
        Plc = 6,
        AutoTests = 7,
        Visu = 8
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
        SorceOrdnerErstellen = 1,
        ProjektKopieren = 2,
        CmdDateiProjektStarten = 3,
        DigitalTwinKopieren = 4,
        DigitalTwinStartenBeckhoff = 5,
        DigitalTwinStartenSiemens = 6,
        FactoryIoKopieren = 7,
        FachtoryIoStarten = 8,
        TemplateOrdnerKopieren = 9,
        DiffOrdnerKopieren = 10
    }
}