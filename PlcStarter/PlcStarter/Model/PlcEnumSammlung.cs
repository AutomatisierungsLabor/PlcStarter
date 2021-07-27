namespace PlcStarter.Model
{
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
        Snap7 = 7,
        SoftwareTests = 8,
        Visu = 9
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
        CmdDateiProjektStarten = 3
    }
}