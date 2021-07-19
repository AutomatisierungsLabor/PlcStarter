namespace PlcStarter.Model
{
    public  interface IPlc
    {
        public string OrdnerSource { get; set; }
        public string OrdnerDestination { get; set; }

        void setOrdnerSource(string source);
        void setOrdnerDestination(string destination);
    }
}