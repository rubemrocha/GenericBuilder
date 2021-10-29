namespace GenericBuilder
{
    //  Definição da interface de um Builder genérico
    public interface IBuilder<out TResult>
    {
        TResult Build();
    }

    public interface IBuilder<TInput, out TResult>
    {
        TResult Build();
    }
}
