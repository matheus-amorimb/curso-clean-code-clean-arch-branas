namespace RideApp.Domain.UseCases;

public interface IUseCase<in TInput, TOutput>
{
    Task<TOutput> Execute(TInput input);
}