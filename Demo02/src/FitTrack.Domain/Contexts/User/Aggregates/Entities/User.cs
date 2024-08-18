using FitTrack.Domain.Contexts.User.Abstractions;
using FitTrack.Domain.Contexts.User.Aggregates.ValueObjects;
using FitTrack.Domain.Contexts.User.Errors;
using FitTrack.Domain.Contexts.User.Exceptions;
using FitTrack.Domain.Notifications;
using FitTrack.Domain.Validations;
using FitTrack.Domain.Validations.Interfaces;

namespace FitTrack.Domain.Contexts.User.Aggregates.Entities;

// A classe User representa uma entidade agregada dentro do domínio de usuários.
// Em DDD, uma entidade agregada é um agrupamento de objetos de valor e outras entidades 
// que são tratadas como uma unidade para garantir consistência nas regras de negócio.
public sealed class User : Entity, IContract
{
    // Listas privadas que armazenam os seguidores, os usuários que este usuário segue,
    // as atividades seguidas, as atividades criadas e os treinos do usuário.
    // Esses dados são encapsulados dentro da entidade agregada para manter a coesão
    // e controlar a lógica de negócio associada.
    private readonly List<User> _follower;
    private readonly List<User> _following;
    private readonly List<Activity.Aggregates.Entities.Activity> _activitiesFollowed;
    private readonly List<Activity.Aggregates.Entities.Activity> _activities;
    private readonly List<Workout.Aggregates.Entities.Workout> _workouts;

    // Construtor privado para restringir a criação de instâncias diretamente,
    // reforçando o uso de métodos de fábrica para garantir que a entidade seja
    // criada em um estado válido e consistente.
    private User(Name name, Email email)
    {
        Name = name;
        Email = email;
        _follower = [];
        _following = [];
        _activities = [];
        _activitiesFollowed = [];
        _workouts = [];
    }

    // Propriedades que expõem os valores dos objetos de valor Name e Email.
    // A encapsulação através de getters privados garante que as propriedades
    // só possam ser modificadas dentro da entidade, respeitando as regras de negócio.
    public Name Name { get; private set; }
    public Email Email { get; private set; }

    // Propriedades que expõem as coleções como somente leitura para o exterior,
    // garantindo que a integridade das coleções seja mantida e que modificações
    // sejam realizadas apenas através de métodos controlados pela entidade.
    public IReadOnlyCollection<User> Follower => _follower.AsReadOnly();
    public IReadOnlyCollection<User> Following => _following.AsReadOnly();
    public IReadOnlyCollection<Activity.Aggregates.Entities.Activity> ActivitiesFollowed => _activitiesFollowed.AsReadOnly();
    public IReadOnlyCollection<Activity.Aggregates.Entities.Activity> Activities => _activities.AsReadOnly();
    public IReadOnlyCollection<Workout.Aggregates.Entities.Workout> Workouts => _workouts.AsReadOnly();

    // Método de fábrica que encapsula a criação de uma nova instância de User.
    // Esse padrão é utilizado para garantir que a entidade seja criada em um estado
    // válido, aplicando as regras de negócio necessárias durante a construção.
    public static User Create(Name name, Email email) => new(name, email);

    // Método para validar as regras de negócio associadas à entidade User.
    // Em DDD, a validação é um passo essencial para assegurar que as invariantes do
    // domínio sejam respeitadas, impedindo estados inválidos na aplicação.
    public override bool Validate()
    {
        var contracts = new ContractValidations<User>()
            .FirstNameIsOk(Name, 5, 20, DomainErrors.USER_FIRSTNAME, nameof(Name.FirstName))
            .LastNameIsOk(Name, 5, 20, DomainErrors.USER_LASTNAME, nameof(Name.FirstName))
            .EmailIsValid(Email, DomainErrors.USER_EMAIL, nameof(Email.Value));

        SetNotificationList(contracts.Notifications as List<Notification>);
        return contracts.IsValid();
    }

    // Método público para seguir outro usuário. 
    // Este método encapsula a lógica de adicionar um usuário à lista de "seguindo",
    // assegurando que as regras de negócio associadas sejam aplicadas corretamente.
    public void FollowUser(User user) => AddFollowing(user);

    // Método privado responsável por adicionar um usuário à lista de "seguindo".
    // A lógica de negócio é mantida dentro da entidade para garantir a consistência.
    internal void AddFollowing(User userToFollow) => _following.Add(userToFollow);

    // Método privado responsável por adicionar um seguidor à lista de seguidores.
    // A lógica é encapsulada dentro da entidade para evitar inconsistências.
    internal void AddFollower(User follower)
    {
        if (_following.Contains(follower))
            throw new ValidationException(DomainErrors.USER_ALREADY_FOLLOWED);

        _follower.Add(follower);
    }

    // Método público para adicionar uma atividade ao usuário.
    // Garante que a lógica de negócio seja aplicada antes de incluir a atividade,
    // prevenindo duplicações ou estados inválidos no domínio.
    public void AddActivity(Activity.Aggregates.Entities.Activity activity)
    {
        if (_activities.Contains(activity))
            throw new ValidationException(DomainErrors.USER_ALREADY_CONTAIN_ACTIVITY);

        _activities.Add(activity);
    }

    // Método interno para seguir uma atividade, respeitando as regras de negócio.
    internal void FollowActivity(Activity.Aggregates.Entities.Activity activity)
    {
        if (_activitiesFollowed.Contains(activity))
            throw new ValidationException(DomainErrors.USER_ALREADY_FOLLOW_ACTIVITY);

        _activitiesFollowed.Add(activity);
    }

    // Método público para adicionar um treino ao usuário.
    // Este método assegura que não haja duplicação de treinos, 
    // respeitando a integridade do domínio antes de adicionar à coleção.
    public void AddWorkout(Workout.Aggregates.Entities.Workout workout)
    {
        if (_workouts.Contains(workout))
            throw new ValidationException(DomainErrors.USER_ALREADY_CONTAIN_WORKOUT);

        _workouts.Add(workout);
    }
}
