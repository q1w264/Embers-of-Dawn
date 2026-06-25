namespace UI.UIBehavior
{
    public interface IUIBehavior<in T>
    {
        public void Bind(T element);
        
        public void Unbind(T element);
    }
}