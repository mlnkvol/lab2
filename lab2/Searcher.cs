namespace lab2
{
    internal class Searcher
    {
        private Scientists scientists;
        private IStrategy analyzer;

        public Searcher(Scientists scientists, IStrategy analyzer)
        {
            this.scientists = scientists;
            this.analyzer = analyzer;
        }

        internal List<Scientists> SearchAlgorithm()
        {
            throw new NotImplementedException();
        }
    }
}