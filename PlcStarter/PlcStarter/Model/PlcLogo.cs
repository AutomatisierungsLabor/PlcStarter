namespace PlcStarter.Model
{
    public class PlcLogo : IPlc
    {
        public string OrdnerSource { get; set; }
        public string OrdnerDestination { get ; set ; }

        public void setOrdnerSource(string source) => OrdnerSource = source;
        public void setOrdnerDestination(string destination) => OrdnerDestination = destination;
    }
}
