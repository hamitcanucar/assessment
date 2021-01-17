using ExampleProject.DataAccess.Entities;

namespace ExampleProject.Models.ControllerModels
{
    public abstract class AControllerEntityModel<TEntityModel>
    {
        public abstract TEntityModel ToModel();
    }
}