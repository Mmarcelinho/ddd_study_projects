using FitTrack.Domain.Notifications;
using FitTrack.Domain.Validations.Interfaces;

namespace FitTrack.Domain.Contexts.User.Abstractions;

// A classe Entity representa uma entidade de domínio base com um identificador único e notificações de validação.
// Em DDD, uma entidade é um objeto que possui uma identidade distinta e é rastreado pelo seu ciclo de vida.
public abstract class Entity : IValidate
{
    // Lista de notificações para armazenar mensagens de validação e erros.
    private List<Notification> _notifications;

    // Construtor protegido para garantir que apenas classes derivadas possam instanciar uma entidade.
    // Inicializa o identificador da entidade e a data de criação.
    protected Entity()
    {
        // O Id é gerado como um GUID único para garantir a identidade exclusiva da entidade.
        Id = Guid.NewGuid();
        // A data de criação é definida para o momento atual em UTC.
        DateCreatedUtc = DateTime.UtcNow;
        // Inicializa a lista de notificações.
        _notifications = new List<Notification>();
    }

    // Propriedade para obter o identificador único da entidade.
    public Guid Id { get; private set; }

    // Propriedade para obter a data de criação da entidade em UTC.
    public DateTime DateCreatedUtc { get; private set; }

    // Propriedade para obter a lista de notificações como uma coleção somente leitura.
    public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();

    // Método para definir a lista de notificações.
    // Em DDD, isso permite que a entidade agregue notificações de validação durante a validação de regras de negócios.
    public void SetNotificationList(List<Notification> notifications) => _notifications = notifications;

    // Método abstrato para validação que deve ser implementado por entidades derivadas.
    // Em DDD, cada entidade deve ser capaz de validar-se com base nas regras de negócio específicas.
    public abstract bool Validate();
}
