using FitTrack.Domain.Notifications;
using FitTrack.Domain.Validations.Interfaces;

namespace FitTrack.Domain.Validations;

// A classe ContractValidations é responsável por centralizar as validações de contrato para as entidades que implementam a interface IContract.
// Em DDD, as validações ajudam a garantir que as regras de negócio sejam respeitadas e que as entidades estejam em um estado válido.
public partial class ContractValidations<T> where T : IContract
{
    // Lista privada para armazenar as notificações de validação.
    // Cada notificação representa uma falha de validação.
    private readonly List<Notification> _notifications;

    // Construtor da classe que inicializa a lista de notificações.
    // As validações podem ser acumuladas e avaliadas posteriormente.
    public ContractValidations()
    {
        _notifications = [];
    }

    // Propriedade que expõe a lista de notificações como uma coleção de somente leitura.
    // Isso evita que o chamador modifique diretamente a lista de notificações, mantendo o encapsulamento.
    public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();

    // Método para adicionar uma nova notificação de validação à lista.
    // Esse método permite que as validações sejam acumuladas e analisadas em conjunto.
    public void AddNotification(Notification notification) => _notifications.Add(notification);

    // Método que verifica se o contrato de validação é válido.
    // O contrato é considerado válido se não houver notificações, ou seja, se todas as regras de validação foram atendidas.
    public bool IsValid() => _notifications.Count == 0;
}
