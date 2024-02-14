using EM.Domain.Interface;

namespace EM.Repository
{
    public abstract class RepositorioAbstrato<T> where T : IEntidade
    {
        public abstract void Adicione(T obj);
        public abstract void Remova(T obj);
        public abstract void Atualize(T obj);
        public abstract IEnumerable<T> ObtenhaTodos();
        public abstract T Obtenha(string obj);
    }
}
