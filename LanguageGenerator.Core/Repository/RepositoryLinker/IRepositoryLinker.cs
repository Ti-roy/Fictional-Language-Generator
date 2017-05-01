namespace LanguageGenerator.Core.Repository.RepositoryLinker
{
    public interface IRepositoryLinker
    {
        bool IsRepositoryLinked(ISyntacticUnitRepository repository);
        void LinkRepository(ISyntacticUnitRepository repository);
    }
}