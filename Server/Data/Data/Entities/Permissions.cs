using DestallMaterials.CodeGeneration.ERP.ClientDependency;

namespace Data.Entities
{
    [PermissionsClass]
    public partial class Permissions : Entity
    {
        public bool TechnicalAdministrating { get; set; }
    }
}
