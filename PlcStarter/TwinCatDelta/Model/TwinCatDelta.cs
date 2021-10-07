namespace TwinCatDelta.Model
{
    public class TwinCatDelta
    {

        public readonly MainWindow MainWindow;

        public TwinCatDelta(MainWindow mw)
        {
            MainWindow = mw;
        }
        private void OrdnerStrukturAnpassen()
        {
            //
        }
        internal void TasterStart() => System.Threading.Tasks.Task.Run(OrdnerStrukturAnpassen);
    }
}